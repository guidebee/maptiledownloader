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
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * A line segment specified with int coordinates.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Line : IShape
    {

        /**
         * The X coordinate of the start point of the line segment.
         */
        public int X1;

        /**
         * The Y coordinate of the start point of the line segment.
         */
        public int Y1;

        /**
         * The X coordinate of the end point of the line segment.
         */
        public int X2;

        /**
         * The Y coordinate of the end point of the line segment.
         */
        public int Y2;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a Line with coordinates (0, 0) -> (0, 0).
         */
        public Line()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>Line</code> from the
         * specified coordinates.
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         */
        public Line(int x1, int y1, int x2, int y2)
        {
            SetLine(x1, y1, x2, y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>Line</code> from the
         * specified <code>Point</code> objects.
         * @param p1 the start <code>Point</code> of this line segment
         * @param p2 the end <code>Point</code> of this line segment
         */
        public Line(Point p1, Point p2)
        {
            SetLine(p1, p2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the start point in integer.
         * @return the X coordinate of the start point of this
         *         {@code Line} object.
         */
        public int GetX1()
        {
            return X1;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the start point in integer.
         * @return the Y coordinate of the start point of this
         *         {@code Line} object.
         */
        public int GetY1()
        {
            return Y1;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the end point.
         * @return the X coordinate of the end point of this
         *         {@code Line} object.
         */
        public int GetX2()
        {
            return X2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the end point.
         * @return the Y coordinate of the end point of this
         *         {@code Line} object.
         */
        public int GetY2()
        {
            return Y2;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the start <code>Point</code> of this <code>Line</code>.
         * @return the start <code>Point</code> of this <code>Line</code>.
         */
        public Point GetP1()
        {
            return new Point(X1, Y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the end <code>Point</code> of this <code>Line</code>.
         * @return the end <code>Point</code> of this <code>Line</code>.
         */
        public Point GetP2()
        {
            return new Point(X2, Y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points of this <code>Line</code> to
         * the specified coordinates.
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         */
        public void SetLine(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points of this <code>Line</code> to
         * the specified <code>Point</code> coordinates.
         * @param p1 the start <code>Point</code> of the line segment
         * @param p2 the end <code>Point</code> of the line segment
         */
        public void SetLine(Point p1, Point p2)
        {
            SetLine(p1.X, p1.Y, p2.X, p2.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points of this <code>Line</code> to
         * the same as those end points of the specified <code>Line</code>.
         * @param l the specified <code>Line</code>
         */
        public void SetLine(Line l)
        {
            SetLine(l.X1, l.Y1, l.X2, l.Y2);
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
        public Rectangle GetBounds()
        {
            int x, y, w, h;
            if (X1 < X2)
            {
                x = X1;
                w = X2 - X1;
            }
            else
            {
                x = X2;
                w = X1 - X2;
            }
            if (Y1 < Y2)
            {
                y = Y1;
                h = Y2 - Y1;
            }
            else
            {
                y = Y2;
                h = Y1 - Y2;
            }
            return new Rectangle(x, y, w, h);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an indicator of where the specified point
         * {@code (px,py)} lies with respect to the line segment from
         * {@code (x1,y1)} to {@code (X2,y2)}.
         * The return Value can be either 1, -1, or 0 and indicates
         * in which direction the specified line must pivot around its
         * first end point, {@code (x1,y1)}, in order to point at the
         * specified point {@code (px,py)}.
         * <p>A return Value of 1 indicates that the line segment must
         * turn in the direction that takes the positive X axis towards
         * the negative Y axis.  In the default coordinate system used by
         * Java 2D, this direction is counterclockwise.
         * <p>A return Value of -1 indicates that the line segment must
         * turn in the direction that takes the positive X axis towards
         * the positive Y axis.  In the default coordinate system, this
         * direction is clockwise.
         * <p>A return Value of 0 indicates that the point lies
         * exactly on the line segment.  Note that an indicator Value
         * of 0 is rare and not useful for determining colinearity
         * because of floating point rounding issues.
         * <p>If the point is colinear with the line segment, but
         * not between the end points, then the Value will be -1 if the point
         * lies "beyond {@code (x1,y1)}" or 1 if the point lies
         * "beyond {@code (X2,y2)}".
         *
         * @param x1 the X coordinate of the start point of the
         *           specified line segment
         * @param y1 the Y coordinate of the start point of the
         *           specified line segment
         * @param X2 the X coordinate of the end point of the
         *           specified line segment
         * @param y2 the Y coordinate of the end point of the
         *           specified line segment
         * @param px the X coordinate of the specified point to be
         *           compared with the specified line segment
         * @param py the Y coordinate of the specified point to be
         *           compared with the specified line segment
         * @return an integer that indicates the position of the third specified
         *			coordinates with respect to the line segment formed
         *			by the first two specified coordinates.
         */
        public static int RelativeCCW(int x1, int y1,
                      int x2, int y2,
                      int px, int py)
        {
            x2 -= x1;
            y2 -= y1;
            px -= x1;
            py -= y1;
            int ccw = px * y2 - py * x2;
            if (ccw == 0)
            {
                // The point is colinear, classify based on which side of
                // the segment the point falls on.  We can calculate a
                // relative Value using the projection of px,py onto the
                // segment - a negative Value indicates the point projects
                // outside of the segment in the direction of the particular
                // endpoint used as the origin for the projection.
                ccw = px * x2 + py * y2;
                if (ccw > 0)
                {
                    // Reverse the projection to be relative to the original X2,y2
                    // X2 and y2 are simply negated.
                    // px and py need to have (X2 - x1) or (y2 - y1) subtracted
                    //    from them (based on the original values)
                    // Since we really want to get a positive answer when the
                    //    point is "beyond (X2,y2)", then we want to calculate
                    //    the inverse anyway - thus we leave X2 & y2 negated.
                    px -= x2;
                    py -= y2;
                    ccw = px * x2 + py * y2;
                    if (ccw < 0)
                    {
                        ccw = 0;
                    }
                }
            }
            return (ccw < 0) ? -1 : ((ccw > 0) ? 1 : 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an indicator of where the specified point
         * {@code (px,py)} lies with respect to this line segment.
         * See the method comments of
         * {@link #relativeCCW(int, int, int, int, int, int)}
         * to interpret the return Value.
         * @param px the X coordinate of the specified point
         *           to be compared with this <code>Line</code>
         * @param py the Y coordinate of the specified point
         *           to be compared with this <code>Line</code>
         * @return an integer that indicates the position of the specified
         *         coordinates with respect to this <code>Line</code>
         */
        public int RelativeCCW(int px, int py)
        {
            return RelativeCCW(X1, Y1, X2, Y2, px, py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an indicator of where the specified <code>Point</code>
         * lies with respect to this line segment.
         * See the method comments of
         * {@link #relativeCCW(int, int, int, int, int, int)}
         * to interpret the return Value.
         * @param p the specified <code>Point</code> to be compared
         *          with this <code>Line</code>
         * @return an integer that indicates the position of the specified
         *         <code>Point</code> with respect to this <code>Line</code>
         */
        public int RelativeCCW(Point p)
        {
            return RelativeCCW(X1, Y1, X2, Y2,
                       p.X, p.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the line segment from {@code (x1,y1)} to
         * {@code (X2,y2)} intersects the line segment from {@code (x3,y3)}
         * to {@code (x4,y4)}.
         *
         * @param x1 the X coordinate of the start point of the first
         *           specified line segment
         * @param y1 the Y coordinate of the start point of the first
         *           specified line segment
         * @param X2 the X coordinate of the end point of the first
         *           specified line segment
         * @param y2 the Y coordinate of the end point of the first
         *           specified line segment
         * @param x3 the X coordinate of the start point of the second
         *           specified line segment
         * @param y3 the Y coordinate of the start point of the second
         *           specified line segment
         * @param x4 the X coordinate of the end point of the second
         *           specified line segment
         * @param y4 the Y coordinate of the end point of the second
         *           specified line segment
         * @return <code>true</code> if the first specified line segment
         *			and the second specified line segment intersect
         *			each other; <code>false</code> otherwise.
         */
        public static bool LinesIntersect(int x1, int y1,
                         int x2, int y2,
                         int x3, int y3,
                         int x4, int y4)
        {
            return ((RelativeCCW(x1, y1, x2, y2, x3, y3) *
                 RelativeCCW(x1, y1, x2, y2, x4, y4) <= 0)
                && (RelativeCCW(x3, y3, x4, y4, x1, y1) *
                    RelativeCCW(x3, y3, x4, y4, x2, y2) <= 0));
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the line segment from {@code (x1,y1)} to
         * {@code (X2,y2)} intersects this line segment.
         *
         * @param x1 the X coordinate of the start point of the
         *           specified line segment
         * @param y1 the Y coordinate of the start point of the
         *           specified line segment
         * @param X2 the X coordinate of the end point of the
         *           specified line segment
         * @param y2 the Y coordinate of the end point of the
         *           specified line segment
         * @return <true> if this line segment and the specified line segment
         *			intersect each other; <code>false</code> otherwise.
         */
        public bool IntersectsLine(int x1, int y1, int x2, int y2)
        {
            return LinesIntersect(x1, y1, x2, y2,
                          X1, Y1, X2, Y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified line segment intersects this line segment.
         * @param l the specified <code>Line</code>
         * @return <code>true</code> if this line segment and the specified line
         *			segment intersect each other;
         *			<code>false</code> otherwise.
         */
        public bool IntersectsLine(Line l)
        {
            return LinesIntersect(l.X1, l.Y1, l.X2, l.Y2,
                          X1, Y1, X2, Y2);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a point to a line segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the specified end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.
         *
         * @param x1 the X coordinate of the start point of the
         *           specified line segment
         * @param y1 the Y coordinate of the start point of the
         *           specified line segment
         * @param X2 the X coordinate of the end point of the
         *           specified line segment
         * @param y2 the Y coordinate of the end point of the
         *           specified line segment
         * @param px the X coordinate of the specified point being
         *           measured against the specified line segment
         * @param py the Y coordinate of the specified point being
         *           measured against the specified line segment
         * @return a int Value that is the square of the distance from the
         *			specified point to the specified line segment.
         */
        public static int PtSegDistSq(int x1, int y1,
                         int x2, int y2,
                         int px, int py)
        {
            // Adjust vectors relative to x1,y1
            // X2,y2 becomes relative vector from x1,y1 to end of segment
            x2 -= x1;
            y2 -= y1;
            // px,py becomes relative vector from x1,y1 to test point
            px -= x1;
            py -= y1;
            int dotprod = px * x2 + py * y2;
            int projlenSq;
            if (dotprod <= 0)
            {
                // px,py is on the side of x1,y1 away from X2,y2
                // distance to segment is length of px,py vector
                // "length of its (clipped) projection" is now 0.0
                projlenSq = 0;
            }
            else
            {
                // switch to backwards vectors relative to X2,y2
                // X2,y2 are already the negative of x1,y1=>X2,y2
                // to get px,py to be the negative of px,py=>X2,y2
                // the dot product of two negated vectors is the same
                // as the dot product of the two normal vectors
                px = x2 - px;
                py = y2 - py;
                dotprod = px * x2 + py * y2;
                if (dotprod <= 0)
                {
                    // px,py is on the side of X2,y2 away from x1,y1
                    // distance to segment is length of (backwards) px,py vector
                    // "length of its (clipped) projection" is now 0.0
                    projlenSq = 0;
                }
                else
                {
                    // px,py is between x1,y1 and X2,y2
                    // dotprod is the length of the px,py vector
                    // projected on the X2,y2=>x1,y1 vector times the
                    // length of the X2,y2=>x1,y1 vector
                    projlenSq = dotprod * dotprod / (x2 * x2 + y2 * y2);
                }
            }
            // Distance to line is now the length of the relative point
            // vector minus the length of its projection onto the line
            // (which is zero if the projection falls outside the range
            //  of the line segment).
            int lenSq = px * px + py * py - projlenSq;
            if (lenSq < 0)
            {
                lenSq = 0;
            }
            return lenSq;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a point to a line segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the specified end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.
         *
         * @param x1 the X coordinate of the start point of the
         *           specified line segment
         * @param y1 the Y coordinate of the start point of the
         *           specified line segment
         * @param X2 the X coordinate of the end point of the
         *           specified line segment
         * @param y2 the Y coordinate of the end point of the
         *           specified line segment
         * @param px the X coordinate of the specified point being
         *           measured against the specified line segment
         * @param py the Y coordinate of the specified point being
         *           measured against the specified line segment
         * @return a int Value that is the distance from the specified point
         *				to the specified line segment.
         */
        public static int PtSegDist(int x1, int y1,
                       int x2, int y2,
                       int px, int py)
        {
            long dis = PtSegDistSq(x1, y1, x2, y2, px, py);
            dis <<= MathFP.DEFAULT_PRECISION;
            return MathFP.ToInt(MathFP.Sqrt(dis));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a point to this line segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the current line's end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.0.
         *
         * @param px the X coordinate of the specified point being
         *           measured against this line segment
         * @param py the Y coordinate of the specified point being
         *           measured against this line segment
         * @return a int Value that is the distance from the specified
         *			point to the current line segment.
         */
        public int PtSegDist(int px, int py)
        {
            return PtSegDist(X1, Y1, X2, Y2, px, py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a <code>Point</code> to this line
         * segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the current line's end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.
         * @param pt the specified <code>Point</code> being measured
         *		against this line segment
         * @return a int Value that is the distance from the specified
         *				<code>Point</code> to the current line
         *				segment.
         */
        public int PtSegDist(Point pt)
        {
            return PtSegDist(X1, Y1, X2, Y2,
                     pt.X, pt.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a point to this line segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the current line's end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.0.
         *
         * @param px the X coordinate of the specified point being
         *           measured against this line segment
         * @param py the Y coordinate of the specified point being
         *           measured against this line segment
         * @return a int Value that is the square of the distance from the
         *			specified point to the current line segment.
         */
        public int PtSegDistSq(int px, int py)
        {
            return PtSegDistSq(X1, Y1, X2, Y2, px, py);
        }




        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a <code>Point</code> to
         * this line segment.
         * The distance measured is the distance between the specified
         * point and the closest point between the current line's end points.
         * If the specified point intersects the line segment in between the
         * end points, this method returns 0.0.
         * @param pt the specified <code>Point</code> being measured against
         *	         this line segment.
         * @return a int Value that is the square of the distance from the
         *			specified <code>Point</code> to the current
         *			line segment.
         */
        public int PtSegDistSq(Point pt)
        {
            return PtSegDistSq(X1, Y1, X2, Y2,
                       pt.X, pt.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a point to a line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by the specified coordinates.  If the specified point
         * intersects the line, this method returns 0.
         *
         * @param x1 the X coordinate of the start point of the specified line
         * @param y1 the Y coordinate of the start point of the specified line
         * @param X2 the X coordinate of the end point of the specified line
         * @param y2 the Y coordinate of the end point of the specified line
         * @param px the X coordinate of the specified point being
         *           measured against the specified line
         * @param py the Y coordinate of the specified point being
         *           measured against the specified line
         * @return a int Value that is the square of the distance from the
         *			specified point to the specified line.
         */
        public static int PtLineDistSq(int x1, int y1,
                          int x2, int y2,
                          int px, int py)
        {
            // Adjust vectors relative to x1,y1
            // X2,y2 becomes relative vector from x1,y1 to end of segment
            x2 -= x1;
            y2 -= y1;
            // px,py becomes relative vector from x1,y1 to test point
            px -= x1;
            py -= y1;
            int dotprod = px * x2 + py * y2;
            // dotprod is the length of the px,py vector
            // projected on the x1,y1=>X2,y2 vector times the
            // length of the x1,y1=>X2,y2 vector
            int projlenSq = dotprod * dotprod / (x2 * x2 + y2 * y2);
            // Distance to line is now the length of the relative point
            // vector minus the length of its projection onto the line
            int lenSq = px * px + py * py - projlenSq;
            if (lenSq < 0)
            {
                lenSq = 0;
            }
            return lenSq;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a specified
         * <code>Point</code> to this line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by this <code>Line</code>.  If the specified point
         * intersects the line, this method returns 0.0.
         * @param pt the specified <code>Point</code> being measured
         *           against this line
         * @return a int Value that is the square of the distance from a
         *			specified <code>Point</code> to the current
         *			line.
         */
        public int PtLineDistSq(Point pt)
        {
            return PtLineDistSq(X1, Y1, X2, Y2,
                        pt.X, pt.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from a point to this line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by this <code>Line</code>.  If the specified point
         * intersects the line, this method returns 0.
         *
         * @param px the X coordinate of the specified point being
         *           measured against this line
         * @param py the Y coordinate of the specified point being
         *           measured against this line
         * @return a int Value that is the square of the distance from a
         *			specified point to the current line.
         */
        public int PtLineDistSq(int px, int py)
        {
            return PtLineDistSq(X1, Y1, X2, Y2, px, py);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a point to a line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by the specified coordinates.  If the specified point
         * intersects the line, this method returns 0.
         *
         * @param x1 the X coordinate of the start point of the specified line
         * @param y1 the Y coordinate of the start point of the specified line
         * @param X2 the X coordinate of the end point of the specified line
         * @param y2 the Y coordinate of the end point of the specified line
         * @param px the X coordinate of the specified point being
         *           measured against the specified line
         * @param py the Y coordinate of the specified point being
         *           measured against the specified line
         * @return a int Value that is the distance from the specified
         *			 point to the specified line.
         */
        public static int PtLineDist(int x1, int y1,
                int x2, int y2,
                int px, int py)
        {
            long dis = PtLineDistSq(x1, y1, x2, y2, px, py);
            dis <<= MathFP.DEFAULT_PRECISION;
            return MathFP.ToInt(MathFP.Sqrt(dis));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a point to this line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by this <code>Line</code>.  If the specified point
         * intersects the line, this method returns 0.
         *
         * @param px the X coordinate of the specified point being
         *           measured against this line
         * @param py the Y coordinate of the specified point being
         *           measured against this line
         * @return a int Value that is the distance from a specified point
         *			to the current line.
         */
        public int PtLineDist(int px, int py)
        {
            return PtLineDist(X1, Y1, X2, Y2, px, py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from a <code>Point</code> to this line.
         * The distance measured is the distance between the specified
         * point and the closest point on the infinitely-extended line
         * defined by this <code>Line</code>.  If the specified point
         * intersects the line, this method returns 0.
         * @param pt the specified <code>Point</code> being measured
         * @return a int Value that is the distance from a specified
         *			<code>Point</code> to the current line.
         */
        public int PtLineDist(Point pt)
        {
            return PtLineDist(X1, Y1, X2, Y2,
                     pt.X, pt.Y);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if a specified coordinate is inside the boundary of this
         * <code>Line</code>.  This method is required to implement the
         * {@link IShape} interface, but in the case of <code>Line</code>
         * objects it always returns <code>false</code> since a line contains
         * no area.
         * @param x the X coordinate of the specified point to be tested
         * @param y the Y coordinate of the specified point to be tested
         * @return <code>false</code> because a <code>Line</code> contains
         * no area.
         */
        public bool Contains(int x, int y)
        {
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if a given <code>Point</code> is inside the boundary of
         * this <code>Line</code>.
         * This method is required to implement the {@link IShape} interface,
         * but in the case of <code>Line</code> objects it always returns
         * <code>false</code> since a line contains no area.
         * @param p the specified <code>Point</code> to be tested
         * @return <code>false</code> because a <code>Line</code> contains
         * no area.
         */
        public bool Contains(Point p)
        {
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of this <code>Line</code> entirely contains
         * the specified <code>Rectangle</code>.
         * This method is required to implement the <code>IShape</code> interface,
         * but in the case of <code>Line</code> objects it always returns
         * <code>false</code> since a line contains no area.
         * @param r the specified <code>Rectangle</code> to be tested
         * @return <code>false</code> because a <code>Line</code> contains
         * no area.
         */
        public bool Contains(Rectangle r)
        {
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of this <code>Line</code> entirely contains
         * the specified set of rectangular coordinates.
         * This method is required to implement the <code>IShape</code> interface,
         * but in the case of <code>Line</code> objects it always returns
         * false since a line contains no area.
         * @param x the X coordinate of the upper-left corner of the
         *          specified rectangular area
         * @param y the Y coordinate of the upper-left corner of the
         *          specified rectangular area
         * @param w the width of the specified rectangular area
         * @param h the height of the specified rectangular area
         * @return <code>false</code> because a <code>Line</code> contains
         * no area.
         */
        public bool Contains(int x, int y, int w, int h)
        {
            return false;
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
        public bool Intersects(int x, int y, int w, int h)
        {
            return Intersects(new Rectangle(x, y, w, h));
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
        public bool Intersects(Rectangle r)
        {
            return r.IntersectsLine(X1, Y1, X2, Y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of this
         * <code>Line</code>.
         * The iterator for this class is not multi-threaded safe,
         * which means that this <code>Line</code> class does not
         * guarantee that modifications to the geometry of this
         * <code>Line</code> object do not affect any iterations of that
         * geometry that are already in process.
         * @param at the specified {@link AffineTransform}
         * @return a {@link PathIterator} that defines the boundary of this
         *		<code>Line</code>.
         */
        public PathIterator GetPathIterator(AffineTransform at)
        {
            return new LineIterator(this, at);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of this
         * flattened <code>Line</code>.
         * The iterator for this class is not multi-threaded safe,
         * which means that this <code>Line</code> class does not
         * guarantee that modifications to the geometry of this
         * <code>Line</code> object do not affect any iterations of that
         * geometry that are already in process.
         * @param at the specified <code>AffineTransform</code>
         * @param flatness the maximum amount that the control points for a
         *		given curve can vary from colinear before a subdivided
         *		curve is replaced by a straight line connecting the
         *		end points.  Since a <code>Line</code> object is
         *	        always flat, this parameter is ignored.
         * @return a <code>PathIterator</code> that defines the boundary of the
         *			flattened <code>Line</code>
         */
        public PathIterator GetPathIterator(AffineTransform at, int flatness)
        {
            return new LineIterator(this, at);
        }


    }
}
