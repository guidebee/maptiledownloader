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
     * a 2D point class.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class PointFP
    {

        /**
         * X coordinate.
         */
        public int X;

        /**
         * Y coordinate.
         */
        public int Y;

        /**
         * the (0,0) point.
         */
        public static readonly PointFP Origin = new PointFP(0, 0);

        /**
         * Empty point.
         */
        public static readonly PointFP Empty = new PointFP(SingleFP.NOT_A_NUMBER, SingleFP.NOT_A_NUMBER);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public PointFP()
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
         * @param p
         */
        public PointFP(PointFP p)
        {
            Reset(p);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param ff_x
         * @param ff_y
         */
        public PointFP(int ffX, int ffY)
        {
            Reset(ffX, ffY);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see if the point is empty one.
         * @param p
         * @return
         */
        public static bool IsEmpty(PointFP p)
        {
            return Empty.Equals(p);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the point to the same location as the given point.
         * @param p
         * @return
         */
        public PointFP Reset(PointFP p)
        {
            return Reset(p.X, p.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the point to give location.
         * @param ff_x
         * @param ff_y
         * @return
         */
        public PointFP Reset(int ffX, int ffY)
        {
            X = ffX;
            Y = ffY;
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * transform the point with give matrix.
         * @param m
         * @return
         */
        public PointFP Transform(MatrixFP m)
        {
            Reset(MathFP.Mul(X, m.ScaleX) + MathFP.Mul(Y, m.RotateY) + m.TranslateX,
                    MathFP.Mul(Y, m.ScaleY) + MathFP.Mul(X, m.RotateX) + m.TranslateY);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * the distance between 2 points.
         * @param p1
         * @param p2
         * @return
         */
        static public int Distance(PointFP p1, PointFP p2)
        {
            return Distance(p1.X - p2.X, p1.Y - p2.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculate the distance.
         * @param dx
         * @param dy
         * @return
         */
        static public int Distance(int dx, int dy)
        {
            dx = MathFP.Abs(dx);
            dy = MathFP.Abs(dy);
            if (dx == 0)
            {
                return dy;
            }
            if (dy == 0)
            {
                return dx;
            }

            var len = (((long)dx * dx) >> SingleFP.DECIMAL_BITS)
                    + (((long)dy * dy) >> SingleFP.DECIMAL_BITS);
            long s = (dx + dy) - (MathFP.Min(dx, dy) >> 1);
            s = (s + ((len << SingleFP.DECIMAL_BITS) / s)) >> 1;
            s = (s + ((len << SingleFP.DECIMAL_BITS) / s)) >> 1;
            return (int)s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add given point the location to this point.
         * @param p
         * @return
         */
        public PointFP Add(PointFP p)
        {
            Reset(X + p.X, Y + p.Y);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * substract given distance (x,y) to this point.
         * @param p
         * @return
         */
        public PointFP Sub(PointFP p)
        {
            Reset(X - p.X, Y - p.Y);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see the two point are equal.
         * @param obj
         * @return
         */
        public new bool Equals(object obj)
        {
            if (obj is PointFP)
            {
                var p = (PointFP)obj;
                return X == p.X && Y == p.Y;
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the hashcode for this <code>Point</code>.
         * @return      a hash code for this <code>Point</code>.
         */
        public int HashCode()
        {
            var bits = (int)((X << 16) & 0xFFFF0000);
            bits ^= Y;
            return bits;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * convert to a string.
         * @return
         */
        public override string ToString()
        {
            return "Point(" + new SingleFP(X) + "," + new SingleFP(Y) + ")";
        }
    }

}
