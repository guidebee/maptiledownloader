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
     * Represents a series of connected lines and curves.
     * This class cannot be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GraphicsPathFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public GraphicsPathFP()
        {
            _cmds = null;
            _pnts = null;
            _cmdsSize = _pntsSize = 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param from the one to be copied.
         */
        public GraphicsPathFP(GraphicsPathFP from)
        {
            _cmdsSize = from._cmdsSize;
            _pntsSize = from._pntsSize;
            if (_cmdsSize > 0)
            {
                _cmds = new int[_cmdsSize];
                _pnts = new PointFP[_pntsSize];
                Array.Copy(from._cmds, 0, _cmds, 0, _cmdsSize);
                for (int i = 0; i < _pntsSize; i++)
                {
                    _pnts[i] = new PointFP(from._pnts[i]);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create the line path from given coordinates.
         * @param ff_x1
         * @param ff_y1
         * @param ff_x2
         * @param ff_y2
         * @return
         */
        public static GraphicsPathFP CreateLine(int ffX1, int ffY1,
                int ffX2, int ffY2)
        {
            var path = new GraphicsPathFP();
            path.AddMoveTo(new PointFP(ffX1, ffY1));
            path.AddLineTo(new PointFP(ffX2, ffY2));
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create the oval path from given rectangle.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @return
         */
        public static GraphicsPathFP CreateOval(int ffXmin, int ffYmin,
                int ffXmax, int ffYmax)
        {
            var path = CreateArc(ffXmin, ffYmin,
                    ffXmax, ffYmax, 0, MathFP.PI * 2, false);
            path.AddClose();
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create an round rectangle path from given parameter.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_rx
         * @param ff_ry
         * @return
         */
        public static GraphicsPathFP CreateRoundRect(int ffXmin, int ffYmin,
                int ffXmax, int ffYmax, int ffRx, int ffRy)
        {
            const int ffPi = MathFP.PI;
            var path = new GraphicsPathFP();
            path.AddMoveTo(new PointFP(ffXmin + ffRx, ffYmin));
            path.AddLineTo(new PointFP(ffXmax - ffRx, ffYmin));
            var ffRmax = MathFP.Min(ffXmax - ffXmin, ffYmax - ffYmin) / 2;
            if (ffRx > ffRmax)
            {
                ffRx = ffRmax;
            }
            if (ffRy > ffRmax)
            {
                ffRy = ffRmax;
            }
            if (ffRx != 0 && ffRy != 0)
            {
                path.AddPath(CreateArc(ffXmax - ffRx * 2,
                        ffYmin, ffXmax, ffYmin + ffRy * 2,
                        (-ffPi) / 2, 0, false, false));
            }
            path.AddLineTo(new PointFP(ffXmax, ffYmin + ffRy));
            path.AddLineTo(new PointFP(ffXmax, ffYmax - ffRy));
            if (ffRx != 0 && ffRy != 0)
            {
                path.AddPath(CreateArc(ffXmax - ffRx * 2,
                        ffYmax - ffRy * 2, ffXmax, ffYmax, 0,
                        ffPi / 2, false, false));
            }
            path.AddLineTo(new PointFP(ffXmax - ffRx, ffYmax));
            path.AddLineTo(new PointFP(ffXmin + ffRx, ffYmax));
            if (ffRx != 0 && ffRy != 0)
            {
                path.AddPath(CreateArc(ffXmin, ffYmax - ffRy * 2,
                        ffXmin + ffRx * 2, ffYmax,
                        ffPi / 2, ffPi, false, false));
            }
            path.AddLineTo(new PointFP(ffXmin, ffYmax - ffRy));
            path.AddLineTo(new PointFP(ffXmin, ffYmin + ffRy));
            if (ffRx != 0 && ffRy != 0)
            {
                path.AddPath(CreateArc(ffXmin, ffYmin,
                        ffXmin + ffRx * 2, ffYmin + ffRy * 2, -ffPi,
                        (-ffPi) / 2, false, false));
            }
            path.AddClose();
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create a smooth curve from given parameters.
         * @param points
         * @param offset
         * @param numberOfSegments
         * @param ff_factor
         * @param closed
         * @return
         */
        public static GraphicsPathFP CreateSmoothCurves(PointFP[] points,
                int offset, int numberOfSegments, int ffFactor, bool closed)
        {
            var len = points.Length;
            var path = new GraphicsPathFP();

            if (numberOfSegments < 1 ||
                    numberOfSegments > points.Length - 1 ||
                    offset < 0 ||
                    offset + numberOfSegments > len - 1)
            {
                return path;
            }

            var pc1S = new PointFP[points.Length];
            var pc2S = new PointFP[points.Length];
            if (!closed)
            {
                pc1S[0] = points[0];
                pc2S[len - 1] = points[len - 1];
            }
            else
            {
                pc1S[0] = CalcControlPoint(points[len - 1],
                        points[0], points[1], ffFactor);
                pc2S[0] = CalcControlPoint(points[1], points[0],
                        points[len - 1], ffFactor);
                pc1S[len - 1] = CalcControlPoint(points[len - 2], points[len - 1],
                        points[0], ffFactor);
                pc2S[len - 1] = CalcControlPoint(points[0], points[len - 1],
                        points[len - 2], ffFactor);
            }
            for (var i = 1; i < len - 1; i++)
            {
                pc1S[i] = CalcControlPoint(points[i - 1], points[i],
                        points[i + 1], ffFactor);
                pc2S[i] = CalcControlPoint(points[i + 1], points[i],
                        points[i - 1], ffFactor);
            }

            path.AddMoveTo(points[offset]);
            for (var i = 0; i < numberOfSegments; i++)
            {
                path.AddCurveTo(pc1S[offset + i], pc2S[offset + i + 1],
                        points[offset + i + 1]);
            }
            if (closed)
            {
                path.AddCurveTo(pc1S[len - 1], pc2S[0], points[0]);
                path.AddClose();
            }
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * create a polyline path.
         * @param points
         * @return
         */
        public static GraphicsPathFP CreatePolyline(PointFP[] points)
        {
            var path = new GraphicsPathFP();
            if (points.Length > 0)
            {
                path.AddMoveTo(points[0]);
                for (var i = 1; i < points.Length; i++)
                {
                    path.AddLineTo(points[i]);
                }
            }
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create a polygon path.
         * @param points
         * @return
         */
        public static GraphicsPathFP CreatePolygon(PointFP[] points)
        {
            var path = CreatePolyline(points);
            if (points.Length > 0)
            {
                path.AddClose();
            }
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create a rect path.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @return
         */
        public static GraphicsPathFP CreateRect(int ffXmin, int ffYmin,
                int ffXmax, int ffYmax)
        {
            return CreatePolygon(
                    new[]{
                    new PointFP(ffXmin, ffYmin),
                    new PointFP(ffXmax, ffYmin),
                    new PointFP(ffXmax, ffYmax),
                    new PointFP(ffXmin, ffYmax)
                });
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create arc path.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_arg1
         * @param ff_arg2
         * @param closed
         * @return
         */
        public static GraphicsPathFP CreateArc(int ffXmin, int ffYmin,
                int ffXmax, int ffYmax, int ffArg1, int ffArg2,
                bool closed)
        {
            return CreateArc(ffXmin, ffYmin, ffXmax, ffYmax, ffArg1,
                    ffArg2, closed, true);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create arc path.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_startangle
         * @param ff_sweepangle
         * @param closed
         * @param standalone
         * @return
         */
        public static GraphicsPathFP CreateArc(int ffXmin, int ffYmin,
                int ffXmax, int ffYmax, int ffStartangle,
                int ffSweepangle, bool closed, bool standalone)
        {
            if (ffSweepangle < 0)
            {
                ffStartangle += ffSweepangle;
                ffSweepangle = -ffSweepangle;
            }
            var segments = MathFP.Round(MathFP.Div(4 * MathFP.Abs(ffSweepangle),
                    MathFP.PI)) >> SingleFP.DECIMAL_BITS;
            if (segments == 0)
            {
                segments = 1;
            }
            var path = new GraphicsPathFP();
            var ffDarg = ffSweepangle / segments;
            var ffArg = ffStartangle;
            var ffLastcos = MathFP.Cos(ffStartangle);
            var ffLastsin = MathFP.Sin(ffStartangle);
            var ffXc = (ffXmin + ffXmax) / 2;
            var ffYc = (ffYmin + ffYmax) / 2;
            var ffRx = (ffXmax - ffXmin) / 2;
            var ffRy = (ffYmax - ffYmin) / 2;
            var ffRxbeta = MathFP.Mul(17381, ffRx);
            var ffRybeta = MathFP.Mul(17381, ffRy);

            if (closed)
            {
                path.AddMoveTo(new PointFP(ffXc, ffYc));
            }

            for (var i = 1; i <= segments; i++)
            {
                ffArg = i == segments ? ffStartangle + ffSweepangle
                        : ffArg + ffDarg;
                var ffCurrcos = MathFP.Cos(ffArg);
                var ffCurrsin = MathFP.Sin(ffArg);
                var ffX1 = ffXc + MathFP.Mul(ffRx, ffLastcos);
                var ffY1 = ffYc + MathFP.Mul(ffRy, ffLastsin);
                var ffX2 = ffXc + MathFP.Mul(ffRx, ffCurrcos);
                var ffY2 = ffYc + MathFP.Mul(ffRy, ffCurrsin);
                if (i == 1)
                {
                    if (closed)
                    {
                        path.AddLineTo(new PointFP(ffX1, ffY1));
                    }
                    else if (standalone)
                    {
                        path.AddMoveTo(new PointFP(ffX1, ffY1));
                    }
                }

                path.AddCurveTo(
                        new PointFP(ffX1 - MathFP.Mul(ffRxbeta, ffLastsin),
                        ffY1 + MathFP.Mul(ffRybeta, ffLastcos)),
                        new PointFP(ffX2 + MathFP.Mul(ffRxbeta, ffCurrsin),
                        ffY2 - MathFP.Mul(ffRybeta, ffCurrcos)),
                        new PointFP(ffX2, ffY2));
                ffLastcos = ffCurrcos;
                ffLastsin = ffCurrsin;
            }
            if (closed)
            {
                path.AddClose();
            }
            return path;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a path to this path.
         * @param path
         */
        public void AddPath(GraphicsPathFP path)
        {
            if (path._cmdsSize > 0)
            {
                ExtendIfNeeded(path._cmdsSize, path._pntsSize);
                Array.Copy(path._cmds, 0, _cmds, _cmdsSize, path._cmdsSize);
                for (int i = 0; i < path._pntsSize; i++)
                {
                    _pnts[i + _pntsSize] = new PointFP(path._pnts[i]);
                }
                _cmdsSize += path._cmdsSize;
                _pntsSize += path._pntsSize;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * add move to this path.
         * @param point
         */
        public void AddMoveTo(PointFP point)
        {
            ExtendIfNeeded(1, 1);
            _cmds[_cmdsSize++] = CMD_MOVETO;
            _pnts[_pntsSize++] = new PointFP(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add line to this path.
         * @param point
         */
        public void AddLineTo(PointFP point)
        {
            ExtendIfNeeded(1, 1);
            _cmds[_cmdsSize++] = CMD_LINETO;
            _pnts[_pntsSize++] = new PointFP(point);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add curve to.
         * @param control
         * @param point
         */
        public void AddQuadTo(PointFP control, PointFP point)
        {
            if (control.Equals(point))
            {
                AddLineTo(point);
                return;
            }
            ExtendIfNeeded(1, 2);
            _cmds[_cmdsSize++] = CMD_QCURVETO;
            _pnts[_pntsSize++] = new PointFP(control);
            _pnts[_pntsSize++] = new PointFP(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param control1
         * @param control2
         * @param point
         */
        public void AddCurveTo(PointFP control1, PointFP control2, PointFP point)
        {
            if (_pnts[_pntsSize - 1].Equals(control1))
            {
                AddQuadTo(control2, point);
                return;
            }
            if (point.Equals(control2))
            {
                AddQuadTo(control1, point);
                return;
            }
            ExtendIfNeeded(1, 3);
            _cmds[_cmdsSize++] = CMD_CCURVETO;
            _pnts[_pntsSize++] = new PointFP(control1);
            _pnts[_pntsSize++] = new PointFP(control2);
            _pnts[_pntsSize++] = new PointFP(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Close the path
         */
        public void AddClose()
        {
            ExtendIfNeeded(1, 0);
            _cmds[_cmdsSize++] = CMD_CLOSE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Calculate outline with given pen.
         * @param lineStyle
         * @return
         */
        public GraphicsPathFP CalcOutline(PenFP lineStyle)
        {
            var outline = new GraphicsPathFP();
            var outlineGenerator =
                    new GraphicsPathOutlineFP(outline, lineStyle);
            Visit(outlineGenerator);
            return outline;
        }

        internal int[] _cmds;
        internal PointFP[] _pnts;
        internal int _cmdsSize;
        internal int _pntsSize;

        internal static readonly PointFP[] ROUNDCAP = new PointFP[7];
        internal static readonly PointFP[] SQUARECAP = new PointFP[2];
        internal static readonly int ONE;


        private const int CMD_NOP = 0;
        private const int CMD_MOVETO = 1;
        private const int CMD_LINETO = 2;
        private const int CMD_QCURVETO = 3;
        private const int CMD_CCURVETO = 4;
        private const int CMD_CLOSE = 6;
        private const int BLOCKSIZE = 16;



        static GraphicsPathFP()
        {
            ONE = SingleFP.ONE;
            ROUNDCAP[0] = new PointFP(25080, 60547);
            ROUNDCAP[1] = new PointFP(46341, 46341);
            ROUNDCAP[2] = new PointFP(60547, 25080);
            ROUNDCAP[3] = new PointFP(ONE, 0);
            ROUNDCAP[4] = new PointFP(60547, -25080);
            ROUNDCAP[5] = new PointFP(46341, -46341);
            ROUNDCAP[6] = new PointFP(25080, -60547);
            SQUARECAP[0] = new PointFP(ONE, ONE);
            SQUARECAP[1] = new PointFP(ONE, -ONE);
        }

        internal void Visit(IGraphicsPathIteratorFP iterator)
        {
            if (iterator != null)
            {
                iterator.Begin();
                int j = 0;
                for (int i = 0; i < _cmdsSize; i++)
                {
                    switch (_cmds[i])
                    {

                        case CMD_NOP:
                            break;

                        case CMD_MOVETO:
                            iterator.MoveTo(_pnts[j++]);
                            break;

                        case CMD_LINETO:
                            iterator.LineTo(_pnts[j++]);
                            break;

                        case CMD_QCURVETO:
                            iterator.QuadTo(_pnts[j++], _pnts[j++]);
                            break;

                        case CMD_CCURVETO:
                            iterator.CurveTo(_pnts[j++], _pnts[j++], _pnts[j++]);
                            break;

                        case CMD_CLOSE:
                            iterator.Close();
                            break;

                        default:
                            return;

                    }
                }
                iterator.End();
            }
        }


        private static PointFP CalcControlPoint(PointFP p1, PointFP p2,
                PointFP p3, int ffFactor)
        {
            var ps = new PointFP(p2.X + MathFP.Mul(p2.X - p1.X, ffFactor),
                    p2.Y + MathFP.Mul(p2.Y - p1.Y, ffFactor));
            return new LineFP((new LineFP(p2, ps)).GetCenter(),
                    (new LineFP(p2, p3)).GetCenter()).GetCenter();
        }


        internal void ExtendIfNeeded(int cmdsAddNum, int pntsAddNum)
        {
            if (_cmds == null)
            {
                _cmds = new int[BLOCKSIZE];
            }
            if (_pnts == null)
            {
                _pnts = new PointFP[BLOCKSIZE];
            }

            if (_cmdsSize + cmdsAddNum > _cmds.Length)
            {
                var newdata = new int[_cmds.Length + (cmdsAddNum > BLOCKSIZE
                        ? cmdsAddNum : BLOCKSIZE)];
                if (_cmdsSize > 0)
                {
                    Array.Copy(_cmds, 0, newdata, 0, _cmdsSize);
                }
                _cmds = newdata;
            }
            if (_pntsSize + pntsAddNum > _pnts.Length)
            {
                var newdata = new PointFP[_pnts.Length +
                        (pntsAddNum > BLOCKSIZE ? pntsAddNum : BLOCKSIZE)];
                if (_pntsSize > 0)
                {
                    Array.Copy(_pnts, 0, newdata, 0, _pntsSize);
                }
                _pnts = newdata;
            }
        }
    }
}
