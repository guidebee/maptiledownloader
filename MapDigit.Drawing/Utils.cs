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
     * utility functions to Convert class from drawing to drawingfp.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    internal abstract class Utils
    {

        internal static MatrixFP ToMatrixFP(AffineTransform matrix)
        {
            if (matrix == null)
            {
                return null;
            }
            if (matrix.IsIdentity())
            {
                return MatrixFP.Identity;
            }

            MatrixFP matrixFP = new MatrixFP(SingleFP.FromDouble(matrix.GetScaleX()),
                    SingleFP.FromDouble(matrix.GetScaleY()),
                    SingleFP.FromDouble(matrix.GetShearX()),
                    SingleFP.FromDouble(matrix.GetShearY()),
                    SingleFP.FromDouble(matrix.GetTranslateX()),
                    SingleFP.FromDouble(matrix.GetTranslateY()));
            return matrixFP;
        }

        internal static AffineTransform ToMatrix(MatrixFP matrixFP)
        {
            if (matrixFP == null)
            {
                return null;
            }
            if (matrixFP.IsIdentity())
            {
                return new AffineTransform();
            }
            AffineTransform matrix = new AffineTransform(SingleFP.ToDouble(matrixFP.ScaleX),
                    SingleFP.ToDouble(matrixFP.RotateX),
                    SingleFP.ToDouble(matrixFP.RotateY),
                    SingleFP.ToDouble(matrixFP.ScaleY),
                    SingleFP.ToDouble(matrixFP.TranslateX),
                    SingleFP.ToDouble(matrixFP.TranslateY));
            return matrix;
        }

        internal static RectangleFP ToRectangleFP(Rectangle rect)
        {
            return new RectangleFP(
                    SingleFP.FromInt(rect.GetMinX()),
                    SingleFP.FromInt(rect.GetMinY()),
                    SingleFP.FromInt(rect.GetMaxX()),
                    SingleFP.FromInt(rect.GetMaxY()));
        }



        public static PointFP ToPointFP(Point pnt)
        {
            return new PointFP(SingleFP.FromInt(pnt.X), SingleFP.FromInt(pnt.Y));
        }


        public static Point ToPoint(PointFP pnt)
        {
            return new Point(SingleFP.ToInt(pnt.X), SingleFP.ToInt(pnt.Y));
        }



        internal static PointFP[] ToPointFPArray(Point[] pnts)
        {
            PointFP[] result = new PointFP[pnts.Length];
            for (int i = 0; i < pnts.Length; i++)
            {
                result[i] = ToPointFP(pnts[i]);
            }
            return result;
        }


        public static Point[] ToPointArray(PointFP[] pnts)
        {
            Point[] result = new Point[pnts.Length];
            for (int i = 0; i < pnts.Length; i++)
            {
                result[i] = ToPoint(pnts[i]);
            }
            return result;
        }



    }

}
