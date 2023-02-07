namespace RAY
{
    partial class Scripts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scripts));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.MediumFontButton = new System.Windows.Forms.Button();
            this.SmallFontButton = new System.Windows.Forms.Button();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.MediumFontButton);
            this.splitContainer.Panel1.Controls.Add(this.SmallFontButton);
            this.splitContainer.Panel1.Controls.Add(this.PreviewPictureBox);
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.DataGridView);
            // 
            // MediumFontButton
            // 
            resources.ApplyResources(this.MediumFontButton, "MediumFontButton");
            this.MediumFontButton.Name = "MediumFontButton";
            this.MediumFontButton.UseVisualStyleBackColor = true;
            this.MediumFontButton.Click += new System.EventHandler(this.MediumFontButton_Click);
            // 
            // SmallFontButton
            // 
            resources.ApplyResources(this.SmallFontButton, "SmallFontButton");
            this.SmallFontButton.Name = "SmallFontButton";
            this.SmallFontButton.UseVisualStyleBackColor = true;
            this.SmallFontButton.Click += new System.EventHandler(this.SmallFontButton_Click);
            // 
            // PreviewPictureBox
            // 
            resources.ApplyResources(this.PreviewPictureBox, "PreviewPictureBox");
            this.PreviewPictureBox.BackColor = System.Drawing.Color.Black;
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.TabStop = false;
            // 
            // DataGridView
            // 
            resources.ApplyResources(this.DataGridView, "DataGridView");
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.DataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // Scripts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "Scripts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Scripts_FormClosed);
            this.Load += new System.EventHandler(this.Scripts_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Button SmallFontButton;
        private System.Windows.Forms.Button MediumFontButton;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}