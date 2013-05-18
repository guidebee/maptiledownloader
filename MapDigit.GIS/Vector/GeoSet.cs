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
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Geometry;
using MapDigit.Util;

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
     *  This class is used to store driving directions results
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GeoSet
    {

        /**
         * map unit is in miles.
         */
        private const int MAPUNIT_MILE = 0;
        /**
         * map unit is in kilometer.
         */
        private const int MAPUNIT_KM = 1;
        /**
         * map unit.
         */
        private volatile int _mapUnit = MAPUNIT_KM;
        /**
         * default font Ex
         */
        public volatile IFont FontEx;
        /**
         * the backcolor for this map.
         */
        public volatile int BackColor = 0xffffff;



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default Constructor.
         */
        public GeoSet()
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param reader GeoSet input stream.
         * @
         */
        public GeoSet(BinaryReader reader)
        {
            ReadGeoSet(reader);
        }

     
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a map layer to map's layer collection.
         * @param mapLayer a map feature layer to be added.
         */
        public void AddMapFeatureLayer(MapFeatureLayer mapLayer)
        {
            lock (_mapFeatureLayers)
            {
                if (!_mapFeatureLayers.Contains(mapLayer))
                {
                    _mapFeatureLayers.Add(mapLayer);
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
         * return the count of layers in the map.
         * @param index the index of the map layer.
         * @return the number of map layers in the map layer collection.
         */
        public MapFeatureLayer GetMapFeatureLayer(int index)
        {
            lock (_mapFeatureLayers)
            {
                MapFeatureLayer mapLayer = null;
                if (index >= 0 && index < _mapFeatureLayers.Count)
                {
                    mapLayer = (MapFeatureLayer)_mapFeatureLayers[index];
                }
                return mapLayer;
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
         * @return the number of map layers in the map layer collection.
         */
        public int GetMapFeatureLayerCount()
        {
            lock (_mapFeatureLayers)
            {
                return _mapFeatureLayers.Count;
            }
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
            lock (_mapFeatureLayers)
            {
                if (!_mapFeatureLayers.Contains(mapLayer))
                {
                    _mapFeatureLayers.Insert(index, mapLayer);
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
         * Moves a layer in the Layer collection to change the order in which
         * layers are drawn.
         * @param from Index number of the layer to move. The topmost layer is 0.
         * @param to New location for the layer. For example, if you want it to be
         *  the second layer, use 1
         */
        public void MoveMapFeatureLayer(int from, int to)
        {
            lock (_mapFeatureLayers)
            {
                if (from < 0 || from >= _mapFeatureLayers.Count ||
                        to < 0 || to >= _mapFeatureLayers.Count)
                {
                    return;
                }
                MapFeatureLayer mapLayerFrom = (MapFeatureLayer)_mapFeatureLayers[from];
                MapFeatureLayer mapLayerTo = (MapFeatureLayer)_mapFeatureLayers[to];
                _mapFeatureLayers[from] = mapLayerTo;
                _mapFeatureLayers[to] = mapLayerFrom;
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
            lock (_mapFeatureLayers)
            {
                if (_mapFeatureLayers.Contains(mapLayer))
                {
                    _mapFeatureLayers.Remove(mapLayer);
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
         * Remove all map layers from map's layer collection.
         */
        public void RemoveAllMapFeatureLayers()
        {
            lock (_mapFeatureLayers)
            {
                _mapFeatureLayers.Clear();
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
            lock (_mapFeatureLayers)
            {
                if (_mapFeatureLayers.Count > 0)
                {
                    MapFeatureLayer[] copiedFeatureLayers = new MapFeatureLayer[_mapFeatureLayers.Count];
                    _mapFeatureLayers.CopyTo(copiedFeatureLayers);
                    return copiedFeatureLayers;
                }

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
            lock (_mapFeatureLayers)
            {
                Hashtable[] retTable = new Hashtable[_mapFeatureLayers.Count];
                for (int i = 0; i < _mapFeatureLayers.Count; i++)
                {
                    MapFeatureLayer mapLayer = (MapFeatureLayer)_mapFeatureLayers[i];
                    FindConditions findConditions = new FindConditions();
                    findConditions.AddCondition(mapLayer.DataTable.GetFieldIndex(mapLayer.KeyField.GetName()), matchString);
                    retTable[i] = mapLayer.Search(findConditions);
                }
                return retTable;
            }
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
            lock (_mapFeatureLayers)
            {
                Hashtable[] retTable = new Hashtable[_mapFeatureLayers.Count];
                GeoLatLng pt1 = new GeoLatLng(rectGeo.Y, rectGeo.X);
                GeoLatLng pt2 = new GeoLatLng(rectGeo.Y + rectGeo.Height,
                        rectGeo.X + rectGeo.Width);
                double distance = GeoLatLng.Distance(pt1, pt2);

                if (_mapUnit == MAPUNIT_MILE)
                {
                    distance /= 1.632;
                }

                for (int i = 0; i < _mapFeatureLayers.Count; i++)
                {
                    MapFeatureLayer mapLayer = (MapFeatureLayer)_mapFeatureLayers[i];
                    if (mapLayer.CanBeShown(distance))
                    {
                        retTable[i] = mapLayer.Search(rectGeo);
                    }
                    else
                    {
                        retTable[i] = new Hashtable();
                    }
                }
                return retTable;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Open the map.
         * @
         */
        public void Open()
        {
            lock (_mapFeatureLayers)
            {
                int layerCount = _mapFeatureLayers.Count;


                if (layerCount > 0)
                {
                    ((MapFeatureLayer)_mapFeatureLayers[0]).Open();
                    _bounds = ((MapFeatureLayer)_mapFeatureLayers[0]).Bounds;
                }
                else
                {
                    _bounds = new GeoLatLngBounds();
                }

                for (int i = 1; i < layerCount; i++)
                {
                    MapFeatureLayer mapLayer = (MapFeatureLayer)_mapFeatureLayers[i];
                    mapLayer.Open();
                    GeoBounds.Union(mapLayer.Bounds, _bounds, _bounds);
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
         * Close the map.
         * @
         */
        public void Close()
        {
            lock (_mapFeatureLayers)
            {
                int layerCount = _mapFeatureLayers.Count;
                for (int i = 0; i < layerCount; i++)
                {
                    MapFeatureLayer mapLayer = (MapFeatureLayer)_mapFeatureLayers[i];
                    mapLayer.Close();
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
         * get all records based on search condition  in give map layer.
         * @param index the index of given map layer.
         * @param findConditions the search condition.
         * @return a hashtable of all matched record.the key is the mapInfo ID.
         * @
         */
        public Hashtable Search(int index, FindConditions findConditions)
        {
            lock (_mapFeatureLayers)
            {
                MapFeatureLayer mapLayer = GetMapFeatureLayer(index);
                if (mapLayer != null)
                {
                    return mapLayer.Search(findConditions);
                }

                return null;
            }

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
            lock (_mapFeatureLayers)
            {
                MapFeatureLayer mapLayer = GetMapFeatureLayer(index);
                if (mapLayer != null)
                {
                    return mapLayer.Search(rectGeo);
                }
                return null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * read from the geoset file.
         */

        private void ReadGeoSet(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new IOException("can not read from null reader!");
            }

            _mapFeatureLayerInfos.Clear();
            

            DataReader.Seek(reader, 0);
            string fileVersion = DataReader.ReadString(reader);
            DataReader.Seek(reader, 16);
            string fileFormat = DataReader.ReadString(reader);
            DataReader.Seek(reader, 32);
            string pstType = DataReader.ReadString(reader);
            DataReader.Seek(reader, 48);
            if (!(fileFormat.ToUpper().Equals("JAVA") &&
                    pstType.ToUpper().Equals("PST")))
            {
                throw new IOException("Invalid file format!");
            }
            DataReader.ReadString(reader);
            DataReader.Seek(reader, 128);
            _mapUnit = DataReader.ReadInt(reader);
            DataReader.ReadDouble(reader);
            int mapLayerCount = DataReader.ReadInt(reader);

            for (int i = 0; i < mapLayerCount; i++)
            {
                DataReader.Seek(reader, (long)(i * 512 + 144));
                string layerName = DataReader.ReadString(reader);
                string description = DataReader.ReadString(reader);
                byte layerVisible = reader.ReadByte();
                double zoomMax = DataReader.ReadDouble(reader);
                double zoomMin = DataReader.ReadDouble(reader);
                MapFeatureLayerInfo mapLayerInfo = new MapFeatureLayerInfo();
                mapLayerInfo.Description = description;
                mapLayerInfo.LayerName = layerName;

                if (layerVisible == 1)
                {
                    mapLayerInfo.Visible = true;
                }
                else
                {
                    mapLayerInfo.Visible = false;
                }
                mapLayerInfo.ZoomMax = zoomMax;
                mapLayerInfo.ZoomMin = zoomMin;
                if (zoomMax == zoomMin && zoomMin < 0.001)
                {
                    mapLayerInfo.ZoomLevel = false;
                }
                else
                {
                    mapLayerInfo.ZoomLevel = true;
                }
                _mapFeatureLayerInfos.Add(mapLayerInfo);
            }
            reader.Close();
            if (_mapFeatureLayerInfos.Count > 0)
            {
                _layerNames = new string[_mapFeatureLayerInfos.Count];
                for (int i = 0; i < _mapFeatureLayerInfos.Count; i++)
                {
                    _layerNames[i] = ((MapFeatureLayerInfo)_mapFeatureLayerInfos[i]).LayerName;
                }
            }

        }

        public MapFeatureLayerInfo GetMapMapFeatureLayerInfo(string layerName)
        {
            foreach(MapFeatureLayerInfo info in _mapFeatureLayerInfos)
            {
                if(info.LayerName.CompareTo(layerName)==0)
                {
                    return info;
                }
            }
            return null;
        }

        public MapFeatureLayer GetMapMapFeatureLayer(string layerName)
        {
            foreach (MapFeatureLayer layer in _mapFeatureLayers)
            {
                if (layer.LayerName.CompareTo(layerName) == 0)
                {
                    return layer;
                }
            }
            return null;
        }

        public string []GetLayerNames()
        {
            string[] retString = new string[_layerNames.Length];
            System.Array.Copy(_layerNames, retString, retString.Length);
            return retString;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the bound rect of the geoset.
         * @return the bound rectangle of the geoset.
         */
        public GeoLatLngBounds GetBounds()
        {
            return new GeoLatLngBounds(_bounds);
        }
        /**
         * map Layers object.
         */
        private readonly ArrayList _mapFeatureLayers = new ArrayList();
        private readonly ArrayList _mapFeatureLayerInfos = new ArrayList();

        private string[] _layerNames;
        /**
         * the boundary of this map.
         */
        private GeoLatLngBounds _bounds;

        public class MapFeatureLayerInfo
        {

            public bool ZoomLevel;
            public bool Visible;
            public double ZoomMax;
            public double ZoomMin;
            public string LayerName;
            public string Description;
            public string FontName;
            public int FontColor;
        }
    }

}
