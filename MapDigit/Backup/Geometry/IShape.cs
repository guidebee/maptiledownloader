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
     * The <code>IShape</code> interface provides definitions for objects
     * that represent some form of geometric shape.  The <code>IShape</code>
     * is described by a {@link IPathIterator} object, which can express the
     * outline of the <code>IShape</code> as well as a rule for determining
     * how the outline divides the 2D plane into interior and exterior
     * points.  Each <code>IShape</code> object provides callbacks to get the
     * bounding box of the geometry, determine whether points or
     * rectangles lie partly or entirely within the interior
     * of the <code>IShape</code>, and retrieve a <code>IPathIterator</code>
     * object that describes the trajectory path of the <code>IShape</code>
     * outline.
     * <p>
     * <b>Definition of insideness:</b>
     * A point is considered to lie inside a
     * <code>IShape</code> if and only if:
     * <ul>
     * <li> it lies completely
     * inside the<code>IShape</code> boundary <i>or</i>
     * <li>
     * it lies exactly on the <code>IShape</code> boundary <i>and</i> the
     * space immediately adjacent to the
     * point in the increasing <code>X</code> direction is
     * entirely inside the boundary <i>or</i>
     * <li>
     * it lies exactly on a horizontal boundary segment <b>and</b> the
     * space immediately adjacent to the point in the
     * increasing <code>Y</code> direction is inside the boundary.
     * </ul>
     * <p>The <code>contains</code> and <code>intersects</code> methods
     * consider the interior of a <code>IShape</code> to be the area it
     * encloses as if it were filled.  This means that these methods
     * consider
     * unclosed shapes to be implicitly closed for the purpose of
     * determining if a shape contains or intersects a rectangle or if a
     * shape contains a point.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */ 
    public interface IShape
    {

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an integer {@link Rectangle} that completely encloses the
         * <code>IShape</code>.  Note that there is no guarantee that the
         * returned <code>Rectangle</code> is the smallest bounding box that
         * encloses the <code>IShape</code>, only that the <code>IShape</code>
         * lies entirely within the indicated  <code>Rectangle</code>.  The
         * returned <code>Rectangle</code> might also fail to completely
         * enclose the <code>IShape</code> if the <code>IShape</code> overflows
         * the limited range of the integer data type.  The
         * <code>getBounds2D</code> method generally returns a
         * tighter bounding box due to its greater flexibility in
         * representation.
         * @return an integer <code>Rectangle</code> that completely encloses
         *                 the <code>IShape</code>.
         */
        Rectangle GetBounds();

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified coordinates are inside the boundary of the
         * <code>IShape</code>.
         * @param x the specified X coordinate to be tested
         * @param y the specified Y coordinate to be tested
         * @return <code>true</code> if the specified coordinates are inside
         *         the <code>IShape</code> boundary; <code>false</code>
         *         otherwise.
         */
        bool Contains(int x, int y);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if a specified {@link Point} is inside the boundary
         * of the <code>IShape</code>.
         * @param p the specified <code>Point</code> to be tested
         * @return <code>true</code> if the specified <code>Point</code> is
         *          inside the boundary of the <code>IShape</code>;
         *		<code>false</code> otherwise.
         */
        bool Contains(Point p);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the <code>IShape</code> entirely contains
         * the specified rectangular area.  All coordinates that lie inside
         * the rectangular area must lie within the <code>IShape</code> for the
         * entire rectanglar area to be considered contained within the
         * <code>IShape</code>.
         * <p>
         * The {@code IShape.contains()} method allows a {@code IShape}
         * implementation to conservatively return {@code false} when:
         * <ul>
         * <li>
         * the <code>intersect</code> method returns <code>true</code> and
         * <li>
         * the calculations to determine whether or not the
         * <code>IShape</code> entirely contains the rectangular area are
         * prohibitively expensive.
         * </ul>
         * This means that for some {@code Shapes} this method might
         * return {@code false} even though the {@code IShape} contains
         * the rectangular area.
         * The {@link com.mapdigit.drawing.geometry.Area Area} class performs
         * more accurate geometric computations than most
         * {@code IShape} objects and therefore can be used if a more precise
         * answer is required.
         *
         * @param x the X coordinate of the upper-left corner
         *          of the specified rectangular area
         * @param y the Y coordinate of the upper-left corner
         *          of the specified rectangular area
         * @param w the width of the specified rectangular area
         * @param h the height of the specified rectangular area
         * @return <code>true</code> if the interior of the <code>IShape</code>
         * 		entirely contains the specified rectangular area;
         * 		<code>false</code> otherwise or, if the <code>IShape</code>
         *		contains the rectangular area and the
         *		<code>intersects</code> method returns <code>true</code>
         * 		and the containment calculations would be too expensive to
         * 		perform.
         */
        bool Contains(int x, int y, int w, int h);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the <code>IShape</code> entirely contains the
         * specified <code>Rectangle</code>.
         * The {@code IShape.contains()} method allows a {@code IShape}
         * implementation to conservatively return {@code false} when:
         * <ul>
         * <li>
         * the <code>intersect</code> method returns <code>true</code> and
         * <li>
         * the calculations to determine whether or not the
         * <code>IShape</code> entirely contains the <code>Rectangle</code>
         * are prohibitively expensive.
         * </ul>
         * This means that for some {@code Shapes} this method might
         * return {@code false} even though the {@code IShape} contains
         * the {@code Rectangle}.
         * The {@link com.mapdigit.drawing.geometry.Area Area} class performs
         * more accurate geometric computations than most
         * {@code IShape} objects and therefore can be used if a more precise
         * answer is required.
         *
         * @param r The specified <code>Rectangle</code>
         * @return <code>true</code> if the interior of the <code>IShape</code>
         *          entirely contains the <code>Rectangle</code>;
         *          <code>false</code> otherwise or, if the <code>IShape</code>
         *          contains the <code>Rectangle</code> and the
         *          <code>intersects</code> method returns <code>true</code>
         *          and the containment calculations would be too expensive to
         *          perform.
         */
        bool Contains(Rectangle r);


        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the <code>IShape</code> intersects the
         * interior of a specified rectangular area.
         * The rectangular area is considered to intersect the <code>IShape</code>
         * if any point is contained in both the interior of the
         * <code>IShape</code> and the specified rectangular area.
         * <p>
         * The {@code IShape.intersects()} method allows a {@code IShape}
         * implementation to conservatively return {@code true} when:
         * <ul>
         * <li>
         * there is a high probability that the rectangular area and the
         * <code>IShape</code> intersect, but
         * <li>
         * the calculations to accurately determine this intersection
         * are prohibitively expensive.
         * </ul>
         * This means that for some {@code Shapes} this method might
         * return {@code true} even though the rectangular area does not
         * intersect the {@code IShape}.
         * The {@link com.mapdigit.drawing.geometry.Area Area} class performs
         * more accurate computations of geometric intersection than most
         * {@code IShape} objects and therefore can be used if a more precise
         * answer is required.
         *
         * @param x the X coordinate of the upper-left corner
         *          of the specified rectangular area
         * @param y the Y coordinate of the upper-left corner
         *          of the specified rectangular area
         * @param w the width of the specified rectangular area
         * @param h the height of the specified rectangular area
         * @return <code>true</code> if the interior of the <code>IShape</code> and
         * 		the interior of the rectangular area intersect, or are
         * 		both highly likely to intersect and intersection calculations
         * 		would be too expensive to perform; <code>false</code> otherwise.
         */
        bool Intersects(int x, int y, int w, int h);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the <code>IShape</code> intersects the
         * interior of a specified <code>Rectangle</code>.
         * The {@code IShape.intersects()} method allows a {@code IShape}
         * implementation to conservatively return {@code true} when:
         * <ul>
         * <li>
         * there is a high probability that the <code>Rectangle</code> and the
         * <code>IShape</code> intersect, but
         * <li>
         * the calculations to accurately determine this intersection
         * are prohibitively expensive.
         * </ul>
         * This means that for some {@code Shapes} this method might
         * return {@code true} even though the {@code Rectangle} does not
         * intersect the {@code IShape}.
         * The {@link com.mapdigit.drawing.geometry.Area Area} class performs
         * more accurate computations of geometric intersection than most
         * {@code IShape} objects and therefore can be used if a more precise
         * answer is required.
         *
         * @param r the specified <code>Rectangle</code>
         * @return <code>true</code> if the interior of the <code>IShape</code> and
         * 		the interior of the specified <code>Rectangle</code>
         *		intersect, or are both highly likely to intersect and intersection
         *		calculations would be too expensive to perform; <code>false</code>
         * 		otherwise.
         */
        bool Intersects(Rectangle r);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iterator object that iterates along the
         * <code>IShape</code> boundary and provides access to the geometry of the
         * <code>IShape</code> outline.  If an optional {@link AffineTransform}
         * is specified, the coordinates returned in the iteration are
         * transformed accordingly.
         * <p>
         * Each call to this method returns a fresh <code>IPathIterator</code>
         * object that traverses the geometry of the <code>IShape</code> object
         * independently from any other <code>IPathIterator</code> objects in use
         * at the same time.
         * <p>
         * It is recommended, but not guaranteed, that objects
         * implementing the <code>IShape</code> interface isolate iterations
         * that are in process from any changes that might occur to the original
         * object's geometry during such iterations.
         *
         * @param at an optional <code>AffineTransform</code> to be applied to the
         * 		coordinates as they are returned in the iteration, or
         *		<code>null</code> if untransformed coordinates are desired
         * @return a new <code>IPathIterator</code> object, which independently
         *		traverses the geometry of the <code>IShape</code>.
         */
        PathIterator GetPathIterator(AffineTransform at);

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iterator object that iterates along the <code>IShape</code>
         * boundary and provides access to a flattened view of the
         * <code>IShape</code> outline geometry.
         * <p>
         * Only SEG_MOVETO, SEG_LINETO, and SEG_CLOSE point types are
         * returned by the iterator.
         * <p>
         * If an optional <code>AffineTransform</code> is specified,
         * the coordinates returned in the iteration are transformed
         * accordingly.
         * <p>
         * The amount of subdivision of the curved segments is controlled
         * by the <code>flatness</code> parameter, which specifies the
         * maximum distance that any point on the unflattened transformed
         * curve can deviate from the returned flattened path segments.
         * that a limit on the accuracy of the flattened path might be
         * silently imposed, causing very small flattening parameters to be
         * treated as larger values.  This limit, if there is one, is
         * defined by the particular implementation that is used.
         * <p>
         * Each call to this method returns a fresh <code>IPathIterator</code>
         * object that traverses the <code>IShape</code> object geometry
         * independently from any other <code>IPathIterator</code> objects in use at
         * the same time.
         * <p>
         * It is recommended, but not guaranteed, that objects
         * implementing the <code>IShape</code> interface isolate iterations
         * that are in process from any changes that might occur to the original
         * object's geometry during such iterations.
         *
         * @param at an optional <code>AffineTransform</code> to be applied to the
         * 		coordinates as they are returned in the iteration, or
         *		<code>null</code> if untransformed coordinates are desired
         * @param flatness the maximum distance that the line segments used to
         *          approximate the curved segments are allowed to deviate
         *          from any point on the original curve
         * @return a new <code>IPathIterator</code> that independently traverses
         *         a flattened view of the geometry of the  <code>IShape</code>.
         */
        PathIterator GetPathIterator(AffineTransform at, int flatness);
    }

}
