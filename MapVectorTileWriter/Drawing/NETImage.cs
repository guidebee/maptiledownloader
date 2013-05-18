using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MapDigit.GIS.Drawing;

namespace MapDigit.Drawing
{
    class NETImage : IImage
    {
        internal Bitmap image = null;

        public static IImage createImage(Stream stream)
        {
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(stream);
            lwuitImage.image.SetResolution(96, 96);
            return lwuitImage;
        }

        public static IImage createImage(int[] rgb, int width, int height)
        {
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(width, height);
            lwuitImage.image.SetResolution(96, 96);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    lwuitImage.image.SetPixel(i, j, System.Drawing.Color.FromArgb(rgb[i * height + j]));
                }
            }
            return lwuitImage;
        }
        public static IImage createImage(int width,
                                    int height)
        {
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(width, height);
            lwuitImage.image.SetResolution(96,96);
            return lwuitImage;
        }

        public static IImage createImage(byte[] bytes,
                                    int offset,
                                    int len)
        {
            //NETImage lwuitImage = new NETImage();
            //lwuitImage.image = Bitmap.CreateImage(bytes, offset, len);
            MemoryStream memoryStream = new MemoryStream(bytes, offset, len);
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(memoryStream);
            lwuitImage.image.SetResolution(96, 96);
            return lwuitImage;


        }

        public IImage SubImage(int x, int y, int width, int height, bool processAlpha)
        {
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(width, height);
            lwuitImage.image.SetResolution(96, 96);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int rgb = image.GetPixel(x + i, y + j).ToArgb();
                    lwuitImage.image.SetPixel(i, j, System.Drawing.Color.FromArgb(rgb));
                }
            }


            return lwuitImage;
        }

        public IGraphics GetGraphics()
        {
            NETGraphics lwuitGraphics = new NETGraphics(Graphics.FromImage(image));

            return lwuitGraphics;
        }

        public int[] GetRGB()
        {
            int[] rgb = new int[image.Width * image.Height];
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    rgb[i * image.Width + j] = image.GetPixel(j, i).ToArgb();
                }
            }
            return rgb;
        }

        public IImage ModifyAlpha(byte alpha, int removeColor)
        {
            NETImage lwuitImage = new NETImage();
            lwuitImage.image = new Bitmap(image.Width, image.Height);
            lwuitImage.image.SetResolution(96, 96);
            System.Drawing.Color emptyColor = System.Drawing.Color.FromArgb(0);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    System.Drawing.Color rgb = image.GetPixel(i, j);
                    System.Drawing.Color rgb1 = System.Drawing.Color.FromArgb(alpha, rgb.R, rgb.G, rgb.B);
                    if ((rgb.ToArgb() & 0xffffff) == removeColor)
                    {
                        lwuitImage.image.SetPixel(i, j, emptyColor);
                    }
                    else
                    {
                        lwuitImage.image.SetPixel(i, j, rgb1);
                    }

                }
            }
            return lwuitImage;
        }

        public int GetHeight()
        {
            return image.Height;
        }

        public int GetWidth()
        {
            return image.Width;
        }

        public Object GetNativeImage()
        {
            return image;
        }

    }

}
