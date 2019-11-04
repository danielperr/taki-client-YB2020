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
        static Color[] bgGradientColors = { Color.FromArgb(25, 64, 111), Color.FromArgb(57, 128, 227) };

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.Paint += new PaintEventHandler(SetBackground);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetBackground(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush gradientBrush = new LinearGradientBrush(gradient_rectangle, bgGradientColors[0], bgGradientColors[1], 65f);
            Brush blackBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(gradientBrush, gradient_rectangle);
            graphics.FillRectangle(gradientBrush, gradient_rectangle);
            // dispose
            gradientBrush.Dispose();
            blackBrush.Dispose();

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
