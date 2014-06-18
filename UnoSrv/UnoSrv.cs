using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Nito.Async.Sockets;
using Nito.Async;
using System.Collections;
using UnoC;

namespace UnoSrv
{
    class UnoSrv
    {
        private SimpleServerTcpSocket ListeningSocket;
        private List<SimpleServerChildTcpSocket> clients;
        private List<bool> bclients;
        private int clientsToUse;
        private bool bStart = false;
        private bool bCardTaken = false;
        private bool bEndRound = false;
        private bool bEndGame = false;
        private bool bDirection = false; //false = clockvise
        private int iTake = 0;
        private int clientIsOn = 0;
        private UnoCards cards;
        private Stack<UnoCard> cDeck = new Stack<UnoCard>();

        public UnoSrv(int Clients)
        {
            this.cards = new UnoCards();
            this.clients = new List<SimpleServerChildTcpSocket>();
            this.bclients = new List<bool>();
            this.clientsToUse = Clients;
        }

        private void ListeningSocket_ConnectionArrived(AsyncResultEventArgs<SimpleServerChildTcpSocket> e)
        {
            // Check for errors
            if (e.Error != null)
            {
                ResetListeningSocket();
                return;
            }
            SimpleServerChildTcpSocket socket = e.Result;

            try
            {
                // Save the new child socket connection
                clients.Add(socket);
                bclients.Add(false);
                socket.PacketArrived += (args) => ChildSocket_PacketArrived(socket, args);
                socket.ShutdownCompleted += (args) => ChildSocket_ShutdownCompleted(socket, args);
                // Display the connection information
                //textBoxLog.AppendText("Connection established to " + socket.RemoteEndPoint.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                ResetChildSocket(socket);
                //textBoxLog.AppendText("Socket error accepting connection: [" + ex.GetType().Name + "] " + ex.Message + Environment.NewLine);
            }
            finally
            {
                //RefreshDisplay();
            }
        }

