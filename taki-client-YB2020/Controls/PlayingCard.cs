using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taki_client_YB2020
{
    public enum PCColor { Green, Red, Yellow, Blue }  // PLAYING CARD COLOR
    public enum PCValue {  // PLAYING CARD VALUE (None = back of the card)
        None, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, TakeTwo, Plus, Stop, Taki, Chdir, Chcol, SuperTaki }

    public partial class PlayingCard : UserControl
    {
        #region Constants
        // Colors
        static Color backColor = Color.BlueViolet;
        static Color frontColor = Color.White;
        static Color[] colors = { Color.Green, Color.Red, Color.Yellow, Color.Blue };
        static float[] fontSizes = { 84, 64, 52 };

        // Dimensions
        static int roundedRectRadius = 12;  // rounded rectangle circle radius

        // Values
        static string[] value2text = {
            "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "2+", "+", "STOP", "TAKI", "Change Direction", "Change Color", "SUPER TAKI" };
        #endregion

        public PCColor CardColor { get; set; }
        public PCValue CardValue { get; set; }

        #region Constructors
        public PlayingCard()
        {
            CardColor = 0;
            CardValue = 0;
            InitializeComponent();
            AddPaintHandlers();
        }

        public PlayingCard(PCColor color, PCValue value)
        {
            CardColor = color;
            CardValue = value;
            InitializeComponent();
            AddPaintHandlers();
        }
        #endregion

        #region Paint Events
        private void AddPaintHandlers()
        {
            this.Paint += new PaintEventHandler(DrawBackground);
            this.Paint += new PaintEventHandler(DrawValue);
        }

        private void DrawBackground(object sender, PaintEventArgs e)
        {
            int r = roundedRectRadius;
            int d = 2 * r;
            int w = Width, h = Height;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(r, 0, w - r - 1, 0);
            path.AddArc(w - d - 1, 0, d, d, 270, 90);
            path.AddLine(w - 1, r, w - 1, h - r);
            path.AddArc(w - d - 1, h - d - 1, d, d, 0, 90);
            path.AddLine(w - r, h - 1, r, h - 1);
            path.AddArc(0, h - d - 1, d, d, 90, 90);
            path.AddLine(0, h - r, 0, r);
            path.AddArc(0, 0, d, d, 180, 90);
            //
            Color color = CardValue == PCValue.None ? backColor : frontColor;
            Brush brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), color, ControlPaint.Dark(color, 0.05f), 225f);
            Pen blackPen = new Pen(Color.Black);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(brush, path);
            e.Graphics.DrawPath(blackPen, path);
            //
            brush.Dispose();
            blackPen.Dispose();
            path.Dispose();
        }

        private void DrawValue(object sender, PaintEventArgs e)
        {
            string text = value2text[(int)CardValue];
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            //
            Brush fillBrush = new SolidBrush(colors[(int)CardColor]);
            if (CardValue == PCValue.SuperTaki)  // super taki rainbow party
            {
                LinearGradientBrush gradBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.Red, Color.Yellow, 0f);
                ColorBlend cblend = new ColorBlend(4);
                cblend.Colors = new Color[4] { Color.Red, Color.Yellow, Color.Green, Color.Blue };
                cblend.Positions = new float[4] { 0f, 0.5f, 0.8f, 1f };
                gradBrush.InterpolationColors = cblend;
                fillBrush = gradBrush;
                gradBrush.Dispose();
            }
            Pen borderPen = new Pen(Color.Black);
            GraphicsPath path = new GraphicsPath();
            //
            if (text.Contains(" "))  // Two lines
            {
                string[] lines = text.Split(null);
                sf.LineAlignment = StringAlignment.Far;
                path.AddString(lines[0], new FontFamily("Arial"), 0, 26, new Rectangle(0, 0, Width, Height / 2), sf);
                sf.LineAlignment = StringAlignment.Near;
                path.AddString(lines[1], new FontFamily("Arial"), 0, 26, new Rectangle(0, Height / 2, Width, Height / 2), sf);
                e.Graphics.FillPath(fillBrush, path);
            }
            else  // One line
            {
                int size = 72;
                if (text.Length > 2)
                    size = 36;
                path.AddString(text, new FontFamily("Arial"), 0, size, new Rectangle(0, 0, Width, Height), sf);
                e.Graphics.DrawPath(borderPen, path);
            }
            //
            sf.Dispose();
            fillBrush.Dispose();
            borderPen.Dispose();
            path.Dispose();
        }
        #endregion
    }
}
