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
using System.Collections;
using MapDigit.GIS.Geometry;
using MapDigit.GIS.Drawing;

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
     * MapLayerContainer is a collection of map layers.
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class MapLayerContainer : MapLayer
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add a map layer to the tail of the container.
         * @param mapLayer
         */
        public void AddMapLayer(MapLayer mapLayer)
        {
            lock (_mapLayers)
            {
                if (!_mapLayers.Contains(mapLayer))
                {
                    _mapLayers.Add(mapLayer);
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
         * Add a map layer after given index
         * @param index the index after which a new map layer is added
         * @param mapLayer a map layer inserted into the container
         */
        public void AddMapLayerAt(int index, MapLayer mapLayer)
        {
            lock (_mapLayers)
            {
                _mapLayers.Insert(index, mapLayer);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove all map layers from the container.
         */
        public void RemoveAllMapLayers()
        {
            lock (_mapLayers)
            {
                _mapLayers.Clear();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * remove a map layer at given index
         * @param index the index of the map layer.
         */
        public void RemoveMapLayerAt(int index)
        {
            lock (_mapLayers)
            {
                _mapLayers.RemoveAt(index);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * remove a givne map layer from the container.
         * @param mapLayer
         */
        public void RemoveMapLayer(MapLayer mapLayer)
        {
            lock (_mapLayers)
            {
                _mapLayers.Remove(mapLayer);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get a map layer at given index.
         * @param index the index of the map layer
         * @return the map layer at given index.
         */
        public MapLayer GetMapLayerAt(int index)
        {
            lock (_mapLayers)
            {
                return (MapLayer)_mapLayers[index];
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get all map layers as an array.
         * @return all map layers included in this container.
         */
        public MapLayer[] GetMapLayers()
        {
            lock (_mapLayers)
            {
                MapLayer[] retArray = new MapLayer[_mapLayers.Count];
                _mapLayers.CopyTo(retArray);
                return retArray;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the count of the map layers in this container.
         * @return the count of the map layers.
         */
        public int GetMapLayerCount()
        {
            lock (_mapLayers)
            {
                return _mapLayers.Count;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param graphics the graphics object to paint.
         */
        public override void Paint(IGraphics graphics)
        {
            Paint(graphics, 0, 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param graphics the graphics object to paint.
         * @param offsetX
         * @param offsetY
         */
        public override void Paint(IGraphics graphics, int offsetX, int offsetY)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.Paint(graphics, offsetX, offsetY);
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
         * Starts a pan with given Distance in pixels.
         * directions. +1 is right and down, -1 is left and up, respectively.
         * @param dx X offset.
         * @param dy Y offset.
         */
        public override void PanDirection(int dx, int dy)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.PanDirection(dx, dy);
                }
            }
            base.PanDirection(dx, dy);
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
        public override void PanTo(GeoLatLng center)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.PanTo(center);
                }
            }
            base.PanTo(center);
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
        public override void SetCenter(GeoLatLng center, int zoomLevel)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.SetCenter(center, zoomLevel);
                }
            }
            base.SetCenter(center, zoomLevel);
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
        public override void ZoomIn()
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.ZoomIn();
                }
            }
            base.ZoomIn();
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
        public override void ZoomOut()
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.ZoomOut();
                }
            }
            base.ZoomOut();
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
        public override void SetZoom(int level)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.SetZoom(level);
                }
            }
            base.SetZoom(level);
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
        public override void Resize(GeoLatLngBounds bounds)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.Resize(bounds);
                }
            }
            base.Resize(bounds);
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
        public override void SetMapSize(int width, int height)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.SetMapSize(width, height);
                }
            }
            base.SetMapSize(width, height);
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
        public override void SetScreenSize(int width, int height)
        {
            lock (_mapLayers)
            {
                for (int i = 0; i < _mapLayers.Count; i++)
                {
                    MapLayer mapLayer = (MapLayer)_mapLayers[i];
                    mapLayer.SetScreenSize(width, height);
                }
            }
            base.SetScreenSize(width, height);
        }

        /**
         * ArrayList store all map layers.
         */
        protected ArrayList _mapLayers = new ArrayList();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ///////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param Width the Width of the map layer container.
         * @param Height the Height of the map layer container.
         */
        protected MapLayerContainer(int width, int height)
            : base(width, height)
        {

        }

    }

}
