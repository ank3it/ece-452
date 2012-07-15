using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RestaurantServer.Utilities
{
    class SocketHandler
    {
        TcpListener listener;
        const string sIP = "169.254.207.133";  //server IP       
        const int iPort = 4449; //server port

        public void StartListening()
        {
            listener = new TcpListener(System.Net.IPAddress.Parse(sIP), iPort);
            //listener = new TcpListener(System.Net.IPAddress.Any, iPort);
            listener.Start();

            Thread t = new Thread(new ThreadStart(ListenForMessages));
            t.Start();
        }

        void ListenForMessages()
        {
            const int bufferSize = 1024;

            Console.WriteLine("Waiting for client...");
            Socket clientSocket = listener.AcceptSocket();
            Console.WriteLine("Accepted client");

            while (true)
            {
                try
                {
                    byte[] bytes = new byte[bufferSize];

                    //receive bytes on the socket (blocking receive)
                    Console.WriteLine("Waiting for messages...");
                    int numBytesReceived = clientSocket.Receive(bytes); //store message into bytes array
                    if (numBytesReceived == 0)
                    {
                        Console.WriteLine("SOCKET DISCONNECTED");
                        if (clientSocket != null)
                        {
                            clientSocket.Dispose();
                            break;
                            //reconnect?
                        }
                    }

                    string msg = Encoding.ASCII.GetString(bytes, 0, numBytesReceived);

                    if (!String.IsNullOrEmpty(msg))
                        Console.WriteLine("(FROM CLIENT): {0}", msg);

                    string r = MessageParser.Parse(msg);

                    // if the response message is not empty, send it back to the client
                    if (!string.IsNullOrEmpty(r))
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes(r));
                        Console.WriteLine("Server response: " + r);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Client has disconnected.");
                    Console.WriteLine(e.ToString());
                    break;
                }
            }

            ListenForMessages();
        }
    }
}
