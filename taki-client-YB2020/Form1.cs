using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taki_client_YB2020
{
    public partial class Form1 : Form
    {
        public int playersNum = 4; // max 4
        public int currentPlayer = 0;  // 0 = me, goes counter clockwise

        #region Constants
        static Color[] bgGradColors = { Color.FromArgb(25, 64, 111), Color.FromArgb(57, 128, 227) };
        static Color[] ellipseGradColors = { Color.FromArgb(50, Color.White), Color.FromArgb(0, Color.White) };
        static Color[] selectedEllipseGradColors = { Color.FromArgb(150, Color.Orange), Color.FromArgb(100, Color.Orange) };
        static int ellipseHeight = 50;
        static int ellipseMargin = 50;
        static float gradAngle = 65f;
        static int cardsDistance = 8;  // The distance between two adjacent cards
        static int foldedCardsDist = 30;  // The shift in position when cards are on top of one another
        static int handEdgeDistance = 16;  // The distance between the edge of the window and the cards
        #endregion

        private List<PlayingCard> myCards;
        private List<PlayingCard> player1Cards;
        private List<PlayingCard> player2Cards;
        private List<PlayingCard> player3Cards;

        public Form1()
        {
            myCards = new List<PlayingCard>();
            player1Cards = new List<PlayingCard>();
            player2Cards = new List<PlayingCard>();
            player3Cards = new List<PlayingCard>();
            //
            InitializeComponent();
            pile.Location = new Point(Width/2 + cardsDistance / 2, Height/2 - pile.Height/2);
            deck.Location = new Point(Width/2 - deck.Width - cardsDistance / 2, Height / 2 - pile.Height / 2);
            //
            DoubleBuffered = true;
            Paint += new PaintEventHandler(SetBackground);
            Paint += new PaintEventHandler(DrawEllipses);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();
            addressForm.ShowDialog();
            if (addressForm.IP == null)  // Pressed quit or closed window
                Close();
            try { ConnectToServer(addressForm.IP, addressForm.Port, addressForm.Password); }
            catch
            {  // Fire up error message
                MessageBox.Show("Failed to connect to the address specified", "Socket error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        #region Playing Cards
        public void AddToMyCards(PCColor color, PCValue value)
            /// Adds a card to my hand
        {
            PlayingCard card = new PlayingCard(color, value);
            card.Location = new Point(0, ClientSize.Height - pile.Height - handEdgeDistance);
            card.Anchor = AnchorStyles.Bottom;
            myCards.Add(card);
            Controls.Add(card);
            RearrangeMyCards();
        }

        public void PlaceCard(PCColor color, PCValue value)
            /// Places a card in the pile (REMOVES it from my hand)
            /// If it's a super taki or a change color, the requested color needs to be specified
        {
            PlayingCard cardToPlace = null;
            foreach (PlayingCard card in myCards)
                if (card.CardValue == value && (card.CardColor == color || card.CardColor == PCColor.All))
                    cardToPlace = card;
            if (cardToPlace == null)
                return;
            myCards.Remove(cardToPlace);
            Controls.Remove(cardToPlace);
            cardToPlace.CardColor = color;  // TODO: try to dispose
            // Change the pile card
            pile.CardColor = color;
            pile.CardValue = value;
            pile.Invalidate();
            RearrangeMyCards();
        }

        public void SetPlayerCards(int player, int count)
            /// Set other player's card count
        {
            List<PlayingCard> cards;
            Point initialLocation;
            AnchorStyles anchor;
            switch (player)
            {
                case 1:  // Left
                    cards = player1Cards;
                    initialLocation = new Point(handEdgeDistance, 0);
                    anchor = AnchorStyles.Left;
                    break;
                case 2:  // Up
                    cards = player2Cards;
                    initialLocation = new Point(0, handEdgeDistance);
                    anchor = AnchorStyles.Top;
                    break;
                case 3:  // Right
                    cards = player3Cards;
                    initialLocation = new Point(ClientSize.Width - handEdgeDistance - pile.Height, 0);
                    anchor = AnchorStyles.Right;
                    break;
                default: return;
            }
            if (count == cards.Count)
                return;
            // Match count by adding / removing
            if (count > cards.Count)
                while (count != cards.Count)
                {
                    PlayingCard card = new PlayingCard(PCColor.All, PCValue.None);
                    card.Location = initialLocation;
                    card.Anchor = anchor;
                    cards.Add(card);
                    Controls.Add(card);
                }
            else
                while (count != cards.Count)
                {
                    PlayingCard card = cards[0];
                    cards.Remove(card);
                    Controls.Remove(card);
                }
            RearrangeOtherCards();
        }

        public void RearrangeMyCards()
        {
            SuspendLayout();
            // My cards
            int count = myCards.Count;
            int totalWidth = Math.Min(pile.Width * count + cardsDistance * (count - 1), Width - 2 * ellipseMargin);
            for (int i = 0; i < count; i++)
                myCards[i].Location = new Point(Width / 2 - totalWidth / 2 + totalWidth / count * i, myCards[i].Location.Y);
            // Others cards
            
            ResumeLayout();
        }

        public void RearrangeOtherCards()
        {
            int count = player1Cards.Count;
            for (int i = 0; i < count; i++)
            {
                player1Cards[i].Size = new Size(pile.Height, pile.Width);  // Card is rotated by 90 degrees
                player1Cards[i].Location =
                    new Point(player1Cards[i].Location.X, Height / 2 - (foldedCardsDist * count + pile.Width) / 2 + foldedCardsDist * i);
            }
            //
            count = player2Cards.Count;
            for (int i = 0; i < count; i++)
                player2Cards[i].Location =
                    new Point(Width / 2 - (foldedCardsDist * count + pile.Width) / 2 + foldedCardsDist * i, player2Cards[i].Location.Y);
            //
            count = player3Cards.Count;
            for (int i = 0; i < count; i++)
            {
                player3Cards[i].Size = new Size(pile.Height, pile.Width);  // Card is rotated by 90 degrees
                player3Cards[i].Location =
                    new Point(player3Cards[i].Location.X, Height / 2 - (foldedCardsDist * count + pile.Width) / 2 + foldedCardsDist * i);
            }
        }
        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            RearrangeMyCards();
            Invalidate();
        }

        #region Drawing

        private void SetBackground(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush gradientBrush = new LinearGradientBrush(gradient_rectangle, bgGradColors[0], bgGradColors[1], gradAngle);
            Brush blackBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(gradientBrush, gradient_rectangle);
            graphics.FillRectangle(gradientBrush, gradient_rectangle);
            // dispose
            gradientBrush.Dispose();
            blackBrush.Dispose();
        }

        private void DrawEllipses(Object sender, PaintEventArgs e)
        {
            Rectangle[] ellipses = {
                new Rectangle(ellipseMargin, Height-2*ellipseHeight, Width-2*ellipseMargin, 2*ellipseHeight),
                new Rectangle(Width-ellipseHeight, ellipseMargin, 2*ellipseHeight, Height-2*ellipseMargin),
                new Rectangle(ellipseMargin, -ellipseHeight, Width-2*ellipseMargin, 2*ellipseHeight),
                new Rectangle(-ellipseHeight, ellipseMargin, 2*ellipseHeight, Height-2*ellipseMargin)
            };
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < Math.Min(playersNum, ellipses.Length); i++)
            {
                Brush brush = new LinearGradientBrush(ellipses[i], ellipseGradColors[0], ellipseGradColors[1], gradAngle);
                e.Graphics.FillEllipse(brush, ellipses[i]);
                brush.Dispose();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            AddToMyCards(PCColor.All, PCValue.Taki);
            AddToMyCards(PCColor.All, PCValue.Chcol);
            AddToMyCards(PCColor.Green, PCValue.Chdir);
            AddToMyCards(PCColor.Green, PCValue.Stop);
            AddToMyCards(PCColor.Green, PCValue.One);
            AddToMyCards(PCColor.Red, PCValue.Two);
            AddToMyCards(PCColor.Red, PCValue.TakeTwo);
            AddToMyCards(PCColor.Red, PCValue.Three);
            AddToMyCards(PCColor.Blue, PCValue.Four);
            AddToMyCards(PCColor.Blue, PCValue.Five);
            AddToMyCards(PCColor.Blue, PCValue.Six);
            AddToMyCards(PCColor.Yellow, PCValue.Seven);
            AddToMyCards(PCColor.Yellow, PCValue.Eight);
            AddToMyCards(PCColor.Yellow, PCValue.Nine);
            AddToMyCards(PCColor.Yellow, PCValue.Plus);
            SetPlayerCards(1, 8);
            SetPlayerCards(2, 8);
            SetPlayerCards(3, 8);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PlaceCard(PCColor.Blue, PCValue.Chcol);
        }

    }
}
