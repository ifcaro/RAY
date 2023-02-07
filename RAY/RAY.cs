using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace RAY
{
    public partial class RAY : Form
    {
        private static Config _config = new Config();
        private static Fonts _fontsForm = null;
        private static Textures _texturesForm = null;
        private static Scripts _scriptsForm = null;

        internal static TextureManager TextureManager;
        internal static FontManager FontManager;
        internal static ScriptManager ScriptManager;
        internal static string WorkspacePath = null;
        internal static Bitmap[] Bitmaps = new Bitmap[5];

        internal static bool Saved
        {
            set
            {
                if (_fontsForm != null)
                {
                    _fontsForm.Saved = value;
                }
            }
        }

        public RAY()
        {
            _config.ReadConfig(FileInfo.Config);

            string selectedLanguage = _config.Get("General", "Language");
            bool positionSaved = false;
            int x;
            int y;
            int w;
            int h;

            if (int.TryParse(_config.Get("General", "x_pos"), out x))
            {
                Left = x;
                positionSaved = true;
            }

            if (int.TryParse(_config.Get("General", "y_pos"), out y))
            {
                Top = y;
                positionSaved = true;
            }

            if (int.TryParse(_config.Get("General", "width"), out w))
            {
                Width = w;
                positionSaved = true;
            }

            if (int.TryParse(_config.Get("General", "height"), out h))
            {
                Height = h;
                positionSaved = true;
            }

            if (positionSaved)
            {
                StartPosition = FormStartPosition.Manual;
            }

            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }

            ToolStripMenuItem[] languageMenuItems = SetLocale();
            InitializeComponent();

            LanguageToolStripMenuItem.DropDownItems.AddRange(languageMenuItems);
        }

        private void RAY_FormClosing(object sender, FormClosingEventArgs e)
        {
            _config.Set("General", "x_pos", $"{Left}");
            _config.Set("General", "y_pos", $"{Top}");
            _config.Set("General", "width", $"{Height}");
            _config.Set("General", "height", $"{Width}");

            _config.SaveConfig(FileInfo.Config);
        }

        private void OpenWorkspace(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string selectedFolder = _config.Get("General", "LastDirectory");

                if (!string.IsNullOrEmpty(selectedFolder))
                {
                    openFileDialog.InitialDirectory = selectedFolder;
                }
                else
                {
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }

                openFileDialog.ValidateNames = false;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.FileName = "Workspace";

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    selectedFolder = Path.GetDirectoryName(openFileDialog.FileName);

                    if (!File.Exists($@"{selectedFolder}\SLES_000.49"))
                    {
                        MessageBox.Show(Strings.SLES_000_49_MISSING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!File.Exists($@"{selectedFolder}\RAY.XXX"))
                    {
                        MessageBox.Show(Strings.RAY_XXX_MISSING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!File.Exists($@"{selectedFolder}\LET2.IMG"))
                    {
                        MessageBox.Show(Strings.LET2_IMG_MISSING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!File.Exists($@"{selectedFolder}\rayus.txt"))
                    {
                        MessageBox.Show(Strings.RAYUS_TXT_MISSING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    _config.Set("General", "LastDirectory", selectedFolder);

                    WorkspacePath = selectedFolder;

                    TextureManager = new TextureManager();
                    FontManager = new FontManager();
                    ScriptManager = new ScriptManager();

                    SaveToolStripButton.Enabled = true;
                    SaveToolStripMenuItem.Enabled = true;
                    ManageTexturesToolStripMenuItem.Enabled = true;
                    EditTextsToolStripMenuItem.Enabled = true;

                    toolStripStatusLabel.Text = $"{Strings.LOADING}...";
                    Application.DoEvents();
                    LoadData();

                    _fontsForm = new Fonts();
                    _fontsForm.MdiParent = this;
                    _fontsForm.Show();
                    _fontsForm.WindowState = FormWindowState.Maximized;
                    _fontsForm.Disposed += FontsForm_Disposed;

                    toolStripStatusLabel.Text = Strings.LOADED;
                }
            }
        }

        private void FontsForm_Disposed(object sender, EventArgs e)
        {
            for (int i = 0; i < Bitmaps.Length; i++)
            {
                if (Bitmaps[i] != null)
                {
                    Bitmaps[i].Dispose();
                    Bitmaps[i] = null;
                }
            }

            if (TextureManager != null)
            {
                TextureManager.Dispose();
                TextureManager = null;
            }

            if (FontManager != null)
            {
                FontManager.Dispose();
                FontManager = null;
            }

            if (ScriptManager != null)
            {
                ScriptManager.Dispose();
                ScriptManager = null;
            }

            SaveToolStripButton.Enabled = false;
            SaveToolStripMenuItem.Enabled = false;
            ManageTexturesToolStripMenuItem.Enabled = false;
            EditTextsToolStripMenuItem.Enabled = false;

            if (_fontsForm != null)
            {
                _fontsForm.Dispose();
                _fontsForm = null;
            }

            if (_texturesForm != null)
            {
                _texturesForm.Dispose();
                _texturesForm = null;
            }

            if (_scriptsForm != null)
            {
                _scriptsForm.Dispose();
                _scriptsForm = null;
            }

            WorkspacePath = null;


            GC.Collect();
        }

        private void SaveWorkspace(object sender, EventArgs e)
        {
            if (_fontsForm != null)
            {
                _fontsForm.SaveData();
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in LanguageToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            selectedItem.Checked = true;

            System.Threading.Thread.CurrentThread.CurrentUICulture = (CultureInfo)selectedItem.Tag;

            MessageBox.Show(Strings.RESTART_APP_TO_APPLY_CHANGES, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            _config.Set("General", "Language", $"{((CultureInfo)selectedItem.Tag).IetfLanguageTag}");
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WorkspacePath != null)
            {
                if (_texturesForm == null)
                {
                    _texturesForm = new Textures();
                    _texturesForm.MdiParent = this;
                    _texturesForm.Show();
                    _texturesForm.Disposed += TexturesForm_Disposed;
                    _texturesForm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    _texturesForm.Focus();
                }
            }
        }

        private void TexturesForm_Disposed(object sender, EventArgs e)
        {
            _texturesForm = null;
        }

        private void menuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.GetType().ToString() == "System.Windows.Forms.MdiControlStrip+SystemMenuItem")
            {
                e.Item.Visible = false;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }

        private void EditTextsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WorkspacePath != null)
            {
                if (_scriptsForm == null)
                {
                    _scriptsForm = new Scripts();
                    _scriptsForm.MdiParent = this;
                    _scriptsForm.Show();
                    _scriptsForm.Disposed += ScriptsForm_Disposed; ;
                    _scriptsForm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    _scriptsForm.Focus();
                }
            }
        }

        private void ScriptsForm_Disposed(object sender, EventArgs e)
        {
            _scriptsForm = null;
        }

        private void LoadData()
        {
            TextureManager.Load();

            RAY.Bitmaps[0] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[0], TextureManager.Colors);
            RAY.Bitmaps[1] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[1], TextureManager.Colors);
            RAY.Bitmaps[2] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[2], TextureManager.Colors);
            RAY.Bitmaps[3] = RAY.TextureManager.Clut4bit(RAY.TextureManager.textures[1], TextureManager.Let2);
            RAY.Bitmaps[4] = RAY.TextureManager.Clut4bit(RAY.TextureManager.textures[3], TextureManager.Let2);

            FontManager.ReadTable();

            ScriptManager.ReadScript();
        }

        private ToolStripMenuItem[] SetLocale()
        {
            List<ToolStripMenuItem> languageMenuItems = new List<ToolStripMenuItem>();
            bool found = false;

            var defaultItem = new ToolStripMenuItem("English");
            defaultItem.Tag = CultureInfo.InvariantCulture;
            defaultItem.Click += Item_Click;
            languageMenuItems.Add(defaultItem);

            string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
            string[] directories = Directory.GetDirectories(executablePath);
            foreach (string s in directories)
            {
                try
                {
                    DirectoryInfo langDirectory = new DirectoryInfo(s);
                    CultureInfo cultureInfo = CultureInfo.GetCultureInfo(langDirectory.Name);
                    var item = new ToolStripMenuItem(cultureInfo.DisplayName);
                    item.Tag = cultureInfo;
                    item.Click += Item_Click;
                    item.Checked = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == cultureInfo.TwoLetterISOLanguageName;

                    found |= item.Checked;

                    languageMenuItems.Add(item);
                }
                catch (Exception)
                {

                }
            }

            if (!found)
            {
                languageMenuItems[0].Checked = true;
            }

            return languageMenuItems.ToArray();
        }
    }
}
