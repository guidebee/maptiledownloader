using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MapDigit.GIS.Drawing;

namespace MapDigit.Drawing
{
    public class NETGraphicsFactory : AbstractGraphicsFactory{


    private static readonly NETGraphicsFactory INSTANCE=new NETGraphicsFactory();

    private NETGraphicsFactory(){

    }

    public static NETGraphicsFactory getInstance(){
        return INSTANCE;
    }
    public override IDisplay GetDisplayInstance() {
        return NETDisplay.getInstance();
    }

    public override IImage CreateImage(Stream stream) {
        try {
            return NETImage.createImage(stream);
        } catch (IOException ex) {
           
        }
        return null;
    }



    public override IImage CreateImage() {
        return null;
    }

    public override IImage CreateImage(int width, int height) {
        return NETImage.createImage(width,height);
    }

    public override IImage CreateImage(int[] rgb, int width, int height)
    {
        return NETImage.createImage(rgb, width, height);
    }

    public override IImage CreateImage(byte[] bytes, int offset, int len)
    {
        return NETImage.createImage(bytes, offset, len);
    }

    public override IFont CreateFont(Object nativeFont) {
        if (nativeFont is Font) {
            NETFont lwuitFont = new NETFont();
            lwuitFont.font = (Font)nativeFont;
            return lwuitFont;
        }else{
            throw new ArgumentException("Font type is not valid");
        }

    }

}

}
