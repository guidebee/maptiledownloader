using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using MapDigit.GIS.Geometry;
using Rectangle=System.Drawing.Rectangle;

namespace MapDigit.GIS.Vector
{
    public class VectorMapNativeCanvas : VectorMapAbstractCanvas
    {
        private static BufferedGraphics SharedGraphics2D;

        private static Bitmap RenderBitmap;

        private static Graphics BitmapGraphics;

        private readonly List<TextPosInfo> _textInfos = new List<TextPosInfo>();

        private static readonly int[] RGB_BUFFER = new int[MapLayer.MAP_TILE_WIDTH*MapLayer.MAP_TILE_WIDTH];

        private static readonly Font MAP_INFO_FONT = new Font("MapInfo Symbols", 8);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the graphics2D instance. the graphics 2D shall be protected by
         * graphic2DMutex to support multi-threading.
         * @return the shared the graphics2D objects.
         */

        private static void GetGraphics2DInstance()
        {
            lock (GRAPHICS_MUTEX)
            {
                if (SharedGraphics2D == null)
                {
                    // Retrieves the BufferedGraphicsContext for the 
                    // current application domain.
                    BufferedGraphicsContext  context = BufferedGraphicsManager.Current;

                    // Sets the maximum size for the primary graphics buffer
                    // of the buffered graphics context for the application
                    // domain.  Any allocation requests for a buffer larger 
                    // than this will create a temporary buffered graphics 
                    // context to host the graphics buffer.
                    context.MaximumBuffer = new Size(MapLayer.MAP_TILE_WIDTH + 1, MapLayer.MAP_TILE_WIDTH + 1);

                    // Allocates a graphics buffer the size of this form
                    // using the pixel format of the Graphics created by 
                    // the Form.CreateGraphics() method, which returns a 
                    // Graphics object that matches the pixel format of the form.

                    RenderBitmap = new Bitmap(MapLayer.MAP_TILE_WIDTH, MapLayer.MAP_TILE_WIDTH);
                    BitmapGraphics = Graphics.FromImage(RenderBitmap);
                    SharedGraphics2D = context.Allocate(BitmapGraphics,
                         new Rectangle(0, 0, MapLayer.MAP_TILE_WIDTH, MapLayer.MAP_TILE_WIDTH));

                    SharedGraphics2D.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    SharedGraphics2D.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    SharedGraphics2D.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    SharedGraphics2D.Graphics.PageUnit = GraphicsUnit.Pixel;
                    BitmapGraphics.SmoothingMode=  SmoothingMode.HighQuality;
                    BitmapGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    BitmapGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    BitmapGraphics.PageUnit = GraphicsUnit.Pixel;

                }
            }

        }

        public VectorMapNativeCanvas()
        {
            GetGraphics2DInstance();
            _mapSize.X = 0; _mapSize.Y = 0;
            _mapSize.MaxX = MapLayer.MAP_TILE_WIDTH;
            _mapSize.MaxY = MapLayer.MAP_TILE_WIDTH;
            _mapSize.Width = MapLayer.MAP_TILE_WIDTH;
            _mapSize.Height = MapLayer.MAP_TILE_WIDTH;
        }

