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
     * A quadratic parametric curve segment specified with 
     * {@code int} coordinates.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class QuadCurve : IShape
    {

        /**
         * The X coordinate of the start point of the quadratic curve
         * segment.
         */
        public double X1;
        /**
         * The Y coordinate of the start point of the quadratic curve
         * segment.
         */
        public double Y1;
        /**
         * The X coordinate of the control point of the quadratic curve
         * segment.
         */
        public double Ctrlx;
        /**
         * The Y coordinate of the control point of the quadratic curve
         * segment.
         */
        public double Ctrly;
        /**
         * The X coordinate of the end point of the quadratic curve
         * segment.
         */
        public double X2;
        /**
         * The Y coordinate of the end point of the quadratic curve
         * segment.
         */
        public double Y2;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>QuadCurve</code> with
         * coordinates (0, 0, 0, 0, 0, 0).
         */
        public QuadCurve()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a <code>QuadCurve</code> from the
         * specified {@code double} coordinates.
         *
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param ctrlx the X coordinate of the control point
         * @param ctrly the Y coordinate of the control point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         */
        public QuadCurve(double x1, double y1,
                double ctrlx, double ctrly,
                double x2, double y2)
        {
            SetCurve(x1, y1, ctrlx, ctrly, x2, y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the start point in
         * <code>double</code> in precision.
         * @return the X coordinate of the start point.
         */
        public int GetX1()
        {
            return (int)(X1 + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the start point in
         * <code>double</code> precision.
         * @return the Y coordinate of the start point.
         */
        public int GetY1()
        {
            return (int)(Y1 + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the start point.
         * @return a <code>Point</code> that is the start point of this
         * 		<code>QuadCurve</code>.
         */
        public Point GetP1()
        {
            return new Point((int)(X1 + .5), (int)(Y1 + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the control point in
         * <code>double</code> precision.
         * @return X coordinate the control point
         */
        public int GetCtrlX()
        {
            return (int)(Ctrlx + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the control point in
         * <code>double</code> precision.
         * @return the Y coordinate of the control point.
         */
        public int GetCtrlY()
        {
            return (int)(Ctrly + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the control point.
         * @return a <code>Point2D</code> that is the control point of this
         * 		<code>Point2D</code>.
         */
        public Point GetCtrlPt()
        {
            return new Point((int)(Ctrlx + .5), (int)(Ctrly + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the control point.
         * @return a <code>Point</code> that is the control point of this
         * 		<code>Point</code>.
         */
        public int GetX2()
        {
            return (int)(X2 + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the end point in
         * <code>double</code> precision.
         * @return the Y coordinate of the end point.
         */
        public int GetY2()
        {
            return (int)(Y2 + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the end point.
         * @return a <code>Point</code> object that is the end point
         * 		of this <code>Point2D</code>.
         */
        public Point GetP2()
        {
            return new Point((int)(X2 + .5), (int)(Y2 + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points and control point of this curve
         * to the specified <code>double</code> coordinates.
         *
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param ctrlx the X coordinate of the control point
         * @param ctrly the Y coordinate of the control point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         */
        public void SetCurve(double x1, double y1,
                double ctrlx, double ctrly,
                double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            Ctrlx = ctrlx;
            Ctrly = ctrly;
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
         * {@inheritDoc}
         */
        public Rectangle GetBounds()
        {
            double left = Math.Min(Math.Min(X1, X2), Ctrlx);
            double top = Math.Min(Math.Min(Y1, Y2), Ctrly);
            double right = Math.Max(Math.Max(X1, X2), Ctrlx);
            double bottom = Math.Max(Math.Max(Y1, Y2), Ctrly);
            return new Rectangle((int)(left + .5),
                    (int)(top + .5),
                    (int)(right - left + .5),
                    (int)(bottom - top + .5));
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points and control points of this
         * <code>QuadCurve</code> to the <code>double</code> coordinates at
         * the specified offset in the specified array.
         * @param coords the array containing coordinate values
         * @param offset the index into the array from which to start
         *		getting the coordinate values and assigning them to this
         *		<code>QuadCurve</code>
         */
        public void SetCurve(double[] coords, int offset)
        {
            SetCurve(coords[offset + 0], coords[offset + 1],
                    coords[offset + 2], coords[offset + 3],
                    coords[offset + 4], coords[offset + 5]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points and control point of this
         * <code>QuadCurve</code> to the specified <code>Point</code>
         * coordinates.
         * @param p1 the start point
         * @param cp the control point
         * @param p2 the end point
         */
        public void SetCurve(Point p1, Point cp, Point p2)
        {
            SetCurve(p1.GetX(), p1.GetY(),
                    cp.GetX(), cp.GetY(),
                    p2.GetX(), p2.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points and control points of this
         * <code>QuadCurve</code> to the coordinates of the
         * <code>Point</code> objects at the specified offset in
         * the specified array.
         * @param pts an array containing <code>Point</code> that define
         *		coordinate values
         * @param offset the index into <code>pts</code> from which to start
         *		getting the coordinate values and assigning them to this
         *		<code>QuadCurve</code>
         */
        public void SetCurve(Point[] pts, int offset)
        {
            SetCurve(pts[offset + 0].GetX(), pts[offset + 0].GetY(),
                    pts[offset + 1].GetX(), pts[offset + 1].GetY(),
                    pts[offset + 2].GetX(), pts[offset + 2].GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the end points and control point of this
         * <code>QuadCurve</code> to the same as those in the specified
         * <code>QuadCurve</code>.
         * @param c the specified <code>QuadCurve</code>
         */
        public void SetCurve(QuadCurve c)
        {
            SetCurve(c.GetX1(), c.GetY1(),
                    c.GetCtrlX(), c.GetCtrlY(),
                    c.GetX2(), c.GetY2());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the flatness, or maximum distance of a
         * control point from the line connecting the end points, of the
         * quadratic curve specified by the indicated control points.
         *
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param ctrlx the X coordinate of the control point
         * @param ctrly the Y coordinate of the control point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         * @return the square of the flatness of the quadratic curve
         *		defined by the specified coordinates.
         */
        public static int GetFlatnessSq(int x1, int y1,
                int ctrlx, int ctrly,
                int x2, int y2)
        {
            return Line.PtSegDistSq(x1, y1, x2, y2, ctrlx, ctrly);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the flatness, or maximum distance of a
         * control point from the line connecting the end points, of the
         * quadratic curve specified by the indicated control points.
         *
         * @param x1 the X coordinate of the start point
         * @param y1 the Y coordinate of the start point
         * @param ctrlx the X coordinate of the control point
         * @param ctrly the Y coordinate of the control point
         * @param X2 the X coordinate of the end point
         * @param y2 the Y coordinate of the end point
         * @return the flatness of the quadratic curve defined by the
         *		specified coordinates.
         */
        public static int GetFlatness(int x1, int y1,
                int ctrlx, int ctrly,
                int x2, int y2)
        {
            return Line.PtSegDist(x1, y1, x2, y2, ctrlx, ctrly);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the flatness, or maximum distance of a
         * control point from the line connecting the end points, of the
         * quadratic curve specified by the control points stored in the
         * indicated array at the indicated index.
         * @param coords an array containing coordinate values
         * @param offset the index into <code>coords</code> from which to
         *		to start getting the values from the array
         * @return the flatness of the quadratic curve that is defined by the
         * 		values in the specified array at the specified index.
         */
        public static int GetFlatnessSq(int[] coords, int offset)
        {
            return Line.PtSegDistSq(coords[offset + 0], coords[offset + 1],
                    coords[offset + 4], coords[offset + 5],
                    coords[offset + 2], coords[offset + 3]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the flatness, or maximum distance of a
         * control point from the line connecting the end points, of the
         * quadratic curve specified by the control points stored in the
         * indicated array at the indicated index.
         * @param coords an array containing coordinate values
         * @param offset the index into <code>coords</code> from which to
         *		start getting the coordinate values
         * @return the flatness of a quadratic curve defined by the
         *		specified array at the specified offset.
         */
        public static int GetFlatness(int[] coords, int offset)
        {
            return Line.PtSegDist(coords[offset + 0], coords[offset + 1],
                    coords[offset + 4], coords[offset + 5],
                    coords[offset + 2], coords[offset + 3]);
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the flatness, or maximum distance of a
         * control point from the line connecting the end points, of this
         * <code>QuadCurve</code>.
         * @return the square of the flatness of this
         *		<code>QuadCurve</code>.
         */
        public double GetFlatnessSq()
        {
            return Line.PtSegDistSq(GetX1(), GetY1(),
                    GetX2(), GetY2(),
                    GetCtrlX(), GetCtrlY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the flatness, or maximum distance of a
         * control point from the line connecting the end points, of this
         * <code>QuadCurve</code>.
         * @return the flatness of this <code>QuadCurve</code>.
         */
        public double GetFlatness()
        {
            return Line.PtSegDist(GetX1(), GetY1(),
                    GetX2(), GetY2(),
                    GetCtrlX(), GetCtrlY());
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Subdivides this <code>QuadCurve</code> and stores the resulting
         * two subdivided curves into the <code>left</code> and
         * <code>right</code> curve parameters.
         * Either or both of the <code>left</code> and <code>right</code>
         * objects can be the same as this <code>QuadCurve</code> or
         * <code>null</code>.
         * @param left the <code>QuadCurve</code> object for storing the
         * left or first half of the subdivided curve
         * @param right the <code>QuadCurve</code> object for storing the
         * right or second half of the subdivided curve
         */
        public void Subdivide(QuadCurve left, QuadCurve right)
        {
            Subdivide(this, left, right);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Subdivides the quadratic curve specified by the <code>src</code>
         * parameter and stores the resulting two subdivided curves into the
         * <code>left</code> and <code>right</code> curve parameters.
         * Either or both of the <code>left</code> and <code>right</code>
         * objects can be the same as the <code>src</code> object or
         * <code>null</code>.
         * @param src the quadratic curve to be subdivided
         * @param left the <code>QuadCurve</code> object for storing the
         *		left or first half of the subdivided curve
         * @param right the <code>QuadCurve</code> object for storing the
         *		right or second half of the subdivided curve
         */
        public static void Subdivide(QuadCurve src,
                QuadCurve left,
                QuadCurve right)
        {
            double x1 = src.GetX1();
            double y1 = src.GetY1();
            double ctrlx = src.GetCtrlX();
            double ctrly = src.GetCtrlY();
            double x2 = src.GetX2();
            double y2 = src.GetY2();
            double ctrlx1 = (x1 + ctrlx) / 2.0;
            double ctrly1 = (y1 + ctrly) / 2.0;
            double ctrlx2 = (x2 + ctrlx) / 2.0;
            double ctrly2 = (y2 + ctrly) / 2.0;
            ctrlx = (ctrlx1 + ctrlx2) / 2.0;
            ctrly = (ctrly1 + ctrly2) / 2.0;
            if (left != null)
            {
                left.SetCurve(x1, y1, ctrlx1, ctrly1, ctrlx, ctrly);
            }
            if (right != null)
            {
                right.SetCurve(ctrlx, ctrly, ctrlx2, ctrly2, x2, y2);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Subdivides the quadratic curve specified by the coordinates
         * stored in the <code>src</code> array at indices
         * <code>srcoff</code> through <code>srcoff</code>&nbsp;+&nbsp;5
         * and stores the resulting two subdivided curves into the two
         * result arrays at the corresponding indices.
         * Either or both of the <code>left</code> and <code>right</code>
         * arrays can be <code>null</code> or a reference to the same array
         * and offset as the <code>src</code> array.
         * Note that the last point in the first subdivided curve is the
         * same as the first point in the second subdivided curve.  Thus,
         * it is possible to pass the same array for <code>left</code> and
         * <code>right</code> and to use offsets such that
         * <code>rightoff</code> equals <code>leftoff</code> + 4 in order
         * to avoid allocating extra storage for this common point.
         * @param src the array holding the coordinates for the source curve
         * @param srcoff the offset into the array of the beginning of the
         * the 6 source coordinates
         * @param left the array for storing the coordinates for the first
         * half of the subdivided curve
         * @param leftoff the offset into the array of the beginning of the
         * the 6 left coordinates
         * @param right the array for storing the coordinates for the second
         * half of the subdivided curve
         * @param rightoff the offset into the array of the beginning of the
         * the 6 right coordinates
         */
        public static void Subdivide(double[] src, int srcoff,
                double[] left, int leftoff,
                double[] right, int rightoff)
        {
            double x1 = src[srcoff + 0];
            double y1 = src[srcoff + 1];
            double ctrlx = src[srcoff + 2];
            double ctrly = src[srcoff + 3];
            double x2 = src[srcoff + 4];
            double y2 = src[srcoff + 5];
            if (left != null)
            {
                left[leftoff + 0] = x1;
                left[leftoff + 1] = y1;
            }
            if (right != null)
            {
                right[rightoff + 4] = x2;
                right[rightoff + 5] = y2;
            }
            x1 = (x1 + ctrlx) / 2.0;
            y1 = (y1 + ctrly) / 2.0;
            x2 = (x2 + ctrlx) / 2.0;
            y2 = (y2 + ctrly) / 2.0;
            ctrlx = (x1 + x2) / 2.0;
            ctrly = (y1 + y2) / 2.0;
            if (left != null)
            {
                left[leftoff + 2] = x1;
                left[leftoff + 3] = y1;
                left[leftoff + 4] = ctrlx;
                left[leftoff + 5] = ctrly;
            }
            if (right != null)
            {
                right[rightoff + 0] = ctrlx;
                right[rightoff + 1] = ctrly;
                right[rightoff + 2] = x2;
                right[rightoff + 3] = y2;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Solves the quadratic whose coefficients are in the <code>eqn</code>
         * array and places the non-complex roots back into the same array,
         * returning the number of roots.  The quadratic solved is represented
         * by the equation:
         * <pre>
         *     eqn = {C, B, A};
         *     ax^2 + bx + c = 0
         * </pre>
         * A return Value of <code>-1</code> is used to distinguish a constant
         * equation, which might be always 0 or never 0, from an equation that
         * has no zeroes.
         * @param eqn the array that contains the quadratic coefficients
         * @return the number of roots, or <code>-1</code> if the equation is
         *		a constant
         */
        public static int SolveQuadratic(double[] eqn)
        {
            return SolveQuadratic(eqn, eqn);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Solves the quadratic whose coefficients are in the <code>eqn</code>
         * array and places the non-complex roots into the <code>res</code>
         * array, returning the number of roots.
         * The quadratic solved is represented by the equation:
         * <pre>
         *     eqn = {C, B, A};
         *     ax^2 + bx + c = 0
         * </pre>
         * A return Value of <code>-1</code> is used to distinguish a constant
         * equation, which might be always 0 or never 0, from an equation that
         * has no zeroes.
         * @param eqn the specified array of coefficients to use to solve
         *        the quadratic equation
         * @param res the array that contains the non-complex roots
         *        resulting from the solution of the quadratic equation
         * @return the number of roots, or <code>-1</code> if the equation is
         *	a constant.
         */
        public static int SolveQuadratic(double[] eqn, double[] res)
        {
            double a = eqn[2];
            double b = eqn[1];
            double c = eqn[0];
            int roots = 0;
            if (a == 0.0)
            {
                // The quadratic parabola has degenerated to a line.
                if (b == 0.0)
                {
                    // The line has degenerated to a constant.
                    return -1;
                }
                res[roots++] = -c / b;
            }
            else
            {
                // From Numerical Recipes, 5.6, Quadratic and Cubic Equations
                double d = b * b - 4.0 * a * c;
                if (d < 0.0)
                {
                    // If d < 0.0, then there are no roots
                    return 0;
                }
                d = Math.Sqrt(d);
                // For accuracy, calculate one root using:
                //     (-b +/- d) / 2a
                // and the other using:
                //     2c / (-b +/- d)
                // Choose the sign of the +/- so that b+d gets larger in magnitude
                if (b < 0.0)
                {
                    d = -d;
                }
                double q = (b + d) / -2.0;
                // We already tested a for being 0 above
                res[roots++] = q / a;
                if (q != 0.0)
                {
                    res[roots++] = c / q;
                }
            }
            return roots;
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

            double xp1 = GetX1();
            double yp1 = GetY1();
            double xc = GetCtrlX();
            double yc = GetCtrlY();
            double xp2 = GetX2();
            double yp2 = GetY2();

            /*
             * We have a convex shape bounded by quad curve Pc(t)
             * and ine Pl(t).
             *
             *     P1 = (x1, y1) - start point of curve
             *     P2 = (X2, y2) - end point of curve
             *     Pc = (xc, yc) - control point
             *
             *     Pq(t) = P1*(1 - t)^2 + 2*Pc*t*(1 - t) + P2*t^2 =
             *           = (P1 - 2*Pc + P2)*t^2 + 2*(Pc - P1)*t + P1
             *     Pl(t) = P1*(1 - t) + P2*t
             *     t = [0:1]
             *
             *     P = (x, y) - point of interest
             *
             * Let's look at second derivative of quad curve equation:
             *
             *     Pq''(t) = 2 * (P1 - 2 * Pc + P2) = Pq''
             *     It's constant vector.
             *
             * Let's draw a line through P to be parallel to this
             * vector and find the intersection of the quad curve
             * and the line.
             *
             * Pq(t) is point of intersection if system of equations
             * below has the solution.
             *
             *     L(s) = P + Pq''*s == Pq(t)
             *     Pq''*s + (P - Pq(t)) == 0
             *
             *     | xq''*s + (x - xq(t)) == 0
             *     | yq''*s + (y - yq(t)) == 0
             *
             * This system has the solution if rank of its matrix equals to 1.
             * That is, determinant of the matrix should be zero.
             *
             *     (y - yq(t))*xq'' == (x - xq(t))*yq''
             *
             * Let's solve this equation with 't' variable.
             * Also let kx = x1 - 2*xc + X2
             *          ky = y1 - 2*yc + y2
             *
             *     t0q = (1/2)*((x - x1)*ky - (y - y1)*kx) /
             *                 ((xc - x1)*ky - (yc - y1)*kx)
             *
             * Let's do the same for our line Pl(t):
             *
             *     t0l = ((x - x1)*ky - (y - y1)*kx) /
             *           ((X2 - x1)*ky - (y2 - y1)*kx)
             *
             * It's easy to check that t0q == t0l. This fact means
             * we can compute t0 only one time.
             *
             * In case t0 < 0 or t0 > 1, we have an intersections outside
             * of shape bounds. So, P is definitely out of shape.
             *
             * In case t0 is inside [0:1], we should calculate Pq(t0)
             * and Pl(t0). We have three points for now, and all of them
             * lie on one line. So, we just need to detect, is our point
             * of interest between points of intersections or not.
             *
             * If the denominator in the t0q and t0l equations is
             * zero, then the points must be collinear and so the
             * curve is degenerate and encloses no area.  Thus the
             * result is false.
             */
            double kx = xp1 - 2 * xc + xp2;
            double ky = yp1 - 2 * yc + yp2;
            double dx = x - xp1;
            double dy = y - yp1;
            double dxl = xp2 - xp1;
            double dyl = yp2 - yp1;

            double t0 = (dx * ky - dy * kx) / (dxl * ky - dyl * kx);
            if (t0 < 0 || t0 > 1 || t0 != t0)
            {
                return false;
            }

            double xb = kx * t0 * t0 + 2 * (xc - xp1) * t0 + xp1;
            double yb = ky * t0 * t0 + 2 * (yc - yp1) * t0 + yp1;
            double xl = dxl * t0 + xp1;
            double yl = dyl * t0 + yp1;

            return (x >= xb && x < xl) ||
                    (x >= xl && x < xb) ||
                    (y >= yb && y < yl) ||
                    (y >= yl && y < yb);
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
         * Fill an array with the coefficients of the parametric equation
         * in t, ready for solving against val with solveQuadratic.
         * We currently have:
         *     val = Py(t) = C1*(1-t)^2 + 2*CP*t*(1-t) + C2*t^2
         *                 = C1 - 2*C1*t + C1*t^2 + 2*CP*t - 2*CP*t^2 + C2*t^2
         *                 = C1 + (2*CP - 2*C1)*t + (C1 - 2*CP + C2)*t^2
         *               0 = (C1 - val) + (2*CP - 2*C1)*t + (C1 - 2*CP + C2)*t^2
         *               0 = C + Bt + At^2
         *     C = C1 - val
         *     B = 2*CP - 2*C1
         *     A = C1 - 2*CP + C2
         */
        private static void FillEqn(double[] eqn, double val,
                double c1, double cp, double c2)
        {
            eqn[0] = c1 - val;
            eqn[1] = cp + cp - c1 - c1;
            eqn[2] = c1 - cp - cp + c2;
            return;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Evaluate the t values in the first num slots of the vals[] array
         * and place the evaluated values back into the same array.  Only
         * evaluate t values that are within the range <0, 1>, including
         * the 0 and 1 ends of the range iff the include0 or include1
         * booleans are true.  If an "inflection" equation is handed in,
         * then any points which represent a point of inflection for that
         * quadratic equation are also ignored.
         */
        private static int EvalQuadratic(double[] vals, int num,
                bool include0,
                bool include1,
                double[] inflect,
                double c1, double ctrl, double c2)
        {
            int j = 0;
            for (int i = 0; i < num; i++)
            {
                double t = vals[i];
                if ((include0 ? t >= 0 : t > 0) &&
                        (include1 ? t <= 1 : t < 1) &&
                        (inflect == null ||
                        inflect[1] + 2 * inflect[2] * t != 0))
                {
                    double u = 1 - t;
                    vals[j++] = c1 * u * u + 2 * ctrl * t * u + c2 * t * t;
                }
            }
            return j;
        }
        private const int BELOW = -2;
        private const int LOWEDGE = -1;
        private const int INSIDE = 0;
        private const int HIGHEDGE = 1;
        private const int ABOVE = 2;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determine where coord lies with respect to the range from
         * low to high.  It is assumed that low <= high.  The return
         * Value is one of the 5 values BELOW, LOWEDGE, INSIDE, HIGHEDGE,
         * or ABOVE.
         */
        private static int GetTag(double coord, double low, double high)
        {
            if (coord <= low)
            {
                return (coord < low ? BELOW : LOWEDGE);
            }
            if (coord >= high)
            {
                return (coord > high ? ABOVE : HIGHEDGE);
            }
            return INSIDE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determine if the pttag represents a coordinate that is already
         * in its test range, or is on the border with either of the two
         * opttags representing another coordinate that is "towards the
         * inside" of that test range.  In other words, are either of the
         * two "opt" points "drawing the pt inward"?
         */
        private static bool Inwards(int pttag, int opt1Tag, int opt2Tag)
        {
            switch (pttag)
            {
                default:
                    return false;
                case LOWEDGE:
                    return (opt1Tag >= INSIDE || opt2Tag >= INSIDE);
                case INSIDE:
                    return true;
                case HIGHEDGE:
                    return (opt1Tag <= INSIDE || opt2Tag <= INSIDE);
            }
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
            // Trivially reject non-existant rectangles
            if (w <= 0 || h <= 0)
            {
                return false;
            }

            // Trivially accept if either endpoint is inside the rectangle
            // (not on its border since it may end there and not go inside)
            // Record where they lie with respect to the rectangle.
            //     -1 => left, 0 => inside, 1 => right
            double xp1 = GetX1();
            int x1Tag = GetTag(xp1, x, x + w);
            int y1Tag = GetTag(Y1, y, y + h);
            if (x1Tag == INSIDE && y1Tag == INSIDE)
            {
                return true;
            }
            double xp2 = GetX2();
            double yp2 = GetY2();
            int x2Tag = GetTag(xp2, x, x + w);
            int y2Tag = GetTag(yp2, y, y + h);
            if (x2Tag == INSIDE && y2Tag == INSIDE)
            {
                return true;
            }
            double ctrlpx = GetCtrlX();
            double ctrlpy = GetCtrlY();
            int ctrlxtag = GetTag(ctrlpx, x, x + w);
            int ctrlytag = GetTag(ctrlpy, y, y + h);

            // Trivially reject if all points are entirely to one side of
            // the rectangle.
            if (x1Tag < INSIDE && x2Tag < INSIDE && ctrlxtag < INSIDE)
            {
                return false;	// All points left
            }
            if (y1Tag < INSIDE && y2Tag < INSIDE && ctrlytag < INSIDE)
            {
                return false;	// All points above
            }
            if (x1Tag > INSIDE && x2Tag > INSIDE && ctrlxtag > INSIDE)
            {
                return false;	// All points right
            }
            if (y1Tag > INSIDE && y2Tag > INSIDE && ctrlytag > INSIDE)
            {
                return false;	// All points below
            }

            // Test for endpoints on the edge where either the segment
            // or the curve is headed "inwards" from them
            // Note: These tests are a superset of the fast endpoint tests
            //       above and thus repeat those tests, but take more time
            //       and cover more cases
            if (Inwards(x1Tag, x2Tag, ctrlxtag) &&
                    Inwards(y1Tag, y2Tag, ctrlytag))
            {
                // First endpoint on border with either edge moving inside
                return true;
            }
            if (Inwards(x2Tag, x1Tag, ctrlxtag) &&
                    Inwards(y2Tag, y1Tag, ctrlytag))
            {
                // Second endpoint on border with either edge moving inside
                return true;
            }

            // Trivially accept if endpoints span directly across the rectangle
            bool xoverlap = (x1Tag * x2Tag <= 0);
            bool yoverlap = (y1Tag * y2Tag <= 0);
            if (x1Tag == INSIDE && x2Tag == INSIDE && yoverlap)
            {
                return true;
            }
            if (y1Tag == INSIDE && y2Tag == INSIDE && xoverlap)
            {
                return true;
            }

            // We now know that both endpoints are outside the rectangle
            // but the 3 points are not all on one side of the rectangle.
            // Therefore the curve cannot be contained inside the rectangle,
            // but the rectangle might be contained inside the curve, or
            // the curve might intersect the boundary of the rectangle.

            double[] eqn = new double[3];
            double[] res = new double[3];
            if (!yoverlap)
            {
                // Both Y coordinates for the closing segment are above or
                // below the rectangle which means that we can only intersect
                // if the curve crosses the top (or bottom) of the rectangle
                // in more than one place and if those crossing locations
                // span the horizontal range of the rectangle.
                FillEqn(eqn, (y1Tag < INSIDE ? y : y + h), Y1, ctrlpy, yp2);
                return (SolveQuadratic(eqn, res) == 2 &&
                        EvalQuadratic(res, 2, true, true, null,
                        xp1, ctrlpx, xp2) == 2 &&
                        GetTag(res[0], x, x + w) * GetTag(res[1], x, x + w) <= 0);
            }

            // Y ranges overlap.  Now we examine the X ranges
            if (!xoverlap)
            {
                // Both X coordinates for the closing segment are left of
                // or right of the rectangle which means that we can only
                // intersect if the curve crosses the left (or right) edge
                // of the rectangle in more than one place and if those
                // crossing locations span the vertical range of the rectangle.
                FillEqn(eqn, (x1Tag < INSIDE ? x : x + w), xp1, ctrlpx, xp2);
                return (SolveQuadratic(eqn, res) == 2 &&
                        EvalQuadratic(res, 2, true, true, null,
                        Y1, ctrlpy, yp2) == 2 &&
                        GetTag(res[0], y, y + h) * GetTag(res[1], y, y + h) <= 0);
            }

            // The X and Y ranges of the endpoints overlap the X and Y
            // ranges of the rectangle, now find out how the endpoint
            // line segment intersects the Y range of the rectangle
            double dx = xp2 - xp1;
            double dy = yp2 - Y1;
            double k = yp2 * xp1 - xp2 * Y1;
            int c1Tag, c2Tag;
            if (y1Tag == INSIDE)
            {
                c1Tag = x1Tag;
            }
            else
            {
                c1Tag = GetTag((k + dx * (y1Tag < INSIDE ? y : y + h)) / dy, x, x + w);
            }
            if (y2Tag == INSIDE)
            {
                c2Tag = x2Tag;
            }
            else
            {
                c2Tag = GetTag((k + dx * (y2Tag < INSIDE ? y : y + h)) / dy, x, x + w);
            }
            // If the part of the line segment that intersects the Y range
            // of the rectangle crosses it horizontally - trivially accept
            if (c1Tag * c2Tag <= 0)
            {
                return true;
            }

            // Now we know that both the X and Y ranges intersect and that
            // the endpoint line segment does not directly cross the rectangle.
            //
            // We can almost treat this case like one of the cases above
            // where both endpoints are to one side, except that we will
            // only get one intersection of the curve with the vertical
            // side of the rectangle.  This is because the endpoint segment
            // accounts for the other intersection.
            //
            // (Remember there is overlap in both the X and Y ranges which
            //  means that the segment must cross at least one vertical edge
            //  of the rectangle - in particular, the "near vertical side" -
            //  leaving only one intersection for the curve.)
            //
            // Now we calculate the y tags of the two intersections on the
            // "near vertical side" of the rectangle.  We will have one with
            // the endpoint segment, and one with the curve.  If those two
            // vertical intersections overlap the Y range of the rectangle,
            // we have an intersection.  Otherwise, we don't.

            // c1tag = vertical intersection class of the endpoint segment
            //
            // Choose the y tag of the endpoint that was not on the same
            // side of the rectangle as the subsegment calculated above.
            // Note that we can "steal" the existing Y tag of that endpoint
            // since it will be provably the same as the vertical intersection.
            c1Tag = ((c1Tag * x1Tag <= 0) ? y1Tag : y2Tag);

            // c2tag = vertical intersection class of the curve
            //
            // We have to calculate this one the straightforward way.
            // Note that the c2tag can still tell us which vertical edge
            // to test against.
            FillEqn(eqn, (c2Tag < INSIDE ? x : x + w), xp1, ctrlpx, xp2);
            int num = SolveQuadratic(eqn, res);

            // Note: We should be able to assert(num == 2); since the
            // X range "crosses" (not touches) the vertical boundary,
            // but we pass num to evalQuadratic for completeness.
            EvalQuadratic(res, num, true, true, null, Y1, ctrlpy, yp2);

            // Note: We can assert(num evals == 1); since one of the
            // 2 crossings will be out of the [0,1] range.
            c2Tag = GetTag(res[0], y, y + h);

            // Finally, we have an intersection if the two crossings
            // overlap the Y range of the rectangle.
            return (c1Tag * c2Tag <= 0);
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
         * {@inheritDoc}
         */
        public bool Contains(int x, int y, int w, int h)
        {
            if (w <= 0 || h <= 0)
            {
                return false;
            }
            // Assertion: Quadratic curves closed by connecting their
            // endpoints are always convex.
            return (Contains(x, y) &&
                    Contains(x + w, y) &&
                    Contains(x + w, y + h) &&
                    Contains(x, y + h));
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
         * Returns an iteration object that defines the boundary of the
         * shape of this <code>QuadCurve</code>.
         * The iterator for this class is not multi-threaded safe,
         * which means that this <code>QuadCurve</code> class does not
         * guarantee that modifications to the geometry of this
         * <code>QuadCurve</code> object do not affect any iterations of
         * that geometry that are already in process.
         * @param at an optional {@link AffineTransform} to apply to the
         *		shape boundary
         * @return a {@link PathIterator} object that defines the boundary
         *		of the shape.
         */
        public PathIterator GetPathIterator(AffineTransform at)
        {
            return new QuadIterator(this, at);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of the
         * flattened shape of this <code>QuadCurve</code>.
         * The iterator for this class is not multi-threaded safe,
         * which means that this <code>QuadCurve</code> class does not
         * guarantee that modifications to the geometry of this
         * <code>QuadCurve</code> object do not affect any iterations of
         * that geometry that are already in process.
         * @param at an optional <code>AffineTransform</code> to apply
         *		to the boundary of the shape
         * @param flatness the maximum distance that the control points for a
         *		subdivided curve can be with respect to a line connecting
         * 		the end points of this curve before this curve is
         *		replaced by a straight line connecting the end points.
         * @return a <code>PathIterator</code> object that defines the
         *		flattened boundary of the shape.
         */
        public PathIterator GetPathIterator(AffineTransform at, int flatness)
        {
            return new FlatteningPathIterator(GetPathIterator(at), flatness);
        }
    }
}
