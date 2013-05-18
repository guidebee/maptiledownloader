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
     * Encapsulates a 2D drawing surface.
     * be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GraphicsFP
    {

        /**
         * draw mode , XOR
         */
        public const int MODE_XOR = GraphicsPathRendererFP.MODE_XOR;

        /**
         * draw mode. nothing.
         */
        public const int MODE_ZERO = GraphicsPathRendererFP.MODE_ZERO;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default Constructor.
         */
        public GraphicsFP()
        {
            InitBlock();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor. create a graphics object with given width and height
         */
        public GraphicsFP(int width, int height)
        {
            InitBlock();
            Resize(width, height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the content of this image as ARGB array.
         * @return the ARGB array of the image content.
         */
        public int[] GetRGB()
        {
            return _renderer._buffer;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the brush object of the graphics.
         * @return the brush object.
         */
        public BrushFP GetBrush()
        {
            return _fillStyle;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set a new brush for this graphics object.
         * @param Value a new brush.
         */
        public void SetBrush(BrushFP value)
        {
            _fillStyle = value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the pen object for this graphics.
         * @return the pen object.
         */
        public PenFP GetPen()
        {
            return _lineStyle;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the new pen for this graphics object.
         * @param Value a new pen object.
         */
        public void SetPen(PenFP value)
        {
            _lineStyle = value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the paint mode.
         * @return the paint mode.
         */
        public int GetPaintMode()
        {
            return _paintMode;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the paint mode for this graphics.
         * @param Value
         */
        public void SetPaintMode(int value)
        {
            _paintMode = value;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the graphics drawing matrix.
         * @return the drawing matrix.
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
         * Set the graphics matrix.
         * @param Value the new matrix.
         */
        public void SetMatrix(MatrixFP value)
        {
            _matrix = value == null ? null : new MatrixFP(value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * resize the graphics object.
         * @param width the new width of the graphics.
         * @param height the new height of the graphics object.
         */
        public void Resize(int width, int height)
        {
            _renderer.Reset(width, height, width);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Clear the graphics content with given color.
         * @param color the clear color.
         */
        public void Clear(int color)
        {
            _renderer.Clear(color);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw a line.
         * @param ff_x1  the x coord of the first point of the line.
         * @param ff_y1  the y coord of the first point of the line.
         * @param ff_x2  the x coord of the second point of the line.
         * @param ff_y2  the y coord of the second point of the line.
         */
        public void DrawLine(int ffX1, int ffY1, int ffX2, int ffY2)
        {
            DrawPath(GraphicsPathFP.CreateLine(ffX1, ffY1, ffX2, ffY2));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a polyline.
         * @param points the coordinates  of the polyline.
         */
        public void DrawPolyline(PointFP[] points)
        {
            DrawPath(GraphicsPathFP.CreatePolyline(points));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw a polygon
         * @param points the coordinates  of the polygon.
         */
        public void DrawPolygon(PointFP[] points)
        {
            DrawPath(GraphicsPathFP.CreatePolygon(points));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw curves.
         * @param points
         * @param offset
         * @param numberOfSegments
         * @param ff_factor
         */
        public void DrawCurves(PointFP[] points, int offset, int numberOfSegments,
                int ffFactor)
        {
            DrawPath(GraphicsPathFP.CreateSmoothCurves(points,
                    offset, numberOfSegments, ffFactor, false));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *
         * @param points
         * @param offset
         * @param numberOfSegments
         * @param ff_factor
         */
        public void DrawClosedCurves(PointFP[] points, int offset,
                int numberOfSegments, int ffFactor)
        {
            DrawPath(GraphicsPathFP.CreateSmoothCurves(points, offset,
                    numberOfSegments, ffFactor, true));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a round rect
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_rx    the radius of the round circle.
         * @param ff_ry    the radius of the round circle.
         */
        public void DrawRoundRect(int ffXmin, int ffYmin, int ffXmax,
                int ffYmax, int ffRx, int ffRy)
        {
            DrawPath(GraphicsPathFP.CreateRoundRect(ffXmin, ffYmin, ffXmax,
                    ffYmax, ffRx, ffRy));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a rectangle.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         */
        public void DrawRect(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            DrawPath(GraphicsPathFP.CreateRect(ffXmin, ffYmin, ffXmax, ffYmax));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a oval.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         */
        public void DrawOval(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            DrawPath(GraphicsPathFP.CreateOval(ffXmin, ffYmin, ffXmax, ffYmax));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw an arc.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_startangle
         * @param ff_sweepangle
         */
        public void DrawArc(int ffXmin, int ffYmin, int ffXmax, int ffYmax,
                int ffStartangle, int ffSweepangle)
        {
            DrawPath(GraphicsPathFP.CreateArc(ffXmin, ffYmin, ffXmax, ffYmax,
                    ffStartangle, ffSweepangle, false));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a pie.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_startangle
         * @param ff_sweepangle
         */
        public void DrawPie(int ffXmin, int ffYmin, int ffXmax, int ffYmax,
                int ffStartangle, int ffSweepangle)
        {
            DrawPath(GraphicsPathFP.CreateArc(ffXmin, ffYmin, ffXmax, ffYmax,
                    ffStartangle, ffSweepangle, true));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a path.
         * @param path
         */
        public void DrawPath(GraphicsPathFP path)
        {
            if (_lineStyle.DashArray != null)
            {
                var newlineStyle = new PenFP(_lineStyle.Brush, _lineStyle.Width,
                        PenFP.LINECAP_BUTT, PenFP.LINECAP_BUTT, PenFP.LINEJOIN_MITER) 
                        {DashArray = _lineStyle.DashArray};

                var dasher = new GraphicsPathDasherFP(path,
                        newlineStyle.DashArray, 0);
                var newPath = dasher.GetDashedGraphicsPath();
                _renderer.DrawPath(newPath.CalcOutline(newlineStyle), _matrix,
                    _lineStyle.Brush, GraphicsPathRendererFP.MODE_ZERO);

            }
            else
            {

                _renderer.DrawPath(path.CalcOutline(_lineStyle), _matrix,
                    _lineStyle.Brush, GraphicsPathRendererFP.MODE_ZERO);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill  closed curves.
         * @param points
         * @param offset
         * @param numberOfSegments
         * @param ff_factor
         */
        public void FillClosedCurves(PointFP[] points, int offset,
                int numberOfSegments, int ffFactor)
        {
            FillPath(GraphicsPathFP.CreateSmoothCurves(points, offset,
                    numberOfSegments, ffFactor, true));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a polygon.
         * @param points
         */
        public void FillPolygon(PointFP[] points)
        {
            FillPath(GraphicsPathFP.CreatePolygon(points));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a round rectangle.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_rx
         * @param ff_ry
         */
        public void FillRoundRect(int ffXmin, int ffYmin, int ffXmax,
                int ffYmax, int ffRx, int ffRy)
        {
            FillPath(GraphicsPathFP.CreateRoundRect(ffXmin, ffYmin, ffXmax,
                    ffYmax, ffRx, ffRy));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a rectangle.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         */
        public void FillRect(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            var path = GraphicsPathFP.CreateRect(ffXmin, ffYmin,
                    ffXmax, ffYmax);
            path.AddClose();
            FillPath(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a oval.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         */
        public void FillOval(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            var path = GraphicsPathFP.CreateOval(ffXmin,
                    ffYmin, ffXmax, ffYmax);
            path.AddClose();
            FillPath(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a pie.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @param ff_startangle
         * @param ff_sweepangle
         */
        public void FillPie(int ffXmin, int ffYmin, int ffXmax, int ffYmax,
                int ffStartangle, int ffSweepangle)
        {
            FillPath(GraphicsPathFP.CreateArc(ffXmin, ffYmin, ffXmax, ffYmax,
                    ffStartangle, ffSweepangle, true));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * fill a path.
         * @param path
         */
        public void FillPath(GraphicsPathFP path)
        {
            _renderer.DrawPath(path, _matrix, _fillStyle, _paintMode);
        }

        public int GetClipHeight()
        {
            return _renderer._clipHeight;
        }

        public int GetClipWidth()
        {
            return _renderer._clipWidth;
        }

        public int GetClipX()
        {
            return _renderer._clipX;
        }

        public int GetClipY()
        {
            return _renderer._clipY;
        }
        public void SetClip(int x,
                        int y,
                        int width,
                        int height)
        {
            _renderer.SetClip(x, y, width, height);
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

            _renderer.FinalizeBuffer(color);
        }



        private PenFP _lineStyle;
        private BrushFP _fillStyle;
        private readonly GraphicsPathRendererFP _renderer = new GraphicsPathRendererFP();
        private int _paintMode;
        private MatrixFP _matrix;

        private void InitBlock()
        {

            _lineStyle = new PenFP(0x0, SingleFP.ONE);
            _fillStyle = new SolidBrushFP(0x0);
            _paintMode = MODE_XOR;
        }
    }

}
