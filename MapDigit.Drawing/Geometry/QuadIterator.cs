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
     * A utility class to iterate over the path segments of a quadratic curve
     * segment through the IPathIterator interface.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class QuadIterator : PathIterator
    {
        readonly QuadCurve _quad;
        readonly AffineTransform _affine;
        int _index;

        internal QuadIterator(QuadCurve q, AffineTransform at)
        {
            _quad = q;
            _affine = at;
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
            return (_index > 1);
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
                throw new IndexOutOfRangeException("quad iterator iterator out of bounds");
            }
            int type;
            if (_index == 0)
            {
                coords[0] = _quad.GetX1();
                coords[1] = _quad.GetY1();
                type = SEG_MOVETO;
            }
            else
            {
                coords[0] = _quad.GetCtrlX();
                coords[1] = _quad.GetCtrlY();
                coords[2] = _quad.GetX2();
                coords[3] = _quad.GetY2();
                type = SEG_QUADTO;
            }
            if (_affine != null)
            {
                _affine.Transform(coords, 0, coords, 0, _index == 0 ? 1 : 2);
            }
            return type;
        }
    }

}
