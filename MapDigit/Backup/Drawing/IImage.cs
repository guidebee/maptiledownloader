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
     *  Image  interface.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public interface IImage
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Extracts a subimage from the given image allowing us to breakdown a
         * single large image into multiple smaller images in RAM, this actually
         * creates a standalone version of the image for use.
         * @param X the X coordinate of the result image.
         * @param Y the Y coordinate of the result image.
         * @param Width the Width coordinate of the result image.
         * @param Height the Height coordinate of the result image.
         * @param processAlpha has alpha channel.
         * @return an sub image of given image.
         */
        IImage SubImage(int x, int y, int width, int height, bool processAlpha);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the graphcis object associated with this image.
         * @return a graphics object which allow to draw on the image.
         */
        IGraphics GetGraphics();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the RGB array of this image.
         * @return the rgb array of the image.
         */
        int[] GetRGB();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a new image instance with the alpha channel of opaque/translucent
         * pixels within the image using the new alpha value. Transparent
         * (alpha == 0) pixels remain transparent. All other pixels will have the
         * new alpha value.
         * @param alpha  New value for the entire alpha channel
         * @param removeColor pixels matching this color are made transparent
         * (alpha channel ignored)
         * @return Translucent/Opaque image based on the alpha value and the pixels
         * of this image
         */
        IImage ModifyAlpha(byte alpha, int removeColor);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        /////////////////////////////////////////////////////////////////////////////
        /**
         * get the Height of the image.
         * @return the Height of the image.
         */
        int GetHeight();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the Width of the image.
         * @return the widht of the image.
         */
        int GetWidth();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the native image assocated with this Image object.
         * @return the native image object.
         */
        object GetNativeImage();
    }

}
