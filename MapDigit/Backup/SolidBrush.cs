//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 15JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using MapDigit.DrawingFP;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 15JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines a brush of a single color. Brushes are used to fill graphics shapes,
     * such as rectangles, ellipses, pies, polygons, and paths. This class cannot
     * be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class SolidBrush : Brush
    {
        readonly Color _brushColor;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an opaque sRGB brush with the specified red, green,
         * and blue values in the range (0 - 255).
         * The actual color used in rendering depends
         * on finding the best match given the color space
         * available for a given output device.
         * Alpha is defaulted to 255.
         *
         * @throws IllegalArgumentException if <code>r</code>, <code>g</code>
         *        or <code>b</code> are outside of the range
         *        0 to 255, inclusive
         * @param r the red component
         * @param g the green component
         * @param b the blue component
         */
        public SolidBrush(int r, int g, int b)
        {
            _brushColor = new Color(r, g, b);
            _wrappedBrushFP = new SolidBrushFP(_brushColor._value);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an sRGB brush with the specified red, green, blue, and alpha
         * values in the range (0 - 255).
         *
         * @throws IllegalArgumentException if <code>r</code>, <code>g</code>,
         *        <code>b</code> or <code>a</code> are outside of the range
         *        0 to 255, inclusive
         * @param r the red component
         * @param g the green component
         * @param b the blue component
         * @param a the alpha component
         */
        public SolidBrush(int r, int g, int b, int a)
        {
            _brushColor = new Color(r, g, b, a);
            _wrappedBrushFP = new SolidBrushFP(_brushColor._value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an opaque sRGB brush with the specified combined RGB Value
         * consisting of the red component in bits 16-23, the green component
         * in bits 8-15, and the blue component in bits 0-7.  The actual color
         * used in rendering depends on finding the best match given the
         * color space available for a particular output device.  Alpha is
         * defaulted to 255.
         *
         * @param rgb the combined RGB components
         */
        public SolidBrush(int rgb)
        {
            _brushColor = new Color(rgb);
            _wrappedBrushFP = new SolidBrushFP(_brushColor._value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an sRGB brush with the specified combined RGBA Value consisting
         * of the alpha component in bits 24-31, the red component in bits 16-23,
         * the green component in bits 8-15, and the blue component in bits 0-7.
         * If the <code>hasalpha</code> argument is <code>false</code>, alpha
         * is defaulted to 255.
         *
         * @param rgba the combined RGBA components
         * @param hasalpha <code>true</code> if the alpha bits are valid;
         *        <code>false</code> otherwise
         */
        public SolidBrush(int rgba, bool hasalpha)
        {
            _brushColor = new Color(rgba, hasalpha);
            _wrappedBrushFP = new SolidBrushFP(_brushColor._value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an sRGB brush with the specified combined RGBA Value consisting
         * of the alpha component in bits 24-31, the red component in bits 16-23,
         * the green component in bits 8-15, and the blue component in bits 0-7.
         * If the <code>hasalpha</code> argument is <code>false</code>, alpha
         * is defaulted to 255.
         *
         * @param color the color of the brush
         */
        public SolidBrush(Color color)
        {
            _brushColor = color;
            _wrappedBrushFP = new SolidBrushFP(_brushColor._value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the color of the solid brush.
         * @return the color of the brush.
         */
        public Color GetColor()
        {
            return _brushColor;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the transparency mode for this <code>Color</code>.  This is
         * required to implement the <code>Paint</code> interface.
         * @return this <code>Color</code> object's transparency mode.
         */
        public override int GetTransparency()
        {
            return _brushColor.GetTransparency();
        }


    }

}
