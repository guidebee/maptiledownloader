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
using MapDigit.GIS.Geometry;
using MapDigit.GIS.Raster;
using MapDigit.Util;

//--------------------------------- PACKAGE -----------------------------------
namespace MapDigit.GIS.Service.Google
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 20JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * This class is used to obtain driving directions results.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 20/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class GDirections : IDirectionQuery
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set google china or not.
         * @param china query china or not.
         */
        public void SetChina(bool china)
        {
            _isChina = china;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set google query key.
         * @param key google query key.
         */
        public void SetGoogleKey(string key)
        {
            _queryKey = key;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public GDirections()
        {
            _directionQuery = new DirectionQuery();
            _mapDirection = new MapDirection();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * This method issues a new directions query. The query parameter is
         * a string containing any valid directions query,
         * e.g. "from: Seattle to: San Francisco" or
         * "from: Toronto to: Ottawa to: New York".
         * @param query the directions query string
         * @param listener the routing listener.
         */
        public void GetDirection(string query, IRoutingListener listener)
        {

            _listener = listener;
            _routeQuery = query;
            if (!_isChina)
            {
                Arg[] args = {
                new Arg("q", query),
                new Arg("output", "js"),
                new Arg("oe", "utf8"),
                new Arg("key", _queryKey),
                null
            };
                Request.Get(SEARCH_BASE, args, null, _directionQuery, this);
            }
            else
            {
                Arg[] args = {
                new Arg("q", query),
                new Arg("output", "js"),
                new Arg("ie", "utf8"),
                new Arg("oe", "utf8"),
                null
            };
                Request.Get(SEARCH_BASE_CHINA, args, null, _directionQuery, this);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public void GetDirection(int mapType, string query, IRoutingListener listener)
        {
            _isChina = mapType == MapType.MICROSOFTCHINA ||
                    mapType == MapType.GOOGLECHINA || mapType == MapType.MAPABCCHINA;
            SetChina(_isChina);
            SetGoogleKey(GoogleQueryKeys[GoogleKeyIndex]);
            GetDirection(query, listener);
            GoogleKeyIndex++;
            GoogleKeyIndex %= 10;
        }

        private const string SEARCH_BASE = "http://maps.google.com/maps/nav";
        private const string SEARCH_BASE_CHINA = "http://ditu.google.cn/maps/nav";
        private string _queryKey = "ABQIAAAAi44TY0V29QjeejKd2l3ipRTRERdeAiwZ9EeJWta3L_JZVS0bOBQlextEji5FPvXs8mXtMbELsAFL0w";
        readonly DirectionQuery _directionQuery;
        readonly MapDirection _mapDirection;
        IRoutingListener _listener;
        string _routeQuery;
        bool _isChina;
        private static readonly string[] GoogleQueryKeys = {
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hRcdv_D3MipQG6beFVt4q3n2KstuxQVPGsK1seABGQPugXw_P7Iua0JYw",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hRr3VMBZ1cGe19qTgaCju5hrS8dIxSVwolc1mXM0pUIqSvJSNaW7jJUiA",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hRm_ifamDDETX3GYECVeBf43IL7kxQhowIvbl9G-Mq1Jo874g3vZbr9KA",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hTdoKf24hPXAkfeSPvoX63LdjNnwhTXeivbZPtE5W6vLnal3MgqR1Q4og",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hQHwqwnNik4w_uH95OtQPrGD8h2aRQkX34t6brsYYQjMh5Al7WxZC-uRQ",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hTxDsZgO1TyNw5Fb7lqwb1yrhjwjBRA87P_DQ_K07IWadLOQuyPYDfHIA",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hQma3cdF9cz-FT2e3x_QfYqxZ-lIBQLKb6_-IocP_EZaz6BpXiLhuD8fg",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hRm5GNFjZ8GN__mSLFDVmdMUufGqxTxofYdQZGsDgJOJ6_h-Q7HO4WF8w",
        "ABQIAAAAHxBdP31K2IukU7-aAA8n5hTEKgbPjtpwuJgXSRRhfbfuAHQlfRRdwtWTdkWiS7_AQmBiH4zhIHsUTQ",
        "ABQIAAAAi44TY0V29QjeejKd2l3ipRTRERdeAiwZ9EeJWta3L_JZVS0bOBQlextEji5FPvXs8mXtMbELsAFL0w"
    };
        private static int GoogleKeyIndex;

        class DirectionQuery : IRequestListener
        {
            readonly Html2Text _html2Text = new Html2Text();


            private void SearchResponse(GDirections gDirection, Response response)
            {

                Exception ex = response.GetException();
                if (ex != null || response.GetCode() != HttpStatusCode.OK)
                {

                    if (gDirection._listener != null)
                    {
                        gDirection._listener.Done(gDirection._routeQuery, null);
                    }
                    return;
                }
                try
                {
                    Result result = response.GetResult();
                    gDirection._mapDirection.Name = result.GetAsString("name");
                    gDirection._mapDirection.Status = result.GetAsInteger("Status.code");
                    gDirection._mapDirection.Duration = result.GetAsInteger("Directions.Duration.seconds");
                    gDirection._mapDirection.Distance = result.GetAsInteger("Directions.Distance.meters");
                    gDirection._mapDirection.Summary = _html2Text.Convert(result.GetAsString("Directions.summaryHtml"));
                    string points = result.GetAsString("Directions.Polyline.points");
                    string levels = result.GetAsString("Directions.Polyline.levels");
                    int zoomFactor = result.GetAsInteger("Directions.Polyline.ZoomFactor");
                    int numLevels = result.GetAsInteger("Directions.Polyline.NumLevels");
                    gDirection._mapDirection.Polyline = GeoPolyline.FromEncoded(0x00FF00, 4, 1, points,
                            zoomFactor, levels, numLevels);
                    int numOfGeocodes = result.GetSizeOfArray("Placemark");
                    if (numOfGeocodes > 0)
                    {
                        gDirection._mapDirection.GeoCodes = new MapPoint[numOfGeocodes];
                        for (int i = 0; i < numOfGeocodes; i++)
                        {
                            gDirection._mapDirection.GeoCodes[i] = new MapPoint();
                            gDirection._mapDirection.GeoCodes[i].Name = result.GetAsString("Placemark[" + i + "].address");
                            string location = result.GetAsString("Placemark[" + i + "].Point.coordinates");
                            GeoLatLng latLng = MapLayer.FromStringToLatLng(location);
                            gDirection._mapDirection.GeoCodes[i].SetPoint(latLng);

                        }
                    }
                    int numOfRoutes = result.GetSizeOfArray("Directions.Routes");
                    if (numOfRoutes > 0)
                    {
                        gDirection._mapDirection.Routes = new MapRoute[numOfRoutes];
                        for (int i = 0; i < numOfRoutes; i++)
                        {
                            string routeString = "Directions.Routes[" + i + "]";
                            gDirection._mapDirection.Routes[i] = MapDirection.NewRoute();
                            gDirection._mapDirection.Routes[i].Summary = _html2Text.Convert(result.GetAsString(routeString + ".summaryHtml"));
                            gDirection._mapDirection.Routes[i].Distance = result.GetAsInteger(routeString + ".Distance.meters");
                            gDirection._mapDirection.Routes[i].Duration = result.GetAsInteger(routeString + ".Duration.seconds");
                            string lastLatLng = result.GetAsString(routeString + ".End.coordinates");
                            gDirection._mapDirection.Routes[i].LastLatLng = MapLayer.FromStringToLatLng(lastLatLng);
                            int numOfSteps = result.GetSizeOfArray(routeString + ".Steps");
                            if (numOfSteps > 0)
                            {
                                gDirection._mapDirection.Routes[i].Steps = new MapStep[numOfSteps];
                                for (int j = 0; j < numOfSteps; j++)
                                {
                                    string stepString = routeString + ".Steps[" + j + "]";
                                    gDirection._mapDirection.Routes[i].Steps[j] = MapRoute.NewStep();
                                    gDirection._mapDirection.Routes[i].Steps[j].Description = _html2Text.Convert(result.GetAsString(stepString + ".descriptionHtml"));
                                    gDirection._mapDirection.Routes[i].Steps[j].Distance = result.GetAsInteger(stepString + ".Distance.meters");
                                    gDirection._mapDirection.Routes[i].Steps[j].Duration = result.GetAsInteger(stepString + ".Duration.seconds");
                                    gDirection._mapDirection.Routes[i].Steps[j].FirstLocationIndex = result.GetAsInteger(stepString + ".polylineIndex");
                                    string firstLocation = result.GetAsString(stepString + ".Point.coordinates");
                                    gDirection._mapDirection.Routes[i].Steps[j].FirstLatLng = MapLayer.FromStringToLatLng(firstLocation);
                                }
                            }
                        }

                    }

                }
                catch (Exception)
                {

                    if (gDirection._listener != null)
                    {
                        gDirection._listener.Done(gDirection._routeQuery, null);
                    }
                    return;

                }
                if (gDirection._listener != null)
                {
                    MapDirection mapDirection = gDirection._mapDirection;
                    if (mapDirection.GeoCodes.Length == mapDirection.Routes.Length + 1)
                    {
                        for (int i = 0; i < mapDirection.Routes.Length; i++)
                        {
                            mapDirection.Routes[i].StartGeocode = mapDirection.GeoCodes[i];
                            mapDirection.Routes[i].EndGeocode = mapDirection.GeoCodes[i + 1];
                        }
                    }

                    for (int i = 0; i < mapDirection.Routes.Length; i++)
                    {
                        MapRoute mapRoute = mapDirection.Routes[i];
                        for (int j = 0; j < mapRoute.Steps.Length - 1; j++)
                        {
                            MapStep mapStep = mapRoute.Steps[j];

                            mapStep.LastLocationIndex = mapRoute.Steps[j + 1].FirstLocationIndex;
                            mapStep.LastLatLng = mapDirection.Polyline.GetVertex(mapRoute.Steps[j + 1].FirstLocationIndex);
                        }
                        mapRoute.Steps[mapRoute.Steps.Length - 1].LastLocationIndex =
                                mapDirection.Polyline.GetVertexCount() - 1;
                        mapRoute.Steps[mapRoute.Steps.Length - 1].LastLatLng =
                                mapDirection.Polyline.GetVertex(mapDirection.Polyline.GetVertexCount() - 1);
                    }
                    GeoPolyline polyline = mapDirection.Polyline;
                    if (polyline.GetVertexCount() > 1)
                    {
                        GeoLatLng latLngTemp = polyline.GetVertex(0);
                        mapDirection.Bounds = new GeoLatLngBounds(latLngTemp, latLngTemp);

                        for (int i = 0; i < mapDirection.Routes.Length; i++)
                        {
                            MapRoute mapRoute = mapDirection.Routes[i];
                            latLngTemp = polyline.GetVertex(mapRoute.Steps[0].FirstLocationIndex);
                            mapRoute.Bounds = new GeoLatLngBounds(latLngTemp, latLngTemp);
                            for (int j = 0; j < mapRoute.Steps.Length; j++)
                            {
                                latLngTemp = polyline.GetVertex(mapRoute.Steps[j].FirstLocationIndex);
                                MapStep mapStep = mapRoute.Steps[j];
                                mapStep.Bounds = new GeoLatLngBounds(latLngTemp, latLngTemp);
                                for (int k = mapStep.FirstLocationIndex; k <= mapStep.LastLocationIndex; k++)
                                {
                                    GeoLatLng latLng = polyline.GetVertex(k);
                                    mapStep.Bounds.Add(latLng.Lng(), latLng.Lat());
                                    mapRoute.Bounds.Add(latLng.Lng(), latLng.Lat());
                                    mapDirection.Bounds.Add(latLng.Lng(), latLng.Lat());
                                }

                            }

                        }
                    }

                    gDirection._listener.Done(gDirection._routeQuery, mapDirection);
                }

            }

            public void ReadProgress(Object context, int bytes, int total)
            {
                GDirections gDirection = (GDirections)context;
                gDirection._listener.readProgress(bytes, total);
            }

            public void WriteProgress(Object context, int bytes, int total)
            {
            }

            public void Done(Object context, Response response)
            {
                GDirections gDirection = (GDirections)context;
                SearchResponse(gDirection, response);
            }

            public void Done(Object context, string rawResult)
            {
            }
        }
    }
}
