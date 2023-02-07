using RAY.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static RAY.Classes.Drawing;
using static RAY.FontManager;

namespace RAY
{
    public partial class Fonts : Form
    {
        public bool Saved = true;

        private Brush _hiddenBrush = new SolidBrush(Color.FromArgb(120, 200, 200, 200));
        private bool _changePrevented = false;
        private int _selectedIndex = -1;
        private Pointer _saved = null;

        public Fonts()
        {
            InitializeComponent();
        }

        public void SaveData()
        {
            if (RAY.FontManager.SaveTable() && RAY.ScriptManager.SaveScript())
            {
                MessageBox.Show(Strings.SAVED, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Saved = true;
            }
            else
            {
                MessageBox.Show(Strings.ERROR_SAVING, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fonts_Load(object sender, EventArgs e)
        {
            LoadSprites();
        }

        private void Fonts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                DialogResult result = MessageBox.Show(Strings.THERE_ARE_PENDING_CHANGES_DO_YOU_WANT_TO_SAVE, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    SaveSprites();
                    SaveData();
                }
            }
        }

        private void Fonts_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void SpritesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpritesListView.SelectedIndices.Count > 0)
            {
                if (!Saved && SpritesListView.SelectedIndices[0] != _selectedIndex)
                {
                    SpritesListView.SelectedIndices.Clear();
                    SpritesListView.Items[_selectedIndex].Selected = true;
                }

                if(_selectedIndex < 51)
                {
                    EquivalentCharTextBox.Enabled = false;
                }
                else if (_selectedIndex < 77)
                {
                    EquivalentCharTextBox.Enabled = true;
                }
                else if (_selectedIndex < 127)
                {
                    EquivalentCharTextBox.Enabled = false;
                }
                else
                {
                    EquivalentCharTextBox.Enabled = true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Pointer pointer = SavePointerInfo();

            if (pointer != null)
            {
                Saved = false;

                if (PreviewPictureBox.Image != null)
                {
                    PreviewPictureBox.Image.Dispose();
                }

                PreviewPictureBox.Image = GetSprite(pointer);

                if (pointer.ReplacementChar != '\0')
                {
                    EquivalentCharTextBox.Text = $"{pointer.ReplacementChar}";
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSprites();
        }

        private void CopyValuesButton_Click(object sender, EventArgs e)
        {
            _saved = SavePointerInfo();
        }

        private void PasteValuesButton_Click(object sender, EventArgs e)
        {
            FillSpriteInfo(_saved);
        }

        private void SpritesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.ItemIndex != _selectedIndex)
            {
                if (!Saved && _selectedIndex > -1)
                {
                    if (!_changePrevented)
                    {
                        DialogResult result = MessageBox.Show(Strings.THERE_ARE_PENDING_CHANGES_DO_YOU_WANT_TO_SAVE, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (result == DialogResult.Cancel)
                        {
                            _changePrevented = true;
                            return;
                        }
                        else
                        {
                            if(result == DialogResult.Yes)
                            {
                                SaveSprites();
                            }
                            Saved = true;
                        }
                    }
                    else
                    {
                        _changePrevented = false;
                    }
                }
            } 

            if (e.IsSelected && Saved)
            {
                _selectedIndex = e.ItemIndex;
                FillSpriteInfo(_selectedIndex);
            }
        }

        private void LoadSprites()
        {
            List<ListViewItem> listItems = new List<ListViewItem>();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(16, 16);
            int i = 0;

            SpritesListView.SmallImageList = imageList;
            SpritesListView.LargeImageList = imageList;

            foreach (Pointer pointer in RAY.FontManager.Pointers)
            {
                pointer.RealWidth = GetRealWidth(i);
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
                    imageList.Images.Add($"sprite_{i}", sprite);
                }

                ListViewItem item = new ListViewItem();
                item.SubItems.Add($"{pointer.Index} sprite_{i}");
                item.ImageKey = $"sprite_{i}";

                listItems.Add(item);

                i++;
            }

            SpritesListView.BeginUpdate();
            SpritesListView.Items.AddRange(listItems.ToArray());
            SpritesListView.EndUpdate();
        }

        private void SaveSprites()
        {
            if (_selectedIndex > -1)
            {
                Pointer pointer = SavePointerInfo();

                if (pointer != null)
                {
                    RAY.FontManager.Pointers[_selectedIndex] = pointer;

                    if (_selectedIndex > 50 && _selectedIndex < 77)
                    {
                        RAY.FontManager.Pointers[_selectedIndex - 51 + 127].ReplacementChar = pointer.ReplacementChar;
                    }
                    else if (_selectedIndex > 126)
                    {
                        RAY.FontManager.Pointers[_selectedIndex - 127 + 51].ReplacementChar = pointer.ReplacementChar;
                    }

                    SetRealWidth(_selectedIndex, pointer.RealWidth);

                    Saved = true;
                }

                SpritesListView.LargeImageList.Images.RemoveByKey($"sprite_{_selectedIndex}");
                SpritesListView.LargeImageList.Images.Add($"sprite_{_selectedIndex}", PreviewPictureBox.Image);
                SpritesListView.Refresh();
            }
        }

        private Pointer SavePointerInfo()
        {
            byte index;
            byte width;
            byte height;
            byte collisionWidth;
            byte collisionHeight;
            byte unk1;
            byte unk2;
            byte unk3;
            ushort PaletteInfo;
            ushort TexturePageInfo;
            byte ImageOffsetInPageX;
            byte ImageOffsetInPageY;
            byte RealWidth;

            if (!byte.TryParse(IndexTextBox.Text, out index)) return null;
            if (!byte.TryParse(WidthTextBox.Text, out width)) return null;
            if (!byte.TryParse(HeightTextBox.Text, out height)) return null;
            if (!byte.TryParse(DrawWidthTextBox.Text, out collisionWidth)) return null;
            if (!byte.TryParse(DrawHeightTextBox.Text, out collisionHeight)) return null;
            if (!byte.TryParse(Unk1TextBox.Text, out unk1)) return null;
            if (!byte.TryParse(Unk2TextBox.Text, out unk2)) return null;
            if (!byte.TryParse(Unk3TextBox.Text, out unk3)) return null;
            if (!ushort.TryParse(PaletteInfoTextBox.Text, System.Globalization.NumberStyles.HexNumber, null, out PaletteInfo)) return null;
            if (!ushort.TryParse(TexturePageInfoTextBox.Text, out TexturePageInfo)) return null;
            if (!byte.TryParse(SourceXTextBox.Text, out ImageOffsetInPageX)) return null;
            if (!byte.TryParse(SourceYTextBox.Text, out ImageOffsetInPageY)) return null;
            if (!byte.TryParse(RealWidthTextBox.Text, out RealWidth)) return null;

            Pointer pointer = new Pointer();

            pointer.Index = index;
            pointer.Width = width;
            pointer.height = height;
            pointer.DrawWidth = collisionWidth;
            pointer.DrawHeight = collisionHeight;
            pointer.unk1 = unk1;
            pointer.unk2 = unk2;
            pointer.unk3 = unk3;
            pointer.PaletteInfo = PaletteInfo;
            pointer.TexturePageInfo = TexturePageInfo;
            pointer.ImageOffsetInPageX = ImageOffsetInPageX;
            pointer.ImageOffsetInPageY = ImageOffsetInPageY;
            pointer.RealWidth = RealWidth;

            if (!string.IsNullOrEmpty(EquivalentCharTextBox.Text))
            {
                pointer.ReplacementChar = EquivalentCharTextBox.Text.ToLower()[0];
            }
            else
            {
                pointer.ReplacementChar = '\0';
            }

            return pointer;
        }

        private void FillSpriteInfo(int position)
        {
            Pointer pointer = RAY.FontManager.Pointers[position];
            pointer.RealWidth = GetRealWidth(position);

            FillSpriteInfo(pointer);
        }


        private void FillSpriteInfo(Pointer pointer)
        {
            IndexTextBox.Text = $"{pointer.Index}";
            WidthTextBox.Text = $"{pointer.Width}";
            HeightTextBox.Text = $"{pointer.height}";
            DrawWidthTextBox.Text = $"{pointer.DrawWidth}";
            DrawHeightTextBox.Text = $"{pointer.DrawHeight}";
            Unk1TextBox.Text = $"{pointer.unk1}";
            Unk2TextBox.Text = $"{pointer.unk2}";
            Unk3TextBox.Text = $"{pointer.unk3}";
            PaletteInfoTextBox.Text = $"{pointer.PaletteInfo.ToString("X")}";
            TexturePageInfoTextBox.Text = $"{pointer.TexturePageInfo}";
            SourceXTextBox.Text = $"{pointer.ImageOffsetInPageX}";
            SourceYTextBox.Text = $"{pointer.ImageOffsetInPageY}";
            RealWidthTextBox.Text = $"{pointer.RealWidth}";
            EquivalentCharTextBox.Text = $"{pointer.ReplacementChar}";

            if (PreviewPictureBox.Image != null)
            {
                PreviewPictureBox.Image.Dispose();
            }

            PreviewPictureBox.Image = GetSprite(pointer);

            Saved = true;
        }

        private Bitmap GetSprite(Pointer pointer)
        {
            Bitmap bm = null;

            if (pointer.TexturePageInfo > 0)
            {
                DrawInfo drawInfo = Drawing.GetDrawInfo(pointer);
                Color color = Color.Yellow;

                if (drawInfo.Image != null && pointer.Width > 0 && pointer.height > 0)
                {
                    bm = new Bitmap(pointer.Width * 4, pointer.height * 4);

                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;

                        Rectangle srect = new Rectangle
                        {
                            X = drawInfo.RealX,
                            Y = drawInfo.RealY,
                            Width = pointer.Width,
                            Height = pointer.height
                        };

                        Rectangle drect = new Rectangle
                        {
                            X = 0,
                            Y = 0,
                            Width = pointer.Width * 4,
                            Height = pointer.height * 4
                        };

                        g.DrawImage(drawInfo.Image, drect, srect, GraphicsUnit.Pixel);

                        g.FillRectangle(_hiddenBrush, pointer.DrawWidth * 4, 0, bm.Width, bm.Height);
                        g.DrawRectangle(Pens.Yellow, 0, 0, pointer.RealWidth * 4 - 1, pointer.height * 4 - 1);
                    }
                }
            }

            return bm;
        }
    }
}
