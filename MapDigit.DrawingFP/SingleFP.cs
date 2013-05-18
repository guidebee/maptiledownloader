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
     * Single is a fix point single class.
     * <hr><b>&copy; Copyright 2008 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 08/11/08
     * @author      Guidebee, Inc.
     */
    public class SingleFP
    {

        /**
         * Positive Infinity.
         */
        private const int POSITIVE_INFINITY = int.MaxValue;

        /**
         * Negative infinity.
         */
        private const int NEGATIVE_INFINITY = int.MinValue;

        /**
         * Max Value.
         */
        public const int MAX_VALUE = POSITIVE_INFINITY - 1;

        /**
         * Min Value.
         */
        public const int MIN_VALUE = NEGATIVE_INFINITY + 2;
        

        /**
         * Not a number.
         */
        public const int NOT_A_NUMBER =   NEGATIVE_INFINITY + 1;

        /**
         * Fix point length.
         */
        public const int DECIMAL_BITS = 16;

        /**
         * the number 1 in this fix point float.
         */
        public const int ONE = 1 << DECIMAL_BITS;

        /**
         * int format for this single.
         */
        private readonly int _value;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Counstructor.
         * @param v the integer format for this fixed point number.
         */
        public SingleFP(int v)
        {
            _value = v;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * copy constructor.
         * @param f a fixed point float.
         */
        public SingleFP(SingleFP f)
        {
            _value = f._value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Test if it's NaN.
         * @param x check if the input is a NaN
         * @return true, if it's NaN.
         */
        public static bool IsNaN(int x)
        {
            return x == NOT_A_NUMBER;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Test if it's infinity.
         *
         * @param x the fixed point number to be tested.
         * @return true it's positive or negitive infinity.
         */
        public static bool IsInfinity(int x)
        {
            return x == NEGATIVE_INFINITY || x == POSITIVE_INFINITY;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Test if it's negative infinity.
         * @param x the fixed point number to be tested.
         * @return true it's  negitive infinity.
         */
        public static bool IsNegativeInfinity(int x)
        {
            return x == NEGATIVE_INFINITY;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Test if it's positive infinity.
         * @param x the fixed point number to be tested.
         * @return true it's  positive infinity.
         */
        public static bool IsPositiveInfinity(int x)
        {
            return x == POSITIVE_INFINITY;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * convert a float to this fixed point float.
         * @param f a float number
         * @return a fixed point number.
         */
        public static int FromFloat(float f)
        {
            return (int)(f * ONE + 0.5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * convert a double to this fixed point float.
         * @param f a double number
         * @return a fixed point number.
         */
        public static int FromDouble(double f)
        {
            return (int)(f * ONE + 0.5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert this fixed point float to a float.
         * @param x a fixed point number
         * @return a float.
         */
        public static float ToFloat(int x)
        {
            return (float)x / ONE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert this fixed point float to a double.
         * @param x a fixed point number
         * @return a double
         */
        public static double ToDouble(int x)
        {
            return (double)x / ONE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert an integer to the fixed point float.
         * @param x a integer
         * @return a fixed point number.
         */
        public static int FromInt(int x)
        {
            return x << DECIMAL_BITS;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert the fixed point float back to an integer.
         * @param ff_x  the fixed point number
         * @return an integer.
         */
        public static int ToInt(int ffX)
        {
            return ffX >> DECIMAL_BITS;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Parse an string can convert it to fixed point float.
         * @param strValue a string reprents a float.
         * @return the fixed point number.
         */
        public static SingleFP ParseSingle(string strValue)
        {
            var s = strValue;
            var eNeg = false;
            int v, e = 0;
            var posE = s.IndexOf('E');
            if (posE == -1)
            {
                posE = s.IndexOf('e');
            }
            if (posE != -1)
            {
                e = int.Parse(s.Substring(posE + 1));
                if (e < 0)
                {
                    eNeg = true;
                    e = -e;
                }
                s = s.Substring(0, (posE) - (0));
            }
            var posDot = s.IndexOf('.');
            if (posDot == -1)
            {
                v = int.Parse(s);
                v = v << DECIMAL_BITS;
            }
            else
            {
                v = int.Parse(s.Substring(0, (posDot) - (0))) << DECIMAL_BITS;
                s = s.Substring(posDot + 1);
                s = s + "0000";
                s = s.Substring(0, (4) - (0));
                var f = int.Parse(s);
                f = (f << DECIMAL_BITS) / 10000;
                if (v < 0)
                {
                    v -= f;
                }
                else
                {
                    v += f;
                }
            }
            for (int i = 0; i < e; i++)
            {
                if (eNeg)
                {
                    v /= 10;
                }
                else
                {
                    v *= 10;
                }
            }
            return new SingleFP(v);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to string format.
         * @return a string repents the fixed point number.
         */
        public override string ToString()
        {
            var s = "";
            var v = _value;
            if (v < 0)
            {
                s = "-";
                v = -v;
            }
            s = s + (v >> DECIMAL_BITS);
            v = 0xFFFF & v;
            if (v != 0)
            {
                s = s + ".";
            }
            //while (v != 0)
            for (int i = 0; i < 4; i++)
            {
                v = v * 10;
                s = s + (v >> DECIMAL_BITS);
                v = 0xFFFF & v;
            }
            return s;
        }
    }
}
