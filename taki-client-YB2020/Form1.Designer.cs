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
            this.playingCard1 = new taki_client_YB2020.PlayingCard();
            this.SuspendLayout();
            // 
            // playingCard1
            // 
            this.playingCard1.BackColor = System.Drawing.Color.Transparent;
            this.playingCard1.CardColor = taki_client_YB2020.PCColor.Green;
            this.playingCard1.CardValue = taki_client_YB2020.PCValue.Chcol;
            this.playingCard1.Location = new System.Drawing.Point(255, 372);
            this.playingCard1.Name = "playingCard1";
            this.playingCard1.Size = new System.Drawing.Size(124, 184);
            this.playingCard1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(128)))), ((int)(((byte)(227)))));
            this.ClientSize = new System.Drawing.Size(868, 710);
            this.Controls.Add(this.playingCard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "Form1";
            this.Text = "TAKI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private PlayingCard playingCard1;
    }
}

