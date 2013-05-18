//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 13JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.DrawingFP
{

    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The <code>ColorFP</code> class is used to encapsulate colors in the default
     * sRGB color space  Every color has an implicit alpha Value of 1.0 or
     * an explicit one provided in the constructor.  The alpha Value
     * defines the transparency of a color and can be represented by
     * a int Value in the range 0&nbsp;-&nbsp;255.
     * An alpha Value of  255 means that the color is completely
     * opaque and an alpha Value of 0 means that the color is
     * completely transparent.
     * <p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class ColorFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an opaque sRGB color with the specified combined RGB Value
         * consisting of the red component in bits 16-23, the green component
         * in bits 8-15, and the blue component in bits 0-7.  The actual color
         * used in rendering depends on finding the best match given the
         * color space available for a particular output device.  Alpha is
         * defaulted to 255.
         *
         * @param rgb the combined RGB components
         */
        public ColorFP(int rgb)
        {
            Value = rgb;
            Red = (Value >> 16) & 0xFF;
            Green = (Value >> 8) & 0xFF;
            Blue = Value & 0xFF;
            Alpha = (Value >> 24) & 0xff;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a Color structure from the four 8-bit ARGB components
         * (alpha, red, green, and blue) values.
         * @param color A Value specifying the 32-bit ARGB Value.
         * @return The Color object that this method creates.
         */
        public static ColorFP FromArgb(int color)
        {
            return new ColorFP(color);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a Color structure from the four 8-bit ARGB components
         * (alpha, red, green, and blue) values.
         * @param color A Value specifying the 32-bit ARGB Value.
         * @return The Color object that this method creates.
         */
        public static ColorFP FromArgb(int red, int green, int blue)
        {
            var value =
                    ((red & 0xFF) << 16) |
                    ((green & 0xFF) << 8) |
                    ((blue & 0xFF) << 0);
            return new ColorFP(value);
        }


        /**
         * The color Value.
         */
        public int Value;

        /**
         * the red component.
         */
        public int Red;

        /**
         * the green compoent
         */
        public int Green;

        /**
         * the blue component.
         */
        public int Blue;

        /**
         * the alpha compoent.
         */
        public int Alpha;

    }

}
