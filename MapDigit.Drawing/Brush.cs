//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 15JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using MapDigit.Drawing.Geometry;
using MapDigit.DrawingFP;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 15JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Classes derived from this abstract base class define objects used to fill the
     * interiors of graphical shapes such as rectangles, ellipses, pies, polygons,
     * and paths.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class Brush
    {

        internal BrushFP _wrappedBrushFP;

        /**
         * Use the terminal colors to fill the remaining area.
         */
        public const int NO_CYCLE = BrushFP.NO_CYCLE;

        /**
         * Cycle the gradient colors start-to-end, end-to-start
         * to fill the remaining area.
         */
        public const int REFLECT = BrushFP.REFLECT;


        /**
         * Cycle the gradient colors start-to-end, start-to-end
         * to fill the remaining area.
         */
        public const int REPEAT = BrushFP.REPEAT;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the matrix associated with this brush.
         * @return the matrix.
         */
        public AffineTransform GetMatrix()
        {
            return Utils.ToMatrix(_wrappedBrushFP.GetMatrix());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the matrix for this brush.
         * @param Value a new matrix.
         */
        public void SetMatrix(AffineTransform value)
        {
            _wrappedBrushFP.SetMatrix(Utils.ToMatrixFP(value));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transform with a new matrix.
         * @param m1 a new matrix.
         */
        public void Transform(AffineTransform m1)
        {
            _wrappedBrushFP.Transform(Utils.ToMatrixFP(m1));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the transparency mode for this <code>Color</code>.  This is
         * required to implement the <code>Paint</code> interface.
         * @return this <code>Color</code> object's transparency mode.
         */
        public abstract int GetTransparency();

    }

}
