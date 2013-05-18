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
using System.IO;
using MapDigit.GIS.Geometry;

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
     * MapLayer defines a map layer.Computer maps are organized into layers. Think 
     * of the layers as transparencies that are stacked on top of one another. Each
     * layer Contains different aspects of the entire map. Each layer Contains 
     * different map objects, such as regions, points, lines and text.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapFeatureLayer
    {

        /**
         *  Predominant feature type is Point.
         */
        public const int FEATURE_TYPE_POINT = MapObject.POINT;

        /**
         *  Predominant feature type is polyline.
         */
        public const int FEATURE_TYPE_PLINE = MapObject.PLINE;

        /**
         *  Predominant feature type is region.
         */
        public const int FEATURE_TYPE_REGION = MapObject.REGION;

        /**
         * This property controls whether a layer is automatically labeled. In 
         * order for a label to be displayed automatically, its centroid must be 
         * within the viewable map area. This is a Boolean value, and its default
         * is true.
         */
        public bool AutoLabel = true;

        /**
         * font color.
         */
        public int FontColor;

        public string FontName;

        public string LayerName;

        /**
         * A Rectangle object representing the geographic extents 
         * (i.e., the minimum bounding rectangle) of all objects in the layer.
         */
        public GeoLatLngBounds Bounds;

        /**
         * The tabular data table object associated with this map layer.
         */
        public DataTable DataTable;

        /**
         * This string property identifies the column (field) name in the layer's
         * tabular table that will be the name property of a feature object. 
         * It currently defaults to the first column in the layer's table or the 
         * column with name as "Name" if there is any.
         */
        public DataField KeyField;

        /**
         * Predominant feature type. can be POINT,PLINE or REGION.
         */
        public int PredominantFeatureType;

        /**
         * Is this map layer visible.
         */
        public bool Visible = true;

        /**
         * Description for this mapLayer.
         */
        public string Description = "";

        /**
         * This controls whether the layer is zoom layered. Zoom layering controls 
         * the range of zoom levels (Distance across map) for which the layer is 
         * displayed. If Zoom Layering is on, then the values stored in the zoomMax 
         * and zoomMin properties are used. This is a Boolean value, and the default
         * is false.
         */
        public bool ZoomLevel;

        /**
         * If ZoomLayering is on (zoomLevel=true), then this specifies 
         * the maximum zoom value for which a layer will be drawn on the map.
         * This takes a double value specifying Distance in Map units (Map.mapUnit).
         */
        public double ZoomMax;

        /**
         * If ZoomLayering is on (zoomLevel=true), then this specifies 
         * the minimum zoom value for which a layer will be drawn on the map. 
         * This takes a double value specifying Distance in Map units (Map.MapUnit).
         */
        public double ZoomMin;


        /**
         * the index of the key field.
         */
        private int _keyFieldIndex;

        /**
         * the map file related to this map layer.
         */
        private readonly MapFile.MapFile _mapFile;

        private volatile bool _opened;
        /**
         * MapObject internal cache.
         */
        private readonly Hashtable _mapObjectCache = new Hashtable(CACHE_SIZE);

        /**
         * the internal cache size
         */
        private const int CACHE_SIZE = 256;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param reader  the input stream of the map file.
         */
        public MapFeatureLayer(BinaryReader reader)
        {
            _mapFile = new MapFile.MapFile(reader);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Open the map layer.
         */
        public void Open()
        {
            if (!_opened)
            {
                _opened = true;
                _mapFile.Open();
                DataTable = new DataTable(_mapFile.TabularData, _mapFile.Header.Fields,
                        _mapFile.Header.RecordCount);
                int foundName = -1;
                for (int i = 0; i < _mapFile.Header.Fields.Length; i++)
                {
                    KeyField = _mapFile.Header.Fields[i];
                    if (KeyField.GetName().ToLower().StartsWith("name"))
                    {
                        foundName = i;
                        _keyFieldIndex = i;
                        break;
                    }
                }
                if (foundName == -1)
                {
                    for (int i = 0; i < _mapFile.Header.Fields.Length; i++)
                    {
                        KeyField = _mapFile.Header.Fields[i];
                        if (KeyField.GetFieldType() == DataField.TYPE_CHAR)
                        {
                            foundName = i;
                            _keyFieldIndex = i;
                            break;
                        }
                    }
                }
                if (foundName == -1)
                {
                    KeyField = _mapFile.Header.Fields[0];
                    _keyFieldIndex = 0;
                }
                if (_mapFile.Header.DominantType.ToUpper().Equals("POINT"))
                {
                    PredominantFeatureType = FEATURE_TYPE_POINT;
                }
                else if (_mapFile.Header.DominantType.ToUpper().Equals("PLINE"))
                {
                    PredominantFeatureType = FEATURE_TYPE_PLINE;
                }
                else
                {
                    PredominantFeatureType = FEATURE_TYPE_REGION;
                }
                Bounds = _mapFile.Header.MapBounds;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Close the map layer.
         */
        public void Close()
        {
            if (_opened)
            {
                _mapFile.Close();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get MapFeature at given mapInfoID.
         * @param mapInfoID the index of the record(MapInfoID).
         * @return MapFeature at given mapInfoID.
         */
        public MapFeature GetMapFeatureByID(int mapInfoID)
        {
            MapFeature mapFeature;
            int mapObjectIDKey = mapInfoID;

            if (_mapObjectCache.ContainsKey(mapObjectIDKey))
            {
                mapFeature = (MapFeature)_mapObjectCache[mapObjectIDKey];
            }
            else
            {
                mapFeature = new MapFeature();
                mapFeature.MapInfoID = mapInfoID;
                mapFeature.MapObject = GetMapObjectByID(mapInfoID);
                mapFeature.DataRowValue = GetDataRowValueByID(mapInfoID);
                mapFeature.MapObject.Name =
                        mapFeature.DataRowValue.GetString(_keyFieldIndex);
                StoreToCache(mapObjectIDKey, mapFeature);
            }

            return mapFeature;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get MapObject at given mapInfoID.
         * @param mapInfoID the index of the record(MapInfoID).
         * @return MapObject at given mapInfoID.
         */
        public MapObject GetMapObjectByID(int mapInfoID)
        {
            return _mapFile.GetMapObject(mapInfoID);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get tabular record at given mapInfo ID.
         * @param mapInfoID the index of the record(MapInfoID).
         * @return tabular record at given mapInfoID.
         */
        public DataRowValue GetDataRowValueByID(int mapInfoID)
        {
            return _mapFile.GetDataRowValue(mapInfoID);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get all records based on search condition.
         * @param findConditions the search condition.
         * @return a hashtable of all matched record.the key is the mapInfo ID. the
         *  value is the matched string.
         */
        public Hashtable Search(FindConditions findConditions)
        {
            return _mapFile.Search(findConditions);
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
         * @return a hashtable of all matched record.the key is the mapInfo ID. the
         * value is the matched MapObject's MBR.
         */
        public Hashtable Search(GeoLatLngBounds rectGeo)
        {
            return _mapFile.SearchMapObjectsInRect(rectGeo);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the total record number.
         * @return the total record number.
         */
        public int GetRecordCount()
        {
            return _mapFile.GetRecordCount();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this map player can be shown.
         * @param screenWidthDistance the Distance of the screen Width (in same unit
         * as the map layer(km or mile).
         */
        internal bool CanBeShown(double screenWidthDistance)
        {
            bool isShown = Visible;
            if (ZoomLevel)
            {
                if (!(screenWidthDistance >= ZoomMin &&
                        screenWidthDistance <= ZoomMax))
                {
                    isShown = false;
                }
            }
            return isShown;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Store the mapFeature to cache.
         * @param mapObjectIDKey the mapInfo ID key.
         * @param mapFeature the map feature to be cached.
         */
        private void StoreToCache(int mapObjectIDKey, MapFeature mapFeature)
        {
            if (_mapObjectCache.Count >= CACHE_SIZE)
            {
                MapFeature[] mapFeatures = new MapFeature[CACHE_SIZE];
                ICollection enuValues = _mapObjectCache.Values;
                int index = 0;
                foreach (var o in enuValues)
                {
                    mapFeatures[index++] = (MapFeature)o;
                }

                SortMapFeature(mapFeatures);
                for (int i = 0; i < CACHE_SIZE / 2; i++)
                {
                    int deleteMapObjectID = mapFeatures[i].MapInfoID;
                    _mapObjectCache.Remove(deleteMapObjectID);
                }

            }
            _mapObjectCache.Add(mapObjectIDKey, mapFeature);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * sort the map feature array.
         */
        private static void SortMapFeature(MapFeature[] mapFeatures)
        {
            int n = mapFeatures.Length;
            int i, j;
            MapFeature ai;

            for (i = 1; i < n; i++)
            {
                j = i - 1;
                ai = mapFeatures[i];
                while (mapFeatures[j].MapObject.CacheAccessTime >
                        ai.MapObject.CacheAccessTime)
                {
                    mapFeatures[j + 1] = mapFeatures[j];
                    j--;
                    if (j < 0) break;
                }
                mapFeatures[j + 1] = ai;
            }
        }
    }

}
