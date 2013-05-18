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
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Service;
using MapDigit.GIS.Service.Google;

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
     * DigitalMap is the base class for VectorMap and RasterMap. It's an abstract
     * class provides all common functions for digtial maps.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class DigitalMap : MapLayerContainer
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set digital map service instance for this digital map.
         * @param digitalMapService an instance of the ditigal map service.
         */
        public void SetDigitalMapService(DigitalMapService digitalMapService)
        {
            _digitalMapService = digitalMapService;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the digital map service associcated with this map.
         * @return the digital map service instance.
         */
        public DigitalMapService GetDigitalMapService()
        {
            return _digitalMapService;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to get the direction
         * @param query  address to query
         */
        public void GetDirections(string query)
        {
            _digitalMapService.GetDirections(_mapType, query);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for geocoding query.
         * @param geocodingListener callback when query is done and in progress

         */
        public void SetGeocodingListener(IGeocodingListener geocodingListener)
        {
            _digitalMapService.SetGeocodingListener(geocodingListener);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for reverse geocoding query.
         * @param reverseGeocodingListener callback when query is done and in progress
         */
        public void SetReverseGeocodingListener(IReverseGeocodingListener
                reverseGeocodingListener)
        {
            _digitalMapService.SetReverseGeocodingListener(reverseGeocodingListener);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for geocoding query.
         * @param geocodingListener callback when query is done and in progress
         */
        public void SetIpAddressGeocodingListener(IIpAddressGeocodingListener
                geocodingListener)
        {
            _digitalMapService.SetIpAddressGeocodingListener(geocodingListener);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for direction query.
         * @param routingListener the routing listener
         */
        public void SetRoutingListener(IRoutingListener routingListener)
        {
            _digitalMapService.SetRoutingListener(routingListener);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param ipaddress  address to query
         */
        public void GetIpLocations(string ipaddress)
        {
            _digitalMapService.GetIpLocations(ipaddress);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param address  address to query
         */
        public void GetLocations(string address)
        {
            _digitalMapService.GetLocations(_mapType, address);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param latlngAddress  address to query
         */
        public void GetReverseLocations(string latlngAddress)
        {
            _digitalMapService.GetReverseLocations(_mapType, latlngAddress);
        }

        /**
         * the type of map.
         */
        protected int _mapType;

        /**
         * the map canvas
         */
        protected IImage _mapImage;

        /**
         * Graphics Object for map canvas.
         */
        protected IGraphics _mapGraphics;

        /**
         * Digital map service instance.
         */
        protected DigitalMapService _digitalMapService = new GoogleMapService();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new DigitalMap with given Width and Height.
         * @param Width the Width of the map image.
         * @param Height the Height of the map image.
         */
        protected DigitalMap(int width, int height)
            : base(width, height)
        {

            _mapImage = AbstractGraphicsFactory.CreateImage(width, height);
            _mapGraphics = _mapImage.GetGraphics();

        }
    }

}
