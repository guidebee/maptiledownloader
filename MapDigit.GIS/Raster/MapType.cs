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
using System.Collections;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 19JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines the map types and relations between map types.
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 19/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapType
    {

        /**
     * Google Road Maps
     */
    public const int GOOGLEMAP = 0;
    /**
     * Google Satellite Images
     */
    public const int GOOGLESATELLITE = 1;
    /**
     * Google Satellite Images with Road Maps Overlayed
     */
    public const int GOOGLEHYBRID = 2;
    /**
     * Google Road Maps (China)
     */
    public const int GOOGLECHINA = 3;
    /**
     * Yahoo Road Maps
     */
    public const int YAHOOMAP = 4;
    /**
     * Yahoo Satellite Images
     */
    public const int YAHOOSATELLITE = 5;
    /**
     * Yahoo Satellite Images with Road Maps Overlayed
     */
    public const int YAHOOHYBRID = 6;
    /**
     * Yahoo India Road Maps
     */
    const int YAHOOINDIAMAP = 7;
    /**
     * Yahoo Satellite Images with India Road Maps Overlayed
     */
    const int YAHOOINDIAHYBRID = 8;
    /**
     * Ask.com Road Maps
     */
    public const int ASKDOTCOMMAP = 9;
    /**
     * Ask.com Satellite Images
     */
    public const int ASKDOTCOMSATELLITE = 10;
    /**
     * Ask.com Satellite Images with Labels
     */
    public const int ASKDOTCOMHYBRID = 11;
    /**
     *  Microsoft Road Maps
     */
    public const int MICROSOFTMAP = 12;
    /**
     *  Microsoft Satellite Maps
     */
    public const int MICROSOFTSATELLITE = 13;
    /**
     * Microsoft Satellite Images with Road Maps Overlayed
     */
    public const int MICROSOFTHYBRID = 14;
    /**
     * Microsoft Live China
     */
    public const int MICROSOFTCHINA = 15;
    /**
     * Nokia normal map
     */
    public const int NOKIAMAP = 16;
    /**
     * Map abc china
     */
    public const int MAPABCCHINA = 17;
    /**
     * Google terren
     */
    public const int GOOGLETERREN = 18;
    /**
     * OpenStreetMap.org Maps
     */
    public const int OPENSTREETMAP = 19;
    /**
     * Open satellite Maps
     */
    public const int OPENSATELLITETMAP = 20;
    /**
     * Open cycle Maps
     */
    public const int OPENCYCLEMAP = 21;
    /**
     * OSMA Maps
     */
    public const int OSMAMAP = 22;
    /**
     * Microsoft terren
     */
    public const int MICROSOFTTERREN = 23;
    /**
     * max map type
     */
    public const int MAXMAPTYPE = MICROSOFTTERREN;


    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_1 = 190;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_2 = 191;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_3 = 192;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_4 = 193;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_5 = 194;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_6 = 195;
    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_7 = 196;

    /**
     * Generic map type ,used to extension.
     */
    public const int GENERIC_MAPTYPE_CHINA = 197;
    /**
     * Routing direction map.
     */
    public const int ROUTING_DIRECTION = 198;
    /**
     * Mapinfo Vector map type
     */
    public const int MAPINFOVECTORMAP = 199;


        /**
         * for each map type, what consists each map type.
         * some map type like hybrid may consistes two map types, the satellites
         * and the hybrid itself. 
         */
        public static readonly Hashtable MapSequences = new Hashtable();

    /**
     * map type names and it's index
     */
    public static Hashtable MAP_TYPE_NAMES = new Hashtable();
    /**
     * map tile urls.
     */
    public static Hashtable MAP_TYPE_URLS = new Hashtable();

    static MapType(){
        MapSequences.Add((GOOGLEMAP), new int[]{GOOGLEMAP});
        MapSequences.Add((GOOGLESATELLITE), new int[]{GOOGLESATELLITE});
        MapSequences.Add((GOOGLEHYBRID), new int[] { GOOGLESATELLITE,GOOGLEHYBRID });
        MapSequences.Add((GOOGLECHINA), new int[]{GOOGLECHINA});
        MapSequences.Add((GOOGLETERREN), new int[]{GOOGLETERREN});
        MapSequences.Add((YAHOOMAP), new int[]{YAHOOMAP});
        MapSequences.Add((YAHOOSATELLITE), new int[]{YAHOOSATELLITE});
        MapSequences.Add((YAHOOHYBRID), new int[]{YAHOOSATELLITE, YAHOOHYBRID});
        MapSequences.Add((YAHOOINDIAMAP), new int[]{YAHOOINDIAMAP});
        MapSequences.Add((YAHOOINDIAHYBRID), new int[]{YAHOOINDIAHYBRID});
        MapSequences.Add((ASKDOTCOMMAP), new int[]{ASKDOTCOMMAP});
        MapSequences.Add((ASKDOTCOMSATELLITE), new int[]{ASKDOTCOMSATELLITE});
        MapSequences.Add((ASKDOTCOMHYBRID), new int[]{ASKDOTCOMSATELLITE, ASKDOTCOMHYBRID});
        MapSequences.Add((MICROSOFTMAP), new int[]{MICROSOFTMAP});
        MapSequences.Add((MICROSOFTSATELLITE), new int[]{MICROSOFTSATELLITE});
        MapSequences.Add((MICROSOFTHYBRID), new int[]{MICROSOFTSATELLITE, MICROSOFTHYBRID});
        MapSequences.Add((MICROSOFTCHINA), new int[]{MICROSOFTCHINA});
        MapSequences.Add((MICROSOFTTERREN), new int[]{MICROSOFTTERREN});
        MapSequences.Add((OPENSTREETMAP), new int[]{OPENSTREETMAP});
        MapSequences.Add((OPENSATELLITETMAP), new int[]{OPENSATELLITETMAP});
        MapSequences.Add((OPENCYCLEMAP), new int[]{OPENCYCLEMAP});
        MapSequences.Add((OSMAMAP), new int[]{OSMAMAP});
        MapSequences.Add((NOKIAMAP), new int[]{NOKIAMAP});
        MapSequences.Add((MAPABCCHINA), new int[]{MAPABCCHINA});
        MapSequences.Add((ROUTING_DIRECTION), new int[]{ROUTING_DIRECTION});
        MapSequences.Add((MAPINFOVECTORMAP), new int[]{MAPINFOVECTORMAP});
        MapSequences.Add((GENERIC_MAPTYPE_1), new int[]{GENERIC_MAPTYPE_1});
        MapSequences.Add((GENERIC_MAPTYPE_2), new int[]{GENERIC_MAPTYPE_2});
        MapSequences.Add((GENERIC_MAPTYPE_3), new int[]{GENERIC_MAPTYPE_3});
        MapSequences.Add((GENERIC_MAPTYPE_4), new int[]{GENERIC_MAPTYPE_4});
        MapSequences.Add((GENERIC_MAPTYPE_5), new int[]{GENERIC_MAPTYPE_5});
        MapSequences.Add((GENERIC_MAPTYPE_CHINA), new int[]{GENERIC_MAPTYPE_CHINA});
        MapSequences.Add((GENERIC_MAPTYPE_6), new int[]{GENERIC_MAPTYPE_6});
        MapSequences.Add((GENERIC_MAPTYPE_7), new int[]{GENERIC_MAPTYPE_6, GENERIC_MAPTYPE_7});


        MAP_TYPE_NAMES.Add("GOOGLEMAP", (GOOGLEMAP));
        MAP_TYPE_NAMES.Add("GOOGLESATELLITE", (GOOGLESATELLITE));
        MAP_TYPE_NAMES.Add("GOOGLEHYBRID", (GOOGLEHYBRID));
        MAP_TYPE_NAMES.Add("GOOGLECHINA", (GOOGLECHINA));
        MAP_TYPE_NAMES.Add("GOOGLETERREN", (GOOGLETERREN));
        MAP_TYPE_NAMES.Add("YAHOOMAP", (YAHOOMAP));
        MAP_TYPE_NAMES.Add("YAHOOSATELLITE", (YAHOOSATELLITE));
        MAP_TYPE_NAMES.Add("YAHOOHYBRID", (YAHOOHYBRID));
        MAP_TYPE_NAMES.Add("YAHOOINDIAMAP", (YAHOOINDIAMAP));
        MAP_TYPE_NAMES.Add("YAHOOINDIAHYBRID", (YAHOOINDIAHYBRID));
        MAP_TYPE_NAMES.Add("MICROSOFTMAP", (MICROSOFTMAP));
        MAP_TYPE_NAMES.Add("MICROSOFTSATELLITE", (MICROSOFTSATELLITE));
        MAP_TYPE_NAMES.Add("MICROSOFTHYBRID", (MICROSOFTHYBRID));
        MAP_TYPE_NAMES.Add("MICROSOFTCHINA", (MICROSOFTCHINA));
        MAP_TYPE_NAMES.Add("MICROSOFTTERREN", (MICROSOFTTERREN));
        MAP_TYPE_NAMES.Add("OPENSTREETMAP", (OPENSTREETMAP));
        MAP_TYPE_NAMES.Add("OPENSATELLITETMAP", (OPENSATELLITETMAP));
        MAP_TYPE_NAMES.Add("OPENCYCLEMAP", (OPENCYCLEMAP));
        MAP_TYPE_NAMES.Add("OSMAMAP", (OSMAMAP));
        MAP_TYPE_NAMES.Add("NOKIAMAP", (NOKIAMAP));
        MAP_TYPE_NAMES.Add("MAPABCCHINA", (MAPABCCHINA));

        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_1", (GENERIC_MAPTYPE_1));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_2", (GENERIC_MAPTYPE_2));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_3", (GENERIC_MAPTYPE_3));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_4", (GENERIC_MAPTYPE_4));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_5", (GENERIC_MAPTYPE_5));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_6", (GENERIC_MAPTYPE_6));
        MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_7", (GENERIC_MAPTYPE_7));
         MAP_TYPE_NAMES.Add("GENERIC_MAPTYPE_CHINA", (GENERIC_MAPTYPE_CHINA));


    }


        
        /**
     * replace all entries of pattern[N] with value replace[N]
     * length of pattern[] must equal to length of replace[]

     * @return result string with replaced values

     * @param string[] pattern - array of patterns to be replaced
     * @param string[] replace - array of values to be inserted instead of pattern[i]
     * @param string source entire string

     */
        public static string replace2(string[] pattern, string[] replace, string source)
        {
            string result = "";
            if (pattern.Length != replace.Length)
            {
                return source;
            }
            result = source;
            for (int i = 0; i < pattern.Length; i++)
            {
                result = replace1(pattern[i], replace[i], result);
            }
            return result;
        }

        private static string replaceMetaString(string input, int mtype, int x, int y, int zoomLevel)
        {
            int digit = ((x + y) % 4);
            zoomLevel = NUMZOOMLEVELS - zoomLevel;
            string[] pattern = new string[]{
            "{GOOGLE_DIGIT}",
            "{X}",
            "{Y}",
            "{ZOOM}",
            "{GALILEO}",
            "{MS_DIGIT}",
            "{QUAD}",
            "{YAHOO_DIGIT}",
            "{YAHOO_Y}",
            "{YAHOO_ZOOM}",
            "{YAHOO_ZOOM_2}",
            "{OAM_ZOOM}",
            "{NOKIA_ZOOM}"
        };

            string quad = "";
            for (int i = zoomLevel - 1; i >= 0; i--)
            {
                quad = quad + (((((y >> i) & 1) << 1) + ((x >> i) & 1)));
            }
            string[] replace = new string[]{
            digit.ToString(),//"{GOOGLE_DIGIT}"
            x.ToString(),//"{X}"
            y.ToString(),//"{Y}"
            zoomLevel.ToString(),//"{ZOOM}"
            ((3 * x + y) % 8).ToString(),//"{GALILEO}"
            (((y & 1) << 1) + (x & 1)).ToString(),//"{MS_DIGIT}"
            quad,//"{QUAD}"
            (1 + ((x + y) % 3)).ToString(),//"{YAHOO_DIGIT}"
            (((1 << (zoomLevel)) >> 1) - 1 - y).ToString(),//"{YAHOO_Y}"
            (zoomLevel + 1).ToString(),//"{YAHOO_ZOOM}"
            (NUMZOOMLEVELS - zoomLevel + 1).ToString(),//"{YAHOO_ZOOM_2}"
            (NUMZOOMLEVELS - zoomLevel).ToString(),//"{OAM_ZOOM}"
            zoomLevel.ToString()//"{NOKIA_ZOOM}"
        };
            string url = replace2(pattern, replace, input);
            return url;
        }


        /**
         * replace all entries of pattern with value replace

         * @return result string with replaced values

         * @param string pattern - pattern to be replaced
         * @param string replace - value to be inserted instead of pattern[i]
         * @param string source entire string

         */
        public static string replace1(string pattern, string replace, string source)
        {
            string result = "";
            int firstIndex = 0;
            while (source.IndexOf(pattern, firstIndex) != -1)
            {
                result += source.Substring(firstIndex, source.IndexOf(pattern, firstIndex));
                result += replace;
                firstIndex = source.IndexOf(pattern, firstIndex) + pattern.Length;
            }
            if (firstIndex < source.Length)
            {
                result += source.Substring(firstIndex);
            }
            return result;
        }

   public static string getTileURL(int mtype, int x, int y, int zoomLevel) {

        string metaURL = null;
        lock (MAP_TYPE_URLS) {
            metaURL = (string)MAP_TYPE_URLS[mtype];
        }
        if (metaURL != null) {
            return replaceMetaString(metaURL, mtype, x, y, zoomLevel);
        } else {
            return getTileInternalURL(mtype, x, y, zoomLevel);
        }

    }

