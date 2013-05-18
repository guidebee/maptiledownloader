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
     * A utility class to iterate over the path segments of an rounded rectangle
     * through the IPathIterator interface.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class RoundRectIterator : PathIterator
    {
        readonly double _x;
        readonly double _y;
        readonly double _w;
        readonly double _h;
        readonly double _aw;
        readonly double _ah;
        readonly AffineTransform _affine;
        int _index;

        internal RoundRectIterator(RoundRectangle rr, AffineTransform at)
        {
            _x = rr.GetX();
            _y = rr.GetY();
            _w = rr.GetWidth();
            _h = rr.GetHeight();
            _aw = Math.Min(_w, Math.Abs(rr.GetArcWidth()));
            _ah = Math.Min(_h, Math.Abs(rr.GetArcHeight()));
            _affine = at;
            if (_aw < 0 || _ah < 0)
            {
                // Don't draw anything...
                _index = CTRLPTS.Length;
            }
        }

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

        /**
         * Tests if there are more points to read.
         * @return true if there are more points to read
         */
        public override bool IsDone()
        {
            return _index >= CTRLPTS.Length;
        }

        /**
         * Moves the iterator to the next segment of the path forwards
         * along the primary direction of traversal as long as there are
         * more points in that direction.
         */
        public override void Next()
        {
            _index++;
        }

        private const double ANGLE = Math.PI / 4.0;
        private static readonly double A = 1.0 - Math.Cos(ANGLE);
        private static readonly double B = Math.Tan(ANGLE);
        private static readonly double C = Math.Sqrt(1.0 + B * B) - 1 + A;
        private static readonly double CV = 4.0 / 3.0 * A * B / C;
        private static readonly double ACV = (1.0 - CV) / 2.0;

        // For each array:
        //     4 values for each point {v0, v1, v2, v3}:
        //         point = (x + v0 * w + v1 * arcWidth,
        //                  y + v2 * h + v3 * arcHeight);
        private static readonly double[][] CTRLPTS = new[]
                                                         {
	new[] {  0.0,  0.0,  0.0,  0.5 },
	new[]{  0.0,  0.0,  1.0, -0.5 },
	new[]{  0.0,  0.0,  1.0, -ACV,
	   0.0,  ACV,  1.0,  0.0,
	   0.0,  0.5,  1.0,  0.0 },
	new[]{  1.0, -0.5,  1.0,  0.0 },
	new[]{  1.0, -ACV,  1.0,  0.0,
	   1.0,  0.0,  1.0, -ACV,
	   1.0,  0.0,  1.0, -0.5 },
	new[]{  1.0,  0.0,  0.0,  0.5 },
	new[]{  1.0,  0.0,  0.0,  ACV,
	   1.0, -ACV,  0.0,  0.0,
	   1.0, -0.5,  0.0,  0.0 },
	new[]{  0.0,  0.5,  0.0,  0.0 },
	new[]{  0.0,  ACV,  0.0,  0.0,
	   0.0,  0.0,  0.0,  ACV,
	   0.0,  0.0,  0.0,  0.5 },
	new double[]{},
    };
        private static readonly int[] TYPES = {
	SEG_MOVETO,
	SEG_LINETO, SEG_CUBICTO,
	SEG_LINETO, SEG_CUBICTO,
	SEG_LINETO, SEG_CUBICTO,
	SEG_LINETO, SEG_CUBICTO,
	SEG_CLOSE,
    };

        /**
         * Returns the coordinates and type of the current path segment in
         * the iteration.
         * The return Value is the path segment type:
         * SEG_MOVETO, SEG_LINETO, SEG_QUADTO, SEG_CUBICTO, or SEG_CLOSE.
         * A int array of length 6 must be passed in and may be used to
         * store the coordinates of the point(s).
         * Each point is stored as a pair of int x,y coordinates.
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
                throw new IndexOutOfRangeException("roundrect iterator out of bounds");
            }
            double[] ctrls = CTRLPTS[_index];
            int nc = 0;
            for (int i = 0; i < ctrls.Length; i += 4)
            {
                coords[nc++] = (int)(_x + ctrls[i + 0] * _w + ctrls[i + 1] * _aw + .5);
                coords[nc++] = (int)(_y + ctrls[i + 2] * _h + ctrls[i + 3] * _ah + .5);
            }
            if (_affine != null)
            {
                _affine.Transform(coords, 0, coords, 0, nc / 2);
            }
            return TYPES[_index];
        }


    }

}
