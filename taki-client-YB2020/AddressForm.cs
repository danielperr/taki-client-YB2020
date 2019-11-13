using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace taki_client_YB2020
{
    public partial class AddressForm : Form
    {
        public string IP;
        public int Port;
        public string Password;

        public AddressForm()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            IP = ipTextBox.Text;
            Port = int.Parse(portTextBox.Text);
            Password = passwdTextBox.Text;
            Close();
        }

        private void ValidateInput()
        {
            goButton.Enabled =
                ipTextBox.Text.Length > 0
                && portTextBox.Text.Length > 0
                && Regex.Match(ipTextBox.Text, @"^(\d+\.){3}\d+$").Success
                && Regex.Match(portTextBox.Text, @"^\d{2,5}$").Success;
        }

        private void IpTextBox_TextChanged(object sender, EventArgs e) => ValidateInput();
        private void PortTextBox_TextChanged(object sender, EventArgs e) => ValidateInput();

    }
}
