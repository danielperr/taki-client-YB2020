namespace taki_client_YB2020
{
    partial class AddressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressForm));
            this.ipGroupBox = new System.Windows.Forms.GroupBox();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portGroupBox = new System.Windows.Forms.GroupBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.ipGroupBox.SuspendLayout();
            this.portGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipGroupBox
            // 
            this.ipGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipGroupBox.Controls.Add(this.ipTextBox);
            this.ipGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ipGroupBox.Name = "ipGroupBox";
            this.ipGroupBox.Size = new System.Drawing.Size(200, 48);
            this.ipGroupBox.TabIndex = 0;
            this.ipGroupBox.TabStop = false;
            this.ipGroupBox.Text = "IP Address";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipTextBox.Location = new System.Drawing.Point(6, 19);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(188, 20);
            this.ipTextBox.TabIndex = 0;
            this.ipTextBox.TextChanged += new System.EventHandler(this.IpTextBox_TextChanged);
            // 
            // portGroupBox
            // 
            this.portGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portGroupBox.Controls.Add(this.portTextBox);
            this.portGroupBox.Location = new System.Drawing.Point(12, 66);
            this.portGroupBox.Name = "portGroupBox";
            this.portGroupBox.Size = new System.Drawing.Size(200, 48);
            this.portGroupBox.TabIndex = 1;
            this.portGroupBox.TabStop = false;
            this.portGroupBox.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(6, 19);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(188, 20);
            this.portTextBox.TabIndex = 1;
            this.portTextBox.TextChanged += new System.EventHandler(this.PortTextBox_TextChanged);
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Enabled = false;
            this.goButton.Location = new System.Drawing.Point(137, 126);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 3;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.quitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.quitButton.Location = new System.Drawing.Point(12, 126);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // AddressForm
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.quitButton;
            this.ClientSize = new System.Drawing.Size(224, 161);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.portGroupBox);
            this.Controls.Add(this.ipGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(240, 200);
            this.MinimumSize = new System.Drawing.Size(240, 200);
            this.Name = "AddressForm";
            this.Text = "Server Address Selection";
            this.ipGroupBox.ResumeLayout(false);
            this.ipGroupBox.PerformLayout();
            this.portGroupBox.ResumeLayout(false);
            this.portGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ipGroupBox;
        private System.Windows.Forms.GroupBox portGroupBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button quitButton;
    }
}