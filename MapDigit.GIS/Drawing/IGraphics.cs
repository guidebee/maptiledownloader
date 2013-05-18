//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 18JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Drawing
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  Graphics  interface.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public interface IGraphics
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the clip region for this graphics
         * @param X   the top left X coordinate.
         * @param Y   the top left Y coordinate.
         * @param Width the Width of the clip region.
         * @param Height the Height of the clip region.
         */
        void SetClip(int x, int y, int width, int height);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw an image.
         * @param img the image object.
         * @param X  the X coordinate where the image is drawn.
         * @param Y  the Y coordinate where the image is drawn.
         */
        void DrawImage(IImage img, int x, int y);


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the draw color
         * @param RGB an RGB color.
         */
        void SetColor(int rgb);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill an rectangle.
         * @param X  the top left X coordinate.
         * @param Y  the top left Y coordinate.
         * @param Width  the Width of the rectangle.
         * @param Height the Height of the rectangle.
         */
        void FillRect(int x, int y, int width, int height);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw an rectangle.
         * @param X  the top left X coordinate.
         * @param Y  the top left Y coordinate.
         * @param Width  the Width of the rectangle.
         * @param Height the Height of the rectangle.
         */
        void DrawRect(int x, int y, int width, int height);


        void DrawLine(int x1, int y1, int x2, int y2);


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a string.
         * @param str the string to draw
         * @param X  the X coordinate where the image is drawn.
         * @param Y  the Y coordinate where the image is drawn.
         */
        void DrawString(string str, int x, int y);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw rgb image.
         * @param rgbData the rgb image data array.
         * @param offset the start index of the image.
         * @param scanlength the scanning Width of the image.
         * @param X the X coordinate
         * @param Y the Y coordniate
         * @param w the widht of the image.
         * @param h the Height of the image.
         * @param processAlpha whether or not process the alpha channel.
         */
        void DrawRGB(int[] rgbData, int offset, int scanlength,
                int x, int y, int w, int h, bool processAlpha);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the font when draw chars.
         * @param font the font object.
         */
        void SetFont(IFont font);
    }

}
