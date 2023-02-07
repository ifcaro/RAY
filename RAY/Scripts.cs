using RAY.Classes;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static RAY.Classes.Drawing;
using static RAY.FontManager;

namespace RAY
{
    public partial class Scripts : Form
    {
        private Bitmap[] _sprites = new Bitmap[150];

        private int _selectedFont = 1;
        private int _blankSpaceWidth = 8;
        private int _lineHeight = 15;

        public Scripts()
        {
            InitializeComponent();
        }

        private void Scripts_Load(object sender, EventArgs e)
        {
            LoadSprites();

            DataGridView.DataSource = RAY.ScriptManager.Strings;

            DataGridView.Columns[0].HeaderText = Strings.SOURCE;
            DataGridView.Columns[0].ReadOnly = true;
            DataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridView.Columns[1].Visible = false;
            DataGridView.Columns[2].HeaderText = Strings.FINAL_TEXT;
            DataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Scripts_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < _sprites.Length; i++)
            {
                if (_sprites[i] != null)
                {
                    _sprites[i].Dispose();
                    _sprites[i] = null;
                }
            }

            Dispose();
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DrawString();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView.Rows[e.RowIndex].Cells[1].Value = RAY.ScriptManager.ReplaceCharsReverse((string)DataGridView.Rows[e.RowIndex].Cells[2].Value);

            RAY.Saved = false;
        }

        private void SmallFontButton_Click(object sender, EventArgs e)
        {
            _selectedFont = 1;
            _blankSpaceWidth = 8;
            _lineHeight = 15;

            DrawString();
        }

        private void MediumFontButton_Click(object sender, EventArgs e)
        {
            _selectedFont = 2;
            _blankSpaceWidth = 10;
            _lineHeight = 23;

            DrawString();
        }

        private void DrawString()
        {
            if (DataGridView.SelectedRows.Count > 0 && DataGridView.SelectedRows[0].Cells[1].Value != DBNull.Value)
            {
                byte[] text = (byte[])DataGridView.SelectedRows[0].Cells[1].Value;
                int textLength = GetStringLength(text);
                int xPos = 0;
                int yPos = 0;
                int width;
                int height;

                if (PreviewPictureBox.Image != null)
                {
                    PreviewPictureBox.BackgroundImage.Dispose();
                }

                GetTotalWidth(text, out width, out height);

                if (width > 0 && height > 0)
                {
                    Bitmap bitmap = new Bitmap(width, height);

                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;

                        g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);

                        for (int i = 0; i < textLength; i++)
                        {
                            if (text[i] == '/' && i > 0 && i < textLength - 3)
                            {
                                xPos = 0;
                                yPos += _lineHeight;
                            }
                            else if (text[i] == ' ')
                            {
                                xPos += _blankSpaceWidth;
                            }
                            else
                            {
                                int spriteIndex = GetSpriteByChar(text[i]);
                                if (spriteIndex > -1)
                                {
                                    Pointer pointer = RAY.FontManager.Pointers[spriteIndex];

                                    if (_sprites[spriteIndex] != null)
                                    {
                                        g.DrawImage(_sprites[spriteIndex], xPos, yPos);
                                    }

                                    xPos += pointer.RealWidth;
                                }
                            }
                        }
                    }

                    PreviewPictureBox.BackgroundImage = bitmap;
                }
                else
                {
                    PreviewPictureBox.BackgroundImage = null;
                }
            }
        }

        private void GetTotalWidth(byte[] text, out int xPos, out int yPos)
        {
            int textLength = GetStringLength(text);
            int totalWidth = 0;
            xPos = 0;
            yPos = _lineHeight;

            for (int i = 0; i < textLength; i++)
            {
                if (text[i] == '/' && i > 0 && i < textLength - 3)
                {
                    if (xPos > totalWidth)
                    {
                        totalWidth = xPos;
                    }
                    xPos = 0;
                    yPos += _lineHeight;
                }
                else if (text[i] == ' ')
                {
                    xPos += _blankSpaceWidth;
                }
                else
                {
                    int spriteIndex = GetSpriteByChar(text[i]);

                    if (spriteIndex > -1)
                    {
                        xPos += RAY.FontManager.Pointers[spriteIndex].RealWidth;
                    }
                }
            }

            if (totalWidth > xPos)
            {
                xPos = totalWidth;
            }
        }

        private int GetStringLength(byte[] text)
        {
            int length = 0;

            while (length < text.Length && text[length] != 0)
            {
                length++;
            }

            return length;
        }

        private void LoadSprites()
        {
            int i = 0;

            foreach (Pointer pointer in RAY.FontManager.Pointers)
            {
                pointer.RealWidth = Drawing.GetRealWidth(i);

                if (i > 50 && i < 77)
                {
                    pointer.ReplacementChar = RAY.FontManager.GetCharReplacement(i - 51 + 130);
                }
                else if (i > 126)
                {
                    pointer.ReplacementChar = RAY.FontManager.GetCharReplacement(i - 127 + 130);
                }

                Bitmap sprite = GetSprite(pointer);

                if (sprite != null)
                {
                    _sprites[i] = sprite;
                }

                i++;
            }
        }

        private Bitmap GetSprite(Pointer pointer)
        {
            Bitmap bm = null;

            if (pointer.TexturePageInfo > 0)
            {
                Color color = Color.Yellow;

                DrawInfo drawInfo = Drawing.GetDrawInfo(pointer);

                if (drawInfo.Image != null && pointer.Width > 0 && pointer.height > 0)
                {
                    bm = new Bitmap(pointer.Width, pointer.height);

                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;

                        Rectangle srect = new Rectangle
                        {
                            X = drawInfo.RealX,
                            Y = drawInfo.RealY + pointer.height - pointer.DrawHeight,
                            Width = pointer.DrawWidth,
                            Height = pointer.DrawHeight
                        };

                        Rectangle drect = new Rectangle
                        {
                            X = 0,
                            Y = pointer.height - pointer.DrawHeight,
                            Width = pointer.DrawWidth,
                            Height = pointer.DrawHeight
                        };

                        g.DrawImage(drawInfo.Image, drect, srect, GraphicsUnit.Pixel);
                    }
                }
            }

            return bm;
        }

        private int GetSpriteByChar(byte c)
        {
            int spriteIndex = -1;

            if (c == '?')
            {
                spriteIndex = 1;
            }
            else if (c == '!')
            {
                spriteIndex = 2;
            }
            else if (c == '.')
            {
                spriteIndex = 3;
            }
            else if (c == ':')
            {
                spriteIndex = 44;
            }
            else if (c == '´')
            {
                spriteIndex = 45;
            }
            else if (c == '|')
            {
                spriteIndex = 50;
            }
            else if (c > 64 && c < 91)
            {
                spriteIndex = c - 61;
            }
            else if (c > 96 && c < 123)
            {
                spriteIndex = c - 93;
            }
            else if (c > 47 && c < 58)
            {
                spriteIndex = c - 14;
            }
            else if (c >= 130)
            {
                spriteIndex = c - 130 + 51;
            }

            if (spriteIndex > -1 && _selectedFont == 2)
            {
                spriteIndex += 76;
            }

            return spriteIndex;
        }
    }
}
