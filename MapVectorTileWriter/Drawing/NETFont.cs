using System;
using System.Drawing;
using MapDigit.GIS.Drawing;

namespace MapDigit.Drawing
{
    public class NETFont : IFont
    {

        internal Font font;
        public static  Graphics graphics;
        private readonly object syncObject = new object();

        static NETFont()
        {
            Bitmap map = new Bitmap(20, 20);
            graphics = Graphics.FromImage(map);

        }
        public Object GetNativeFont()
        {
            return font;
        }

        public int CharsWidth(char[] ch, int offset, int length)
        {
            lock (syncObject)
            {
                char[] str = new char[length];
                System.Array.Copy(ch, offset, str, 0, length);
                return (int) graphics.MeasureString(new string(str), font).Width;
            }

        }



    }

}
