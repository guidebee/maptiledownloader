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
    abstract class Curve
    {

        public const int INCREASING = 1;
        public const int DECREASING = -1;
        protected int _direction;

        public static void InsertMove(ArrayList curves, double x, double y)
        {
            curves.Add(new Order0(x, y));
        }

        public static void InsertLine(ArrayList curves,
                double x0, double y0,
                double x1, double y1)
        {
            if (y0 < y1)
            {
                curves.Add(new Order1(x0, y0,
                        x1, y1,
                        INCREASING));
            }
            else if (y0 > y1)
            {
                curves.Add(new Order1(x1, y1,
                        x0, y0,
                        DECREASING));
            }
        }

        public static void InsertQuad(ArrayList curves,
                double x0, double y0,
                double[] coords)
        {
            double y1 = coords[3];
            if (y0 > y1)
            {
                Order2.Insert(curves, coords,
                        coords[2], y1,
                        coords[0], coords[1],
                        x0, y0,
                        DECREASING);
            }
            else if (y0 == y1 && y0 == coords[1])
            {
                // Do not add horizontal lines
                return;
            }
            else
            {
                Order2.Insert(curves, coords,
                        x0, y0,
                        coords[0], coords[1],
                        coords[2], y1,
                        INCREASING);
            }
        }

        public static void InsertCubic(ArrayList curves,
                double x0, double y0,
                double[] coords)
        {
            double y1 = coords[5];
            if (y0 > y1)
            {
                Order3.Insert(curves, coords,
                        coords[4], y1,
                        coords[2], coords[3],
                        coords[0], coords[1],
                        x0, y0,
                        DECREASING);
            }
            else if (y0 == y1 && y0 == coords[1] && y0 == coords[3])
            {
                // Do not add horizontal lines
                return;
            }
            else
            {
                Order3.Insert(curves, coords,
                        x0, y0,
                        coords[0], coords[1],
                        coords[2], coords[3],
                        coords[4], y1,
                        INCREASING);
            }
        }

        /**
         * Calculates the number of times the given path
         * crosses the ray extending to the right from (px,py).
         * If the point lies on a part of the path,
         * then no crossings are counted for that intersection.
         * +1 is added for each crossing where the Y coordinate is increasing
         * -1 is added for each crossing where the Y coordinate is decreasing
         * The return Value is the sum of all crossings for every segment in
         * the path.
         * The path must start with a SEG_MOVETO, otherwise an exception is
         * thrown.
         * The caller must check p[xy] for NaN values.
         * The caller may also reject infinite p[xy] values as well.
         */
        public static int PointCrossingsForPath(PathIterator pi,
                double px, double py)
        {
            if (pi.IsDone())
            {
                return 0;
            }
            int[] coords = new int[6];
            if (pi.CurrentSegment(coords) != PathIterator.SEG_MOVETO)
            {
                throw new IllegalPathStateException("missing initial moveto " +
                        "in path definition");
            }
            pi.Next();
            double movx = coords[0];
            double movy = coords[1];
            double curx = movx;
            double cury = movy;
            int crossings = 0;
            while (!pi.IsDone())
            {
                double endx;
                double endy;
                switch (pi.CurrentSegment(coords))
                {
                    case PathIterator.SEG_MOVETO:
                        if (cury != movy)
                        {
                            crossings += PointCrossingsForLine(px, py,
                                    curx, cury,
                                    movx, movy);
                        }
                        movx = curx = coords[0];
                        movy = cury = coords[1];
                        break;
                    case PathIterator.SEG_LINETO:
                        endx = coords[0];
                        endy = coords[1];
                        crossings += PointCrossingsForLine(px, py,
                                curx, cury,
                                endx, endy);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        endx = coords[2];
                        endy = coords[3];
                        crossings += PointCrossingsForQuad(px, py,
                                curx, cury,
                                coords[0], coords[1],
                                endx, endy, 0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CUBICTO:
                        endx = coords[4];
                        endy = coords[5];
                        crossings += PointCrossingsForCubic(px, py,
                                curx, cury,
                                coords[0], coords[1],
                                coords[2], coords[3],
                                endx, endy, 0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CLOSE:
                        if (cury != movy)
                        {
                            crossings += PointCrossingsForLine(px, py,
                                    curx, cury,
                                    movx, movy);
                        }
                        curx = movx;
                        cury = movy;
                        break;
                }
                pi.Next();
            }
            if (cury != movy)
            {
                crossings += PointCrossingsForLine(px, py,
                        curx, cury,
                        movx, movy);
            }
            return crossings;
        }

        /**
         * Calculates the number of times the line from (x0,y0) to (x1,y1)
         * crosses the ray extending to the right from (px,py).
         * If the point lies on the line, then no crossings are recorded.
         * +1 is returned for a crossing where the Y coordinate is increasing
         * -1 is returned for a crossing where the Y coordinate is decreasing
         */
        public static int PointCrossingsForLine(double px, double py,
                double x0, double y0,
                double x1, double y1)
        {
            if (py < y0 && py < y1)
            {
                return 0;
            }
            if (py >= y0 && py >= y1)
            {
                return 0;
            }
            // assert(y0 != y1);
            if (px >= x0 && px >= x1)
            {
                return 0;
            }
            if (px < x0 && px < x1)
            {
                return (y0 < y1) ? 1 : -1;
            }
            double xintercept = x0 + (py - y0) * (x1 - x0) / (y1 - y0);
            if (px >= xintercept)
            {
                return 0;
            }
            return (y0 < y1) ? 1 : -1;
        }

        /**
         * Calculates the number of times the quad from (x0,y0) to (x1,y1)
         * crosses the ray extending to the right from (px,py).
         * If the point lies on a part of the curve,
         * then no crossings are counted for that intersection.
         * the level parameter should be 0 at the top-level call and will count
         * up for each recursion level to prevent infinite recursion
         * +1 is added for each crossing where the Y coordinate is increasing
         * -1 is added for each crossing where the Y coordinate is decreasing
         */
        public static int PointCrossingsForQuad(double px, double py,
                double x0, double y0,
                double xc, double yc,
                double x1, double y1, int level)
        {
            if (py < y0 && py < yc && py < y1)
            {
                return 0;
            }
            if (py >= y0 && py >= yc && py >= y1)
            {
                return 0;
            }
            // Note y0 could equal y1...
            if (px >= x0 && px >= xc && px >= x1)
            {
                return 0;
            }
            if (px < x0 && px < xc && px < x1)
            {
                if (py >= y0)
                {
                    if (py < y1)
                    {
                        return 1;
                    }
                }
                else
                {
                    // py < y0
                    if (py >= y1)
                    {
                        return -1;
                    }
                }
                // py outside of y01 range, and/or y0==y1
                return 0;
            }
            // double precision only has 52 bits of mantissa
            if (level > 52)
            {
                return PointCrossingsForLine(px, py, x0, y0, x1, y1);
            }
            double x0C = (x0 + xc) / 2;
            double y0C = (y0 + yc) / 2;
            double xc1 = (xc + x1) / 2;
            double yc1 = (yc + y1) / 2;
            xc = (x0C + xc1) / 2;
            yc = (y0C + yc1) / 2;
            if (double.IsNaN(xc) || double.IsNaN(yc))
            {
                // [xy]c are NaN if any of [xy]0c or [xy]c1 are NaN
                // [xy]0c or [xy]c1 are NaN if any of [xy][0c1] are NaN
                // These values are also NaN if opposing infinities are added
                return 0;
            }
            return (PointCrossingsForQuad(px, py,
                    x0, y0, x0C, y0C, xc, yc,
                    level + 1) +
                    PointCrossingsForQuad(px, py,
                    xc, yc, xc1, yc1, x1, y1,
                    level + 1));
        }

        /**
         * Calculates the number of times the cubic from (x0,y0) to (x1,y1)
         * crosses the ray extending to the right from (px,py).
         * If the point lies on a part of the curve,
         * then no crossings are counted for that intersection.
         * the level parameter should be 0 at the top-level call and will count
         * up for each recursion level to prevent infinite recursion
         * +1 is added for each crossing where the Y coordinate is increasing
         * -1 is added for each crossing where the Y coordinate is decreasing
         */
        public static int PointCrossingsForCubic(double px, double py,
                double x0, double y0,
                double xc0, double yc0,
                double xc1, double yc1,
                double x1, double y1, int level)
        {
            if (py < y0 && py < yc0 && py < yc1 && py < y1)
            {
                return 0;
            }
            if (py >= y0 && py >= yc0 && py >= yc1 && py >= y1)
            {
                return 0;
            }
            // Note y0 could equal yc0...
            if (px >= x0 && px >= xc0 && px >= xc1 && px >= x1)
            {
                return 0;
            }
            if (px < x0 && px < xc0 && px < xc1 && px < x1)
            {
                if (py >= y0)
                {
                    if (py < y1)
                    {
                        return 1;
                    }
                }
                else
                {
                    // py < y0
                    if (py >= y1)
                    {
                        return -1;
                    }
                }
                // py outside of y01 range, and/or y0==yc0
                return 0;
            }
            // double precision only has 52 bits of mantissa
            if (level > 52)
            {
                return PointCrossingsForLine(px, py, x0, y0, x1, y1);
            }
            double xmid = (xc0 + xc1) / 2;
            double ymid = (yc0 + yc1) / 2;
            xc0 = (x0 + xc0) / 2;
            yc0 = (y0 + yc0) / 2;
            xc1 = (xc1 + x1) / 2;
            yc1 = (yc1 + y1) / 2;
            double xc0M = (xc0 + xmid) / 2;
            double yc0M = (yc0 + ymid) / 2;
            double xmc1 = (xmid + xc1) / 2;
            double ymc1 = (ymid + yc1) / 2;
            xmid = (xc0M + xmc1) / 2;
            ymid = (yc0M + ymc1) / 2;
            if (double.IsNaN(xmid) || double.IsNaN(ymid))
            {
                // [xy]mid are NaN if any of [xy]c0m or [xy]mc1 are NaN
                // [xy]c0m or [xy]mc1 are NaN if any of [xy][c][01] are NaN
                // These values are also NaN if opposing infinities are added
                return 0;
            }
            return (PointCrossingsForCubic(px, py,
                    x0, y0, xc0, yc0,
                    xc0M, yc0M, xmid, ymid, level + 1) +
                    PointCrossingsForCubic(px, py,
                    xmid, ymid, xmc1, ymc1,
                    xc1, yc1, x1, y1, level + 1));
        }
        /**
         * The rectangle intersection test counts the number of times
         * that the path crosses through the shadow that the rectangle
         * projects to the right towards (x => +INFINITY).
         *
         * During processing of the path it actually counts every time
         * the path crosses either or both of the top and bottom edges
         * of that shadow.  If the path enters from the top, the count
         * is incremented.  If it then exits back through the top, the
         * same way it came in, the count is decremented and there is
         * no impact on the winding count.  If, instead, the path exits
         * out the bottom, then the count is incremented again and a
         * full pass through the shadow is indicated by the winding count
         * having been incremented by 2.
         *
         * Thus, the winding count that it accumulates is actually double
         * the real winding count.  Since the path is continuous, the
         * final answer should be a multiple of 2, otherwise there is a
         * logic error somewhere.
         *
         * If the path ever has a direct hit on the rectangle, then a
         * special Value is returned.  This special Value terminates
         * all ongoing accumulation on up through the call chain and
         * ends up getting returned to the calling function which can
         * then produce an answer directly.  For intersection tests,
         * the answer is always "true" if the path intersects the
         * rectangle.  For containment tests, the answer is always
         * "false" if the path intersects the rectangle.  Thus, no
         * further processing is ever needed if an intersection occurs.
         */
        public const int RECT_INTERSECTS = 0x8000000;

        /**
         * Accumulate the number of times the path crosses the shadow
         * extending to the right of the rectangle.  See the comment
         * for the RECT_INTERSECTS constant for more complete details.
         * The return Value is the sum of all crossings for both the
         * top and bottom of the shadow for every segment in the path,
         * or the special Value RECT_INTERSECTS if the path ever enters
         * the interior of the rectangle.
         * The path must start with a SEG_MOVETO, otherwise an exception is
         * thrown.
         * The caller must check r[xy]{Min,Max} for NaN values.
         */
        public static int RectCrossingsForPath(PathIterator pi,
                double rxmin, double rymin,
                double rxmax, double rymax)
        {
            if (rxmax <= rxmin || rymax <= rymin)
            {
                return 0;
            }
            if (pi.IsDone())
            {
                return 0;
            }
            int[] coords = new int[6];
            if (pi.CurrentSegment(coords) != PathIterator.SEG_MOVETO)
            {
                throw new IllegalPathStateException("missing initial moveto " +
                        "in path definition");
            }
            pi.Next();
            double movx, movy;
            double curx = movx = coords[0];
            double cury = movy = coords[1];
            int crossings = 0;
            while (crossings != RECT_INTERSECTS && !pi.IsDone())
            {
                double endx;
                double endy;
                switch (pi.CurrentSegment(coords))
                {
                    case PathIterator.SEG_MOVETO:
                        if (curx != movx || cury != movy)
                        {
                            crossings = RectCrossingsForLine(crossings,
                                    rxmin, rymin,
                                    rxmax, rymax,
                                    curx, cury,
                                    movx, movy);
                        }
                        // Count should always be a multiple of 2 here.
                        // assert((crossings & 1) != 0);
                        movx = curx = coords[0];
                        movy = cury = coords[1];
                        break;
                    case PathIterator.SEG_LINETO:
                        endx = coords[0];
                        endy = coords[1];
                        crossings = RectCrossingsForLine(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                endx, endy);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        endx = coords[2];
                        endy = coords[3];
                        crossings = RectCrossingsForQuad(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                coords[0], coords[1],
                                endx, endy, 0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CUBICTO:
                        endx = coords[4];
                        endy = coords[5];
                        crossings = RectCrossingsForCubic(crossings,
                                rxmin, rymin,
                                rxmax, rymax,
                                curx, cury,
                                coords[0], coords[1],
                                coords[2], coords[3],
                                endx, endy, 0);
                        curx = endx;
                        cury = endy;
                        break;
                    case PathIterator.SEG_CLOSE:
                        if (curx != movx || cury != movy)
                        {
                            crossings = RectCrossingsForLine(crossings,
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
                pi.Next();
            }
            if (crossings != RECT_INTERSECTS && (curx != movx || cury != movy))
            {
                crossings = RectCrossingsForLine(crossings,
                        rxmin, rymin,
                        rxmax, rymax,
                        curx, cury,
                        movx, movy);
            }
            // Count should always be a multiple of 2 here.
            // assert((crossings & 1) != 0);
            return crossings;
        }

        /**
         * Accumulate the number of times the line crosses the shadow
         * extending to the right of the rectangle.  See the comment
         * for the RECT_INTERSECTS constant for more complete details.
         */
        public static int RectCrossingsForLine(int crossings,
                double rxmin, double rymin,
                double rxmax, double rymax,
                double x0, double y0,
                double x1, double y1)
        {
            if (y0 >= rymax && y1 >= rymax)
            {
                return crossings;
            }
            if (y0 <= rymin && y1 <= rymin)
            {
                return crossings;
            }
            if (x0 <= rxmin && x1 <= rxmin)
            {
                return crossings;
            }
            if (x0 >= rxmax && x1 >= rxmax)
            {
                // Line is entirely to the right of the rect
                // and the vertical ranges of the two overlap by a non-empty amount
                // Thus, this line segment is partially in the "right-shadow"
                // Path may have done a complete crossing
                // Or path may have entered or exited the right-shadow
                if (y0 < y1)
                {
                    // y-increasing line segment...
                    // We know that y0 < rymax and y1 > rymin
                    if (y0 <= rymin)
                    {
                        crossings++;
                    }
                    if (y1 >= rymax)
                    {
                        crossings++;
                    }
                }
                else if (y1 < y0)
                {
                    // y-decreasing line segment...
                    // We know that y1 < rymax and y0 > rymin
                    if (y1 <= rymin)
                    {
                        crossings--;
                    }
                    if (y0 >= rymax)
                    {
                        crossings--;
                    }
                }
                return crossings;
            }
            // Remaining case:
            // Both x and y ranges overlap by a non-empty amount
            // First do trivial INTERSECTS rejection of the cases
            // where one of the endpoints is inside the rectangle.
            if ((x0 > rxmin && x0 < rxmax && y0 > rymin && y0 < rymax) ||
                    (x1 > rxmin && x1 < rxmax && y1 > rymin && y1 < rymax))
            {
                return RECT_INTERSECTS;
            }
            // Otherwise calculate the y intercepts and see where
            // they fall with respect to the rectangle
            double xi0 = x0;
            if (y0 < rymin)
            {
                xi0 += ((rymin - y0) * (x1 - x0) / (y1 - y0));
            }
            else if (y0 > rymax)
            {
                xi0 += ((rymax - y0) * (x1 - x0) / (y1 - y0));
            }
            double xi1 = x1;
            if (y1 < rymin)
            {
                xi1 += ((rymin - y1) * (x0 - x1) / (y0 - y1));
            }
            else if (y1 > rymax)
            {
                xi1 += ((rymax - y1) * (x0 - x1) / (y0 - y1));
            }
            if (xi0 <= rxmin && xi1 <= rxmin)
            {
                return crossings;
            }
            if (xi0 >= rxmax && xi1 >= rxmax)
            {
                if (y0 < y1)
                {
                    // y-increasing line segment...
                    // We know that y0 < rymax and y1 > rymin
                    if (y0 <= rymin)
                    {
                        crossings++;
                    }
                    if (y1 >= rymax)
                    {
                        crossings++;
                    }
                }
                else if (y1 < y0)
                {
                    // y-decreasing line segment...
                    // We know that y1 < rymax and y0 > rymin
                    if (y1 <= rymin)
                    {
                        crossings--;
                    }
                    if (y0 >= rymax)
                    {
                        crossings--;
                    }
                }
                return crossings;
            }
            return RECT_INTERSECTS;
        }

        /**
         * Accumulate the number of times the quad crosses the shadow
         * extending to the right of the rectangle.  See the comment
         * for the RECT_INTERSECTS constant for more complete details.
         */
        public static int RectCrossingsForQuad(int crossings,
                double rxmin, double rymin,
                double rxmax, double rymax,
                double x0, double y0,
                double xc, double yc,
                double x1, double y1,
                int level)
        {
            if (y0 >= rymax && yc >= rymax && y1 >= rymax)
            {
                return crossings;
            }
            if (y0 <= rymin && yc <= rymin && y1 <= rymin)
            {
                return crossings;
            }
            if (x0 <= rxmin && xc <= rxmin && x1 <= rxmin)
            {
                return crossings;
            }
            if (x0 >= rxmax && xc >= rxmax && x1 >= rxmax)
            {
                // Quad is entirely to the right of the rect
                // and the vertical range of the 3 Y coordinates of the quad
                // overlaps the vertical range of the rect by a non-empty amount
                // We now judge the crossings solely based on the line segment
                // connecting the endpoints of the quad.
                // Note that we may have 0, 1, or 2 crossings as the control
                // point may be causing the Y range intersection while the
                // two endpoints are entirely above or below.
                if (y0 < y1)
                {
                    // y-increasing line segment...
                    if (y0 <= rymin && y1 > rymin)
                    {
                        crossings++;
                    }
                    if (y0 < rymax && y1 >= rymax)
                    {
                        crossings++;
                    }
                }
                else if (y1 < y0)
                {
                    // y-decreasing line segment...
                    if (y1 <= rymin && y0 > rymin)
                    {
                        crossings--;
                    }
                    if (y1 < rymax && y0 >= rymax)
                    {
                        crossings--;
                    }
                }
                return crossings;
            }
            // The intersection of ranges is more complicated
            // First do trivial INTERSECTS rejection of the cases
            // where one of the endpoints is inside the rectangle.
            if ((x0 < rxmax && x0 > rxmin && y0 < rymax && y0 > rymin) ||
                    (x1 < rxmax && x1 > rxmin && y1 < rymax && y1 > rymin))
            {
                return RECT_INTERSECTS;
            }
            // Otherwise, subdivide and look for one of the cases above.
            // double precision only has 52 bits of mantissa
            if (level > 52)
            {
                return RectCrossingsForLine(crossings,
                        rxmin, rymin, rxmax, rymax,
                        x0, y0, x1, y1);
            }
            double x0C = (x0 + xc) / 2;
            double y0C = (y0 + yc) / 2;
            double xc1 = (xc + x1) / 2;
            double yc1 = (yc + y1) / 2;
            xc = (x0C + xc1) / 2;
            yc = (y0C + yc1) / 2;
            if (double.IsNaN(xc) || double.IsNaN(yc))
            {
                // [xy]c are NaN if any of [xy]0c or [xy]c1 are NaN
                // [xy]0c or [xy]c1 are NaN if any of [xy][0c1] are NaN
                // These values are also NaN if opposing infinities are added
                return 0;
            }
            crossings = RectCrossingsForQuad(crossings,
                    rxmin, rymin, rxmax, rymax,
                    x0, y0, x0C, y0C, xc, yc,
                    level + 1);
            if (crossings != RECT_INTERSECTS)
            {
                crossings = RectCrossingsForQuad(crossings,
                        rxmin, rymin, rxmax, rymax,
                        xc, yc, xc1, yc1, x1, y1,
                        level + 1);
            }
            return crossings;
        }

        /**
         * Accumulate the number of times the cubic crosses the shadow
         * extending to the right of the rectangle.  See the comment
         * for the RECT_INTERSECTS constant for more complete details.
         */
        public static int RectCrossingsForCubic(int crossings,
                double rxmin, double rymin,
                double rxmax, double rymax,
                double x0, double y0,
                double xc0, double yc0,
                double xc1, double yc1,
                double x1, double y1,
                int level)
        {
            if (y0 >= rymax && yc0 >= rymax && yc1 >= rymax && y1 >= rymax)
            {
                return crossings;
            }
            if (y0 <= rymin && yc0 <= rymin && yc1 <= rymin && y1 <= rymin)
            {
                return crossings;
            }
            if (x0 <= rxmin && xc0 <= rxmin && xc1 <= rxmin && x1 <= rxmin)
            {
                return crossings;
            }
            if (x0 >= rxmax && xc0 >= rxmax && xc1 >= rxmax && x1 >= rxmax)
            {
                // Cubic is entirely to the right of the rect
                // and the vertical range of the 4 Y coordinates of the cubic
                // overlaps the vertical range of the rect by a non-empty amount
                // We now judge the crossings solely based on the line segment
                // connecting the endpoints of the cubic.
                // that we may have 0, 1, or 2 crossings as the control
                // points may be causing the Y range intersection while the
                // two endpoints are entirely above or below.
                if (y0 < y1)
                {
                    // y-increasing line segment...
                    if (y0 <= rymin && y1 > rymin)
                    {
                        crossings++;
                    }
                    if (y0 < rymax && y1 >= rymax)
                    {
                        crossings++;
                    }
                }
                else if (y1 < y0)
                {
                    // y-decreasing line segment...
                    if (y1 <= rymin && y0 > rymin)
                    {
                        crossings--;
                    }
                    if (y1 < rymax && y0 >= rymax)
                    {
                        crossings--;
                    }
                }
                return crossings;
            }
            // The intersection of ranges is more complicated
            // First do trivial INTERSECTS rejection of the cases
            // where one of the endpoints is inside the rectangle.
            if ((x0 > rxmin && x0 < rxmax && y0 > rymin && y0 < rymax) ||
                    (x1 > rxmin && x1 < rxmax && y1 > rymin && y1 < rymax))
            {
                return RECT_INTERSECTS;
            }
            // Otherwise, subdivide and look for one of the cases above.
            // double precision only has 52 bits of mantissa
            if (level > 52)
            {
                return RectCrossingsForLine(crossings,
                        rxmin, rymin, rxmax, rymax,
                        x0, y0, x1, y1);
            }
            double xmid = (xc0 + xc1) / 2;
            double ymid = (yc0 + yc1) / 2;
            xc0 = (x0 + xc0) / 2;
            yc0 = (y0 + yc0) / 2;
            xc1 = (xc1 + x1) / 2;
            yc1 = (yc1 + y1) / 2;
            double xc0M = (xc0 + xmid) / 2;
            double yc0M = (yc0 + ymid) / 2;
            double xmc1 = (xmid + xc1) / 2;
            double ymc1 = (ymid + yc1) / 2;
            xmid = (xc0M + xmc1) / 2;
            ymid = (yc0M + ymc1) / 2;
            if (double.IsNaN(xmid) || double.IsNaN(ymid))
            {
                // [xy]mid are NaN if any of [xy]c0m or [xy]mc1 are NaN
                // [xy]c0m or [xy]mc1 are NaN if any of [xy][c][01] are NaN
                // These values are also NaN if opposing infinities are added
                return 0;
            }
            crossings = RectCrossingsForCubic(crossings,
                    rxmin, rymin, rxmax, rymax,
                    x0, y0, xc0, yc0,
                    xc0M, yc0M, xmid, ymid, level + 1);
            if (crossings != RECT_INTERSECTS)
            {
                crossings = RectCrossingsForCubic(crossings,
                        rxmin, rymin, rxmax, rymax,
                        xmid, ymid, xmc1, ymc1,
                        xc1, yc1, x1, y1, level + 1);
            }
            return crossings;
        }

        internal Curve(int direction)
        {
            _direction = direction;
        }

        public int GetDirection()
        {
            return _direction;
        }

        public Curve GetWithDirection(int direction)
        {
            return (_direction == direction ? this : GetReversedCurve());
        }

        public static double Round(double v)
        {
            //return Math.Rint(v*10)/10;
            return v;
        }

        public static int Orderof(double x1, double x2)
        {
            if (x1 < x2)
            {
                return -1;
            }
            if (x1 > x2)
            {
                return 1;
            }
            return 0;
        }


        public override string ToString()
        {
            return ("Curve[" +
                    GetOrder() + ", " +
                    ("(" + Round(GetX0()) + ", " + Round(GetY0()) + "), ") +
                    ControlPointString() +
                    ("(" + Round(GetX1()) + ", " + Round(GetY1()) + "), ") +
                    (_direction == INCREASING ? "D" : "U") +
                    "]");
        }

        public virtual string ControlPointString()
        {
            return "";
        }

        public abstract int GetOrder();

        public abstract double GetXTop();

        public abstract double GetYTop();

        public abstract double GetXBot();

        public abstract double GetYBot();

        public abstract double GetXMin();

        public abstract double GetXMax();

        public abstract double GetX0();

        public abstract double GetY0();

        public abstract double GetX1();

        public abstract double GetY1();

        public abstract double XforY(double y);

        public abstract double TforY(double y);

        public abstract double XforT(double t);

        public abstract double YforT(double t);

        public abstract double DXforT(double t, int deriv);

        public abstract double DYforT(double t, int deriv);

        public abstract double NextVertical(double t0, double t1);

        public virtual int CrossingsFor(double x, double y)
        {
            if (y >= GetYTop() && y < GetYBot())
            {
                if (x < GetXMax() && (x < GetXMin() || x < XforY(y)))
                {
                    return 1;
                }
            }
            return 0;
        }

        public virtual bool AccumulateCrossings(Crossings c)
        {
            double xhi = c.GetXHi();
            if (GetXMin() >= xhi)
            {
                return false;
            }
            double xlo = c.GetXLo();
            double ylo = c.GetYLo();
            double yhi = c.GetYHi();
            double y0 = GetYTop();
            double y1 = GetYBot();
            double tstart, ystart, tend, yend;
            if (y0 < ylo)
            {
                if (y1 <= ylo)
                {
                    return false;
                }
                ystart = ylo;
                tstart = TforY(ylo);
            }
            else
            {
                if (y0 >= yhi)
                {
                    return false;
                }
                ystart = y0;
                tstart = 0;
            }
            if (y1 > yhi)
            {
                yend = yhi;
                tend = TforY(yhi);
            }
            else
            {
                yend = y1;
                tend = 1;
            }
            bool hitLo = false;
            bool hitHi = false;
            while (true)
            {
                double x = XforT(tstart);
                if (x < xhi)
                {
                    if (hitHi || x > xlo)
                    {
                        return true;
                    }
                    hitLo = true;
                }
                else
                {
                    if (hitLo)
                    {
                        return true;
                    }
                    hitHi = true;
                }
                if (tstart >= tend)
                {
                    break;
                }
                tstart = NextVertical(tstart, tend);
            }
            if (hitLo)
            {
                c.Record((int)(ystart + .5), (int)(yend + .5), _direction);
            }
            return false;
        }

        public abstract void Enlarge(Rectangle r);

        public Curve GetSubCurve(double ystart, double yend)
        {
            return GetSubCurve(ystart, yend, _direction);
        }

        public abstract Curve GetReversedCurve();

        public abstract Curve GetSubCurve(double ystart, double yend, int dir);

        public int CompareTo(Curve that, double[] yrange)
        {
            /*
            System.out.println(this+".compareTo("+that+")");
            System.out.println("target range = "+yrange[0]+"=>"+yrange[1]);
             */
            double y0 = yrange[0];
            double y1 = yrange[1];
            y1 = Math.Min(Math.Min(y1, GetYBot()), that.GetYBot());
            if (y1 <= yrange[0])
            {
                throw new SystemException("backstepping from " + yrange[0] + " to " + y1);
            }
            yrange[1] = y1;
            if (GetXMax() <= that.GetXMin())
            {
                if (GetXMin() == that.GetXMax())
                {
                    return 0;
                }
                return -1;
            }
            if (GetXMin() >= that.GetXMax())
            {
                return 1;
            }
            // Parameter s for thi(s) curve and t for tha(t) curve
            // [st]0 = parameters for top of current section of interest
            // [st]1 = parameters for bottom of valid range
            // [st]h = parameters for hypothesis point
            // [d][xy]s = valuations of thi(s) curve at sh
            // [d][xy]t = valuations of tha(t) curve at th
            double s0 = TforY(y0);
            double ys0 = YforT(s0);
            if (ys0 < y0)
            {
                s0 = RefineTforY(s0, ys0, y0);
                ys0 = YforT(s0);
            }
            double s1 = TforY(y1);
            if (YforT(s1) < y0)
            {
                s1 = RefineTforY(s1, YforT(s1), y0);
                //System.out.println("s1 problem!");
            }
            double t0 = that.TforY(y0);
            double yt0 = that.YforT(t0);
            if (yt0 < y0)
            {
                t0 = that.RefineTforY(t0, yt0, y0);
                yt0 = that.YforT(t0);
            }
            double t1 = that.TforY(y1);
            if (that.YforT(t1) < y0)
            {
                t1 = that.RefineTforY(t1, that.YforT(t1), y0);
                //System.out.println("t1 problem!");
            }
            double xs0 = XforT(s0);
            double xt0 = that.XforT(t0);
            double scale = Math.Max(Math.Abs(y0), Math.Abs(y1));
            double ymin = Math.Max(scale * 1E-14, 1E-300);
            if (FairlyClose(xs0, xt0))
            {
                double bump = ymin;
                double maxbump = Math.Min(ymin * 1E13, (y1 - y0) * .1);
                double y = y0 + bump;
                while (y <= y1)
                {
                    if (FairlyClose(XforY(y), that.XforY(y)))
                    {
                        if ((bump *= 2) > maxbump)
                        {
                            bump = maxbump;
                        }
                    }
                    else
                    {
                        y -= bump;
                        while (true)
                        {
                            bump /= 2;
                            double newy = y + bump;
                            if (newy <= y)
                            {
                                break;
                            }
                            if (FairlyClose(XforY(newy), that.XforY(newy)))
                            {
                                y = newy;
                            }
                        }
                        break;
                    }
                    y += bump;
                }
                if (y > y0)
                {
                    if (y < y1)
                    {
                        yrange[1] = y;
                    }
                    return 0;
                }
            }

            while (s0 < s1 && t0 < t1)
            {
                double sh = NextVertical(s0, s1);
                double xsh = XforT(sh);
                double ysh = YforT(sh);
                double th = that.NextVertical(t0, t1);
                double xth = that.XforT(th);
                double yth = that.YforT(th);
                /*
                System.out.println("sh = "+sh);
                System.out.println("th = "+th);
                 */
                try
                {
                    if (FindIntersect(that, yrange, ymin, 0, 0,
                            s0, xs0, ys0, sh, xsh, ysh,
                            t0, xt0, yt0, th, xth, yth))
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
                if (ysh < yth)
                {
                    if (ysh > yrange[0])
                    {
                        if (ysh < yrange[1])
                        {
                            yrange[1] = ysh;
                        }
                        break;
                    }
                    s0 = sh;
                    xs0 = xsh;
                    ys0 = ysh;
                }
                else
                {
                    if (yth > yrange[0])
                    {
                        if (yth < yrange[1])
                        {
                            yrange[1] = yth;
                        }
                        break;
                    }
                    t0 = th;
                    xt0 = xth;
                    yt0 = yth;
                }
            }
            double ymid = (yrange[0] + yrange[1]) / 2;


            return Orderof(XforY(ymid), that.XforY(ymid));
        }
        public const double TMIN = 1E-3;

        public bool FindIntersect(Curve that, double[] yrange, double ymin,
                int slevel, int tlevel,
                double s0, double xs0, double ys0,
                double s1, double xs1, double ys1,
                double t0, double xt0, double yt0,
                double t1, double xt1, double yt1)
        {
            /*
            string pad = "        ";
            pad = pad+pad+pad+pad+pad;
            pad = pad+pad;
            System.out.println("----------------------------------------------");
            System.out.println(pad.substring(0, slevel)+ys0);
            System.out.println(pad.substring(0, slevel)+ys1);
            System.out.println(pad.substring(0, slevel)+(s1-s0));
            System.out.println("-------");
            System.out.println(pad.substring(0, tlevel)+yt0);
            System.out.println(pad.substring(0, tlevel)+yt1);
            System.out.println(pad.substring(0, tlevel)+(t1-t0));
             */
            if (ys0 > yt1 || yt0 > ys1)
            {
                return false;
            }
            if (Math.Min(xs0, xs1) > Math.Max(xt0, xt1) ||
                    Math.Max(xs0, xs1) < Math.Min(xt0, xt1))
            {
                return false;
            }
            // Bounding boxes intersect - back off the larger of
            // the two subcurves by half until they stop intersecting
            // (or until they get small enough to switch to a more
            //  intensive algorithm).
            if (s1 - s0 > TMIN)
            {
                double s = (s0 + s1) / 2;
                double xs = XforT(s);
                double ys = YforT(s);
                if (s == s0 || s == s1)
                {

                    throw new SystemException("no s progress!");
                }
                if (t1 - t0 > TMIN)
                {
                    double t = (t0 + t1) / 2;
                    double xt = that.XforT(t);
                    double yt = that.YforT(t);
                    if (t == t0 || t == t1)
                    {

                        throw new SystemException("no t progress!");
                    }
                    if (ys >= yt0 && yt >= ys0)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel + 1,
                                s0, xs0, ys0, s, xs, ys,
                                t0, xt0, yt0, t, xt, yt))
                        {
                            return true;
                        }
                    }
                    if (ys >= yt)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel + 1,
                                s0, xs0, ys0, s, xs, ys,
                                t, xt, yt, t1, xt1, yt1))
                        {
                            return true;
                        }
                    }
                    if (yt >= ys)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel + 1,
                                s, xs, ys, s1, xs1, ys1,
                                t0, xt0, yt0, t, xt, yt))
                        {
                            return true;
                        }
                    }
                    if (ys1 >= yt && yt1 >= ys)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel + 1,
                                s, xs, ys, s1, xs1, ys1,
                                t, xt, yt, t1, xt1, yt1))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (ys >= yt0)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel,
                                s0, xs0, ys0, s, xs, ys,
                                t0, xt0, yt0, t1, xt1, yt1))
                        {
                            return true;
                        }
                    }
                    if (yt1 >= ys)
                    {
                        if (FindIntersect(that, yrange, ymin, slevel + 1, tlevel,
                                s, xs, ys, s1, xs1, ys1,
                                t0, xt0, yt0, t1, xt1, yt1))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (t1 - t0 > TMIN)
            {
                double t = (t0 + t1) / 2;
                double xt = that.XforT(t);
                double yt = that.YforT(t);
                if (t == t0 || t == t1)
                {

                    throw new SystemException("no t progress!");
                }
                if (yt >= ys0)
                {
                    if (FindIntersect(that, yrange, ymin, slevel, tlevel + 1,
                            s0, xs0, ys0, s1, xs1, ys1,
                            t0, xt0, yt0, t, xt, yt))
                    {
                        return true;
                    }
                }
                if (ys1 >= yt)
                {
                    if (FindIntersect(that, yrange, ymin, slevel, tlevel + 1,
                            s0, xs0, ys0, s1, xs1, ys1,
                            t, xt, yt, t1, xt1, yt1))
                    {
                        return true;
                    }
                }
            }
            else
            {
                // No more subdivisions
                double xlk = xs1 - xs0;
                double ylk = ys1 - ys0;
                double xnm = xt1 - xt0;
                double ynm = yt1 - yt0;
                double xmk = xt0 - xs0;
                double ymk = yt0 - ys0;
                double det = xnm * ylk - ynm * xlk;
                if (det != 0)
                {
                    double detinv = 1 / det;
                    double s = (xnm * ymk - ynm * xmk) * detinv;
                    double t = (xlk * ymk - ylk * xmk) * detinv;
                    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
                    {
                        s = s0 + s * (s1 - s0);
                        t = t0 + t * (t1 - t0);
                        if (s < 0 || s > 1 || t < 0 || t > 1)
                        {

                        }
                        double y = (YforT(s) + that.YforT(t)) / 2;
                        if (y <= yrange[1] && y > yrange[0])
                        {
                            yrange[1] = y;
                            return true;
                        }
                    }
                }
                //System.out.println("Testing lines!");
            }
            return false;
        }

        public double RefineTforY(double t0, double yt0, double y0)
        {
            double t1 = 1;
            while (true)
            {
                double th = (t0 + t1) / 2;
                if (th == t0 || th == t1)
                {
                    return t1;
                }
                double y = YforT(th);
                if (y < y0)
                {
                    t0 = th;
                }
                else if (y > y0)
                {
                    t1 = th;
                }
                else
                {
                    return t1;
                }
            }
        }

        public bool FairlyClose(double v1, double v2)
        {
            return (Math.Abs(v1 - v2) <
                    Math.Max(Math.Abs(v1), Math.Abs(v2)) * 1E-10);
        }

        public abstract int GetSegment(double[] coords);
    }

}
