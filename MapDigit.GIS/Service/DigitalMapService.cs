//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 20JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Service
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 20JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     *  This class is used to store driving directions results
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 20/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class DigitalMapService
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for geocoding query.
         * @param geocodingListener callback when query is done and in progress

         */
        public void SetGeocodingListener(IGeocodingListener geocodingListener)
        {
            _geocodingListener = geocodingListener;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for reverse geocoding query.
         * @param reverseGeocodingListener callback when query is done and in progress
         */
        public void SetReverseGeocodingListener(IReverseGeocodingListener reverseGeocodingListener)
        {
            _reverseGeocodingListener = reverseGeocodingListener;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for geocoding query.
         * @param geocodingListener callback when query is done and in progress

         */
        public void SetIpAddressGeocodingListener(IIpAddressGeocodingListener geocodingListener)
        {
            _ipAddressGeocodingListener = geocodingListener;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the listener for direction query.
         * @param routingListener the routing listener

         */
        public void SetRoutingListener(IRoutingListener routingListener)
        {
            _routingListener = routingListener;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to reverse geocode the specified address
         * @param latlngAddress  address to query
         */
        public void GetReverseLocations(string latlngAddress)
        {
            if (_reverseGeocodingListener != null)
            {
                _reverseGeocoder.GetLocations(latlngAddress,
                        _reverseGeocodingListener);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to reverse geocode the specified address
         * @param mapType map type.
         * @param latlngAddress  latitude,longitude string address.delimited by comma
         */
        public void GetReverseLocations(int mapType, string latlngAddress)
        {
            if (_reverseGeocodingListener != null)
            {
                _reverseGeocoder.GetLocations(mapType, latlngAddress,
                        _reverseGeocodingListener);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param address  address to query
         */
        public void GetLocations(string address)
        {
            if (_geocodingListener != null)
            {
                _geocoder.GetLocations(address, _geocodingListener);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param mapType the map type.
         * @param address  address to query
         */
        public void GetLocations(int mapType, string address)
        {
            if (_geocodingListener != null)
            {
                _geocoder.GetLocations(mapType, address, _geocodingListener);
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to geocode the specified address
         * @param address  address to query
         */
        public void GetIpLocations(string address)
        {
            if (_ipAddressGeocodingListener != null)
            {
                _ipAddressGeocoder.GetLocations(address, _ipAddressGeocodingListener);

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to get the direction
         * @param query  address to query
         */
        public void GetDirections(string query)
        {
            if (_routingListener != null)
            {
                _directionQuery.GetDirection(query, _routingListener);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to servers to get the direction
         * @param mapType the map type.
         * @param query  address to query
         */
        public void GetDirections(int mapType, string query)
        {
            if (_routingListener != null)
            {
                _directionQuery.GetDirection(mapType, query, _routingListener);
            }
        }

        protected IIpAddressGeocodingListener _ipAddressGeocodingListener;
        protected IGeocodingListener _geocodingListener;
        protected IReverseGeocodingListener _reverseGeocodingListener;
        protected IRoutingListener _routingListener;

        protected IGeocoder _geocoder;
        protected IReverseGeocoder _reverseGeocoder;
        protected IDirectionQuery _directionQuery;
        protected IIpAddressGeocoder _ipAddressGeocoder;


    }

}
