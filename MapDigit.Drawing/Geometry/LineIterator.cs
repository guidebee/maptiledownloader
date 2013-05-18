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
     * A utility class to iterate over the path segments of a line segment
     * through the IPathIterator interface.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class LineIterator : PathIterator
    {
        readonly Line _line;
        readonly AffineTransform _affine;
        int _index;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param l
         * @param at
         */
        internal LineIterator(Line l, AffineTransform at)
        {
            _line = l;
            _affine = at;
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
            return (_index > 1);
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
        /**
         * Returns the coordinates and type of the current path segment in
         * the iteration.
         * The return Value is the path segment type:
         * SEG_MOVETO, SEG_LINETO, SEG_QUADTO, SEG_CUBICTO, or SEG_CLOSE.
         * A long array of length 6 must be passed in and may be used to
         * store the coordinates of the point(s).
         * Each point is stored as a pair of long x,y coordinates.
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
                throw new IndexOutOfRangeException("line iterator out of bounds");
            }
            int type;
            if (_index == 0)
            {
                coords[0] = _line.X1;
                coords[1] = _line.Y1;
                type = SEG_MOVETO;
            }
            else
            {
                coords[0] = _line.X2;
                coords[1] = _line.Y2;
                type = SEG_LINETO;
            }
            if (_affine != null)
            {
                _affine.Transform(coords, 0, coords, 0, 1);
            }
            return type;
        }


    }

}
