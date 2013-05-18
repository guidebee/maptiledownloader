//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 11JUL2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.Collections;
using MapDigit.GIS.Drawing;
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
     *  base class for draw vector map objects.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 11/07/09
     * @author      Guidebee, Inc.
     */
    public abstract class VectorMapAbstractCanvas
    {

        /**
         * Graphics2D mutex.
         */
        public readonly static object GRAPHICS_MUTEX = new object();

        /**
         * default font color.
         */
        protected int _fontColor;


        protected IFont _font;


        /**
         * current map zoom level
         */
        protected volatile int _mapZoomLevel = 1;

        /**
         * the center of this map.
         */
        protected volatile GeoLatLng _mapCenterPt = new GeoLatLng();


        /**
          * the size of the map size.
          */
        protected volatile GeoBounds _mapSize = new GeoBounds();

        /**
         * SutherlandHodgman clip pline and region.
         */
        protected SutherlandHodgman _sutherlandHodgman;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the pixel coordinates of the given geographical point in the map.
         * @param latlng the geographical coordinates.
         * @return the pixel coordinates in the map.
         */

        protected GeoPoint FromLatLngToMapPixel(GeoLatLng latlng)
        {
            GeoPoint center = MapLayer.FromLatLngToPixel(_mapCenterPt, _mapZoomLevel);
            GeoPoint topLeft = new GeoPoint(center.X - _mapSize.Width / 2.0,
                    center.Y - _mapSize.Height / 2.0);
            GeoPoint pointPos = MapLayer.FromLatLngToPixel(latlng, _mapZoomLevel);
            pointPos.X -= topLeft.X;
            pointPos.Y -= topLeft.Y;
            return new GeoPoint((int)(pointPos.X + 0.5), (int)(pointPos.Y + 0.5));

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the pixel coordinates of the given geographical point vector
         * in the map.
         * @param vpts the geographical coordinates vector.
         * @return the pixel coordinates in the map.
         */

        protected GeoPoint[] FromLatLngToMapPixel(ArrayList vpts)
        {

            GeoPoint[] retPoints = new GeoPoint[vpts.Count];
            for (int i = 0; i < vpts.Count; i++)
            {
                retPoints[i] = FromLatLngToMapPixel(
                        (GeoLatLng)vpts[i]);
            }
            return retPoints;

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the font color for this map canvas.
         * @param fontColor the font color.
         */
        public void SetFontColor(int fontColor)
        {
            _fontColor = fontColor;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the font for the map canvas.
         * @param font the font the map canvas.
         */
        public void SetFont(IFont font)
        {
            _font = font;
        }

        public abstract int[] GetRGB();

        public abstract void DrawMapObject(MapObject mapObject, GeoLatLngBounds drawBoundary,
                                  int zoomLevel);

        public abstract void DrawMapText();

        public abstract void ClearCanvas(int color);
    }
}
