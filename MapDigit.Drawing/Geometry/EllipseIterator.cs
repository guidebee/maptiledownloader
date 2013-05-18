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
     * A utility class to iterate over the path segments of an ellipse
     * through the IPathIterator interface.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class EllipseIterator : PathIterator
    {
        readonly double _x;
        readonly double _y;
        readonly double _w;
        readonly double _h;
        readonly AffineTransform _affine;
        int _index;

        internal EllipseIterator(Ellipse e, AffineTransform at)
        {
            _x = e.GetX();
            _y = e.GetY();
            _w = e.GetWidth();
            _h = e.GetHeight();
            _affine = at;
            if (_w < 0 || _h < 0)
            {
                _index = 6;
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
            return _index > 5;
        }

        /**
         * Moves the iterator to the next segment of the path forwards
         * along the primary direction of traversal as long as there are
         * more points in that direction.
         */
        public override void Next()
        {
            _index++;
        }    // ArcIterator.btan(Math.PI/2)
        public const double CTRL_VAL = 0.5522847498307933;

        /*
         * ctrlpts contains the control points for a set of 4 cubic
         * bezier curves that approximate a circle of radius 0.5
         * centered at 0.5, 0.5
         */
        private const double PCV = 0.5 + CTRL_VAL * 0.5;
        private const double NCV = 0.5 - CTRL_VAL * 0.5;
        private static readonly double[][] Ctrlpts = new[]
                                                     {
        new[] {1.0, PCV, PCV, 1.0, 0.5, 1.0},
        new[] {NCV, 1.0, 0.0, PCV, 0.0, 0.5},
        new[] {0.0, NCV, NCV, 0.0, 0.5, 0.0},
        new[] {PCV, 0.0, 1.0, NCV, 1.0, 0.5}
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
                throw new IndexOutOfRangeException("ellipse iterator out of bounds");
            }
            if (_index == 5)
            {
                return SEG_CLOSE;
            }
            if (_index == 0)
            {
                double[] ctrls = Ctrlpts[3];
                coords[0] = (int)(_x + ctrls[4] * _w + .5);
                coords[1] = (int)(_y + ctrls[5] * _h + .5);
                if (_affine != null)
                {
                    _affine.Transform(coords, 0, coords, 0, 1);
                }
                return SEG_MOVETO;
            }
            {
                double[] ctrls = Ctrlpts[_index - 1];
                coords[0] = (int)(_x + ctrls[0] * _w + .5);
                coords[1] = (int)(_y + ctrls[1] * _h + .5);
                coords[2] = (int)(_x + ctrls[2] * _w + .5);
                coords[3] = (int)(_y + ctrls[3] * _h + .5);
                coords[4] = (int)(_x + ctrls[4] * _w + .5);
                coords[5] = (int)(_y + ctrls[5] * _h + .5);
                if (_affine != null)
                {
                    _affine.Transform(coords, 0, coords, 0, 3);
                }
            }
            return SEG_CUBICTO;
        }
    }

}
