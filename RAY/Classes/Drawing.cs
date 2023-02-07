using System.Drawing;
using static RAY.FontManager;

namespace RAY.Classes
{
    internal class Drawing
    {
        public class DrawInfo
        {
            public int RealX = 0;
            public int RealY = 0;
            public Image Image = null;
        }

        public static DrawInfo GetDrawInfo(Pointer pointer)
        {
            DrawInfo result = new DrawInfo();

            if (pointer.TexturePageInfo == 26)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY + 30;
                result.Image = RAY.Bitmaps[4];
            }
            else if (pointer.TexturePageInfo == 27)
            {
                result.RealX = pointer.ImageOffsetInPageX + 256;
                result.RealY = pointer.ImageOffsetInPageY + 30;
                result.Image = RAY.Bitmaps[4];
            }
            else if (pointer.TexturePageInfo == 10)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY - 226;
                result.Image = RAY.Bitmaps[4];
            }
            else if (pointer.TexturePageInfo == 136)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY;
                result.Image = RAY.Bitmaps[2];
            }
            else if (pointer.TexturePageInfo == 134)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY;
                result.Image = RAY.Bitmaps[1];
            }
            else if (pointer.TexturePageInfo == 149)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY + 248;
                result.Image = RAY.Bitmaps[0];
            }
            else if (pointer.TexturePageInfo == 150)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY + 256;
                result.Image = RAY.Bitmaps[1];
            }
            else if (pointer.TexturePageInfo == 22)
            {
                result.RealX = pointer.ImageOffsetInPageX;
                result.RealY = pointer.ImageOffsetInPageY + 256;
                result.Image = RAY.Bitmaps[3];
            }
            else if (pointer.TexturePageInfo == 23)
            {
                result.RealX = pointer.ImageOffsetInPageX + 256;
                result.RealY = pointer.ImageOffsetInPageY + 256;
                result.Image = RAY.Bitmaps[3];
            }

            return result;
        }

        public static byte GetRealWidth(int index)
        {
            if (index < 77)
            {
                return RAY.FontManager.WidthsSmall[index];
            }
            else
            {
                return RAY.FontManager.WidthsMedium[index];
            }
        }

        public static void SetRealWidth(int index, byte value)
        {
            if (index < 77)
            {
                RAY.FontManager.WidthsSmall[index] = value;
            }
            else
            {
                RAY.FontManager.WidthsMedium[index] = value;
            }
        }
    }
}
