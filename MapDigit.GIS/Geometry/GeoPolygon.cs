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

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Geometry
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Polygon on map.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GeoPolygon
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public GeoPolygon()
        {
            _latlngs = null;
            _levels = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * copy constructor.
         * @param polygon polygon object copied from.
         */
        public GeoPolygon(GeoPolygon polygon)
        {
            if (polygon._latlngs != null)
            {
                _latlngs = new GeoLatLng[polygon._latlngs.Length];
                Array.Copy(_latlngs, 0, _latlngs, 0, _latlngs.Length);
                _levels = new int[polygon._levels.Length];
                for (int i = 0; i < _levels.Length; i++)
                {
                    _levels[i] = polygon._levels[i];
                }
                _bounds = new GeoLatLngBounds(polygon._bounds);
            }
            _strokeColor = polygon._strokeColor;
            _strokeOpacity = polygon._strokeOpacity;
            _strokeWeight = polygon._strokeWeight;
            _fillColor = polygon._fillColor;
            _fillOpacity = polygon._fillOpacity;
            _zoomFactor = polygon._zoomFactor;
            _numLevels = polygon._numLevels;
            _visible = polygon._visible;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a polygon from an array of vertices.  The weight is the Width of
         * the line in pixels. The opacity is given as a number between 0 and 1.
         * The line will be antialiased and semitransparent.
         * @param _latlngs array of points.
         * @param _strokeColor the color of the polygon stroke.
         * @param _strokeWeight the Width of the polygon stroke.
         * @param _strokeOpacity the opacity of the polygon stroke.
         * @param _fillColor the inner color of the polygon.
         * @param _fillOpacity the inner opacity of the polygon.
         */
        public GeoPolygon(GeoLatLng[] latlngs, int strokeColor, int strokeWeight,
                double strokeOpacity, int fillColor, double fillOpacity)
        {
            if (latlngs != null)
            {
                _latlngs = new GeoLatLng[latlngs.Length];
                Array.Copy(latlngs, 0, _latlngs, 0, latlngs.Length);
                _levels = new int[latlngs.Length];
                for (int i = 0; i < _levels.Length; i++)
                {
                    _levels[i] = 0;
                }
                double maxlat = 0, minlat = 0, maxlon = 0, minlon = 0;
                GeoLatLng[] points = _latlngs;
                for (int i = 0; i < points.Length; i++)
                {

                    // determin _bounds (Max/Min Lat/lon)
                    if (i == 0)
                    {
                        maxlat = minlat = points[i].Lat();
                        maxlon = minlon = points[i].Lng();
                    }
                    else
                    {
                        if (points[i].Lat() > maxlat)
                        {
                            maxlat = points[i].Lat();
                        }
                        else if (points[i].Lat() < minlat)
                        {
                            minlat = points[i].Lat();
                        }
                        else if (points[i].Lng() > maxlon)
                        {
                            maxlon = points[i].Lng();
                        }
                        else if (points[i].Lng() < minlon)
                        {
                            minlon = points[i].Lng();
                        }
                    }
                }
                GeoLatLng sw, ne;
                sw = new GeoLatLng(minlat, minlon);
                ne = new GeoLatLng(maxlat, maxlon);
                _bounds = new GeoLatLngBounds(sw, ne);
            }
            _strokeColor = strokeColor;
            _strokeOpacity = strokeOpacity;
            _strokeWeight = strokeWeight;
            _fillColor = fillColor;
            _fillOpacity = fillOpacity;
            _zoomFactor = 1;
            _numLevels = 0;
            _visible = true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a polygon from encoded strings of aggregated points and _levels.
         * _zoomFactor and  _numLevels  these two values determine the precision
         * of the _levels within an encoded polygon.
         * @param _strokeColor the color of the polygon.
         * @param _strokeWeight Width of the line in pixels.
         * @param _strokeOpacity the opacity of the polygon.
         * @param _fillColor the inner color of the polygon.
         * @param _fillOpacity the inner opacity of the polygon.
         * @param points a string containing the encoded latitude and longitude
         *  coordinates.
         * @param _zoomFactor  the magnification between adjacent sets of zoom _levels
         * in the encoded _levels string.
         * @param _levels a string containing the encoded polygon zoom level groups.
         * @param _numLevels the number of zoom _levels contained in the encoded _levels string.
         * @return Geo polygon object.
         */
        public static GeoPolygon FromEncoded(int strokeColor, int strokeWeight,
                double strokeOpacity, int fillColor, double fillOpacity,
                String points, int zoomFactor, String levels, int numLevels)
        {
            ArrayList trk = PolylineEncoder.CreateDecodings(points);
            GeoLatLng[] array = new GeoLatLng[trk.Count];
            var temp = trk.ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                array[i] = (GeoLatLng)temp[i];
            }
            GeoPolygon polygon = new GeoPolygon(array, strokeColor, strokeWeight, strokeOpacity,
                    fillColor, fillOpacity);
            polygon._levels = PolylineEncoder.DecodeLevel(levels);
            polygon._zoomFactor = zoomFactor;
            polygon._numLevels = numLevels;
            return polygon;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the number of vertices in the polygon.
         * @return  the number of vertices in the polygon.
         */
        public int GetVertexCount()
        {
            if (_latlngs != null)
            {
                return _latlngs.Length;
            }
            return 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the vertex with the given index in the polygon.
         * @param index the index of the point.
         * @return  the vertex with the given index in the polygon.
         */
        public GeoLatLng GetVertex(int index)
        {
            if (_latlngs != null)
            {
                return _latlngs[index];
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
         * Returns the _bounds for this polygon. 
         * @return  the _bounds for this polygon. 
         */
        public GeoLatLngBounds GetBounds()
        {
            return _bounds;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Hides the polygon. 
         */
        public void Hide()
        {
            _visible = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Shows the polygon. 
         */
        public void Show()
        {
            _visible = true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if the polygon is currently hidden. Otherwise returns false.
         * @return true if the polygon is currently hidden.
         */
        public bool IsHidden()
        {
            return !_visible;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if GeoPolygon.Hide() is supported 
         * @return always is true.
         */
        public bool SupportsHide()
        {
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the array of points which consist of the line.
         * @param points array of points
         */
        public void SetPoints(GeoLatLng[] points)
        {
            _latlngs = points;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the array of points which consist of the line.
         * @return the points stored in the line
         */
        public GeoLatLng[] GetPoints()
        {
            return _latlngs;
        }

        /**
         * array store points in the polygon
         */
        private GeoLatLng[] _latlngs;

        /**
         * stroke color of the polygon
         */
        private readonly int _strokeColor;

        /**
         * stroke Width of the polygon
         */
        private readonly int _strokeWeight;

        /**
         * stroke opacity of the polygon
         */
        private readonly double _strokeOpacity;

        /**
         * fill color
         */
        private readonly int _fillColor;

        /**
         * fill opacity of the polygon
         */
        private readonly double _fillOpacity;

        /**
         * Zoom factor
         */
        private int _zoomFactor;

        /**
         * total zoom level, default 18
         */
        private int _numLevels;

        /**
         * level for each point.
         */
        private int[] _levels;

        /**
         * the _bounds of the polyline
         */
        private readonly GeoLatLngBounds _bounds;

        /**
         * _visible or not
         */
        private bool _visible = true;

    }

}
