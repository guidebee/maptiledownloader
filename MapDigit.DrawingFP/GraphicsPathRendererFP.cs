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
     * This class actually renders path in memory.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    internal class GraphicsPathRendererFP : GraphicsPathSketchFP
    {

        /**
         * paint mode XOR
         */
        public const int MODE_XOR = 1;
        /**
         * paint mode ZERO.(copy)
         */
        public const int MODE_ZERO = 2;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public GraphicsPathRendererFP()
            : this(1, 1)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor
         * @param _width the _width for the drawing canvas
         * @param _height the _height for the drawing cavas.
         */
        public GraphicsPathRendererFP(int width, int height)
        {
            _ffXmin = SingleFP.MAX_VALUE;
            _ffXmax = SingleFP.MIN_VALUE;
            _ffYmin = SingleFP.MAX_VALUE;
            _ffYmax = SingleFP.MIN_VALUE;
            Reset(width, height, width);
            Scanbuf = new int[4096];
            ScanbufTmp = new int[4096];

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the _width of the graphics object.
         * @return
         */
        public int GetWidth()
        {
            return _width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the _height of the graphics.
         * @return
         */
        public int GetHeight()
        {
            return _height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param path
         * @param style
         * @param mode
         */
        public void DrawPath(GraphicsPathFP path, BrushFP style, int mode)
        {
            _scanIndex = 0;
            _drawMode = mode;
            path.Visit(this);
            RadixSort(Scanbuf, ScanbufTmp, _scanIndex);
            _fillStyle = style;
            if (_transformMatrix != null)
            {
                _fillStyle.SetGraphicsMatrix(_transformMatrix);
            }
            DrawBuffer();
            _fillStyle = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param path
         * @param matrix
         * @param fillStyle
         * @param mode
         */
        public void DrawPath(GraphicsPathFP path, MatrixFP matrix,
                BrushFP fs, int mode)
        {
            _transformMatrix = matrix;
            DrawPath(path, fs, mode);
            _transformMatrix = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param _width
         * @param _height
         * @param _scanline
         */
        public void Reset(int width, int height, int scanline)
        {
            _buffer = new int[width * height];
            _width = width;
            _height = height;
            SetClip(0, 0, width, height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param color
         */
        public void Clear(int color)
        {
            _backGroundColor = color;
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer[i] = color;
            }
            SetClip(0, 0, _width, _height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param color
         */
        public void FinalizeBuffer(int color)
        {
            _backGroundColor = color;
            var bk = ColorFP.FromArgb(color);
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (ClipContains(x, y))
                    {
                        var c = ColorFP.FromArgb(_buffer[x + y * _width]);
                        if (c.Alpha != 0x00)
                        {
                            if (c.Alpha != 0xFF)
                            {

                                _buffer[x + y * _width] = ColorFP.FromArgb(
                                        (c.Red * c.Alpha +
                                        (0xFF - c.Alpha) * bk.Red) >> 8,
                                        (c.Green * c.Alpha
                                        + (0xFF - c.Alpha) * bk.Green) >> 8,
                                        (c.Blue * c.Alpha
                                        + (0xFF - c.Alpha) * bk.Blue) >> 8).Value;
                            }
                        }
                        else
                        {
                            _buffer[x + y * _width] = color;
                        }
                    }
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
         *
         * @param point
         */
        public override void MoveTo(PointFP point)
        {
            _transformedPoint = new PointFP(point);
            if (_transformMatrix != null)
            {
                _transformedPoint.Transform(_transformMatrix);
            }
            base.MoveTo(point);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 09NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param point
         */
        public override void LineTo(PointFP point)
        {
            var pntTemp = new PointFP(point);

            _ffXmin = MathFP.Min(_ffXmin, CurrentPoint().X);
            _ffXmax = MathFP.Max(_ffXmax, point.X);
            _ffYmin = MathFP.Min(_ffYmin, CurrentPoint().Y);
            _ffYmax = MathFP.Max(_ffYmax, point.Y);

            if (_transformMatrix != null)
            {
                pntTemp.Transform(_transformMatrix);
            }

            Scanline(_transformedPoint.X, _transformedPoint.Y, pntTemp.X, pntTemp.Y);
            _transformedPoint = pntTemp;
            base.LineTo(point);
        }

        public void SetClip(int x,
                        int y,
                        int width,
                        int height)
        {
            _clipX = x;
            _clipY = y;
            _clipHeight = height;
            _clipWidth = width;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see if this rectangle contains given point.
         * @param p
         * @return
         */
        private bool ClipContains(int x, int y)
        {
            return _clipX <= x && x <= _clipX + _clipWidth
                    && _clipY <= y && y <= _clipY + _clipHeight;
        }



        internal int[] _buffer;
        internal int _backGroundColor = 0x00FFFFFF;
        private const int RENDERER_FRAC_Y = 4;
        private const int RENDERER_FRAC_X = 4;
        private const int RENDERER_REAL_X = 12;
        private const int RENDERER_REAL_Y = 11;
        private const int BUFFERSIZE = 2048;
        private const int RENDERER_REAL_X_MASK = (1 << RENDERER_REAL_X) - 1;
        private const int RENDERER_REAL_Y_MASK = (1 << RENDERER_REAL_Y) - 1;
        private const int RENDERER_FRAC_X_FACTOR = 1 << RENDERER_FRAC_X;
        private const int RENDERER_FRAC_X_MASK = (1 << RENDERER_FRAC_X) - 1;
        private MatrixFP _transformMatrix;
        private BrushFP _fillStyle;
        private static int[] Scanbuf;
        private static int[] ScanbufTmp;
        private static readonly int[] Counts = new int[256];
        private static readonly int[] Index = new int[256];
        private PointFP _transformedPoint;
        private int _width;
        private int _height;
        private int _drawMode = MODE_XOR;
        private int _scanIndex;
        private int _ffXmin;
        private int _ffXmax;
        private int _ffYmin;
        private int _ffYmax;

        internal int _clipX;
        internal int _clipY;
        internal int _clipWidth;
        internal int _clipHeight;

        private static void RadixSort(int[] dataSrc, int[] dataTmp, int num)
        {
            int shift, i;
            var src = dataSrc;
            var dst = dataTmp;
            for (shift = 0; shift <= 24; shift += 8)
            {
                for (i = 0; i < 256; i++)
                {
                    Counts[i] = 0;
                }

                for (i = 0; i < num; i++)
                {
                    Counts[(src[i] >> shift) & 0xFF]++;
                }
                int indexnow = 0;
                for (i = 0; i < 256; i++)
                {
                    Index[i] = indexnow;
                    indexnow += Counts[i];
                }
                for (i = 0; i < num; i++)
                {
                    dst[Index[(src[i] >> shift) & 0xFF]++] = src[i];
                }
                var tmp = src;
                src = dst;
                dst = tmp;
            }
        }

        private void DrawBuffer()
        {
            var curd = 0;
            var cure = 0;
            var cura = 0;
            var cula = 0;
            var cury = 0;
            var curx = 0;
            var count = _scanIndex;
            for (int c = 0; c <= count; c++)
            {
                var curs = c == count ? 0 : Scanbuf[c];

                var newy = ((curs >> (RENDERER_REAL_X + RENDERER_FRAC_X + 1))
                        & RENDERER_REAL_Y_MASK);
                var newx = ((curs >> (RENDERER_FRAC_X + 1)) & RENDERER_REAL_X_MASK);
                if ((newx != curx) || (newy != cury))
                {
                    var alp = (256 * cure) / (RENDERER_FRAC_Y) +
                            (256 * cula) / (RENDERER_FRAC_Y
                            * (RENDERER_FRAC_X_FACTOR - 1)) +
                            (256 * cura) / (RENDERER_FRAC_Y
                            * (RENDERER_FRAC_X_FACTOR - 1));
                    if (alp != 0)
                    {
                        if (_drawMode == MODE_XOR)
                        {
                            alp = (alp & 0x100) != 0
                                    ? (0xFF - (alp & 0xFF)) : (alp & 0xFF);
                        }
                        else
                        {
                            alp = MathFP.Min(255, MathFP.Abs(alp));
                        }
                        if (alp != 0)
                        {
                            MergePixels(curx, cury, 1, alp);
                        }
                    }
                    cure = curd;

                    if (newy == cury)
                    {
                        if (curd != 0)
                        {
                            alp = (256 * curd) / RENDERER_FRAC_Y;
                            if (alp != 0)
                            {
                                if (_drawMode == MODE_XOR)
                                {
                                    alp = (alp & 0x100) != 0
                                            ? (0xFF - (alp & 0xFF)) : (alp & 0xFF);
                                }
                                else
                                {
                                    alp = MathFP.Min(255, MathFP.Abs(alp));
                                }
                                if (alp != 0)
                                {
                                    MergePixels(curx + 1, cury, newx - curx - 1, alp);
                                }
                            }
                        }
                    }
                    else
                    {
                        cury = newy;
                        curd = cure = 0;
                    }

                    curx = newx;
                    cura = cula = 0;
                }

                if ((curs & 1) != 0)
                {
                    curd++;
                    cula += ((~(curs >> 1)) & RENDERER_FRAC_X_MASK);
                }
                else
                {
                    curd--;
                    cura -= ((~(curs >> 1)) & RENDERER_FRAC_X_MASK);
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
         *
         * @param ff_sx
         * @param ff_sy
         * @param ff_ex
         * @param ff_ey
         */
        private void Scanline(int ffSx, int ffSy, int ffEx, int ffEy)
        {
            var sx = ffSx >> (SingleFP.DECIMAL_BITS - RENDERER_FRAC_X);
            var ex = ffEx >> (SingleFP.DECIMAL_BITS - RENDERER_FRAC_X);
            var sy = (ffSy * RENDERER_FRAC_Y) >> SingleFP.DECIMAL_BITS;
            var ey = (ffEy * RENDERER_FRAC_Y) >> SingleFP.DECIMAL_BITS;
            var xmin = MathFP.Min(sx, ex);
            var xmax = MathFP.Max(sx, ex);
            var ymin = MathFP.Min(sy, ey);
            var ymax = MathFP.Max(sy, ey);
            var incx = ffSx < ffEx && ffSy < ffEy || ffSx >= ffEx
                    && ffSy >= ffEy ? 1 : -1;
            var x = incx == 1 ? xmin : xmax;
            var dire = ffSy < ffEy ? 1 : 0;

            if (((ymin < 0) && (ymax < 0)) || ((ymin >= (_height * RENDERER_FRAC_Y))
                    && (ymax >= (_height * RENDERER_FRAC_Y))))
            {
                return;
            }

            var n = MathFP.Abs(xmax - xmin);
            var d = MathFP.Abs(ymax - ymin);
            var i = d;

            ymax = MathFP.Min(ymax, _height * RENDERER_FRAC_Y);

            for (var y = ymin; y < ymax; y++)
            {
                if (y >= 0)
                {
                    if (_scanIndex >= Scanbuf.Length)
                    {
                        var bufSize = _scanIndex / BUFFERSIZE;
                        if ((_scanIndex + 1) % BUFFERSIZE != 0)
                        {
                            bufSize += 1;
                        }
                        ScanbufTmp = new int[bufSize * BUFFERSIZE];
                        Array.Copy(Scanbuf, 0, ScanbufTmp, 0, _scanIndex);
                        Scanbuf = new int[bufSize * BUFFERSIZE];
                        Array.Copy(ScanbufTmp, 0, Scanbuf, 0, _scanIndex);

                    }
                    Scanbuf[_scanIndex++] = ((y / RENDERER_FRAC_Y)
                            << (RENDERER_REAL_X + RENDERER_FRAC_X + 1))
                            | (MathFP.Max(0, MathFP.Min((_width *
                            RENDERER_FRAC_X_FACTOR) - 1, x)) << 1) | dire;

                }
                i += n;
                if (i > d)
                {
                    var idivd = (i - 1) / d;
                    x += incx * idivd;
                    i -= d * idivd;
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
         *
         * @param x
         * @param y
         * @param count
         * @param opacity
         */
        private void MergePixels(int x, int y, int count, int opacity)
        {
            var isMonoColor = _fillStyle.IsMonoColor();
            var color = 0;
            if (isMonoColor)
            {
                color = _fillStyle.GetNextColor();
                color = ((((color >> 24) & 0xFF) * opacity) >> 8)
                        << 24 | color & 0xFFFFFF;
            }
            var lastBackColor = 0;
            var lastMergedColor = 0;
            for (var i = 0; i < count; i++)
            {
                if (ClipContains(x + i, y))
                {
                    var bkColor = _buffer[x + i + y * _width];
                    if (!isMonoColor)
                    {
                        color = i == 0 ? _fillStyle.GetColorAt(x + i, y, count == 1)
                                : _fillStyle.GetNextColor();
                        if (opacity != 0xFF)
                        {
                            color = ((((color >> 24) & 0xFF) * opacity) >> 8)
                                    << 24 | color & 0xFFFFFF;
                        }
                    }
                    if (lastBackColor == bkColor && isMonoColor)
                    {
                        _buffer[x + i + y * _width] = lastMergedColor;

                    }
                    else
                    {
                        _buffer[x + i + y * _width] = Merge(bkColor, color);
                        lastBackColor = bkColor;
                        lastMergedColor = _buffer[x + i + y * _width];
                    }
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
         *
         * @param color1
         * @param color2
         * @return
         */
        private static int Merge(int color1, int color2)
        {
            int a2 = (color2 >> 24) & 0xFF;
            if (a2 == 0xFF || color1 == 0x0)
            {
                return color2;
            }
            if (a2 == 0)
            {
                return color1;
            }
            var a1 = 0xFF - ((color1 >> 24) & 0xFF);
            var a3 = 0xFF - a2;
            var b1 = color1 & 0xFF;
            var g1 = (color1 >> 8) & 0xFF;
            var r1 = (color1 >> 16) & 0xFF;
            var b2 = color2 & 0xFF;
            var g2 = (color2 >> 8) & 0xFF;
            var r2 = (color2 >> 16) & 0xFF;

            var Ca = (0xFF * 0xFF - a1 * a3) >> 8;
            var Cr = (r1 * a3 + r2 * a2) >> 8;
            var Cg = (g1 * a3 + g2 * a2) >> 8;
            var Cb = (b1 * a3 + b2 * a2) >> 8;
            return Ca << 24 | Cr << 16 | Cg << 8 | Cb;
        }
    }

}
