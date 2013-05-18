//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 21JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  MIDP PNG Encoder for J2ME
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.Cody Konior
     */
    internal static class PNGEncoder
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return a PNG stored in a byte array from the supplied values.
         *
         * @param Width   the Width of the image
         * @param Height  the Height of the image
         * @param alpha   the byte array of the alpha channel
         * @param red     the byte array of the red channel
         * @param green   the byte array of the green channel
         * @param blue    the byte array of the blue channel
         * @return        a byte array containing PNG data
         */
        public static byte[] GetPngrgb(int width, int height, int[] rgb)
        {
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(96,96);
            for(int i=0;i<width;i++)
            {
                for(int j=0;j<height;j++)
                {
                    bitmap.SetPixel(j,i,Color.FromArgb(rgb[i*height+j]));
                }
            }
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream,ImageFormat.Png);
            return memoryStream.GetBuffer();
        }
    }
   
}
