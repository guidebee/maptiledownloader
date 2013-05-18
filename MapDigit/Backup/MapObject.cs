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
     * Base class of all map objects.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class MapObject
    {

        /**
         * Unkown Object type.
         */
        public const int UNKOWN = -1;

        /**
         * None Object type.
         */
        public const int NONE = 0;

        /**
         * Point Object type.
         */
        public const int POINT = 1;

        /**
         * multi point Object type.
         */
        public const int MULTIPOINT = 2;

        /**
         * Pline Object type.
         */
        public const int PLINE = 3;

        /**
         * multi pline Object type.
         */
        public const int MULTIPLINE = 4;

        /**
         * region Object type.
         */
        public const int REGION = 5;


        /**
         * multi region Object type.
         */
        public const int MULTIREGION = 6;


        /**
         * Collection Object type.
         */
        public const int COLLECTION = 7;

        /**
         * text Object type.
         */
        public const int TEXT = 8;

        /**
         * ROAD Object type.
         */
        public const int ROAD = 15;

        /**
         * Direction Object type.
         */
        public const int DIRECTION = 16;

        /**
         * Route Object type.
         */
        public const int ROUTE = 17;

        /**
         * route step Object type.
         */
        public const int ROUTE_STEP = 18;

        /**
         * The MapInfo ID of the map object.
         */
        public int MapInfoID;

        /**
         * The name of the map object.
         */
        public string Name;

        /**
         * The out bound of the map object.
         */
        public GeoLatLngBounds Bounds;

        /**
         * The type of the map object.
         */
        protected int _mapObjectType;

        /**
         * The Time for cache
         */
        public DateTime CacheAccessTime;

        /**
         * indicate Highlighted or not
         */
        public bool Highlighted;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param mapObject map object copy from.
         */
        protected MapObject(MapObject mapObject)
        {
            MapInfoID = mapObject.MapInfoID;
            Name = mapObject.Name;
            Bounds = new GeoLatLngBounds(mapObject.Bounds);
            _mapObjectType = mapObject._mapObjectType;
            CacheAccessTime = mapObject.CacheAccessTime;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Empty constuctor.
         */
        protected MapObject()
        {
            _mapObjectType = UNKOWN;
            MapInfoID = -1;
            Name = "Unknown";
            Bounds = new GeoLatLngBounds();
            CacheAccessTime = new DateTime();
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the mapinfo ID of the map object.
         * @return the mapinfo ID of the map object
         */
        public int GetMapInfoID()
        {
            return MapInfoID;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the mapinfo ID of the map object.
         *
         * @param id the mapinfo ID
         */
        public void SetMapInfoID(int id)
        {
            MapInfoID = id;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the name of the map object.
         * @return the name of the map object
         */
        public string GetName()
        {
            return Name;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the name of the map object.
         * @param name the name of the map object.
         */
        public void SetName(string name)
        {
            Name = name;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the out bound of the map object.
         * @return the out bound
         */
        public GeoLatLngBounds GetBound()
        {
            return Bounds;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the out bound of the map object.
         * @param bounds   the out bound
         */
        public void SetBound(GeoLatLngBounds bounds)
        {
            Bounds = new GeoLatLngBounds(bounds);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the out type of the map object.
         * @return the type
         */
        public int GetMapObjectType()
        {
            return _mapObjectType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the type of the map object.
         * @param type  the type of the map object.
         */
        public void SetMapObjectType(int type)
        {
            _mapObjectType = type;
        }

        /**
         * Carriage return.
         */
        protected const string CRLF = "\n";


    }

}
