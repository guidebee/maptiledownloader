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
     * provide dash support.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class GraphicsPathDasherFP : GraphicsPathSketchFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param from  the path which need to be dashed.
         * @param dashArray the dash array.
         * @param offset  from where the dash starts.
         */
        public GraphicsPathDasherFP(GraphicsPathFP from, int[] dashArray, int offset)
        {
            _fromPath = new GraphicsPathFP(from);
            var arrayLength = dashArray.Length - offset;
            if (arrayLength > 1)
            {
                _pnts = new PointFP[BLOCKSIZE];
                _cmds = new int[BLOCKSIZE];
                _dashArray = new int[dashArray.Length - offset];
                Array.Copy(dashArray, offset,
                        _dashArray, 0, dashArray.Length);
                VisitPath(this);
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the dashed path, if the dash array is null, return the path
         * unchanged.
         * @return the dash path.
         */
        public GraphicsPathFP GetDashedGraphicsPath()
        {
            if (_dashArray == null)
            {
                return _fromPath;
            }

            var dashedPath = new GraphicsPathFP();
            var lineFP = new LineFP();
            var j = 0;
            for (var i = 0; i < _cmdsSize; i++)
            {
                switch (_cmds[i])
                {

                    case CMD_MOVETO:
                        dashedPath.AddMoveTo(_pnts[j++]);
                        break;
                    case CMD_LINETO:
                        {
                            int pointIndex = j;
                            lineFP.Reset(_pnts[pointIndex - 1], _pnts[pointIndex]);
                            DashLine(dashedPath, lineFP);
                            j++;
                        }
                        break;
                    case CMD_CLOSE:
                        dashedPath.AddClose();
                        break;
                }
            }

            return dashedPath;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param point
         */
        public override void MoveTo(PointFP point)
        {
            base.MoveTo(point);
            ExtendIfNeeded(1, 1);
            AddMoveTo(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 11NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param point
         */
        public override void LineTo(PointFP point)
        {
            base.LineTo(point);
            ExtendIfNeeded(1, 1);
            AddLineTo(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 11NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void Close()
        {
            base.Close();
            ExtendIfNeeded(1, 0);
            AddClose();
        }


        private const int CMD_NOP = 0;
        private const int CMD_MOVETO = 1;
        private const int CMD_LINETO = 2;
        private const int CMD_QCURVETO = 3;
        private const int CMD_CCURVETO = 4;
        private const int CMD_CLOSE = 6;
        private const int BLOCKSIZE = 16;
        private int[] _cmds;
        private PointFP[] _pnts;
        private int _cmdsSize;
        private int _pntsSize;
        private readonly GraphicsPathFP _fromPath;
        private readonly int[] _dashArray;
        private int _dashIndex;
        private int _nextDistance = -1;
        private bool _isEmpty;

        private void AddMoveTo(PointFP point)
        {

            _cmds[_cmdsSize++] = CMD_MOVETO;
            _pnts[_pntsSize++] = new PointFP(point);
        }

        private void AddLineTo(PointFP point)
        {

            _cmds[_cmdsSize++] = CMD_LINETO;
            _pnts[_pntsSize++] = new PointFP(point);
        }

        private void AddClose()
        {
            _cmds[_cmdsSize++] = CMD_CLOSE;
        }

        private void ExtendIfNeeded(int cmdsAddNum, int pntsAddNum)
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

        private void DashLine(GraphicsPathFP path, LineFP line)
        {

            if (_nextDistance < 0)
            {
                _nextDistance = _dashArray[_dashIndex];
                _dashIndex = (_dashIndex + 1) % _dashArray.Length;
            }
            var distance = _nextDistance;

            var pt = line.GetPointAtDistance(distance);
            while (pt != null)
            {
                if (_isEmpty)
                {
                    path.AddMoveTo(pt);
                }
                else
                {
                    path.AddLineTo(pt);
                }

                _isEmpty = !_isEmpty;
                _nextDistance += _dashArray[_dashIndex];
                distance = _nextDistance;
                pt = line.GetPointAtDistance(distance);
                _dashIndex = (_dashIndex + 1) % _dashArray.Length;
            }
            if (_isEmpty)
            {
                path.AddMoveTo(line.Pt2);
            }
            else
            {
                path.AddLineTo(line.Pt2);
            }
            _nextDistance = _nextDistance - line.GetLength();


        }

        private void VisitPath(IGraphicsPathIteratorFP iterator)
        {
            if (iterator != null)
            {

                iterator.Begin();
                int j = 0;
                for (int i = 0; i < _fromPath._cmdsSize; i++)
                {
                    switch (_fromPath._cmds[i])
                    {

                        case CMD_NOP:
                            break;

                        case CMD_MOVETO:
                            iterator.MoveTo(_fromPath._pnts[j++]);
                            break;

                        case CMD_LINETO:
                            iterator.LineTo(_fromPath._pnts[j++]);
                            break;

                        case CMD_QCURVETO:
                            iterator.QuadTo(_fromPath._pnts[j++],
                                    _fromPath._pnts[j++]);
                            break;

                        case CMD_CCURVETO:
                            iterator.CurveTo(_fromPath._pnts[j++],
                                    _fromPath._pnts[j++], _fromPath._pnts[j++]);
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
    }
}
