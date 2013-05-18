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

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing.Geometry
{
    internal class Order2 : Curve
    {

        private readonly double _x0;
        private readonly double _y0;
        private readonly double _cx0;
        private readonly double _cy0;
        private readonly double _x1;
        private readonly double _y1;
        private readonly double _xmin;
        private readonly double _xmax;
        private readonly double _xcoeff0;
        private readonly double _xcoeff1;
        private readonly double _xcoeff2;
        private readonly double _ycoeff0;
        private readonly double _ycoeff1;
        private readonly double _ycoeff2;

        public static void Insert(ArrayList curves, double[] tmp,
                double x0, double y0,
                double cx0, double cy0,
                double x1, double y1,
                int direction)
        {
            int numparams = GetHorizontalParams(y0, cy0, y1, tmp);
            if (numparams == 0)
            {
                // We are using addInstance here to avoid inserting horisontal
                // segments
                AddInstance(curves, x0, y0, cx0, cy0, x1, y1, direction);
                return;
            }
            // assert(numparams == 1);
            double t = tmp[0];
            tmp[0] = x0;
            tmp[1] = y0;
            tmp[2] = cx0;
            tmp[3] = cy0;
            tmp[4] = x1;
            tmp[5] = y1;
            Split(tmp, 0, t);
            int i0 = (direction == INCREASING) ? 0 : 4;
            int i1 = 4 - i0;
            AddInstance(curves, tmp[i0], tmp[i0 + 1], tmp[i0 + 2], tmp[i0 + 3],
                    tmp[i0 + 4], tmp[i0 + 5], direction);
            AddInstance(curves, tmp[i1], tmp[i1 + 1], tmp[i1 + 2], tmp[i1 + 3],
                    tmp[i1 + 4], tmp[i1 + 5], direction);
        }

        public static void AddInstance(ArrayList curves,
                double x0, double y0,
                double cx0, double cy0,
                double x1, double y1,
                int direction)
        {
            if (y0 > y1)
            {
                curves.Add(new Order2(x1, y1, cx0, cy0, x0, y0, -direction));
            }
            else if (y1 > y0)
            {
                curves.Add(new Order2(x0, y0, cx0, cy0, x1, y1, direction));
            }
        }

        /*
         * Return the count of the number of horizontal sections of the
         * specified quadratic Bezier curve.  Put the parameters for the
         * horizontal sections into the specified <code>ret</code> array.
         * <p>
         * If we examine the parametric equation in t, we have:
         *     Py(t) = C0*(1-t)^2 + 2*CP*t*(1-t) + C1*t^2
         *           = C0 - 2*C0*t + C0*t^2 + 2*CP*t - 2*CP*t^2 + C1*t^2
         *           = C0 + (2*CP - 2*C0)*t + (C0 - 2*CP + C1)*t^2
         *     Py(t) = (C0 - 2*CP + C1)*t^2 + (2*CP - 2*C0)*t + (C0)
         * If we take the derivative, we get:
         *     Py(t) = At^2 + Bt + C
         *     dPy(t) = 2At + B = 0
         *     2*(C0 - 2*CP + C1)t + 2*(CP - C0) = 0
         *     2*(C0 - 2*CP + C1)t = 2*(C0 - CP)
         *     t = 2*(C0 - CP) / 2*(C0 - 2*CP + C1)
         *     t = (C0 - CP) / (C0 - CP + C1 - CP)
         * that this method will return 0 if the equation is a line,
         * which is either always horizontal or never horizontal.
         * Completely horizontal curves need to be eliminated by other
         * means outside of this method.
         */
        public static int GetHorizontalParams(double c0, double cp, double c1,
                double[] ret)
        {
            if (c0 <= cp && cp <= c1)
            {
                return 0;
            }
            c0 -= cp;
            c1 -= cp;
            double denom = c0 + c1;
            // If denom == 0 then cp == (c0+c1)/2 and we have a line.
            if (denom == 0)
            {
                return 0;
            }
            double t = c0 / denom;
            // No splits at t==0 and t==1
            if (t <= 0 || t >= 1)
            {
                return 0;
            }
            ret[0] = t;
            return 1;
        }

        /*
         * Split the quadratic Bezier stored at coords[pos...pos+5] representing
         * the paramtric range [0..1] into two subcurves representing the
         * parametric subranges [0..t] and [t..1].  Store the results back
         * into the array at coords[pos...pos+5] and coords[pos+4...pos+9].
         */
        public static void Split(double[] coords, int pos, double t)
        {
            double x1, y1;
            coords[pos + 8] = x1 = coords[pos + 4];
            coords[pos + 9] = y1 = coords[pos + 5];
            double cx = coords[pos + 2];
            double cy = coords[pos + 3];
            x1 = cx + (x1 - cx) * t;
            y1 = cy + (y1 - cy) * t;
            double x0 = coords[pos + 0];
            double y0 = coords[pos + 1];
            x0 = x0 + (cx - x0) * t;
            y0 = y0 + (cy - y0) * t;
            cx = x0 + (x1 - x0) * t;
            cy = y0 + (y1 - y0) * t;
            coords[pos + 2] = x0;
            coords[pos + 3] = y0;
            coords[pos + 4] = cx;
            coords[pos + 5] = cy;
            coords[pos + 6] = x1;
            coords[pos + 7] = y1;
        }

        public Order2(double x0, double y0,
                double cx0, double cy0,
                double x1, double y1,
                int direction)
            : base(direction)
        {
            // REMIND: Better accuracy in the root finding methods would
            //  ensure that cy0 is in range.  As it stands, it is never
            //  more than "1 mantissa bit" out of range...
            if (cy0 < y0)
            {
                cy0 = y0;
            }
            else if (cy0 > y1)
            {
                cy0 = y1;
            }
            _x0 = x0;
            _y0 = y0;
            _cx0 = cx0;
            _cy0 = cy0;
            _x1 = x1;
            _y1 = y1;
            _xmin = Math.Min(Math.Min(x0, x1), cx0);
            _xmax = Math.Max(Math.Max(x0, x1), cx0);
            _xcoeff0 = x0;
            _xcoeff1 = cx0 + cx0 - x0 - x0;
            _xcoeff2 = x0 - cx0 - cx0 + x1;
            _ycoeff0 = y0;
            _ycoeff1 = cy0 + cy0 - y0 - y0;
            _ycoeff2 = y0 - cy0 - cy0 + y1;
        }

        public override int GetOrder()
        {
            return 2;
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
            return _cx0;
        }

        public double GetCY0()
        {
            return _cy0;
        }

        public override double GetX1()
        {
            return (_direction == DECREASING) ? _x0 : _x1;
        }

        public override double GetY1()
        {
            return (_direction == DECREASING) ? _y0 : _y1;
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
            return TforY(y, _ycoeff0, _ycoeff1, _ycoeff2);
        }

        public static double TforY(double y,
                double ycoeff0, double ycoeff1, double ycoeff2)
        {
            // The caller should have already eliminated y values
            // outside of the y0 to y1 range.
            ycoeff0 -= y;
            if (ycoeff2 == 0.0)
            {
                // The quadratic parabola has degenerated to a line.
                // ycoeff1 should not be 0.0 since we have already eliminated
                // totally horizontal lines, but if it is, then we will generate
                // infinity here for the root, which will not be in the [0,1]
                // range so we will pass to the failure code below.
                double root = -ycoeff0 / ycoeff1;
                if (root >= 0 && root <= 1)
                {
                    return root;
                }
            }
            else
            {
                // From Numerical Recipes, 5.6, Quadratic and Cubic Equations
                double d = ycoeff1 * ycoeff1 - 4.0 * ycoeff2 * ycoeff0;
                // If d < 0.0, then there are no roots
                if (d >= 0.0)
                {
                    d = Math.Sqrt(d);
                    // For accuracy, calculate one root using:
                    //     (-ycoeff1 +/- d) / 2ycoeff2
                    // and the other using:
                    //     2ycoeff0 / (-ycoeff1 +/- d)
                    // Choose the sign of the +/- so that ycoeff1+d
                    // gets larger in magnitude
                    if (ycoeff1 < 0.0)
                    {
                        d = -d;
                    }
                    double q = (ycoeff1 + d) / -2.0;
                    // We already tested ycoeff2 for being 0 above
                    double root = q / ycoeff2;
                    if (root >= 0 && root <= 1)
                    {
                        return root;
                    }
                    if (q != 0.0)
                    {
                        root = ycoeff0 / q;
                        if (root >= 0 && root <= 1)
                        {
                            return root;
                        }
                    }
                }
            }
            /* We failed to find a root in [0,1].  What could have gone wrong?
             * First, remember that these curves are constructed to be monotonic
             * in Y and totally horizontal curves have already been eliminated.
             * Now keep in mind that the Y coefficients of the polynomial form
             * of the curve are calculated from the Y coordinates which define
             * our curve.  They should theoretically define the same curve,
             * but they can be off by a couple of bits of precision after the
             * math is done and so can represent a slightly modified curve.
             * This is normally not an issue except when we have solutions near
             * the endpoints.  Since the answers we get from solving the polynomial
             * may be off by a few bits that means that they could lie just a
             * few bits of precision outside the [0,1] range.
             *
             * Another problem could be that while the parametric curve defined
             * by the Y coordinates has a local minima or maxima at or just
             * outside of the endpoints, the polynomial form might express
             * that same Min/Max just inside of and just shy of the Y coordinate
             * of that endpoint.  In that case, if we solve for a Y coordinate
             * at or near that endpoint, we may be solving for a Y coordinate
             * that is below that minima or above that maxima and we would find
             * no solutions at all.
             *
             * In either case, we can assume that y is so near one of the
             * endpoints that we can just collapse it onto the nearest endpoint
             * without losing more than a couple of bits of precision.
             */
            // First calculate the midpoint between y0 and y1 and choose to
            // return either 0.0 or 1.0 depending on whether y is above
            // or below the midpoint...
            // Note that we subtracted y from ycoeff0 above so both y0 and y1
            // will be "relative to y" so we are really just looking at where
            // zero falls with respect to the "relative midpoint" here.
            double y0 = ycoeff0;
            double y1 = ycoeff0 + ycoeff1 + ycoeff2;
            return (0 < (y0 + y1) / 2) ? 0.0 : 1.0;
        }

        public override double XforT(double t)
        {
            return (_xcoeff2 * t + _xcoeff1) * t + _xcoeff0;
        }

        public override double YforT(double t)
        {
            return (_ycoeff2 * t + _ycoeff1) * t + _ycoeff0;
        }

        public override double DXforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return (_xcoeff2 * t + _xcoeff1) * t + _xcoeff0;
                case 1:
                    return 2 * _xcoeff2 * t + _xcoeff1;
                case 2:
                    return 2 * _xcoeff2;
                default:
                    return 0;
            }
        }

        public override double DYforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return (_ycoeff2 * t + _ycoeff1) * t + _ycoeff0;
                case 1:
                    return 2 * _ycoeff2 * t + _ycoeff1;
                case 2:
                    return 2 * _ycoeff2;
                default:
                    return 0;
            }
        }

        public override double NextVertical(double t0, double t1)
        {
            double t = -_xcoeff1 / (2 * _xcoeff2);
            if (t > t0 && t < t1)
            {
                return t;
            }
            return t1;
        }

        public override void Enlarge(Rectangle r)
        {
            r.Add((int)(_x0 + 0.5), (int)(_y0 + 0.5));
            double t = -_xcoeff1 / (2 * _xcoeff2);
            if (t > 0 && t < 1)
            {
                r.Add((int)(XforT(t) + .5), (int)(YforT(t) + .5));
            }
            r.Add((int)(_x1 + 0.5), (int)(_y1 + 0.5));
        }

        public override Curve GetSubCurve(double ystart, double yend, int dir)
        {
            double t0, t1;
            if (ystart <= _y0)
            {
                if (yend >= _y1)
                {
                    return GetWithDirection(dir);
                }
                t0 = 0;
            }
            else
            {
                t0 = TforY(ystart, _ycoeff0, _ycoeff1, _ycoeff2);
            }
            if (yend >= _y1)
            {
                t1 = 1;
            }
            else
            {
                t1 = TforY(yend, _ycoeff0, _ycoeff1, _ycoeff2);
            }
            double[] eqn = new double[10];
            eqn[0] = _x0;
            eqn[1] = _y0;
            eqn[2] = _cx0;
            eqn[3] = _cy0;
            eqn[4] = _x1;
            eqn[5] = _y1;
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
                i = 4;
            }
            return new Order2(eqn[i + 0], ystart,
                    eqn[i + 2], eqn[i + 3],
                    eqn[i + 4], yend,
                    dir);
        }

        public override Curve GetReversedCurve()
        {
            return new Order2(_x0, _y0, _cx0, _cy0, _x1, _y1, -_direction);
        }

        public override int GetSegment(double[] coords)
        {
            coords[0] = _cx0;
            coords[1] = _cy0;
            if (_direction == INCREASING)
            {
                coords[2] = _x1;
                coords[3] = _y1;
            }
            else
            {
                coords[2] = _x0;
                coords[3] = _y0;
            }
            return PathIterator.SEG_QUADTO;
        }

        public override String ControlPointString()
        {
            return ("(" + Round(_cx0) + ", " + Round(_cy0) + "), ");
        }
    }

}