        public override int[] GetRGB()
        {
            SharedGraphics2D.Render(BitmapGraphics);
            for (int i = 0; i < MapLayer.MAP_TILE_WIDTH; i++)
            {
                for (int j = 0; j < MapLayer.MAP_TILE_WIDTH; j++)
                {
                    RGB_BUFFER[i*MapLayer.MAP_TILE_WIDTH + j] =(int) (RenderBitmap.GetPixel(j, i).ToArgb() | 0xFF000000);
                }
            }
            return RGB_BUFFER;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a map object.
         * @param mapObject the map object to be drawing.
         * @param drawBoundary the drawing boundry.
         * @param zoomLevel the current zoomLevel.
         */
        public override void DrawMapObject(MapObject mapObject, GeoLatLngBounds drawBoundary,
                int zoomLevel)
        {
            GeoLatLng drawPt = new GeoLatLng();
            _sutherlandHodgman = new SutherlandHodgman(drawBoundary);
            _mapZoomLevel = zoomLevel;
            _mapCenterPt.X = drawBoundary.GetCenterX();
            _mapCenterPt.Y = drawBoundary.GetCenterY();
            bool pointFound = false;
            Point[] plinePoints=null;
            switch (mapObject.GetMapObjectType())
            {
                case MapObject.NONE:
                    break;
                case MapObject.POINT:
                    {
                        MapPoint mapPoint = (MapPoint)mapObject;
                        DrawPoint(mapPoint);
                        drawPt.X = mapPoint.Point.X;
                        drawPt.Y = mapPoint.Point.Y;
                        pointFound = true;
                    }
                    break;
                case MapObject.MULTIPOINT:
                    {
                        MapMultiPoint mapMultiPoint = (MapMultiPoint)mapObject;
                        for (int i = 0; i < mapMultiPoint.Points.Length; i++)
                        {
                            MapPoint mapPoint = new MapPoint
                            {
                                SymbolType = mapMultiPoint.SymbolType,
                                Point = new GeoLatLng(mapMultiPoint.Points[i])
                            };
                            DrawPoint(mapPoint);
                        }
                        for (int i = 0; i < mapMultiPoint.Points.Length; i++)
                        {
                            if (drawBoundary.Contains(mapMultiPoint.Points[i]))
                            {
                                drawPt.X = mapMultiPoint.Points[i].X;
                                drawPt.Y = mapMultiPoint.Points[i].Y;
                                pointFound = true;
                                break;
                            }
                        }

                    }
                    break;
                case MapObject.PLINE:
                    {
                        MapPline mapPline = (MapPline)mapObject;
                        plinePoints=DrawPline(mapPline.PenStyle, mapPline.Pline);
                        for (int i = 0; i < mapPline.Pline.GetVertexCount(); i++)
                        {
                            if (drawBoundary.Contains(mapPline.Pline.GetVertex(i)))
                            {
                                drawPt.X = mapPline.Pline.GetVertex(i).X;
                                drawPt.Y = mapPline.Pline.GetVertex(i).Y;
                                pointFound = true;
                                break;
                            }
                        }
                    }
                    break;
                case MapObject.MULTIPLINE:
                    {
                        MapMultiPline mapMultiPline = (MapMultiPline)mapObject;
                        for (int i = 0; i < mapMultiPline.Plines.Length; i++)
                        {
                            DrawPline(mapMultiPline.PenStyle,
                                    mapMultiPline.Plines[i]);
                            for (int j = 0; j < mapMultiPline.Plines[i].GetVertexCount(); j++)
                            {
                                if (drawBoundary.Contains(mapMultiPline.Plines[i].GetVertex(j)))
                                {
                                    drawPt.X = mapMultiPline.Plines[i].GetVertex(j).X;
                                    drawPt.Y = mapMultiPline.Plines[i].GetVertex(j).Y;
                                    pointFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                case MapObject.REGION:
                    {
                        MapRegion mapRegion = (MapRegion)mapObject;
                        DrawRegion(mapRegion.PenStyle, mapRegion.BrushStyle,
                                mapRegion.Region);
                        drawPt.X = mapRegion.CenterPt.X;
                        drawPt.Y = mapRegion.CenterPt.Y;
                        pointFound = true;
                    }
                    break;
                case MapObject.MULTIREGION:
                    {
                        MapMultiRegion mapMultiRegion = (MapMultiRegion)mapObject;
                        for (int i = 0; i < mapMultiRegion.Regions.Length; i++)
                        {
                            DrawRegion(mapMultiRegion.PenStyle,
                                    mapMultiRegion.BrushStyle,
                                    mapMultiRegion.Regions[i]);

                        }
                        drawPt.X = mapMultiRegion.CenterPt.X;
                        drawPt.Y = mapMultiRegion.CenterPt.Y;
                        pointFound = true;
                    }
                    break;
                case MapObject.COLLECTION:
                    {
                        MapCollection mapCollection = (MapCollection)mapObject;
                        if (mapCollection.MultiRegion != null)
                        {
                            MapMultiRegion mapMultiRegion = mapCollection.MultiRegion;
                            for (int i = 0; i < mapMultiRegion.Regions.Length; i++)
                            {
                                DrawRegion(mapMultiRegion.PenStyle,
                                        mapMultiRegion.BrushStyle,
                                        mapMultiRegion.Regions[i]);
                            }
                        }
                        if (mapCollection.MultiPline != null)
                        {
                            MapMultiPline mapMultiPline = mapCollection.MultiPline;
                            for (int i = 0; i < mapMultiPline.Plines.Length; i++)
                            {
                                DrawPline(mapMultiPline.PenStyle,
                                        mapMultiPline.Plines[i]);
                            }
                        }
                        if (mapCollection.MultiPoint != null)
                        {
                            MapMultiPoint mapMultiPoint = mapCollection.MultiPoint;
                            for (int i = 0; i < mapMultiPoint.Points.Length; i++)
                            {
                                MapPoint mapPoint = new MapPoint
                                {
                                    SymbolType = mapMultiPoint.SymbolType,
                                    Point = new GeoLatLng(mapMultiPoint.Points[i])
                                };
                                DrawPoint(mapPoint);
                            }
                        }
                        pointFound = true;
                        drawPt.X = mapCollection.Bounds.X + mapCollection.Bounds.Width / 2;
                        drawPt.Y = mapCollection.Bounds.Y + mapCollection.Bounds.Height / 2;

                    }
                    break;
                case MapObject.TEXT:
                    {
                        MapText mapText = (MapText)mapObject;
                        drawPt.X = mapText.Point.X;
                        drawPt.Y = mapText.Point.Y;
                        pointFound = true;
                    }
                    break;
            }
            if (!(mapObject.Name.ToLower().Equals("unknown") || mapObject.Name.Length==0) && pointFound)
            {
                MapText mapName = new MapText { Font = _font };
                mapName.SetForeColor(_fontColor);
                mapName.TextString = mapObject.Name;
                GeoPoint screenPt = FromLatLngToMapPixel(drawPt);
                mapName.Point.X = screenPt.X;
                mapName.Point.Y = screenPt.Y;
                mapName.Bounds.X = mapName.Point.X;
                mapName.Bounds.Y = mapName.Point.Y;
                Font font = null;
                if (_font != null)
                {
    
                    font = (Font)_font.GetNativeFont();
                    SizeF sizeF = SharedGraphics2D.Graphics.MeasureString(mapName.TextString, font);
                    mapName.Bounds.Height = sizeF.Height;
                    mapName.Bounds.Width = sizeF.Width;

                }
                TextPosInfo textPosInfo = new TextPosInfo();
                textPosInfo._mapText = mapName;
                if(mapObject.GetMapObjectType()==MapObject.PLINE)
                {
                    if(plinePoints!=null)
                    {
                        GraphicsPath graphicsPath = new GraphicsPath();
                        graphicsPath.AddLines(plinePoints);
                        RectangleF []rectangleF;
                        double angle = GetAngle(plinePoints[0], plinePoints[plinePoints.Length - 1]);
                        if (angle < 180)
                        {
                            graphicsPath.Reverse();
                        }
                        angle = GetAngle(plinePoints[0], plinePoints[plinePoints.Length - 1]);

                        float rotateAngle = GetRotateAngle(angle);
                        rectangleF = SharedGraphics2D.Graphics.MeasureString(mapName.TextString, font,
                                                                                new SolidBrush(
                                                                                    Color.FromArgb(_fontColor)),
                                                                                TextPathAlign.Center,
                                                                                TextPathPosition.CenterPath, 100, rotateAngle,
                                                                                graphicsPath);

                        textPosInfo._graphicsPath = graphicsPath;
                        textPosInfo._rectangles = rectangleF;

                       


                        if (rectangleF.Length == mapName.TextString.Length)
                        {
                           
                            //RectangleF[] rectangleFs = new RectangleF[rectangleF.Length];
                            //Array.Copy(rectangleF, rectangleFs, rectangleF.Length);
                            //for (int i = 0; i < rectangleFs.Length;i++ )
                            //{
                            //    for(int j=0;j<rectangleF.Length;j++)
                            //    {
                            //        if(i!=j)
                            //        {
                            //            if(rectangleFs[i].IntersectsWith(rectangleF[j]))
                            //            {
                            //                return;
                            //            }
                            //        }
                            //    }
                            //}


                           AddMapName(textPosInfo);
                        }


                    }
                }else
                {
                    textPosInfo._graphicsPath = null;
                    textPosInfo._rectangles = new[]{new RectangleF((float)textPosInfo._mapText.Bounds.X, (float)textPosInfo._mapText.Bounds.Y,
                                                           (float)textPosInfo._mapText.Bounds.Width,
                                                           (float)textPosInfo._mapText.Bounds.Height)};
                    AddMapName(textPosInfo);
                }
                
            }

        }

        private static float GetRotateAngle(double angle)
        {
            float rotateAngle = 0;
            if ((angle > 180 && angle <210) || (angle > 330 && angle <360))
            {
                rotateAngle = 0;
            }
            return rotateAngle;
        }

        private void AddMapName(TextPosInfo textPosInfo)
        {
            //check if there's collide
            foreach (var o in _textInfos)
            {
               
                
                foreach(var p in textPosInfo._rectangles)
                {
                    foreach(var q in o._rectangles)
                    {
                        if(p.IntersectsWith(q))
                        {
                            return;
                        }
                    }
                }
                
            }
           bool somethingInScreen = true;
           foreach(var p in textPosInfo._rectangles)
           {
               GeoBounds geoBounds = new GeoBounds(p.X, p.Y, p.Width, p.Height);
               if (!_mapSize.Contains(geoBounds))
               {
                   somethingInScreen = false;
                   break;
               }
           }

           if(somethingInScreen)
           {
               _textInfos.Add(textPosInfo);
           }
        }

        private static double GetAngle(PointF point1, PointF point2)
        {
            double c = Math.Sqrt(Math.Pow((point2.X - point1.X), 2) + Math.Pow((point2.Y - point1.Y), 2));
            double degree = 0;
            if (c == 0)
            {
                degree= 0;
            }
            else if (point1.X > point2.X)
            {
                //We must change the side where the triangle is
                degree = Math.Asin((point1.Y - point2.Y) / c) * 180 / Math.PI - 180 - 90;
            }
            else
            {
                degree = Math.Asin((point2.Y - point1.Y)/c)*180/Math.PI - 90;
            }

            return degree+360;
        }

        private void DrawRegion(MapPen mapPen, MapBrush mapBrush, GeoPolygon region)
        {
            Pen pen = new Pen(Color.FromArgb((int)(mapPen.Color | 0xFF000000)), mapPen.Width);
            Brush brush = GetBrush(mapBrush);
            GeoBounds bounds = new GeoBounds(118.808451, 31.907395, 0.003907, 0.0035);
            ArrayList clippedPts = _sutherlandHodgman.ClipRegion(region.GetPoints());
            GeoPoint[] screenPts = FromLatLngToMapPixel(clippedPts);

            if (screenPts.Length > 2)
            {
                {
                    int[] xpoints = new int[screenPts.Length];
                    int[] ypoints = new int[screenPts.Length];
                    for (int i = 0; i < screenPts.Length; i++)
                    {
                        xpoints[i] = (int)screenPts[i].X;
                        ypoints[i] = (int)screenPts[i].Y;

                    }

                    Point[] points = new Point[xpoints.Length];
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i] = new Point(xpoints[i], ypoints[i]);
                    }
                    SharedGraphics2D.Graphics.DrawPolygon(pen, points);
                    SharedGraphics2D.Graphics.FillPolygon(brush, points);
                }
            }
        }


        private static Pen GetPen(MapPen mapPen)
        {
            Bitmap patternBitmap = new Bitmap(4, 4);
            int penWidth = mapPen.Width;
            if (mapPen.Pattern > 62)
            {
                penWidth = mapPen.Width * 4;
            }


            Pen pen = new Pen(Color.FromArgb((int)(mapPen.Color | 0xFF000000)), penWidth);
            switch(mapPen.Pattern)
            {
                case 0:
                case 1:
                    break;
                case 63:
                case 64:
                    pen.CompoundArray = new float[] {0,0.25f,0.5f,0.75f};
                    break;
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                    pen.DashPattern = new float[] { 3f, 3f, 3f, 3f };
                    break;
                default:
                    break;
                    

                
            }
            return pen;
        }

        private static Brush GetBrush(MapBrush mapBrush)
        {
            int imageIndex = 0;
            switch (mapBrush.Pattern)
            {
                case 0:
                case 9:
                case 1: 
                case 10:
                case 11:
                    return new SolidBrush(Color.Empty);
                    break;
                case 2:
                    imageIndex = 1;
                    break;
                case 3:
                    imageIndex = 12;
                    break;
                case 4:
                    imageIndex = 16;
                    break;
                case 5:
                    imageIndex = 19;
                    break;
                case 6:
                    imageIndex = 24;
                    break;
                case 7:
                    imageIndex = 31;
                    break;
                case 8:
                    imageIndex = 34;
                    break;
                case 12:
                    imageIndex = 2;
                    break;
                case 13:
                    imageIndex = 3;
                    break;
                default:
                    imageIndex = mapBrush.Pattern - 10;
                    break;
         

            }
            string imgIndex = string.Empty;
            if(imageIndex<10)
            {
                imgIndex = "0" + imageIndex.ToString();
            }else
            {
                imgIndex = imageIndex.ToString();
            }
            string filename = @"C:\CVSMapDigit\MapTileDownloader\MapDigit\brushpatterns\fill" + imgIndex +
                              ".bmp";
            Bitmap bitmap = (Bitmap)Image.FromFile(filename);
            Color foreColor = Color.FromArgb((int) (mapBrush.ForeColor | 0xE0000000));
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            for(int i=0;i<bitmap.Width;i++)
            {
                for(int j=0;j<bitmap.Height;j++)
                {
                    Color getColor = bitmap.GetPixel(i, j);
                    if (getColor.R == 0 && getColor.G == 0 && getColor.B==0)
                   {
                       newBitmap.SetPixel(i, j, foreColor); 
                   }else
                   {
                       newBitmap.SetPixel(i, j, Color.Empty); 
                   }
                }
            }
            return new TextureBrush(newBitmap);

        }

        private Point[] DrawPline(MapPen mapPen, GeoPolyline pline)
        {
            ArrayList clippedPts = _sutherlandHodgman.ClipPline(pline.GetPoints());
            GeoPoint[] screenPts = FromLatLngToMapPixel(clippedPts);
            if (screenPts.Length > 1)
            {
                {

                    Pen pen = GetPen(mapPen);
                    int[] xpoints = new int[screenPts.Length];
                    int[] ypoints = new int[screenPts.Length];
                    for (int i = 0; i < screenPts.Length; i++)
                    {
                        xpoints[i] = (int)screenPts[i].X;
                        ypoints[i] = (int)screenPts[i].Y;

                    }

                    Point[] points = new Point[xpoints.Length];
                    for(int i=0;i<points.Length;i++)
                    {
                        points[i] = new Point(xpoints[i], ypoints[i]);
                    }
                    SharedGraphics2D.Graphics.DrawLines(pen, points);
                    return points;
                }

            }
            return null;
        }

        private void DrawPoint(MapPoint mapPoint)
        {
            GeoPoint screenPt = FromLatLngToMapPixel(mapPoint.Point);
            SolidBrush brush = new SolidBrush(Color.FromArgb((int)(mapPoint.SymbolType.Color | 0xFF000000)));
        
            SharedGraphics2D.Graphics.DrawString(((char)(mapPoint.SymbolType.Shape+1)).ToString(), MAP_INFO_FONT, brush,
                                                (float)screenPt.X - 3, (float)screenPt.Y - 3);
            //SolidBrush brush = new SolidBrush(Color.FromArgb((int)(mapPoint.SymbolType.Color | 0xFF000000)));
            //SharedGraphics2D.Graphics.FillRectangle(brush, new Rectangle((int)screenPt.X - 2,
            //        (int)screenPt.Y - 2, 4, 4));
        }

        

        public override void DrawMapText()
        {
            foreach(var textInfo in _textInfos)
            {
                MapText mapText = textInfo._mapText;
                if (_font != null)
                {
                   // Font font = (Font) _font.GetNativeFont();
                    Font font =(Font) mapText.Font.GetNativeFont();
                    SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)(textInfo._mapText.GetForeColor() | 0xFF000000)));
                    //SharedGraphics2D.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    //SharedGraphics2D.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    //SharedGraphics2D.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    
     
                    if (textInfo._graphicsPath == null)
                    {
                        SharedGraphics2D.Graphics.DrawString(mapText.TextString, font, solidBrush, (float)mapText.Bounds.X,
                                                             (float)mapText.Bounds.Y);
                    }else
                    {
                        

                        double angle = GetAngle(textInfo._graphicsPath.PathPoints[0], textInfo._graphicsPath.PathPoints[textInfo._graphicsPath.PathPoints.Length - 1]);
                        float rotateAngle = GetRotateAngle(angle);
                        SharedGraphics2D.Graphics.DrawString(textInfo._mapText.TextString, font, Color.FromArgb((int)(textInfo._mapText.GetForeColor() | 0xFF000000)),
                                                                                new SolidBrush(
                                                                                    Color.FromArgb((int)(textInfo._mapText.GetForeColor() | 0xFF000000))),
                                                                                TextPathAlign.Center,
                                                                                TextPathPosition.CenterPath, 100, rotateAngle,
                                                                                textInfo._graphicsPath);
                    }
                }
            }
        }

        public override void ClearCanvas(int color)
        {
            if (SharedGraphics2D != null)
            {
                SharedGraphics2D.Graphics.Clear(Color.FromArgb((int) (color | 0xFF000000)));
                 _textInfos.Clear();
            }
        }
    }


    internal class TextPosInfo
    {
        internal MapText _mapText;
        internal RectangleF[] _rectangles;
        internal GraphicsPath _graphicsPath;

    }
}
