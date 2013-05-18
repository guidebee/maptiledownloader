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
using System.IO;
using MapDigit.Drawing;
using MapDigit.GIS.Drawing;
using MapDigit.Drawing.Geometry;
using MapDigit.GIS.Geometry;
using MapDigit.GIS.Vector;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  Vector map render, each time, the renderer draw one map tile.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    internal class MapDirectionRenderer : MapTileAbstractReader
    {

        /**
         * transparent color value.
         */
        private const int TRANSPARENCY = 0x7fffffff;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constrcutor.
         */
        public MapDirectionRenderer()
        {

            if (MapConfiguration.RoutePen != null)
            {
                RoutePen = MapConfiguration.RoutePen;
            }
            else
            {
                RoutePen = new Pen(new Color(0x7F00FF00, false), 4);
            }
            if (MapConfiguration.StartIcon != null)
            {
                StartIcon = MapConfiguration.StartIcon;
            }
            else
            {
                StartIcon = ROUTE_ICONS[0];
            }
            if (MapConfiguration.EndIcon != null)
            {
                EndIcon = MapConfiguration.EndIcon;
            }
            else
            {
                EndIcon = ROUTE_ICONS[0];
            }
            if (MapConfiguration.MiddleIcon != null)
            {
                MiddleIcon = MapConfiguration.MiddleIcon;
            }
            else
            {
                MiddleIcon = ROUTE_ICONS[1];
            }
            _mapDirectionLayer = new MapDirectionLayer(MapLayer.MAP_TILE_WIDTH,
                    MapLayer.MAP_TILE_WIDTH);
            _mapTileImage = MapLayer.GetAbstractGraphicsFactory()
                    .CreateImage(MapLayer.MAP_TILE_WIDTH,
                    MapLayer.MAP_TILE_WIDTH);
            _mapTileGraphics = _mapTileImage.GetGraphics();

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the route direction.
         * @param newDirection
         */
        public void SetMapDirection(MapDirection newDirection)
        {
            _mapDirectionLayer.SetMapDirection(newDirection);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the route direction.
         * @param newDirection
         */
        public MapDirection GetMapDirection()
        {
            return _mapDirectionLayer.GetMapDirection();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * empty implmetation.
         * @inheritDoc
         */
        public override void GetImage(int mtype, int x, int y, int zoomLevel)
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the image at given X,Y zoom level.
         * @param X  X index of the map tile
         * @param Y  Y index of the map tile.
         * @param zoomLevel zoom level of the map tile
         * @return the given image.
         */
        public IImage GetImage(int x, int y, int zoomLevel)
        {
            MapDirection mapDirection = GetMapDirection();
            if (mapDirection != null)
            {
                try
                {
                    const int shiftWidth = 4;
                    GeoPoint pt1 = new GeoPoint(x * MapLayer.MAP_TILE_WIDTH - shiftWidth,
                            y * MapLayer.MAP_TILE_WIDTH - shiftWidth);
                    GeoPoint pt2 = new GeoPoint((x + 1) * MapLayer.MAP_TILE_WIDTH + shiftWidth,
                            (y + 1) * MapLayer.MAP_TILE_WIDTH + shiftWidth);
                    GeoLatLng latLng1 = MapLayer.FromPixelToLatLng(pt1, zoomLevel);
                    GeoLatLng latLng2 = MapLayer.FromPixelToLatLng(pt2, zoomLevel);
                    double minY = Math.Min(latLng1.Lat(), latLng2.Lat());
                    double maxY = Math.Max(latLng1.Lat(), latLng2.Lat());
                    double minX = Math.Min(latLng1.Lng(), latLng2.Lng());
                    double maxX = Math.Max(latLng1.Lng(), latLng2.Lng());
                    GeoLatLngBounds geoBounds = new GeoLatLngBounds(minX, minY,
                            maxX - minX, maxY - minY);
                    GeoLatLng centerPt = geoBounds.GetCenter();
                    _mapDirectionLayer.SetCenter(centerPt, zoomLevel);
                    _mapDirectionLayer._screenBounds = geoBounds;
                    _mapTileGraphics.SetColor(TRANSPARENCY);
                    _mapTileGraphics.FillRect(0, 0, MapLayer.MAP_TILE_WIDTH,
                            MapLayer.MAP_TILE_WIDTH);
                    _mapDirectionLayer.Paint(_mapTileGraphics);
                    IsImagevalid = true;
                    if (_readListener != null)
                    {
                        _readListener.readProgress(16, 16);
                    }
                    return _mapTileImage.ModifyAlpha(160,
                            TRANSPARENCY);

                }
                catch (Exception)
                {

                }
            }
            return null;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * empty implmetation.
         * @inheritDoc
         */
        public override void CancelRead()
        {
        }

        readonly MapDirectionLayer _mapDirectionLayer;

        /**
         * one map tile size map canva.
         */
        private readonly IImage _mapTileImage;
        private readonly IGraphics _mapTileGraphics;
        private static Pen RoutePen;
        /**
         * start route icon.
         */
        private static IImage StartIcon;

        /**
         * start route icon.
         */
        private static IImage MiddleIcon;


        /**
         * start route icon.
         */
        private static IImage EndIcon;

        private static readonly IImage[] ROUTE_ICONS = new IImage[2];


        private static readonly byte[] ROUTE_IMAGE_ARRAY = new byte[]{
                 0x89,  0x50,  0x4e,  0x47,  0x0d,  0x0a,  0x1a,  0x0a,
                 0x00,  0x00,  0x00,  0x0d,  0x49,  0x48,  0x44,  0x52,
                 0x00,  0x00,  0x00,  0x12,  0x00,  0x00,  0x00,  0x09,
                 0x08,  0x03,  0x00,  0x00,  0x00,  0x08,  0x01,  0x8c,
                 0x3d,  0x00,  0x00,  0x00,  0x04,  0x67,  0x41,  0x4d,
                 0x41,  0x00,  0x00,  0xb1,  0x8e,  0x7c,  0xfb,  0x51,
                 0x93,  0x00,  0x00,  0x00,  0x20,  0x63,  0x48,  0x52,
                 0x4d,  0x00,  0x00,  0x7a,  0x25,  0x00,  0x00,  0x80,
                 0x83,  0x00,  0x00,  0xf9,  0xff,  0x00,  0x00,  0x80,
                 0xe6,  0x00,  0x00,  0x75,  0x2e,  0x00,  0x00,  0xea,
                 0x5f,  0x00,  0x00,  0x3a,  0x97,  0x00,  0x00,  0x17,
                 0x6f,  0x69,  0xe4,  0xc4,  0x2b,  0x00,  0x00,  0x03,
                 0x00,  0x50,  0x4c,  0x54,  0x45,  0x00,  0x00,  0x00,
                 0x4c,  0x6c,  0xf2,  0xff,  0x1d,  0x1d,  0xc0,  0xc0,
                 0xc0,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
                 0x00,  0x00,  0x00,  0x00,  0x00,  0xeb,  0x3e,  0x1a,
                 0x98,  0x00,  0x00,  0x00,  0x04,  0x74,  0x52,  0x4e,
                 0x53,  0xff,  0xff,  0xff,  0x00,  0x40,  0x2a,  0xa9,
                 0xf4,  0x00,  0x00,  0x00,  0x4d,  0x49,  0x44,  0x41,
                 0x54,  0x78,  0x9c,  0x62,  0x60,  0xc6,  0x00,  0x00,
                 0x01,  0xc4,  0x00,  0x27,  0xe0,  0x0c,  0x80,  0x00,
                 0x02,  0x11,  0x0c,  0x8c,  0x50,  0x31,  0x06,  0x26,
                 0x10,  0x03,  0x20,  0x80,  0x18,  0x40,  0x22,  0x8c,
                 0x10,  0x31,  0x06,  0x26,  0x26,  0x90,  0x18,  0x40,
                 0x00,  0x31,  0x80,  0x45,  0xc0,  0x62,  0x20,  0x11,
                 0x90,  0x18,  0x40,  0x00,  0x61,  0x51,  0x05,  0x10,
                 0x40,  0x58,  0xcc,  0x02,  0x08,  0x20,  0x2c,  0x36,
                 0x02,  0x04,  0x10,  0x9c,  0x87,  0x00,  0x00,  0x01,
                 0x06,  0x00,  0x7d,  0xaf,  0x01,  0x78,  0xb6,  0x3b,
                 0x01,  0xe8,  0x00,  0x00,  0x00,  0x00,  0x49,  0x45,
                 0x4e,  0x44,  0xae,  0x42,  0x60,  0x82
            };


        static MapDirectionRenderer()
        {
            try
            {
                IImage iconImage = MapLayer.GetAbstractGraphicsFactory().
                        CreateImage(new MemoryStream(ROUTE_IMAGE_ARRAY));

                if (iconImage != null)
                {
                    for (int i = 0; i < ROUTE_ICONS.Length; i++)
                    {
                        int x = i * 9 - 1;
                        if (x < 0)
                        {
                            x = 0;
                        }
                        ROUTE_ICONS[i] = iconImage.SubImage(x, 0,
                                9, 9, true);
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
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set downloader listener
         * @param listener
         */
        public override void SetMapDownloadingListener(IReaderListener listener)
        {
            base.SetMapDownloadingListener(listener);
            _mapDirectionLayer.SetMapDownloadingListener(listener);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // --------   -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  This a map layer delicated to draw routing result.
         * <p></p>
         * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
         * @version     1.00, 03/01/09
         * @author      Guidebee, Inc.
         */

        private class MapDirectionLayer : MapLayer
        {

            private IReaderListener _readListener;

            public void SetMapDownloadingListener(IReaderListener listener)
            {
                _readListener = listener;
            }
            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * Constructor.
             * @param Width
             * @param Height
             */
            public MapDirectionLayer(int width, int height)
                : base(width, height)
            {

                _routeDrawWaypointOnly = MapConfiguration.DrawRouteWaypointOnly;

                _mapDrawingTileWidth = MAP_TILE_WIDTH / MapConfiguration.MapDirectionRenderBlocks;
                _routeImage = AbstractGraphicsFactory.CreateImage(MAP_TILE_WIDTH,
                        MAP_TILE_WIDTH);
                _routeGraphics = _routeImage.GetGraphics();
                if (!_routeDrawWaypointOnly)
                {
                    _routeGraphics2D = new Graphics2D(_mapDrawingTileWidth,
                        _mapDrawingTileWidth);
                }

                //int[] rgb = routeImage.GetRGB();
                //transparency = rgb[0];


            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * set the route direction.
             * @param newDirection
             */
            public void SetMapDirection(MapDirection newDirection)
            {
                _currentMapDirection = newDirection;
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * set the route direction.
             * @param newDirection
             */
            public MapDirection GetMapDirection()
            {
                return _currentMapDirection;
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * @inheritDoc
             */
            public override void Paint(IGraphics graphics)
            {
                Paint(graphics, 0, 0);
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * @inheritDoc
             */
            public override void Paint(IGraphics graphics, int offsetX, int offsetY)
            {
                Paint(graphics, offsetX, offsetY, MAP_TILE_WIDTH, MAP_TILE_WIDTH);
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * @inheritDoc
             */

            private void Paint(IGraphics graphics, int offsetX, int offsetY,
                    int width, int height)
            {
                if (_currentMapDirection != null)
                {
                    lock (_syncObject)
                    {
                        DrawRouteCanvas(offsetX, offsetY, width, height);
                        graphics.DrawImage(_routeImage, offsetX, offsetY);
                    }


                }
            }

            internal GeoLatLngBounds _screenBounds;
            private readonly IImage _routeImage;
            private readonly IGraphics _routeGraphics;
            private readonly Graphics2D _routeGraphics2D;
            private volatile MapDirection _currentMapDirection;
            /**
             * SutherlandHodgman clip pline and region.
             */
            private SutherlandHodgman _sutherlandHodgman;

            /**
             * When draw the map tile, the default map tile Width is 64X64
             * using 64X64 istread of 256X256 mainly for saving memory usage in
             * memeory constrained devices. it can change to a bigger value if the
             * memory is not issue.
             */
            private readonly int _mapDrawingTileWidth;

            private readonly bool _routeDrawWaypointOnly;


            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * check if need to Show on map.
             * @param numLevel
             * @param zoomLevel
             * @return
             */
            private static int NeedShowLevel(int numLevel, int zoomLevel)
            {
                const int totalZoomLevel = 16;
                if (numLevel == 0) return 0;
                int mapGrade = (totalZoomLevel - zoomLevel) / numLevel - 1;
                return mapGrade;

            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             *
             * @param X
             * @param Y
             * @param Width
             * @param Height
             */
            private void DrawRouteCanvas(int x, int y,
                    int width, int height)
            {
                if (_currentMapDirection != null)
                {
                    try
                    {
                        DrawRouteImage(_currentMapDirection, x, y, width, height);
                        DrawRouteIcons(_currentMapDirection, x, y, width, height);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             *
             * @param mapDirection
             * @param X
             * @param Y
             * @param Width
             * @param Height
             */
            private void DrawRouteImage(MapDirection mapDirection, int x, int y,
                    int width, int height)
            {
                if (!_routeDrawWaypointOnly)
                {
                    _sutherlandHodgman = new SutherlandHodgman(_screenBounds);

                    ArrayList polyline = new ArrayList();
                    int minLevel = NeedShowLevel(mapDirection.Polyline.NumLevels, GetZoom());
                    for (int i = 0; i < mapDirection.Polyline.GetVertexCount(); i++)
                    {
                        int level = mapDirection.Polyline.GetLevel(i);
                        if (level >= minLevel)
                        {
                            polyline.Add(mapDirection.Polyline.GetVertex(i));
                        }
                    }
                    ArrayList clippedPts = _sutherlandHodgman.ClipPline(polyline);

                    GeoPoint newPt1;
                    GeoPoint newPt2;

                    GeoPoint drawPt1 = new GeoPoint(0, 0), drawPt2 = new GeoPoint(0, 0);
                    const int steps = 1;
                    int numOfTiles = MAP_TILE_WIDTH / _mapDrawingTileWidth;
                    Rectangle drawArea = new Rectangle();
                    Rectangle intersectRect = new Rectangle(0, 0, width, height);
                    int xIndex;
                    for (xIndex = 0; xIndex < numOfTiles; xIndex++)
                    {
                        int yIndex;
                        for (yIndex = 0; yIndex < numOfTiles; yIndex++)
                        {
                            bool hasPt1 = false;
                            GeoLatLng pt1 = null;
                            _routeGraphics2D.Clear(Color.White);
                            drawArea.X = xIndex * _mapDrawingTileWidth;
                            drawArea.Y = yIndex * _mapDrawingTileWidth;
                            drawArea.Width = drawArea.Height = _mapDrawingTileWidth;
                            drawArea = intersectRect.Intersection(drawArea);
                            int totalPointSize = clippedPts.Count;
                            if (!drawArea.IsEmpty())
                            {
                                _routeGraphics2D.SetClip(0, 0,
                                        drawArea.Width, drawArea.Height);
                                try
                                {
                                    for (int j = 0; j < totalPointSize; j += steps)
                                    {
                                        GeoLatLng pt = (GeoLatLng)clippedPts[j];
                                        int level = minLevel;
                                        if (hasPt1 == false)
                                        {
                                            if (level >= minLevel)
                                            {
                                                {
                                                    {
                                                        hasPt1 = true;
                                                        pt1 = pt;
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                        if (hasPt1)
                                        {
                                            if (level >= minLevel)
                                            {
                                                GeoLatLng pt2 = pt;
                                                newPt1 = FromLatLngToMapPixel(pt1);
                                                newPt2 = FromLatLngToMapPixel(pt2);
                                                newPt1.X -= x + xIndex * _mapDrawingTileWidth;
                                                newPt1.Y -= y + yIndex * _mapDrawingTileWidth;
                                                newPt2.X -= x + xIndex * _mapDrawingTileWidth;
                                                newPt2.Y -= y + yIndex * _mapDrawingTileWidth;
                                                drawPt1.X = (int)newPt1.X;
                                                drawPt1.Y = (int)newPt1.Y;
                                                drawPt2.X = (int)newPt2.X;
                                                drawPt2.Y = (int)newPt2.Y;

                                                if ((drawPt1.Distance(drawPt2) > 0))
                                                {
                                                    _routeGraphics2D.DrawLine(RoutePen, (int)drawPt1.X,
                                                            (int)drawPt1.Y,
                                                            (int)drawPt2.X, (int)drawPt2.Y);
                                                    pt1 = pt2;
                                                    if (_readListener != null)
                                                    {
                                                        _readListener.readProgress(j,
                                                                totalPointSize);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }

                            _routeGraphics.DrawRGB(_routeGraphics2D.GetRGB(), 0,
                                    _mapDrawingTileWidth,
                                    xIndex * _mapDrawingTileWidth,
                                    yIndex * _mapDrawingTileWidth,
                                    _mapDrawingTileWidth,
                                    _mapDrawingTileWidth, true);
                        }
                    }
                }
                else
                {
                    _routeGraphics.SetColor(TRANSPARENCY);
                    _routeGraphics.FillRect(0, 0, MAP_TILE_WIDTH, MAP_TILE_WIDTH);
                }
            }

            ////////////////////////////////////////////////////////////////////////
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * draw route icon in given rectangle.
             * @param mapDirection
             * @param X
             * @param Y
             * @param Width
             * @param Height
             */
            private void DrawRouteIcons(MapDirection mapDirection, int x, int y,
                    int width, int height)
            {

                GeoPoint newPt;
                GeoLatLng pt;
                _routeGraphics.SetClip(0, 0, width, height);
                for (int i = 0; i < mapDirection.Routes[0].Steps.Length - 1; i++)
                {
                    MapStep mapStep = mapDirection.Routes[0].Steps[i];
                    pt = mapStep.LastLatLng;
                    if (_screenBounds.ContainsLatLng(pt))
                    {
                        newPt = FromLatLngToMapPixel(pt);
                        newPt.X -= x;
                        newPt.Y -= y;
                        _routeGraphics.DrawImage(MiddleIcon,
                                (int)newPt.X - MiddleIcon.GetWidth() / 2, (int)newPt.Y - MiddleIcon.GetHeight() / 2);
                    }
                }
                pt = mapDirection.Polyline.GetVertex(0);
                newPt = FromLatLngToMapPixel(pt);
                newPt.X -= x;
                newPt.Y -= y;
                _routeGraphics.DrawImage(StartIcon,
                        (int)newPt.X - StartIcon.GetWidth() / 2, (int)newPt.Y - StartIcon.GetHeight() / 2);
                pt = mapDirection.Polyline.GetVertex(mapDirection.Polyline.GetVertexCount() - 1);
                newPt = FromLatLngToMapPixel(pt);
                newPt.X -= x;
                newPt.Y -= y;
                _routeGraphics.DrawImage(EndIcon,
                        (int)newPt.X - EndIcon.GetWidth() / 2, (int)newPt.Y - EndIcon.GetHeight() / 2);


            }

            protected override void DrawMapCanvas()
            {
            }

        }

    }

}
