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
     * The <code>Polygon</code> class encapsulates a description of a
     * closed, two-dimensional region within a coordinate space. This
     * region is bounded by an arbitrary number of line segments, each of
     * which is one side of the polygon. Internally, a polygon
     * comprises of a list of {@code (x,y)}
     * coordinate pairs, where each pair defines a <i>vertex</i> of the
     * polygon, and two successive pairs are the endpoints of a
     * line that is a side of the polygon. The first and final
     * pairs of {@code (x,y)} points are joined by a line segment
     * that closes the polygon.  This <code>Polygon</code> is defined with
     * an even-odd winding rule.  See
     * {@link com.mapdigit.drawing.geometry.IPathIterator#WIND_EVEN_ODD WIND_EVEN_ODD}
     * for a definition of the even-odd winding rule.
     * This class's hit-testing methods, which include the
     * <code>contains</code>, <code>intersects</code> and <code>inside</code>
     * methods, use the <i>insideness</i> definition described in the
     * {@link IShape} class comments.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Polygon : IShape
    {

        /**
         * The total number of points.  The Value of <code>npoints</code>
         * represents the number of valid points in this <code>Polygon</code>
         * and might be less than the number of elements in
         * {@link #xpoints xpoints} or {@link #ypoints ypoints}.
         * This Value can be NULL.
         */
        public int NumOfNpoints;
        /**
         * The array of X coordinates.  The number of elements in
         * this array might be more than the number of X coordinates
         * in this <code>Polygon</code>.  The extra elements allow new points
         * to be added to this <code>Polygon</code> without re-creating this
         * array.  The Value of {@link #npoints npoints} is equal to the
         * number of valid points in this <code>Polygon</code>.
         */
        public int[] Xpoints;
        /**
         * The array of Y coordinates.  The number of elements in
         * this array might be more than the number of Y coordinates
         * in this <code>Polygon</code>.  The extra elements allow new points
         * to be added to this <code>Polygon</code> without re-creating this
         * array.  The Value of <code>npoints</code> is equal to the
         * number of valid points in this <code>Polygon</code>.
         */
        public int[] Ypoints;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an empty polygon.
         */
        public Polygon()
        {
            Xpoints = new int[MIN_LENGTH];
            Ypoints = new int[MIN_LENGTH];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>Polygon</code> from the specified
         * parameters.
         * @param xpoints an array of X coordinates
         * @param ypoints an array of Y coordinates
         * @param npoints the total number of points in the
         *				<code>Polygon</code>
         * @exception  NegativeArraySizeException if the Value of
         *                       <code>npoints</code> is negative.
         * @exception  IndexOutOfBoundsException if <code>npoints</code> is
         *             greater than the length of <code>xpoints</code>
         *             or the length of <code>ypoints</code>.
         * @exception  NullPointerException if <code>xpoints</code> or
         *             <code>ypoints</code> is <code>null</code>.
         */
        public Polygon(int[] xpoints, int[] ypoints, int npoints)
        {
            if (npoints > xpoints.Length || npoints > ypoints.Length)
            {
                throw new IndexOutOfRangeException("npoints > xpoints.length || " +
                        "npoints > ypoints.length");
            }
            if (npoints < 0)
            {
                throw new ArgumentException("npoints < 0");
            }
            NumOfNpoints = npoints;
            Xpoints = new int[xpoints.Length];
            Array.Copy(xpoints, Xpoints, xpoints.Length);
            Ypoints = new int[ypoints.Length];
            Array.Copy(ypoints, Ypoints, ypoints.Length);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Resets this <code>Polygon</code> object to an empty polygon.
         * The coordinate arrays and the data in them are left untouched
         * but the number of points is reset to zero to mark the old
         * vertex data as invalid and to start accumulating new vertex
         * data at the beginning.
         * All internally-cached data relating to the old vertices
         * are discarded.
         * that since the coordinate arrays from before the reset
         * are reused, creating a new empty <code>Polygon</code> might
         * be more memory efficient than resetting the current one if
         * the number of vertices in the new polygon data is significantly
         * smaller than the number of vertices in the data from before the
         * reset.
         */
        public void Reset()
        {
            NumOfNpoints = 0;
            _bounds = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Invalidates or flushes any internally-cached data that depends
         * on the vertex coordinates of this <code>Polygon</code>.
         * This method should be called after any direct manipulation
         * of the coordinates in the <code>xpoints</code> or
         * <code>ypoints</code> arrays to avoid inconsistent results
         * from methods such as <code>getBounds</code> or <code>contains</code>
         * that might cache data from earlier computations relating to
         * the vertex coordinates.
         */
        public void Invalidate()
        {
            _bounds = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Translates the vertices of the <code>Polygon</code> by
         * <code>deltaX</code> along the x axis and by
         * <code>deltaY</code> along the y axis.
         * @param deltaX the amount to translate along the X axis
         * @param deltaY the amount to translate along the Y axis
         */
        public void Translate(int deltaX, int deltaY)
        {
            for (int i = 0; i < NumOfNpoints; i++)
            {
                Xpoints[i] += deltaX;
                Ypoints[i] += deltaY;
            }
            if (_bounds != null)
            {
                _bounds.Translate(deltaX, deltaY);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Appends the specified coordinates to this <code>Polygon</code>.
         * <p>
         * If an operation that calculates the bounding box of this
         * <code>Polygon</code> has already been performed, such as
         * <code>getBounds</code> or <code>contains</code>, then this
         * method updates the bounding box.
         * @param       pt a point to be added.
         */
        public void AddPoint(Point pt)
        {
            AddPoint(pt.X, pt.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Appends the specified coordinates to this <code>Polygon</code>.
         * <p>
         * If an operation that calculates the bounding box of this
         * <code>Polygon</code> has already been performed, such as
         * <code>getBounds</code> or <code>contains</code>, then this
         * method updates the bounding box.
         * @param       x the specified X coordinate
         * @param       y the specified Y coordinate
         */
        public void AddPoint(int x, int y)
        {
            if (NumOfNpoints >= Xpoints.Length || NumOfNpoints >= Ypoints.Length)
            {
                int newLength = NumOfNpoints * 2;
                // Make sure that newLength will be greater than MIN_LENGTH and
                // aligned to the power of 2
                if (newLength < MIN_LENGTH)
                {
                    newLength = MIN_LENGTH;
                }

                int[] temp = new int[Xpoints.Length];
                Array.Copy(Xpoints, temp, Xpoints.Length);
                Xpoints = new int[newLength];
                Array.Copy(temp, Xpoints, temp.Length);

                temp = new int[Ypoints.Length];
                Array.Copy(Ypoints, temp, Ypoints.Length);
                Ypoints = new int[newLength];
                Array.Copy(temp, Ypoints, temp.Length);

            }
            Xpoints[NumOfNpoints] = x;
            Ypoints[NumOfNpoints] = y;
            NumOfNpoints++;
            if (_bounds != null)
            {
                UpdateBounds(x, y);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the bounding box of this <code>Polygon</code>.
         * The bounding box is the smallest {@link Rectangle} whose
         * sides are parallel to the x and y axes of the
         * coordinate space, and can completely contain the <code>Polygon</code>.
         * @return a <code>Rectangle</code> that defines the bounds of this
         * <code>Polygon</code>.
         */
        public Rectangle GetBounds()
        {
            return GetBoundingBox();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether the specified {@link Point} is inside this
         * <code>Polygon</code>.
         * @param p the specified <code>Point</code> to be tested
         * @return <code>true</code> if the <code>Polygon</code> contains the
         * 			<code>Point</code>; <code>false</code> otherwise.
         */
        public bool Contains(Point p)
        {
            return Contains(p.X, p.Y);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether the specified coordinates are contained in this
         * <code>Polygon</code>.
         * @param x the specified X coordinate to be tested
         * @param y the specified Y coordinate to be tested
         * @return {@code true} if this {@code Polygon} contains
         *         the specified coordinates {@code (x,y)};
         *         {@code false} otherwise.
         */
        public bool Inside(int x, int y)
        {
            return Contains(x, y);
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
        public bool Contains(int x, int y)
        {
            if (NumOfNpoints <= 2 || !GetBoundingBox().Contains(x, y))
            {
                return false;
            }
            int hits = 0;

            int lastx = Xpoints[NumOfNpoints - 1];
            int lasty = Ypoints[NumOfNpoints - 1];
            int curx, cury;

            // Walk the edges of the polygon
            for (int i = 0; i < NumOfNpoints; lastx = curx, lasty = cury, i++)
            {
                curx = Xpoints[i];
                cury = Ypoints[i];

                if (cury == lasty)
                {
                    continue;
                }

                int leftx;
                if (curx < lastx)
                {
                    if (x >= lastx)
                    {
                        continue;
                    }
                    leftx = curx;
                }
                else
                {
                    if (x >= curx)
                    {
                        continue;
                    }
                    leftx = lastx;
                }

                double test1, test2;
                if (cury < lasty)
                {
                    if (y < cury || y >= lasty)
                    {
                        continue;
                    }
                    if (x < leftx)
                    {
                        hits++;
                        continue;
                    }
                    test1 = x - curx;
                    test2 = y - cury;
                }
                else
                {
                    if (y < lasty || y >= cury)
                    {
                        continue;
                    }
                    if (x < leftx)
                    {
                        hits++;
                        continue;
                    }
                    test1 = x - lastx;
                    test2 = y - lasty;
                }

                if (test1 < (test2 / (lasty - cury) * (lastx - curx)))
                {
                    hits++;
                }
            }

            return ((hits & 1) != 0);
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
            if (NumOfNpoints <= 0 || !GetBoundingBox().Intersects(x, y, w, h))
            {
                return false;
            }

            Crossings cross = GetCrossings(x, y, x + w, y + h);
            return (cross == null || !cross.IsEmpty());
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
            return Intersects(r.X, r.Y, r.Width, r.Height);
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
        public bool Contains(int x, int y, int w, int h)
        {
            if (NumOfNpoints <= 0 || !GetBoundingBox().Intersects(x, y, w, h))
            {
                return false;
            }

            Crossings cross = GetCrossings(x, y, x + w, y + h);
            return (cross != null && cross.Covers(y, y + h));
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
        public bool Contains(Rectangle r)
        {
            return Contains(r.X, r.Y, r.Width, r.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iterator object that iterates along the boundary of this
         * <code>Polygon</code> and provides access to the geometry
         * of the outline of this <code>Polygon</code>.  An optional
         * {@link AffineTransform} can be specified so that the coordinates
         * returned in the iteration are transformed accordingly.
         * @param at an optional <code>AffineTransform</code> to be applied to the
         * 		coordinates as they are returned in the iteration, or
         *		<code>null</code> if untransformed coordinates are desired
         * @return a {@link IPathIterator} object that provides access to the
         *		geometry of this <code>Polygon</code>.
         */
        public PathIterator GetPathIterator(AffineTransform at)
        {
            return new PolygonPathIterator(this, at);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iterator object that iterates along the boundary of
         * the <code>IShape</code> and provides access to the geometry of the
         * outline of the <code>IShape</code>.  Only SEG_MOVETO, SEG_LINETO, and
         * SEG_CLOSE point types are returned by the iterator.
         * Since polygons are already flat, the <code>flatness</code> parameter
         * is ignored.  An optional <code>AffineTransform</code> can be specified
         * in which case the coordinates returned in the iteration are transformed
         * accordingly.
         * @param at an optional <code>AffineTransform</code> to be applied to the
         * 		coordinates as they are returned in the iteration, or
         *		<code>null</code> if untransformed coordinates are desired
         * @param flatness the maximum amount that the control points
         * 		for a given curve can vary from colinear before a subdivided
         *		curve is replaced by a straight line connecting the
         * 		endpoints.  Since polygons are already flat the
         * 		<code>flatness</code> parameter is ignored.
         * @return a <code>IPathIterator</code> object that provides access to the
         * 		<code>IShape</code> object's geometry.
         */
        public PathIterator GetPathIterator(AffineTransform at, int flatness)
        {
            return GetPathIterator(at);
        }
        /**
         * The bounds of this {@code Polygon}.
         * This Value can be null.
         */
        protected Rectangle _bounds;

        /*
         * Default length for xpoints and ypoints.
         */
        private const int MIN_LENGTH = 4;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /*
         * Calculates the bounding box of the points passed to the constructor.
         * Sets <code>bounds</code> to the result.
         * @param xpoints[] array of <i>x</i> coordinates
         * @param ypoints[] array of <i>y</i> coordinates
         * @param npoints the total number of points
         */
        private void CalculateBounds(int[] xpoints, int[] ypoints, int npoints)
        {
            int boundsMinX = int.MaxValue;
            int boundsMinY = int.MaxValue;
            int boundsMaxX = int.MinValue;
            int boundsMaxY = int.MinValue;

            for (int i = 0; i < npoints; i++)
            {
                int x = xpoints[i];
                boundsMinX = Math.Min(boundsMinX, x);
                boundsMaxX = Math.Max(boundsMaxX, x);
                int y = ypoints[i];
                boundsMinY = Math.Min(boundsMinY, y);
                boundsMaxY = Math.Max(boundsMaxY, y);
            }
            _bounds = new Rectangle(boundsMinX, boundsMinY,
                    boundsMaxX - boundsMinX,
                    boundsMaxY - boundsMinY);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /*
         * Resizes the bounding box to accomodate the specified coordinates.
         * @param x,&nbsp;y the specified coordinates
         */
        private void UpdateBounds(int x, int y)
        {
            if (x < _bounds.X)
            {
                _bounds.Width = _bounds.Width + (_bounds.X - x);
                _bounds.X = x;
            }
            else
            {
                _bounds.Width = Math.Max(_bounds.Width, x - _bounds.X);
                // bounds.x = bounds.x;
            }

            if (y < _bounds.Y)
            {
                _bounds.Height = _bounds.Height + (_bounds.Y - y);
                _bounds.Y = y;
            }
            else
            {
                _bounds.Height = Math.Max(_bounds.Height, y - _bounds.Y);
                // bounds.y = bounds.y;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the bounds of this <code>Polygon</code>.
         * @return the bounds of this <code>Polygon</code>.
         */
        private Rectangle GetBoundingBox()
        {
            if (NumOfNpoints == 0)
            {
                return new Rectangle();
            }
            if (_bounds == null)
            {
                CalculateBounds(Xpoints, Ypoints, NumOfNpoints);
            }
            if (_bounds != null) return _bounds.GetBounds();
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private Crossings GetCrossings(int xlo, int ylo,
                int xhi, int yhi)
        {
            Crossings cross = new Crossings.EvenOdd(xlo, ylo, xhi, yhi);
            int lastx = Xpoints[NumOfNpoints - 1];
            int lasty = Ypoints[NumOfNpoints - 1];
            int curx, cury;

            // Walk the edges of the polygon
            for (int i = 0; i < NumOfNpoints; i++)
            {
                curx = Xpoints[i];
                cury = Ypoints[i];
                if (cross.AccumulateLine(lastx, lasty, curx, cury))
                {
                    return null;
                }
                lastx = curx;
                lasty = cury;
            }

            return cross;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        class PolygonPathIterator : PathIterator
        {

            Polygon poly;
            AffineTransform transform;
            int index;

            public PolygonPathIterator(Polygon pg, AffineTransform at)
            {
                poly = pg;
                transform = at;
                if (pg.NumOfNpoints == 0)
                {
                    // Prevent a spurious SEG_CLOSE segment
                    index = 1;
                }
            }

            /**
             * Returns the winding rule for determining the interior of the
             * path.
             * @return an integer representing the current winding rule.
             */
            public override int GetWindingRule()
            {
                return WIND_EVEN_ODD;
            }

            /**
             * Tests if there are more points to read.
             * @return <code>true</code> if there are more points to read;
             *          <code>false</code> otherwise.
             */
            public override bool IsDone()
            {
                return index > poly.NumOfNpoints;
            }

            /**
             * Moves the iterator forwards, along the primary direction of
             * traversal, to the next segment of the path when there are
             * more points in that direction.
             */
            public override void Next()
            {
                index++;
            }

            /**
             * Returns the coordinates and type of the current path segment in
             * the iteration.
             * The return Value is the path segment type:
             * SEG_MOVETO, SEG_LINETO, or SEG_CLOSE.
             * A <code>int</code> array of length 2 must be passed in and
             * can be used to store the coordinates of the point(s).
             * Each point is stored as a pair of <code>int</code> x,&nbsp;y
             * coordinates.  SEG_MOVETO and SEG_LINETO types return one
             * point, and SEG_CLOSE does not return any points.
             * @param coords a <code>int</code> array that specifies the
             * coordinates of the point(s)
             * @return an integer representing the type and coordinates of the
             * 		current path segment.
             */
            public override int CurrentSegment(int[] coords)
            {
                if (index >= poly.NumOfNpoints)
                {
                    return SEG_CLOSE;
                }
                coords[0] = poly.Xpoints[index];
                coords[1] = poly.Ypoints[index];
                if (transform != null)
                {
                    transform.Transform(coords, 0, coords, 0, 1);
                }
                return (index == 0 ? SEG_MOVETO : SEG_LINETO);
            }
        }
    }

}
