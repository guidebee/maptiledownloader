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
using System;
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
     * This Graphics2D class provides more sophisticated control over geometry,
     * coordinate transformations, color management, and text layout. This is the
     * fundamental class for rendering 2-dimensional shapes, text and images
     * on the JavaME platform.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class Graphics2D
    {

        private readonly int _graphicsWidth;
        private readonly int _graphicsHeight;
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor. create a graphics object with given width and height
         * @param width the width of the graphics 2d object.
         * @param height the height of the graphics 2d object.
         */
        public Graphics2D(int width, int height)
        {
            _graphicsFP = new GraphicsFP(width, height);
            _defaultPen = new Pen(Color.Black);
            _defaultBrush = new SolidBrush(Color.White);
            _graphicsWidth = width;
            _graphicsHeight = height;
        }

        public int GetWidth()
        {
            return _graphicsWidth;
        }

        public int GetHeight()
        {
            return _graphicsHeight;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Strokes the outline of a IShape using the settings of the current
         * Graphics2D context.
         * @param pen the pen used to stroke the shape.
         * @param shape the IShape to be rendered.
         */
        public void Draw(Pen pen, IShape shape)
        {
            SetGraphicsFPPenAttribute(pen);
            PathIterator pathIterator = shape.GetPathIterator(null);
            int[] coords = new int[6];
            GraphicsPathFP graphicsPathFP = new GraphicsPathFP();
            PointFP pointFP1 = new PointFP();
            PointFP pointFPCtl1 = new PointFP();
            PointFP pointFPCtl2 = new PointFP();

            while (!pathIterator.IsDone())
            {
                int type = pathIterator.CurrentSegment(coords);
                switch (type)
                {
                    case PathIterator.SEG_MOVETO:
                        pointFP1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddMoveTo(pointFP1);
                        break;
                    case PathIterator.SEG_CLOSE:
                        graphicsPathFP.AddClose();
                        break;
                    case PathIterator.SEG_LINETO:
                        pointFP1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddLineTo(pointFP1);
                        break;
                    case PathIterator.SEG_QUADTO:
                        pointFPCtl1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        pointFP1.Reset(coords[2] << SingleFP.DECIMAL_BITS,
                                coords[3] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddQuadTo(pointFPCtl1, pointFP1);
                        break;
                    case PathIterator.SEG_CUBICTO:
                        pointFPCtl1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        pointFPCtl2.Reset(coords[2] << SingleFP.DECIMAL_BITS,
                                coords[3] << SingleFP.DECIMAL_BITS);
                        pointFP1.Reset(coords[4] << SingleFP.DECIMAL_BITS,
                                coords[5] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddCurveTo(pointFPCtl1, pointFPCtl2, pointFP1);
                        break;

                }
                pathIterator.Next();

            }
            _graphicsFP.DrawPath(graphicsPathFP);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Strokes the outline of a IShape using the settings of the current
         * Graphics2D context.
         * @param brush the brush used to fill the shape.
         * @param shape the IShape to be rendered.
         */
        public void Fill(Brush brush, IShape shape)
        {
            if (brush != null)
            {
                _graphicsFP.SetBrush(brush._wrappedBrushFP);
                _defaultBrush = brush;
            }

            PathIterator pathIterator = shape.GetPathIterator(null);
            int[] coords = new int[6];
            GraphicsPathFP graphicsPathFP = new GraphicsPathFP();
            PointFP pointFP1 = new PointFP();
            PointFP pointFPCtl1 = new PointFP();
            PointFP pointFPCtl2 = new PointFP();

            while (!pathIterator.IsDone())
            {
                int type = pathIterator.CurrentSegment(coords);
                switch (type)
                {
                    case PathIterator.SEG_MOVETO:
                        pointFP1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddMoveTo(pointFP1);
                        break;
                    case PathIterator.SEG_CLOSE:
                        graphicsPathFP.AddClose();
                        break;
                    case PathIterator.SEG_LINETO:
                        pointFP1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddLineTo(pointFP1);
                        break;
                    case PathIterator.SEG_QUADTO:
                        pointFPCtl1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        pointFP1.Reset(coords[2] << SingleFP.DECIMAL_BITS,
                                coords[3] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddQuadTo(pointFPCtl1, pointFP1);
                        break;
                    case PathIterator.SEG_CUBICTO:
                        pointFPCtl1.Reset(coords[0] << SingleFP.DECIMAL_BITS,
                                coords[1] << SingleFP.DECIMAL_BITS);
                        pointFPCtl2.Reset(coords[2] << SingleFP.DECIMAL_BITS,
                                coords[3] << SingleFP.DECIMAL_BITS);
                        pointFP1.Reset(coords[4] << SingleFP.DECIMAL_BITS,
                                coords[5] << SingleFP.DECIMAL_BITS);
                        graphicsPathFP.AddCurveTo(pointFPCtl1, pointFPCtl2, pointFP1);
                        break;

                }
                pathIterator.Next();

            }
            _graphicsFP.FillPath(graphicsPathFP);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws a line between the points (x1, y1) and (X2, y2).
         * @param pen pen used to draw the line
         * @param x1 the first point's x coordinate.
         * @param y1 the first point's y coordinate.
         * @param X2 the second point's x coordinate.
         * @param y2 the second point's y coordinate.
         */
        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            SetGraphicsFPPenAttribute(pen);
            _graphicsFP.DrawLine(x1 << SingleFP.DECIMAL_BITS, y1 << SingleFP.DECIMAL_BITS,
                    x2 << SingleFP.DECIMAL_BITS, y2 << SingleFP.DECIMAL_BITS);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws a line between the points pt1 and pt2.
         * @param pen pen used to draw the line
         * @param pt1 the first point.
         * @param pt2 the second point.
         */
        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the default pen of the graphics
         * @param pen default pen to be used by the graphcis if any draw method's
         *  pen set to null.
         */
        public void SetDefaultPen(Pen pen)
        {
            _defaultPen = pen;
            SetGraphicsFPPenAttribute(pen);

        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the default pen and brush together of the graphics
         * @param pen default pen to be used by the graphcis if any draw method's
         *  pen set to null.
         * @param brush default brush to be used by the graphics.
         */
        public void SetPenAndBrush(Pen pen, Brush brush)
        {
            SetDefaultPen(pen);
            if (brush != null)
            {
                _graphicsFP.SetBrush(brush._wrappedBrushFP);
                _defaultBrush = brush;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the default pen of the graphics
         * @return pen default pen used by the graphcis.
         */
        public Pen GetDefaultPen()
        {
            return _defaultPen;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the default brush of the graphics
         * @param brush default brush to be used by the graphcis if any fill method's
         *  brush set to null.
         */
        public void SetDefaultBrush(Brush brush)
        {
            if (brush != null)
            {
                _graphicsFP.SetBrush(brush._wrappedBrushFP);
                _defaultBrush = brush;
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the default brush of the graphics
         * @return pen default brush used by the graphcis.
         */
        public Brush GetDefaultBrush()
        {
            return _defaultBrush;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw a rectangle with given pen
         * @param pen pen used to draw the rectangle.
         * @param rectangle rectangle to be drawn.
         */
        public void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            Draw(pen, rectangle);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Fill a rectangle with given brush
         * @param brush brush used to fill the rectangle.
         * @param rectangle rectangle to be filled.
         */
        public void FillRectangle(Brush brush, Rectangle rectangle)
        {
            Fill(brush, rectangle);

        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws the outline of an oval. The result is a circle or ellipse that fits
         * within the rectangle specified by the x, y, width, and height arguments.
         * @param pen the pen used to draw the oval.
         * @param x the x coordinate of the upper left corner of the oval to be drawn.
         * @param y the y coordinate of the upper left corner of the oval to be drawn.
         * @param width the width of the oval to be drawn.
         * @param height the height of the oval to be drawn.
         */
        public void DrawOval(Pen pen, int x, int y, int width, int height)
        {
            SetGraphicsFPPenAttribute(pen);
            _graphicsFP.DrawOval(x << SingleFP.DECIMAL_BITS,
                    y << SingleFP.DECIMAL_BITS,
                    width << SingleFP.DECIMAL_BITS,
                    height << SingleFP.DECIMAL_BITS);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Fills an oval bounded by the specified rectangle with the current color.
         * @param brush the brush used to fill the oval.
         * @param x the x coordinate of the upper left corner of the oval to be filled.
         * @param y the y coordinate of the upper left corner of the oval to be filled.
         * @param width the width of the oval to be filled.
         * @param height the height of the oval to be filled.
         */
        public void FillOval(Brush brush, int x, int y, int width, int height)
        {
            SetDefaultBrush(brush);
            _graphicsFP.DrawOval(x << SingleFP.DECIMAL_BITS,
                    y << SingleFP.DECIMAL_BITS,
                    width << SingleFP.DECIMAL_BITS,
                    height << SingleFP.DECIMAL_BITS);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws a polyline
         * @param pen the pen used to draw the polyline.
         * @param polyline the polyline to be drawn.
         */
        public void DrawPolyline(Pen pen, Polyline polyline)
        {
            Draw(pen, polyline);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws a polygon
         * @param pen the pen used to draw the polygon.
         * @param polygon the polygon to be drawn.
         */
        public void DrawPolygon(Pen pen, Polygon polygon)
        {
            Draw(pen, polygon);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Fill a polygon.
         * @param brush the brush used to fill the polygon.
         * @param polygon the polygon to be filled.
         */
        public void FillPolygon(Brush brush, Polygon polygon)
        {
            Fill(brush, polygon);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the current transformation matrix from user space to device space.
         * @param matrix transformation matrix from user space to device space.
         */
        public void SetAffineTransform(AffineTransform matrix)
        {
            _graphicsFP.SetMatrix(Utils.ToMatrixFP(matrix));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the current transformation matrix from user space to device space.
         * @return  transformation matrix from user space to device space.
         */
        public AffineTransform GetAffineTransform()
        {
            return Utils.ToMatrix(_graphicsFP.GetMatrix());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return Returns the current clip of this graphcis object.
         * @return  the current clip  rectangle.
         */
        public Rectangle GetClip()
        {

            return new Rectangle(_graphicsFP.GetClipX(), _graphicsFP.GetClipY(),
                    _graphicsFP.GetClipWidth(),
                    _graphicsFP.GetClipHeight());
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set current clip of this graphcis object.
         * @param x  the x coordinate of the top left point.
         * @param y  the y coordinate of the top left point.
         * @param width the widht of the clip rectangle.
         * @param height the height of the clip rectangle.
         */
        public void SetClip(int x, int y, int width, int height)
        {
            _graphicsFP.SetClip(x, y, width, height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 26DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws the specified Image object at the specified location and with the
         * specified size.
         * @param imageRGB  Image object to draw.
         * @param dstX  x-coordinate of the upper-left corner of the drawn image.
         * @param dstY  y-coordinate of the upper-left corner of the drawn image.
         * @param width Width of the portion of the source image to draw.
         * @param height Height of the portion of the source image to draw.
         */
        public void DrawImage(int[] imageRGB, int width, int height,
                int dstX, int dstY)
        {

            Rectangle rect1 = GetClip();
            Rectangle rect2 = new Rectangle(dstX,
                    dstY,
                    width + dstX,
                    height + dstY);
            Rectangle rect = rect1.Intersection(rect2);
            if (!rect.IsEmpty())
            {
                width = rect.Width;
                height = rect.Height;

                int[] destBuffer = GetRGB();
                int desWidth = _graphicsWidth;
                int i;
                for (i = 0; i < height; i++)
                {
                    Array.Copy(imageRGB, i * width, destBuffer,
                            dstX + (i + dstY) * desWidth, width);
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 26DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draws the transparent Image object at the specified location and with the
         * specified size
         * @param imageRGB  Image object to draw.
         * @param dstX  x-coordinate of the upper-left corner of the drawn image.
         * @param dstY  y-coordinate of the upper-left corner of the drawn image.
         * @param width Width of the portion of the source image to draw.
         * @param height Height of the portion of the source image to draw.
         * @param transpency specify the transparent color of the image.
         */
        public void DrawImage(int[] imageRGB, int width, int height,
                int dstX, int dstY,
                int transpency)
        {

            Rectangle rect1 = GetClip();
            Rectangle rect2 = new Rectangle(dstX,
                    dstY,
                    width + dstX,
                    height + dstY);
            Rectangle rect = rect1.Intersection(rect2);
            if (!rect.IsEmpty())
            {
                int[] destBuffer = GetRGB();
                int desWidth = _graphicsWidth;
                int i;
                int j;
                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        if (((dstX + i) < _graphicsWidth) && ((dstY + j) < _graphicsHeight) &&
                                dstX + i >= 0 && dstY + j >= 0)
                        {
                            if ((imageRGB[i + j * width] & 0x00ffffff)
                                    != (transpency & 0x00ffffff))
                            {
                                destBuffer[dstX + i + (j + dstY) * desWidth]
                                        = imageRGB[i + j * width];
                            }
                        }
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set current clip of this graphcis object.
         * @param rectangle the new clip  rectangle.
         */
        public void SetClip(Rectangle rectangle)
        {
            SetClip(rectangle.X, rectangle.Y,
                    rectangle.Width,
                    rectangle.Height);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Clear the graphicis object with given color.
         * @param color the color used to clear the graphics.
         */
        public void Clear(Color color)
        {
            Clear(color._value);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Clear the graphics content with given color.
         * @param color the clear color.
         */
        public void Clear(int color)
        {
            _graphicsFP.Clear(color);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the content of this image as ARGB array.
         * @return the ARGB array of the image content.
         */
        public int[] GetRGB()
        {
            return _graphicsFP.GetRGB();
        }


        public int this[int x, int y]
        {
            get { return _graphicsFP.GetRGB()[y * _graphicsWidth + x]; }
        }
        /**
         * the wraped graphicsFP object.
         */
        private readonly GraphicsFP _graphicsFP;
        /**
         * default pen for drawing.
         */
        private Pen _defaultPen;
        /**
         * default brush for filling.
         */
        private Brush _defaultBrush;

        private void SetGraphicsFPPenAttribute(Pen pen)
        {
            if (pen != null)
            {
                _defaultPen = pen;
                PenFP penFP = _graphicsFP.GetPen();
                penFP.EndCap = pen._cap;
                penFP.StartCap = pen._cap;
                penFP.LineJoin = pen._join;
                penFP.Width = pen._width << SingleFP.DECIMAL_BITS;
                if (pen._brush != null)
                {
                    penFP.Brush = pen._brush._wrappedBrushFP;

                }
                else
                {
                    penFP.Brush = new SolidBrushFP(pen._color._value);
                }
                if (pen._dash != null)
                {
                    penFP.DashArray = new int[pen._dash.Length - pen._dashPhase];
                    for (int i = 0; i < pen._dash.Length - pen._dashPhase; i++)
                    {
                        penFP.DashArray[i] =
                                pen._dash[i - pen._dashPhase]
                                << SingleFP.DECIMAL_BITS;
                    }
                }
            }

        }
    }

}
