namespace RAY
{
    partial class RAY
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAY));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageTexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTextsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsMenu,
            this.WindowsMenu,
            this.LanguageToolStripMenuItem,
            this.HelpMenu});
            this.menuStrip.MdiWindowListItem = this.WindowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.toolTip.SetToolTip(this.menuStrip, resources.GetString("menuStrip.ToolTip"));
            this.menuStrip.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip_ItemAdded);
            // 
            // fileMenu
            // 
            resources.ApplyResources(this.fileMenu, "fileMenu");
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.toolStripSeparator3,
            this.SaveToolStripMenuItem,
            this.toolStripSeparator5,
            this.ExitToolStripMenuItem});
            this.fileMenu.Name = "fileMenu";
            // 
            // OpenToolStripMenuItem
            // 
            resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenWorkspace);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // SaveToolStripMenuItem
            // 
            resources.ApplyResources(this.SaveToolStripMenuItem, "SaveToolStripMenuItem");
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveWorkspace);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // ExitToolStripMenuItem
            // 
            resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            resources.ApplyResources(this.toolsMenu, "toolsMenu");
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ManageTexturesToolStripMenuItem,
            this.EditTextsToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            // 
            // ManageTexturesToolStripMenuItem
            // 
            resources.ApplyResources(this.ManageTexturesToolStripMenuItem, "ManageTexturesToolStripMenuItem");
            this.ManageTexturesToolStripMenuItem.Name = "ManageTexturesToolStripMenuItem";
            this.ManageTexturesToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // EditTextsToolStripMenuItem
            // 
            resources.ApplyResources(this.EditTextsToolStripMenuItem, "EditTextsToolStripMenuItem");
            this.EditTextsToolStripMenuItem.Name = "EditTextsToolStripMenuItem";
            this.EditTextsToolStripMenuItem.Click += new System.EventHandler(this.EditTextsToolStripMenuItem_Click);
            // 
            // WindowsMenu
            // 
            resources.ApplyResources(this.WindowsMenu, "WindowsMenu");
            this.WindowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.TileVerticalToolStripMenuItem,
            this.TileHorizontalToolStripMenuItem,
            this.ArrangeIconsToolStripMenuItem});
            this.WindowsMenu.Name = "WindowsMenu";
            // 
            // cascadeToolStripMenuItem
            // 
            resources.ApplyResources(this.cascadeToolStripMenuItem, "cascadeToolStripMenuItem");
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // TileVerticalToolStripMenuItem
            // 
            resources.ApplyResources(this.TileVerticalToolStripMenuItem, "TileVerticalToolStripMenuItem");
            this.TileVerticalToolStripMenuItem.Name = "TileVerticalToolStripMenuItem";
            this.TileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // TileHorizontalToolStripMenuItem
            // 
            resources.ApplyResources(this.TileHorizontalToolStripMenuItem, "TileHorizontalToolStripMenuItem");
            this.TileHorizontalToolStripMenuItem.Name = "TileHorizontalToolStripMenuItem";
            this.TileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // ArrangeIconsToolStripMenuItem
            // 
            resources.ApplyResources(this.ArrangeIconsToolStripMenuItem, "ArrangeIconsToolStripMenuItem");
            this.ArrangeIconsToolStripMenuItem.Name = "ArrangeIconsToolStripMenuItem";
            this.ArrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // LanguageToolStripMenuItem
            // 
            resources.ApplyResources(this.LanguageToolStripMenuItem, "LanguageToolStripMenuItem");
            this.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem";
            // 
            // HelpMenu
            // 
            resources.ApplyResources(this.HelpMenu, "HelpMenu");
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            // 
            // AboutToolStripMenuItem
            // 
            resources.ApplyResources(this.AboutToolStripMenuItem, "AboutToolStripMenuItem");
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripButton,
            this.SaveToolStripButton});
            this.toolStrip.Name = "toolStrip";
            this.toolTip.SetToolTip(this.toolStrip, resources.GetString("toolStrip.ToolTip"));
            // 
            // OpenToolStripButton
            // 
            resources.ApplyResources(this.OpenToolStripButton, "OpenToolStripButton");
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.Click += new System.EventHandler(this.OpenWorkspace);
            // 
            // SaveToolStripButton
            // 
            resources.ApplyResources(this.SaveToolStripButton, "SaveToolStripButton");
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveWorkspace);
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Name = "statusStrip";
            this.toolTip.SetToolTip(this.statusStrip, resources.GetString("statusStrip.ToolTip"));
            // 
            // toolStripStatusLabel
            // 
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            // 
            // RAY
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "RAY";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RAY_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem ManageTexturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WindowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ArrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripButton OpenToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem LanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditTextsToolStripMenuItem;
    }
}



