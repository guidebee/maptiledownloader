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
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * An <code>Area</code> object stores and manipulates a
     * resolution-independent description of an enclosed area of
     * 2-dimensional space.
     * <code>Area</code> objects can be transformed and can perform
     * various Constructive Area Geometry (CAG) operations when combined
     * with other <code>Area</code> objects.
     * The CAG operations include area
     * {@link #add addition}, {@link #subtract subtraction},
     * {@link #intersect intersection}, and {@link #exclusiveOr exclusive or}.
     * See the linked method documentation for examples of the various
     * operations.
     * <p>
     * The <code>Area</code> class implements the <code>IShape</code>
     * interface and provides full support for all of its hit-testing
     * and path iteration facilities, but an <code>Area</code> is more
     * specific than a generalized path in a number of ways:
     * <ul>
     * <li>Only closed paths and sub-paths are stored.
     *     <code>Area</code> objects constructed from unclosed paths
     *     are implicitly closed during construction as if those paths
     *     had been filled by the <code>Graphics2D.fill</code> method.
     * <li>The interiors of the individual stored sub-paths are all
     *     non-empty and non-overlapping.  Paths are decomposed during
     *     construction into separate component non-overlapping parts,
     *     empty pieces of the path are discarded, and then these
     *     non-empty and non-overlapping properties are maintained
     *     through all subsequent CAG operations.  Outlines of different
     *     component sub-paths may touch each other, as long as they
     *     do not cross so that their enclosed areas overlap.
     * <li>The geometry of the path describing the outline of the
     *     <code>Area</code> resembles the path from which it was
     *     constructed only in that it describes the same enclosed
     *     2-dimensional area, but may use entirely different types
     *     and ordering of the path segments to do so.
     * </ul>
     * Interesting issues which are not always obvious when using
     * the <code>Area</code> include:
     * <ul>
     * <li>Creating an <code>Area</code> from an unclosed (open)
     *     <code>IShape</code> results in a closed outline in the
     *     <code>Area</code> object.
     * <li>Creating an <code>Area</code> from a <code>IShape</code>
     *     which encloses no area (even when "closed") produces an
     *     empty <code>Area</code>.  A common example of this issue
     *     is that producing an <code>Area</code> from a line will
     *     be empty since the line encloses no area.  An empty
     *     <code>Area</code> will iterate no geometry in its
     *     <code>IPathIterator</code> objects.
     * <li>A self-intersecting <code>IShape</code> may be split into
     *     two (or more) sub-paths each enclosing one of the
     *     non-intersecting portions of the original path.
     * <li>An <code>Area</code> may take more path segments to
     *     describe the same geometry even when the original
     *     outline is simple and obvious.  The analysis that the
     *     <code>Area</code> class must perform on the path may
     *     not reflect the same concepts of "simple and obvious"
     *     as a human being perceives.
     * </ul>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Area : IShape
    {
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor which creates an empty area.
         */
        public Area()
        {
            _curves = EmptyCurves;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * The <code>Area</code> class creates an area geometry from the
         * specified {@link IShape} object.  The geometry is explicitly
         * closed, if the <code>IShape</code> is not already closed.  The
         * fill rule (even-odd or winding) specified by the geometry of the
         * <code>IShape</code> is used to determine the resulting enclosed area.
         * @param s  the <code>IShape</code> from which the area is constructed
         * @throws NullPointerException if <code>s</code> is null
         */
        public Area(IShape s)
        {
            if (s is Area)
            {
                _curves = ((Area)s)._curves;
            }
            else
            {
                _curves = PathToCurves(s.GetPathIterator(null));
            }
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds the shape of the specified <code>Area</code> to the
         * shape of this <code>Area</code>.
         * The resulting shape of this <code>Area</code> will include
         * the union of both shapes, or all areas that were contained
         * in either this or the specified <code>Area</code>.
         * <pre>
         *     // Example:
         *     Area a1 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 0,8]);
         *     Area a2 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 8,8]);
         *     a1.add(a2);
         *
         *        a1(before)     +         a2         =     a1(after)
         *
         *     ################     ################     ################
         *     ##############         ##############     ################
         *     ############             ############     ################
         *     ##########                 ##########     ################
         *     ########                     ########     ################
         *     ######                         ######     ######    ######
         *     ####                             ####     ####        ####
         *     ##                                 ##     ##            ##
         * </pre>
         * @param   rhs  the <code>Area</code> to be added to the
         *          current shape
         * @throws NullPointerException if <code>rhs</code> is null
         */
        public void Add(Area rhs)
        {
            _curves = new AreaOp.AddOp().Calculate(_curves, rhs._curves);
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Subtracts the shape of the specified <code>Area</code> from the
         * shape of this <code>Area</code>.
         * The resulting shape of this <code>Area</code> will include
         * areas that were contained only in this <code>Area</code>
         * and not in the specified <code>Area</code>.
         * <pre>
         *     // Example:
         *     Area a1 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 0,8]);
         *     Area a2 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 8,8]);
         *     a1.subtract(a2);
         *
         *        a1(before)     -         a2         =     a1(after)
         *
         *     ################     ################
         *     ##############         ##############     ##
         *     ############             ############     ####
         *     ##########                 ##########     ######
         *     ########                     ########     ########
         *     ######                         ######     ######
         *     ####                             ####     ####
         *     ##                                 ##     ##
         * </pre>
         * @param   rhs  the <code>Area</code> to be subtracted from the
         *		current shape
         * @throws NullPointerException if <code>rhs</code> is null
         */
        public void Subtract(Area rhs)
        {
            _curves = new AreaOp.SubOp().Calculate(_curves, rhs._curves);
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the shape of this <code>Area</code> to the intersection of
         * its current shape and the shape of the specified <code>Area</code>.
         * The resulting shape of this <code>Area</code> will include
         * only areas that were contained in both this <code>Area</code>
         * and also in the specified <code>Area</code>.
         * <pre>
         *     // Example:
         *     Area a1 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 0,8]);
         *     Area a2 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 8,8]);
         *     a1.intersect(a2);
         *
         *      a1(before)   intersect     a2         =     a1(after)
         *
         *     ################     ################     ################
         *     ##############         ##############       ############
         *     ############             ############         ########
         *     ##########                 ##########           ####
         *     ########                     ########
         *     ######                         ######
         *     ####                             ####
         *     ##                                 ##
         * </pre>
         * @param   rhs  the <code>Area</code> to be intersected with this
         *		<code>Area</code>
         * @throws NullPointerException if <code>rhs</code> is null
         */
        public void Intersect(Area rhs)
        {
            _curves = new AreaOp.IntOp().Calculate(_curves, rhs._curves);
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the shape of this <code>Area</code> to be the combined area
         * of its current shape and the shape of the specified <code>Area</code>,
         * minus their intersection.
         * The resulting shape of this <code>Area</code> will include
         * only areas that were contained in either this <code>Area</code>
         * or in the specified <code>Area</code>, but not in both.
         * <pre>
         *     // Example:
         *     Area a1 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 0,8]);
         *     Area a2 = new Area([triangle 0,0 =&gt; 8,0 =&gt; 8,8]);
         *     a1.exclusiveOr(a2);
         *
         *        a1(before)    xor        a2         =     a1(after)
         *
         *     ################     ################
         *     ##############         ##############     ##            ##
         *     ############             ############     ####        ####
         *     ##########                 ##########     ######    ######
         *     ########                     ########     ################
         *     ######                         ######     ######    ######
         *     ####                             ####     ####        ####
         *     ##                                 ##     ##            ##
         * </pre>
         * @param   rhs  the <code>Area</code> to be exclusive ORed with this
         *		<code>Area</code>.
         * @throws NullPointerException if <code>rhs</code> is null
         */
        public void ExclusiveOr(Area rhs)
        {
            _curves = new AreaOp.XorOp().Calculate(_curves, rhs._curves);
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Removes all of the geometry from this <code>Area</code> and
         * restores it to an empty area.
         */
        public void Reset()
        {
            _curves = new ArrayList();
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests whether this <code>Area</code> object encloses any area.
         * @return    <code>true</code> if this <code>Area</code> object
         * represents an empty area; <code>false</code> otherwise.
         */
        public bool IsEmpty()
        {
            return (_curves.Count == 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests whether this <code>Area</code> consists entirely of
         * straight edged polygonal geometry.
         * @return    <code>true</code> if the geometry of this
         * <code>Area</code> consists entirely of line segments;
         * <code>false</code> otherwise.
         */
        public bool IsPolygonal()
        {
            IEnumerator enumerator = _curves.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (((Curve)enumerator.Current).GetOrder() > 1)
                {
                    return false;
                }
            }
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests whether this <code>Area</code> is rectangular in shape.
         * @return    <code>true</code> if the geometry of this
         * <code>Area</code> is rectangular in shape; <code>false</code>
         * otherwise.
         */
        public bool IsRectangular()
        {
            int size = _curves.Count;
            if (size == 0)
            {
                return true;
            }
            if (size > 3)
            {
                return false;
            }
            var c1 = (Curve)_curves[1];
            var c2 = (Curve)_curves[2];
            if (c1.GetOrder() != 1 || c2.GetOrder() != 1)
            {
                return false;
            }
            if (c1.GetXTop() != c1.GetXBot() || c2.GetXTop() != c2.GetXBot())
            {
                return false;
            }
            if (c1.GetYTop() != c2.GetYTop() || c1.GetYBot() != c2.GetYBot())
            {
                // One might be able to prove that this is impossible...
                return false;
            }
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests whether this <code>Area</code> is comprised of a single
         * closed subpath.  This method returns <code>true</code> if the
         * path contains 0 or 1 subpaths, or <code>false</code> if the path
         * contains more than 1 subpath.  The subpaths are counted by the
         * number of {@link IPathIterator#SEG_MOVETO SEG_MOVETO}  segments
         * that appear in the path.
         * @return    <code>true</code> if the <code>Area</code> is comprised
         * of a single basic geometry; <code>false</code> otherwise.
         */
        public bool IsSingular()
        {
            if (_curves.Count < 3)
            {
                return true;
            }
            IEnumerator enumerator = _curves.GetEnumerator();
            enumerator.MoveNext(); // First Order0 "moveto"
            while (enumerator.MoveNext())
            {
                if (((Curve)enumerator.Current).GetOrder() == 0)
                {
                    return false;
                }
            }
            return true;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a bounding {@link Rectangle} that completely encloses
         * this <code>Area</code>.
         * <p>
         * The Area class will attempt to return the tightest bounding
         * box possible for the IShape.  The bounding box will not be
         * padded to include the control points of curves in the outline
         * of the IShape, but should tightly fit the actual geometry of
         * the outline itself.  Since the returned object represents
         * the bounding box with integers, the bounding box can only be
         * as tight as the nearest integer coordinates that encompass
         * the geometry of the IShape.
         * @return    the bounding <code>Rectangle</code> for the
         * <code>Area</code>.
         */
        public Rectangle GetBounds()
        {
            return GetCachedBounds().GetBounds();
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests whether the geometries of the two <code>Area</code> objects
         * are equal.
         * This method will return false if the argument is null.
         * @param   other  the <code>Area</code> to be compared to this
         *		<code>Area</code>
         * @return  <code>true</code> if the two geometries are equal;
         *		<code>false</code> otherwise.
         */
        public bool Equals(Area other)
        {
            // REMIND: A *much* simpler operation should be possible...
            // Should be able to do a curve-wise comparison since all Areas
            // should evaluate their curves in the same top-down order.
            if (other == this)
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }
            ArrayList c = new AreaOp.XorOp().Calculate(_curves, other._curves);
            return c.Count == 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms the geometry of this <code>Area</code> using the specified
         * {@link AffineTransform}.  The geometry is transformed in place, which
         * permanently changes the enclosed area defined by this object.
         * @param t  the transformation used to transform the area
         * @throws NullPointerException if <code>t</code> is null
         */
        public void Transform(AffineTransform t)
        {
            if (t == null)
            {
                throw new NullReferenceException("transform must not be null");
            }
            // REMIND: A simpler operation can be performed for some types
            // of transform.
            _curves = PathToCurves(GetPathIterator(t));
            InvalidateBounds();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a new <code>Area</code> object that contains the same
         * geometry as this <code>Area</code> transformed by the specified
         * <code>AffineTransform</code>.  This <code>Area</code> object
         * is unchanged.
         * @param t  the specified <code>AffineTransform</code> used to transform
         *           the new <code>Area</code>
         * @throws NullPointerException if <code>t</code> is null
         * @return   a new <code>Area</code> object representing the transformed
         *           geometry.
         */
        public Area CreateTransformedArea(AffineTransform t)
        {
            Area a = new Area(this);
            a.Transform(t);
            return a;
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
            if (!GetCachedBounds().Contains(x, y))
            {
                return false;
            }
            IEnumerator enumerator = _curves.GetEnumerator();
            int crossings = 0;
            while (enumerator.MoveNext())
            {
                var c = (Curve)enumerator.Current;
                crossings += c.CrossingsFor(x, y);
            }
            return ((crossings & 1) == 1);
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
        public bool Contains(Point p)
        {
            return Contains(p.GetX(), p.GetY());
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
            if (w < 0 || h < 0)
            {
                return false;
            }
            if (!GetCachedBounds().Contains(x, y, w, h))
            {
                return false;
            }
            Crossings c = Crossings.FindCrossings(_curves, x, y, x + w, y + h);
            return (c.Covers(y, y + h));
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
            return Contains(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
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
            if (w < 0 || h < 0)
            {
                return false;
            }
            if (!GetCachedBounds().Intersects(x, y, w, h))
            {
                return false;
            }
            Crossings c = Crossings.FindCrossings(_curves, x, y, x + w, y + h);
            return (c == null || !c.IsEmpty());
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
            return Intersects(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a {@link IPathIterator} for the outline of this
         * <code>Area</code> object.  This <code>Area</code> object is unchanged.
         * @param at an optional <code>AffineTransform</code> to be applied to
         * the coordinates as they are returned in the iteration, or
         * <code>null</code> if untransformed coordinates are desired
         * @return    the <code>IPathIterator</code> object that returns the
         *		geometry of the outline of this <code>Area</code>, one
         *		segment at a time.
         */
        public PathIterator GetPathIterator(AffineTransform at)
        {
            return new AreaIterator(_curves, at);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a <code>IPathIterator</code> for the flattened outline of
         * this <code>Area</code> object.  Only uncurved path segments
         * represented by the SEG_MOVETO, SEG_LINETO, and SEG_CLOSE point
         * types are returned by the iterator.  This <code>Area</code>
         * object is unchanged.
         * @param at an optional <code>AffineTransform</code> to be
         * applied to the coordinates as they are returned in the
         * iteration, or <code>null</code> if untransformed coordinates
         * are desired
         * @param flatness the maximum amount that the control points
         * for a given curve can vary from colinear before a subdivided
         * curve is replaced by a straight line connecting the end points
         * @return    the <code>IPathIterator</code> object that returns the
         * geometry of the outline of this <code>Area</code>, one segment
         * at a time.
         */
        public PathIterator GetPathIterator(AffineTransform at, int flatness)
        {
            return new FlatteningPathIterator(GetPathIterator(at), flatness);
        }

        private static readonly ArrayList EmptyCurves = new ArrayList();
        private ArrayList _curves;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private static ArrayList PathToCurves(PathIterator pi)
        {
            ArrayList curves = new ArrayList();
            int windingRule = pi.GetWindingRule();
            // coords array is big enough for holding:
            //     coordinates returned from currentSegment (6)
            //     OR
            //         two subdivided quadratic curves (2+4+4=10)
            //         AND
            //             0-1 horizontal splitting parameters
            //             OR
            //             2 parametric equation derivative coefficients
            //     OR
            //         three subdivided cubic curves (2+6+6+6=20)
            //         AND
            //             0-2 horizontal splitting parameters
            //             OR
            //             3 parametric equation derivative coefficients
            var coords = new int[23];
            double movx = 0, movy = 0;
            double curx = 0, cury = 0;
            while (!pi.IsDone())
            {
                double newx;
                double newy;
                switch (pi.CurrentSegment(coords))
                {
                    case PathIterator.SEG_MOVETO:
                        Curve.InsertLine(curves, curx, cury, movx, movy);
                        curx = movx = coords[0];
                        cury = movy = coords[1];
                        Curve.InsertMove(curves, movx, movy);
                        break;
                    case PathIterator.SEG_LINETO:
                        newx = coords[0];
                        newy = coords[1];
                        Curve.InsertLine(curves, curx, cury, newx, newy);
                        curx = newx;
                        cury = newy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        {
                            newx = coords[2];
                            newy = coords[3];
                            var dblCoords = new double[coords.Length];
                            for (int i = 0; i < coords.Length; i++)
                            {
                                dblCoords[i] = coords[i];
                            }
                            Curve.InsertQuad(curves, curx, cury, dblCoords);
                            curx = newx;
                            cury = newy;
                        }
                        break;
                    case PathIterator.SEG_CUBICTO:
                        {
                            newx = coords[4];
                            newy = coords[5];
                            var dblCoords = new double[coords.Length];
                            for (int i = 0; i < coords.Length; i++)
                            {
                                dblCoords[i] = coords[i];
                            }
                            Curve.InsertCubic(curves, curx, cury, dblCoords);
                            curx = newx;
                            cury = newy;
                        }
                        break;
                    case PathIterator.SEG_CLOSE:
                        Curve.InsertLine(curves, curx, cury, movx, movy);
                        curx = movx;
                        cury = movy;
                        break;
                }
                pi.Next();
            }
            Curve.InsertLine(curves, curx, cury, movx, movy);
            AreaOp op;
            if (windingRule == PathIterator.WIND_EVEN_ODD)
            {
                op = new AreaOp.EoWindOp();
            }
            else
            {
                op = new AreaOp.NzWindOp();
            }
            return op.Calculate(curves, EmptyCurves);
        }

        private Rectangle _cachedBounds;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private void InvalidateBounds()
        {
            _cachedBounds = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private Rectangle GetCachedBounds()
        {
            if (_cachedBounds != null)
            {
                return _cachedBounds;
            }
            Rectangle r = new Rectangle();
            if (_curves.Count > 0)
            {
                Curve c = (Curve)_curves[0];
                // First point is always an order 0 curve (moveto)
                r.SetRect((int)(c.GetX0() + .5),
                        (int)(c.GetY0() + .5), 0, 0);
                for (int i = 1; i < _curves.Count; i++)
                {
                    ((Curve)_curves[i]).Enlarge(r);
                }
            }
            return (_cachedBounds = r);
        }

    }

    class AreaIterator : PathIterator
    {

        private readonly AffineTransform _transform;
        private readonly ArrayList _curves;
        private int _index;
        private Curve _prevcurve;
        private Curve _thiscurve;

        public AreaIterator(ArrayList curves, AffineTransform at)
        {
            _curves = curves;
            _transform = at;
            if (curves.Count >= 1)
            {
                _thiscurve = (Curve)curves[0];
            }
        }

        public override int GetWindingRule()
        {
            // REMIND: Which is better, EVEN_ODD or NON_ZERO?
            //         The paths calculated could be classified either way.
            //return WIND_EVEN_ODD;
            return WIND_NON_ZERO;
        }

        public override bool IsDone()
        {
            return (_prevcurve == null && _thiscurve == null);
        }

        public override void Next()
        {
            if (_prevcurve != null)
            {
                _prevcurve = null;
            }
            else
            {
                _prevcurve = _thiscurve;
                _index++;
                if (_index < _curves.Count)
                {
                    _thiscurve = (Curve)_curves[_index];
                    if (_thiscurve.GetOrder() != 0 &&
                            _prevcurve.GetX1() == _thiscurve.GetX0() &&
                            _prevcurve.GetY1() == _thiscurve.GetY0())
                    {
                        _prevcurve = null;
                    }
                }
                else
                {
                    _thiscurve = null;
                }
            }
        }

        public override int CurrentSegment(int[] coords)
        {
            int segtype;
            int numpoints;
            if (_prevcurve != null)
            {
                // Need to finish off junction between curves
                if (_thiscurve == null || _thiscurve.GetOrder() == 0)
                {
                    return SEG_CLOSE;
                }
                coords[0] = (int)(_thiscurve.GetX0() + .5);
                coords[1] = (int)(_thiscurve.GetY0() + .5);
                segtype = SEG_LINETO;
                numpoints = 1;
            }
            else if (_thiscurve == null)
            {
                throw new IndexOutOfRangeException("area iterator out of bounds");
            }
            else
            {
                double[] dblCoords = new double[coords.Length];

                segtype = _thiscurve.GetSegment(dblCoords);
                numpoints = _thiscurve.GetOrder();
                if (numpoints == 0)
                {
                    numpoints = 1;
                }
                for (int i = 0; i < coords.Length; i++)
                {
                    coords[i] = (int)(dblCoords[i] + .5);
                }
            }
            if (_transform != null)
            {
                _transform.Transform(coords, 0, coords, 0, numpoints);
            }
            return segtype;
        }

    }

}
