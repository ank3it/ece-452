﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication1
{

    class Program
    {
        static void Main(string[] args)
        {
            SocketHandler sh = new SocketHandler();
            sh.StartListening();

            while (true) { };
        }
    }
}
