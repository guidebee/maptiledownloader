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
using System;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing.Geometry
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * J2ME Fixed-Point Math Library. default all number shall be limited in range
     * between -8388608.999999 to 8388608.999999 (precision bit 20).
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    abstract class MathFP
    {

        /**
         * Default precision lenght.(1/ 2^21).
         */
        public const int DEFAULT_PRECISION = 20;
        /**
         * Constant for ONE, HALF etc for the fixed-point math.
         */
        public static long ONE, Half, Two, E, PI, PIHalf, PITwo;
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @return the precision
         */
        public static int GetPrecision()
        {
            return (int)Precision;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the precision for all fixed-point operations.
         * The maximum precision is 31 bits.
         *
         * @param precision the desired precision in number of bits
         */
        public static void SetPrecision(int precision)
        {
            if (precision > MAX_PRECISION || precision < 0)
            {
                return;
            }
            int i;
            Precision = precision;
            ONE = 1 << precision;
            Half = ONE >> 1;
            Two = ONE << 1;
            PI = (precision <= PI_PRECISION) ?
                PI_VALUE >> (PI_PRECISION - precision)
                : PI_VALUE << (precision - PI_PRECISION);
            PIHalf = PI >> 1;
            PITwo = PI << 1;
            E = (precision <= E_PRECISION) ?
                E_VALUE >> (E_PRECISION - precision)
                : E_VALUE >> (precision - E_PRECISION);
            for (i = 0; i < SK_VALUE.Length; i++)
            {
                SK[i] = (precision <= SK_PRECISION) ?
                    SK_VALUE[i] >> (SK_PRECISION - precision)
                        : SK_VALUE[i] << (precision - SK_PRECISION);
            }
            for (i = 0; i < AS_VALUE.Length; i++)
            {
                AS[i] = (precision <= AS_PRECISION) ?
                    AS_VALUE[i] >> (AS_PRECISION - precision)
                    : AS_VALUE[i] << (precision - AS_PRECISION);
            }
            Ln2 = (precision <= LN2_PRECISION) ?
                LN2_VALUE >> (LN2_PRECISION - precision)
                    : LN2_VALUE << (precision - LN2_PRECISION);
            Ln2Inv = (precision <= LN2_PRECISION) ?
                LN2_INV_VALUE >> (LN2_PRECISION - precision) :
                LN2_INV_VALUE << (precision - LN2_PRECISION);
            for (i = 0; i < LG_VALUE.Length; i++)
            {
                LG[i] = (precision <= LG_PRECISION) ?
                    LG_VALUE[i] >> (LG_PRECISION - precision)
                    : LG_VALUE[i] << (precision - LG_PRECISION);
            }
            for (i = 0; i < EXP_P_VALUE.Length; i++)
            {
                EXP_P[i] = (precision <= EXP_P_PRECISION)
                        ? EXP_P_VALUE[i] >> (EXP_P_PRECISION - precision)
                        : EXP_P_VALUE[i] << (precision - EXP_P_PRECISION);
            }
            FracMask = ONE - 1;
            PIOverOneEighty = Div(PI, ToFP(180));
            ONEEightyOverPi = Div(ToFP(180), PI);

            MaxDigitsMul = 1;
            MaxDigitsCount = 0;
            for (long j = ONE; j != 0; )
            {
                j /= 10;
                MaxDigitsMul *= 10;
                MaxDigitsCount++;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts a fixed-point Value to the current set precision.
         *
         * @param fp the fixed-point Value to Convert.
         * @param precision the precision of the fixed-point Value passed in.
         * @return a fixed-point Value of the current precision
         */
        public static long Convert(long fp, int precision)
        {
            long num, xabs = Math.Abs(fp);
            if (precision > MAX_PRECISION || precision < 0)
            {
                return fp;
            }
            if (precision > Precision)
            {
                num = xabs >> (int)(precision - Precision);
            }
            else
            {
                num = xabs << (int)(Precision - precision);
            }
            if (fp < 0)
            {
                num = -num;
            }
            return num;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts an long to a fixed-point long.
         *
         * @param i long to Convert.
         * @return the converted fixed-point Value.
         */
        public static long ToFP(int i)
        {
            return (i < 0) ? -(-i << (int)Precision) : i << (int)Precision;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts a string to a fixed-point Value. <br>
         * The string should trimmed of any whitespace before-hand. <br>
         * A few examples of valid strings:<br>
         *
         * <pre>
         * .01
         * 0.01
         * 10
         * 130.0
         * -30000.12345
         * </pre>
         *
         * @param s the string to Convert.
         * @return the fixed-point Value.
         */
        public static long ToFP(string s)
        {
            long fp, i, integer, frac = 0;
            string fracString = null;
            bool neg = false;
            if (s[0] == '-')
            {
                neg = true;
                s = s.Substring(1);
            }
            int index = s.IndexOf('.');

            if (index < 0)
            {
                integer = int.Parse(s);
            }
            else if (index == 0)
            {
                integer = 0;
                fracString = s.Substring(1);
            }
            else if (index == s.Length - 1)
            {
                integer = int.Parse(s.Substring(0, index));
            }
            else
            {
                integer = int.Parse(s.Substring(0, index));
                fracString = s.Substring(index + 1);
            }

            if (fracString != null)
            {
                if (fracString.Length > MaxDigitsCount)
                {
                    fracString = fracString.Substring(0, (int)MaxDigitsCount);
                }
                if (fracString.Length > 0)
                {
                    frac = int.Parse(fracString);
                    for (i = MaxDigitsCount - fracString.Length; i > 0; --i)
                    {
                        frac *= 10;
                    }
                }
            }
            fp = (integer << (int)Precision) + (frac << (int)(Precision + Half)) / MaxDigitsMul;
            if (neg)
            {
                fp = -fp;
            }
            return fp;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts a fixed-point Value to an long.
         *
         * @param fp fixed-point Value to Convert
         * @return the converted long Value.
         */
        public static int ToInt(long fp)
        {
            return (int)((fp < 0) ? -(-fp >> (int)Precision) : fp >> (int)Precision);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts a fixed-point Value to a string.
         *
         * Same as <code>ToString(x, 0, max_possible_digits)</code>
         *
         * @param fp the fixed-point Value to Convert.
         * @return a string representing the fixed-point Value with a minimum of
         *         decimals in the string.
         */
        public static string ToString(long fp)
        {
            bool neg = false;
            if (fp < 0)
            {
                neg = true;
                fp = -fp;
            }
            long integer = fp >> (int)Precision;
            long fp1 = (fp & FracMask) * MaxDigitsMul;
            long fp2 = fp1 >> (int)Precision;
            string fracString = fp2.ToString();

            long len = MaxDigitsCount - fracString.Length;
            for (long i = len; i > 0; --i)
            {
                fracString = "0" + fracString;
            }
            if ((neg && integer != 0))
            {
                integer = -integer;
            }
            return integer + "." + fracString;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smallest (closest to negative infinity) fixed-point Value
         * that is greater than or equal to the argument and is equal to a
         * mathematical integer.
         *
         * @param fp a fixed-point Value.
         * @return the smallest (closest to negative infinity) fixed-point Value
         *         that is greater than or equal to the argument and is equal to a
         *         mathematical integer.
         */
        public static long Ceil(long fp)
        {
            bool neg = false;
            if (fp < 0)
            {
                fp = -fp;
                neg = true;
            }
            if ((fp & FracMask) == 0)
            {
                return (neg) ? -fp : fp;
            }
            if (neg)
            {
                return -(fp & ~FracMask);
            }
            return (fp & ~FracMask) + ONE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the largest (closest to positive infinity) fixed-point Value
         * Value that is less than or equal to the argument and is equal to a
         * mathematical integer.
         *
         * @param fp a fixed-point Value.
         * @return the largest (closest to positive infinity) fixed-point Value that
         *         less than or equal to the argument and is equal to a mathematical
         *         integer.
         */
        public static long Floor(long fp)
        {
            bool neg = false;
            if (fp < 0)
            {
                fp = -fp;
                neg = true;
            }
            if ((fp & FracMask) == 0)
            {
                return (neg) ? -fp : fp;
            }
            if (neg)
            {
                return -(fp & ~FracMask) - ONE;
            }
            return (fp & ~FracMask);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Removes the fractional part of a fixed-point Value.
         *
         * @param fp the fixed-point Value to truncate.
         * @return a truncated fixed-point Value.
         */
        public static long Trunc(long fp)
        {
            return (fp < 0) ? -(-fp & ~FracMask) : fp & ~FracMask;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the fractional part of a fixed-point Value.
         *
         * @param fp a fixed-point Value to get fractional part of.
         * @return positive fractional fixed-point Value if input is positive,
         *         negative fractional otherwise.
         */
        public static long Frac(long fp)
        {
            return (fp < 0) ? -(-fp & FracMask) : fp & FracMask;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts a fixed-point integer to an int with only the decimal Value.
         * <p>
         * For example, if <code>fp</code> represents <code>12.34</code> the
         * method returns <code>34</code>
         * </p>
         *
         * @param fp the fixed-point integer to be converted
         * @return a int in a normal integer representation
         */
        public static int FracAsInt(long fp)
        {
            if (fp < 0)
            {
                fp = -fp;
            }
            return (int)((MaxDigitsMul * (fp & FracMask)) >> (int)Precision);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the closest integer to the argument.
         *
         * @param fp the fixed-point Value to Round
         * @return the Value of the argument rounded to the nearest integer Value.
         */
        public static long Round(long fp)
        {
            bool neg = false;
            if (fp < 0)
            {
                fp = -fp;
                neg = true;
            }
            fp += Half;
            fp &= ~FracMask;
            return (neg) ? -fp : fp;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smaller of two values.
         *
         * @param fp1 the fixed-point Value.
         * @param fp2 the fixed-point Value.
         * @return the smaller of fp1 and fp2.
         */
        public static long Min(long fp1, long fp2)
        {
            return fp2 >= fp1 ? fp1 : fp2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the greater of two values.
         *
         * @param fp1 the fixed-point Value.
         * @param fp2 the fixed-point Value.
         * @return the greater of fp1 and fp2.
         */
        public static long Max(long fp1, long fp2)
        {
            return fp1 >= fp2 ? fp1 : fp2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the absolute Value of a fix float Value.
         *
         * @param fp the fixed-point Value.
         * @return the absolute Value of the argument.
         */
        public static long Abs(long fp)
        {
            if (fp < 0)
            {
                return -fp;
            }
            return fp;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * add two fixed-point values.
         *
         * @param fp1 first fixed-point Value.
         * @param fp2 second fixed-point Value.
         * @return the result of the addition.
         */
        public static long Add(long fp1, long fp2)
        {
            return fp1 + fp2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * substract two fixed-point values.
         *
         * @param fp1 first fixed-point Value.
         * @param fp2 second fixed-point Value.
         * @return the result of the substraction.
         */
        public static long Sub(long fp1, long fp2)
        {
            return fp1 - fp2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the remainder operation on two arguments .
         *
         * @param fp1 first fixed-point Value.
         * @param fp2 second fixed-point Value.
         * @return the remainder when fp1 is divided by fp2
         */
        public static long IEEERemainder(long fp1, long fp2)
        {
            return fp1 - Mul(Floor(Div(fp1, fp2)), fp2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Multiplies two fixed-point values.
         *
         * @param fp1 first fixed-point Value.
         * @param fp2 second fixed-point Value.
         * @return the result of the multiplication.
         */
        public static long Mul(long fp1, long fp2)
        {
            long fp = fp1 * fp2;
            return (fp >> (int)Precision);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Divides two fixed-point values.
         *
         * @param fp1 mumerator fixed-point Value.
         * @param fp2 denominator fixed-point Value.
         * @return the result of the division.
         */
        public static long Div(long fp1, long fp2)
        {
            if (fp1 == 0)
            {
                return 0;
            }
            if (fp2 == 0)
            {
                return (fp1 < 0) ? -INFINITY : INFINITY;
            }
            long xneg = 0, yneg = 0;
            if (fp1 < 0)
            {
                xneg = 1;
                fp1 = -fp1;
            }
            if (fp2 < 0)
            {
                yneg = 1;
                fp2 = -fp2;
            }
            long msb = 0, lsb = 0;
            while ((fp1 & (1 << (int)(MAX_PRECISION - msb))) == 0)
            {
                msb++;
            }
            while ((fp2 & (1 << (int)lsb)) == 0)
            {
                lsb++;
            }
            long shifty = Precision - (msb + lsb);
            long res = ((fp1 << (int)msb) / (fp2 >> (int)lsb));
            if (shifty > 0)
            {
                res <<= (int)shifty;
            }
            else
            {
                res >>= (int)-shifty;
            }
            if ((xneg ^ yneg) == 1)
            {
                res = -res;
            }
            return res;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the correctly rounded positive square root of a fixed-point
         * Value.
         *
         * @param fp a fixed-point Value.
         * @return the positive square root of <code>fp</code>. If the argument
         *         is NaN or less than zero, the result is NaN.
         */
        public static long Sqrt(long fp)
        {
            long s = (fp + ONE) >> 1;
            for (int i = 0; i < 8; i++)
            {
                s = (s + Div(fp, s)) >> 1;
            }
            return s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the trigonometric sine of an angle.
         *
         * @param fp the angle in radians
         * @return the sine of the argument.
         */
        public static long Sin(long fp)
        {
            long sign = 1;
            fp %= PI * 2;
            if (fp < 0)
            {
                fp = PI * 2 + fp;
            }
            if ((fp > PIHalf) && (fp <= PI))
            {
                fp = PI - fp;
            }
            else if ((fp > PI) && (fp <= (PI + PIHalf)))
            {
                fp = fp - PI;
                sign = -1;
            }
            else if (fp > (PI + PIHalf))
            {
                fp = (PI << 1) - fp;
                sign = -1;
            }

            long sqr = Mul(fp, fp);
            long result = SK[0];
            result = Mul(result, sqr);
            result -= SK[1];
            result = Mul(result, sqr);
            result += ONE;
            result = Mul(result, fp);
            return sign * result;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the trigonometric cosine of an angle.
         *
         * @param fp the angle in radians
         * @return the cosine of the argument.
         */
        public static long Cos(long fp)
        {
            return Sin(PIHalf - fp);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the trigonometric tangent of an angle.
         *
         * @param fp the angle in radians
         * @return the tangent of the argument.
         */
        public static long Tan(long fp)
        {
            return Div(Sin(fp), Cos(fp));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the arc sine of a Value; the returned angle is in the range
         * -<i>pi</i>/2 through <i>pi</i>/2.
         *
         * @param fp the fixed-point Value whose arc sine is to be returned.
         * @return the arc sine of the argument.
         */
        public static long Asin(long fp)
        {
            bool neg = false;
            if (fp < 0)
            {
                neg = true;
                fp = -fp;
            }

            long fRoot = Sqrt(ONE - fp);
            long result = AS[0];

            result = Mul(result, fp);
            result += AS[1];
            result = Mul(result, fp);
            result -= AS[2];
            result = Mul(result, fp);
            result += AS[3];
            result = PIHalf - (Mul(fRoot, result));
            if (neg)
            {
                result = -result;
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the arc cosine of a Value; the returned angle is in the range 0.0
         * through <i>pi</i>.
         *
         * @param fp the fixed-point Value whose arc cosine is to be returned.
         * @return the arc cosine of the argument.
         */
        public static long Acos(long fp)
        {
            return PIHalf - Asin(fp);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the arc tangent of a Value; the returned angle is in the range
         * -<i>pi</i>/2 through <i>pi</i>/2.
         *
         * @param fp the fiexed-point Value whose arc tangent is to be returned.
         * @return the arc tangent of the argument.
         */
        public static long Atan(long fp)
        {
            return Asin(Div(fp, Sqrt(ONE + Mul(fp, fp))));
        }    // This is a finely tuned error around 0. The inaccuracies stabilize at
        //around this Value.
        private const int ATAN2_ZERO_ERROR = 65;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the angle <i>theta</i> from the conversion of rectangular
         * coordinates (<code>fpX</code>,&nbsp;<code>fpY</code>) to polar
         * coordinates (r,&nbsp;<i>theta</i>).
         *
         * @param fpX the ordinate coordinate
         * @param fpY the abscissa coordinate
         * @return the <i>theta</i> component of the point
         * (<i>r</i>,&nbsp;<i>theta</i>)
         * in polar coordinates that corresponds to the point
         * (<i>fpX</i>,&nbsp;<i>fpY</i>) in Cartesian coordinates.
         */
        public static long Atan2(long fpX, long fpY)
        {
            if (fpX == 0)
            {
                if (fpY >= 0)
                {
                    return 0;
                }
                if (fpY < 0)
                {
                    return PI;
                }
            }
            else if (fpY >= -ATAN2_ZERO_ERROR && fpY <= ATAN2_ZERO_ERROR)
            {
                return (fpX > 0) ? PIHalf : -PIHalf;
            }
            long z = Atan(Math.Abs(Div(fpX, fpY)));
            if (fpY > 0)
            {
                return (fpX > 0) ? z : -z;
            }
            return (fpX > 0) ? PI - z : z - PI;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns Euler's number <i>e</i> raised to the power of a fixed-point
         * Value.
         *
         * @param fp the exponent to raise <i>e</i> to.
         * @return the Value <i>e</i><sup><code>fp</code></sup>, where <i>e</i>
         *         is the base of the natural logarithms.
         */
        public static long Exp(long fp)
        {
            if (fp == 0)
            {
                return ONE;
            }
            long xabs = Math.Abs(fp);
            long k = Mul(xabs, Ln2Inv);
            k += Half;
            k &= ~FracMask;
            if (fp < 0)
            {
                k = -k;
            }
            fp -= Mul(k, Ln2);
            long z = Mul(fp, fp);
            long l = Two + Mul(z, EXP_P[0] + Mul(z, EXP_P[1] + Mul(z, EXP_P[2]
                    + Mul(z, EXP_P[3] + Mul(z, EXP_P[4])))));
            long xp = ONE + Div(Mul(Two, fp), l - fp);
            if (k < 0)
            {
                k = ONE >> (int)(-k >> (int)Precision);
            }
            else
            {
                k = ONE << (int)(k >> (int)Precision);
            }
            return Mul(k, xp);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the natural logarithm (base e) of a fixed-point Value.
         *
         * @param x a fixed-point Value
         * @return the Value ln&nbsp;<code>a</code>, the natural logarithm of
         *         <code>fp</code>.
         */
        public static long Log(long x)
        {
            if (x < 0)
            {
                return 0;
            }
            if (x == 0)
            {
                return -INFINITY;
            }
            long log2 = 0, xi = x;
            while (xi >= Two)
            {
                xi >>= 1;
                log2++;
            }
            long f = xi - ONE;
            long s = Div(f, Two + f);
            long z = Mul(s, s);
            long w = Mul(z, z);
            long l = Mul(w, LG[1] + Mul(w, LG[3] + Mul(w, LG[5])))
                    + Mul(z, LG[0] + Mul(w, LG[2] + Mul(w, LG[4] + Mul(w, LG[6]))));
            return Mul(Ln2, (log2 << (int)Precision)) + f - Mul(s, f - l);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the logarithm (base <code>base</code>) of a fixed-point Value.
         *
         * @param fp a fixed-point Value
         * @param base
         * @return the Value Log2&nbsp;<code>a</code>, the logarithm of
         *         <code>fp</code>
         */
        public static long Log(long fp, long baseNumber)
        {
            return Div(Log(fp), Log(baseNumber));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Value of the first argument raised to the power of the second
         * argument
         *
         * @param fp1 the base
         * @param fp2 the exponent
         * @return the Value <code>a<sup>b</sup></code>.
         */
        public static long Pow(long fp1, long fp2)
        {
            if (fp2 == 0)
            {
                return ONE;
            }
            if (fp1 < 0)
            {
                return 0;
            }
            return Exp(Mul(Log(fp1), fp2));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts an angle measured in degrees to an approximately equivalent
         * angle measured in radians.
         *
         * @param fp a fixed-point angle in degrees
         * @return the measurement of the angle angrad in radians.
         */
        public static long ToRadians(long fp)
        {
            return Mul(fp, PIOverOneEighty);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Converts an angle measured in radians to an approximately equivalent
         * angle measured in degrees.
         *
         * @param fp a fixed-point angle in radians
         * @return the measurement of the angle angrad in degrees.
         */
        public static long ToDegrees(long fp)
        {
            return Mul(fp, ONEEightyOverPi);
        }
        private const int MAX_PRECISION = 30;
        /**
         * largest possible number
         */
        public const int INFINITY = 0x7fffffff;
        // 2.7182818284590452353602874713527 * 2^29
        private const int E_PRECISION = 29;
        private const int E_VALUE = 1459366444;
        // 3.1415926535897932384626433832795 * 2^29
        private const int PI_PRECISION = 29;
        private const int PI_VALUE = 1686629713;
        /**
         * number of fractional bits in all operations, do not modify directly
         */
        private static long Precision;
        private static long FracMask;
        private static long ONEEightyOverPi;
        private static long PIOverOneEighty;
        private static long MaxDigitsCount;
        private static long MaxDigitsMul;
        private const int SK_PRECISION = 31;

        private static readonly int[] SK_VALUE = new[]{
                                                              16342350, //7.61e-03 * 2^31
                                                              356589659, //1.6605e-01 * 2^31
                                                          };

        private static readonly int[] SK = new int[SK_VALUE.Length];
        private const int AS_PRECISION = 30;

        private static readonly int[] AS_VALUE = new[]{
                                                           -20110432, //-0.0187293 * 2^30
                                                           79737141, //0.0742610 * 2^30
                                                           227756102, //0.2121144 * 2^30
                                                           1686557206 //1.5707288 * 2^30
                                                       };

        private static readonly int[] AS = new int[AS_VALUE.Length];
        //0.69314718055994530941723212145818 * 2^30
        private const int LN2_PRECISION = 30;
        private const int LN2_VALUE = 744261117;
        //1.4426950408889634073599246810019 * 2^30
        private const int LN2_INV_VALUE = 1549082004;
        private static int Ln2, Ln2Inv;
        private const int LG_PRECISION = 31;

        private static readonly int[] LG_VALUE = new[]{
                                                              1431655765, //6.666666666666735130e-01 * 2^31
                                                              858993459, //3.999999999940941908e-01 * 2^31
                                                              613566760, //2.857142874366239149e-01 * 2^31
                                                              477218077, //2.222219843214978396e-01 * 2^31
                                                              390489238, //1.818357216161805012e-01 * 2^31
                                                              328862160, //1.531383769920937332e-01 * 2^31
                                                              317788895 //1.479819860511658591e-01 * 2^31
                                                          };

        private static readonly int[] LG = new int[LG_VALUE.Length];
        private const int EXP_P_PRECISION = 31;

        private static readonly int[] EXP_P_VALUE = new[]{
                                                                 357913941, //1.66666666666666019037e-01 * 2^31
                                                                 -5965232, //-2.77777777770155933842e-03 * 2^31
                                                                 142029, //6.61375632143793436117e-05 * 2^31
                                                                 -3550, //-1.65339022054652515390e-06 * 2^31
                                                                 88, //4.13813679705723846039e-08 * 2^31
                                                             };

        private static readonly int[] EXP_P = new int[EXP_P_VALUE.Length];
        // Init the default precision


        static MathFP()
        {
            SetPrecision(DEFAULT_PRECISION);
        }
    }

}
