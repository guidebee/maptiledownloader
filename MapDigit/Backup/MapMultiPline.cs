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
     *  Class MapMultiPline stands for map plines¡¯ collection.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapMultiPline : MapObject
    {

        /**
         * the pen style of the pline.
         */
        public MapPen PenStyle;

        /**
         * the geo information for the pline object.
         */
        public GeoPolyline[] Plines;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param multiPline     map object copy from.
         */
        public MapMultiPline(MapMultiPline multiPline)
            : base(multiPline)
        {

            SetMapObjectType(MULTIPLINE);
            PenStyle = new MapPen(multiPline.PenStyle);
            Plines = new GeoPolyline[multiPline.Plines.Length];
            for (int i = 0; i < Plines.Length; i++)
            {
                Plines[i] = new GeoPolyline(multiPline.Plines[i]);
            }
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
        public MapMultiPline()
        {

            SetMapObjectType(MULTIPLINE);
            PenStyle = new MapPen();
            Plines = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the pen type of the map pline.
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
         * Get the GeoPolyline of the map MultiPline.
         * @return the GeoPolyline object.
         */
        public GeoPolyline[] GetPlines()
        {
            return Plines;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set GeoPolyline array of the map MultiPline.
         * @param plines  the GeoPolyline object array.
         */
        public void SetPlines(GeoPolyline[] plines)
        {
            Plines = plines;
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
            string retStr = "PLINE  MULTIPLE  ";
            retStr += Plines.Length + CRLF;
            for (int j = 0; j < Plines.Length; j++)
            {
                retStr += "  " + Plines[j].GetVertexCount() + CRLF;
                for (int i = 0; i < Plines[j].GetVertexCount(); i++)
                {
                    GeoLatLng latLng = Plines[j].GetVertex(i);
                    retStr += latLng.X + " " + latLng.Y + CRLF;
                }
            }
            retStr += "\t" + "PEN(" + PenStyle.Width + "," + PenStyle.Pattern + ","
                    + PenStyle.Color + ")" + CRLF;
            return retStr;
        }
    }

}
