using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace taki_client_YB2020
{
    class ClientSocket
    {
        private string password = "1234";
        public string response = "";
        private Socket socket;

        public ClientSocket()
        {
        }

        public void StartSocket(string ip, int port)
        {
            byte[] bytes = new byte[1024];
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);
            Console.WriteLine("connected to " + ip + " port " + port);
            int bytesSent = socket.Send(Encoding.ASCII.GetBytes(password));

            while (true)
            {
                int bytesReceived = socket.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes);
                Console.WriteLine(response);
            }
        }

        public int Send(string message)
        {
            int bytesSent = socket.Send(Encoding.ASCII.GetBytes(message));
            return bytesSent;
        }
    }
}
