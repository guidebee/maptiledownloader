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
namespace MapDigit.Drawing.Geometry
{
    internal class Order0 : Curve
    {

        private readonly double _x;
        private readonly double _y;

        public Order0(double x, double y)
            : base(INCREASING)
        {
            _x = x;
            _y = y;
        }

        public override int GetOrder()
        {
            return 0;
        }

        public override double GetXTop()
        {
            return _x;
        }

        public override double GetYTop()
        {
            return _y;
        }

        public override double GetXBot()
        {
            return _x;
        }

        public override double GetYBot()
        {
            return _y;
        }

        public override double GetXMin()
        {
            return _x;
        }

        public override double GetXMax()
        {
            return _x;
        }

        public override double GetX0()
        {
            return _x;
        }

        public override double GetY0()
        {
            return _y;
        }

        public override double GetX1()
        {
            return _x;
        }

        public override double GetY1()
        {
            return _y;
        }

        public override double XforY(double y)
        {
            return y;
        }

        public override double TforY(double y)
        {
            return 0;
        }

        public override double XforT(double t)
        {
            return _x;
        }

        public override double YforT(double t)
        {
            return _y;
        }

        public override double DXforT(double t, int deriv)
        {
            return 0;
        }

        public override double DYforT(double t, int deriv)
        {
            return 0;
        }

        public override double NextVertical(double t0, double t1)
        {
            return t1;
        }

        public override int  CrossingsFor(double x, double y)
        {
            return 0;
        }

        public override bool AccumulateCrossings(Crossings c)
        {
            return (_x > c.GetXLo() &&
                    _x < c.GetXHi() &&
                    _y > c.GetYLo() &&
                    _y < c.GetYHi());
        }

        public override void Enlarge(Rectangle r)
        {
            r.Add((int)(_x + 0.5), (int)(_y + 0.5));
        }

        public override Curve GetSubCurve(double ystart, double yend, int dir)
        {
            return this;
        }

        public override Curve GetReversedCurve()
        {
            return this;
        }

        public override int GetSegment(double[] coords)
        {
            coords[0] = _x;
            coords[1] = _y;
            return PathIterator.SEG_MOVETO;
        }
    }

}
