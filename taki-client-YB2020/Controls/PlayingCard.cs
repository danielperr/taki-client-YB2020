using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taki_client_YB2020
{
    public enum PCColor { Green, Red, Yellow, Blue }  // PLAYING CARD COLOR
    public enum PCValue {  // PLAYING CARD VALUE
        One, Two, Three, Four, Five, Six, Seven, Eight, Nine, TakeTwo, Plus, Stop, Taki, Chdir, Chcol, SuperTaki }

    public partial class PlayingCard : UserControl
    {
        static string[] value2text = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "2+", "+", "STOP", "TAKI", "-><-", "COL", "TAKI" };
        static Color[] colors = { Color.Green, Color.Red, Color.Yellow, Color.Blue };
        static float[] fontSizes = { 84, 64, 52 };

        public PCColor CardColor
        {
            get { return _color; }
            set
            {
                _color = value;
                mainLabel.ForeColor = colors[(int)value];
            }
        }
        public PCValue CardValue
        {
            get { return _value; }
            set
            {
                _value = value;
                mainLabel.Text = value2text[(int)value];
                // changing font size to fit
                float size = fontSizes[0];
                if (mainLabel.Text.Length == 2)
                    size = fontSizes[1];
                else if (mainLabel.Text.Length > 2)
                    size = fontSizes[2];
                mainLabel.Font = new Font(mainLabel.Font.FontFamily, size);
            }
        }

        private PCColor _color;
        private PCValue _value;

        public PlayingCard()
        {
            InitializeComponent();
            CardColor = PCColor.Green;
            CardValue = PCValue.One;
        }
        public PlayingCard(PCColor color, PCValue value)
        {
            InitializeComponent();
            CardColor = color;
            CardValue = value;
        }

        private void PlayingCard_Load(object sender, EventArgs e)
        {

        }
    }
}
