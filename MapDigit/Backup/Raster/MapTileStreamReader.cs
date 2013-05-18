//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 20JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.Collections;
using System.IO;
using MapDigit.GIS.Geometry;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 20JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  This class is used to store driving directions results
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 20/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapTileStreamReader : MapTileAbstractReader
    {


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Open the map.
         * @throws IOException if there's problem opening the map.
         */
        public void Open()
        {
            lock (_mapTiledZones)
            {
                int layerCount = _mapTiledZones.Count;
                if (layerCount > 0)
                {
                    ((MapTiledZone)_mapTiledZones[0]).Open();
                    _bounds = ((MapTiledZone)_mapTiledZones[0]).Bounds;
                }
                else
                {
                    _bounds = new GeoLatLngBounds();
                }
                for (int i = 1; i < layerCount; i++)
                {
                    MapTiledZone mapTiledZone = (MapTiledZone)_mapTiledZones[i];
                    mapTiledZone.Open();
                    GeoBounds.Union(mapTiledZone.Bounds, _bounds, _bounds);
                }
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a map zone to map's zone collection.
         * @param mapZone a map zone to Add.
         */
        public void AddZone(MapTiledZone mapZone)
        {
            lock (_mapTiledZones)
            {
                if (!_mapTiledZones.Contains(mapZone))
                {
                    mapZone._readListener = _readListener;
                    _mapTiledZones.Add(mapZone);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set downloader listener
         * @param listener
         */
        public override void SetMapDownloadingListener(IReaderListener listener)
        {
            base.SetMapDownloadingListener(listener);
            lock (_mapTiledZones)
            {
                for (int i = 0; i < _mapTiledZones.Count; i++)
                {
                    ((MapTiledZone)_mapTiledZones[i])._readListener
                            = listener;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return map zone object of given index.
         * @param index index of the map zone.
         * @return map zone object of given index.
         */
        public MapTiledZone GetMapZone(int index)
        {
            lock (_mapTiledZones)
            {
                MapTiledZone mapZone = null;
                if (index >= 0 && index < _mapTiledZones.Count)
                {
                    mapZone = (MapTiledZone)_mapTiledZones[index];
                }
                return mapZone;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the count of zones in the map.
         * @return the number of map zones in the map zone collection.
         */
        public int GetZoneCount()
        {
            lock (_mapTiledZones)
            {
                return _mapTiledZones.Count;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Inserts the specified map zone to map's zone collection at the
         * specified index. Each map zone in map's zone collection  with an index
         * greater or equal to the specified index is shifted upward to have an
         * index one greater than the value it had previously.
         * @param mapZone the map zone to insert.
         * @param index  where to insert the new map zone.
         */
        public void InsertZone(MapTiledZone mapZone, int index)
        {
            lock (_mapTiledZones)
            {
                if (!_mapTiledZones.Contains(mapZone))
                {
                    _mapTiledZones.Insert(index, mapZone);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Moves a zone in the Zone collection to change the order in which
         * zones are drawn.
         * @param from Index number of the zone to move. The topmost zone is 0.
         * @param to New location for the zone. For example, if you want it to be
         *  the second zone, use 1
         */
        public void MoveZone(int from, int to)
        {
            lock (_mapTiledZones)
            {
                if (from < 0 || from >= _mapTiledZones.Count ||
                        to < 0 || to >= _mapTiledZones.Count)
                {
                    return;
                }
                MapTiledZone mapZoneFrom = (MapTiledZone)_mapTiledZones[from];
                MapTiledZone mapZoneTo = (MapTiledZone)_mapTiledZones[to];
                _mapTiledZones[from] = mapZoneTo;
                _mapTiledZones[to] = mapZoneFrom;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove a map zone from map's zone collection.
         * @param mapZone map zone to be removed.
         */
        public void RemoveZone(MapTiledZone mapZone)
        {
            lock (_mapTiledZones)
            {
                if (_mapTiledZones.Contains(mapZone))
                {
                    _mapTiledZones.Remove(mapZone);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove all map zones from map's zone collection.
         */
        public void RemoveAllZones()
        {
            lock (_mapTiledZones)
            {
                _mapTiledZones.Clear();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map zone collection.
         * @return the map zone collection.
         */
        public ArrayList GetMapZones()
        {
            return _mapTiledZones;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override void GetImage(int mtype, int x, int y, int zoomLevel)
        {
            byte[] imgBuffer = null;
            try
            {
                lock (_mapTiledZones)
                {
                    int zoneCount = _mapTiledZones.Count;
                    for (int i = 0; i < zoneCount; i++)
                    {
                        MapTiledZone mapTiledZone
                                = (MapTiledZone)_mapTiledZones[i];
                        imgBuffer = mapTiledZone.GetImage(zoomLevel, x, y);
                        if (imgBuffer != null)
                        {
                            break;
                        }
                    }
                }

            }
            catch (IOException e)
            {

                //inglore the error
            }
            if (imgBuffer == null)
            {
                IsImagevalid = false;
                ImageArray = null;

            }
            else
            {
                ImageArray = imgBuffer;
                IsImagevalid = true;
                ImageArraySize = ImageArray.Length;
            }

        }

        /**
         * map zones object.
         */
        private readonly ArrayList _mapTiledZones = new ArrayList();
        /**
         * the boundary of this map.
         */
        private GeoLatLngBounds _bounds;


    }

}
