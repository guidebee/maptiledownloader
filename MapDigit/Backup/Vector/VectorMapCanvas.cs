//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 21JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.Collections;
using MapDigit.Drawing;
using MapDigit.Drawing.Geometry;
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Geometry;
using Color = MapDigit.Drawing.Color;
using Pen = MapDigit.Drawing.Pen;
using Rectangle = MapDigit.Drawing.Geometry.Rectangle;
using SolidBrush = MapDigit.Drawing.SolidBrush;
using TextureBrush = MapDigit.Drawing.TextureBrush;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  This class is used to draw vector map objects.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class VectorMapCanvas : VectorMapAbstractCanvas
    {

        /**
         * Shared graphics2D instance used to drawing map objects.
         */
        private static Graphics2D SharedGraphics2D;

        

        
        
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
                    SharedGraphics2D = new Graphics2D(MapLayer.MAP_TILE_WIDTH,
                            MapLayer.MAP_TILE_WIDTH);
                }
            }
            return;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construtor.
         */
        public VectorMapCanvas()
        {
            GetGraphics2DInstance();
            _textImage = MapLayer.GetAbstractGraphicsFactory()
                    .CreateImage(MapLayer.MAP_TILE_WIDTH,
                    IMAGE_PATERN_WIDTH);
            _textGraphics = _textImage.GetGraphics();
            _textGraphics.SetColor(0xC0C0FF);
            _textGraphics.FillRect(0, 0, _textImage.GetWidth(), _textImage.GetHeight());
            _fontTranspency = _textImage.GetRGB()[0];
            _imagePattern = MapLayer.GetAbstractGraphicsFactory()
                    .CreateImage(IMAGE_PATERN_WIDTH,
                    IMAGE_PATERN_WIDTH);

            _imagePatternGraphics = _imagePattern.GetGraphics();
            
            _mapSize.X = 0; _mapSize.Y = 0;
            _mapSize.MaxX = MapLayer.MAP_TILE_WIDTH;
            _mapSize.MaxY = MapLayer.MAP_TILE_WIDTH;
            _mapSize.Width = MapLayer.MAP_TILE_WIDTH;
            _mapSize.Height = MapLayer.MAP_TILE_WIDTH;
        }

        

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the RGB array of the map canvas.
         * @return the rgb array of the map cavas.
         */
        public override int[] GetRGB()
        {
            if (SharedGraphics2D != null)
            {
                return SharedGraphics2D.GetRGB();
            }
            return null;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a point.
         * @param mapPoint a map point object.
         */

        private void DrawPoint(MapPoint mapPoint)
        {
            GeoPoint screenPt = FromLatLngToMapPixel(mapPoint.Point);
            SolidBrush brush = new SolidBrush(new Color(mapPoint.SymbolType.Color,false));
            SharedGraphics2D.FillRectangle(brush, new Rectangle((int)screenPt.X - 2,
                    (int)screenPt.Y - 2, 4, 4));

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a pline.
         * @param mapPen the pen used to draw the polyline
         * @param pline the polyline object.
         */

        private void DrawPline(MapPen mapPen, GeoPolyline pline)
        {
            ArrayList clippedPts = _sutherlandHodgman.ClipPline(pline.GetPoints());
            GeoPoint[] screenPts = FromLatLngToMapPixel(clippedPts);
            if (screenPts.Length > 1)
            {
                {
                    int penWidth = mapPen.Width;
                    if (mapPen.Pattern > 62)
                    {
                        penWidth = mapPen.Width * 2;
                    }
                    Pen pen = new Pen(new Color(mapPen.Color,false), penWidth);
                    SharedGraphics2D.SetDefaultPen(pen);
                    int[] xpoints = new int[screenPts.Length];
                    int[] ypoints = new int[screenPts.Length];
                    for (int i = 0; i < screenPts.Length; i++)
                    {
                        xpoints[i] = (int)screenPts[i].X;
                        ypoints[i] = (int)screenPts[i].Y;

                    }
                    Polyline polyline = new Polyline
                                            {
                                                Xpoints = xpoints,
                                                Ypoints = ypoints,
                                                NumOfPoints = xpoints.Length
                                            };

                    SharedGraphics2D.DrawPolyline(null, polyline);
                }

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a region.
         * @param mapPen  pen for the border of the region.
         * @param mapBrush brush to fill the region.
         * @param region the polygon object.
         */

        private void DrawRegion(MapPen mapPen, MapBrush mapBrush, GeoPolygon region)
        {
            Pen pen = new Pen(new Color(mapPen.Color,false), mapPen.Width);
            TextureBrush brush = GetImagePatternBrush(mapBrush);
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
                    Polygon polygon = new Polygon
                                          {
                                              Xpoints = xpoints,
                                              Ypoints = ypoints,
                                              NumOfNpoints = xpoints.Length
                                          };

                    if (mapBrush.Pattern == 2)
                    {
                        SharedGraphics2D.SetPenAndBrush(pen, brush);
                        SharedGraphics2D.DrawPolygon(null, polygon);
                        SharedGraphics2D.FillPolygon(null, polygon);
                    }
                    else
                    {
                        SharedGraphics2D.SetDefaultPen(pen);
                        SharedGraphics2D.DrawPolygon(null, polygon);
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw a text.
         * @param mapText a map text object.
         */

        private void DrawText(MapText mapText)
        {
            {
                _fontColor = mapText.ForeColor;
                DrawString(_font, mapText.TextString,
                        (int)mapText.Point.X,
                        (int)mapText.Point.Y);
            }
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a texture brush based on region's brush attributes.
         * @param brush the map brush object.
         * @return the texture brush to used for the map brush.
         */

        private TextureBrush GetImagePatternBrush(MapBrush brush)
        {
            switch (brush.Pattern)
            {
                case 1:
                    break;
                case 2:
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    _imagePatternGraphics.FillRect( 0, 0, IMAGE_PATERN_WIDTH,
                            IMAGE_PATERN_WIDTH);
                    break;
                case 3:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect( 0, 0, IMAGE_PATERN_WIDTH,
                            IMAGE_PATERN_WIDTH);

                    for (int i = 0; i < 4; i++)
                    {
                        _imagePatternGraphics.DrawLine(0, i * 4, IMAGE_PATERN_WIDTH, i * 4);
                    }
                    break;
                case 4:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect( 0, 0, IMAGE_PATERN_WIDTH,
                            IMAGE_PATERN_WIDTH);
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    for (int i = 0; i < 4; i++)
                    {
                        _imagePatternGraphics.DrawLine(i * 4, 0, i * 4, IMAGE_PATERN_WIDTH);
                    }
                    break;
                case 5:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect( 0, 0, IMAGE_PATERN_WIDTH,
                           IMAGE_PATERN_WIDTH);
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    for (int i = 0; i < 8; i++)
                    {
                        _imagePatternGraphics.DrawLine(0, i * 4, i * 4, 0);
                    }
                    break;
                case 6:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect(0, 0, IMAGE_PATERN_WIDTH,
                           IMAGE_PATERN_WIDTH);
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    for (int i = 0; i < 8; i++)
                    {
                        _imagePatternGraphics.DrawLine(0, IMAGE_PATERN_WIDTH - i * 4, i * 4, 0);
                    }
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 48:
                case 49:
                case 50:
                case 51:
                case 53:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect(0, 0, IMAGE_PATERN_WIDTH,
                           IMAGE_PATERN_WIDTH);
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _imagePatternGraphics.FillRect(i * 4, j * 4, 1, 1);
                        }
                    }
                    break;
                default:
                    _imagePatternGraphics.SetColor(brush.BackColor);
                    _imagePatternGraphics.FillRect(0, 0, IMAGE_PATERN_WIDTH,
                           IMAGE_PATERN_WIDTH);
                    _imagePatternGraphics.SetColor(brush.ForeColor);
                    for (int i = 0; i < 4; i++)
                    {
                        _imagePatternGraphics.DrawLine(0, i * 4, IMAGE_PATERN_WIDTH, i * 4);
                        _imagePatternGraphics.DrawLine(i * 4, 0, i * 4, IMAGE_PATERN_WIDTH);
                    }


                    break;

            }

            int[] rgbData = _imagePattern.GetRGB();

            TextureBrush textureBrush = new TextureBrush(rgbData, IMAGE_PATERN_WIDTH, IMAGE_PATERN_WIDTH);


            return textureBrush;
        }



        

        /**
         * pattern Image;
         */
        private readonly IImage _imagePattern;
        /**
         * draw the image pattern.
         */
        private readonly IGraphics _imagePatternGraphics;
        /**
         * used to Show text on the image.
         */
        private readonly ArrayList _mapNameHolder = new ArrayList();


        /**
         * image used to draw char with system fonts.
         */
        private readonly IImage _textImage;

        /**
         * graphics used to draw char with system fonts.
         */
        private readonly IGraphics _textGraphics;


        private readonly int _fontTranspency;
        /**
         * defautl imagePattern size;
         */
        private const int IMAGE_PATERN_WIDTH = 24;

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
                        DrawPline(mapPline.PenStyle, mapPline.Pline);
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
            if (!mapObject.Name.ToLower().Equals("unknown") && pointFound)
            {
                MapText mapName = new MapText {Font = _font};
                mapName.SetForeColor(_fontColor);
                mapName.TextString = mapObject.Name;
                GeoPoint screenPt = FromLatLngToMapPixel(drawPt);
                mapName.Point.X = screenPt.X;
                mapName.Point.Y = screenPt.Y;
                mapName.Bounds.X = mapName.Point.X;
                mapName.Bounds.Y = mapName.Point.Y;
                if (_font != null)
                {
                    mapName.Bounds.Height = IMAGE_PATERN_WIDTH;
                    mapName.Bounds.Width = _font.CharsWidth(mapObject.Name.ToCharArray(), 0,
                            mapObject.Name.ToCharArray().Length);

                }
                AddMapName(mapName);

            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Clear the map canvas with given color.
         * @param color the color to fill the whole map canvas.
         */
        public override void ClearCanvas(int color)
        {
            if (SharedGraphics2D != null)
            {
                SharedGraphics2D.Clear(new Color(color));
                _mapNameHolder.Clear();
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw the map text.
         */
        public override void DrawMapText()
        {
            for (int i = 0; i < _mapNameHolder.Count; i++)
            {
                MapText mapText = (MapText)_mapNameHolder[i];
                if (_font != null)
                {
                    DrawText(mapText);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a map text to the drawing list.
         * @param mapText 
         */

        private void AddMapName(MapText mapText)
        {
            GeoLatLngBounds mapTextBounds = mapText.Bounds;
            for (int i = 0; i < _mapNameHolder.Count; i++)
            {
                GeoLatLngBounds storedMapTextBounds =
                        ((MapText)_mapNameHolder[i]).Bounds;
                if (storedMapTextBounds.Intersects(mapTextBounds))
                {
                    return;
                }
            }
            if (_mapSize.Contains(mapTextBounds))
            {
                _mapNameHolder.Add(mapText);
            }
        }




        /////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS -------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      -----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        /////////////////////////////////////////////////////////////////////////////
        /**
         * Draws the specified characters using the given font.
         * The offset and Length parameters must specify a valid range of characters
         * within the character array data. The offset parameter must be within the
         * range [0..(data.Length)], inclusive. The Length parameter must be a
         * non-negative integer such that (offset + Length) <= data.Length.
         * @param font the font object.
         * @param str the array of characters to be drawn.
         * @param X the X coordinate of the anchor point.
         * @param Y the Y coordinate of the anchor point.
         */

        private void DrawString(IFont font, string str, int x, int y)
        {
            lock (_textGraphics)
            {
                _textGraphics.SetColor(_fontTranspency);
                _textGraphics.FillRect(0, 0, _textImage.GetWidth(), _textImage.GetHeight());
                _textGraphics.SetFont(font);
                _textGraphics.SetColor(_fontColor);
                _textGraphics.DrawString(str, 0, 0);
                SharedGraphics2D.DrawImage(_textImage.GetRGB(), MapLayer.MAP_TILE_WIDTH,
                        IMAGE_PATERN_WIDTH, x, y, _fontTranspency);
            }
        }


    }

}
