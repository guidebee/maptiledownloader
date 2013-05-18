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
     * The <code>RoundRectangle</code> class defines a rectangle with rounded
     * corners all specified in <code>long</code> coordinates.
      * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class RoundRectangle : RectangularShape
    {

        /**
         * The X coordinate of this <code>RoundRectangle</code>.
         */
        public double X;

        /**
         * The Y coordinate of this <code>RoundRectangle</code>.
         */
        public double Y;

        /**
         * The width of this <code>RoundRectangle</code>.
         */
        public double Width;

        /**
         * The height of this <code>RoundRectangle</code>.
         */
        public double Height;

        /**
         * The width of the arc that rounds off the corners.
         */
        public double Arcwidth;

        /**
         * The height of the arc that rounds off the corners.
         */
        public double Archeight;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>RoundRectangle</code>, initialized to
         * location (0.0,&nbsp;0), size (0.0,&nbsp;0.0), and corner arcs
         * of radius 0.0.
         */
        public RoundRectangle()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>RoundRectangle</code>
         * from the specified <code>double</code> coordinates.
         *
         * @param x the X coordinate of the newly
         *          constructed <code>RoundRectangle</code>
         * @param y the Y coordinate of the newly
         *          constructed <code>RoundRectangle</code>
         * @param w the width to which to set the newly
         *          constructed <code>RoundRectangle</code>
         * @param h the height to which to set the newly
         *          constructed <code>RoundRectangle</code>
         * @param arcw the width of the arc to use to Round off the
         *             corners of the newly constructed
         *             <code>RoundRectangle</code>
         * @param arch the height of the arc to use to Round off the
         *             corners of the newly constructed
         *             <code>RoundRectangle</code>
         */
        public RoundRectangle(double x, double y, double w, double h,
                      double arcw, double arch)
        {
            SetRoundRect(x, y, w, h, arcw, arch);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override int GetX()
        {
            return (int)(X + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override int GetY()
        {
            return (int)(Y + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override int GetWidth()
        {
            return (int)(Width + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override int GetHeight()
        {
            return (int)(Height + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the width of the arc that rounds off the corners.
         * @return the width of the arc that rounds off the corners
         * of this <code>RoundRectangle2D</code>.
         */
        public double GetArcWidth()
        {
            return Arcwidth;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the height of the arc that rounds off the corners.
         * @return the height of the arc that rounds off the corners
         * of this <code>RoundRectangle2D</code>.
         */
        public double GetArcHeight()
        {
            return Archeight;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override bool IsEmpty()
        {
            return (Width <= 0.0f) || (Height <= 0.0f);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location, size, and corner radii of this
         * <code>RoundRectangle2D</code> to the specified
         * <code>double</code> values.
         *
         * @param x the X coordinate to which to set the
         *          location of this <code>RoundRectangle2D</code>
         * @param y the Y coordinate to which to set the
         *          location of this <code>RoundRectangle2D</code>
         * @param w the width to which to set this
         *          <code>RoundRectangle2D</code>
         * @param h the height to which to set this
         *          <code>RoundRectangle2D</code>
         * @param arcw the width to which to set the arc of this
         *                 <code>RoundRectangle2D</code>
         * @param arch the height to which to set the arc of this
         *                  <code>RoundRectangle2D</code>
         */
        public void SetRoundRect(double x, double y, double w, double h,
                                 double arcw, double arch)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Arcwidth = arcw;
            Archeight = arch;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this <code>RoundRectangle2D</code> to be the same as the
         * specified <code>RoundRectangle2D</code>.
         * @param rr the specified <code>RoundRectangle2D</code>
         */
        public void SetRoundRect(RoundRectangle rr)
        {
            X = rr.GetX();
            Y = rr.GetY();
            Width = rr.GetWidth();
            Height = rr.GetHeight();
            Arcwidth = rr.GetArcWidth();
            Archeight = rr.GetArcHeight();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override Rectangle GetBounds()
        {
            return new Rectangle(
                    (int)(X + .5),
                    (int)(Y + .5),
                    (int)(Width + .5),
                    (int)(Height + .5));
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override void SetFrame(int x, int y, int w, int h)
        {
            SetRoundRect(x, y, w, h, GetArcWidth(), GetArcHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override bool Contains(int x, int y)
        {
            if (IsEmpty())
            {
                return false;
            }
            double rrx0 = GetX();
            double rry0 = GetY();
            double rrx1 = rrx0 + GetWidth();
            double rry1 = rry0 + GetHeight();
            // Check for trivial rejection - point is outside bounding rectangle
            if (x < rrx0 || y < rry0 || x >= rrx1 || y >= rry1)
            {
                return false;
            }
            double aw = Math.Min(GetWidth(), Math.Abs(GetArcWidth())) / 2.0;
            double ah = Math.Min(GetHeight(), Math.Abs(GetArcHeight())) / 2.0;
            // Check which corner point is in and do circular containment
            // test - otherwise simple acceptance
            if (x >= (rrx0 += aw) && x < (rrx0 = rrx1 - aw))
            {
                return true;
            }
            if (y >= (rry0 += ah) && y < (rry0 = rry1 - ah))
            {
                return true;
            }
            double xp = (x - rrx0) / aw;
            double yp = (y - rry0) / ah;
            return (xp * xp + yp * yp <= 1.0);
        }




        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override bool Intersects(int x, int y, int w, int h)
        {
            if (IsEmpty() || w <= 0 || h <= 0)
            {
                return false;
            }
            double rrx0 = GetX();
            double rry0 = GetY();
            double rrx1 = rrx0 + GetWidth();
            double rry1 = rry0 + GetHeight();
            // Check for trivial rejection - bounding rectangles do not intersect
            if (x + w <= rrx0 || x >= rrx1 || y + h <= rry0 || y >= rry1)
            {
                return false;
            }
            double aw = Math.Min(GetWidth(), Math.Abs(GetArcWidth())) / 2.0;
            double ah = Math.Min(GetHeight(), Math.Abs(GetArcHeight())) / 2.0;
            int x0Class = Classify(x, rrx0, rrx1, aw);
            int x1Class = Classify(x + w, rrx0, rrx1, aw);
            int y0Class = Classify(y, rry0, rry1, ah);
            int y1Class = Classify(y + h, rry0, rry1, ah);
            // Trivially accept if any point is inside inner rectangle
            if (x0Class == 2 || x1Class == 2 || y0Class == 2 || y1Class == 2)
            {
                return true;
            }
            // Trivially accept if either edge spans inner rectangle
            if ((x0Class < 2 && x1Class > 2) || (y0Class < 2 && y1Class > 2))
            {
                return true;
            }
            // Since neither edge spans the center, then one of the corners
            // must be in one of the rounded edges.  We detect this case if
            // a [xy]0class is 3 or a [xy]1class is 1.  One of those two cases
            // must be true for each direction.
            // We now find a "nearest point" to test for being inside a rounded
            // corner.
            double xp = x; double yp = y;
            xp = (x1Class == 1) ? (xp + w - (rrx0 + aw)) : (xp - (rrx1 - aw));
            yp = (y1Class == 1) ? (yp + h - (rry0 + ah)) : (yp - (rry1 - ah));
            xp = xp / aw;
            yp = yp / ah;
            return (xp * xp + yp * yp <= 1.0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override bool Contains(int x, int y, int w, int h)
        {
            if (IsEmpty() || w <= 0 || h <= 0)
            {
                return false;
            }
            return (Contains(x, y) &&
                Contains(x + w, y) &&
                Contains(x, y + h) &&
                Contains(x + w, y + h));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of this
         * <code>RoundRectangle</code>.
         * The iterator for this class is multi-threaded safe, which means
         * that this <code>RoundRectangle</code> class guarantees that
         * modifications to the geometry of this <code>RoundRectangle</code>
         * object do not affect any iterations of that geometry that
         * are already in process.
         * @param at an optional <code>AffineTransform</code> to be applied to
         * the coordinates as they are returned in the iteration, or
         * <code>null</code> if untransformed coordinates are desired
         * @return    the <code>PathIterator</code> object that returns the
         *          geometry of the outline of this
         *          <code>RoundRectangle</code>, one segment at a time.
         */
        public override PathIterator GetPathIterator(AffineTransform at)
        {
            return new RoundRectIterator(this, at);
        }




        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified <code>Object</code> is
         * equal to this <code>RoundRectangle</code>.  The specified
         * <code>Object</code> is equal to this <code>RoundRectangle</code>
         * if it is an instance of <code>RoundRectangle</code> and if its
         * location, size, and corner arc dimensions are the same as this
         * <code>RoundRectangle</code>.
         * @param obj  an <code>Object</code> to be compared with this
         *             <code>RoundRectangle</code>.
         * @return  <code>true</code> if <code>obj</code> is an instance
         *          of <code>RoundRectangle</code> and has the same values;
         *          <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is RoundRectangle)
            {
                RoundRectangle rr2D = (RoundRectangle)obj;
                return ((GetX() == rr2D.GetX()) &&
                        (GetY() == rr2D.GetY()) &&
                        (GetWidth() == rr2D.GetWidth()) &&
                        (GetHeight() == rr2D.GetHeight()) &&
                        (GetArcWidth() == rr2D.GetArcWidth()) &&
                        (GetArcHeight() == rr2D.GetArcHeight()));
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private static int Classify(double coord, double left, double right,
                             double arcsize)
        {
            if (coord < left)
            {
                return 0;
            }
            if (coord < left + arcsize)
            {
                return 1;
            }
            if (coord < right - arcsize)
            {
                return 2;
            }
            if (coord < right)
            {
                return 3;
            }
            return 4;
        }
    }
}
