namespace taki_client_YB2020
{
    partial class PlayingCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayingCard));
            this.mainLabel = new taki_client_YB2020.BorderLabel(this.components);
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mainLabel.ForeColor = System.Drawing.Color.Red;
            this.mainLabel.Location = new System.Drawing.Point(0, 0);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.OutlineForeColor = System.Drawing.Color.Black;
            this.mainLabel.OutlineWidth = 4F;
            this.mainLabel.Size = new System.Drawing.Size(124, 184);
            this.mainLabel.TabIndex = 1;
            this.mainLabel.Text = "6";
            // 
            // PlayingCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.mainLabel);
            this.Name = "PlayingCard";
            this.Size = new System.Drawing.Size(124, 184);
            this.ResumeLayout(false);

        }

        #endregion

        private BorderLabel mainLabel;
    }
}
