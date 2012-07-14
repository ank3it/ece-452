using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication1
{
    class SocketHandler
    {
        TcpListener listener;
        const string sIP = "169.254.33.133";  //server IP       
        const int iPort = 8080; //server port

        public void StartListening()
        {
            listener = new TcpListener(System.Net.IPAddress.Parse(sIP), iPort);
            listener.Start();

            Thread t = new Thread(new ThreadStart(ListenForMessages));
            t.Start();
        }

        void ListenForMessages()
        {
            const int bufferSize = 1024;

            Socket clientSocket = listener.AcceptSocket();
            Console.WriteLine("Accepted client");

            while (true)
            {
                byte[] bytes = new byte[bufferSize];

                //receive bytes on the socket (blocking receive)
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

                var mp = new MessageParser();
                string r = mp.Parse(msg);

                clientSocket.Send(Encoding.ASCII.GetBytes(r));
                Console.WriteLine("Sent back result");
            }
        }
    }
}
