//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 18JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using System.Collections;
using MapDigit.AJAX;
using MapDigit.Drawing.Geometry;
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Geometry;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 19JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * RasterMap a map class uses to display raster map.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 19/06/09
     * @author      Guidebee, Inc.
     */
    public class RasterMap : DigitalMap
    {

        public GeoBounds SelectedMapTileArea = new GeoBounds(0, 0, 0, 0);


        public void SetSelectedArea(int x,int y,int width,int height)
        {
            GeoLatLng latLng1 = FromScreenPixelToLatLng(new GeoPoint(x, y));
            GeoLatLng latLng2 = FromScreenPixelToLatLng(new GeoPoint(x+width, y+height));
            GeoPoint index1 = ConvertCoordindates2Tiles(latLng1.Lat(), latLng1.Lng(), GetZoom());
            GeoPoint index2 = ConvertCoordindates2Tiles(latLng2.Lat(), latLng2.Lng(), GetZoom());
            GeoBounds bounds = new GeoBounds(index1.X, index1.Y, Math.Abs(index2.X - index1.X)+1,
                                                Math.Abs(index2.Y - index1.Y)+1);
            if (!(bounds.X == SelectedMapTileArea.X &&
                bounds.Y == SelectedMapTileArea.Y &&
                bounds.Width == SelectedMapTileArea.Width &&
                bounds.Height == SelectedMapTileArea.Height))
            {
                SelectedMapTileArea = bounds;
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get total downloaded bytes, (approx)
         * @return the total byte has be downloaded
         */
        public static long GetTotalDownloadedBytes()
        {
            return Request.TotaldownloadedBytes +
                    MapTileAbstractReader.TotaldownloadedBytes;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new ServerMap with given Width and Height.
         * @param Width the Width of the map image.
         * @param Height the Height of the map image.
         * @param mapTileDownloadManager map tile download manager.
         * @throws InvalidLicenceException invalid licence.
         */
        public RasterMap(int width, int height,
                MapTileDownloadManager mapTileDownloadManager)
            : this(width, height, MapType.MICROSOFTMAP, mapTileDownloadManager)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new ServerMap with given Width and Height.
         * @param Width the Width of the map image.
         * @param Height the Height of the map image.
         * @param mapType map type.
         * @param mapTileDownloadManager map tile download manager.
         * @throws InvalidLicenceException invalid licence.
         */
        public RasterMap(int width, int height, int mapType,
                MapTileDownloadManager mapTileDownloadManager)
            : base(width, height)
        {

            this._mapType = mapType;//MapType.MAPABCCHINA;
            _mapTileReadyListener = new MapTileReadyListener(this);

            if (mapTileDownloadManager != null)
            {
                _mapTileDownloadManager = mapTileDownloadManager;
                _mapTileDownloadManager._mapTileReadyListener = _mapTileReadyListener;
            }

            //the following Rect(s) are all temporary variables, initialze here
            //to avoid new /gc so ,these temp vairables are heavily used.
            _centerRect = new GeoBounds(0, 0, 0, 0);
            _drawRect = new GeoBounds(0, 0, 0, 0);
            _mapRects = new GeoBounds[16];
            _newMapRects = new GeoBounds[16];
            for (int i = 0; i < 16; i++)
            {
                _mapRects[i] = new GeoBounds(0, 0, 0, 0);
                _newMapRects[i] = new GeoBounds(0, 0, 0, 0);
            }
            _mapRect = new GeoBounds(0, 0, 0, 0);
            _newCenterRect = new GeoBounds(0, 0, 0, 0);
            _mapRectangle = new Rectangle(0, 0, width, height);
            _screenOffsetX = (GetMapWidth() - _screenSize.Width) / 2;
            _screenOffsetY = (GetMapHeight() - _screenSize.Height) / 2;
            _screenRectangle = new Rectangle(0, 0, _screenSize.Width,
                    _screenSize.Height);
           
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for map drawing .
         * @param mapDrawingListener callback when mapdrawing is done.

         */
        public void SetMapDrawingListener(IMapDrawingListener mapDrawingListener)
        {
            _mapDrawingListener = mapDrawingListener;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void SetScreenSize(int width, int height)
        {
            lock (_syncObject)
            {
                base.SetScreenSize(width, height);
                _screenSize.Height = height;
                _screenSize.Width = width;
                _screenOffsetX = (GetMapWidth() - _screenSize.Width) / 2;
                _screenOffsetY = (GetMapHeight() - _screenSize.Height) / 2;
                _screenRectangle = new Rectangle(0, 0, _screenSize.Width,
                        _screenSize.Height);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the map view to the given center.
         * @param center the center latitude,longitude of the map.
         * @param zoomLevel the zoom Level of the map [0,17].
         * @param mapType msn map, yahoo map etc.
         */
        public void SetCenter(GeoLatLng center, int zoomLevel, int mapType)
        {
            lock (_syncObject)
            {
                this._mapType = mapType;
                base.SetCenter(center, zoomLevel);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * is the point in current screen (is shown or not).
         * @param pt point to be tested.
         * @return true is in screen range.
         */
        public override bool IsPointVisible(GeoLatLng pt)
        {
            lock (_syncObject)
            {
                GeoPoint screenPt = FromLatLngToMapPixel(pt);
                _screenRectangle.SetX(_screenOffsetX);
                _screenRectangle.SetY(_screenOffsetY);
                return _screenRectangle.Contains((int)screenPt.X, (int)screenPt.Y);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the latitude,longitude of the screen center.
         * @return the center of the screen in latitude,longititude pair.
         */
        public GeoLatLng GetScreenCenter()
        {
            lock (_syncObject)
            {
                GeoPoint pt = new GeoPoint(_screenOffsetX + _screenSize.Width / 2,
                        _screenOffsetY + _screenSize.Height / 2);
                return FromMapPixelToLatLng(pt);
            }
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Starts a pan with given Distance in pixels.
         * directions. +1 is right and down, -1 is left and up, respectively.
         * @param dx X offset.
         * @param dy Y offset.
         */
        public override void PanDirection(int dx, int dy)
        {
            lock (_syncObject)
            {
                _screenOffsetX -= dx;
                _screenOffsetY -= dy;
                //needToGetNewMapImage checks if the mapRect Contains the screen rect
                //or not, it still Contains the whole screen rect, it doesn't
                //need to redraw the whole map canvas.
                if (NeedToGetNewMapImage())
                {
                    SetCenter(GetScreenCenter(), _mapZoomLevel, _mapType);
                }
                else
                {
                    _newCenter = GetScreenCenter();
                    //isMapImageStale check if the map rect has some image need to
                    //update.
                    if (IsMapImageStale(_newCenter))
                    {
                        DrawUpdatedMapCanvas();
                    }
                }
                lock (_mapLayers)
                {
                    for (int i = 0; i < _mapLayers.Count; i++)
                    {
                        MapLayer mapLayer = (MapLayer)_mapLayers[i];
                        mapLayer.PanDirection(dx, dy);
                    }
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets current map type
         * @param mapType new map type.
         */
        public void SetMapType(int mapType)
        {
            lock (_syncObject)
            {
                this._mapType = mapType;
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the map type.
         * @return the map type.
         */
        public virtual int GetMapType()
        {
            lock (_syncObject)
            {
                return _mapType;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Stores the current map position and zoom level for later
         * recall by returnToSavedPosition().
         */
        public void SavePosition()
        {
            lock (_syncObject)
            {
                _storedZoomLevel = _mapZoomLevel;
                _storedPosition = new GeoLatLng(_mapCenterPt);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Restores the map view that was saved by savePosition().
         */
        public void ReturnToSavedPosition()
        {
            lock (_syncObject)
            {
                if (_storedPosition != null)
                {
                    _mapZoomLevel = _storedZoomLevel;
                    PanTo(_storedPosition);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Restore map image cache from persistent memory.
         */
        public void RestoreMapCache()
        {
            lock (_syncObject)
            {
                _mapTileDownloadManager.RestoreMapCache();
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Save map image cache to persistent memory.
         */
        public void SaveMapCache()
        {
            lock (_syncObject)
            {
                _mapTileDownloadManager.SaveMapCache();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void Paint(IGraphics graphics)
        {
            graphics.DrawImage(_mapImage,
                           -_screenOffsetX,
                           -_screenOffsetY);
            graphics.DrawString("Zoom:"+ GetZoom(),16,GetMapHeight()-32);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void Paint(IGraphics graphics, int offsetX, int offsetY)
        {
            graphics.DrawImage(_mapImage,
                           offsetX,
                           offsetY);

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert laititude,longitude pair to the coordinates on screen.
         * @param latlng the latitude,longitude location.
         * @return X,Y coordniate on screen.
         */
        public GeoPoint FromLatLngToScreenPixel(GeoLatLng latlng)
        {
            lock (_syncObject)
            {
                GeoPoint pt = FromLatLngToMapPixel(latlng);
                pt.X -= _screenOffsetX;
                pt.Y -= _screenOffsetY;
                return pt;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert the coordinates on screen to laititude,longitude pair .
         * @param pt (X,Y) coordinates on screen
         * @return the latitude,longitude position.
         */
        public GeoLatLng FromScreenPixelToLatLng(GeoPoint pt)
        {
            lock (_syncObject)
            {
                GeoPoint pt1 = new GeoPoint(pt);
                pt1.X += _screenOffsetX;
                pt1.Y += _screenOffsetY;
                return FromMapPixelToLatLng(pt1);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set routing direction.
         * @param newDirection new routing direction.
         */
        public void SetMapDirection(MapDirection newDirection)
        {
            lock (_syncObject)
            {
                _mapTileDownloadManager.SetMapDirection(newDirection);
                if (newDirection != null)
                {
                }
                else
                {
                    ClearMapDirection();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Clear map (routes etc).
         */
        public void ClearMapDirection()
        {
            lock (_syncObject)
            {
                _mapTileDownloadManager.SetMapDirection(null);
                SetCenter(GetScreenCenter(), GetZoom());

            }
        }

        /**
         * record store
         */
        //protected static RecordStore mapDataRecordStore = null;
        /**
         * mutex.
         */
        //protected final Object drawMapCanvasMutex = new Object();
        /**
         * Map drawing  listener.
         */
        protected IMapDrawingListener _mapDrawingListener;
        /**
         * Map tiles downloader
         */
        protected MapTileDownloadManager _mapTileDownloadManager;
        /**
         * stored position
         */
        protected GeoLatLng _storedPosition;
        /**
         * stored zoom Level
         */
        protected int _storedZoomLevel;

        /**
         * pending drawing image queue, used to order the routing and map images.
         */
        private readonly Hashtable _pendingDrawImageQueue = new Hashtable();


        private readonly GeoBounds _centerRect;
        private int _numOfMapRects;
        private readonly GeoBounds[] _mapRects;
        private GeoLatLng _newCenter;
        private readonly GeoBounds _newCenterRect;
        private int _numOfNewMapRects;
        private readonly GeoBounds[] _newMapRects;
        private readonly GeoBounds _drawRect;
        private readonly GeoBounds _mapRect;
        private readonly Hashtable _whatsInMapCanvas = new Hashtable();
        private readonly ArrayList _needToUpdateMapIndexes = new ArrayList();
        private int _screenOffsetX;
        private int _screenOffsetY;

        private readonly Rectangle _mapRectangle;
        private Rectangle _screenRectangle;
        private readonly IMapTileReadyListener _mapTileReadyListener;

        internal class MapTileReadyListener : IMapTileReadyListener
        {

            private readonly RasterMap _rasterMap;
            internal MapTileReadyListener(RasterMap rasterMap)
            {
                _rasterMap = rasterMap;
            }

            public void Done(ImageTileIndex imageTileIndex, IImage image)
            {
                try
                {
                    _rasterMap.DrawMapTileInMapCanvas(imageTileIndex, image);
                }
                catch (Exception)
                {

                }
            }
        }


        //static RasterMap(){
        //    try {
        //     InputStream in=(new Object()).getClass().
        //                getResourceAsStream("/images/downloading.png");
        //        byte []buffer=new byte[in.available()];
        //        in.read(buffer);
        //        in.close();
        //        for(int i=0;i<buffer.length;i++){
        //            if(i%8==0){
        //                System.out.println();
        //            }
        //            byte ch=buffer[i];
        //            int ich=(int)ch;
        //            if(ich<0) {
        //                ich += 256;
        //            }

        //            string str=Integer.toHexString(ich);
        //            if(ich<16){
        //                str="0"+str;
        //            }
        //            str="0x"+str+",";
        //            System.out.print(str);

        //        }
        //        System.out.println();

        //    } catch (IOException ex) {
        //        ex.printStackTrace();
        //    }
        //}
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        protected override void DrawMapCanvas()
        {
            try
            {
                lock (_syncObject)
                {
                    _pendingDrawImageQueue.Clear();
                    _screenOffsetX = (GetMapWidth() - _screenSize.Width) / 2;
                    _screenOffsetY = (GetMapHeight() - _screenSize.Height) / 2;
                    GeoPoint topLeft;
                    GeoPoint bottomRight;
                    GeoPoint center = FromLatLngToPixel(_mapCenterPt, _mapZoomLevel);
                    topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0, center.Y
                            - _mapSize.Height / 2.0);
                    bottomRight = new GeoPoint(center.X + _mapSize.Width / 2.0,
                            center.Y + _mapSize.Height / 2.0);
                    GeoLatLng topLeftLatLng = FromPixelToLatLng(topLeft,
                            _mapZoomLevel);
                    GeoLatLng bottomRightLatLng = FromPixelToLatLng(bottomRight,
                            _mapZoomLevel);

                    GeoPoint topLeftIndex;
                    GeoPoint bottomRightIndex;
                    topLeftIndex = ConvertCoordindates2Tiles(topLeftLatLng.Lat(),
                            topLeftLatLng.Lng(), _mapZoomLevel);
                    bottomRightIndex =
                            ConvertCoordindates2Tiles(bottomRightLatLng.Lat(),
                            bottomRightLatLng.Lng(), _mapZoomLevel);
                    int maxTile = (int)(MathEx.Pow(2, _mapZoomLevel) + 0.5);
                    int srcX, srcY;
                    int srcWidth, srcHeight;
                    int destX, destY;

                    int xIndex, yIndex;
                    _mapGraphics.SetColor(0xFFFFFF);
                    _mapGraphics.SetClip(0, 0, _mapSize.Width,
                             _mapSize.Height);
                    _mapGraphics.FillRect(0, 0, _mapSize.Width,
                             _mapSize.Height);
                    _numOfMapRects = 0;
                    //the centered rectangle
                    _centerRect.SetRect(center.X - _screenSize.Width / 2.0,
                            center.Y - _screenSize.Height / 2.0,
                            _screenSize.Width, _screenSize.Height);
                    _drawRect.SetRect(center.X - _mapSize.Width / 2.0,
                            center.Y - _mapSize.Height / 2.0,
                            _mapSize.Width, _mapSize.Height);
                    for (xIndex = 0; xIndex <
                            _mapSize.Width / MapTileDownloadManager.TileDownloading.GetWidth(); xIndex++)
                    {
                        for (yIndex = 0;
                        yIndex <= _mapSize.Height / MapTileDownloadManager.TileDownloading.GetHeight();
                        yIndex++)
                        {

                            _mapGraphics.DrawImage(MapTileDownloadManager.TileDownloading,
                                    xIndex * MapTileDownloadManager.TileDownloading.GetWidth(),
                                    yIndex * MapTileDownloadManager.TileDownloading.GetHeight());
                        }
                    }
                    _whatsInMapCanvas.Clear();

                    //get all the mapRect 256X256 which has intersection with
                    //the center rectangle(screen rectangle).
                    for (xIndex = (int)topLeftIndex.X;
                    xIndex <= bottomRightIndex.X; xIndex++)
                    {
                        for (yIndex = (int)topLeftIndex.Y;
                        yIndex <= bottomRightIndex.Y; yIndex++)
                        {
                            _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                    yIndex * MAP_TILE_WIDTH,
                                    MAP_TILE_WIDTH, MAP_TILE_WIDTH);
                            if (_mapRect.Intersects(_centerRect.GetX(),
                                    _centerRect.GetY(),
                                    _centerRect.GetWidth(),
                                    _centerRect.GetHeight()))
                            {

                                _mapRects[_numOfMapRects]
                                        .SetRect(xIndex * MAP_TILE_WIDTH,
                                        yIndex * MAP_TILE_WIDTH, MAP_TILE_WIDTH,
                                        MAP_TILE_WIDTH);
                                _numOfMapRects++;
                            }
                        }
                    }

                    //reorder the map rects, make the most close to center rectangle
                    //the first.
                    ReorderMapRects();

                    for (int i = 0; i < _numOfMapRects; i++)
                    {
                        xIndex = (int)(_mapRects[i].GetX() / MAP_TILE_WIDTH + 0.5);
                        yIndex = (int)(_mapRects[i].GetY() / MAP_TILE_WIDTH + 0.5);
                        _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                yIndex * MAP_TILE_WIDTH, MAP_TILE_WIDTH,
                                MAP_TILE_WIDTH);
                        if (_mapRect.Intersects(_centerRect.GetX(), _centerRect.GetY(),
                                _centerRect.GetWidth(), _centerRect.GetHeight()))
                        {

                            GeoBounds intersectRect
                                    = _mapRect.CreateIntersection(_drawRect);
                            srcX = (int)(intersectRect.GetX()
                                    - xIndex * MAP_TILE_WIDTH + 0.5);
                            srcY = (int)(intersectRect.GetY()
                                    - yIndex * MAP_TILE_WIDTH + 0.5);
                            srcWidth = MAP_TILE_WIDTH - srcX;

                            srcHeight = MAP_TILE_WIDTH - srcY;
                            destX = (int)(intersectRect.GetX()
                                    - _drawRect.GetX() + 0.5);
                            destY = (int)(intersectRect.GetY()
                                    - _drawRect.GetY() + 0.5);

                            int[] mapSequences = (int[])MapType.MapSequences[_mapType];
                            for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                            {
                                IImage image = GetImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                        yIndex % maxTile,
                                        _mapZoomLevel);

                                try
                                {
                                    if (image != null
                                            && image != MapTileDownloadManager.TileDownloading)
                                    {
                                        DrawRegion(_mapGraphics, image,
                                                srcX, srcY, srcWidth, srcHeight,
                                                destX, destY);
                                        if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                        {
                                            _mapGraphics.SetColor(0x4f0000FF);
                                            _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                        }
                                        _mapGraphics.DrawString("("+(xIndex % maxTile)+","+(yIndex % maxTile)+")",destX,destY );
                                       
 
                                        string inMapCanvsKey =
                                                mapSequences[mapSequenceIndex] + "|" + (xIndex % maxTile) + "|" + (yIndex % maxTile) + "|" + _mapZoomLevel;
                                        GeoPoint mapCavasIndex = new GeoPoint(xIndex, yIndex);
                                        if (!_whatsInMapCanvas.ContainsKey(inMapCanvsKey))
                                            _whatsInMapCanvas.Add(inMapCanvsKey, mapCavasIndex);
                                    }

                                }
                                catch (Exception)
                                {

                                }


                            }

                            //check if there's map direction need to draw.
                            if (_mapTileDownloadManager.GetMapDirection() != null)
                            {
                                GetImage(MapType.ROUTING_DIRECTION, xIndex % maxTile,
                                    yIndex % maxTile,
                                    _mapZoomLevel);

                            }
                        }

                    }
                    if (_mapDrawingListener != null)
                    {
                        _mapDrawingListener.Done();
                    }
                }


            }
            catch (Exception)
            {

            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get updated map images.(the screen rect still within the map rect)
         */
        private void DrawUpdatedMapCanvas()
        {
            try
            {
                lock (_syncObject)
                {
                    int maxTile = (int)(MathEx.Pow(2, _mapZoomLevel) + 0.5);

                    for (int i = 0; i < _needToUpdateMapIndexes.Count; i++)
                    {
                        GeoPoint mapCanvasIndex =
                                (GeoPoint)_needToUpdateMapIndexes[i];

                        int xIndex = (int)(mapCanvasIndex.GetX() + 0.5);
                        int yIndex = (int)(mapCanvasIndex.GetY() + 0.5);
                        _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                yIndex * MAP_TILE_WIDTH,
                                MAP_TILE_WIDTH, MAP_TILE_WIDTH);

                        GeoBounds intersectRect = _mapRect.CreateIntersection(_drawRect);
                        int srcX = (int)(intersectRect.GetX()
                                         - xIndex * MAP_TILE_WIDTH + 0.5);
                        int srcY = (int)(intersectRect.GetY()
                                         - yIndex * MAP_TILE_WIDTH + 0.5);
                        int srcWidth = MAP_TILE_WIDTH - srcX;

                        int srcHeight = MAP_TILE_WIDTH - srcY;
                        int destX = (int)(intersectRect.GetX() - _drawRect.GetX() + 0.5);
                        int destY = (int)(intersectRect.GetY() - _drawRect.GetY() + 0.5);

                        int[] mapSequences = (int[])MapType.MapSequences[_mapType];
                        for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                        {
                            IImage image = GetImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                    yIndex % maxTile,
                                    _mapZoomLevel);

                            try
                            {
                                if (image != null && image != MapTileDownloadManager.TileDownloading)
                                {
                                    DrawRegion(_mapGraphics, image,
                                            srcX, srcY, srcWidth, srcHeight,
                                            destX, destY);
                                    if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                    {
                                        _mapGraphics.SetColor(0x4f0000FF);
                                        _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                    }
                                    _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);
                      
                                    string inMapCanvsKey = mapSequences[mapSequenceIndex] + "|" + (xIndex % maxTile) + "|" + (yIndex % maxTile) + "|" + _mapZoomLevel;
                                    GeoPoint mapCavasIndex = new GeoPoint(xIndex, yIndex);
                                    if (!_whatsInMapCanvas.ContainsKey(inMapCanvsKey))
                                        _whatsInMapCanvas.Add(inMapCanvsKey, mapCavasIndex);
                                }

                            }
                            catch (Exception)
                            {

                            }

                        }


                        if (_mapTileDownloadManager.GetMapDirection() != null)
                        {
                            GetImage(MapType.ROUTING_DIRECTION, xIndex % maxTile,
                                yIndex % maxTile,
                                _mapZoomLevel);

                        }

                    }
                    if (_mapDrawingListener != null)
                    {
                        _mapDrawingListener.Done();
                    }
                }

            }
            catch (Exception)
            {

            }

        }

        /**
         *
         */
        private void DrawMapTileInMapCanvas(ImageTileIndex imageTileIndex,
                IImage image)
        {
            try
            {
                lock (_syncObject)
                {


                    if (_mapZoomLevel == imageTileIndex.MapZoomLevel)
                    {
                        GeoPoint center = FromLatLngToPixel(GetScreenCenter(),
                                _mapZoomLevel);
                        GeoPoint topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0,
                                                        center.Y - _mapSize.Height / 2.0);
                        GeoPoint bottomRight = new GeoPoint(center.X + _mapSize.Width / 2.0,
                                                            center.Y + _mapSize.Height / 2.0);
                        GeoLatLng topLeftLatLng =
                                FromPixelToLatLng(topLeft, _mapZoomLevel);
                        GeoLatLng bottomRightLatLng =
                                FromPixelToLatLng(bottomRight, _mapZoomLevel);

                        GeoPoint topLeftIndex = ConvertCoordindates2Tiles(topLeftLatLng.Lat(),
                                                                          topLeftLatLng.Lng(), _mapZoomLevel);
                        GeoPoint bottomRightIndex = ConvertCoordindates2Tiles(
                            bottomRightLatLng.Lat(),
                            bottomRightLatLng.Lng(), _mapZoomLevel);
                        int xIndex, yIndex;
                        int destX, destY;
                        int srcX, srcY;
                        int srcWidth, srcHeight;
                        _numOfMapRects = 0;
                        _centerRect.SetRect(center.X - _screenSize.Width / 2.0,
                                center.Y - _screenSize.Height / 2.0,
                                _screenSize.Width, _screenSize.Height);


                        for (xIndex = (int)topLeftIndex.X;
                            xIndex <= bottomRightIndex.X; xIndex++)
                        {
                            for (yIndex = (int)topLeftIndex.Y;
                                    yIndex <= bottomRightIndex.Y; yIndex++)
                            {
                                _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                        yIndex * MAP_TILE_WIDTH,
                                        MAP_TILE_WIDTH, MAP_TILE_WIDTH);
                                if (_mapRect.Intersects(_centerRect.GetX(),
                                        _centerRect.GetY(),
                                        _centerRect.GetWidth(),
                                        _centerRect.GetHeight()))
                                {

                                    _mapRects[_numOfMapRects].SetRect(
                                            xIndex * MAP_TILE_WIDTH,
                                            yIndex * MAP_TILE_WIDTH,
                                            MAP_TILE_WIDTH, MAP_TILE_WIDTH);
                                    _numOfMapRects++;
                                }
                            }
                        }

                        for (int i = 0; i < _numOfMapRects; i++)
                        {
                            xIndex = (int)(_mapRects[i].GetX()
                                    / MAP_TILE_WIDTH + 0.5);
                            yIndex = (int)(_mapRects[i].GetY()
                                    / MAP_TILE_WIDTH + 0.5);
                            if (xIndex == imageTileIndex.XIndex
                                    && yIndex == imageTileIndex.YIndex)
                            {
                                _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                        yIndex * MAP_TILE_WIDTH, MAP_TILE_WIDTH,
                                        MAP_TILE_WIDTH);
                                if (_mapRect.Intersects(_centerRect.GetX(),
                                        _centerRect.GetY(),
                                        _centerRect.GetWidth(),
                                        _centerRect.GetHeight()))
                                {
                                    GeoBounds intersectRect
                                            = _mapRect.CreateIntersection(_drawRect);
                                    srcX = (int)(intersectRect.GetX()
                                            - xIndex * MAP_TILE_WIDTH + 0.5);
                                    srcY = (int)(intersectRect.GetY()
                                            - yIndex * MAP_TILE_WIDTH + 0.5);
                                    srcWidth = MAP_TILE_WIDTH - srcX;

                                    srcHeight = MAP_TILE_WIDTH - srcY;
                                    destX = (int)(intersectRect.GetX()
                                            - _drawRect.GetX() + 0.5);
                                    destY = (int)(intersectRect.GetY()
                                            - _drawRect.GetY() + 0.5);
                                    try
                                    {
                                        if (image != null &&
                                                image != MapTileDownloadManager
                                         .TileDownloading)
                                        {
                                            int maxTile = (int)(MathEx.Pow(2, _mapZoomLevel) + 0.5);

                                            if (imageTileIndex.MapType != MapType.ROUTING_DIRECTION)
                                            {

                                                //first draw map tiles below current map tile.
                                                //if there's map tile below current map tile havn't
                                                //be drawn, adding current map tile to pending queue.
                                                int[] mapSequences = (int[])MapType.MapSequences[_mapType];
                                                bool needToAddToQueue = false;
                                                for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                {
                                                    if (imageTileIndex.MapType == mapSequences[mapSequenceIndex])
                                                    {
                                                        break;
                                                    }
                                                    IImage backImage = GetCachedImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                                            yIndex % maxTile,
                                                            _mapZoomLevel);

                                                    string key = mapSequences[mapSequenceIndex] + "|" +
                                                            (xIndex % maxTile) + "|" +
                                                            (yIndex % maxTile) + "|" +
                                                            _mapZoomLevel;
                                                    if (!_whatsInMapCanvas.Contains(key))
                                                    {
                                                        needToAddToQueue = true;
                                                    }
                                                    if (backImage != null && backImage != MapTileDownloadManager.TileDownloading)
                                                    {
                                                        DrawRegion(_mapGraphics, backImage,
                                                                srcX, srcY, srcWidth,
                                                                srcHeight, destX, destY);
                                                        if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                        {
                                                            _mapGraphics.SetColor(0x4f0000FF);
                                                            _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                        }
                                                        _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);
                                    

                                                    }
                                                    else
                                                    {
                                                        needToAddToQueue = true;
                                                    }
                                                }

                                                if (needToAddToQueue)
                                                {
                                                    string key = imageTileIndex.MapType + "|" +
                                                            (xIndex % maxTile) + "|" +
                                                            (yIndex % maxTile) + "|" +
                                                            _mapZoomLevel;

                                                    _pendingDrawImageQueue.Add(key, image);
                                                }

                                                //draw current map tile.
                                                DrawRegion(_mapGraphics, image,
                                                        srcX, srcY, srcWidth,
                                                        srcHeight, destX, destY);
                                                if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                {
                                                    _mapGraphics.SetColor(0x4f0000FF);
                                                    _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                }
                                                _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);


                                                //find next image sequence.
                                                int nextImageSequence = 0;
                                                for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                {
                                                    if (imageTileIndex.MapType == mapSequences[mapSequenceIndex])
                                                    {
                                                        nextImageSequence = mapSequenceIndex + 1;
                                                        break;
                                                    }
                                                }
                                                //draw next image sequences.
                                                for (int mapSequenceIndex = nextImageSequence; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                {
                                                    string key = mapSequences[mapSequenceIndex] + "|" +
                                                      (xIndex % maxTile) + "|" +
                                                      (yIndex % maxTile) + "|" +
                                                      _mapZoomLevel;

                                                    IImage pendImage = (IImage)_pendingDrawImageQueue[key];
                                                    if (pendImage != null)
                                                    {
                                                        DrawRegion(_mapGraphics, pendImage,
                                                                srcX, srcY, srcWidth,
                                                                srcHeight, destX, destY);
                                                        if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                        {
                                                            _mapGraphics.SetColor(0x4f0000FF);
                                                            _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                        }
                                                        _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);
                

                                                    }
                                                }
                                                if (_mapTileDownloadManager.GetMapDirection() != null)
                                                {
                                                    string key = MapType.ROUTING_DIRECTION + "|" +
                                                            (xIndex % maxTile) + "|" +
                                                            (yIndex % maxTile) + "|" +
                                                            _mapZoomLevel;

                                                    IImage pendImage = (IImage)_pendingDrawImageQueue[key];
                                                    if (pendImage != null)
                                                    {
                                                        DrawRegion(_mapGraphics, pendImage,
                                                                srcX, srcY, srcWidth,
                                                                srcHeight, destX, destY);
                                                        if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                        {
                                                            _mapGraphics.SetColor(0x4f0000FF);
                                                            _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                        }
                                                        _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);
                        

                                                        //check to see need to remove the route image.
                                                        bool needRemoveRoutingImage = true;
                                                        for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                        {

                                                            IImage pendImage1 = GetCachedImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                                            yIndex % maxTile,
                                                            _mapZoomLevel);
                                                            string key1 = mapSequences[mapSequenceIndex] + "|" +
                                                                (xIndex % maxTile) + "|" +
                                                                (yIndex % maxTile) + "|" +
                                                                _mapZoomLevel;
                                                            if (pendImage1 == null || pendImage1 == MapTileDownloadManager.TileDownloading ||
                                                                    !_whatsInMapCanvas.ContainsKey(key1))
                                                            {
                                                                needRemoveRoutingImage = false;

                                                                break;
                                                            }
                                                        }

                                                        if (needRemoveRoutingImage)
                                                        {

                                                            _pendingDrawImageQueue.Remove(key);
                                                        }
                                                    }

                                                }
                                                string inMapCanvsKey = imageTileIndex.MapType + "|" + (xIndex % maxTile) + "|" + (yIndex % maxTile) +
                                                        "|" + imageTileIndex.MapZoomLevel;
                                                GeoPoint mapCavasIndex = new GeoPoint(xIndex, yIndex);
                                                if (!_whatsInMapCanvas.ContainsKey(inMapCanvsKey))
                                                    _whatsInMapCanvas.Add(inMapCanvsKey,
                                                        mapCavasIndex);

                                                //check to see to remove from pendingDrawImageQueue

                                                bool needRemovePendingImage = true;
                                                for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                {

                                                    IImage pendImage1 = GetCachedImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                                            yIndex % maxTile,
                                                            _mapZoomLevel);
                                                    if (pendImage1 == null || pendImage1 == MapTileDownloadManager.TileDownloading)
                                                    {

                                                        needRemovePendingImage = false;
                                                        break;
                                                    }
                                                }

                                                if (needRemovePendingImage)
                                                {
                                                    for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                    {
                                                        string key = mapSequences[mapSequenceIndex] + "|" +
                                                                (xIndex % maxTile) + "|" +
                                                                (yIndex % maxTile) + "|" +
                                                                _mapZoomLevel;
                                                        if (_pendingDrawImageQueue.ContainsKey(key))
                                                        {

                                                            _pendingDrawImageQueue.Remove(key);
                                                        }
                                                    }

                                                }


                                            }
                                            else
                                            {//routing image.

                                                int[] mapSequences = (int[])MapType.MapSequences[_mapType];
                                                bool needToAddToQueue = false;
                                                for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                                                {
                                                    IImage backImage = GetCachedImage(mapSequences[mapSequenceIndex], xIndex % maxTile,
                                                            yIndex % maxTile,
                                                            _mapZoomLevel);

                                                    string key = mapSequences[mapSequenceIndex] + "|" +
                                                            (xIndex % maxTile) + "|" +
                                                            (yIndex % maxTile) + "|" +
                                                            _mapZoomLevel;
                                                    if (!_whatsInMapCanvas.Contains(key))
                                                    {
                                                        needToAddToQueue = true;
                                                    }

                                                    if (backImage != null && backImage != MapTileDownloadManager.TileDownloading)
                                                    {
                                                        DrawRegion(_mapGraphics, backImage,
                                                                srcX, srcY, srcWidth,
                                                                srcHeight, destX, destY);
                                                        if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                        {
                                                            _mapGraphics.SetColor(0x4f0000FF);
                                                            _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                        }
                                                        _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);


                                                    }
                                                    else
                                                    {
                                                        needToAddToQueue = true;
                                                    }
                                                }

                                                if (needToAddToQueue)
                                                {
                                                    string key = MapType.ROUTING_DIRECTION + "|" +
                                                            (xIndex % maxTile) + "|" +
                                                            (yIndex % maxTile) + "|" +
                                                            _mapZoomLevel;
                                                    _pendingDrawImageQueue.Add(key, image);

                                                }


                                                DrawRegion(_mapGraphics, image,
                                                        srcX, srcY, srcWidth,
                                                        srcHeight, destX, destY);
                                                if (SelectedMapTileArea.ContainsPoint(new GeoPoint(xIndex, yIndex)))
                                                {
                                                    _mapGraphics.SetColor(0x4f0000FF);
                                                    _mapGraphics.FillRect(destX, destY, srcWidth, srcHeight);
                                                }
                                                _mapGraphics.DrawString("(" + (xIndex % maxTile) + "," + (yIndex % maxTile) + ")", destX, destY);

                                            }



                                        }

                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                                break;
                            }
                        }
                        if (_mapDrawingListener != null)
                        {
                            _mapDrawingListener.Done();
                        }


                    }

                }



            }
            catch (Exception)
            {

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see if need to download images according to the new center.
         */
        private bool IsMapImageStale(GeoLatLng newCenter)
        {
            lock (_syncObject)
            {
                int maxTile = (int)(MathEx.Pow(2, _mapZoomLevel) + 0.5);
                _newCenter = newCenter;
                GeoPoint topLeft;
                GeoPoint bottomRight;
                GeoPoint center = FromLatLngToPixel(newCenter, _mapZoomLevel);
                topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0,
                        center.Y - _mapSize.Height / 2.0);
                bottomRight = new GeoPoint(center.X + _mapSize.Width / 2.0,
                        center.Y + _mapSize.Height / 2.0);
                GeoLatLng topLeftLatLng = FromPixelToLatLng(topLeft, _mapZoomLevel);
                GeoLatLng bottomRightLatLng = FromPixelToLatLng(bottomRight,
                        _mapZoomLevel);

                GeoPoint topLeftIndex;
                GeoPoint bottomRightIndex;
                topLeftIndex = ConvertCoordindates2Tiles(topLeftLatLng.Lat(),
                        topLeftLatLng.Lng(), _mapZoomLevel);
                bottomRightIndex = ConvertCoordindates2Tiles(bottomRightLatLng.Lat(),
                        bottomRightLatLng.Lng(), _mapZoomLevel);
                int xIndex, yIndex;
                _numOfNewMapRects = 0;
                _newCenterRect.SetRect(center.X - _screenSize.Width / 2.0,
                        center.Y - _screenSize.Height / 2.0,
                        _screenSize.Width, _screenSize.Height);

                for (xIndex = (int)topLeftIndex.X;
                xIndex <= bottomRightIndex.X; xIndex++)
                {
                    for (yIndex = (int)topLeftIndex.Y;
                    yIndex <= bottomRightIndex.Y; yIndex++)
                    {
                        _mapRect.SetRect(xIndex * MAP_TILE_WIDTH,
                                yIndex * MAP_TILE_WIDTH, MAP_TILE_WIDTH,
                                MAP_TILE_WIDTH);
                        if (_mapRect.Intersects(_newCenterRect.GetX(),
                                _newCenterRect.GetY(),
                                _newCenterRect.GetWidth(),
                                _newCenterRect.GetHeight()))
                        {
                            _newMapRects[_numOfNewMapRects]
                                    .SetRect(xIndex * MAP_TILE_WIDTH,
                                    yIndex * MAP_TILE_WIDTH,
                                    MAP_TILE_WIDTH, MAP_TILE_WIDTH);
                            _numOfNewMapRects++;
                        }
                    }
                }
                _needToUpdateMapIndexes.Clear();
                int[] mapSequences = (int[])MapType.MapSequences[_mapType];
                for (int i = 0; i < _numOfNewMapRects; i++)
                {
                    xIndex = (int)(_newMapRects[i].GetX() / MAP_TILE_WIDTH + 0.5);
                    yIndex = (int)(_newMapRects[i].GetY() / MAP_TILE_WIDTH + 0.5);
                    for (int mapSequenceIndex = 0; mapSequenceIndex < mapSequences.Length; mapSequenceIndex++)
                    {
                        string key = mapSequences[mapSequenceIndex] + "|" + (xIndex % maxTile) + "|" + (yIndex % maxTile) + "|" + _mapZoomLevel;
                        if (!_whatsInMapCanvas.ContainsKey(key))
                        {
                            GeoPoint mapCavasIndex = new GeoPoint(xIndex, yIndex);
                            _needToUpdateMapIndexes.Add(mapCavasIndex);
                            break;
                        }

                    }
                }
            }
            return _needToUpdateMapIndexes.Count != 0;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * need to get a new map image.
         * @return
         */
        private bool NeedToGetNewMapImage()
        {
            _screenRectangle.SetX(_screenOffsetX);
            _screenRectangle.SetY(_screenOffsetY);
            return !_mapRectangle.Contains(_screenRectangle);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reorder the map tiles, make the most close to center download first
         */
        private void ReorderMapRects()
        {
            int i, j;
            double dj, dai;
            GeoBounds ai = new GeoBounds(0, 0, 0, 0);
            for (i = 1; i < _numOfMapRects; i++)
            {
                j = i - 1;
                ai.SetRect(_mapRects[i].GetX(),
                        _mapRects[i].GetY(),
                        _mapRects[i].GetWidth(),
                        _mapRects[i].GetHeight());
                dj = (_mapRects[j].GetCenterX() - _centerRect.GetCenterX()) *
                        (_mapRects[j].GetCenterX() - _centerRect.GetCenterX()) +
                        (_mapRects[j].GetCenterY() - _centerRect.GetCenterY()) *
                        (_mapRects[j].GetCenterY() - _centerRect.GetCenterY());

                dai = (ai.GetCenterX() - _centerRect.GetCenterX()) *
                        (ai.GetCenterX() - _centerRect.GetCenterX()) +
                        (ai.GetCenterY() - _centerRect.GetCenterY()) *
                        (ai.GetCenterY() - _centerRect.GetCenterY());
                while (dj > dai && j >= 0)
                {
                    _mapRects[j + 1].SetRect(_mapRects[j].GetX(),
                            _mapRects[j].GetY(),
                            _mapRects[j].GetWidth(),
                            _mapRects[j].GetHeight());
                    j--;
                }
                _mapRects[j + 1].SetRect(ai.GetX(),
                        ai.GetY(),
                        ai.GetWidth(),
                        ai.GetHeight());

            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * ask download mananger to download required map tile.
         * @param mtype
         * @param X
         * @param Y
         * @param zoomLevel
         * @return
         */
        private IImage GetCachedImage(int mtype, int x, int y, int zoomLevel)
        {
            return _mapTileDownloadManager.GetCachedImage(mtype, x, y,
                    zoomLevel);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * ask download mananger to download required map tile.
         * @param mtype
         * @param X
         * @param Y
         * @param zoomLevel
         * @return
         */
        private IImage GetImage(int mtype, int x, int y, int zoomLevel)
        {
            return _mapTileDownloadManager.GetImage(mtype, x, y,
                    zoomLevel);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw specific region on the map canvas.
         * @param graphics
         * @param src
         * @param x_src
         * @param y_src
         * @param Width
         * @param Height
         * @param x_dest
         * @param y_dest
         */
        private static void DrawRegion(IGraphics graphics, IImage src,
                int xSrc,
                int ySrc,
                int width,
                int height,
                int xDest,
                int yDest)
        {
            if (width != 0 && height != 0)
            {
                graphics.SetClip(xDest, yDest, width, height);
                graphics.DrawImage(src, -xSrc + xDest, -ySrc + yDest);
                graphics.SetColor(0x7FFF0000);
                graphics.DrawRect(xDest, yDest, width, height);
            }
        }
    }

}
