using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Nito.Async;
using Nito.Async.Sockets;
using UnoC;

namespace UnoServer
{
    public class UnoSrv
    {
        private SimpleServerTcpSocket ListeningSocket;
        private List<UnoPlayer> clients;
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
        private ConcurrentBag<string> logger;
        private Thread t;

        public UnoSrv(int Clients, ref ConcurrentBag<string> Logger)
        {
            this.ListeningSocket = new SimpleServerTcpSocket();
            this.ListeningSocket.ConnectionArrived += ListeningSocket_ConnectionArrived;
            this.cards = new UnoCards();
            this.clients = new List<UnoPlayer>();
            this.clientsToUse = Clients;
            this.logger = Logger;
        }

        private void ListeningSocket_ConnectionArrived(AsyncResultEventArgs<SimpleServerChildTcpSocket> e)
        {
            // Check for errors
            if (e.Error != null)
            {
                ResetListeningSocket();
                lock (logger)
                {
                    logger.Add("Connection error, couldn't established");
                }
                return;
            }
            SimpleServerChildTcpSocket socket = e.Result;

            try
            {
                // Save the new child socket connection
                clients.Add(new UnoPlayer(socket));
                socket.PacketArrived += (args) => ChildSocket_PacketArrived(socket, args);
                socket.ShutdownCompleted += (args) => ChildSocket_ShutdownCompleted(socket, args);
                // Display the connection information
                lock (logger)
                {
                    logger.Add("Connection established to " + socket.RemoteEndPoint.ToString());
                }
            }
            catch (Exception ex)
            {
                ResetChildSocket(socket);
                lock (logger)
                {
                    logger.Add("Socket error accepting connection: [" + ex.GetType().Name + "] " + ex.Message);
                }
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
                    lock (logger)
                    {
                        logger.Add("Client socket error during Read from " + socket.RemoteEndPoint.ToString() + ": [" + e.Error.GetType().Name + "] " + e.Error.Message + Environment.NewLine);
                    }
                    ResetChildSocket(socket);
                }
                else if (e.Result == null)
                {
                    // PacketArrived completes with a null packet when the other side gracefully closes the connection
                    lock (logger)
                    {
                        logger.Add("Socket graceful close detected from " + socket.RemoteEndPoint.ToString() + Environment.NewLine);
                    }
                    // Close the socket and remove it from the list
                    ResetChildSocket(socket);
                }
                else
                {
                    UnoPlayer player = (UnoPlayer)Util.Deserialize(e.Result);
                    if (player != null)
                    {
                        clients[clientIsOn].Name = player.Name;
                        lock (logger)
                        {
                            logger.Add("Client " + clients[clientIsOn++].Name + " connected");
                        }
                        return;
                    }
                    UnoCard msg = (UnoCard)Util.Deserialize(e.Result);
                    if (!bStart)
                    {
                        if (socket.LocalEndPoint.Address == ListeningSocket.LocalEndPoint.Address && msg == UnoCard.StartRound)
                            bStart = true;
                    }
                    else if (socket == clients[clientIsOn].Socket)
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
                            clients[clientIsOn].HasLastCard = true;
                        }
                        else if (msg == UnoCard.Won)
                        {
                            if (clients[clientIsOn].HasLastCard == true)
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
                            else if (msg.Number == (int)UnoC.SpecialCards.ChangeColorPlusFour)
                            {
                                iTake = 4;
                            }
                            else if (msg.Number == (int)UnoC.SpecialCards.Turn)
                            {
                                bDirection = !bDirection;
                            }
                            clients[clientIsOn].Cards--;
                            cDeck.Push(msg);
                            bEndRound = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lock (logger)
                {
                    logger.Add("Error reading from socket " + socket.RemoteEndPoint.ToString() + ": [" + ex.GetType().Name + "] " + ex.Message + Environment.NewLine);
                }
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
                lock (logger)
                {
                    logger.Add("Socket error during Shutdown of " + socket.RemoteEndPoint.ToString() + ": [" + e.Error.GetType().Name + "] " + e.Error.Message + Environment.NewLine);
                }
                ResetChildSocket(socket);
            }
            else
            {
                lock (logger)
                {
                    logger.Add("Socket shutdown completed on " + socket.RemoteEndPoint.ToString() + Environment.NewLine);
                }

                // Close the socket and remove it from the list
                ResetChildSocket(socket);
            }
            // RefreshDisplay();
        }

        private bool RemoveElement(SimpleServerChildTcpSocket s)
        {
            if (s != null)
            {
                foreach (UnoPlayer p in clients)
                {
                    if (p.Socket == s)
                    {
                        s.AbortiveClose();
                        return clients.Remove(p);
                    }
                }
            }
            return false;
        }

        private bool RemoveElement(UnoPlayer p)
        {
            if (p != null)
            {
                if (p.Socket != null)
                {
                    p.Socket.AbortiveClose();
                    return clients.Remove(p);
                }
            }
            return false;
        }

        private void ResetChildSocket(SimpleServerChildTcpSocket childSocket)
        {
            RemoveElement(childSocket);
        }

        private void ResetListeningSocket()
        {
            // Close all child sockets and delete their handles to the ServerDlg
            foreach (UnoPlayer s in clients)
            {
                RemoveElement(s);
            }
            clients.Clear();

            // Close the listening socket
            ListeningSocket.Close();
            ListeningSocket = null;
        }

        private int SearchSocket(SimpleServerChildTcpSocket s)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (s == clients[i].Socket)
                    return i;
            }
            return 99;
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
            this.ListeningSocket.Listen(3456);
            lock (logger)
            {
                logger.Add("Server is Listening...");
            }
            t = new Thread(StartGame);
            t.Start();
            //this.StartGame();
        }

        public void Stop()
        {
            bEndGame = true;
        }

        private void EndConnections()
        {
            foreach (UnoPlayer p in clients)
            {
                p.Socket.Close();
            }
        }

        public bool IsRunning
        {
            get { return t.IsAlive; }
        }

        private void StartGame()
        {
            //Wait for clients to connect
            while (clients.Count < clientsToUse || bStart)
            {
                Thread.Sleep(100);
            }

            //Share all Playername's

            //Share cards to all players + deck
            cards = new UnoCards();
            for (int i = 0; i < clients.Count * 7; i++)
            {
                clients[i % clients.Count].Socket.WriteAsync(Util.Serialize(cards.GetNext()));
                if (i >= (clients.Count * 6) - 1)
                    clients[i % clients.Count].Cards = 7;
            }
            cDeck.Push(cards.GetNext());

            //Loop until somebody has won
            while (!bEndGame)
            {
                clients[clientIsOn].Socket.WriteAsync(Util.Serialize(cDeck.Peek()));
                if (iTake > 0)
                {
                    for(int i=0;i<iTake;i++)
                        clients[clientIsOn].Socket.WriteAsync(Util.Serialize(cards.GetNext()));
                    iTake = 0;
                }
                clients[clientIsOn].Socket.WriteAsync(Util.Serialize(UnoCard.EndRound));
                foreach (UnoPlayer p in clients)
                {
                    clients[clientIsOn].Socket.WriteAsync(Util.Serialize(p));
                }
                clients[clientIsOn].Socket.WriteAsync(Util.Serialize(UnoPlayer.EndMessage));
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
            EndConnections();
        }
    }
}
