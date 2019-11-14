using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Taki_Project
{
    class Game
    {

        public List<Dictionary<string, string>> SpecialCards { get; set; }
        public List<Dictionary<string, string>> RegularCards { get; set; }
        public List<Dictionary<string, string>> hand { get; set; }
        public Dictionary<string, object> Kabala { get; set; }
        //public List<string> Cards;

        public Game()
        {
            SpecialCards = new List<Dictionary<string, string>>();
            RegularCards = new List<Dictionary<string, string>>();
            //  Cards = new List<string>() { "TAKI", "CHCOL", "CHDIR", "STOP", "+2", "Number" };


        }
        public Dictionary<string, object> CreateCard(string value, string color, string order)
        {
            Dictionary<string, object> Card = new Dictionary<string, object>();
            Card.Add("card", new Dictionary<string, string>() { { "color", color }, { "value", value } });
            Card.Add("order", order);

            return Card;
        }

        public Dictionary<string, string> FindCard(List<Dictionary<string, string>> pack, string card, string color, int i = 0) // finding card by value;
        {
            if (i >= pack.Count)
                return null;
            if (pack[i]["value"] == card || card == "")
                if (pack[i]["color"] == color || color == "")
                    return pack[i];
                else return FindCard(pack, card, color, i + 1);
            else return FindCard(pack, card, color, i + 1);

        }

        public Dictionary<string, object> ChooseCard(List<List<string>> prio)
        {
            string pileColor = Kabala["pile_color"].ToString();
            string pileValue = ((Dictionary<string, string>)Kabala["pile"])["value"];
            Dictionary<string, string> card = new Dictionary<string, string>();
            Dictionary<string, object> finCard = new Dictionary<string, object>();

            if (pileValue == "+2")
            {
                card = this.FindCard(this.SpecialCards, "+2", "");
                if (card == null)
                    return CreateCard("", "", "draw card");
                return CreateCard(card["color"], card["value"], "");
            }
            else
            {
                foreach (List<string> li in prio)
                {

                    switch (li[0])
                    {
                        case "Number":
                            card = this.FindCard(this.RegularCards, "", pileColor);
                            if (card == null && Regex.IsMatch(pileValue, @"^\d+$"))
                                card = this.FindCard(this.RegularCards, pileValue, "");
                            break;
                        case "TAKI":
                            if (li[1] == "ALL") card = this.FindCard(this.SpecialCards, li[0], li[1]);
                            else card = this.FindCard(this.SpecialCards, li[0], pileColor);
                            break;
                        default:
                            card = this.FindCard(this.SpecialCards, li[0], pileColor);
                            break;
                    }
                    if (card != null)
                        return CreateCard(card["value"], card["color"], ""); 
                }
                return null;
            }



        }

        public void CardsDistrib()
        {
            List<Dictionary<string, string>> cards = (List<Dictionary<string, string>>)this.Kabala["hand"];

            foreach (var card in cards)
            {

                if (Regex.IsMatch(card["value"], @"^\d+$"))
                    this.RegularCards.Add(card);
                else this.SpecialCards.Add(card);
            }
        }



    }

    class Cards : Game
    {
        public string ALot_Cards_Or_Players { get; set; }
        public Dictionary<string, Delegate> CardsExcep;
        public delegate bool Check(Dictionary<string, object> cardi);
        public List<string> suspend;

        public Cards()
        {
            this.ALot_Cards_Or_Players = "";
            Check taki = takiFunc, chdir = chdirFunc, plusTwo = plusTwoFunc;
            CardsExcep = new Dictionary<string, Delegate> { { "TAKI", taki }, { "CHCOL", plusTwo }, { "CHDIR", chdir }, { "STOP", chdir }, { "+2", plusTwo }, { "+", chdir } };
            suspend = new List<string>();
        }
        public List<Dictionary<string, string>> CardsInSpecifCol(string color, string value)
        {
            List<Dictionary<string, string>> newl = new List<Dictionary<string, string>>();
            foreach (Dictionary<string, string> card in hand)
            {
                if (card["color"] == color && card["value"] != value)
                    newl.Add(card);
            }
            return newl;
        }

        public int takiFunc(Dictionary<string, object> cardi)
        {
            List<Dictionary<string, string>> newl;
            switch (((Dictionary<string, string>)cardi["card"])["color"])
            {
                case "ALL":
                    newl = CardsInSpecifCol(Kabala["pile_color"].ToString(), "TAKI");
                    break;
                default:
                    newl = CardsInSpecifCol(((Dictionary<string, string>)cardi["card"])["color"], "TAKI");
                    break;
            }
            if (newl.Count >= 4 || hand.Count - newl.Count <= 2)
            {
                Send(cardi);
                foreach (Dictionary<string, string> card in newl)
                {
                    this.Kabala = (Dictionary<string, object>)Recv(socli); //!!
                    if ((int)Kabala["turn"] == Globals.ID)
                    {
                        if (newl.IndexOf(card) == newl.Count - 1)
                            Send(this.CreateCard(card["value"], card["color"], "close taki")); //enter berenshtein's work
                        else
                        {
                            Send(this.CreateCard(card["value"], card["color"], "")); //enter berenshtein's work
                        }
                    }
                }
                return 0;
            }
            return 2;

        }


        public int chdirFunc(Dictionary<string, object> cardi)
        {
            List<Dictionary<string, string>> newl;
            if (ALot_Cards_Or_Players == "ff" || ALot_Cards_Or_Players == "tf")
            {
                newl = CardsInSpecifCol(((Dictionary<string, string>)cardi["card"])["color"], "CHDIR");
                if (newl.Count >= 1 || suspend.Contains(((Dictionary<string, string>)cardi["card"])["value"]))
                {
                    Send(cardi); //enter berenshtein's work
                    this.Kabala = (Dictionary<string, object>)Recv(socli);
                    if (newl.Count > 0) Send(newl[0]);
                    return 0;

                }
                else
                {
                   suspend.Add(((Dictionary<string, string>)cardi["card"])["value"]);
                   return 1;
                    
                }
            }
            return 0;

        }

        public int plusTwoFunc(Dictionary<string, object> cardi)
        {
            Send(cardi);
            return 0;
        }

    }


    static class Globals
    {
        public static int ID;
    }



    class Program
    {

        public static object Recv(Socket socli)
        {
            byte[] messageReceived = new byte[1024];
            int byteRecv = socli.Receive(messageReceived);
            return Encoding.ASCII.GetString(messageReceived,
                                             0, byteRecv);
            //I need to convert to JSON, and all the procedure
        }
        public static object Send(Socket socli, object toSend)
        {
            byte[] messageReceived = new byte[1024];
            int byteRecv = socli.Receive(messageReceived);
            return Encoding.ASCII.GetString(messageReceived,
                                             0, byteRecv);
            //we need to convert to JSON, and all the procedure
        }


        static void Main(string[] args)
        {

            Game game = new Game();
            Cards funcCards = new Cards();
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 50000);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket socli = new Socket(ipAddr.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);

            socli.Connect(localEndPoint);
            Globals.ID = 0; //In reality, I get the ID from the socket



            Dictionary<string, List<List<string>>> priority = new Dictionary<string, List<List<string>>>();
            priority.Add("tt", new List<List<string>> { new List<string> { "TAKI", "" }, new List<string> { "TAKI", "ALL" }, new List<string> { "+2", "" }, new List<string> { "Number", "" }, new List<string> { "STOP", "" }, new List<string> { "CHDIR", "" } });
            priority.Add("tf", new List<List<string>> { new List<string> { "TAKI", "" }, new List<string> { "TAKI", "ALL" }, new List<string> { "+2", "" }, new List<string> { "Number", "" }, new List<string> { "STOP", "" }, new List<string> { "CHDIR", "" }, new List<string> { "STOP", "" }, new List<string> { "CHCOL", "" } });
            priority.Add("ft", new List<List<string>> { new List<string> { "TAKI", "" }, new List<string> { "TAKI", "ALL" }, new List<string> { "STOP", "" }, new List<string> { "CHDIR", "" }, new List<string> { "+2", "" }, new List<string> { "Number", "" }, new List<string> { "CHCOL", "" } });
            priority.Add("ff", new List<List<string>> { new List<string> { "TAKI", "" }, new List<string> { "TAKI", "ALL" }, new List<string> { "STOP", "" }, new List<string> { "CHDIR", "" }, new List<string> { "Number", "" }, new List<string> { "CHCOL", "" }, new List<string> { "+2", "" } }); //here - stop, chdir, and taki need to come back again to the end line.


            Dictionary<string, object> cardTurn;
            List<List<string>> prio = new List<List<string>>();

            bool Turns = true, check = false;
            while (Turns)
            {

                game.Kabala = (Dictionary<string, object>)Recv(socli);
                if ((int)game.Kabala["turn"] == Globals.ID)
                {
                    funcCards.ALot_Cards_Or_Players = "";
                    game.CardsDistrib();
                    if (game.SpecialCards.Count + game.RegularCards.Count > 4) funcCards.ALot_Cards_Or_Players += "t";
                    else funcCards.ALot_Cards_Or_Players += "f";

                    if (((List<int>)game.Kabala["players"]).Count >= 3) funcCards.ALot_Cards_Or_Players += "t";
                    else funcCards.ALot_Cards_Or_Players += "f";

                    check = true;
                    prio = priority[funcCards.ALot_Cards_Or_Players];
                    funcCards.suspend.Clear();
                    while (check || prio.Count==0)
                    {
                        cardTurn = game.ChooseCard(prio);
                        List<string> cardd = new List<string> { ((Dictionary<string, string>)(cardTurn["card"]))["value"], ((Dictionary<string, string>)(cardTurn["card"]))["color"] };
                        int ok = (int)funcCards.CardsExcep[cardd[0]].DynamicInvoke(cardTurn);

                        switch (ok)
                        {                          
                            case 0: check = false; break;
                            case 1: prio = prio.GetRange(prio.IndexOf(cardd)+1, prio.Count - prio.IndexOf(cardd)-1); prio.Remove(cardd); prio.Add(cardd); break;
                            case 2: prio = prio.GetRange(prio.IndexOf(cardd) + 1, prio.Count - prio.IndexOf(cardd) - 1); prio.Remove(cardd); break;

                        }
                    }
                    if (prio.Count == 0)
                        Send(socli,CreateCard("", "", "draw card"));
                    
                }




            }




        }
    }
}



