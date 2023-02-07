using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using static RAY.FontManager;
using static RAY.TextureManager;

namespace RAY
{
    public partial class Textures : Form
    {
        public bool Saved = true;

        private int _currentElement = 0;

        public Textures()
        {
            InitializeComponent();
        }

        private void Textures_Load(object sender, EventArgs e)
        {
            Texture1PictureBox.Image = new Bitmap(RAY.Bitmaps[0]);
            Texture2PictureBox.Image = new Bitmap(RAY.Bitmaps[1]);
            Texture3PictureBox.Image = new Bitmap(RAY.Bitmaps[2]);
            Texture24BitPictureBox.Image = new Bitmap(RAY.Bitmaps[3]);
            Let2PictureBox.Image = new Bitmap(RAY.Bitmaps[4]);
        }

        private void NextSpriteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Pointer pointer = RAY.FontManager.Pointers.ElementAt(_currentElement);

                if (pointer.TexturePageInfo > 0)
                {
                    int realX = 0;
                    int realY = 0;
                    Image image = null;
                    Color color = Color.Yellow;

                    if (pointer.TexturePageInfo == 26)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY + 30;
                        image = Let2PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 27)
                    {
                        realX = pointer.ImageOffsetInPageX + 256;
                        realY = pointer.ImageOffsetInPageY + 30;
                        image = Let2PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 10)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY - 226;
                        image = Let2PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 136)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY;
                        image = Texture3PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 134)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY;
                        image = Texture2PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 149)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY + 248;
                        image = Texture1PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 150)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY + 256;
                        image = Texture2PictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 22)
                    {
                        realX = pointer.ImageOffsetInPageX;
                        realY = pointer.ImageOffsetInPageY + 256;
                        image = Texture24BitPictureBox.Image;
                    }
                    else if (pointer.TexturePageInfo == 23)
                    {
                        realX = pointer.ImageOffsetInPageX + 256;
                        realY = pointer.ImageOffsetInPageY + 256;
                        image = Texture24BitPictureBox.Image;
                    }

                    if (image != null)
                    {
                        Bitmap bm = new Bitmap(pointer.Width, pointer.height);

                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            Rectangle rect = new Rectangle
                            {
                                X = realX,
                                Y = realY,
                                Width = pointer.Width,
                                Height = pointer.height
                            };

                            g.DrawImage(image, 0, 0, rect, GraphicsUnit.Pixel);
                        }
                        PreviewPictureBox.Image = bm;

                        using (Graphics g = Graphics.FromImage(image))
                        {
                            var fill = new Pen(color);

                            g.DrawRectangle(fill, realX, realY, pointer.Width, pointer.height);
                        }
                        Texture1PictureBox.Refresh();
                        Texture2PictureBox.Refresh();
                        Texture3PictureBox.Refresh();
                        Texture24BitPictureBox.Refresh();
                        Let2PictureBox.Refresh();
                    }
                }
            }
            catch
            {
                return;
            }

            IndexLabel.Text = $"{_currentElement}";
            _currentElement++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
        }

        private void TexturePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                import4bitRawToolStripMenuItem.Enabled = false;
                import8bitRawToolStripMenuItem.Enabled = false;
                exportAs4bitRawToolStripMenuItem.Enabled = false;
                exportAs8bitRawToolStripMenuItem.Enabled = false;
                exportAsPngToolStripMenuItem.Enabled = false;

                if (sender == Texture1PictureBox)
                {
                    import8bitRawToolStripMenuItem.Enabled = true;
                    exportAs8bitRawToolStripMenuItem.Enabled = true;
                    exportAsPngToolStripMenuItem.Enabled = true;
                }
                else if (sender == PreviewPictureBox)
                {
                    exportAsPngToolStripMenuItem.Enabled = true;
                }
                else
                {
                    import4bitRawToolStripMenuItem.Enabled = true;
                    import8bitRawToolStripMenuItem.Enabled = true;
                    exportAs4bitRawToolStripMenuItem.Enabled = true;
                    exportAs8bitRawToolStripMenuItem.Enabled = true;
                    exportAsPngToolStripMenuItem.Enabled = true;
                }

                exportMenuStrip.Show((Control)sender, e.X, e.Y);
            }
        }

        private void Import4bitRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Texture selectedTexture = GetTextureBySender(exportMenuStrip.SourceControl);

            if (selectedTexture != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "RAW|*.raw";

                if (exportMenuStrip.SourceControl == Let2PictureBox)
                {
                    openFileDialog.Filter += "|LET2.IMG|LET2.IMG";
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (RAY.TextureManager.Import(selectedTexture, openFileDialog.FileName))
                    {
                        UpdatePictureBoxBySender(exportMenuStrip.SourceControl);

                        Saved = false;
                    }
                    else
                    {
                        MessageBox.Show(Strings.ERROR_IMPORTING_IMAGE, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void Import8bitRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Texture selectedTexture = GetTextureBySender(exportMenuStrip.SourceControl);

            if (selectedTexture != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "RAW|*.raw";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bool result;

                    if (exportMenuStrip.SourceControl == Texture1PictureBox
                        || exportMenuStrip.SourceControl == Texture2PictureBox
                        || exportMenuStrip.SourceControl == Texture3PictureBox)
                    {
                        result = RAY.TextureManager.Import(selectedTexture, openFileDialog.FileName);
                    }
                    else
                    {
                        result = RAY.TextureManager.Reduce(selectedTexture, openFileDialog.FileName);
                    }

                    if (result)
                    {
                        UpdatePictureBoxBySender(exportMenuStrip.SourceControl);

                        Saved = false;
                    }
                    else
                    {
                        MessageBox.Show(Strings.ERROR_IMPORTING_IMAGE, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void ExportAs4bitRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Texture selectedTexture = GetTextureBySender(exportMenuStrip.SourceControl);

            if (selectedTexture != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "RAW|*.raw";

                if(exportMenuStrip.SourceControl == Let2PictureBox)
                {
                    saveFileDialog.Filter += "|LET2.IMG|LET2.IMG";
                }

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RAY.TextureManager.Export(selectedTexture, saveFileDialog.FileName);
                }
            }
        }

        private void ExportAs8bitRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Texture selectedTexture = GetTextureBySender(exportMenuStrip.SourceControl);

            if (selectedTexture != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "RAW|*.raw";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (exportMenuStrip.SourceControl == Texture1PictureBox 
                        || exportMenuStrip.SourceControl == Texture2PictureBox 
                        || exportMenuStrip.SourceControl == Texture3PictureBox)
                    {
                        RAY.TextureManager.Export(selectedTexture, saveFileDialog.FileName);
                    }
                    else
                    {
                        RAY.TextureManager.Expand(selectedTexture, saveFileDialog.FileName);
                    }
                }
            }
        }

        private void ExportAsPngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)exportMenuStrip.SourceControl;
            Bitmap bitmap = null;

            if (pictureBox == Texture1PictureBox)
            {
                bitmap = RAY.Bitmaps[0];
            }
            else if (pictureBox == Texture2PictureBox)
            {
                bitmap = RAY.Bitmaps[1];
            }
            else if (pictureBox == Texture3PictureBox)
            {
                bitmap = RAY.Bitmaps[2];
            }
            else if (pictureBox == Texture24BitPictureBox)
            {
                bitmap = RAY.Bitmaps[3];
            }
            else if (pictureBox == Let2PictureBox)
            {
                bitmap = RAY.Bitmaps[4];
            }

            if (bitmap != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "PNG|*.png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void Textures_FormClosed(object sender, FormClosedEventArgs e)
        {
            Texture1PictureBox.Image.Dispose();
            Texture2PictureBox.Image.Dispose();
            Texture3PictureBox.Image.Dispose();
            Texture24BitPictureBox.Image.Dispose();
            Let2PictureBox.Image.Dispose();
        }

        private void Textures_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                DialogResult result = MessageBox.Show(Strings.THERE_ARE_PENDING_CHANGES_DO_YOU_WANT_TO_SAVE, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.OK)
                {
                    SaveTextures();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveTextures();
        }

        private void SaveTextures()
        {
            if (RAY.TextureManager.Save())
            {
                MessageBox.Show(Strings.SAVED, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Strings.ERROR_SAVING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Saved = true;
            RAY.Saved = false;
        }

        private Texture GetTextureBySender(object sender)
        {
            Texture selectedTexture = null;

            if (sender == Texture1PictureBox)
            {
                selectedTexture = RAY.TextureManager.textures[0];
            }
            else if (sender == Texture2PictureBox || sender == Texture24BitPictureBox)
            {
                selectedTexture = RAY.TextureManager.textures[1];
            }
            else if (sender == Texture3PictureBox)
            {
                selectedTexture = RAY.TextureManager.textures[2];
            }
            else if (sender == Let2PictureBox)
            {
                selectedTexture = RAY.TextureManager.textures[3];
            }

            return selectedTexture;
        }

        private void UpdatePictureBoxBySender(object sender)
        {
            if (sender == Texture1PictureBox)
            {
                RAY.Bitmaps[0] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[0], TextureManager.Colors);
                Texture1PictureBox.Image = new Bitmap(RAY.Bitmaps[0]);
            }
            else if (sender == Texture2PictureBox || sender == Texture24BitPictureBox)
            {
                RAY.Bitmaps[1] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[1], TextureManager.Colors);
                RAY.Bitmaps[3] = RAY.TextureManager.Clut4bit(RAY.TextureManager.textures[1], TextureManager.Let2);
                Texture2PictureBox.Image = new Bitmap(RAY.Bitmaps[1]);
                Texture24BitPictureBox.Image = new Bitmap(RAY.Bitmaps[3]);
            }
            else if (sender == Texture3PictureBox)
            {
                RAY.Bitmaps[2] = RAY.TextureManager.Clut8bit(RAY.TextureManager.textures[2], TextureManager.Colors);
                Texture3PictureBox.Image = new Bitmap(RAY.Bitmaps[2]);
            }
            else if (sender == Let2PictureBox)
            {
                RAY.Bitmaps[4] = RAY.TextureManager.Clut4bit(RAY.TextureManager.textures[3], TextureManager.Let2);
                Let2PictureBox.Image = new Bitmap(RAY.Bitmaps[4]);
            }
        }
    }
}
