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
    internal class Order1 : Curve
    {

        private readonly double _x0;
        private readonly double _y0;
        private readonly double _x1;
        private readonly double _y1;
        private readonly double _xmin;
        private readonly double _xmax;

        public Order1(double x0, double y0,
                double x1, double y1,
                int direction)
            : base(direction)
        {

            _x0 = x0;
            _y0 = y0;
            _x1 = x1;
            _y1 = y1;
            if (x0 < x1)
            {
                _xmin = x0;
                _xmax = x1;
            }
            else
            {
                _xmin = x1;
                _xmax = x0;
            }
        }

        public override int GetOrder()
        {
            return 1;
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
            if (_x0 == _x1 || y <= _y0)
            {
                return _x0;
            }
            if (y >= _y1)
            {
                return _x1;
            }
            // assert(y0 != y1); /* No horizontal lines... */
            return (_x0 + (y - _y0) * (_x1 - _x0) / (_y1 - _y0));
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
            return (y - _y0) / (_y1 - _y0);
        }

        public override double XforT(double t)
        {
            return _x0 + t * (_x1 - _x0);
        }

        public override double YforT(double t)
        {
            return _y0 + t * (_y1 - _y0);
        }

        public override double DXforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return _x0 + t * (_x1 - _x0);
                case 1:
                    return (_x1 - _x0);
                default:
                    return 0;
            }
        }

        public override double DYforT(double t, int deriv)
        {
            switch (deriv)
            {
                case 0:
                    return _y0 + t * (_y1 - _y0);
                case 1:
                    return (_y1 - _y0);
                default:
                    return 0;
            }
        }

        public override double NextVertical(double t0, double t1)
        {
            return t1;
        }

        public override bool AccumulateCrossings(Crossings c)
        {
            double xlo = c.GetXLo();
            double ylo = c.GetYLo();
            double xhi = c.GetXHi();
            double yhi = c.GetYHi();
            if (_xmin >= xhi)
            {
                return false;
            }
            double xstart, ystart, xend, yend;
            if (_y0 < ylo)
            {
                if (_y1 <= ylo)
                {
                    return false;
                }
                ystart = ylo;
                xstart = XforY(ylo);
            }
            else
            {
                if (_y0 >= yhi)
                {
                    return false;
                }
                ystart = _y0;
                xstart = _x0;
            }
            if (_y1 > yhi)
            {
                yend = yhi;
                xend = XforY(yhi);
            }
            else
            {
                yend = _y1;
                xend = _x1;
            }
            if (xstart >= xhi && xend >= xhi)
            {
                return false;
            }
            if (xstart > xlo || xend > xlo)
            {
                return true;
            }
            c.Record((int)(ystart + .5), (int)(yend + 0.5), _direction);
            return false;
        }

        public override void Enlarge(Rectangle r)
        {
            r.Add((int)(_x0 + 0.5), (int)(_y0 + 0.5));
            r.Add((int)(_x1 + 0.5), (int)(_y1 + 0.5));
        }

        public override Curve GetSubCurve(double ystart, double yend, int dir)
        {
            if (ystart == _y0 && yend == _y1)
            {
                return GetWithDirection(dir);
            }
            if (_x0 == _x1)
            {
                return new Order1(_x0, ystart, _x1, yend, dir);
            }
            double num = _x0 - _x1;
            double denom = _y0 - _y1;
            double xstart = (_x0 + (ystart - _y0) * num / denom);
            double xend = (_x0 + (yend - _y0) * num / denom);
            return new Order1(xstart, ystart, xend, yend, dir);
        }

        public override Curve GetReversedCurve()
        {
            return new Order1(_x0, _y0, _x1, _y1, -_direction);
        }

        public new int CompareTo(Curve other, double[] yrange)
        {
            if (!(other is Order1))
            {
                return base.CompareTo(other, yrange);
            }
            Order1 c1 = (Order1)other;
            if (yrange[1] <= yrange[0])
            {
                throw new SystemException("yrange already screwed up...");
            }
            yrange[1] = Math.Min(Math.Min(yrange[1], _y1), c1._y1);
            if (yrange[1] <= yrange[0])
            {
                throw new SystemException("backstepping from " + yrange[0] + " to " + yrange[1]);
            }
            if (_xmax <= c1._xmin)
            {
                return (_xmin == c1._xmax) ? 0 : -1;
            }
            if (_xmin >= c1._xmax)
            {
                return 1;
            }
            /*
             * If "this" is curve A and "other" is curve B, then...
             * xA(y) = x0A + (y - y0A) (x1A - x0A) / (y1A - y0A)
             * xB(y) = x0B + (y - y0B) (x1B - x0B) / (y1B - y0B)
             * xA(y) == xB(y)
             * x0A + (y - y0A) (x1A - x0A) / (y1A - y0A)
             *    == x0B + (y - y0B) (x1B - x0B) / (y1B - y0B)
             * 0 == x0A (y1A - y0A) (y1B - y0B) + (y - y0A) (x1A - x0A) (y1B - y0B)
             *    - x0B (y1A - y0A) (y1B - y0B) - (y - y0B) (x1B - x0B) (y1A - y0A)
             * 0 == (x0A - x0B) (y1A - y0A) (y1B - y0B)
             *    + (y - y0A) (x1A - x0A) (y1B - y0B)
             *    - (y - y0B) (x1B - x0B) (y1A - y0A)
             * If (dxA == x1A - x0A), etc...
             * 0 == (x0A - x0B) * dyA * dyB
             *    + (y - y0A) * dxA * dyB
             *    - (y - y0B) * dxB * dyA
             * 0 == (x0A - x0B) * dyA * dyB
             *    + y * dxA * dyB - y0A * dxA * dyB
             *    - y * dxB * dyA + y0B * dxB * dyA
             * 0 == (x0A - x0B) * dyA * dyB
             *    + y * dxA * dyB - y * dxB * dyA
             *    - y0A * dxA * dyB + y0B * dxB * dyA
             * 0 == (x0A - x0B) * dyA * dyB
             *    + y * (dxA * dyB - dxB * dyA)
             *    - y0A * dxA * dyB + y0B * dxB * dyA
             * y == ((x0A - x0B) * dyA * dyB
             *       - y0A * dxA * dyB + y0B * dxB * dyA)
             *    / (-(dxA * dyB - dxB * dyA))
             * y == ((x0A - x0B) * dyA * dyB
             *       - y0A * dxA * dyB + y0B * dxB * dyA)
             *    / (dxB * dyA - dxA * dyB)
             */
            double dxa = _x1 - _x0;
            double dya = _y1 - _y0;
            double dxb = c1._x1 - c1._x0;
            double dyb = c1._y1 - c1._y0;
            double denom = dxb * dya - dxa * dyb;
            double y;
            if (denom != 0)
            {
                double num = ((_x0 - c1._x0) * dya * dyb - _y0 * dxa * dyb + c1._y0 * dxb * dya);
                y = num / denom;
                if (y <= yrange[0])
                {
                    // intersection is above us
                    // Use bottom-most common y for comparison
                    y = Math.Min(_y1, c1._y1);
                }
                else
                {
                    // intersection is below the top of our range
                    if (y < yrange[1])
                    {
                        // If intersection is in our range, adjust valid range
                        yrange[1] = y;
                    }
                    // Use top-most common y for comparison
                    y = Math.Max(_y0, c1._y0);
                }
            }
            else
            {
                // lines are parallel, choose any common y for comparison
                // Note - prefer an endpoint for speed of calculating the X
                // (see shortcuts in Order1.XforY())
                y = Math.Max(_y0, c1._y0);
            }
            return Orderof(XforY(y), c1.XforY(y));
        }

        public override int GetSegment(double[] coords)
        {
            if (_direction == INCREASING)
            {
                coords[0] = _x1;
                coords[1] = _y1;
            }
            else
            {
                coords[0] = _x0;
                coords[1] = _y0;
            }
            return PathIterator.SEG_LINETO;
        }
    }

}
