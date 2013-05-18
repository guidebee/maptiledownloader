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
namespace MapDigit.DrawingFP
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines objects used to fill the interiors of graphical shapes such as
     * rectangles, ellipses, pies, polygons, and paths.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class BrushFP
    {

        /**
         * Use the terminal colors to fill the remaining area.
         */
        public const int NO_CYCLE = 0;

        /**
         * Cycle the gradient colors start-to-end, end-to-start
         * to fill the remaining area.
         */
        public const int REFLECT = 1;


        /**
         * Cycle the gradient colors start-to-end, start-to-end
         * to fill the remaining area.
         */
        public const int REPEAT = 2;


        public int FillMode = REPEAT;
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the matrix associated with this brush.
         * @return the matrix.
         */
        public MatrixFP GetMatrix()
        {
            return _matrix;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the matrix for this brush.
         * @param Value a new matrix.
         */
        public void SetMatrix(MatrixFP value)
        {
            _matrix = new MatrixFP(value);
            _matrix.Invert();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the matrix associated with the graphics object.
         * @return the matrix of the graphics object.
         */
        internal MatrixFP GetGraphicsMatrix()
        {
            return _graphicsMatrix;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the graphics matrix.
         * @param Value the graphics matrix.
         */
        internal void SetGraphicsMatrix(MatrixFP value)
        {
            _graphicsMatrix = new MatrixFP(value);
            _graphicsMatrix.Invert();
            _finalMatrix = new MatrixFP(_graphicsMatrix);
            if (_matrix != null)
            {
                _finalMatrix.Multiply(_matrix);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transform with a new matrix.
         * @param m1 a new matrix.
         */
        public void Transform(MatrixFP m1)
        {
            var m = new MatrixFP(m1);
            m.Invert();
            if (_matrix == null)
            {
                _matrix = m;
            }
            else
            {
                _matrix.Multiply(m);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check if it's a mono color brush.
         * @return true if it's mono color brush.
         */
        public abstract bool IsMonoColor();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the color Value at given position.
         * @param x x coordinate.
         * @param y y coordinate.
         * @param singlePoint single point or not.
         * @return the color Value.
         */
        public abstract int GetColorAt(int x, int y, bool singlePoint);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next color for this brush.
         * @return the next color.
         */
        public abstract int GetNextColor();

        /**
         * Brush matrix.
         */
        protected MatrixFP _matrix;

        /**
         * Graphics matrix.
         */
        protected MatrixFP _graphicsMatrix;

        /**
         * The combined matrix.
         */
        protected MatrixFP _finalMatrix;
    }
}
