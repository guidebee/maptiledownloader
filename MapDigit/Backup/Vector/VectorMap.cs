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
     * VectorMap is the basic building blocks for Guidebee local map. Each map is
     * consists of multiple map Layers.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class VectorMap : RasterMap
    {

        private GeoSet _geoSet;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         * @param Width the Width of the map.
         * @param Height the Height of the map.
         * @param mapTileDownloadManager map download manager.
         * @param geoSet geoset instance.
         * @throws InvalidLicenceException
         */
        public VectorMap(int width, int height,
                MapTileDownloadManager mapTileDownloadManager, GeoSet geoSet)
            : base(width, height, mapTileDownloadManager)
        {

            _geoSet = geoSet;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set a new geoset for this vector map.
         * @param geoSet a new geoset.
         */
        public void SetGeoSet(GeoSet geoSet)
        {
            _geoSet = geoSet;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the map type.
         * @return the map type, always MapType.MAPINFOVECTORMAP.
         */
        public override int GetMapType()
        {
            return MapType.MAPINFOVECTORMAP;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map objects in the screen area whose center is given point
         * @return the map ojectes in the screen area.
         * @
         */
        public Hashtable[] GetScreenObjects()
        {
            return GetScreenObjects(_mapCenterPt);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////

        /**
         * Get the map objects in the screen area whose center is given point
         * @param pt center of the screen.
         * @return the map objects in the screen area.
         * @
         */
        public Hashtable[] GetScreenObjects(GeoLatLng pt)
        {
            _mapCenterPt.X = pt.X;
            _mapCenterPt.Y = pt.Y;
            GeoLatLngBounds rectGeo = GetScreenBounds(_mapCenterPt);
            return _geoSet.Search(rectGeo);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a map layer to map's layer collection.
         * @param mapLayer a layer to be added.
         */
        public void AddMapFeatureLayer(MapFeatureLayer mapLayer)
        {
            if (_geoSet != null)
            {
                _geoSet.AddMapFeatureLayer(mapLayer);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the count of layers in the map.
         * @param index the index of the map layer.
         * @return the number of map layers in the map layer collection.
         */
        public MapFeatureLayer GetMapFeatureLayer(int index)
        {
            if (_geoSet != null)
            {
                _geoSet.GetMapFeatureLayer(index);
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
         * return the count of layers in the map.
         * @return the number of map layers in the map layer collection.
         */
        public int GetMapFeatureLayerCount()
        {
            if (_geoSet != null)
            {
                return _geoSet.GetMapFeatureLayerCount();
            }
            return 0;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Inserts the specified map layer to map's layer collection at the
         * specified index. Each map layer in map's layer collection  with an index
         * greater or equal to the specified index is shifted upward to have an
         * index one greater than the value it had previously.
         * @param mapLayer the map layer to insert.
         * @param index  where to insert the new map layer.
         */
        public void InsertMapFeatureLayer(MapFeatureLayer mapLayer, int index)
        {
            if (_geoSet != null)
            {
                _geoSet.InsertMapFeatureLayer(mapLayer, index);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Moves a layer in the Layer collection to change the order in which
         * layers are drawn.
         * @param from Index number of the layer to move. The topmost layer is 0.
         * @param to New location for the layer. For example, if you want it to be
         *  the second layer, use 1
         */
        public void MoveMapFeatureLayer(int from, int to)
        {
            if (_geoSet != null)
            {
                _geoSet.MoveMapFeatureLayer(from, to);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove a map layer from map's layer collection.
         * @param mapLayer map layer to be removed.
         */
        public void RemoveMapFeatureLayer(MapFeatureLayer mapLayer)
        {
            if (_geoSet != null)
            {
                _geoSet.RemoveMapFeatureLayer(mapLayer);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove all map layers from map's layer collection.
         */
        public void RemoveAllMapFeatureLayers()
        {
            if (_geoSet != null)
            {
                _geoSet.RemoveAllMapFeatureLayers();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map layer collection.
         * @return the map layer collection.
         */
        public MapFeatureLayer[] GetMapFeatureLayers()
        {
            if (_geoSet != null)
            {
                return _geoSet.GetMapFeatureLayers();
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
         * get all records based on given string. the seach will search based on
         * map layer's key field.
         * @param matchString
         * @return a hashtable array Contains of all matched record.
         *  the key is the mapInfo ID. the value is the matched string.
         * @
         */
        public Hashtable[] Search(string matchString)
        {
            if (_geoSet != null)
            {
                return _geoSet.Search(matchString);
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
         * get all records based on given rectangle.
         * @param rectGeo the boundary..
         * @return a hashtable array Contains of all matched record.
         *  the key is the mapInfo ID. the value is the MBR of map object.
         * @
         */
        public Hashtable[] Search(GeoLatLngBounds rectGeo)
        {
            if (_geoSet != null)
            {
                return _geoSet.Search(rectGeo);
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
         * get all records based on search condition  in give map layer.
         * @param index the index of given map layer.
         * @param findConditions the search condition.
         * @return a hashtable of all matched record.the key is the mapInfo ID.
         * @
         */
        public Hashtable Search(int index, FindConditions findConditions)
        {

            if (_geoSet != null)
            {
                return _geoSet.Search(index, findConditions);
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
         * get all records based on given rectangle in give map layer.
         * @param index the index of given map layer.
         * @param rectGeo the boundary..
         * @return a hashtable of all matched record.the key is the mapInfo ID.
         * @
         */
        public Hashtable Search(int index, GeoLatLngBounds rectGeo)
        {

            if (_geoSet != null)
            {
                return _geoSet.Search(index, rectGeo);
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
         * return the bound rect of the geoset.
         * @return the bound rect of the geoset.
         */
        public GeoLatLngBounds GetBounds()
        {
            if (_geoSet != null)
            {
                return _geoSet.GetBounds();
            }
            return new GeoLatLngBounds();
        }

    }

}
