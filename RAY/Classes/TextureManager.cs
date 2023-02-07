using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAY
{
    internal class TextureManager : IDisposable
    {
        public static Color[] Colors = new Color[256];
        public static Color[] Let2 = new Color[16];

        public class Texture
        {
            public int Width;
            public int Height;
            public int param_3;
            public int param_4;
            public long Pointer;

            public byte[] Buffer;
        }

        public List<Texture> textures = new List<Texture>()
        {
            new Texture() { Width = 64, Height = 504, param_3 = 320, param_4 = 8, Pointer = 0x487AC},
            new Texture() { Width = 128, Height = 512, param_3 = 384, param_4 = 0, Pointer = 0x583AC},
            new Texture() { Width = 128, Height = 19, param_3 = 512, param_4 = 0, Pointer = 0x783AC},
            new Texture() { Width = 128, Height = 196, param_3 = 0, param_4 = 0, Pointer = 0}
        };

        public void Load()
        {
            LoadPalette(FileInfo.Colors, Colors);
            LoadPalette(FileInfo.Let2, Let2);

            ExtractImage(textures[0]);
            ExtractImage(textures[1]);
            ExtractImage(textures[2]);

            LoadLet2(textures[3]);
        }

        public bool Save()
        {
            bool result;

            result = InsertImage(textures[0]);

            if (!result) return result;

            result = InsertImage(textures[1]);

            if (!result) return result;

            result = InsertImage(textures[2]);

            if (!result) return result;

            result = SaveLet2(textures[3]);

            return result;
        }

        private void LoadLet2(Texture texture)
        {
            using (FileStream fs = new FileStream(FileInfo.Let2Img, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    texture.Buffer = br.ReadBytes((int)fs.Length);
                }
            }
        }

        private bool SaveLet2(Texture texture)
        {
            try
            {
                using (FileStream fs = new FileStream(FileInfo.Let2Img, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(texture.Buffer);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private int ExtractImage(Texture texture)
        {
            int bytes = texture.Width * texture.Height * 2;

            using (FileStream fs = new FileStream(FileInfo.RayXXX, FileMode.Open))
            {
                fs.Position = texture.Pointer;

                using (BinaryReader br = new BinaryReader(fs))
                {
                    texture.Buffer = br.ReadBytes(bytes);
                }
            }

            return bytes;
        }

        private bool InsertImage(Texture texture)
        {
            try
            {
                using (FileStream fs = new FileStream(FileInfo.RayXXX, FileMode.Open))
                {
                    fs.Position = texture.Pointer;

                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(texture.Buffer);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void LoadPalette(string filename, Color[] clut)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                fs.Position = 0x18;

                using (BinaryReader br = new BinaryReader(fs))
                {
                    for (int i = 0; i < clut.Length; i++)
                    {
                        byte r = br.ReadByte();
                        byte g = br.ReadByte();
                        byte b = br.ReadByte();
                        byte a = br.ReadByte();

                        clut[i] = Color.FromArgb(255, r, g, b);
                    }
                }
            }
        }

        public Bitmap Clut4bit(Texture texture, Color[] clut)
        {
            int x = 0;
            int y = 0;

            Bitmap bitmap = new Bitmap(texture.Width * 4, texture.Height);

            for (int i = 0; i < texture.Buffer.Length; i++)
            {
                byte byt = texture.Buffer[i];

                int p1 = byt & 0x0F;

                bitmap.SetPixel(x, y, clut[p1]);

                if (x < bitmap.Width - 1)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                }

                int p2 = (byt & 0xF0) >> 4;
                bitmap.SetPixel(x, y, clut[p2]);

                if (x < bitmap.Width - 1)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                }
            }

            return bitmap;
        }

        public Bitmap Clut8bit(Texture texture, Color[] clut)
        {
            int x = 0;
            int y = 0;

            Bitmap bitmap = new Bitmap(texture.Width * 2, texture.Height);

            for (int i = 0; i < texture.Buffer.Length; i++)
            {
                byte p = texture.Buffer[i];

                bitmap.SetPixel(x, y, clut[p]);

                if (x < bitmap.Width - 1)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                }
            }

            return bitmap;
        }

        public bool Import(Texture texture, string src)
        {
            using (FileStream fs = new FileStream(src, FileMode.Open))
            {
                if (fs.Length == texture.Width * texture.Height * 2)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        texture.Buffer = new byte[fs.Length];

                        while (fs.Position < fs.Length)
                        {
                            texture.Buffer[fs.Position] = br.ReadByte();
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void Export(Texture texture, string dst)
        {
            using (FileStream fs2 = new FileStream(dst, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs2))
                {
                    for (int i = 0; i < texture.Buffer.Length; i++)
                    {
                        bw.Write(texture.Buffer[i]);
                    }
                }
            }
        }

        public void Expand(Texture texture, string dst)
        {
            using (FileStream fs2 = new FileStream(dst, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs2))
                {
                    for (int i = 0; i < texture.Buffer.Length; i++)
                    {
                        byte s = texture.Buffer[i];

                        bw.Write((byte)(s & 0x0F));
                        bw.Write((byte)((s & 0xF0) >> 4));
                    }
                }
            }
        }

        public bool Reduce(Texture texture, string src)
        {
            using (FileStream fs = new FileStream(src, FileMode.Open))
            {
                if (fs.Length == texture.Width * texture.Height * 4)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        texture.Buffer = new byte[fs.Length / 2];
                        int i = 0;

                        while (fs.Position < fs.Length)
                        {
                            byte a = br.ReadByte();
                            byte b = br.ReadByte();

                            byte c = (byte)(a | (b << 4));

                            texture.Buffer[i++] = c;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
        }
    }
}
