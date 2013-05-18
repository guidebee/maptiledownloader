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
     *  Class MapMultiPoint stands for a map points collection.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapMultiPoint : MapObject
    {

        /**
         * The symbol type of the map point.
         */
        public MapSymbol SymbolType;

        /**
         * The location of the map point.
         */
        public GeoLatLng[] Points;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param mapPoints     map object copy from.
         */
        public MapMultiPoint(MapMultiPoint mapPoints)
            : base(mapPoints)
        {
            SetMapObjectType(MULTIPOINT);
            SymbolType = new MapSymbol(mapPoints.SymbolType);
            Points = new GeoLatLng[mapPoints.Points.Length];
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new GeoLatLng(mapPoints.Points[i]);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Empty constructor.
         */
        public MapMultiPoint()
        {

            SetMapObjectType(MULTIPOINT);
            SymbolType = new MapSymbol();
            Points = null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the symbol type of the map point.
         * @return the symbol type
         */
        public MapSymbol GetSymbolType()
        {
            return SymbolType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the symbol type of the map point.
         * @param symbol the symbol type
         */
        public void SetSymbolType(MapSymbol symbol)
        {
            SymbolType = symbol;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the location of the map points.
         * @return the location array.
         */
        public GeoLatLng[] GetPoints()
        {
            return Points;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the location of the map points.
         * @param pts  the location
         */
        public void SetPoint(GeoLatLng[] pts)
        {
            Points = pts;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert to MapInfo string.
         * @return a MapInfo MIF string.
         */
        public override string ToString()
        {
            string retStr = "MULTIPOINT    ";
            retStr += Points.Length + CRLF;
            for (int i = 0; i < Points.Length; i++)
            {
                retStr += Points[i].X + " " + Points[i].Y + CRLF;
            }
            retStr += "\t" + "SYMBOL(" + SymbolType.Shape + "," + SymbolType.Color + ","
                    + SymbolType.Size + ")" + CRLF;
            return retStr;
        }
    }

}
