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
using System;
using System.Net;
using MapDigit.AJAX;

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
     * query reverse geo coding.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 20/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class IpAddressGeocoder : IIpAddressGeocoder
    {

        private const string SEARCH_BASE = "http://www.mapdigit.com/ipaddress.aspx";
        internal IIpAddressGeocodingListener _listener;
        internal string _searchAddress;
        internal AddressQuery _addressQuery;
        public const string IP_NOT_FOUND = "IP_NOT_FOUND";

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public IpAddressGeocoder()
        {
            _addressQuery = new AddressQuery();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sends a request to Google servers to geocode the specified address
         * @param ipAddress  address to query
         * @param listener callback when query is done.
         */
        public void GetLocations(string ipAddress, IIpAddressGeocodingListener listener)
        {
            _listener = listener;
            _searchAddress = ipAddress;
            Request.Get(SEARCH_BASE, null, null, _addressQuery, this);

        }
    }

    internal class AddressQuery : IRequestListener
    {
        private static void SearchResponse(IpAddressGeocoder geoCoder, Response response)
        {
            IpAddressLocation ipAddressLocation = null;
            Exception ex = response.GetException();
            if (ex != null || response.GetCode() != HttpStatusCode.OK)
            {

                if (geoCoder._listener != null)
                {
                    geoCoder._listener.Done(geoCoder._searchAddress, null);
                }
                return;
            }
            try
            {
                Result result = response.GetResult();
                ipAddressLocation = new IpAddressLocation
                                        {
                                            ipaddress = result.GetAsString("ipaddress"),
                                            country = result.GetAsString("country"),
                                            region = result.GetAsString("region"),
                                            city = result.GetAsString("city"),
                                            postal = result.GetAsString("postal"),
                                            latitude = result.GetAsString("latitude"),
                                            longitude = result.GetAsString("longitude"),
                                            metro_code = result.GetAsString("metro_code"),
                                            area_code = result.GetAsString("area_code"),
                                            isp = result.GetAsString("isp"),
                                            organization = result.GetAsString("organization"),
                                            error = result.GetAsString("error")
                                        };
            }
            catch (Exception)
            {


            }
            if (geoCoder._listener != null)
            {
                geoCoder._listener.Done(geoCoder._searchAddress, ipAddressLocation);
            }

        }

        public void ReadProgress(Object context, int bytes, int total)
        {
            IpAddressGeocoder geoCoder = (IpAddressGeocoder)context;
            geoCoder._listener.ReadProgress(bytes, total);
        }

        public void WriteProgress(Object context, int bytes, int total)
        {
        }

        public void Done(Object context, Response response)
        {
            IpAddressGeocoder geoCoder = (IpAddressGeocoder)context;
            SearchResponse(geoCoder, response);
        }

        public void Done(Object context, string rawResult)
        {

        }
    }

}
