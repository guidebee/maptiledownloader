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
     * the class uses line to draw a sketch for a given path.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class GraphicsPathSketchFP : IGraphicsPathIteratorFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the current point.
         * @return
         */
        public PointFP CurrentPoint()
        {
            return _currPoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the start point.
         * @return
         */
        public PointFP StartPoint()
        {
            return _startPoint;
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         */
        public virtual void Begin()
        {
            _started = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         */
        public virtual void End()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param point
         */
        public virtual void MoveTo(PointFP point)
        {
            if (!_started)
            {
                _startPoint.Reset(point);
                _started = true;
            }
            _currPoint.Reset(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param point
         */
        public virtual void LineTo(PointFP point)
        {
            _currPoint.Reset(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param control
         * @param point
         */
        public virtual void QuadTo(PointFP control, PointFP point)
        {
            // Compute forward difference values for a quadratic
            // curve of type A*(1-t)^2 + 2*B*t*(1-t) + C*t^2

            var f = new PointFP(_currPoint);
            var tmp = new PointFP((_currPoint.X - control.X * 2 + point.X)
                    / SUBDIVIDE2, (_currPoint.Y - control.Y * 2 + point.Y)
                    / SUBDIVIDE2);
            var ddf = new PointFP(tmp.X * 2, tmp.Y * 2);
            var df = new PointFP(tmp.X + (control.X - _currPoint.X) * 2
                    / SUBDIVIDE, tmp.Y + (control.Y - _currPoint.Y) * 2 / SUBDIVIDE);

            for (int c = 0; c < SUBDIVIDE - 1; c++)
            {
                f.Add(df);
                df.Add(ddf);
                LineTo(f);
            }

            // We specify the last point manually since
            // we obtain rounding errors during the
            // forward difference computation.
            LineTo(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        public virtual void CurveTo(PointFP control1, PointFP control2, PointFP point)
        {
            var tmp1 = new PointFP(_currPoint.X - control1.X * 2 + control2.X,
                    _currPoint.Y - control1.Y * 2 + control2.Y);
            var tmp2 = new PointFP((control1.X - control2.X) * 3 - _currPoint.X
                    + point.X, (control1.Y - control2.Y) * 3 - _currPoint.Y + point.Y);

            var f = new PointFP(_currPoint);
            var df = new PointFP((control1.X - _currPoint.X) * 3 / SUBDIVIDE
                    + tmp1.X * 3 / SUBDIVIDE2 + tmp2.X / SUBDIVIDE3,
                    (control1.Y - _currPoint.Y) * 3 / SUBDIVIDE + tmp1.Y * 3
                    / SUBDIVIDE2 + tmp2.Y / SUBDIVIDE3);
            var ddf = new PointFP(tmp1.X * 6 / SUBDIVIDE2 + tmp2.X * 6
                    / SUBDIVIDE3, tmp1.Y * 6 / SUBDIVIDE2 + tmp2.Y * 6 / SUBDIVIDE3);
            var dddf = new PointFP(tmp2.X * 6
                    / SUBDIVIDE3, tmp2.Y * 6 / SUBDIVIDE3);

            for (var c = 0; c < SUBDIVIDE - 1; c++)
            {
                f.Add(df);
                df.Add(ddf);
                ddf.Add(dddf);
                LineTo(f);
            }

            LineTo(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         */
        public virtual void Close()
        {
            // Connect start point with end point
            LineTo(_startPoint);
            _started = false;
        }

        protected const int SUBDIVIDE = 24;
        protected const int SUBDIVIDE2 = SUBDIVIDE * SUBDIVIDE;
        protected const int SUBDIVIDE3 = SUBDIVIDE2 * SUBDIVIDE;
        protected PointFP _startPoint = new PointFP();
        protected PointFP _currPoint = new PointFP();
        protected bool _started;
    }
}