        private void ChildSocket_PacketArrived(SimpleServerChildTcpSocket socket, AsyncResultEventArgs<byte[]> e)
        {
            try
            {
                // Check for errors
                if (e.Error != null)
                {
                    //textBoxLog.AppendText("Client socket error during Read from " + socket.RemoteEndPoint.ToString() + ": [" + e.Error.GetType().Name + "] " + e.Error.Message + Environment.NewLine);
                    ResetChildSocket(socket);
                }
                else if (e.Result == null)
                {
                    // PacketArrived completes with a null packet when the other side gracefully closes the connection
                    //textBoxLog.AppendText("Socket graceful close detected from " + socket.RemoteEndPoint.ToString() + Environment.NewLine);

                    // Close the socket and remove it from the list
                    ResetChildSocket(socket);
                }
                else
                {
                    // Handle the message
                    UnoCard msg = (UnoCard)Util.Deserialize(e.Result);
                    if (!bStart)
                    {
                        if (socket.LocalEndPoint.Address == ListeningSocket.LocalEndPoint.Address && msg == UnoCard.StartRound)
                            bStart = true;
                    }
                    else if (socket == clients[clientIsOn])
                    {
                        if (msg == UnoCard.EndRound)
                        {
                            bEndRound = true;
                        }
                        else if (msg == UnoCard.GiveCard && !bCardTaken)
                        {
                            bCardTaken = true;
                            socket.WriteAsync(Util.Serialize(this.GetCard()));
                        }
                        else if (msg == UnoCard.Uno)
                        {
                            bclients[clientIsOn] = true;
                        }
                        else if (msg == UnoCard.Won)
                        {
                            if (bclients[clientIsOn] == true)
                                bEndGame = true;
                        }
                        else
                        {
                            if (msg.Number == (int)UnoC.SpecialCards.Skip)
                            {
                                if (bDirection)
                                    clientIsOn = ((clientIsOn - 1) + clients.Count) % clients.Count;
                                else
                                    clientIsOn = (clientIsOn + 1) % clients.Count;
                            }
                            else if (msg.Number == (int)UnoC.SpecialCards.TakeTwo)
                            {
                                iTake = 2;
                            }
                            else if (msg.Color == Colors.Black && msg.Number == 1)
                            {
                                iTake = 4;
                            }
                            else if (msg.Number == (int)UnoC.SpecialCards.Turn)
                            {
                                bDirection = !bDirection;
                            }
                            cDeck.Push(msg);
                            bEndRound = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //textBoxLog.AppendText("Error reading from socket " + socket.RemoteEndPoint.ToString() + ": [" + ex.GetType().Name + "] " + ex.Message + Environment.NewLine);
                ResetChildSocket(socket);
            }
            finally
            {
                //RefreshDisplay();
            }
        }

        private void ChildSocket_ShutdownCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SimpleServerChildTcpSocket socket = (SimpleServerChildTcpSocket)sender;

            // Check for errors
            if (e.Error != null)
            {
                //textBoxLog.AppendText("Socket error during Shutdown of " + socket.RemoteEndPoint.ToString() + ": [" + e.Error.GetType().Name + "] " + e.Error.Message + Environment.NewLine);
                ResetChildSocket(socket);
            }
            else
            {
                //textBoxLog.AppendText("Socket shutdown completed on " + socket.RemoteEndPoint.ToString() + Environment.NewLine);

                // Close the socket and remove it from the list
                ResetChildSocket(socket);
            }
            // RefreshDisplay();
        }

        private bool RemoveElement(SimpleServerChildTcpSocket s)
        {
            if (s != null)
                s.Close();

            return clients.Remove(s);
        }

        private void ResetChildSocket(SimpleServerChildTcpSocket childSocket)
        {
            RemoveElement(childSocket);
        }

        private void ResetListeningSocket()
        {
            // Close all child sockets and delete their handles to the ServerDlg
            foreach (SimpleServerChildTcpSocket s in clients)
            {
                RemoveElement(s);
                s.AbortiveClose();
            }
            clients.Clear();

            // Close the listening socket
            ListeningSocket.Close();
            ListeningSocket = null;
        }

        private UnoCard GetCard()
        {
            if (cards.isEmpty())
            {
                UnoCard c = cDeck.Pop();
                cards.ReInit(cDeck.ToArray());
                cDeck.Clear();
                cDeck.Push(c);
            }
            return cards.GetNext();
        }

        public void Start()
        {
            this.ListeningSocket = new SimpleServerTcpSocket();
            this.ListeningSocket.ConnectionArrived += ListeningSocket_ConnectionArrived;
            this.ListeningSocket.Listen(3456);
            this.StartGame();
        }

        private void StartGame()
        {
            //Wait for clients to connect
            while (clients.Count < clientsToUse || bStart)
            {
                Thread.Sleep(100);
            }

            //Share cards to all players + deck
            cards = new UnoCards();
            for (int i = 0; i < clients.Count * 7; i++)
            {
                clients[i % clients.Count].WriteAsync(Util.Serialize(cards.GetNext()));
            }
            cDeck.Push(cards.GetNext());

            //Loop until somebody has won
            int cnt = cDeck.Count;
            while (!bEndGame)
            {
                clients[clientIsOn].WriteAsync(Util.Serialize(cDeck.Peek()));
                if (iTake > 0)
                {
                    clients[clientIsOn].WriteAsync(Util.Serialize(cards.GetNext()));
                    clients[clientIsOn].WriteAsync(Util.Serialize(cards.GetNext()));
                    iTake = 0;
                }
                clients[clientIsOn].WriteAsync(Util.Serialize(UnoCard.EndRound));
                //blocking call
                while (!bEndRound) { Thread.Sleep(100); }
                //reset for next round
                bEndRound = false;
                if(bDirection)
                    clientIsOn = ((clientIsOn - 1) + clients.Count) % clients.Count;
                else
                    clientIsOn = (clientIsOn + 1) % clients.Count;
                bCardTaken = false;
            }
        }
    }
}
