namespace taki_client_YB2020
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.deck = new taki_client_YB2020.PlayingCard();
            this.pile = new taki_client_YB2020.PlayingCard();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // deck
            // 
            this.deck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deck.BackColor = System.Drawing.Color.Transparent;
            this.deck.CardColor = taki_client_YB2020.PCColor.Green;
            this.deck.CardValue = taki_client_YB2020.PCValue.None;
            this.deck.Location = new System.Drawing.Point(325, 271);
            this.deck.Name = "deck";
            this.deck.Size = new System.Drawing.Size(120, 180);
            this.deck.TabIndex = 3;
            // 
            // pile
            // 
            this.pile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pile.BackColor = System.Drawing.Color.Transparent;
            this.pile.CardColor = taki_client_YB2020.PCColor.Green;
            this.pile.CardValue = taki_client_YB2020.PCValue.None;
            this.pile.Location = new System.Drawing.Point(451, 271);
            this.pile.Name = "pile";
            this.pile.Size = new System.Drawing.Size(120, 180);
            this.pile.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(128)))), ((int)(((byte)(227)))));
            this.ClientSize = new System.Drawing.Size(868, 761);
            this.Controls.Add(this.deck);
            this.Controls.Add(this.pile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "Form1";
            this.Text = "TAKI";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(3)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private PlayingCard pile;
        private PlayingCard deck;
        private System.Windows.Forms.Timer timer1;
    }
}

