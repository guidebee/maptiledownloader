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
     *  Base class for all map tile downloader/reader/render.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class MapTileAbstractReader
    {

        /**
         * This image array stores map tiles downloaded
         */
        public volatile byte[] ImageArray;

        /**
         * the actual image size in the image array
         */
        public volatile int ImageArraySize;

        /**
         * indicates the data in the image array is valid or not.
         */
        public volatile bool IsImagevalid;

        /**
         * total bytes downloaded with this downloader
         */
        public static long TotaldownloadedBytes;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set downloader listener
         * @param listener
         */
        public virtual void SetMapDownloadingListener(IReaderListener listener)
        {
            _readListener = listener;
  
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get image at given location. when the reader is done, imageArray shall
         * store the image byte array. imageArraySize is the actually data size.
         * isImagevalid indicate the data is valid or not. normally this shall be
         * an async call.
         * @param mtype the map type of the map tile.
         * @param X the X index of the map tile.
         * @param Y the Y index of the map tile.
         * @param zoomLevel the zoom level of the map tile.
         */
        public abstract void GetImage(int mtype, int x, int y, int zoomLevel);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * a way app can cancel the reading process.
         */
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public virtual void CancelRead()
        {
        }


        /**
         * downloader listener
         */
        protected IReaderListener _readListener;


    }

}
