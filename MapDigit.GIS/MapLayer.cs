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
using MapDigit.GIS.Geometry;
using MapDigit.Drawing.Geometry;
using MapDigit.GIS.Drawing;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * MapLayer defines a map layer.Computer maps are organized into layers. Think
     * of the layers as transparencies that are stacked on top of one another. Each
     * layer Contains different aspects of the entire map. Each layer Contains
     * different map objects, such as regions, points, lines and text.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class MapLayer
    {

        /**
         * the Width of each map tile
         */
        public const int MAP_TILE_WIDTH = 256;

        /**
         * Max map zoom Level
         */
        public const int MAX_ZOOMLEVEL = 17;

        /**
         * Min map zoom Level
         */
        public const int MIN_ZOOMLEVEL = 0;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the graphics factory for the map layer.
         * @param abstractGraphicsFactory
         */
        public static void SetAbstractGraphicsFactory(AbstractGraphicsFactory
                abstractGraphicsFactory)
        {
            AbstractGraphicsFactory = abstractGraphicsFactory;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the graphics factory used by this map layer.
         * @return the graphics factory used by this map layer.
         */
        public static AbstractGraphicsFactory GetAbstractGraphicsFactory()
        {
            return AbstractGraphicsFactory;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert string to Latitude,Longitude pair, the input string has this format
         * [longitude,Latitude,altitude] for example  [115.857562,-31.948275,0]
         * @param location  location string
         * @return the geographical coordinates.
         */
        public static GeoLatLng FromStringToLatLng(string location)
        {
            location = location.Trim();
            location = location.Substring(1, location.Length - 1);
            int commaIndex = location.IndexOf(",");
            string longitude = location.Substring(0, commaIndex);
            int commaIndex1 = location.IndexOf(",", commaIndex + 1);
            string latitude = location.Substring(commaIndex + 1, commaIndex1 - commaIndex-1);
            double lat = Double.Parse(latitude);
            double lng = Double.Parse(longitude);
            return new GeoLatLng(lat, lng);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////

        /**
         * Computes the pixel coordinates of the given geographical point .
         * @param latLng  latitude,longitude pair of give point
         * @param zoomLevel   current zoom level
         * @return the pixel coordinates.
         */
        public static GeoPoint FromLatLngToPixel(GeoLatLng latLng, int zoomLevel)
        {
            //double latitude = latLng.Lat();
            //double longitude = latLng.Lng();
            //double power = 8 + zoomLevel;
            //double mapsize = MathEx.Pow(2, power);
            //double origin = mapsize / 2;
            //double longdeg = MathEx.Abs(-180 - longitude);
            //double longppd = mapsize / 360;
            //double longppdrad = mapsize / (2 * Math.PI);
            //double pixelx = longdeg * longppd;
            //double e = MathEx.Sin(latitude * (1 / 180.0 * MathEx.PI));
            //if (e > 0.9999)
            //{
            //    e = 0.9999;
            //}
            //if (e < -0.9999)
            //{
            //    e = -0.9999;
            //}

            //double pixely = origin + 0.5 * MathEx.Log2((1 + e) / (1 - e)) * (-longppdrad);
            //return new GeoPoint(pixelx, pixely);
            int pixelX, pixelY;
            TileSystem.LatLongToPixelXY(latLng.Lat(), latLng.Lng(), zoomLevel, out pixelX, out pixelY);
            return new GeoPoint(pixelX, pixelY);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the geographical coordinates from pixel coordinates.
         * @param pt  pixel coordinates.
         * @param zoomLevel   current zoom level
         * @return the geographical coordinates (latitude,longitude) pair
         */
        public static GeoLatLng FromPixelToLatLng(GeoPoint pt, int zoomLevel)
        {
            //const double maxLat = Math.PI;
            //double zoom = zoomLevel;
            //const double tileWidth = 256.0;
            //const double tileHeight = 256.0;
            //double tileY = (pt.Y / tileHeight);
            //double y = (pt.Y - tileY * tileHeight);
            //double maxTileY = MathEx.Pow(2, zoom);
            //double mercatorY = tileY + y / tileHeight;
            //double res = maxLat * (1 - 2 * mercatorY / maxTileY);
            //double a = MathEx.Exp(2 * res);
            //a = (a - 1) / (a + 1);
            //a = a / MathEx.Sqrt(1 - a * a);
            //double lat = MathEx.Atan(a) * 180 / Math.PI;

            //double tileX = pt.X / tileHeight;
            //double x = (pt.X - tileX * tileHeight);
            //double maxTileX = MathEx.Pow(2, zoom);
            //double mercatorX = tileX + x / tileWidth;
            //res = mercatorX / maxTileX;
            //double lng = 360 * res - 180;
            //return new GeoLatLng(lat, lng);
            double lat;
            double lng;
            TileSystem.PixelXYToLatLong((int)pt.X,(int)pt.Y,zoomLevel,out lat,out lng);
            return new GeoLatLng(lat, lng);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the pixel coordinates of the given geographical point in the map.
         * @param latlng the geographical coordinates.
         * @return the pixel coordinates in the map.
         */
        public GeoPoint FromLatLngToMapPixel(GeoLatLng latlng)
        {
            GeoPoint center = FromLatLngToPixel(_mapCenterPt, _mapZoomLevel);
            GeoPoint topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0,
                    center.Y - _mapSize.Height / 2.0);
            GeoPoint pointPos = FromLatLngToPixel(latlng, _mapZoomLevel);
            pointPos.X -= topLeft.X;
            pointPos.Y -= topLeft.Y;
            return new GeoPoint((int)(pointPos.X + 0.5), (int)(pointPos.Y + 0.5));

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the geographical coordinates from pixel coordinates in the map.
         * @param pt pixel coordinates in the map.
         * @return the the geographical coordinates.
         */
        public GeoLatLng FromMapPixelToLatLng(GeoPoint pt)
        {
            GeoPoint center = FromLatLngToPixel(_mapCenterPt, _mapZoomLevel);
            GeoPoint topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0,
                    center.Y - _mapSize.Height / 2.0);
            GeoPoint pointPos = new GeoPoint(pt.X, pt.Y);
            pointPos.X += topLeft.X;
            pointPos.Y += topLeft.Y;
            GeoLatLng latLng = FromPixelToLatLng(pointPos, _mapZoomLevel);
            return latLng;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return screen boundary in geo coordinates.
         * @param pt the center of the screen.
         * @return screen boundary in geo coordinates.
         */
        public GeoLatLngBounds GetScreenBounds(GeoLatLng pt)
        {
            lock (_syncObject)
            {
                GeoPoint center = FromLatLngToPixel(pt, _mapZoomLevel);
                int shiftWidth = _screenSize.Width;
                GeoPoint topLeft = new GeoPoint(center.X - _screenSize.Width / 2.0 - shiftWidth,
                                                center.Y - _screenSize.Height / 2.0 - _screenSize.Height);
                GeoPoint bottomRight = new GeoPoint(center.X + _screenSize.Width / 2.0 + shiftWidth,
                                                    center.Y + _screenSize.Height / 2.0 + _screenSize.Height);
                GeoLatLng topLeftLatLng = FromPixelToLatLng(topLeft, _mapZoomLevel);
                GeoLatLng bottomRightLatLng = FromPixelToLatLng(bottomRight, _mapZoomLevel);
                double minY = Math.Min(bottomRightLatLng.Lat(), topLeftLatLng.Lat());
                double maxY = Math.Max(bottomRightLatLng.Lat(), topLeftLatLng.Lat());
                double minX = Math.Min(bottomRightLatLng.Lng(), topLeftLatLng.Lng());
                double maxX = Math.Max(bottomRightLatLng.Lng(), topLeftLatLng.Lng());
                return new GeoLatLngBounds(minX, minY, maxX - minX, maxY - minY);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return screen boundary in geo coordinates.
         * @return screen boundary in geo coordinates.
         */
        public GeoLatLngBounds GetScreenBounds()
        {
            return GetScreenBounds(_mapCenterPt);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return screen boundary in geo coordinates.
         * @param pt the center of the screen.
         * @return screen boundary in geo coordinates.
         */
        public GeoLatLngBounds GetMapBounds(GeoLatLng pt)
        {
            lock (_syncObject)
            {
                GeoPoint center = FromLatLngToPixel(pt, _mapZoomLevel);
                int shiftWidth = _mapSize.Width / 8;
                GeoPoint topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0 - shiftWidth,
                                                center.Y - _mapSize.Height / 2.0 - _mapSize.Height);
                GeoPoint bottomRight = new GeoPoint(center.X + _mapSize.Width / 2.0 + shiftWidth,
                                                    center.Y + _mapSize.Height / 2.0 + _mapSize.Height);
                GeoLatLng topLeftLatLng = FromPixelToLatLng(topLeft, _mapZoomLevel);
                GeoLatLng bottomRightLatLng = FromPixelToLatLng(bottomRight, _mapZoomLevel);

                double minY = Math.Min(bottomRightLatLng.Lat(), topLeftLatLng.Lat());
                double maxY = Math.Max(bottomRightLatLng.Lat(), topLeftLatLng.Lat());
                double minX = Math.Min(bottomRightLatLng.Lng(), topLeftLatLng.Lng());
                double maxX = Math.Max(bottomRightLatLng.Lng(), topLeftLatLng.Lng());
                return new GeoLatLngBounds(minX, minY, maxX - minX, maxY - minY);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return screen boundary in geo coordinates.
         * @return screen boundary in geo coordinates.
         */
        public GeoLatLngBounds GetMapBounds()
        {
            return GetMapBounds(_mapCenterPt);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Starts a pan with given Distance in pixels.
         * directions. +1 is right and down, -1 is left and up, respectively.
         * @param dx X offset.
         * @param dy Y offset.
         */
        public virtual void PanDirection(int dx, int dy)
        {
            lock (_syncObject)
            {
                GeoPoint center = FromLatLngToPixel(_mapCenterPt, _mapZoomLevel);
                center.X += dx;
                center.Y += dy;
                GeoLatLng newCenter = FromPixelToLatLng(center, _mapZoomLevel);
                PanTo(newCenter);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Changes the center point of the map to the given point.
         * @param center a new center point of the map.
         */
        public virtual void PanTo(GeoLatLng center)
        {
            lock (_syncObject)
            {
                _mapCenterPt.X = center.X;
                _mapCenterPt.Y = center.Y;
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the map view to the given center.
         * @param center the center latitude,longitude of the map.
         * @param zoomLevel the zoom Level of the map [0,17].
         */
        public virtual void SetCenter(GeoLatLng center, int zoomLevel)
        {
            lock (_syncObject)
            {
                _mapZoomLevel = zoomLevel;
                _mapCenterPt.X = center.X;
                _mapCenterPt.Y = center.Y;
                DrawMapCanvas();
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the center point of the map.
         * @return current map center point.
         */
        public GeoLatLng GetCenter()
        {
            lock (_syncObject)
            {
                return _mapCenterPt;
            }
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Increments zoom level by one.
         */
        public virtual void ZoomIn()
        {
            lock (_syncObject)
            {
                _mapZoomLevel++;
                if (_mapZoomLevel >= MAX_ZOOMLEVEL)
                {
                    _mapZoomLevel = MAX_ZOOMLEVEL;
                }
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Decrements zoom level by one.
         */
        public virtual void ZoomOut()
        {
            lock (_syncObject)
            {
                _mapZoomLevel--;
                if (_mapZoomLevel < 0)
                {
                    _mapZoomLevel = 0;
                }
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the zoom level to the given new value.
         * @param level new map zoom level.
         */
        public virtual void SetZoom(int level)
        {
            lock (_syncObject)
            {
                _mapZoomLevel = level;
                DrawMapCanvas();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the zoom level of the map.
         * @return current map zoom level.
         */
        public int GetZoom()
        {
            lock (_syncObject)
            {
                return _mapZoomLevel;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Resize the map to a level that include given bounds
         * @param bounds new bound.
         */
        public virtual void Resize(GeoLatLngBounds bounds)
        {
            lock (_syncObject)
            {
                GeoLatLng sw = bounds.GetSouthWest();
                GeoLatLng ne = bounds.GetNorthEast();
                GeoLatLng center = new GeoLatLng {X = (sw.X + ne.X)/2.0, Y = (sw.Y + ne.Y)/2.0};
                GeoPoint pt1, pt2;
                for (int i = MAX_ZOOMLEVEL; i >= MIN_ZOOMLEVEL; i--)
                {
                    pt1 = FromLatLngToPixel(sw, i);
                    pt2 = FromLatLngToPixel(ne, i);
                    double dblWidth = Math.Abs(pt1.X - pt2.X);
                    double dblHeight = Math.Abs(pt1.Y - pt2.Y);
                    if (dblWidth < _mapSize.Width && dblHeight < _mapSize.Height)
                    {
                        _mapZoomLevel = i;
                        SetCenter(center, i);
                        break;
                    }
                }
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * is the point in current screen (is shown or not).
         * @param pt point to be tested.
         * @return true is in screen range.
         */
        public virtual bool IsPointVisible(GeoLatLng pt)
        {
            GeoPoint screenPt = FromLatLngToMapPixel(pt);
            return _mapSize.Contains((int)screenPt.X, (int)screenPt.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map screen Width.
         * @return the map screen Width.
         */
        public int GetMapWidth()
        {
            return _mapSize.Width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map screen Height.
         * @return the map screen Height.
         */
        public int GetMapHeight()
        {
            return _mapSize.Height;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the size for the map.
         * @param Width the Width of the map.
         * @param Height the Height of the map.
         */
        public virtual void SetMapSize(int width, int height)
        {
            lock (_syncObject)
            {
                _mapSize.Width = width;
                _mapSize.Height = height;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the view size of the map.
         * @param Width the Width of the view.
         * @param Height the Height of the view.
         */
        public virtual void SetScreenSize(int width, int height)
        {
            lock (_syncObject)
            {
                _screenSize.Width = width;
                _screenSize.Height = height;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map screen Width.
         * @return the map screen Width.
         */
        public int GetScreenWidth()
        {
            return _screenSize.Width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map screen Height.
         * @return the map screen Height.
         */
        public int GetScreenHeight()
        {
            return _screenSize.Height;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw the map layer to an graphics.
         * @param graphics
         */
        public abstract void Paint(IGraphics graphics);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Draw the map layer to an graphics.
         * @param graphics the graphics object where the map is drawn.
         * @param offsetX the drawing start X coordinate.
         * @param offsetY the drawing start Y coordinate.
         */
        public abstract void Paint(IGraphics graphics, int offsetX, int offsetY);

        /**
         * the center of this map.
         */
        protected volatile GeoLatLng _mapCenterPt = new GeoLatLng();

        /**
         * current map zoom level
         */
        protected volatile int _mapZoomLevel = 1;

        /**
         * the size of the map size.
         */
        protected volatile Rectangle _mapSize = new Rectangle();

        /**
         * the size of the screen size.
         */
        protected volatile Rectangle _screenSize = new Rectangle();

        /**
         * sync object.
         */
        protected object _syncObject = new object();

        /**
         * Abstract graphics factory.
         */
        protected static AbstractGraphicsFactory AbstractGraphicsFactory;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * draw map on the image canvas
         */
        protected abstract void DrawMapCanvas();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         * @param Width the Width of the map layer.
         * @param Height the Height of the map layer.
         */
        protected MapLayer(int width, int height)
        {
            IDisplay display = AbstractGraphicsFactory.GetDisplayInstance();
            _screenSize.X = 0; _screenSize.Y = 0;
            _screenSize.Width = display.GetDisplayWidth();
            _screenSize.Height = display.GetDisplayHeight();
            _mapSize.X = 0; _mapSize.Y = 0;
            _mapSize.Width = width; _mapSize.Height = height;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the index of map tiles based on given piexl coordinates
         * @param X  X coordinates
         * @param Y Y coordinates .
         * @return the the index of map tiles
         */
        protected static GeoPoint GetMapIndex(double x, double y)
        {
            double longtiles = x / MAP_TILE_WIDTH;
            int tilex = Cast2Integer(longtiles);
            int tiley = Cast2Integer(y / MAP_TILE_WIDTH);
            return new GeoPoint(tilex, tiley);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the index of map tiles based on given geographical coordinates
         * @param latitude  Y coordinates in geographical space.
         * @param longitude X coordinates in geographical space.
         * @param zoomLevel   current zoom level
         * @return the the index of map tiles
         */
        protected static GeoPoint ConvertCoordindates2Tiles(double latitude,
                double longitude, int zoomLevel)
        {

            GeoPoint pt = FromLatLngToPixel(new GeoLatLng(latitude, longitude), zoomLevel);
            double pixelx = pt.X;
            double longtiles = pixelx / MAP_TILE_WIDTH;
            int tilex = Cast2Integer(longtiles);
            double pixely = pt.Y;
            int tiley = Cast2Integer(pixely / MAP_TILE_WIDTH);
            return new GeoPoint(tilex, tiley);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  cast double to integer
         * @param f the double value.
         * @return the closed interger for the double value.
         */
        protected static int Cast2Integer(double f)
        {
            if (f < 0)
            {
                return (int)MathEx.Ceil(f);
            }
            return (int)MathEx.Floor(f);
        }
    }

}
