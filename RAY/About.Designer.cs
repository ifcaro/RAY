namespace RAY
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.OkButton = new System.Windows.Forms.Button();
            this.WebSiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.IconPictureBox = new System.Windows.Forms.PictureBox();
            this.GithubLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            resources.ApplyResources(this.OkButton, "OkButton");
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Name = "OkButton";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // WebSiteLinkLabel
            // 
            resources.ApplyResources(this.WebSiteLinkLabel, "WebSiteLinkLabel");
            this.WebSiteLinkLabel.Name = "WebSiteLinkLabel";
            this.WebSiteLinkLabel.TabStop = true;
            this.WebSiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebSiteLinkLabel_LinkClicked);
            // 
            // VersionLabel
            // 
            resources.ApplyResources(this.VersionLabel, "VersionLabel");
            this.VersionLabel.Name = "VersionLabel";
            // 
            // NameLabel
            // 
            resources.ApplyResources(this.NameLabel, "NameLabel");
            this.NameLabel.Name = "NameLabel";
            // 
            // IconPictureBox
            // 
            resources.ApplyResources(this.IconPictureBox, "IconPictureBox");
            this.IconPictureBox.BackgroundImage = global::RAY.Properties.Resources.ray;
            this.IconPictureBox.Name = "IconPictureBox";
            this.IconPictureBox.TabStop = false;
            // 
            // GithubLinkLabel
            // 
            resources.ApplyResources(this.GithubLinkLabel, "GithubLinkLabel");
            this.GithubLinkLabel.Name = "GithubLinkLabel";
            this.GithubLinkLabel.TabStop = true;
            this.GithubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLinkLabel_LinkClicked);
            // 
            // About
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GithubLinkLabel);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.WebSiteLinkLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.IconPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button OkButton;
        internal System.Windows.Forms.LinkLabel WebSiteLinkLabel;
        internal System.Windows.Forms.Label VersionLabel;
        internal System.Windows.Forms.Label NameLabel;
        internal System.Windows.Forms.PictureBox IconPictureBox;
        internal System.Windows.Forms.LinkLabel GithubLinkLabel;
    }
}