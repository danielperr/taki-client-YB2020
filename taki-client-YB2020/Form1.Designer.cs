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
            this.playingCard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("playingCard1.BackgroundImage")));
            this.playingCard1.Location = new System.Drawing.Point(299, 514);
            this.playingCard1.Name = "playingCard1";
            this.playingCard1.Size = new System.Drawing.Size(124, 184);
            this.playingCard1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(868, 710);
            this.Controls.Add(this.playingCard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TAKI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private PlayingCard playingCard1;
    }
}

