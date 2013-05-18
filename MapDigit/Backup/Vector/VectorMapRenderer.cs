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
using System;
using System.Collections;
using System.Drawing;
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Geometry;
using MapDigit.GIS.Raster;

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
     *  Vector map render, each time, the renderer draw one map tile.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */ 
    public class VectorMapRenderer : MapTileAbstractReader
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constrcutor.
         * @param geoSet
         */
        public VectorMapRenderer(GeoSet geoSet)
        {
            _geoSet = geoSet;
            _vectorMapCanvas = new VectorMapNativeCanvas();

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set font color.
         * @param fontColor font color.
         */
        public void SetFontColor(int fontColor)
        {
            _vectorMapCanvas.SetFontColor(fontColor);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set font.
         * @param font new font.
         */
        public void SetFont(IFont font)
        {
            _vectorMapCanvas.SetFont(font);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set a new vectorMapCanvas instance.
         * @param vectorMapCanvas a new vectorMapCanvas instance.
         */
        public void SetVectorMapCanvas(VectorMapAbstractCanvas vectorMapCanvas)
        {
            if (vectorMapCanvas != null)
            {
                _vectorMapCanvas = vectorMapCanvas;
            }

        }

        private static IFont GetFont(string fontName)
        {
            Font font = new Font(fontName, 13);
            IFont newFont = MapLayer.GetAbstractGraphicsFactory().CreateFont(font);
            return newFont;
        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void GetImage(int mtype, int x, int y, int zoomLevel)
        {
            lock (VectorMapAbstractCanvas.GRAPHICS_MUTEX)
            {

                int shiftWidth = 32;
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
                double width =0.00;
                double height = 0.00;

                //width = width < 0.06 ? 0.06 : width;
                //height = height < 0.06 ? 0.06 : height;

                GeoLatLngBounds geoBounds = new GeoLatLngBounds(minX - width / 2.0, minY - height/2.0,
                        maxX - minX + width , maxY - minY + height);
 
                try
                {
                    Hashtable[] mapFeatures = _geoSet.Search(geoBounds);
                    int totalSize = 0;
                    for (int i = 0; i < mapFeatures.Length; i++)
                    {
                        Hashtable mapFeaturesInLayer = mapFeatures[i];
                        totalSize += mapFeaturesInLayer.Count;
                    }
                    totalSize += 1;
                    int mapObjectIndex = 0;
                    _vectorMapCanvas.ClearCanvas(0xffffff);

                    for (int i = 0; i < mapFeatures.Length; i++)
                    {
                        int zOrder = mapFeatures.Length - 1 - i;
                        Hashtable mapFeaturesInLayer = mapFeatures[zOrder];
                        ICollection enuKeys = mapFeaturesInLayer.Keys;
                        MapFeatureLayer mapLayer = _geoSet.GetMapFeatureLayer(zOrder);

                        foreach (var o in enuKeys)
                        {
                            int mapInfoID = (int)o;
                            MapFeature mapFeature = mapLayer
                                    .GetMapFeatureByID(mapInfoID);
                            mapObjectIndex++;
                            _vectorMapCanvas.SetFont(GetFont(mapLayer.FontName));
                            _vectorMapCanvas.SetFontColor(mapLayer.FontColor);
                            _vectorMapCanvas.DrawMapObject(mapFeature.MapObject,
                                    geoBounds, zoomLevel);
                            if (_readListener != null)
                            {
                                _readListener.readProgress(mapObjectIndex,
                                        totalSize);
                            }
                        }

                        
                    }
                    _vectorMapCanvas.DrawMapText();
                    ImageArray = PNGEncoder.GetPngrgb(MapLayer.MAP_TILE_WIDTH,
                                                         MapLayer.MAP_TILE_WIDTH,
                                                         _vectorMapCanvas.GetRGB());
                    ImageArraySize = ImageArray.Length;
                    IsImagevalid = true;

                    if (ImageArraySize == 1933)
                    {
                        ImageArray = null;
                        IsImagevalid = false;
                        ImageArraySize = 0;

                    }
                   
                    
                    if (_readListener != null)
                    {
                        _readListener.readProgress(totalSize, totalSize);
                    }
                }
                catch (Exception e )
                {

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
         * @inheritDoc
         */
        public override void CancelRead()
        {
        }

        /**
         * Geoset as the database for renderer.
         */
        private readonly GeoSet _geoSet;
        /**
         * Vector map canvas.
         */
        private VectorMapAbstractCanvas _vectorMapCanvas;
    }

}
