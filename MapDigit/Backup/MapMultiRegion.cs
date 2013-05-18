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
     *  Class MapMultiRegion stands for map regions' collection.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapMultiRegion : MapObject
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
        public GeoPolygon[] Regions;

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
         * @param multiRegion     map object copy from.
         */
        public MapMultiRegion(MapMultiRegion multiRegion)
            : base(multiRegion)
        {
            SetMapObjectType(MULTIREGION);
            PenStyle = new MapPen(multiRegion.PenStyle);
            BrushStyle = new MapBrush(multiRegion.BrushStyle);
            Regions = new GeoPolygon[multiRegion.Regions.Length];
            for (int i = 0; i < Regions.Length; i++)
            {
                Regions[i] = new GeoPolygon(multiRegion.Regions[i]);
            }
            CenterPt = new GeoLatLng(multiRegion.CenterPt);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public MapMultiRegion()
        {
            SetMapObjectType(MULTIREGION);
            PenStyle = new MapPen();
            BrushStyle = new MapBrush();
            CenterPt = new GeoLatLng();
            Regions = null;
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
        public GeoPolygon[] GetRegions()
        {
            return Regions;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set GeoPolygon array of the map Region.
         * @param regions  the GeoPolygon object array.
         */
        public void SetRegions(GeoPolygon[] regions)
        {
            Regions = regions;
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
            string retStr = "REGION ";
            retStr += Regions.Length + CRLF;
            for (int j = 0; j < Regions.Length; j++)
            {
                retStr += "  " + Regions[j].GetVertexCount() + CRLF;
                for (int i = 0; i < Regions[j].GetVertexCount(); i++)
                {
                    GeoLatLng latLng = Regions[j].GetVertex(i);
                    retStr += latLng.X + " " + latLng.Y + CRLF;
                }
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
