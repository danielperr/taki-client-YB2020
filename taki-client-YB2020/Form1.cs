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
        public int playersNum = 4;
        public int currentPlayer = 0;  // 0 = me, goes counter clockwise

        static Color[] bgGradColors = { Color.FromArgb(25, 64, 111), Color.FromArgb(57, 128, 227) };
        static Color[] ellipseGradColors = { Color.FromArgb(50, Color.White), Color.FromArgb(0, Color.White) };
        static Color[] selectedEllipseGradColors = { Color.FromArgb(150, Color.Orange), Color.FromArgb(100, Color.Orange) };
        static int ellipseHeight = 50;
        static int ellipseMargin = 50;
        static float gradAngle = 65f;

        public Form1()
        {
            InitializeComponent();
            // paint
            DoubleBuffered = true;
            this.Paint += new PaintEventHandler(SetBackground);
            this.Paint += new PaintEventHandler(DrawEllipses);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
            Graphics graphics = e.Graphics;
            for (int i = 0; i < Math.Min(playersNum, ellipses.Length); i++)
            {
                Brush brush = new LinearGradientBrush(ellipses[i], ellipseGradColors[0], ellipseGradColors[1], gradAngle);
                graphics.FillEllipse(brush, ellipses[i]);
                brush.Dispose();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
