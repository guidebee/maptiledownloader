namespace MapVectorTileWriter
{
    class MapType
    {
        /**
         * Google Road Maps
         */
        public const int GOOGLEMAP=0;

        /**
         * Google Satellite Images
         */
        public const  int GOOGLESATELLITE=1;

        /**
         * Google Satellite Images with Road Maps Overlayed
         */
        public const  int GOOGLEHYBRID=2;


        /**
         * Google Road Maps (China)
         */
        public const  int GOOGLECHINA=3;

        /**
         * Yahoo Road Maps
         */
        public const  int YAHOOMAP=4;

        /**
         * Yahoo Satellite Images
         */
        public const  int YAHOOSATELLITE=5;

        /**
         * Yahoo Satellite Images with Road Maps Overlayed
         */
        public const  int YAHOOHYBRID=6;

        /**
         * Yahoo India Road Maps
         */
        public const  int YAHOOINDIAMAP=7;

        /**
         * Yahoo Satellite Images with India Road Maps Overlayed
         */
        public const  int YAHOOINDIAHYBRID=8;

        /**
         * Ask.com Road Maps
         */
        public const  int ASKDOTCOMMAP=9;

        /**
         * Ask.com Satellite Images
         */
        public const  int ASKDOTCOMSATELLITE=10;

        /**
         * Ask.com Satellite Images with Labels
         */
        public const  int ASKDOTCOMHYBRID=11;

        /**
         *  Microsoft Road Maps
         */
        public const  int MICROSOFTMAP=12;

        /**
         *  Microsoft Satellite Maps
         */
        public const  int MICROSOFTSATELLITE=13;

        /**
         * Microsoft Satellite Images with Road Maps Overlayed
         */
        public const  int MICROSOFTHYBRID=14;

        /**
         * Microsoft Live China
         */
        public const  int MICROSOFTCHINA=15;


        /**
         * OpenStreetMap.org Maps
         */
        public const  int OPENSTREETMAP=16;

        /**
         * Map abc china
         */
        public const  int MAPABCCHINA=17;

        public const int GOOGLETERREN=18;

        /**
         * total zoom levels
         */
        public const int NUMZOOMLEVELS = 17;

        /**
         * the map tile width.
         */
        public const int MAP_TILE_WIDTH = 256;


        /**
         * Microsoft Live China
         */
        protected const  int MAXMAPTYPE=18;

        private static readonly MapType INSTANCE=new MapType();

        private MapType()
        {
            
        }
        
        public static MapType GetInstance()
        {
            return INSTANCE;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the URL of map tile image.
         * @param mtype the map tile (msn,yahoo etc).
         * @param x the x index of the map tile.
         * @param y the y index of the map tile.
         * @param zoomLevel   current zoom level
         * @return the url of given map tile.
         */
        public string getTileURL(int mtype, int x, int y, int zoomLevel)
        {

            string url = "";
            mtype = mtype % (MAXMAPTYPE + 1);
            switch (mtype)
            {
                case GOOGLETERREN:
                    url = "http://khm.google.com/maptilecompress?t=3&q=100";
                    url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                    break;
                case MAPABCCHINA:
                    url = "http://emap" + ((x + y) % 4) + ".mapabc.com/mapabc/maptile?v=";
                    url += "w2.99";
                    url += "&x=" + x + "&y=" + y + "&zoom=" + zoomLevel;
                    break;
                case GOOGLEMAP:
                    url = "http://khm.google.com/maptilecompress?t=1&q=100";
                    url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                    break;
                case GOOGLEHYBRID:
                    url = "http://khm.google.com/maptilecompress?t=3&q=100";
                    url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                    break;
                case GOOGLECHINA:
                    url = "http://khm.google.com/maptilecompress?t=1&q=100&hl=zh-CN";
                    url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                    break;
                case GOOGLESATELLITE:
                    {
                        url = "http://khm.google.com/maptilecompress?t=2&q=100";
                        url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                        break;
                    }
                case YAHOOHYBRID:
                case YAHOOINDIAHYBRID:
                case YAHOOINDIAMAP:
                case YAHOOMAP:
                case YAHOOSATELLITE:
                    url = (mtype == YAHOOMAP)
                              ? "http://png.maps.yimg.com/png?t=m&v=3.52"
                              : (mtype == YAHOOSATELLITE)
                                    ? "http://aerial.maps.yimg.com/tile?t=a&v=1.7"
                                    : (mtype == YAHOOINDIAMAP)
                                          ? "http://tile.in.maps.yahoo.com/tile?imgtype=png&v=0.95"
                                          : (mtype == YAHOOHYBRID)
                                                ? "http://aerial.in.maps.yahoo.com/tile?imgtype=png&v=0.93"
                                                : "http://aerial.maps.yimg.com/png?t=h&v=2.2";
                    url += "&x=" + x + "&y=" +
                           (((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1) - 1 - y)
                           + "&z=" + (zoomLevel + 1);
                    break;
                case MICROSOFTHYBRID:
                case MICROSOFTMAP:
                case MICROSOFTSATELLITE:
                    url = (mtype == MICROSOFTMAP)
                              ? "http://r"
                              : (mtype == MICROSOFTSATELLITE)
                                    ? "http://a" : "http://h";
                    url += (((y & 1) << 1) + (x & 1))
                           + ".ortho.tiles.virtualearth.net/tiles/";
                    url += (mtype == MICROSOFTMAP)
                               ? "r"
                               : (mtype == MICROSOFTSATELLITE)
                                     ? "a" : "h";
                    for (int i = NUMZOOMLEVELS - zoomLevel - 1; i >= 0; i--)
                    {
                        url = url + (((((y >> i) & 1) << 1) + ((x >> i) & 1)));
                    }
                    url += (mtype == MICROSOFTMAP)
                               ? ".png?g=90"
                               : ".jpeg?g=90";
                    break;
                case MICROSOFTCHINA:
                    url = "http://r";
                    url += (((y & 1) << 1) + (x & 1))
                           + ".tiles.ditu.live.com/tiles/";
                    url += "r";
                    for (int i = NUMZOOMLEVELS - zoomLevel - 1; i >= 0; i--)
                    {
                        url = url + (((((y >> i) & 1) << 1) + ((x >> i) & 1)));
                    }
                    url += ".png?g=41";
                    break;
                case ASKDOTCOMHYBRID:
                case ASKDOTCOMMAP:
                case ASKDOTCOMSATELLITE:
                    url = (zoomLevel > 6)
                              ? "http://mapconst"
                              : "http://mapcache";
                    url += ((x + y) % 4 + 1) + ".ask.com/";
                    url += (mtype == ASKDOTCOMMAP)
                               ? "map"
                               : (mtype == ASKDOTCOMSATELLITE)
                                     ? "sat" : "mapt";
                    url += "/" + (zoomLevel + 2) + "/";
                    url += (x - ((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1))
                           + "/" + (y - ((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1));
                    url += "?partner=&tc=28";
                    break;
                case OPENSTREETMAP:
                    url = "http://tile.openstreetmap.org/"
                          + (NUMZOOMLEVELS - zoomLevel)
                          + "/" + x + "/" + y + ".png";
                    break;
            }
            return url;
        }

    }
}