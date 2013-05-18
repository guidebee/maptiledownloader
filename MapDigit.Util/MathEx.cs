using System;

namespace MapDigit.Util
{
    public static class MathEx
    {
        //[------------------------------ CONSTANTS -------------------------------]
        /**
         * The double value limit used to termiate a calcuation.
         */
        private const double PRECISION = 1e-12;
        /**
         * The double value that is closer than any other to pi, the ratio of the
         * circumference of a circle to its diameter.
         */
        public const double PI = Math.PI;
        /**
         * The double value that is closer than any other to e, the base of the
         * natural logarithms.
         */
        public const double E = Math.E;
        //[------------------------------ CONSTRUCTOR -----------------------------]
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Privete constructor,avoid the class to be instantiated.
         */
        //[------------------------------ PUBLIC METHODS --------------------------]
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the absolute value of a <code>double</code> value.
         * If the argument is not negative, the argument is returned.
         * If the argument is negative, the negation of the argument is returned.
         * Special cases:
         * <ul><li>If the argument is positive zero or negative zero, the result
         * is positive zero.
         * <li>If the argument is infinite, the result is positive infinity.
         * <li>If the argument is NaN, the result is NaN.</ul>
         * In other words, the result is equal to the value of the expression:
         * <p><pre>Double.longBitsToDouble((Double.doubleToLongBits(a)<<1)>>>1)</pre>
         * @param a a double value.
         * @return the absolute value of the argument.
         */
        public static double Abs(double a)
        {
            return Math.Abs(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the absolute value of a <code>float</code> value.
         * If the argument is not negative, the argument is returned.
         * If the argument is negative, the negation of the argument is returned.
         * Special cases:
         * <ul><li>If the argument is positive zero or negative zero, the
         * result is positive zero.
         * <li>If the argument is infinite, the result is positive infinity.
         * <li>If the argument is NaN, the result is NaN.</ul>
         * In other words, the result is equal to the value of the expression:
         * <p><pre>Float.intBitsToFloat(0x7fffffff & Float.floatToIntBits(a))</pre>
         *<P><DD>
         * @param a a float value.
         * @return the absolute value of the argument.
         */
        public static float Abs(float a)
        {
            return Math.Abs(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the absolute value of an <code>int</code> value.
         * If the argument is not negative, the argument is returned.
         * If the argument is negative, the negation of the argument is returned.
         * <p> Note that if the argument is equal to the value of
         * <code>Integer.MIN_VALUE</code>, the most negative representable
         * <code>int</code> value, the result is that same value, which is
         * negative.<P><DD>
         * @param a a int value.
         * @return the absolute value of the argument.
         */
        public static int Abs(int a)
        {
            return Math.Abs(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the absolute value of a <code>long</code> value.
         * If the argument is not negative, the argument is returned.
         * If the argument is negative, the negation of the argument is returned.
         * <p> Note that if the argument is equal to the value of
         * <code>Long.MIN_VALUE</code>, the most negative representable
         * <code>long</code> value, the result is that same value, which is
         * negative.<P><DD>
         * @param a a long value.
         * @return the absolute value of the argument.
         */
        public static long Abs(long a)
        {
            return Math.Abs(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the smallest (closest to negative infinity)
         * <code>double</code> value that is not less than the argument and is
         * equal to a mathematical integer. Special cases:
         * <ul><li>If the argument value is already equal to a mathematical
         * integer, then the result is the same as the argument.
         * <li>If the argument is NaN or an infinity or positive zero or negative
         * zero, then the result is the same as the argument.
         * <li>If the argument value is less than zero but greater than -1.0,
         * then the result is negative zero.</ul>
         * Note that the value of <code>Math.Ceil(x)</code> is exactly the
         * value of <code>-Math.Floor(-x)</code>.<P><DD>
         * @param a a double value.
         * @return the smallest (closest to negative infinity) double value that is
         * not less than the argument and is equal to a mathematical integer.
         */
        public static double Ceil(double a)
        {
            return Math.Ceiling(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the trigonometric cosine of an angle. Special case:
         * <ul><li>If the argument is NaN or an infinity, then the
         * result is NaN.</ul><P><DD>
         * @param a an angle, in radians.
         * @return the cosine of the argument.
         */
        public static double Cos(double a)
        {
            return Math.Cos(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the largest (closest to positive infinity)
         * <code>double</code> value that is not greater than the argument and
         * is equal to a mathematical integer. Special cases:
         * <ul><li>If the argument value is already equal to a mathematical
         * integer, then the result is the same as the argument.
         * <li>If the argument is NaN or an infinity or positive zero or
         * negative zero, then the result is the same as the argument.</ul><P><DD>
         * @param a a double value.
         * @return the largest (closest to positive infinity) double value that is
         * not greater than the argument and is equal to a mathematical integer.
         */
        public static double Floor(double a)
        {
            return Math.Floor(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the greater of two <code>double</code> values.  That is, the
         * result is the argument closer to positive infinity. If the
         * arguments have the same value, the result is that same value. If
         * either value is <code>NaN</code>, then the result is <code>NaN</code>.
         * Unlike the the numerical comparison operators, this method considers
         * negative zero to be strictly smaller than positive zero. If one
         * argument is positive zero and the other negative zero, the result
         * is positive zero.<P><DD>
         * @param a a double value.
         * @param b a double value.
         * @return the larger of a and b.
         */
        public static double Max(double a, double b)
        {
            return Math.Max(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the greater of two <code>float</code> values.  That is, the
         * result is the argument closer to positive infinity. If the
         * arguments have the same value, the result is that same value. If
         * either value is <code>NaN</code>, then the result is <code>NaN</code>.
         * Unlike the the numerical comparison operators, this method considers
         * negative zero to be strictly smaller than positive zero. If one
         * argument is positive zero and the other negative zero, the result
         * is positive zero.<P><DD>
         * @param a a float value.
         * @param b a float value.
         * @return the larger of a and b.
         */
        public static float Max(float a, float b)
        {
            return Math.Max(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the greater of two <code>int</code> values. That is, the
         * result is the argument closer to the value of
         * <code>Integer.MAX_VALUE</code>. If the arguments have the same value,
         * the result is that same value.<P><DD>
         * @param a a int value.
         * @param b a int value.
         * @return the larger of a and b.
         */
        public static int Max(int a, int b)
        {
            return Math.Max(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the greater of two <code>long</code> values. That is, the
         * result is the argument closer to the value of
         * <code>Long.MAX_VALUE</code>. If the arguments have the same value,
         * the result is that same value.<P><DD>
         * @param a a long value.
         * @param b a long value.
         * @return the larger of a and b.
         */
        public static long Max(long a, long b)
        {
            return Math.Max(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the smaller of two <code>double</code> values.  That is, the
         * result is the value closer to negative infinity. If the arguments have
         * the same value, the result is that same value. If either value
         * is <code>NaN</code>, then the result is <code>NaN</code>.  Unlike the
         * the numerical comparison operators, this method considers negative zero
         * to be strictly smaller than positive zero. If one argument is
         * positive zero and the other is negative zero, the result is negative
         * zero.<P><DD>
         * @param a a double value.
         * @param b a double value.
         * @return the smaller of a and b.
         */
        public static double Min(double a, double b)
        {
            return Math.Min(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the smaller of two <code>double</code> values.  That is, the
         * result is the value closer to negative infinity. If the arguments have
         * the same value, the result is that same value. If either value
         * is <code>NaN</code>, then the result is <code>NaN</code>.  Unlike the
         * the numerical comparison operators, this method considers negative zero
         * to be strictly smaller than positive zero. If one argument is
         * positive zero and the other is negative zero, the result is negative
         * zero.<P><DD>
         * @param a a double value.
         * @param b a double value.
         * @return the smaller of a and b.
         */
        public static float Min(float a, float b)
        {
            return Math.Min(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the smaller of two <code>int</code> values. That is, the
         * result the argument closer to the value of <code>Integer.MIN_VALUE</code>.
         * If the arguments have the same value, the result is that same value.
         *<P><DD>
         * @param a a int value.
         * @param b a int value.
         * @return the smaller of a and b.
         */
        public static int Min(int a, int b)
        {
            return Math.Min(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the smaller of two <code>long</code> values. That is, the
         * result is the argument closer to the value of
         * <code>Long.MIN_VALUE</code>. If the arguments have the same value,
         * the result is that same value.<P><DD>
         * @param a a long value.
         * @param b a long value.
         * @return the smaller of a and b.
         */
        public static long Min(long a, long b)
        {
            return Math.Min(a, b);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the trigonometric sine of an angle.  Special cases:
         * <ul><li>If the argument is NaN or an infinity, then the
         * result is NaN.
         * <li>If the argument is positive zero, then the result is
         * positive zero; if the argument is negative zero, then the
         * result is negative zero.</ul><P><DD>
         * @param a an angle, in radians
         * @return the sine of the argument.
         */
        public static double Sin(double a)
        {
            return Math.Sin(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the correctly rounded positive square root of a
         * <code>double</code> value.
         * Special cases:
         * <ul><li>If the argument is NaN or less than zero, then the result
         * is NaN.
         * <li>If the argument is positive infinity, then the result is positive
         * infinity.
         * <li>If the argument is positive zero or negative zero, then the
         * result is the same as the argument.</ul><P><DD>
         * @param a a double value.
         * @return the positive square root of a. If the argument is NaN or less
         * than zero, the result is NaN.
         */
        public static double Sqrt(double a)
        {
            return Math.Sqrt(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns the trigonometric tangent of an angle.  Special cases:
         * <ul><li>If the argument is NaN or an infinity, then the result
         * is NaN.
         * <li>If the argument is positive zero, then the result is
         * positive zero; if the argument is negative zero, then the
         * result is negative zero</ul><P><DD>
         * @param a an angle, in radians.
         * @return the tangent of the argument.
         */
        public static double Tan(double a)
        {
            return Math.Tan(a);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Converts an angle measured in radians to the equivalent angle
         * measured in degrees.<P><DD
         * @param angrad  an angle, in radians.
         * @return the measurement of the angle angrad in degrees.
         */
        public static double ToDegrees(double angrad)
        {
            return angrad*180.0/Math.PI;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Converts an angle measured in degrees to the equivalent angle
         * measured in radians.<P><DD>
         * @param angdeg  an angle, in degrees.
         * @return the measurement of the angle angrad in radians.
         */
        public static double ToRadians(double angdeg)
        {
            return angdeg*Math.PI/180.0;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Returns the arc tangent of an angle, in the range of -<i>pi</i>/2
         * through <i>pi</i>/2.  Special cases:  <ul><li>If the argument is NaN,
         * then the result is NaN.
         * <li>If the argument is zero, then the result is a zero with the
         * same sign as the argument.</ul> <p>
         * A result must be within 1 ulp of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param a  the value whose arc tangent is to be returned.
         * @return the arc tangent of the argument.
         */
        public static double Atan(double a)
        {
            bool signChange = false;
            bool invert = false;
            if (a < 0.0)
            {
                a = -a;
                signChange = true;
            }
            // check up the invertation
            if (a > 1.0)
            {
                a = 1 / a;
                invert = true;
            }
            if (a == 0.0)
            {
                return 0;
            }
            if (a == 1.0)
            {
                return 0.7853981633974483;
            }
            double a0 = a;
            double n = 1;
            double a1 = (-1) * a * a * (2 * n - 1) / (2 * n + 1) * a0;
            double s = a0;
            while (Math.Abs(a1) > PRECISION)
            {
                s += a1;
                n += 1.0;
                a0 = a1;
                a1 = (-1) * a * a * (2 * n - 1) / (2 * n + 1) * a0;
            }
            // invertation took place
            if (invert)
            {
                s = Math.PI / 2 - s;
            }
            // sign change took place
            if (signChange)
            {
                s = -s;
            }
            return s;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Converts rectangular coordinates (<code>x</code>,&nbsp;<code>y</code>)
         * to polar (r,&nbsp;<i>theta</i>).
         * This method computes the phase <i>theta</i> by computing an arc tangent
         * of <code>y/x</code> in the range of -<i>pi</i> to <i>pi</i>. Special
         * cases:
         * <ul><li>If either argument is NaN, then the result is NaN.
         * <li>If the first argument is positive zero and the second argument
         * is positive, or the first argument is positive and finite and the
         * second argument is positive infinity, then the result is positive
         * zero.
         * <li>If the first argument is negative zero and the second argument
         * is positive, or the first argument is negative and finite and the
         * second argument is positive infinity, then the result is negative zero.
         * <li>If the first argument is positive zero and the second argument
         * is negative, or the first argument is positive and finite and the
         * second argument is negative infinity, then the result is the
         * <code>double</code> value closest to <i>pi</i>.
         * <li>If the first argument is negative zero and the second argument
         * is negative, or the first argument is negative and finite and the
         * second argument is negative infinity, then the result is the
         * <code>double</code> value closest to -<i>pi</i>.
         * <li>If the first argument is positive and the second argument is
         * positive zero or negative zero, or the first argument is positive
         * infinity and the second argument is finite, then the result is the
         * <code>double</code> value closest to <i>pi</i>/2.
         * <li>If the first argument is negative and the second argument is
         * positive zero or negative zero, or the first argument is negative
         * infinity and the second argument is finite, then the result is the
         * <code>double</code> value closest to -<i>pi</i>/2.
         * <li>If both arguments are positive infinity, then the result is the
         * <code>double</code> value closest to <i>pi</i>/4.
         * <li>If the first argument is positive infinity and the second argument
         * is negative infinity, then the result is the <code>double</code>
         * value closest to 3*<i>pi</i>/4.
         * <li>If the first argument is negative infinity and the second argument
         * is positive infinity, then the result is the <code>double</code> value
         * closest to -<i>pi</i>/4.
         * <li>If both arguments are negative infinity, then the result is the
         * <code>double</code> value closest to -3*<i>pi</i>/4.</ul><p>
         * A result must be within 2 ulps of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param x  the ordinate coordinate.
         * @param y  the abscissa  coordinate.
         * @return the theta component of the point (r, theta) in polar coordinates
         * that corresponds to the point (x, y) in Cartesian coordinates.
         */
        public static double Atan2(double y, double x)
        {
            // if x=y=0
            if (y == 0.0 && x == 0.0)
            {
                return 0.0;
            }
            // if x>0 Atan(y/x)
            if (x > 0.0)
            {
                return Atan(y / x);
            }
            // if x<0 sign(y)*(pi - Atan(|y/x|))
            if (x < 0.0)
            {
                if (y < 0.0)
                {
                    return -(Math.PI - Atan(y / x));
                }
                return Math.PI - Atan(-y / x);
            }
            // if x=0 y!=0 sign(y)*pi/2
            if (y < 0.0)
            {
                return -Math.PI / 2.0;
            }
            return Math.PI / 2.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 05SEP2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the closest <code>int</code> to the argument. The
         * result is rounded to an integer by adding 1/2, taking the
         * Floor of the result, and casting the result to type <code>int</code>.
         * In other words, the result is equal to the value of the expression:
         * <p>
         * <pre>(int)Math.Floor(a + 0.5f)</pre>
         * <p>
         * Special cases:
         * <ul>
         *  <li>If the argument is NaN, the result is 0.
         *  <li>If the argument is negative infinity or any value less than or
         *      equal to the value of <code>Integer.MIN_VALUE</code>, the result is
         *      equal to the value of <code>Integer.MIN_VALUE</code>.
         *  <li>If the argument is positive infinity or any value greater than or
         *      equal to the value of <code>Integer.MAX_VALUE</code>, the result is
         *      equal to the value of <code>Integer.MAX_VALUE</code>.
         * </ul>
         *
         * @param  a - a floating-point value to be rounded to an integer.
         * @return the value of the argument rounded to the nearest <code>int</code> value.
         */
        public static int Round(float a)
        {
            return (int)Math.Floor(a + 0.5f);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 05SEP2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the closest <code>long</code> to the argument. The result
         * is rounded to an integer by adding 1/2, taking the Floor of the
         * result, and casting the result to type <code>long</code>. In other
         * words, the result is equal to the value of the expression:
         * <p>
         * <pre>(long)Math.Floor(a + 0.5d)</pre>
         * <p>
         * Special cases:
         * <ul>
         *  <li>If the argument is NaN, the result is 0.
         *  <li>If the argument is negative infinity or any value less than or
         *      equal to the value of <code>Long.MIN_VALUE</code>, the result is
         *      equal to the value of <code>Long.MIN_VALUE</code>.
         *  <li>If the argument is positive infinity or any value greater than or
         *      equal to the value of <code>Long.MAX_VALUE</code>, the result is
         *      equal to the value of <code>Long.MAX_VALUE</code>.
         * </ul>
         *
         * @param a - a floating-point value to be rounded to a <code>long</code>.
         * @return the value of the argument rounded to the nearest <code>long</code> value.
         */
        public static long Round(double a)
        {
            return (long)Math.Floor(a + 0.5);
        }


        static public double Exp(double x)
        {
            if (x == 0.0)
            {
                return 1.0;
            }
            //
            double f = 1;
            long d = 1;
            double k;
            bool isless = (x < 0.0);
            if (isless)
            {
                x = -x;
            }
            k = x / d;
            //
            for (long i = 2; i < 50; i++)
            {
                f = f + k;
                k = k * x / i;
            }
            //
            if (isless)
            {
                return 1 / f;
            }
            return f;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns Euler's number <i>e</i> raised to the power of a
         * <code>double</code> value.  Special cases:
         * <ul><li>If the argument is NaN, the result is NaN.
         * <li>If the argument is positive infinity, then the result is
         * positive infinity.
         * <li>If the argument is negative infinity, then the result is
         * positive zero.</ul> <p>
         * A result must be within 1 ulp of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param a  the exponent to raise e to.
         * @return the value ea, where e is the base of the natural logarithms.
         */
        public static double Exp1(double a)
        {
            if (a == 0.0)
            {
                return 1.0;
            }
            bool isless = (a < 0.0);
            if (isless)
            {
                a = -a;
            }
            long intPart = (int)a;
            double fractionPart = a - intPart;
            double ret = 1;
            for (long i = 0; i < intPart; i++)
            {
                ret *= E;
            }
            double n = 1;
            double an = fractionPart;
            double sn = 1;
            double subRes = 1;
            if (fractionPart > 0)
            {
                subRes += fractionPart;
                while (an > PRECISION)
                {
                    an *= fractionPart;
                    n++;
                    sn = sn * n;
                    subRes += an / sn;

                }
            }
            ret *= subRes;
            if (isless)
            {
                return 1 / ret;
            }
            return ret;
        }

        static public double LoGdiv2 = -0.6931471805599453094;

        static private double Log(double x)
        {
            if (!(x > 0.0))
            {
                return Double.NaN;
            }
            //
            double f = 0.0;
            //
            int appendix = 0;
            while (x > 0.0 && x <= 1.0)
            {
                x *= 2.0;
                appendix++;
            }
            //
            x /= 2.0;
            appendix--;
            //
            double y1 = x - 1.0;
            double y2 = x + 1.0;
            double y = y1 / y2;
            //
            double k = y;
            y2 = k * y;
            //
            for (long i = 1; i < 50; i += 2)
            {
                f += k / i;
                k *= y2;
            }
            //
            f *= 2.0;
            for (int i = 0; i < appendix; i++)
            {
                f += LoGdiv2;
            }
            //
            return f;
        }

        static public double Log2(double x)
        {
            if (!(x > 0.0))
            {
                return Double.NaN;
            }
            //
            if (x == 1.0)
            {
                return 0.0;
            }
            // Argument of Log must be (0; 1]
            if (x > 1.0)
            {
                x = 1 / x;
                return -Log(x);
            }
            //
            return Log(x);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns the natural logarithm (base <i>e</i>) of a <code>double</code>
         * value.  Special cases:
         * <ul><li>If the argument is NaN or less than zero, then the result
         * is NaN.
         * <li>If the argument is positive infinity, then the result is
         * positive infinity.
         * <li>If the argument is positive zero or negative zero, then the
         * result is negative infinity.</ul><p>
         * A result must be within 1 ulp of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param a  a number greater than 0.0.
         * @return the value ln a, the natural logarithm of a.
         */
        public static double Log1(double a)
        {
            if (a <= 0.0)
            {
                return Double.NaN;
            }
            bool invert = false;
            if (a > 1.0)
            {
                invert = true;
                a = 1 / a;
            }
            if (a == 1.0)
            {
                return 0.0;
            }
            double x = a - 1.0;
            double a0 = x;
            double n = 1;
            double a1 = (-1) * x * n / (n + 1) * a0;
            double s = a0;
            while (Math.Abs(a1) > PRECISION)
            {
                s += a1;
                n += 1.0;
                a0 = a1;
                a1 = (-1) * x * n / (n + 1) * a0;
            }
            if (invert)
            {
                return -s;
            }
            return s;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns the value of the first argument raised to the power of the
         * second argument. Special cases:
         * <ul><li>If the second argument is positive or negative zero, then the
         * result is 1.0.
         * <li>If the second argument is 1.0, then the result is the same as the
         * first argument.
         * <li>If the second argument is NaN, then the result is NaN.
         * <li>If the first argument is NaN and the second argument is nonzero,
         * then the result is NaN.
         * <li>If
         * <ul>
         * <li>the absolute value of the first argument is greater than 1
         * and the second argument is positive infinity, or
         * <li>the absolute value of the first argument is less than 1 and
         * the second argument is negative infinity,
         * </ul>
         * then the result is positive infinity.
         * <li>If
         * <ul>
         * <li>the absolute value of the first argument is greater than 1 and
         * the second argument is negative infinity, or
         * <li>the absolute value of the
         * first argument is less than 1 and the second argument is positive
         * infinity,
         * </ul>
         * then the result is positive zero.
         * <li>If the absolute value of the first argument equals 1 and the
         * second argument is infinite, then the result is NaN.
         * <li>If
         * <ul>
         * <li>the first argument is positive zero and the second argument
         * is greater than zero, or
         * <li>the first argument is positive infinity and the second
         * argument is less than zero,
         * </ul>
         * then the result is positive zero.
         * <li>If
         * <ul>
         * <li>the first argument is positive zero and the second argument
         * is less than zero, or
         * <li>the first argument is positive infinity and the second
         * argument is greater than zero,
         * </ul>
         * then the result is positive infinity.
         * <li>If
         * <ul>
         * <li>the first argument is negative zero and the second argument
         * is greater than zero but not a finite odd integer, or
         * <li>the first argument is negative infinity and the second
         * argument is less than zero but not a finite odd integer,
         * </ul>
         * then the result is positive zero.
         * <li>If
         * <ul>
         * <li>the first argument is negative zero and the second argument
         * is a positive finite odd integer, or
         * <li>the first argument is negative infinity and the second
         * argument is a negative finite odd integer,
         * </ul>
         * then the result is negative zero.
         * <li>If
         * <ul>
         * <li>the first argument is negative zero and the second argument
         * is less than zero but not a finite odd integer, or
         * <li>the first argument is negative infinity and the second
         * argument is greater than zero but not a finite odd integer,
         * </ul>
         * then the result is positive infinity.
         * <li>If
         * <ul>
         * <li>the first argument is negative zero and the second argument
         * is a negative finite odd integer, or
         * <li>the first argument is negative infinity and the second
         * argument is a positive finite odd integer,
         * </ul>
         * then the result is negative infinity.
         * <li>If the first argument is finite and less than zero
         * <ul>
         * <li> if the second argument is a finite even integer, the
         * result is equal to the result of raising the absolute value of
         * the first argument to the power of the second argument
         * <li>if the second argument is a finite odd integer, the result
         * is equal to the negative of the result of raising the absolute
         * value of the first argument to the power of the second
         * argument
         * <li>if the second argument is finite and not an integer, then
         * the result is NaN.
         * </ul>
         * <li>If both arguments are integers, then the result is exactly equal
         * to the mathematical result of raising the first argument to the power
         * of the second argument if that result can in fact be represented
         * exactly as a <code>double</code> value.</ul>
         * <p>(In the foregoing descriptions, a floating-point value is
         * considered to be an integer if and only if it is finite and a
         * fixed point of the method <A href="/jdk142/api/java/lang/Math.html#Ceil
         * (double)"><CODE><tt>Ceil</tt></CODE></A> or,
         * equivalently, a fixed point of the method <A href="/jdk142/api/java/lang
         * /Math.html#Floor(double)"><CODE><tt>Floor</tt></CODE></A>.
         * A value is a fixed point of a one-argument
         * method if and only if the result of applying the method to the
         * value is equal to the value.)
         * <p>A result must be within 1 ulp of the correctly rounded
         * result.  Results must be semi-monotonic.<P><DD>
         * @param a  the base.
         * @param b  the exponent.
         * @return the value <code>a<sup>b</sup></code>.
         */
        public static double Pow(double a, double b)
        {
            return Math.Pow(a, b);

        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns the arc sine of an angle, in the range of -<i>pi</i>/2 through
         * <i>pi</i>/2. Special cases:
         * <ul><li>If the argument is NaN or its absolute value is greater
         * than 1, then the result is NaN.
         * <li>If the argument is zero, then the result is a zero with the
         * same sign as the argument.</ul> <p>
         * A result must be within 1 ulp of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param a  the value whose arc sine is to be returned.
         * @return the arc sine of the argument.
         */
        public static double Asin(double a)
        {
            return Math.Asin(a);

        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *<DD>Returns the arc cosine of an angle, in the range of 0.0 through
         * <i>pi</i>.  Special case:
         * <ul><li>If the argument is NaN or its absolute value is greater
         * than 1, then the result is NaN.</ul> <p>
         * A result must be within 1 ulp of the correctly rounded result.  Results
         * must be semi-monotonic.<P><DD>
         * @param a  the value whose arc cosine is to be returned.
         * @return the arc cosine of the argument.
         */
        public static double Acos(double a)
        {
            double f = Asin(a);
            if (f == Double.NaN)
            {
                return f;
            }
            return Math.PI / 2 - f;
        }
        
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the <code>double</code> value that is closest in value
         * to the argument and is equal to a mathematical integer. If two
         * <code>double</code> values that are mathematical integers are
         * equally Close to the value of the argument, the result is the
         * integer value that is even. Special cases:
         * <ul><li>If the argument value is already equal to a mathematical
         * integer, then the result is the same as the argument.
         * <li>If the argument is NaN or an infinity or positive zero or negative
         * zero, then the result is the same as the argument.</ul>
         *
         * @param   a   a value.
         * @return  the closest floating-point value to <code>a</code> that is
         *          equal to a mathematical integer.
         */
        public static double Rint(double a)
        {
            return a;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09JUN2007  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * <DD>Computes the remainder operation on two arguments as prescribed
         * by the IEEE 754 standard.
         * The remainder value is mathematically equal to
         * <code>f1&nbsp;-&nbsp;f2</code>&nbsp;&times;&nbsp;<i>n</i>,
         * where <i>n</i> is the mathematical integer closest to the exact
         * mathematical value of the quotient <code>f1/f2</code>, and if two
         * mathematical integers are equally Close to <code>f1/f2</code>,
         * then <i>n</i> is the integer that is even. If the remainder is
         * zero, its sign is the same as the sign of the first argument.
         * Special cases:
         * <ul><li>If either argument is NaN, or the first argument is infinite,
         * or the second argument is positive zero or negative zero, then the
         * result is NaN.
         * <li>If the first argument is finite and the second argument is
         * infinite, then the result is the same as the first argument.</ul>
         *<P><DD>
         * @param   f1 the dividend.
         * @param   f2 the divisor
         * @return  the remainder when f1 is divided by f2
         */
        public static double IEEEremainder(double f1, double f2)
        {
            return Math.IEEERemainder(f1, f2);

        }
    }

}
