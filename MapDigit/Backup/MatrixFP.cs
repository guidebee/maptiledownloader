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
     * affine matrix in fixed point format.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class MatrixFP
    {

        /**
         * Scale X factor.
         */
        public int ScaleX; // ScaleX

        /**
         * Scale Y factor.
         */
        public int ScaleY; // ScaleY

        /**
         * Rotate/Shear X factor.
         */
        public int RotateX; // RotateSkewX

        /**
         * Rotate/Shear Y factor.
         */
        public int RotateY; // RotateSkewY

        /**
         * Translate X.
         */
        public int TranslateX; // TranslateX

        /**
         * Translate Y.
         */
        public int TranslateY; // TranslateY

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         */
        public MatrixFP()
        {
            ScaleX = ScaleY = SingleFP.ONE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param ff_sx
         * @param ff_sy
         * @param ff_rx
         * @param ff_ry
         * @param ff_tx
         * @param ff_ty
         */
        public MatrixFP(int ffSx, int ffSy, int ffRx, int ffRy,
                int ffTx, int ffTy)
        {
            Reset(ffSx, ffSy, ffRx, ffRy, ffTx, ffTy);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param m
         */
        public MatrixFP(MatrixFP m)
        {
            Reset(m.ScaleX, m.ScaleY, m.RotateX, m.RotateY, m.TranslateX, m.TranslateY);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset to identity matrix.
         */
        public void Reset()
        {
            Reset(SingleFP.ONE, SingleFP.ONE, 0, 0, 0, 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Identiry matrix.
         * @return
         */
        public static MatrixFP Identity = new MatrixFP(SingleFP.ONE, SingleFP.ONE, 0, 0, 0, 0);


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check if it's identity matrix.
         * @return
         */
        public bool IsIdentity()
        {
            return ScaleX == SingleFP.ONE && ScaleY == SingleFP.ONE && RotateX == 0
                    && RotateY == 0 && TranslateX == 0 && TranslateY == 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check to see if its invertiable.
         * @return
         */
        public bool IsInvertible()
        {
            return Determinant() != 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the matrix to give Value.
         * @param ff_sx
         * @param ff_sy
         * @param ff_rx
         * @param ff_ry
         * @param ff_tx
         * @param ff_ty
         */
        public void Reset(int ffSx, int ffSy, int ffRx, int ffRy,
                int ffTx, int ffTy)
        {
            ScaleX = ffSx;
            ScaleY = ffSy;
            RotateX = ffRx;
            RotateY = ffRy;
            TranslateX = ffTx;
            TranslateY = ffTy;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * rotate operation.
         * @param ff_ang
         * @return
         */
        public MatrixFP Rotate(int ffAng)
        {
            var ffSin = MathFP.Sin(ffAng);
            var ffCos = MathFP.Cos(ffAng);
            return Multiply(new MatrixFP(ffCos, ffCos, ffSin, -ffSin, 0, 0));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Shear or rotate operation.
         * @param ff_rx
         * @param ff_ry
         * @return
         */
        public MatrixFP RotateSkew(int ffRx, int ffRy)
        {
            return Multiply(new MatrixFP(SingleFP.ONE, SingleFP.ONE, ffRx, ffRy, 0, 0));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * translate operation.
         * @param ff_dx
         * @param ff_dy
         * @return
         */
        public MatrixFP Translate(int ffDx, int ffDy)
        {
            TranslateX += ffDx;
            TranslateY += ffDy;
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Scale operation.
         * @param ff_sx
         * @param ff_sy
         * @return
         */
        public MatrixFP Scale(int ffSx, int ffSy)
        {
            Reset(MathFP.Mul(ffSx, ScaleX), MathFP.Mul(ffSy, ScaleY),
                    MathFP.Mul(ffSy, RotateX), MathFP.Mul(ffSx, RotateY),
                    MathFP.Mul(ffSx, TranslateX), MathFP.Mul(ffSy, TranslateY));
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * multipy with another matrix.
         * @param m
         * @return
         */
        public MatrixFP Multiply(MatrixFP m)
        {
            Reset(MathFP.Mul(m.ScaleX, ScaleX) + MathFP.Mul(m.RotateY, RotateX),
                    MathFP.Mul(m.RotateX, RotateY) + MathFP.Mul(m.ScaleY, ScaleY),
                    MathFP.Mul(m.RotateX, ScaleX) + MathFP.Mul(m.ScaleY, RotateX),
                    MathFP.Mul(m.ScaleX, RotateY) + MathFP.Mul(m.RotateY, ScaleY),
                    MathFP.Mul(m.ScaleX, TranslateX) + MathFP.Mul(m.RotateY, TranslateY) + m.TranslateX,
                    MathFP.Mul(m.RotateX, TranslateX) + MathFP.Mul(m.ScaleY, TranslateY) + m.TranslateY);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * compare with another object.
         * @param obj
         * @return
         */
        public new bool Equals(object obj) {
        if (obj is MatrixFP) {
            var m = (MatrixFP) obj;
            return  m.RotateX == RotateX && m.RotateY == RotateY
                    && m.ScaleX == ScaleX && m.ScaleY == ScaleY 
                    && m.TranslateX == TranslateX && m.TranslateY == TranslateY;
        }
        return false;
    }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the hashcode for this transform.
         * @return      a hash code for this transform.
         */
        public int HashCode()
        {
            return RotateX << 24 + RotateY << 20 + ScaleX << 16 
                + ScaleY << 8 + TranslateX << 4 + TranslateY;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * calculat the determinat of the matrix.
         * @return
         */
        private int Determinant()
        {
            var ffDet = MathFP.Mul(ScaleX, ScaleY) - MathFP.Mul(RotateX, RotateY);
            return ffDet;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * invert the matrix.
         * @return
         */
        public MatrixFP Invert()
        {
            int ffDet = Determinant();
            if (ffDet == 0)
            {
                Reset();
            }
            else
            {
                var ffSxNew = MathFP.Div(ScaleY, ffDet);
                var ffSyNew = MathFP.Div(ScaleX, ffDet);
                var ffRxNew = -MathFP.Div(RotateX, ffDet);
                var ffRyNew = -MathFP.Div(RotateY, ffDet);
                var ffTxNew = MathFP.Div(MathFP.Mul(TranslateY, RotateY)
                        - MathFP.Mul(TranslateX, ScaleY), ffDet);
                var ffTyNew = -MathFP.Div(MathFP.Mul(TranslateY, ScaleX)
                        - MathFP.Mul(TranslateX, RotateX), ffDet);
                Reset(ffSxNew, ffSyNew, ffRxNew, ffRyNew,
                        ffTxNew, ffTyNew);
            }
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to a string.
         * @return
         */
        public override string ToString()
        {
            return " Matrix(sx,sy,rx,ry,tx,ty)=(" + new SingleFP(ScaleX)
                    + "," + new SingleFP(ScaleY) + "," + new SingleFP(RotateX)
                    + "," + new SingleFP(RotateY) + "," + new SingleFP(TranslateX)
                    + "," + new SingleFP(TranslateY) + ")";
        }
    }

}
