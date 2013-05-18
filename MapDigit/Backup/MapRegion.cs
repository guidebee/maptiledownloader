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
using MapDigit.GIS.Geometry;

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
     *  Class MapRegion stands for a map region object.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapRegion : MapObject
    {

        /**
         * the pen style of the region.
         */
        public MapPen PenStyle;

        /**
         * the brush style of the region.
         */
        public MapBrush BrushStyle;

        /**
         * the geo information for the region object.
         */
        public GeoPolygon Region;

        /**
         * the center of the region.
         */
        public GeoLatLng CenterPt;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param region     map object copy from.
         */
        public MapRegion(MapRegion region)
            : base(region)
        {

            SetMapObjectType(REGION);
            PenStyle = new MapPen(region.PenStyle);
            BrushStyle = new MapBrush(region.BrushStyle);
            Region = new GeoPolygon(region.Region);
            CenterPt = new GeoLatLng(region.CenterPt);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public MapRegion()
        {

            SetMapObjectType(REGION);
            PenStyle = new MapPen();
            BrushStyle = new MapBrush();
            CenterPt = new GeoLatLng();
            Region = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the pen type of the map region.
         * @return the pen type
         */
        public MapPen GetPenType()
        {
            return PenStyle;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the pen type of the map point.
         * @param mapPen the pen type
         */
        public void SetPenType(MapPen mapPen)
        {
            PenStyle = mapPen;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the brush type of the map region.
         * @return the brush type
         */
        public MapBrush GetBrushType()
        {
            return BrushStyle;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the brush type of the map region.
         * @param mapBrush the brush type
         */
        public void SetPenType(MapBrush mapBrush)
        {
            BrushStyle = mapBrush;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the GeoPolygon of the map Region.
         * @return the GeoPolygon object.
         */
        public GeoPolygon GetRegion()
        {
            return Region;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set GeoPolygon of the map Region.
         * @param region  the GeoPolygon object.
         */
        public void SetRegion(GeoPolygon region)
        {
            Region = region;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert to MapInfo string.
         * @return a MapInfo MIF string.
         */
        public override string ToString()
        {
            string retStr = "REGION 1" + CRLF;
            retStr += "\t" + Region.GetVertexCount() + CRLF;
            for (int i = 0; i < Region.GetVertexCount(); i++)
            {
                GeoLatLng latLng = Region.GetVertex(i);
                retStr += latLng.X + " " + latLng.Y + CRLF;
            }
            retStr += "\t" + "PEN(" + PenStyle.Width + "," + PenStyle.Pattern + ","
                    + PenStyle.Color + ")" + CRLF;
            retStr += "\t" + "BRUSH(" + BrushStyle.Pattern + "," + BrushStyle.ForeColor + ","
                    + BrushStyle.BackColor + ")" + CRLF;
            retStr += "\tCENTER " + CenterPt.X + " " + CenterPt.Y + CRLF;
            return retStr;
        }
    }

}
