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
     * An class that describes a gradient, composed of gradient stops.
     * Classes that inherit from GradientBrush describe different ways of
     * interpreting gradient stops.
     * be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class LinearGradientBrushFP : BrushFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create the gradient brush.
         * @param ff_xmin the top left coordinate.
         * @param ff_ymin the top left coordinate.
         * @param ff_xmax the bottom right coordinate.
         * @param ff_ymax the bottom right coordinate.
         * @param ff_angle the angle for this gradient.
         * @param type  the type of the gradient brush.
         */
        public LinearGradientBrushFP(int ffXmin, int ffYmin, int ffXmax, int ffYmax,
                int ffAngle)
        {
            _bounds.Reset(ffXmin, ffYmin,
                    ffXmax == ffXmin ? ffXmin + 1 : ffXmax,
                    ffYmax == ffYmin ? ffYmin + 1 : ffYmax);
            _matrix = new MatrixFP();
            _centerPt.Reset(ffXmin + (ffXmax - ffXmin) / 2,
                    ffYmin + (ffYmax - ffYmin) / 2);
            _matrix.Translate(-_centerPt.X, -_centerPt.Y);
            _matrix.Rotate(-ffAngle);
            //matrix.translate((ff_xmin + ff_xmax) / 2,(ff_ymin + ff_ymax) / 2);

            var ffAng = MathFP.Atan(MathFP.Div(_bounds.GetHeight(),
                    _bounds.GetWidth() == 0 ? 1 : _bounds.GetWidth()));
            var ffLen = PointFP.Distance(_bounds.GetHeight(), _bounds.GetWidth());
            _ffLength = MathFP.Mul(ffLen, MathFP.Max(
                    MathFP.Abs(MathFP.Cos(ffAngle - ffAng)),
                    MathFP.Abs(MathFP.Cos(ffAngle + ffAng))));

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set gradient color at given ratio.
         * @param ratio the ratio Value.
         * @param color the color Value.
         */
        public void SetGradientColor(int ratio, int color)
        {
            ratio = ratio >> SingleFP.DECIMAL_BITS - RATIO_BITS;
            int i;
            ratio = ratio < 0 ? 0 : (ratio > RATIO_MAX ? RATIO_MAX : ratio);
            if (_ratioCount == _ratios.Length)
            {
                var rs = new int[_ratioCount + 16];
                Array.Copy(_ratios, 0, rs, 0, _ratioCount);
                _ratios = rs;
            }
            _gradientColors[ratio] = color;
            for (i = _ratioCount; i > 0; i--)
            {
                if (ratio >= _ratios[i - 1])
                {
                    break;
                }
            }
            if (!(i > 0 && ratio == _ratios[i]))
            {
                if (i < _ratioCount)
                {
                    Array.Copy(_ratios, i, _ratios, i + 1, _ratioCount - i);
                }
                _ratios[i] = ratio;
                _ratioCount++;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * update the gradient table.
         */
        public void UpdateGradientTable()
        {
            if (_ratioCount == 0)
            {
                return;
            }
            int i;
            for (i = 0; i < _ratios[0]; i++)
            {
                _gradientColors[i] = _gradientColors[_ratios[0]];
            }
            for (i = 1; i < _ratioCount; i++)
            {
                var r1 = _ratios[i - 1];
                var r2 = _ratios[i];
                for (var j = r1 + 1; j < r2; j++)
                {
                    _gradientColors[j] = Interpolate(_gradientColors[r1],
                            _gradientColors[r2], 256 * (j - r1) / (r2 - r1));
                }
            }
            for (i = _ratios[_ratioCount - 1]; i <= RATIO_MAX; i++)
            {
                _gradientColors[i] = _gradientColors[_ratios[_ratioCount - 1]];
            }
        }

        private static int Interpolate(int a, int b, int pos)
        {
            var p2 = pos & 0xFF;
            var p1 = 0xFF - p2;
            var ca = ((a >> 24) & 0xFF) * p1 + ((b >> 24) & 0xFF) * p2;
            var cr = ((a >> 16) & 0xFF) * p1 + ((b >> 16) & 0xFF) * p2;
            var cg = ((a >> 8) & 0xFF) * p1 + ((b >> 8) & 0xFF) * p2;
            var cb = (a & 0xFF) * p1 + (b & 0xFF) * p2;
            return ((ca >> 8) << 24) | ((cr >> 8) << 16) | ((cg >> 8) << 8) | ((cb >> 8));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @return always return false.
         */
        public override bool IsMonoColor()
        {
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param x the x coordinate
         * @param y the y coordinate
         * @param singlePoint
         * @return the color at given position.
         */
        public override int GetColorAt(int x, int y, bool singlePoint)
        {
            var p = new PointFP(x << SingleFP.DECIMAL_BITS,
                    y << SingleFP.DECIMAL_BITS);
            PointFP p1 = null;
            if (!singlePoint)
            {
                p1 = new PointFP(p.X + SingleFP.ONE, p.Y);
            }
            if (_finalMatrix != null)
            {
                p.Transform(_finalMatrix);
                if (!singlePoint)
                {
                    p1.Transform(_finalMatrix);
                }
            }
            //int width = bounds.getWidth();
            //int height = bounds.getHeight();

            var v = p.X + _ffLength / 2;
            _ffCurrpos = (int)(((long)v << RATIO_BITS + SingleFP.DECIMAL_BITS)
                    / _ffLength);
            if (!singlePoint)
            {
                _ffDeltapos = (int)(((long)(p1.X - p.X)
                        << RATIO_BITS + SingleFP.DECIMAL_BITS) / _ffLength);
            }
            int pos = _ffCurrpos >> SingleFP.DECIMAL_BITS;
            pos -= 512;

            //pos >>= Brush.XY_MAX_BITS - RATIO_BITS;

            switch (FillMode)
            {
                case REFLECT:
                    pos = pos % (RATIO_MAX * 2);
                    pos = pos < 0 ? pos + RATIO_MAX * 2 : pos;
                    pos = (pos < RATIO_MAX) ? pos : RATIO_MAX * 2 - pos;
                    break;
                case REPEAT:
                    pos = pos % RATIO_MAX;
                    pos = pos < 0 ? pos + RATIO_MAX : pos;
                    break;
                case NO_CYCLE:
                    pos = pos < 0 ? 0 : (pos > RATIO_MAX ? RATIO_MAX : pos);
                    break;
            }

            return _gradientColors[pos];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @return next color of the gradient brush.
         */
        public override int GetNextColor()
        {
            _ffCurrpos += _ffDeltapos;
            var pos = _ffCurrpos >> SingleFP.DECIMAL_BITS;

            pos -= 512;

            switch (FillMode)
            {
                case REFLECT:
                    pos = pos % (RATIO_MAX * 2);
                    pos = pos < 0 ? pos + RATIO_MAX * 2 : pos;
                    pos = (pos < RATIO_MAX) ? pos : RATIO_MAX * 2 - pos;
                    break;
                case REPEAT:
                    pos = pos % RATIO_MAX;
                    pos = pos < 0 ? pos + RATIO_MAX : pos;
                    break;
                case NO_CYCLE:
                    pos = pos < 0 ? 0 : (pos > RATIO_MAX ? RATIO_MAX : pos);
                    break;
            }
            return _gradientColors[pos];
        }

        private const int RATIO_BITS = 10;
        private const int RATIO_MAX = (1 << RATIO_BITS) - 1;

        private readonly int[] _gradientColors = new int[1 << RATIO_BITS];
        private int[] _ratios = new int[64];
        private int _ratioCount;
        private readonly int _ffLength;
        private int _ffCurrpos;
        private int _ffDeltapos;
        protected PointFP _centerPt = new PointFP();
        protected RectangleFP _bounds = new RectangleFP();
    }
}
