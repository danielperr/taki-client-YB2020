using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace taki_client_YB2020
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    partial class Form1
    {
        private static string[] text2color = { "green", "red", "yellow", "blue", "ALL" };
        private static string[] text2value = { "none", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+2", "+", "STOP", "TAKI", "CHDIR", "CHCOL" };

        private static string response;
        private static ManualResetEvent connected = new ManualResetEvent(false);
        private static ManualResetEvent recvd = new ManualResetEvent(false);
        private static bool startBot = false;

        private Socket server;
        private int myID;
        private string takiState = "";
        private bool isTakeTwoActive = false;

        #region History Lists
        private List<List<string[]>> handHistory = new List<List<string[]>>();
        private List<string[]> pileHistory = new List<string[]>();
        private List<int[]> othersCountHistory = new List<int[]>();
        private List<int> turnHistory = new List<int>();
        #endregion

        private void BotInput(string input)
        {
            if (response.IndexOf("\"command\": \"Game Over\"") > -1)
                return;

            //try
            {
                dynamic jsonObj = JObject.Parse(response);
                Console.WriteLine(jsonObj.ToString());
                dynamic[] hand = jsonObj.hand.ToObject<dynamic[]>();
                string pileColor = jsonObj.pile.color.ToString();
                string pileValue = jsonObj.pile.value.ToString();
                int turn = int.Parse(jsonObj.turn.ToString());
                int turnDir = int.Parse(jsonObj.turn_dir.ToString());
                string[] others = jsonObj.others.ToObject<string[]>();
                //Console.WriteLine("pile color = {0}\npile value = {1}\nturn = {2}\nturnDir = {3}", pileColor, pileValue, turn, turnDir);

                // Add to history lists
                handHistory.Add(new List<string[]>());
                foreach (dynamic card in hand)
                    handHistory[handHistory.Count - 1].Add(new string[2] { card.color, card.value });
                pileHistory.Add(new string[2] { pileColor, pileValue });
                othersCountHistory.Add(new int[others.Length]);
                for (int i = 0; i < others.Length; i++)
                    othersCountHistory[othersCountHistory.Count - 1][i] = int.Parse(others[i]);
                turnHistory.Add(turn);

                if (turn != myID)
                    return;

                dynamic cardToSend = null;
                string order = "";

                if (takiState != "")
                {
                    Console.WriteLine("\n\tTAKI STATE = " + takiState);
                    cardToSend = FindByColor(hand, takiState);
                    if (CountColor(hand, takiState) == 1)
                    {
                        takiState = "";
                        order = "close taki";
                    }
                }
                else if (pileValue == "+2" && cardToSend == null)
                {
                    cardToSend = FindByValue(hand, "+2");
                }
                else
                {
                    if (cardToSend == null)
                        cardToSend = FindByBoth(hand, pileColor, "+");

                    if (cardToSend == null)
                        cardToSend = FindByColor(hand, pileColor);

                    if (cardToSend == null)
                        cardToSend = FindByValue(hand, pileValue);

                    if (cardToSend == null)
                        cardToSend = FindByBoth(hand, pileColor, "CHDIR");

                    if (cardToSend == null)
                        cardToSend = FindByBoth(hand, pileColor, "STOP");

                    if (cardToSend == null)
                    {
                        cardToSend = FindByValue(hand, "CHCOL");
                        if (cardToSend != null)
                            order = MostCommonColor(hand);
                    }

                    if (cardToSend == null)
                    {
                        cardToSend = FindByBoth(hand, "ALL", "TAKI");
                        Console.WriteLine("CARDTOSEND == null? ", (cardToSend == null).ToString());
                        if (cardToSend != null)
                        {
                            order = MostCommonColor(hand);
                            takiState = order;
                        }
                    }

                    if (cardToSend == null)
                        cardToSend = FindByValue(hand, "+2");
                }

                if (cardToSend == null)
                    order = "draw card";

                string data;
                if (cardToSend == null)
                    data = "{\"card\": {\"color\": \"\", \"value\": \"\"}, \"order\": \"" + order + "\"}";
                else
                    data = "{\"card\": {\"color\": \"" + cardToSend.color.ToString() + "\", \"value\": \"" + cardToSend.value.ToString() + "\"}, \"order\": \"" + order + "\"}";
                Console.WriteLine("\n\n\n" + data + "\n\n\n");
                Send(server, data);
                Thread.Sleep(100);

            }
            //catch { return; }
        }

        private dynamic FindByValue(dynamic[] hand, string value)
        {
            dynamic result = null;
            foreach (dynamic card in hand)
                if (card.value.ToString() == value)
                    result = card;
            return result;
        }

        private dynamic FindByColor(dynamic[] hand, string color)
        {
            dynamic result = null;
            foreach (dynamic card in hand)
                if (card.color.ToString() == color)
                    result = card;
            return result;
        }

        private dynamic FindByBoth(dynamic[] hand, string color, string value)
        {
            dynamic result = null;
            foreach (dynamic card in hand)
                if (card.value.ToString() == value && card.color.ToString() == color)
                    result = card;
            return result;
        }

        private string MostCommonColor(dynamic[] hand)
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();
            foreach (dynamic card in hand)
            {
                string color = card.color.ToString();
                if (occurrences.ContainsKey(color))
                    occurrences[color]++;
                else
                    occurrences.Add(color, 1);
            }
            return occurrences.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }

        private int CountColor(dynamic[] hand, string color)
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();
            foreach (dynamic card in hand)
            {
                string col = card.color.ToString();
                if (occurrences.ContainsKey(col))
                    occurrences[col]++;
                else
                    occurrences.Add(col, 1);
            }
            if (occurrences.ContainsKey(color))
                return occurrences[color];
            else
                return 0;
        }

        private void ConnectToServer(string ip, int port, string passwd)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ReceiveTimeout = 100;
            server.BeginConnect(ip, port, new AsyncCallback(ConnectCallBack), server);
            connected.WaitOne();

            // Send password
            Send(server, passwd);

            // Login successful
            Receive(server);
            recvd.WaitOne();
            recvd.Reset();

            // Wait for ID
            Receive(server);
            recvd.WaitOne();
            recvd.Reset();
            Console.WriteLine(response);
            dynamic jsonObj = JObject.Parse(response.Substring(0, 42));
            string command = jsonObj.command.ToString();
            myID = int.Parse(command[command.Length - 1].ToString());

            startBot = true;
            Receive(server);

        }

        #region Async Socket
        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());
                connected.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket server)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = server;
                server.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket server = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = server.EndReceive(ar);
                Console.WriteLine(bytesRead.ToString());
                if (bytesRead < 4)
                    return;
                response = Encoding.ASCII.GetString(state.buffer, 4, bytesRead - 4);
                recvd.Set();
                Console.WriteLine(response);
                if (startBot)
                {
                    BotInput(response);
                    Receive(server);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket server, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            server.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), server);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket server = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = server.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

    }
}
