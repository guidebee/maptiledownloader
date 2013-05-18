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
     * Fixed point float math.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class MathFP
    {

        /**
         * PI.
         */
        public const int PI = 205887;

        /**
         * E
         */
        public const int E = 178145;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the minimun of the two values.
         * @param x
         * @param y
         * @return
         */
        public static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the maximum of the two values.
         * @param x
         * @param y
         * @return
         */
        public static int Max(int x, int y)
        {
            return x > y ? x : y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the abs of the Value.
         * @param x
         * @return
         */
        public static int Abs(int x)
        {
            return x < 0 ? -x : x;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the product of the two values.
         * @param x
         * @param y
         * @return
         */
        public static int Mul(int x, int y)
        {
            var res = x * (long)y >> SingleFP.DECIMAL_BITS;
            return (int)res;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the division of the two values.
         * @param x
         * @param y
         * @return
         */
        public static int Div(int x, int y)
        {
            var res = ((long)x << SingleFP.DECIMAL_BITS) / y;
            return (int)res;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the square root of the given Value.
         * @param n
         * @return
         */
        public static int Sqrt(int n)
        {
            int s;
            if (n < (1000 << SingleFP.DECIMAL_BITS))
            {
                s = n / 20;
            }
            else if (n < (2500 << SingleFP.DECIMAL_BITS))
            {
                s = n / 40;
            }
            else if (n < (5000 << SingleFP.DECIMAL_BITS))
            {
                s = n / 60;
            }
            else if (n < (10000 << SingleFP.DECIMAL_BITS))
            {
                s = n / 86;
            }
            else if (n < (25000 << SingleFP.DECIMAL_BITS))
            {
                s = n / 132;
            }
            else
            {
                s = n / 168;
            }

            s = (s + Div(n, s)) >> 1;
            s = (s + Div(n, s)) >> 1;
            s = (s + Div(n, s)) >> 1;
            return s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculat the IEEE Reminder.
         * @param n
         * @param m
         * @return
         */
        public static int IEEERemainder(int n, int m)
        {
            return n - Mul(Floor(Div(n, m)), m);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculate the floor of the Value.
         * @param x
         * @return
         */
        public static int Floor(int x)
        {
            return x < 0 && (-x & 0xFFFF) != 0 ?
                -((-x + SingleFP.ONE >> SingleFP.DECIMAL_BITS) << SingleFP.DECIMAL_BITS)
                : ((x >> SingleFP.DECIMAL_BITS) << SingleFP.DECIMAL_BITS);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Calculate the round the Value.
         * @param x
         * @return
         */
        public static int Round(int x)
        {
            if (x < 0)
            {
                return -(((-x + SingleFP.ONE / 2) >> SingleFP.DECIMAL_BITS)
                        << SingleFP.DECIMAL_BITS);
            }
            return ((x + SingleFP.ONE / 2) >> SingleFP.DECIMAL_BITS)
                   << SingleFP.DECIMAL_BITS;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to degree.
         * @param f
         * @return
         */
        public static int ToDegrees(int f)
        {
            return Div(f * 180, PI);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to radians.
         * @param f
         * @return
         */
        public static int ToRadians(int f)
        {
            return Mul(f, PI) / 180;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculate of the sine.
         * @param f
         * @return
         */
        public static int Sin(int f)
        {
            if (f < 0 || f >= PI * 2)
            {
                f = IEEERemainder(f, PI * 2);
            }
            var sign = 1;
            if ((f > PI / 2) && (f <= PI))
            {
                f = PI - f;
            }
            else if ((f > PI) && (f <= (PI + PI / 2)))
            {
                f = f - PI;
                sign = -1;
            }
            else if (f > (PI + PI / 2))
            {
                f = (PI << 1) - f;
                sign = -1;
            }

            var sqr = Mul(f, f);
            var result = 498;
            result = Mul(result, sqr);
            result -= 10882;
            result = Mul(result, sqr);
            result += (1 << SingleFP.DECIMAL_BITS);
            result = Mul(result, f);
            return sign * result;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculate the cosine.
         * @param ff_ang
         * @return
         */
        public static int Cos(int ffAng)
        {
            return Sin(PI / 2 - ffAng);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the tan.
         * @param f
         * @return
         */
        public static int Tan(int f)
        {
            return Div(Sin(f), Cos(f));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the atan.
         * @param ff_val
         * @return
         */
        public static int Atan(int ffVal)
        {
            var ffVal1 = ffVal > SingleFP.ONE ? Div(SingleFP.ONE, ffVal)
                    : (ffVal < -SingleFP.ONE ? Div(SingleFP.ONE, -ffVal) : ffVal);
            var sqr = Mul(ffVal1, ffVal1);
            var result = 1365;
            result = Mul(result, sqr);
            result -= 5579;
            result = Mul(result, sqr);
            result += 11805;
            result = Mul(result, sqr);
            result -= 21646;
            result = Mul(result, sqr);
            result += 65527;
            result = Mul(result, ffVal1);
            return ffVal > SingleFP.ONE ? PI / 2 - result
                    : (ffVal < -SingleFP.ONE ? -(PI / 2 - result) : result);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the asin.
         * @param f
         * @return
         */
        public static int Asin(int f)
        {
            return PI / 2 - Acos(f);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the acosine.
         * @param f
         * @return
         */
        public static int Acos(int f)
        {
            var fRoot = Sqrt(SingleFP.ONE - f);
            var result = -1228;
            result = Mul(result, f);
            result += 4866;
            result = Mul(result, f);
            result -= 13901;
            result = Mul(result, f);
            result += 102939;
            result = Mul(fRoot, result);
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the minimum of the two values.
         * @param x
         * @param y
         * @return
         */
        public static long Min(long x, long y)
        {
            return x < y ? x : y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the maximum of the two values.
         * @param x
         * @param y
         * @return
         */
        public static long Max(long x, long y)
        {
            return x > y ? x : y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the abs of the Value.
         * @param x
         * @return
         */
        public static long Abs(long x)
        {
            return x < 0 ? -x : x;
        }
    }

}
