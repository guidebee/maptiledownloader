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
using System.Text;

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
     * The {@code Path} class provides a simple, yet flexible
     * shape which represents an arbitrary geometric path.
     * It can fully represent any path which can be iterated by the
     * {@link IPathIterator} interface including all of its segment
     * types and winding rules and it implements all of the
     * basic hit testing methods of the {@link IShape} interface.
     * <p>
     * {@code Path} provides exactly those facilities required for
     * basic construction and management of a geometric path and
     * implementation of the above interfaces with little added
     * interpretation.
     * If it is useful to manipulate the interiors of closed
     * geometric shapes beyond simple hit testing then the
     * {@link Area} class provides additional capabilities
     * specifically targeted at closed figures.
     * While both classes nominally implement the {@code IShape}
     * interface, they differ in purpose and together they provide
     * two useful views of a geometric shape where {@code Path}
     * deals primarily with a trajectory formed by path segments
     * and {@code Area} deals more with interpretation and manipulation
     * of enclosed regions of 2D geometric space.
     * <p>
     * The {@link IPathIterator} interface has more detailed descriptions
     * of the types of segments that make up a path and the winding rules
     * that control how to determine which regions are inside or outside
     * the path.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Path : IShape
    {

        /**
         * An even-odd winding rule for determining the interior of
         * a path.
         */
        public const int WIND_EVEN_ODD = PathIterator.WIND_EVEN_ODD;
        /**
         * A non-zero winding rule for determining the interior of a
         * path.
         */
        public const int WIND_NON_ZERO = PathIterator.WIND_NON_ZERO;
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new int precision {@code Path} object
         * from an arbitrary {@link IShape} object, transformed by an
         * {@link AffineTransform} object.
         * All of the initial geometry and the winding rule for this path are
         * taken from the specified {@code IShape} object and transformed
         * by the specified {@code AffineTransform} object.
         *
         * @param s the specified {@code IShape} object
         * @param at the specified {@code AffineTransform} object
         */
        public Path(IShape s, AffineTransform at)
        {
            if (s is Path)
            {
                Path p2D = (Path)s;
                SetWindingRule(p2D._windingRule);
                _numTypes = p2D._numTypes;
                _pointTypes = new byte[p2D._pointTypes.Length];
                Array.Copy(p2D._pointTypes, _pointTypes, _pointTypes.Length);

                _numCoords = p2D._numCoords;
                _intCoords = p2D.CloneCoords(at);
            }
            else
            {
                PathIterator pi = s.GetPathIterator(at);
                SetWindingRule(pi.GetWindingRule());
                _pointTypes = new byte[INIT_SIZE];
                _intCoords = new int[INIT_SIZE * 2];
                Append(pi, false);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new empty int precision {@code Path} object
         * with a default winding rule of {@link #WIND_NON_ZERO}.
         */
        public Path()
            : this(WIND_NON_ZERO, INIT_SIZE)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new empty int precision {@code Path} object
         * with the specified winding rule to control operations that
         * require the interior of the path to be defined.
         *
         * @param rule the winding rule
         */
        public Path(int rule)
            : this(rule, INIT_SIZE)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new {@code Path2D} object from the given
         * specified initial values.
         * This method is only intended for internal use and should
         * not be made public if the other constructors for this class
         * are ever exposed.
         *
         * @param rule the winding rule
         * @param initialTypes the size to make the initial array to
         *                     store the path segment types
         */
        public Path(int rule, int initialTypes)
        {
            SetWindingRule(rule);
            _pointTypes = new byte[initialTypes];
            _intCoords = new int[initialTypes * 2];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new int precision {@code Path} object
         * from an arbitrary {@link IShape} object.
         * All of the initial geometry and the winding rule for this path are
         * taken from the specified {@code IShape} object.
         *
         * @param s the specified {@code IShape} object
         */
        public Path(IShape s)
            : this(s, null)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a new object of the same class as this object.
         *
         * @return     a clone of this instance.
         */
        public object Clone()
        {
            return new Path(this);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Closes the current subpath by drawing a straight line back to
         * the coordinates of the last {@code moveTo}.  If the path is already
         * closed then this method has no effect.
         */
        public void ClosePath()
        {
            if (_numTypes == 0 || _pointTypes[_numTypes - 1] != SEG_CLOSE)
            {
                NeedRoom(true, 0);
                _pointTypes[_numTypes++] = SEG_CLOSE;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Appends the geometry of the specified {@code IShape} object to the
         * path, possibly connecting the new geometry to the existing path
         * segments with a line segment.
         * If the {@code connect} parameter is {@code true} and the
         * path is not empty then any initial {@code moveTo} in the
         * geometry of the appended {@code IShape}
         * is turned into a {@code lineTo} segment.
         * If the destination coordinates of such a connecting {@code lineTo}
         * segment match the ending coordinates of a currently open
         * subpath then the segment is omitted as superfluous.
         * The winding rule of the specified {@code IShape} is ignored
         * and the appended geometry is governed by the winding
         * rule specified for this path.
         *
         * @param s the {@code IShape} whose geometry is appended
         *          to this path
         * @param connect a bool to control whether or not to turn an initial
         *                {@code moveTo} segment into a {@code lineTo} segment
         *                to connect the new geometry to the existing path
         */
        public void Append(IShape s, bool connect)
        {
            Append(s.GetPathIterator(null), connect);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the fill style winding rule.
         *
         * @return an integer representing the current winding rule.
         */
        public int GetWindingRule()
        {
            return _windingRule;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the winding rule for this path to the specified Value.
         *
         * @param rule an integer representing the specified
         *             winding rule
         * @exception IllegalArgumentException if
         *		{@code rule} is not either
         *		{@link #WIND_EVEN_ODD} or
         *		{@link #WIND_NON_ZERO}
         */
        public void SetWindingRule(int rule)
        {
            if (rule != WIND_EVEN_ODD && rule != WIND_NON_ZERO)
            {
                throw new ArgumentException("winding rule must be " +
                        "WIND_EVEN_ODD or " +
                        "WIND_NON_ZERO");
            }
            _windingRule = rule;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the coordinates most recently added to the end of the path
         * as a {@link Point} object.
         *
         * @return a {@code Point} object containing the ending coordinates of
         *         the path or {@code null} if there are no points in the path.
         */
        public Point GetCurrentPoint()
        {
            int index = _numCoords;
            if (_numTypes < 1 || index < 1)
            {
                return null;
            }
            if (_pointTypes[_numTypes - 1] == SEG_CLOSE)
            {
            loop:
                for (int i = _numTypes - 2; i > 0; i--)
                {
                    switch (_pointTypes[i])
                    {
                        case SEG_MOVETO:
                            goto loop;
                        case SEG_LINETO:
                            index -= 2;
                            break;
                        case SEG_QUADTO:
                            index -= 4;
                            break;
                        case SEG_CUBICTO:
                            index -= 6;
                            break;
                        case SEG_CLOSE:
                            break;
                    }
                }
            }
            return GetPoint(index - 2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Resets the path to empty.  The Append position is set back to the
         * beginning of the path and all coordinates and point types are
         * forgotten.
         */
        public void Reset()
        {
            _numTypes = _numCoords = 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a new {@code IShape} representing a transformed version
         * of this {@code Path}.
         * that the exact type and coordinate precision of the return
         * Value is not specified for this method.
         * The method will return a IShape that contains no less precision
         * for the transformed geometry than this {@code Path} currently
         * maintains, but it may contain no more precision either.
         * If the tradeoff of precision vs. 
         *
         * @param at the {@code AffineTransform} used to transform a
         *           new {@code IShape}.
         * @return a new {@code IShape}, transformed with the specified
         *         {@code AffineTransform}.
         */
        public IShape CreateTransformedShape(AffineTransform at)
        {
            Path p2D = (Path)Clone();
            if (at != null)
            {
                p2D.Transform(at);
            }
            return p2D;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified coordinates are inside the closed
         * boundary of the specified {@link PathIterator}.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#contains(int, int)} method.
         *
         * @param pi the specified {@code PathIterator}
         * @param x the specified X coordinate
         * @param y the specified Y coordinate
         * @return {@code true} if the specified coordinates are inside the
         *         specified {@code PathIterator}; {@code false} otherwise
         */
        public static bool Contains(PathIterator pi, int x, int y)
        {
            if (x * 0 + y * 0 == 0)
            {
                /* N * 0 is 0 only if N is finite.
                 * Here we know that both x and y are finite.
                 */
                int mask = (pi.GetWindingRule() == WIND_NON_ZERO ? -1 : 1);
                int cross = Curve.PointCrossingsForPath(pi, x, y);
                return ((cross & mask) != 0);
            }
            /* Either x or y was infinite or NaN.
                 * A NaN always produces a negative response to any test
                 * and Infinity values cannot be "inside" any path so
                 * they should return false as well.
                 */
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified {@link Point} is inside the closed
         * boundary of the specified {@link PathIterator}.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#contains(Point)} method.
         *
         * @param pi the specified {@code PathIterator}
         * @param p the specified {@code Point}
         * @return {@code true} if the specified coordinates are inside the
         *         specified {@code PathIterator}; {@code false} otherwise
         */
        public static bool Contains(PathIterator pi, Point p)
        {
            return Contains(pi, p.X, p.Y);
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
            if (x * 0 + y * 0 == 0)
            {
                /* N * 0 is 0 only if N is finite.
                 * Here we know that both x and y are finite.
                 */
                if (_numTypes < 2)
                {
                    return false;
                }
                int mask = (_windingRule == WIND_NON_ZERO ? -1 : 1);
                return ((PointCrossings(x, y) & mask) != 0);
            }
            /* Either x or y was infinite or NaN.
                 * A NaN always produces a negative response to any test
                 * and Infinity values cannot be "inside" any path so
                 * they should return false as well.
                 */
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
         * Tests if the specified rectangular area is entirely inside the
         * closed boundary of the specified {@link PathIterator}.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#contains(int, int, int, int)} method.
         * <p>
         * This method object may conservatively return false in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such segments could lie entirely within the interior of the
         * path if they are part of a path with a {@link #WIND_NON_ZERO}
         * winding rule or if the segments are retraced in the reverse
         * direction such that the two sets of segments cancel each
         * other out without any exterior area falling between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         *
         * @param pi the specified {@code PathIterator}
         * @param x the specified X coordinate
         * @param y the specified Y coordinate
         * @param w the width of the specified rectangular area
         * @param h the height of the specified rectangular area
         * @return {@code true} if the specified {@code PathIterator} contains
         *         the specified rectangluar area; {@code false} otherwise.
         */
        public static bool Contains(PathIterator pi,
                int x, int y, int w, int h)
        {
            if (Double.IsNaN(x + w) || Double.IsNaN(y + h))
            {
                /* [xy]+[wh] is NaN if any of those values are NaN,
                 * or if adding the two together would produce NaN
                 * by virtue of adding opposing Infinte values.
                 * Since we need to add them below, their sum must
                 * not be NaN.
                 * We return false because NaN always produces a
                 * negative response to tests
                 */
                return false;
            }
            if (w <= 0 || h <= 0)
            {
                return false;
            }
            int mask = (pi.GetWindingRule() == WIND_NON_ZERO ? -1 : 2);
            int crossings = Curve.RectCrossingsForPath(pi, x, y, x + w, y + h);
            return (crossings != Curve.RECT_INTERSECTS &&
                    (crossings & mask) != 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified {@link Rectangle} is entirely inside the
         * closed boundary of the specified {@link PathIterator}.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#contains(Rectangle)} method.
         * <p>
         * This method object may conservatively return false in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such segments could lie entirely within the interior of the
         * path if they are part of a path with a {@link #WIND_NON_ZERO}
         * winding rule or if the segments are retraced in the reverse
         * direction such that the two sets of segments cancel each
         * other out without any exterior area falling between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         *
         * @param pi the specified {@code PathIterator}
         * @param r a specified {@code Rectangle}
         * @return {@code true} if the specified {@code PathIterator} contains
         *         the specified {@code Rectangle}; {@code false} otherwise.
         */
        public static bool Contains(PathIterator pi, Rectangle r)
        {
            return Contains(pi, r.X, r.Y, r.Width, r.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * <p>
         * This method object may conservatively return false in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such segments could lie entirely within the interior of the
         * path if they are part of a path with a {@link #WIND_NON_ZERO}
         * winding rule or if the segments are retraced in the reverse
         * direction such that the two sets of segments cancel each
         * other out without any exterior area falling between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         */
        public bool Contains(int x, int y, int w, int h)
        {
            if (Double.IsNaN(x + w) || Double.IsNaN(y + h))
            {
                /* [xy]+[wh] is NaN if any of those values are NaN,
                 * or if adding the two together would produce NaN
                 * by virtue of adding opposing Infinte values.
                 * Since we need to add them below, their sum must
                 * not be NaN.
                 * We return false because NaN always produces a
                 * negative response to tests
                 */
                return false;
            }
            if (w <= 0 || h <= 0)
            {
                return false;
            }
            int mask = (_windingRule == WIND_NON_ZERO ? -1 : 2);
            int crossings = RectCrossings(x, y, x + w, y + h);
            return (crossings != Curve.RECT_INTERSECTS &&
                    (crossings & mask) != 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * <p>
         * This method object may conservatively return false in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such segments could lie entirely within the interior of the
         * path if they are part of a path with a {@link #WIND_NON_ZERO}
         * winding rule or if the segments are retraced in the reverse
         * direction such that the two sets of segments cancel each
         * other out without any exterior area falling between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
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
         * Tests if the interior of the specified {@link PathIterator}
         * intersects the interior of a specified set of rectangular
         * coordinates.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#intersects(int, int, int, int)} method.
         * <p>
         * This method object may conservatively return true in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such a case may occur if some set of segments of the
         * path are retraced in the reverse direction such that the
         * two sets of segments cancel each other out without any
         * interior area between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         *
         * @param pi the specified {@code PathIterator}
         * @param x the specified X coordinate
         * @param y the specified Y coordinate
         * @param w the width of the specified rectangular coordinates
         * @param h the height of the specified rectangular coordinates
         * @return {@code true} if the specified {@code PathIterator} and
         *         the interior of the specified set of rectangular
         *         coordinates intersect each other; {@code false} otherwise.
         */
        public static bool Intersects(PathIterator pi,
                int x, int y, int w, int h)
        {
            if (Double.IsNaN(x + w) || Double.IsNaN(y + h))
            {
                /* [xy]+[wh] is NaN if any of those values are NaN,
                 * or if adding the two together would produce NaN
                 * by virtue of adding opposing Infinte values.
                 * Since we need to add them below, their sum must
                 * not be NaN.
                 * We return false because NaN always produces a
                 * negative response to tests
                 */
                return false;
            }
            if (w <= 0 || h <= 0)
            {
                return false;
            }
            int mask = (pi.GetWindingRule() == WIND_NON_ZERO ? -1 : 2);
            int crossings = Curve.RectCrossingsForPath(pi, x, y, x + w, y + h);
            return (crossings == Curve.RECT_INTERSECTS ||
                    (crossings & mask) != 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the specified {@link PathIterator}
         * intersects the interior of a specified {@link Rectangle}.
         * <p>
         * This method provides a basic facility for implementors of
         * the {@link IShape} interface to implement support for the
         * {@link IShape#intersects(Rectangle)} method.
         * <p>
         * This method object may conservatively return true in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such a case may occur if some set of segments of the
         * path are retraced in the reverse direction such that the
         * two sets of segments cancel each other out without any
         * interior area between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         *
         * @param pi the specified {@code PathIterator}
         * @param r the specified {@code Rectangle}
         * @return {@code true} if the specified {@code PathIterator} and
         *         the interior of the specified {@code Rectangle}
         *         intersect each other; {@code false} otherwise.
         */
        public static bool Intersects(PathIterator pi, Rectangle r)
        {
            return Intersects(pi, r.X, r.Y, r.Width, r.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * <p>
         * This method object may conservatively return true in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such a case may occur if some set of segments of the
         * path are retraced in the reverse direction such that the
         * two sets of segments cancel each other out without any
         * interior area between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
         */
        public bool Intersects(int x, int y, int w, int h)
        {
            if (Double.IsNaN(x + w) || Double.IsNaN(y + h))
            {
                /* [xy]+[wh] is NaN if any of those values are NaN,
                 * or if adding the two together would produce NaN
                 * by virtue of adding opposing Infinte values.
                 * Since we need to add them below, their sum must
                 * not be NaN.
                 * We return false because NaN always produces a
                 * negative response to tests
                 */
                return false;
            }
            if (w <= 0 || h <= 0)
            {
                return false;
            }
            int mask = (_windingRule == WIND_NON_ZERO ? -1 : 2);
            int crossings = RectCrossings(x, y, x + w, y + h);
            return (crossings == Curve.RECT_INTERSECTS ||
                    (crossings & mask) != 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * <p>
         * This method object may conservatively return true in
         * cases where the specified rectangular area intersects a
         * segment of the path, but that segment does not represent a
         * boundary between the interior and exterior of the path.
         * Such a case may occur if some set of segments of the
         * path are retraced in the reverse direction such that the
         * two sets of segments cancel each other out without any
         * interior area between them.
         * To determine whether segments represent true boundaries of
         * the interior of the path would require extensive calculations
         * involving all of the segments of the path and the winding
         * rule and are thus beyond the scope of this implementation.
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
         * <p>
         * The iterator for this class is not multi-threaded safe,
         * which means that this {@code Path} class does not
         * guarantee that modifications to the geometry of this
         * {@code Path} object do not affect any iterations of
         * that geometry that are already in process.
         */
        public PathIterator GetPathIterator(AffineTransform at,
                int flatness)
        {
            return new FlatteningPathIterator(GetPathIterator(at), flatness);
        }

        /*
         * Support fields and methods for serializing the subclasses.
         */
        //    private const byte SERIAL_STORAGE_FLT_ARRAY = 0x30;
        //    private const byte SERIAL_STORAGE_DBL_ARRAY = 0x31;
        //
        //    private const byte SERIAL_SEG_FLT_MOVETO    = 0x40;
        //    private const byte SERIAL_SEG_FLT_LINETO    = 0x41;
        //    private const byte SERIAL_SEG_FLT_QUADTO    = 0x42;
        //    private const byte SERIAL_SEG_FLT_CUBICTO   = 0x43;
        //
        //    private const byte SERIAL_SEG_DBL_MOVETO    = 0x50;
        //    private const byte SERIAL_SEG_DBL_LINETO    = 0x51;
        //    private const byte SERIAL_SEG_DBL_QUADTO    = 0x52;
        //    private const byte SERIAL_SEG_DBL_CUBICTO   = 0x53;
        //
        //    private const byte SERIAL_SEG_CLOSE         = 0x60;
        //    private const byte SERIAL_PATH_END          = 0x61;

        abstract class Iterator : PathIterator
        {

            internal int _typeIdx;
            internal int _pointIdx;
            internal readonly Path _path;
            internal readonly int[] _curvecoords = new[] { 2, 2, 4, 6, 0 };

            protected Iterator(Path path)
            {
                _path = path;
            }

            public override int GetWindingRule()
            {
                return _path.GetWindingRule();
            }

            public override bool IsDone()
            {
                return (_typeIdx >= _path._numTypes);
            }

            public override void Next()
            {
                int type = _path._pointTypes[_typeIdx++];
                _pointIdx += _curvecoords[type];
            }
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds a point to the path by moving to the specified
         * coordinates specified in double precision.
         *
         * @param x the specified X coordinate
         * @param y the specified Y coordinate
         */
        public void MoveTo(int x, int y)
        {
            if (_numTypes > 0 && _pointTypes[_numTypes - 1] == SEG_MOVETO)
            {
                _intCoords[_numCoords - 2] = x;
                _intCoords[_numCoords - 1] = y;
            }
            else
            {
                NeedRoom(false, 2);
                _pointTypes[_numTypes++] = SEG_MOVETO;
                _intCoords[_numCoords++] = x;
                _intCoords[_numCoords++] = y;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds a point to the path by drawing a straight line from the
         * current coordinates to the new specified coordinates
         * specified in double precision.
         *
         * @param x the specified X coordinate
         * @param y the specified Y coordinate
         */
        public void LineTo(int x, int y)
        {
            NeedRoom(true, 2);
            _pointTypes[_numTypes++] = SEG_LINETO;
            _intCoords[_numCoords++] = x;
            _intCoords[_numCoords++] = y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds a curved segment, defined by two new points, to the path by
         * drawing a Quadratic curve that intersects both the current
         * coordinates and the specified coordinates {@code (X2,y2)},
         * using the specified point {@code (x1,y1)} as a quadratic
         * parametric control point.
         * All coordinates are specified in double precision.
         *
         * @param x1 the X coordinate of the quadratic control point
         * @param y1 the Y coordinate of the quadratic control point
         * @param X2 the X coordinate of the final end point
         * @param y2 the Y coordinate of the final end point
         */
        public void QuadTo(int x1, int y1,
                int x2, int y2)
        {
            NeedRoom(true, 4);
            _pointTypes[_numTypes++] = SEG_QUADTO;
            _intCoords[_numCoords++] = x1;
            _intCoords[_numCoords++] = y1;
            _intCoords[_numCoords++] = x2;
            _intCoords[_numCoords++] = y2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds a curved segment, defined by three new points, to the path by
         * drawing a B&eacute;zier curve that intersects both the current
         * coordinates and the specified coordinates {@code (x3,y3)},
         * using the specified points {@code (x1,y1)} and {@code (X2,y2)} as
         * B&eacute;zier control points.
         * All coordinates are specified in double precision.
         *
         * @param x1 the X coordinate of the first B&eacute;zier control point
         * @param y1 the Y coordinate of the first B&eacute;zier control point
         * @param X2 the X coordinate of the second B&eacute;zier control point
         * @param y2 the Y coordinate of the second B&eacute;zier control point
         * @param x3 the X coordinate of the final end point
         * @param y3 the Y coordinate of the final end point
         */
        public void CurveTo(int x1, int y1,
                int x2, int y2,
                int x3, int y3)
        {
            NeedRoom(true, 6);
            _pointTypes[_numTypes++] = SEG_CUBICTO;
            _intCoords[_numCoords++] = x1;
            _intCoords[_numCoords++] = y1;
            _intCoords[_numCoords++] = x2;
            _intCoords[_numCoords++] = y2;
            _intCoords[_numCoords++] = x3;
            _intCoords[_numCoords++] = y3;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Appends the geometry of the specified
         * {@link PathIterator} object
         * to the path, possibly connecting the new geometry to the existing
         * path segments with a line segment.
         * If the {@code connect} parameter is {@code true} and the
         * path is not empty then any initial {@code moveTo} in the
         * geometry of the appended {@code IShape} is turned into a
         * {@code lineTo} segment.
         * If the destination coordinates of such a connecting {@code lineTo}
         * segment match the ending coordinates of a currently open
         * subpath then the segment is omitted as superfluous.
         * The winding rule of the specified {@code IShape} is ignored
         * and the appended geometry is governed by the winding
         * rule specified for this path.
         *
         * @param pi the {@code PathIterator} whose geometry is appended to
         *           this path
         * @param connect a bool to control whether or not to turn an initial
         *                {@code moveTo} segment into a {@code lineTo} segment
         *                to connect the new geometry to the existing path
         */
        public void Append(PathIterator pi, bool connect)
        {
            int[] coords = new int[6];
            while (!pi.IsDone())
            {
                switch (pi.CurrentSegment(coords))
                {
                    case SEG_MOVETO:
                        if (!connect || _numTypes < 1 || _numCoords < 1)
                        {
                            MoveTo(coords[0], coords[1]);
                            break;
                        }
                        if (_pointTypes[_numTypes - 1] != SEG_CLOSE &&
                                _intCoords[_numCoords - 2] == coords[0] &&
                                _intCoords[_numCoords - 1] == coords[1])
                        {
                            // Collapse out initial moveto/lineto
                            break;
                        }
                        LineTo(coords[0], coords[1]);
                        break;
                    case SEG_LINETO:
                        LineTo(coords[0], coords[1]);
                        break;
                    case SEG_QUADTO:
                        QuadTo(coords[0], coords[1],
                                coords[2], coords[3]);
                        break;
                    case SEG_CUBICTO:
                        CurveTo(coords[0], coords[1],
                                coords[2], coords[3],
                                coords[4], coords[5]);
                        break;
                    case SEG_CLOSE:
                        ClosePath();
                        break;
                }
                pi.Next();
                connect = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms the geometry of this path using the specified
         * {@link AffineTransform}.
         * The geometry is transformed in place, which permanently changes the
         * boundary defined by this object.
         *
         * @param at the {@code AffineTransform} used to transform the area
         */
        public void Transform(AffineTransform at)
        {
            at.Transform(_intCoords, 0, _intCoords, 0, _numCoords / 2);
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
            int x1, y1, x2, y2;
            int i = _numCoords;
            if (i > 0)
            {
                y1 = y2 = _intCoords[--i];
                x1 = x2 = _intCoords[--i];
                while (i > 0)
                {
                    int y = _intCoords[--i];
                    int x = _intCoords[--i];
                    if (x < x1)
                    {
                        x1 = x;
                    }
                    if (y < y1)
                    {
                        y1 = y;
                    }
                    if (x > x2)
                    {
                        x2 = x;
                    }
                    if (y > y2)
                    {
                        y2 = y;
                    }
                }
            }
            else
            {
                x1 = y1 = x2 = y2 = 0;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * <p>
         * The iterator for this class is not multi-threaded safe,
         * which means that the {@code Path} class does not
         * guarantee that modifications to the geometry of this
         * {@code Path} object do not affect any iterations of
         * that geometry that are already in process.
         *
         * @param at an {@code AffineTransform}
         * @return a new {@code PathIterator} that iterates along the boundary
         *         of this {@code IShape} and provides access to the geometry
         *         of this {@code IShape}'s outline
         */
        public PathIterator GetPathIterator(AffineTransform at)
        {
            if (at == null)
            {
                return new CopyIterator(this);
            }
            return new TxIterator(this, at);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * To a SVG string.
         * @param shape the shape object.
         * @return a SVG string
         */
        public static String ToSVG(IShape shape)
        {
            PathIterator pathIterator = shape.GetPathIterator(null);
            StringBuilder svgString = new StringBuilder("<path d='");
            int[] coords = new int[6];
            int type;
            while (!pathIterator.IsDone())
            {
                type = pathIterator.CurrentSegment(coords);
                switch (type)
                {
                    case PathIterator.SEG_CLOSE:
                        svgString.Append("Z ");
                        break;
                    case PathIterator.SEG_CUBICTO:
                        svgString.Append("C ");
                        svgString.Append(coords[0] + " ");
                        svgString.Append(coords[1] + " ");
                        svgString.Append(coords[2] + " ");
                        svgString.Append(coords[3] + " ");
                        svgString.Append(coords[4] + " ");
                        svgString.Append(coords[5]);
                        break;
                    case PathIterator.SEG_LINETO:
                        svgString.Append("L ");
                        svgString.Append(coords[0] + " ");
                        svgString.Append(coords[1]);
                        break;
                    case PathIterator.SEG_MOVETO:
                        svgString.Append("M ");
                        svgString.Append(coords[0] + " ");
                        svgString.Append(coords[1]);
                        break;
                    case PathIterator.SEG_QUADTO:
                        svgString.Append("Q ");
                        svgString.Append(coords[0] + " ");
                        svgString.Append(coords[1] + " ");
                        svgString.Append(coords[2] + " ");
                        svgString.Append(coords[3]);
                        break;
                }

                pathIterator.Next();

            }
            svgString.Append("' />");
            return svgString.ToString();
        }    // For code simplicity, copy these constants to our namespace
        // and cast them to byte constants for easy storage.
        protected const byte SEG_MOVETO = PathIterator.SEG_MOVETO;
        protected const byte SEG_LINETO = PathIterator.SEG_LINETO;
        protected const byte SEG_QUADTO = PathIterator.SEG_QUADTO;
        protected const byte SEG_CUBICTO = PathIterator.SEG_CUBICTO;
        protected const byte SEG_CLOSE = PathIterator.SEG_CLOSE;
        internal byte[] _pointTypes;
        protected int _numTypes;
        protected int _numCoords;
        protected int _windingRule;
        protected const int INIT_SIZE = 20;
        protected const int EXPAND_MAX = 500;
        protected int[] _intCoords;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private int PointCrossings(int px, int py)
        {
            int movx, movy;
            int[] coords = _intCoords;
            int curx = movx = coords[0];
            int cury = movy = coords[1];
            int crossings = 0;
            int ci = 2;
            for (int i = 1; i < _numTypes; i++)
            {
                int endx;
                int endy;
                switch (_pointTypes[i])
                {
                    case PathIterator.SEG_MOVETO:
                        if (cury != movy)
                        {
                            crossings +=
                                    Curve.PointCrossingsForLine(px, py,
                                    curx, cury,
                                    movx, movy);
                        }
                        movx = curx = coords[ci++];
                        movy = cury = coords[ci++];
                        break;
                    case PathIterator.SEG_LINETO:
                        crossings +=
                                Curve.PointCrossingsForLine(px, py,
                                curx, cury,
                                endx = coords[ci++],
                                endy = coords[ci++]);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        crossings +=
                                Curve.PointCrossingsForQuad(px, py,
                                curx, cury,
                                coords[ci++],
                                coords[ci++],
                                endx = coords[ci++],
                                endy = coords[ci++],
                                0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CUBICTO:
                        crossings +=
                                Curve.PointCrossingsForCubic(px, py,
                                curx, cury,
                                coords[ci++],
                                coords[ci++],
                                coords[ci++],
                                coords[ci++],
                                endx = coords[ci++],
                                endy = coords[ci++],
                                0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CLOSE:
                        if (cury != movy)
                        {
                            crossings +=
                                    Curve.PointCrossingsForLine(px, py,
                                    curx, cury,
                                    movx, movy);
                        }
                        curx = movx;
                        cury = movy;
                        break;
                }
            }
            if (cury != movy)
            {
                crossings +=
                        Curve.PointCrossingsForLine(px, py,
                        curx, cury,
                        movx, movy);
            }
            return crossings;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private int[] CloneCoords(AffineTransform at)
        {
            int[] ret;
            if (at == null)
            {
                ret = new int[_intCoords.Length];
                Array.Copy(_intCoords, ret, ret.Length);

            }
            else
            {
                ret = new int[_intCoords.Length];
                at.Transform(_intCoords, 0, ret, 0, _numCoords / 2);
            }
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private Point GetPoint(int coordindex)
        {
            Point pt = new Point();
            pt.X = _intCoords[coordindex];
            pt.Y = _intCoords[coordindex + 1];
            return pt;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private void NeedRoom(bool needMove, int newCoords)
        {
            if (needMove && _numTypes == 0)
            {
                throw new IllegalPathStateException("missing initial moveto " +
                        "in path definition");
            }
            int size = _pointTypes.Length;
            if (_numTypes >= size)
            {
                int grow = size;
                if (grow > EXPAND_MAX)
                {
                    grow = EXPAND_MAX;
                }
                byte[] temp = new byte[size];
                Array.Copy(_pointTypes, temp, temp.Length);
                _pointTypes = new byte[size + grow];
                Array.Copy(temp, _pointTypes, temp.Length);
            }
            size = _intCoords.Length;
            if (_numCoords + newCoords > size)
            {
                int grow = size;
                if (grow > EXPAND_MAX * 2)
                {
                    grow = EXPAND_MAX * 2;
                }
                if (grow < newCoords)
                {
                    grow = newCoords;
                }
                int[] temp = new int[size];
                Array.Copy(_intCoords, temp, temp.Length);
                _intCoords = new int[size + grow];
                Array.Copy(temp, _intCoords, temp.Length);

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private int RectCrossings(int rxmin, int rymin,
                int rxmax, int rymax)
        {
            int[] coords = _intCoords;
            int movx, movy;
            int curx = movx = coords[0];
            int cury = movy = coords[1];
            int crossings = 0;
            int ci = 2;
            for (int i = 1;
                    crossings != Curve.RECT_INTERSECTS && i < _numTypes;
                    i++)
            {
                int endx;
                int endy;
                switch (_pointTypes[i])
                {
                    case PathIterator.SEG_MOVETO:
                        if (curx != movx || cury != movy)
                        {
                            crossings =
                                    Curve.RectCrossingsForLine(crossings,
                                    rxmin, rymin,
                                    rxmax, rymax,
                                    curx, cury,
                                    movx, movy);
                        }
                        // Count should always be a multiple of 2 here.
                        // assert((crossings & 1) != 0);
                        movx = curx = coords[ci++];
                        movy = cury = coords[ci++];
                        break;
                    case PathIterator.SEG_LINETO:
                        endx = coords[ci++];
                        endy = coords[ci++];
                        crossings =
                                Curve.RectCrossingsForLine(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                endx, endy);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        crossings =
                                Curve.RectCrossingsForQuad(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                coords[ci++],
                                coords[ci++],
                                endx = coords[ci++],
                                endy = coords[ci++],
                                0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CUBICTO:
                        crossings =
                                Curve.RectCrossingsForCubic(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                coords[ci++],
                                coords[ci++],
                                coords[ci++],
                                coords[ci++],
                                endx = coords[ci++],
                                endy = coords[ci++],
                                0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CLOSE:
                        if (curx != movx || cury != movy)
                        {
                            crossings =
                                    Curve.RectCrossingsForLine(crossings,
                                    rxmin, rymin,
                                    rxmax, rymax,
                                    curx, cury,
                                    movx, movy);
                        }
                        curx = movx;
                        cury = movy;
                        // Count should always be a multiple of 2 here.
                        // assert((crossings & 1) != 0);
                        break;
                }
            }
            if (crossings != Curve.RECT_INTERSECTS &&
                    (curx != movx || cury != movy))
            {
                crossings =
                        Curve.RectCrossingsForLine(crossings,
                        rxmin, rymin,
                        rxmax, rymax,
                        curx, cury,
                        movx, movy);
            }
            // Count should always be a multiple of 2 here.
            // assert((crossings & 1) != 0);
            return crossings;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        class CopyIterator : Iterator
        {
            readonly int[] _intCoords;

            internal CopyIterator(Path p2Dd)
                : base(p2Dd)
            {

                _intCoords = p2Dd._intCoords;
            }

            public override int CurrentSegment(int[] coords)
            {
                int type = _path._pointTypes[_typeIdx];
                int numCoords = _curvecoords[type];
                if (numCoords > 0)
                {
                    for (int i = 0; i < numCoords; i++)
                    {
                        coords[i] = _intCoords[_pointIdx + i];
                    }
                }
                return type;
            }
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 06NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        class TxIterator : Iterator
        {
            readonly int[] _intCoords;
            readonly AffineTransform _affine;

            internal TxIterator(Path p2Dd, AffineTransform at)
                : base(p2Dd)
            {

                _intCoords = p2Dd._intCoords;
                _affine = at;
            }

            public override int CurrentSegment(int[] coords)
            {
                int type = _path._pointTypes[_typeIdx];
                int numCoords = _curvecoords[type];
                if (numCoords > 0)
                {
                    _affine.Transform(_intCoords, _pointIdx,
                            coords, 0, numCoords / 2);
                }
                return type;
            }
        }
    }
}
