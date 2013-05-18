using System;
using System.Drawing;
using MapDigit.GIS.Drawing;

namespace MapDigit.Drawing
{
    public class NETGraphics : IGraphics
    {
        public Graphics graphics;
        private System.Drawing.Color color = System.Drawing.Color.Black;
        private System.Drawing.Font font = new Font(FontFamily.GenericSerif, 12);


        public NETGraphics(Graphics graphics)
        {
            this.graphics = graphics;


        }

        public void SetClip(int x, int y, int width, int height)
        {
            graphics.SetClip(new Rectangle(x,y,width,height));
        }

        public void DrawImage(IImage img, int x, int y)
        {
            graphics.DrawImage(((NETImage)img).image, x, y);
        }

        public void SetColor(int RGB)
        {
            color = System.Drawing.Color.FromArgb(RGB);
        }

        public void FillRect(int x, int y, int width, int height)
        {
            graphics.FillRectangle(new System.Drawing.SolidBrush(color),x,y,width,height);
        }

        public void DrawRGB(int[] rgbData, int offset, int scanlength, int x, int y, int w, int h, bool processAlpha)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(w, h);
            for(int i=0;i<w;i++)
            {
                for(int j=0;j<h;j++)
                {
                    image.SetPixel(i, j, System.Drawing.Color.FromArgb(rgbData[offset + (i - x) + (j - y) * scanlength] ));
                }
            }
            graphics.DrawImage(image,x,y);

        }

        public void SetFont(IFont font)
        {

            this.font =(Font) font.GetNativeFont();
 
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            graphics.DrawLine(new System.Drawing.Pen(color),x1, y1, x2, y2);
        }

        public void DrawString(String str, int x, int y)
        {
             graphics.DrawString(str,font,new System.Drawing.SolidBrush(color),x,y);
        }

        public void DrawRect(int x, int y, int width, int height)
        {
            graphics.DrawRectangle(new System.Drawing.Pen(color), x, y, width, height);
        }

    }

}
