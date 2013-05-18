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
     * a 2D Line class.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class LineFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the lenght of the line.
         */
        public int GetLength()
        {
            return PointFP.Distance(Pt1, Pt2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the center of the line.
         * @return
         */
        public PointFP GetCenter()
        {
            return new PointFP((Pt1.X + Pt2.X) / 2, (Pt1.Y + Pt2.Y) / 2);
        }

        /**
         * start point the line.
         */
        public PointFP Pt1 = new PointFP(0, 0);

        /**
         * end point of the line.
         */
        public PointFP Pt2 = new PointFP(0, 0);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public LineFP()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param l
         */
        public LineFP(LineFP l)
        {
            Reset(l.Pt1, l.Pt2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param p1
         * @param p2
         */
        public LineFP(PointFP p1, PointFP p2)
        {
            Reset(p1, p2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param ff_x1
         * @param ff_y1
         * @param ff_x2
         * @param ff_y2
         */
        public LineFP(int ffX1, int ffY1, int ffX2, int ffY2)
        {
            Reset(ffX1, ffY1, ffX2, ffY2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the line the same location as given line.
         * @param l
         */
        public void Reset(LineFP l)
        {
            Reset(l.Pt1, l.Pt2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the line the to given points.
         * @param p1
         * @param p2
         */
        public void Reset(PointFP p1, PointFP p2)
        {
            Pt1.Reset(p1);
            Pt2.Reset(p2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the line to given coorindate.
         * @param ff_x1
         * @param ff_y1
         * @param ff_x2
         * @param ff_y2
         */
        public void Reset(int ffX1, int ffY1, int ffX2, int ffY2)
        {
            Pt1.Reset(ffX1, ffY1);
            Pt2.Reset(ffX2, ffY2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the head outline.
         * @param ff_rad
         * @return
         */
        public LineFP GetHeadOutline(int ffRad)
        {
            var p = new PointFP(Pt1.X - Pt2.X, Pt1.Y - Pt2.Y);
            var len = GetLength();
            p.Reset(MathFP.Div(-p.Y, len), MathFP.Div(p.X, len));
            p.Reset(MathFP.Mul(p.X, ffRad), MathFP.Mul(p.Y, ffRad));
            return new LineFP(Pt1.X - p.X, Pt1.Y - p.Y, Pt1.X + p.X, Pt1.Y + p.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the tail outline.
         * @param ff_rad
         * @return
         */
        public LineFP GetTailOutline(int ffRad)
        {
            var c = GetCenter();
            var p = new PointFP(Pt2.X - c.X, Pt2.Y - c.Y);
            p.Reset(p.Y, -p.X);
            var dis = PointFP.Distance(PointFP.Origin, p);
            if (dis == 0)
            {
                dis = 1;
            }
            p.Reset(MathFP.Div(MathFP.Mul(p.X, ffRad), dis),
                    MathFP.Div(MathFP.Mul(p.Y, ffRad), dis));
            return new LineFP(Pt2.X - p.X, Pt2.Y - p.Y, Pt2.X + p.X, Pt2.Y + p.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see the Value are equal.
         * @param ff_val1
         * @param ff_val2
         * @return
         */
        private static bool IsEqual(int ffVal1, int ffVal2)
        {
            return IsZero(ffVal1 - ffVal2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see a Value is zero.
         * @param ff_val
         * @return
         */
        private static bool IsZero(int ffVal)
        {
            return MathFP.Abs(ffVal) < (1 << SingleFP.DECIMAL_BITS / 2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check to see if two line intects and return the the intersction point.
         * @param l1
         * @param l2
         * @param intersection
         * @return
         */
        public static bool Intersects(LineFP l1, LineFP l2, PointFP intersection)
        {
            var x = SingleFP.NOT_A_NUMBER;
            var y = SingleFP.NOT_A_NUMBER;

            if (intersection != null)
            {
                intersection.Reset(x, y);
            }

            var ax0 = l1.Pt1.X;
            var ax1 = l1.Pt2.X;
            var ay0 = l1.Pt1.Y;
            var ay1 = l1.Pt2.Y;
            var bx0 = l2.Pt1.X;
            var bx1 = l2.Pt2.X;
            var by0 = l2.Pt1.Y;
            var by1 = l2.Pt2.Y;

            var adx = (ax1 - ax0);
            var ady = (ay1 - ay0);
            var bdx = (bx1 - bx0);
            var bdy = (by1 - by0);

            if (IsZero(adx) && IsZero(bdx))
            {
                return IsEqual(ax0, bx0);
            }
            if (IsZero(ady) && IsZero(bdy))
            {
                return IsEqual(ay0, by0);
            }
            if (IsZero(adx))
            {
                // A  vertical
                x = ax0;
                y = IsZero(bdy) ? by0 : MathFP.Mul(MathFP.Div(bdy, bdx), x - bx0) + by0;
            }
            else if (IsZero(bdx))
            {
                // B vertical
                x = bx0;
                y = IsZero(ady) ? ay0 : MathFP.Mul(MathFP.Div(ady, adx), x - ax0) + ay0;
            }
            else if (IsZero(ady))
            {
                y = ay0;
                x = MathFP.Mul(MathFP.Div(bdx, bdy), y - by0) + bx0;
            }
            else if (IsZero(bdy))
            {
                y = by0;
                x = MathFP.Mul(MathFP.Div(adx, ady), y - ay0) + ax0;
            }
            else
            {
                var xma = MathFP.Div(ady, adx); // slope segment A
                var xba = ay0 - (MathFP.Mul(ax0, xma)); // y intercept of segment A

                var xmb = MathFP.Div(bdy, bdx); // slope segment B
                var xbb = by0 - (MathFP.Mul(bx0, xmb)); // y intercept of segment B

                // parallel lines?
                if (xma == xmb)
                {
                    // Need trig functions
                    return xba == xbb;
                }
                // Calculate points of intersection
                // At the intersection of line segment A and B,
                //XA=XB=XINT and YA=YB=YINT
                x = MathFP.Div((xbb - xba), (xma - xmb));
                y = (MathFP.Mul(xma, x)) + xba;
            }

            // After the point or points of intersection are calculated, each
            // solution must be checked to ensure that the point of intersection lies
            // on line segment A and B.

            var minxa = MathFP.Min(ax0, ax1);
            var maxxa = MathFP.Max(ax0, ax1);

            var minya = MathFP.Min(ay0, ay1);
            var maxya = MathFP.Max(ay0, ay1);

            var minxb = MathFP.Min(bx0, bx1);
            var maxxb = MathFP.Max(bx0, bx1);

            var minyb = MathFP.Min(by0, by1);
            var maxyb = MathFP.Max(by0, by1);

            if (intersection != null)
            {
                intersection.Reset(x, y);
            }
            return ((x >= minxa) && (x <= maxxa) && (y >= minya) && (y <= maxya)
                    && (x >= minxb) && (x <= maxxb) && (y >= minyb) && (y <= maxyb));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param distance
         * @return
         */
        internal PointFP GetPointAtDistance(int distance)
        {
            var lineLength = GetLength();
            if (distance > lineLength)
            {
                return null;
            }
            if (distance == lineLength)
            {
                return new PointFP(Pt2);
            }
            var scale = MathFP.Div(distance, lineLength);
            var pointFP = new PointFP();
            pointFP.Reset(Pt1.X + MathFP.Mul(Pt2.X - Pt1.X, scale),
                          Pt1.Y + MathFP.Mul(Pt2.Y - Pt1.Y, scale));
            return pointFP;
        }
    }

}
