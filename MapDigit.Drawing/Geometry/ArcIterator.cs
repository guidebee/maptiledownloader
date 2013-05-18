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
using MapDigit.Util;

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
     * A utility class to iterate over the path segments of an arc
     * through the IPathIterator interface.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class ArcIterator : PathIterator
    {
        readonly double _x;
        readonly double _y;
        readonly double _w;
        readonly double _h;
        readonly double _angStRad;
        readonly double _increment;
        readonly double _cv;
        readonly AffineTransform _affine;
        int _index;
        readonly int _arcSegs;
        readonly int _lineSegs;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        internal ArcIterator(Arc a, AffineTransform at)
        {
            _w = a.GetWidth() / 2.0;
            _h = a.GetHeight() / 2.0;
            _x = a.GetX() + _w;
            _y = a.GetY() + _h;
            _angStRad = -MathEx.ToRadians(a.GetAngleStart());
            _affine = at;
            double ext = -a.GetAngleExtent();
            if (ext >= 360.0 || ext <= -360)
            {
                _arcSegs = 4;
                _increment = Math.PI / 2;
                // btan(Math.PI / 2);
                _cv = 0.5522847498307933;
                if (ext < 0)
                {
                    _increment = -_increment;
                    _cv = -_cv;
                }
            }
            else
            {
                _arcSegs = (int)MathEx.Ceil(MathEx.Abs(ext) / 90.0);
                _increment = MathEx.ToRadians(ext / _arcSegs);
                _cv = Btan(_increment);
                if (_cv == 0)
                {
                    _arcSegs = 0;
                }
            }
            switch (a.GetArcType())
            {
                case Arc.OPEN:
                    _lineSegs = 0;
                    break;
                case Arc.CHORD:
                    _lineSegs = 1;
                    break;
                case Arc.PIE:
                    _lineSegs = 2;
                    break;
            }
            if (_w < 0 || _h < 0)
            {
                _arcSegs = _lineSegs = -1;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Return the winding rule for determining the insideness of the
         * path.
         * @see #WIND_EVEN_ODD
         * @see #WIND_NON_ZERO
         */
        public override int GetWindingRule()
        {
            return WIND_NON_ZERO;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if there are more points to read.
         * @return true if there are more points to read
         */
        public override bool IsDone()
        {
            return _index > _arcSegs + _lineSegs;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Moves the iterator to the next segment of the path forwards
         * along the primary direction of traversal as long as there are
         * more points in that direction.
         */
        public override void Next()
        {
            _index++;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /*
         * btan computes the length (k) of the control segments at
         * the beginning and end of a cubic bezier that approximates
         * a segment of an arc with extent less than or equal to
         * 90 degrees.  This length (k) will be used to generate the
         * 2 bezier control points for such a segment.
         *
         *   Assumptions:
         *     a) arc is centered on 0,0 with radius of 1.0
         *     b) arc extent is less than 90 degrees
         *     c) control points should preserve tangent
         *     d) control segments should have equal length
         *
         *   Initial data:
         *     start angle: ang1
         *     end angle:   ang2 = ang1 + extent
         *     start point: P1 = (x1, y1) = (Cos(ang1), Sin(ang1))
         *     end point:   P4 = (x4, y4) = (Cos(ang2), Sin(ang2))
         *
         *   Control points:
         *     P2 = (X2, y2)
         *     | X2 = x1 - k * Sin(ang1) = Cos(ang1) - k * Sin(ang1)
         *     | y2 = y1 + k * Cos(ang1) = Sin(ang1) + k * Cos(ang1)
         *
         *     P3 = (x3, y3)
         *     | x3 = x4 + k * Sin(ang2) = Cos(ang2) + k * Sin(ang2)
         *     | y3 = y4 - k * Cos(ang2) = Sin(ang2) - k * Cos(ang2)
         *
         * The formula for this length (k) can be found using the
         * following derivations:
         *
         *   Midpoints:
         *     a) bezier (t = 1/2)
         *        bPm = P1 * (1-t)^3 +
         *              3 * P2 * t * (1-t)^2 +
         *              3 * P3 * t^2 * (1-t) +
         *              P4 * t^3 =
         *            = (P1 + 3P2 + 3P3 + P4)/8
         *
         *     b) arc
         *        aPm = (Cos((ang1 + ang2)/2), Sin((ang1 + ang2)/2))
         *
         *   Let angb = (ang2 - ang1)/2; angb is half of the angle
         *   between ang1 and ang2.
         *
         *   Solve the equation bPm == aPm
         *
         *     a) For xm coord:
         *        x1 + 3*X2 + 3*x3 + x4 = 8*Cos((ang1 + ang2)/2)
         *
         *        Cos(ang1) + 3*Cos(ang1) - 3*k*Sin(ang1) +
         *        3*Cos(ang2) + 3*k*Sin(ang2) + Cos(ang2) =
         *        = 8*Cos((ang1 + ang2)/2)
         *
         *        4*Cos(ang1) + 4*Cos(ang2) + 3*k*(Sin(ang2) - Sin(ang1)) =
         *        = 8*Cos((ang1 + ang2)/2)
         *
         *        8*Cos((ang1 + ang2)/2)*Cos((ang2 - ang1)/2) +
         *        6*k*Sin((ang2 - ang1)/2)*Cos((ang1 + ang2)/2) =
         *        = 8*Cos((ang1 + ang2)/2)
         *
         *        4*Cos(angb) + 3*k*Sin(angb) = 4
         *
         *        k = 4 / 3 * (1 - Cos(angb)) / Sin(angb)
         *
         *     b) For ym coord we derive the same formula.
         *
         * Since this formula can generate "NaN" values for small
         * angles, we will derive a safer form that does not involve
         * dividing by very small values:
         *     (1 - Cos(angb)) / Sin(angb) =
         *     = (1 - Cos(angb))*(1 + Cos(angb)) / Sin(angb)*(1 + Cos(angb)) =
         *     = (1 - Cos(angb)^2) / Sin(angb)*(1 + Cos(angb)) =
         *     = Sin(angb)^2 / Sin(angb)*(1 + Cos(angb)) =
         *     = Sin(angb) / (1 + Cos(angb))
         *
         */
        private static double Btan(double increment)
        {
            increment /= 2.0;
            return 4.0 / 3.0 * MathEx.Sin(increment) / (1.0 + MathEx.Cos(increment));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the coordinates and type of the current path segment in
         * the iteration.
         * The return Value is the path segment type:
         * SEG_MOVETO, SEG_LINETO, SEG_QUADTO, SEG_CUBICTO, or SEG_CLOSE.
         * A float array of length 6 must be passed in and may be used to
         * store the coordinates of the point(s).
         * Each point is stored as a pair of float x,y coordinates.
         * SEG_MOVETO and SEG_LINETO types will return one point,
         * SEG_QUADTO will return two points,
         * SEG_CUBICTO will return 3 points
         * and SEG_CLOSE will not return any points.
         * @see #SEG_MOVETO
         * @see #SEG_LINETO
         * @see #SEG_QUADTO
         * @see #SEG_CUBICTO
         * @see #SEG_CLOSE
         */
        public override int CurrentSegment(int[] coords)
        {
            if (IsDone())
            {
                throw new IndexOutOfRangeException("arc iterator out of bounds");
            }
            double angle = _angStRad;
            if (_index == 0)
            {
                coords[0] = (int)(_x + MathEx.Cos(angle) * _w + .5);
                coords[1] = (int)(_y + MathEx.Sin(angle) * _h + .5);
                if (_affine != null)
                {
                    _affine.Transform(coords, 0, coords, 0, 1);
                }
                return SEG_MOVETO;
            }
            if (_index > _arcSegs)
            {
                if (_index == _arcSegs + _lineSegs)
                {
                    return SEG_CLOSE;
                }
                coords[0] = (int)(_x + .5);
                coords[1] = (int)(_y + .5);
                if (_affine != null)
                {
                    _affine.Transform(coords, 0, coords, 0, 1);
                }
                return SEG_LINETO;
            }
            angle += _increment * (_index - 1);
            double relx = MathEx.Cos(angle);
            double rely = MathEx.Sin(angle);
            coords[0] = (int)(_x + (relx - _cv * rely) * _w + .5);
            coords[1] = (int)(_y + (rely + _cv * relx) * _h + .5);
            angle += _increment;
            relx = MathEx.Cos(angle);
            rely = MathEx.Sin(angle);
            coords[2] = (int)(_x + (relx + _cv * rely) * _w + .5);
            coords[3] = (int)(_y + (rely - _cv * relx) * _h + .5);
            coords[4] = (int)(_x + relx * _w + .5);
            coords[5] = (int)(_y + rely * _h + .5);
            if (_affine != null)
            {
                _affine.Transform(coords, 0, coords, 0, 3);
            }
            return SEG_CUBICTO;
        }

    }

}
