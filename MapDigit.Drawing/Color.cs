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
using System;

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
     * The <code>Color</code> class is used to encapsulate colors in the default
     * sRGB color space  Every color has an implicit alpha Value of 1.0 or
     * an explicit one provided in the constructor.  The alpha Value
     * defines the transparency of a color and can be represented by
     * a int Value in the range 0&nbsp;-&nbsp;255.
     * An alpha Value of  255 means that the color is completely
     * opaque and an alpha Value of 0 means that the color is
     * completely transparent.
     * <p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public class Color
    {

        //[------------------------------ CONSTANTS -------------------------------]
        /**
         * Represents image data that is guaranteed to be completely opaque,
         * meaning that all pixels have an alpha Value of 255.
         */
        public const int OPAQUE = 1;

        /**
         * Represents image data that is guaranteed to be either completely
         * opaque, with an alpha Value of 255, or completely transparent,
         * with an alpha Value of 0.
         */
        public const int BITMASK = 2;

        /**
         * Represents image data that contains or might contain arbitrary
         * alpha values between and including 0 and 255.
         */
        public const int TRANSLUCENT = 3;


        /**
         * The color white.
         */
        public readonly static Color White = new Color(255, 255, 255);


        /**
         * The color light gray.
         */
        public readonly static Color LightGray = new Color(192, 192, 192);


        /**
         * The color gray.
         */
        public readonly static Color Gray = new Color(128, 128, 128);


        /**
         * The color dark gray.
         */
        public readonly static Color DarkGray = new Color(64, 64, 64);


        /**
         * The color black.
         */
        public readonly static Color Black = new Color(0, 0, 0);


        /**
         * The color red.
         */
        public readonly static Color Red = new Color(255, 0, 0);



        /**
         * The color pink.
         */
        public readonly static Color Pink = new Color(255, 175, 175);


        /**
         * The color orange.
         */
        public readonly static Color Orange = new Color(255, 200, 0);


        /**
         * The color yellow.
         */
        public readonly static Color Yellow = new Color(255, 255, 0);


        /**
         * The color green.
         */
        public readonly static Color Green = new Color(0, 255, 0);


        /**
         * The color magenta.
         */
        public readonly static Color Magenta = new Color(255, 0, 255);


        /**
         * The color cyan.
         */
        public readonly static Color Cyan = new Color(0, 255, 255);

 
        /**
         * The color blue.
         */
        public readonly static Color Blue = new Color(0, 0, 255);



        /**
         * The color Value.
         */
        internal int _value;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Checks the color integer components supplied for validity.
         * Throws an {@link IllegalArgumentException} if the Value is out of
         * range.
         * @param r the Red component
         * @param g the Green component
         * @param b the Blue component
         **/
        private static void TestColorValueRange(int r, int g, int b, int a)
        {
            bool rangeError = false;
            string badComponentString = "";

            if (a < 0 || a > 255)
            {
                rangeError = true;
                badComponentString = badComponentString + " Alpha";
            }
            if (r < 0 || r > 255)
            {
                rangeError = true;
                badComponentString = badComponentString + " Red";
            }
            if (g < 0 || g > 255)
            {
                rangeError = true;
                badComponentString = badComponentString + " Green";
            }
            if (b < 0 || b > 255)
            {
                rangeError = true;
                badComponentString = badComponentString + " Blue";
            }
            if (rangeError)
            {
                throw new ArgumentException("Color parameter outside of expected range:"
                                   + badComponentString);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an opaque sRGB color with the specified red, green,
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
        public Color(int r, int g, int b)
            : this(r, g, b, 255)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an sRGB color with the specified red, green, blue, and alpha
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
        public Color(int r, int g, int b, int a)
        {
            _value = ((a & 0xFF) << 24) |
                    ((r & 0xFF) << 16) |
                    ((g & 0xFF) << 8) |
                    ((b & 0xFF) << 0);
            TestColorValueRange(r, g, b, a);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
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
        public Color(int rgb)
        {
            _value = (int)(0xff000000 | rgb);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an sRGB color with the specified combined RGBA Value consisting
         * of the alpha component in bits 24-31, the red component in bits 16-23,
         * the green component in bits 8-15, and the blue component in bits 0-7.
         * If the <code>hasalpha</code> argument is <code>false</code>, alpha
         * is defaulted to 255.
         *
         * @param rgba the combined RGBA components
         * @param hasalpha <code>true</code> if the alpha bits are valid;
         *        <code>false</code> otherwise
         */
        public Color(int rgba, bool hasalpha)
        {
            if (hasalpha)
            {
                _value = rgba;
            }
            else
            {
                _value = (int)(0xff000000 | rgba);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the red component in the range 0-255 in the default sRGB
         * space.
         * @return the red component.
         */
        public int GetRed()
        {
            return (GetRGB() >> 16) & 0xFF;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the green component in the range 0-255 in the default sRGB
         * space.
         * @return the green component.
         */
        public int GetGreen()
        {
            return (GetRGB() >> 8) & 0xFF;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the blue component in the range 0-255 in the default sRGB
         * space.
         * @return the blue component.
         */
        public int GetBlue()
        {
            return (GetRGB() >> 0) & 0xFF;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the alpha component in the range 0-255.
         * @return the alpha component.
         */
        public int GetAlpha()
        {
            return (GetRGB() >> 24) & 0xff;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the RGB Value representing the color in the default sRGB
         * (Bits 24-31 are alpha, 16-23 are red, 8-15 are green, 0-7 are
         * blue).
         * @return the RGB Value of the color in the default sRGB
         */
        public int GetRGB()
        {
            return _value;
        }

        private const double FACTOR = 0.7;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a new <code>Color</code> that is a brighter version of this
         * <code>Color</code>.
         * <p>
         * This method applies an arbitrary scale factor to each of the three RGB
         * components of this <code>Color</code> to create a brighter version
         * of this <code>Color</code>. Although <code>brighter</code> and
         * <code>darker</code> are inverse operations, the results of a
         * series of invocations of these two methods might be inconsistent
         * because of rounding errors.
         * @return     a new <code>Color</code> object that is
         *                 a brighter version of this <code>Color</code>.
         */
        public Color Brighter()
        {
            int r = GetRed();
            int g = GetGreen();
            int b = GetBlue();

            /* From 2D group:
             * 1. black.brighter() should return grey
             * 2. applying brighter to blue will always return blue, brighter
             * 3. non pure color (non zero rgb) will eventually return white
             */
            int i = (int)(1.0 / (1.0 - FACTOR));
            if (r == 0 && g == 0 && b == 0)
            {
                return new Color(i, i, i);
            }
            if (r > 0 && r < i) r = i;
            if (g > 0 && g < i) g = i;
            if (b > 0 && b < i) b = i;

            return new Color(Math.Min((int)(r / FACTOR), 255),
                             Math.Min((int)(g / FACTOR), 255),
                             Math.Min((int)(b / FACTOR), 255));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a new <code>Color</code> that is a darker version of this
         * <code>Color</code>.
         * <p>
         * This method applies an arbitrary scale factor to each of the three RGB
         * components of this <code>Color</code> to create a darker version of
         * this <code>Color</code>.  Although <code>brighter</code> and
         * <code>darker</code> are inverse operations, the results of a series
         * of invocations of these two methods might be inconsistent because
         * of rounding errors.
         * @return  a new <code>Color</code> object that is
         *                    a darker version of this <code>Color</code>.
         */
        public Color Darker()
        {
            return new Color(Math.Max((int)(GetRed() * FACTOR), 0),
                     Math.Max((int)(GetGreen() * FACTOR), 0),
                     Math.Max((int)(GetBlue() * FACTOR), 0));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the hash code for this <code>Color</code>.
         * @return     a hash code Value for this object.
         */
        public int HashCode()
        {
            return _value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether another object is equal to this
         * <code>Color</code>.
         * <p>
         * The result is <code>true</code> if and only if the argument is not
         * <code>null</code> and is a <code>Color</code> object that has the same
         * red, green, blue, and alpha values as this object.
         * @param       obj   the object to test for equality with this
         *				<code>Color</code>
         * @return      <code>true</code> if the objects are the same;
         *                             <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            return obj is Color && ((Color)obj)._value == _value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of this <code>Color</code>. This
         * method is intended to be used only for debugging purposes.  The
         * content and format of the returned string might vary between
         * implementations. The returned string might be empty but cannot
         * be <code>null</code>.
         *
         * @return  a string representation of this <code>Color</code>.
         */
        public override string ToString()
        {
            return "[r=" + GetRed() + ",g=" + GetGreen() + ",b=" + GetBlue() + "]";
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
        public int GetTransparency()
        {
            int alpha = GetAlpha();
            if (alpha == 0xff)
            {
                return OPAQUE;
            }
            if (alpha == 0)
            {
                return BITMASK;
            }
            return TRANSLUCENT;
        }

    }

}
