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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.deck = new taki_client_YB2020.PlayingCard();
            this.pile = new taki_client_YB2020.PlayingCard();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Place";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
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
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private PlayingCard pile;
        private PlayingCard deck;
    }
}

