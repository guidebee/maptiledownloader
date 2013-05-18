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
     * Polyline on map.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GeoPolyline
    {

        /**
         * Zoom factor
         */
        public int ZoomFactor;
        /**
         * total zoom level, default 18
         */
        public int NumLevels;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public GeoPolyline()
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
         * @param pline pline object copied from.
         */
        public GeoPolyline(GeoPolyline pline)
        {
            if (pline._latlngs != null)
            {
                _latlngs = new GeoLatLng[pline._latlngs.Length];
                Array.Copy(_latlngs, 0, _latlngs, 0, _latlngs.Length);
                _levels = new int[pline._levels.Length];
                for (int i = 0; i < _levels.Length; i++)
                {
                    _levels[i] = pline._levels[i];
                }
                _bounds = new GeoLatLngBounds(pline._bounds);
            }
            _color = pline._color;
            _weight = pline._weight;
            _opacity = pline._opacity;
            ZoomFactor = pline.ZoomFactor;
            NumLevels = pline.NumLevels;
            _visible = pline._visible;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a polyline from an array of vertices.  The _weight is the Width of
         * the line in pixels. The _opacity is given as a number between 0 and 1.
         * The line will be antialiased and semitransparent.
         * @param _latlngs array of points.
         * @param _color the _color of the polyline.
         * @param _weight the Width of the polyline.
         * @param _opacity the _opacity of the polyline.
         */
        public GeoPolyline(GeoLatLng[] latlngs, int color, int weight,
                double opacity)
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

                    // determin bounds (Max/Min Lat/lon)
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
                GeoLatLng sw = new GeoLatLng(minlat, minlon);
                GeoLatLng ne = new GeoLatLng(maxlat, maxlon);
                _bounds = new GeoLatLngBounds(sw, ne);
            }
            _color = color;
            _weight = weight;
            _opacity = opacity;
            ZoomFactor = 1;
            NumLevels = 0;
            _visible = true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates a polyline from encoded strings of aggregated points and levels.
         * ZoomFactor and  NumLevels  these two values determine the precision
         * of the levels within an encoded polyline.
         * @param _color the _color of the polyline.
         * @param _weight Width of the line in pixels.
         * @param _opacity the _opacity of the polyline.
         * @param points a string containing the encoded latitude and longitude
         *  coordinates.
         * @param ZoomFactor  the magnification between adjacent sets of zoom levels
         * in the encoded levels string.
         * @param levels a string containing the encoded polyline zoom level groups.
         * @param NumLevels the number of zoom levels contained in the encoded
         * levels string.
         * @return Geo polyline object.
         */
        public static GeoPolyline FromEncoded(int color, int weight, double opacity,
                String points, int zoomFactor, String levels, int numLevels)
        {
            ArrayList trk = PolylineEncoder.CreateDecodings(points);
            GeoLatLng[] array = new GeoLatLng[trk.Count];
            var temp = trk.ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                array[i] = (GeoLatLng)temp[i];
            }
            GeoPolyline polyline = new GeoPolyline(array, color, weight, opacity);
            polyline._levels = PolylineEncoder.DecodeLevel(levels);
            polyline.ZoomFactor = zoomFactor;
            polyline.NumLevels = numLevels;
            return polyline;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the number of vertices in the polyline.
         * @return  the number of vertices in the polyline.
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
         * Returns the vertex with the given index in the polyline.
         * @param index the index of the point.
         * @return  the vertex with the given index in the polyline.
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
         * Returns the level with the given index in the polyline.
         * @param index the index of the point.
         * @return  the level with the given index in the polyline.
         */
        public int GetLevel(int index)
        {
            if (_levels != null)
            {
                return _levels[index];
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
         * Returns the length (in meters) of the polyline along the surface of a
         * spherical Earth
         * @return  the length (in meters) of the polyline.
         */
        public int GetLength()
        {
            int len = 0;
            if (_latlngs != null)
            {
                double length = 0;
                GeoLatLng pt1 = new GeoLatLng(_latlngs[0].Lat(), _latlngs[0].Lng());
                for (int i = 1; i < _latlngs.Length; i++)
                {
                    GeoLatLng pt2 = new GeoLatLng(_latlngs[i].Lat(), _latlngs[i].Lng());
                    length += pt1.DistanceFrom(pt2);
                    pt1 = pt2;
                }
                len = (int)(length * 1000.0 + 0.5);
            }
            return len;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the bounds for this polyline.
         * @return  the bounds for this polyline.
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
         * Hides the polyline.
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
         * Shows the polyline.
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
         * Returns true if the polyline is currently hidden. Otherwise returns false.
         * @return true if the polyline is currently hidden.
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
         * Returns true if GeoPolyline.Hide() is supported
         * @return always true.
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
         * array store points in the polyline
         */
        private GeoLatLng[] _latlngs;
        /**
         * Color of the polyline
         */
        private readonly int _color;
        /**
         * Width of the polyline
         */
        private readonly int _weight;
        /**
         * Opacity of the polyline
         */
        private readonly double _opacity;

        /**
         * level for each point.
         */
        private int[] _levels;
        /**
         * the bounds of the polyline
         */
        private readonly GeoLatLngBounds _bounds;
        /**
         * visible or not
         */
        private bool _visible = true;
    }

}
