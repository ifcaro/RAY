using System;
using System.Collections.Generic;
using System.IO;

namespace RAY
{
    internal class FontManager : IDisposable
    {
        private Config _customChars = new Config();

        public class Pointer
        {
            public uint ImageBuffer;
            public byte Index;
            public byte Width;
            public byte height;
            public byte DrawWidth;
            public byte DrawHeight;
            public byte unk1;
            public byte unk2;
            public byte unk3;
            public ushort PaletteInfo;
            public ushort TexturePageInfo;
            public byte ImageOffsetInPageX;
            public byte ImageOffsetInPageY;
            public ushort unk4;
            public byte RealWidth;
            public char ReplacementChar;
        }

        public List<Pointer> Pointers = new List<Pointer>();
        public byte[] WidthsSmall = new byte[0xA4];
        public byte[] WidthsMedium = new byte[0xA4];

        public void ReadTable()
        {
            _customChars.ReadConfig(FileInfo.Chars);

            using (FileStream fs = new FileStream(FileInfo.RayXXX, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Position = 0x28;
                    fs.Position = br.ReadUInt32() - 0x80010000;

                    for (int i = 0; i < 150; i++)
                    {
                        Pointer pointer = new Pointer()
                        {
                            ImageBuffer = br.ReadUInt32(),
                            Index = br.ReadByte(),
                            Width = br.ReadByte(),
                            height = br.ReadByte(),
                            DrawWidth = br.ReadByte(),
                            DrawHeight = br.ReadByte(),
                            unk1 = br.ReadByte(),
                            unk2 = br.ReadByte(),
                            unk3 = br.ReadByte(),
                            PaletteInfo = br.ReadUInt16(),
                            TexturePageInfo = br.ReadUInt16(),
                            ImageOffsetInPageX = br.ReadByte(),
                            ImageOffsetInPageY = br.ReadByte(),
                            unk4 = br.ReadUInt16()
                        };

                        Pointers.Add(pointer);
                    }
                }
            }

            ReadWidths();
        }

        public bool SaveTable()
        {
            try
            {
                using (FileStream fs = new FileStream(FileInfo.RayXXX, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            fs.Position = 0x28;
                            fs.Position = br.ReadUInt32() - 0x80010000;

                            for (int i = 0; i < 150; i++)
                            {
                                Pointer pointer = Pointers[i];

                                bw.Write(pointer.ImageBuffer);
                                bw.Write(pointer.Index);
                                bw.Write(pointer.Width);
                                bw.Write(pointer.height);
                                bw.Write(pointer.DrawWidth);
                                bw.Write(pointer.DrawHeight);
                                bw.Write(pointer.unk1);
                                bw.Write(pointer.unk2);
                                bw.Write(pointer.unk3);
                                bw.Write(pointer.PaletteInfo);
                                bw.Write(pointer.TexturePageInfo);
                                bw.Write(pointer.ImageOffsetInPageX);
                                bw.Write(pointer.ImageOffsetInPageY);
                                bw.Write(pointer.unk4);

                                if (pointer.ReplacementChar != '\0')
                                {
                                    if (i > 50 && i < 77)
                                    {
                                        SetCharReplacement(i - 51 + 130, pointer.ReplacementChar);
                                    }
                                    else if (i > 126)
                                    {
                                        SetCharReplacement(i - 127 + 130, pointer.ReplacementChar);
                                    }
                                }
                            }
                        }
                    }
                }

                _customChars.SaveConfig(FileInfo.Chars);

                SaveWidths();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public char GetCharReplacement(int c)
        {
            string value = _customChars.Get("Chars", $"{c}");

            if (!string.IsNullOrEmpty(value))
            {
                return char.ToLower(value[0]);
            }

            return '\0';
        }

        public int GetCharReverseReplacement(char c)
        {
            string value = _customChars.GetByValue("Chars", $"{c}");

            if (!string.IsNullOrEmpty(value))
            {
                return int.Parse(value);
            }

            return -1;
        }

        public void SetCharReplacement(int c, char value)
        {
            if (value != '\0')
            {
                _customChars.Set("Chars", $"{c}", $"{char.ToLower(value)}");
            }
            else
            {
                _customChars.Set("Chars", $"{c}", "");
            }
        }

        public void Dispose()
        {
            Pointers.Clear();
            Pointers = null;

            _customChars.Dispose();
        }

        private void ReadWidths()
        {
            using (FileStream fs = new FileStream(FileInfo.Exe, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Position = 0x9ed30;

                    WidthsSmall = br.ReadBytes(WidthsSmall.Length);

                    fs.Position = 0x9edd4;

                    WidthsMedium = br.ReadBytes(WidthsMedium.Length);
                }
            }
        }

        private bool SaveWidths()
        {
            try
            {
                using (FileStream fs = new FileStream(FileInfo.Exe, FileMode.Open))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        fs.Position = 0x9ed30;

                        bw.Write(WidthsSmall);

                        fs.Position = 0x9edd4;

                        bw.Write(WidthsMedium);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
