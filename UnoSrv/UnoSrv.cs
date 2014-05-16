using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace UnoSrv
{
    class UnoSrv
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private TcpClient[] clients;
        private int clientsUsed;

        public UnoSrv(int Clients)
        {
            clients = new TcpClient[Clients];
            clientsUsed = 0;
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (clientsUsed < clients.Length)
            {
                //blocks until a client has connected to the server
                clients[clientsUsed++] = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication with connected client
                HandleClientComm(clientsUsed-1);
            }
        }

        private void HandleClientComm(int client)
        {
            NetworkStream clientStream = clients[client].GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }
    }
}
