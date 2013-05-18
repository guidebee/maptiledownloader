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
using System.Collections;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing.Geometry
{
    internal class Order3 : Curve
    {

        private readonly double _x0;
        private readonly double _y0;
        private readonly double _cx0;
        private readonly double _cy0;
        private readonly double _cx1;
        private readonly double _cy1;
        private readonly double _x1;
        private readonly double _y1;
        private readonly double _xmin;
        private readonly double _xmax;
        private readonly double _xcoeff0;
        private readonly double _xcoeff1;
        private readonly double _xcoeff2;
        private readonly double _xcoeff3;
        private readonly double _ycoeff0;
        private readonly double _ycoeff1;
        private readonly double _ycoeff2;
        private readonly double _ycoeff3;

        public static void Insert(ArrayList curves, double[] tmp,
                double x0, double y0,
                double cx0, double cy0,
                double cx1, double cy1,
                double x1, double y1,
                int direction)
        {
            int numparams = GetHorizontalParams(y0, cy0, cy1, y1, tmp);
            if (numparams == 0)
            {
                // We are using addInstance here to avoid inserting horisontal
                // segments
                AddInstance(curves, x0, y0, cx0, cy0, cx1, cy1, x1, y1, direction);
                return;
            }
            // Store coordinates for splitting at tmp[3..10]
            tmp[3] = x0;
            tmp[4] = y0;
            tmp[5] = cx0;
            tmp[6] = cy0;
            tmp[7] = cx1;
            tmp[8] = cy1;
            tmp[9] = x1;
            tmp[10] = y1;
            double t = tmp[0];
            if (numparams > 1 && t > tmp[1])
            {
                // Perform a "2 element sort"...
                tmp[0] = tmp[1];
                tmp[1] = t;
                t = tmp[0];
            }
            Split(tmp, 3, t);
            if (numparams > 1)
            {
                // Recalculate tmp[1] relative to the range [tmp[0]...1]
                t = (tmp[1] - t) / (1 - t);
                Split(tmp, 9, t);
            }
            int index = 3;
            if (direction == DECREASING)
            {
                index += numparams * 6;
            }
            while (numparams >= 0)
            {
                AddInstance(curves,
                        tmp[index + 0], tmp[index + 1],
                        tmp[index + 2], tmp[index + 3],
                        tmp[index + 4], tmp[index + 5],
                        tmp[index + 6], tmp[index + 7],
                        direction);
                numparams--;
                if (direction == INCREASING)
                {
                    index += 6;
                }
                else
                {
                    index -= 6;
                }
            }
        }

        public static void AddInstance(ArrayList curves,
                double x0, double y0,
                double cx0, double cy0,
                double cx1, double cy1,
                double x1, double y1,
                int direction)
        {
            if (y0 > y1)
            {
                curves.Add(new Order3(x1, y1, cx1, cy1, cx0, cy0, x0, y0,
                        -direction));
            }
            else if (y1 > y0)
            {
                curves.Add(new Order3(x0, y0, cx0, cy0, cx1, cy1, x1, y1,
                        direction));
            }
        }

        /*
         * Return the count of the number of horizontal sections of the
         * specified cubic Bezier curve.  Put the parameters for the
         * horizontal sections into the specified <code>ret</code> array.
         * <p>
         * If we examine the parametric equation in t, we have:
         *   Py(t) = C0(1-t)^3 + 3CP0 t(1-t)^2 + 3CP1 t^2(1-t) + C1 t^3
         *         = C0 - 3C0t + 3C0t^2 - C0t^3 +
         *           3CP0t - 6CP0t^2 + 3CP0t^3 +
         *           3CP1t^2 - 3CP1t^3 +
         *           C1t^3
         *   Py(t) = (C1 - 3CP1 + 3CP0 - C0) t^3 +
         *           (3C0 - 6CP0 + 3CP1) t^2 +
         *           (3CP0 - 3C0) t +
         *           (C0)
         * If we take the derivative, we get:
         *   Py(t) = Dt^3 + At^2 + Bt + C
         *   dPy(t) = 3Dt^2 + 2At + B = 0
         *        0 = 3*(C1 - 3*CP1 + 3*CP0 - C0)t^2
         *          + 2*(3*CP1 - 6*CP0 + 3*C0)t
         *          + (3*CP0 - 3*C0)
         *        0 = 3*(C1 - 3*CP1 + 3*CP0 - C0)t^2
         *          + 3*2*(CP1 - 2*CP0 + C0)t
         *          + 3*(CP0 - C0)
         *        0 = (C1 - CP1 - CP1 - CP1 + CP0 + CP0 + CP0 - C0)t^2
         *          + 2*(CP1 - CP0 - CP0 + C0)t
         *          + (CP0 - C0)
         *        0 = (C1 - CP1 + CP0 - CP1 + CP0 - CP1 + CP0 - C0)t^2
         *          + 2*(CP1 - CP0 - CP0 + C0)t
         *          + (CP0 - C0)
         *        0 = ((C1 - CP1) - (CP1 - CP0) - (CP1 - CP0) + (CP0 - C0))t^2
         *          + 2*((CP1 - CP0) - (CP0 - C0))t
         *          + (CP0 - C0)
         * that this method will return 0 if the equation is a line,
         * which is either always horizontal or never horizontal.
         * Completely horizontal curves need to be eliminated by other
         * means outside of this method.
         */
        public static int GetHorizontalParams(double c0, double cp0,
                double cp1, double c1,
                double[] ret)
        {
            if (c0 <= cp0 && cp0 <= cp1 && cp1 <= c1)
            {
                return 0;
            }
            c1 -= cp1;
            cp1 -= cp0;
            cp0 -= c0;
            ret[0] = cp0;
            ret[1] = (cp1 - cp0) * 2;
            ret[2] = (c1 - cp1 - cp1 + cp0);
            int numroots = QuadCurve.SolveQuadratic(ret, ret);
            int j = 0;
            for (int i = 0; i < numroots; i++)
            {
                double t = ret[i];
                // No splits at t==0 and t==1
                if (t > 0 && t < 1)
                {
                    if (j < i)
                    {
                        ret[j] = t;
                    }
                    j++;
                }
            }
            return j;
        }

        /*
         * Split the cubic Bezier stored at coords[pos...pos+7] representing
         * the parametric range [0..1] into two subcurves representing the
         * parametric subranges [0..t] and [t..1].  Store the results back
         * into the array at coords[pos...pos+7] and coords[pos+6...pos+13].
         */
        public static void Split(double[] coords, int pos, double t)
        {
            double x0, y0, cx0, cy0, cx1, cy1, x1, y1;
            coords[pos + 12] = x1 = coords[pos + 6];
            coords[pos + 13] = y1 = coords[pos + 7];
            cx1 = coords[pos + 4];
            cy1 = coords[pos + 5];
            x1 = cx1 + (x1 - cx1) * t;
            y1 = cy1 + (y1 - cy1) * t;
            x0 = coords[pos + 0];
            y0 = coords[pos + 1];
            cx0 = coords[pos + 2];
            cy0 = coords[pos + 3];
            x0 = x0 + (cx0 - x0) * t;
            y0 = y0 + (cy0 - y0) * t;
            cx0 = cx0 + (cx1 - cx0) * t;
            cy0 = cy0 + (cy1 - cy0) * t;
            cx1 = cx0 + (x1 - cx0) * t;
            cy1 = cy0 + (y1 - cy0) * t;
            cx0 = x0 + (cx0 - x0) * t;
            cy0 = y0 + (cy0 - y0) * t;
            coords[pos + 2] = x0;
            coords[pos + 3] = y0;
            coords[pos + 4] = cx0;
            coords[pos + 5] = cy0;
            coords[pos + 6] = cx0 + (cx1 - cx0) * t;
            coords[pos + 7] = cy0 + (cy1 - cy0) * t;
            coords[pos + 8] = cx1;
            coords[pos + 9] = cy1;
            coords[pos + 10] = x1;
            coords[pos + 11] = y1;
        }

        public Order3(double x0, double y0,
                double cx0, double cy0,
                double cx1, double cy1,
                double x1, double y1,
                int direction)
            : base(direction)
        {

            // REMIND: Better accuracy in the root finding methods would
            //  ensure that cys are in range.  As it stands, they are never
            //  more than "1 mantissa bit" out of range...
            if (cy0 < y0)
            {
                cy0 = y0;
            }
            if (cy1 > y1)
            {
                cy1 = y1;
            }
            _x0 = x0;
            _y0 = y0;
            _cx0 = cx0;
            _cy0 = cy0;
            _cx1 = cx1;
            _cy1 = cy1;
            _x1 = x1;
            _y1 = y1;
            _xmin = MathEx.Min(MathEx.Min(x0, x1), MathEx.Min(cx0, cx1));
            _xmax = MathEx.Max(MathEx.Max(x0, x1), MathEx.Max(cx0, cx1));
            _xcoeff0 = x0;
            _xcoeff1 = (cx0 - x0) * 3.0;
            _xcoeff2 = (cx1 - cx0 - cx0 + x0) * 3.0;
            _xcoeff3 = x1 - (cx1 - cx0) * 3.0 - x0;
            _ycoeff0 = y0;
            _ycoeff1 = (cy0 - y0) * 3.0;
            _ycoeff2 = (cy1 - cy0 - cy0 + y0) * 3.0;
            _ycoeff3 = y1 - (cy1 - cy0) * 3.0 - y0;
            _yforT1 = _yforT2 = _yforT3 = y0;
        }

        public override int GetOrder()
        {
            return 3;
        }

        public override double GetXTop()
        {
            return _x0;
        }

        public override double GetYTop()
        {
            return _y0;
        }

        public override double GetXBot()
        {
            return _x1;
        }

        public override double GetYBot()
        {
            return _y1;
        }

        public override double GetXMin()
        {
            return _xmin;
        }

        public override double GetXMax()
        {
            return _xmax;
        }

        public override double GetX0()
        {
            return (_direction == INCREASING) ? _x0 : _x1;
        }

        public override double GetY0()
        {
            return (_direction == INCREASING) ? _y0 : _y1;
        }

        public double GetCX0()
        {
            return (_direction == INCREASING) ? _cx0 : _cx1;
        }

        public double GetCY0()
        {
            return (_direction == INCREASING) ? _cy0 : _cy1;
        }

        public double GetCX1()
        {
            return (_direction == DECREASING) ? _cx0 : _cx1;
        }

        public double GetCY1()
        {
            return (_direction == DECREASING) ? _cy0 : _cy1;
        }

        public override double GetX1()
        {
            return (_direction == DECREASING) ? _x0 : _x1;
        }

        public override double GetY1()
        {
            return (_direction == DECREASING) ? _y0 : _y1;
        }
        private double _tforY1;
        private double _yforT1;
        private double _tforY2;
        private double _yforT2;
        private double _tforY3;
        private double _yforT3;

        /*
         * Solve the cubic whose coefficients are in the a,b,c,d fields and
         * return the first root in the range [0, 1].
         * The cubic solved is represented by the equation:
         *     x^3 + (ycoeff2)x^2 + (ycoeff1)x + (ycoeff0) = y
         * @return the first valid root (in the range [0, 1])
         */
        public override double TforY(double y)
        {
            if (y <= _y0)
            {
                return 0;
            }
            if (y >= _y1)
            {
                return 1;
            }
            if (y == _yforT1)
            {
                return _tforY1;
            }
            if (y == _yforT2)
            {
                return _tforY2;
            }
            if (y == _yforT3)
            {
                return _tforY3;
            }
            // From Numerical Recipes, 5.6, Quadratic and Cubic Equations
            if (_ycoeff3 == 0.0)
            {
                // The cubic degenerated to quadratic (or line or ...).
                return Order2.TforY(y, _ycoeff0, _ycoeff1, _ycoeff2);
            }
            double a = _ycoeff2 / _ycoeff3;
            double b = _ycoeff1 / _ycoeff3;
            double c = (_ycoeff0 - y) / _ycoeff3;
            double d = (a * a - 3.0 * b) / 9.0;
            double d1 = (2.0 * a * a * a - 9.0 * a * b + 27.0 * c) / 54.0;
            double r21 = d1 * d1;
            double q31 = d * d * d;
            double a3 = a / 3.0;
            double t;
            if (r21 < q31)
            {
                double theta = MathEx.Acos(d1 / MathEx.Sqrt(q31));
                d = -2.0 * MathEx.Sqrt(d);
                t = Refine(a, b, c, y, d * MathEx.Cos(theta / 3.0) - a3);
                if (t < 0)
                {
                    t = Refine(a, b, c, y,
                            d * MathEx.Cos((theta + MathEx.PI * 2.0) / 3.0) - a3);
                }
                if (t < 0)
                {
                    t = Refine(a, b, c, y,
                            d * MathEx.Cos((theta - MathEx.PI * 2.0) / 3.0) - a3);
                }
            }
            else
            {
                bool neg = (d1 < 0.0);
                double sqrt = MathEx.Sqrt(r21 - q31);
                if (neg)
                {
                    d1 = -d1;
                }
                double pow = MathEx.Pow(d1 + sqrt, 1.0 / 3.0);
                if (!neg)
                {
                    pow = -pow;
                }
                double d2 = (pow == 0.0) ? 0.0 : (d / pow);
                t = Refine(a, b, c, y, (pow + d2) - a3);
            }
            if (t < 0)
            {
                //throw new InternalError("bad t");
                double t0 = 0;
                double t1 = 1;
                while (true)
                {
                    t = (t0 + t1) / 2;
                    if (t == t0 || t == t1)
                    {
                        break;
                    }
                    double yt = YforT(t);
                    if (yt < y)
                    {
                        t0 = t;
                    }
                    else if (yt > y)
                    {
                        t1 = t;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (t >= 0)
            {
                _tforY3 = _tforY2;
                _yforT3 = _yforT2;
                _tforY2 = _tforY1;
                _yforT2 = _yforT1;
                _tforY1 = t;
                _yforT1 = y;
            }
            return t;
        }

        public double Refine(double a, double b, double c,
                double target, double t)
        {
            if (t < -0.1 || t > 1.1)
            {
                return -1;
            }
            double y = YforT(t);
            double t0, t1;
            if (y < target)
            {
                t0 = t;
                t1 = 1;
            }
            else
            {
                t0 = 0;
                t1 = t;
            }
            bool useslope = true;
            while (y != target)
            {
                if (!useslope)
                {
                    double t2 = (t0 + t1) / 2;
                    if (t2 == t0 || t2 == t1)
                    {
                        break;
                    }
                    t = t2;
                }
                else
                {
                    double slope = DYforT(t, 1);
                    if (slope == 0)
                    {
                        useslope = false;
                        continue;
                    }
                    double t2 = t + ((target - y) / slope);
                    if (t2 == t || t2 <= t0 || t2 >= t1)
                    {
                        useslope = false;
                        continue;
                    }
                    t = t2;
                }
                y = YforT(t);
                if (y < target)
                {
                    t0 = t;
                }
                else if (y > target)
                {
                    t1 = t;
                }
                else
                {
                    break;
                }
            }

            return (t > 1) ? -1 : t;
        }

        public override double XforY(double y)
        {
            if (y <= _y0)
            {
                return _x0;
            }
            if (y >= _y1)
            {
                return _x1;
            }
            return XforT(TforY(y));
        }

        public override double XforT(double t)
        {
            return (((_xcoeff3 * t) + _xcoeff2) * t + _xcoeff1) * t + _xcoeff0;
        }

        public override double YforT(double t)
        {
            return (((_ycoeff3 * t) + _ycoeff2) * t + _ycoeff1) * t + _ycoeff0;
        }

        public override double DXforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return (((_xcoeff3 * t) + _xcoeff2) * t + _xcoeff1) * t + _xcoeff0;
                case 1:
                    return ((3 * _xcoeff3 * t) + 2 * _xcoeff2) * t + _xcoeff1;
                case 2:
                    return (6 * _xcoeff3 * t) + 2 * _xcoeff2;
                case 3:
                    return 6 * _xcoeff3;
                default:
                    return 0;
            }
        }

        public override double DYforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return (((_ycoeff3 * t) + _ycoeff2) * t + _ycoeff1) * t + _ycoeff0;
                case 1:
                    return ((3 * _ycoeff3 * t) + 2 * _ycoeff2) * t + _ycoeff1;
                case 2:
                    return (6 * _ycoeff3 * t) + 2 * _ycoeff2;
                case 3:
                    return 6 * _ycoeff3;
                default:
                    return 0;
            }
        }

        public override double NextVertical(double t0, double t1)
        {
            double[] eqn = { _xcoeff1, 2 * _xcoeff2, 3 * _xcoeff3 };
            int numroots = QuadCurve.SolveQuadratic(eqn, eqn);
            for (int i = 0; i < numroots; i++)
            {
                if (eqn[i] > t0 && eqn[i] < t1)
                {
                    t1 = eqn[i];
                }
            }
            return t1;
        }

        public override void Enlarge(Rectangle r)
        {
            r.Add((int)(_x0 + .5),
                    (int)(_y0 + .5));
            double[] eqn = { _xcoeff1, 2 * _xcoeff2, 3 * _xcoeff3 };
            int numroots = QuadCurve.SolveQuadratic(eqn, eqn);
            for (int i = 0; i < numroots; i++)
            {
                double t = eqn[i];
                if (t > 0 && t < 1)
                {
                    r.Add((int)(XforT(t) + .5), (int)(YforT(t) + .5));
                }
            }
            r.Add((int)(_x1 + .5), (int)(_y1 + .5));
        }

        public override Curve GetSubCurve(double ystart, double yend, int dir)
        {
            if (ystart <= _y0 && yend >= _y1)
            {
                return GetWithDirection(dir);
            }
            double[] eqn = new double[14];
            double t0, t1;
            t0 = TforY(ystart);
            t1 = TforY(yend);
            eqn[0] = _x0;
            eqn[1] = _y0;
            eqn[2] = _cx0;
            eqn[3] = _cy0;
            eqn[4] = _cx1;
            eqn[5] = _cy1;
            eqn[6] = _x1;
            eqn[7] = _y1;
            if (t0 > t1)
            {
                /* This happens in only rare cases where ystart is
                 * very near yend and solving for the yend root ends
                 * up stepping slightly lower in t than solving for
                 * the ystart root.
                 * Ideally we might want to skip this tiny little
                 * segment and just fudge the surrounding coordinates
                 * to bridge the gap left behind, but there is no way
                 * to do that from here.  Higher levels could
                 * potentially eliminate these tiny "fixup" segments,
                 * but not without a lot of extra work on the code that
                 * coalesces chains of curves into subpaths.  The
                 * simplest solution for now is to just reorder the t
                 * values and chop out a miniscule curve piece.
                 */
                double t = t0;
                t0 = t1;
                t1 = t;
            }
            if (t1 < 1)
            {
                Split(eqn, 0, t1);
            }
            int i;
            if (t0 <= 0)
            {
                i = 0;
            }
            else
            {
                Split(eqn, 0, t0 / t1);
                i = 6;
            }
            return new Order3(eqn[i + 0], ystart,
                    eqn[i + 2], eqn[i + 3],
                    eqn[i + 4], eqn[i + 5],
                    eqn[i + 6], yend,
                    dir);
        }

        public override Curve GetReversedCurve()
        {
            return new Order3(_x0, _y0, _cx0, _cy0, _cx1, _cy1, _x1, _y1, -_direction);
        }

        public override int GetSegment(double[] coords)
        {
            if (_direction == INCREASING)
            {
                coords[0] = _cx0;
                coords[1] = _cy0;
                coords[2] = _cx1;
                coords[3] = _cy1;
                coords[4] = _x1;
                coords[5] = _y1;
            }
            else
            {
                coords[0] = _cx1;
                coords[1] = _cy1;
                coords[2] = _cx0;
                coords[3] = _cy0;
                coords[4] = _x0;
                coords[5] = _y0;
            }
            return PathIterator.SEG_CUBICTO;
        }

        public override String ControlPointString()
        {
            return (("(" + Round(GetCX0()) + ", " + Round(GetCY0()) + "), ") +
                    ("(" + Round(GetCX1()) + ", " + Round(GetCY1()) + "), "));
        }
    }

}
