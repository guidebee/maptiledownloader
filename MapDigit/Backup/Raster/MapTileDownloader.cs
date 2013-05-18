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
using System.IO;
using System.Net;
using MapDigit.GIS.Geometry;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * MapTileDownloader download map image tiles from server (msn,yahoo,etc).
     * <p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class MapTileDownloader : MapTileAbstractReader
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the map tile image and stored in the map cache.
         * @param mtype the map tile (msn,yahoo etc).
         * @param X the X index of the map tile.
         * @param Y the Y index of the map tile.
         * @param zoomLevel   current zoom level
         */
        private void GetImage1(int mtype, int x, int y, int zoomLevel)
        {
            _mapType = mtype;
            _mapXIndex = x;
            _mapYIndex = y;
            _mapZoomLevel = zoomLevel;
            HttpStatusCode imgResponseCode = HttpStatusCode.NotFound;
            IsImagevalid = true;
            ImageArraySize = 0;
            HttpWebResponse httpWResp = null;
            try
            {
                string location = GetTileURL(mtype, x, y, NUMZOOMLEVELS - zoomLevel);
                _imgConn = (HttpWebRequest)WebRequest.Create(location);
                _imgConn.Proxy = WebRequest.DefaultWebProxy;
                _imgConn.Proxy.Credentials = CredentialCache.DefaultCredentials;
                //imgConn.Headers.Add("Accept", "image/png");
                httpWResp = (HttpWebResponse)_imgConn.GetResponse();

                imgResponseCode = httpWResp.StatusCode;
            }
            catch (Exception)
            {
                IsImagevalid = false;
            }

            if (imgResponseCode != HttpStatusCode.OK)
            {
                IsImagevalid = false;
            }

            if (IsImagevalid)
            {
                try
                {
                    if (httpWResp != null)
                    {
                        int totalToReceive
                            = (int)(httpWResp.ContentLength);
                        TotaldownloadedBytes += totalToReceive;
                        MapProgressInputStream stream =
                            new MapProgressInputStream(httpWResp.GetResponseStream(),
                                                       totalToReceive, _readListener, 1024);
                        int totalToRead = totalToReceive;// (int)Math.Max(totalToReceive, stream.avaiable());
                        ImageArraySize = totalToRead;
                        ImageArray = new byte[totalToRead];
                        stream.Read(ImageArray, 0, ImageArray.Length);
                        stream.Close();
                    }
                }
                catch (Exception)
                {
                    IsImagevalid = false;
                    ImageArray = null;
                    GC.Collect();

                }
            }
            _imgConn = null;

        }

        public override void GetImage(int mtype, int x, int y, int zoomLevel)
        {
            switch(mtype)
            {
                case MapType.GOOGLECHINA:
                case MapType.GOOGLEHYBRID:
                case MapType.GOOGLEMAP:
                case MapType.GOOGLESATELLITE:
                case MapType.GOOGLETERREN:
                    GetImage2(mtype, x, y, zoomLevel);
                    break;
                default:
                    GetImage2(mtype, x, y, zoomLevel);
                    break;
            }
        }

        private void GetImage2(int mtype, int x, int y, int zoomLevel)
        {
            _mapType = mtype;
            _mapXIndex = x;
            _mapYIndex = y;
            _mapZoomLevel = zoomLevel;
            IsImagevalid = true;
            ImageArraySize = 0;
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.CacheControl] = "No-Transform";
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                wc.Proxy = WebRequest.DefaultWebProxy;
                wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
                try
                {
                    string location = GetTileURL(mtype, x, y, NUMZOOMLEVELS - zoomLevel);
                    ImageArray = wc.DownloadData(location);
                    ImageArraySize = ImageArray.Length;
                    
                }
                catch (Exception e)
                {
                    IsImagevalid = false;
                    ImageArray = null;
                    GC.Collect();
                }
            }



        }
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Cancel the download http connection.
         */
        public override void CancelRead()
        {
            if (_imgConn != null)
            {
                IsImagevalid = false;
                try
                {
                    _imgConn.Abort();
                }
                catch (IOException)
                {

                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the URL of map tile image.
         * @param mtype the map tile (msn,yahoo etc).
         * @param X the X index of the map tile.
         * @param Y the Y index of the map tile.
         * @param zoomLevel   current zoom level
         * @return the url of given map tile.
         */
        public virtual string GetTileURL(int mtype, int x, int y, int zoomLevel)
        {

            return MapType.getTileURL(mtype, x, y, zoomLevel);
        }


        /**
         * total zoom levels
         */
        protected static int NUMZOOMLEVELS = 17;
        /**
         * map type
         */
        protected int _mapType;
        /**
         * X index of the map tile
         */
        protected int _mapXIndex;
        /**
         * Y index of the map tile
         */
        protected int _mapYIndex;
        /**
         * zoom Level of the map tile.
         */
        protected int _mapZoomLevel;
        /**
         * Max wait time for download an image in seconds.
         */
        protected int _maxWaitingTime = 90;
        /**
         *  Http connection for donwloading images.
         */
        protected HttpWebRequest _imgConn;

        /**
         * the map tile Width.
         */
        protected static int MapTileWidth = 256;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        protected static int Cast2Integer(double f)
        {
            if (f < 0)
            {
                return (int)MathEx.Ceil(f);
            }
            return (int)MathEx.Floor(f);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the index of map tiles based on given piexl coordinates
         * @param X  X coordinates
         * @param Y Y coordinates .
         * @return the the index of map tiles
         */
        protected static GeoPoint GetMapIndex(double x, double y)
        {
            double longtiles = x / MapTileWidth;
            int tilex = Cast2Integer(longtiles);
            int tiley = Cast2Integer(y / MapTileWidth);
            return new GeoPoint(tilex, tiley);
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // --------   -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	      Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * PostData defines HTTP multi-part Post message contents.
         * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
         * @version     1.00, 03/01/09
         * @author      Guidebee, Inc.
         */
        protected class MapProgressInputStream
        {

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            public MapProgressInputStream(Stream stream, int total, IReaderListener listener, int notifyInterval)
            {
                _stream = stream;
                _total = total;
                _listener = listener;
                _notifyInterval = notifyInterval;
                _nread = 0;
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            public int Read()
            {
                if ((++_nread % _notifyInterval) == 0)
                {
                    try
                    {
                        if (_listener!=null)
                            _listener.readProgress(_nread, _total);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _stream.ReadByte();
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 18JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            public void Close()
            {
                _stream.Close();

            }

            public int Avaiable()
            {
                return (int)_stream.Length;
            }

            private readonly Stream _stream;
            private readonly int _total;
            private readonly IReaderListener _listener;
            private readonly int _notifyInterval;
            private int _nread;


            public int Read(byte[] buffer, int offset, int count)
            {
                byte[] buf = new byte[count];
                int val = Read();
                int index = 0;
                while (val != -1 && index < count)
                {
                    buf[index++] = (byte)val;
                    val = Read();

                }
                if (index > 0)
                {
                    Array.Copy(buf, 0, buffer, offset, index);
                }
                return index;
            }


        }
    }




}
