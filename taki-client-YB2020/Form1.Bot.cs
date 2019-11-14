using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taki_client_YB2020
{
    partial class Form1
    {
        private Socket server;

        private void ConnectToServer(string ip, int port, string passwd)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip, port);

            // Send password

            // Bot
        }


    }
}