static  int NUMZOOMLEVELS = 17;

    private static string getTileInternalURL(int mtype, int x, int y, int zoomLevel) {

        string url = "";
        mtype = mtype % (MapType.MAXMAPTYPE + 1);

        switch (mtype) {
//            case MapType.GOOGLETERREN:
//                url = "http://khm.google.com/maptilecompress?t=3&q=100";
//                url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS-zoomLevel);
//                break;


            case MapType.MAPABCCHINA:
                url = "http://emap" + ((x + y) % 4) + ".mapabc.com/mapabc/maptile?v=";
                url += "w2.99";
                url += "&x=" + x + "&y=" + y + "&zoom=" + zoomLevel;
                break;
            case MapType.GOOGLECHINA:
                url = "http://mt" + ((x + y) % 4) + ".google.cn/vt/v=";
                url += "w2.99";
                url += "&hl=en&gl=cn&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                break;
            case MapType.GOOGLEMAP:
                url = "http://mt" + ((x + y) % 4) + ".google.com/vt/v=";
                url += "w2.99";
                url += "&hl=en&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                break;
            case MapType.GOOGLEHYBRID:
                url = "http://mt" + ((x + y) % 4) + ".google.com/vt/v=";
                url += "wt2.99";
                url += "&hl=en&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS - zoomLevel);
                break;

//            case MapType.GOOGLESATELLITE:
//            {
//                url = "http://khm.google.com/maptilecompress?t=2&q=100";
//                url += "&x=" + x + "&y=" + y + "&z=" + (NUMZOOMLEVELS-zoomLevel);
//                break;
//            }
            case MapType.YAHOOHYBRID:

            case MapType.YAHOOMAP:
            case MapType.YAHOOSATELLITE:
                url = "http://maps" + (1 + ((x + y) % 3)) + ".yimg.com/hx/";
                url += (mtype == MapType.YAHOOMAP)
                        ? "tl?v=4.2"
                        : (mtype == MapType.YAHOOSATELLITE)
                        ? "ae/ximg?v=1.9&t=a&s=256"
                        : "ae/ximg?v=1.9&t=a&s=256";
                url += "&.intl=en&x=" + x + "&y=" + (((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1) - 1 - y)
                        + "&z=" + (NUMZOOMLEVELS - zoomLevel + 1) + "&r=1";
                break;
            case MapType.YAHOOINDIAHYBRID:
            case MapType.YAHOOINDIAMAP:
                url = "http://maps.yimg.com/hw/tile?locale=en&imgtype=png&yimgv=1.2&v=4.1";
                url += "&x=" + x + "&y=" + (((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1) - 1 - y)
                        + "&z=" + (zoomLevel + 1);
                break;
            case MapType.MICROSOFTHYBRID:
            case MapType.MICROSOFTMAP:
            case MapType.MICROSOFTSATELLITE:
                url = (mtype == MapType.MICROSOFTMAP)
                        ? "http://r"
                        : (mtype == MapType.MICROSOFTSATELLITE)
                        ? "http://a" : "http://h";
                url += (((y & 1) << 1) + (x & 1))
                        + ".ortho.tiles.virtualearth.net/tiles/";
                url += (mtype == MapType.MICROSOFTMAP)
                        ? "r"
                        : (mtype == MapType.MICROSOFTSATELLITE)
                        ? "a" : "h";
                for (int i = NUMZOOMLEVELS - zoomLevel - 1; i >= 0; i--) {
                    url = url + (((((y >> i) & 1) << 1) + ((x >> i) & 1)));
                }
                url += (mtype == MapType.MICROSOFTMAP)
                        ? ".png?g=90"
                        : ".jpeg?g=90";
                break;
            case MapType.MICROSOFTCHINA:
                url = "http://r";
                url += (((y & 1) << 1) + (x & 1))
                        + ".tiles.ditu.live.com/tiles/";
                url += "r";
                for (int i = NUMZOOMLEVELS - zoomLevel - 1; i >= 0; i--) {
                    url = url + (((((y >> i) & 1) << 1) + ((x >> i) & 1)));
                }
                url += ".png?g=44";
                break;
            case MapType.ASKDOTCOMHYBRID:
            case MapType.ASKDOTCOMMAP:
            case MapType.ASKDOTCOMSATELLITE:
                url = (zoomLevel > 6)
                        ? "http://mapstatic"
                        : "http://mapcache";
                url += ((x + y) % 4 + 1) + ".ask.com/";
                url += (mtype == MapType.ASKDOTCOMMAP)
                        ? "map"
                        : (mtype == MapType.ASKDOTCOMSATELLITE)
                        ? "sat" : "mapt";
                url += "/" + (zoomLevel + 2) + "/";
                url += (x - ((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1))
                        + "/" + (y - ((1 << (NUMZOOMLEVELS - zoomLevel)) >> 1));
                url += "?partner=&tc=28";
                break;
            case MapType.OPENSTREETMAP:
                url = "http://tile.openstreetmap.org/"
                        + (NUMZOOMLEVELS - zoomLevel)
                        + "/" + x + "/" + y + ".png";
                break;
            case MapType.NOKIAMAP:
                url = "http://maptile.svc.nokia.com.edgesuite.net/maptiler/maptile/0.1.22.103/normal.day/"
                        + (NUMZOOMLEVELS - zoomLevel)
                        + "/" + x + "/" + y + "/256/png";
                break;
        }
        return url;
    }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * private constructor.
         */
        private MapType()
        {

        }


    }

}
