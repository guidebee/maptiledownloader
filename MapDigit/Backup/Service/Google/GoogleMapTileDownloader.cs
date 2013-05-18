using MapDigit.GIS.Raster;

namespace MapDigit.GIS.Service.Google
{
    internal class GoogleMapTileDownloader : MapTileDownloader
    {
        const string GOOGLE_VERSION = "99";

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a map downloader
         * @param listener download progress listener
         */
        GoogleMapTileDownloader()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override string GetTileURL(int mtype, int x, int y, int zoomLevel)
        {

            string url;
            mtype = mtype % (MapType.MAXMAPTYPE + 1);

            if (mtype == MapType.GOOGLEMAP || mtype == MapType.GOOGLEHYBRID)
            {
                url = "http://mt" + ((x + y) % 4) + ".google.com/mt?n=404&v=";
                url += (mtype == MapType.GOOGLEMAP) ? "w2." + GOOGLE_VERSION : "w2t." + GOOGLE_VERSION;
                url += "&X=" + x + "&Y=" + y + "&zoom=" + zoomLevel;
            }
            else if (mtype == MapType.GOOGLECHINA)
            {
                url = "http://mapgoogle.mapabc.com/googlechina/maptile?v=w2." + GOOGLE_VERSION;
                url += "&X=" + x + "&Y=" + y + "&zoom=" + zoomLevel;
            }
            else if (mtype == MapType.GOOGLESATELLITE)
            {
                string[] goolgeSatURLLetters = { "q", "r", "t", "s" };

                url = "http://kh" + ((x + y) % 4) + ".google.com/kh?n=404&v=" + GOOGLE_VERSION + "&t=t";
                for (int i = NUMZOOMLEVELS - zoomLevel - 1; i >= 0; i--)
                {
                    url += goolgeSatURLLetters[(((y >> i) & 1) << 1) + ((x >> i) & 1)];
                }
            }
            else
            {
                url = "http://mt" + ((x + y) % 4) + ".google.com/mt?n=404&v=";
                url += "w2." + GOOGLE_VERSION;
                url += "&X=" + x + "&Y=" + y + "&zoom=" + zoomLevel;
            }
            return url;
        }
    }

}
