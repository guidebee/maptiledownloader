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
using System.Collections;
using System.Threading;
using MapDigit.GIS.Drawing;

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
     * map tile download manager.
     * <p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 19/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapTileDownloadManager
    {

        /**
         * The map tile not avaiable image.
         */
        public static IImage TileNotAvaiable;

        /**
         * the map tile is downloading image.
         */
        public static IImage TileDownloading;

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param mapDownloadingListener donwload listener
         */
        public MapTileDownloadManager(IReaderListener mapDownloadingListener)
            : this(mapDownloadingListener, null)
        {

        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param mapDownloadingListener donwload listener
         * @param mapTileReader the map tile downloader.
         */
        public MapTileDownloadManager(IReaderListener mapDownloadingListener,
                MapTileAbstractReader mapTileReader)
        {
            _isCacheOn = MapConfiguration.IsCacheOn;
            _drawRoute = MapConfiguration.DrawRouting;
            _maxImageDownloadWorkder = MapConfiguration.WorkerThreadNumber;
            _maxBytesInCache = MapConfiguration.MapCacheSizeInBytes;
            _imageDownloadWorkers =
                new MapTileDownloadWorker[_maxImageDownloadWorkder + 1];
            _mapDownloadingListener = mapDownloadingListener;
            lock (_threadListMutex)
            {
                _threadLists.Clear();
                for (int i = 0; i < _maxImageDownloadWorkder; i++)
                {
                    if (mapTileReader != null)
                    {
                        _imageDownloadWorkers[i] =
                                new MapTileDownloadWorker(this, mapTileReader,
                                "MapTileDownloadWorker" + i);
                    }
                    else
                    {
                        _imageDownloadWorkers[i] = new MapTileDownloadWorker(this,
                                "MapTileDownloadWorker" + i);
                    }
                    _threadLists.Add("MapTileDownloadWorker" + i,
                            _imageDownloadWorkers[i]._mapTileDownloadWorkerThread);
                }
                if (_drawRoute)
                {
                    _imageDownloadWorkers[_maxImageDownloadWorkder] = new MapDirectionRendererWorker(this);
                    _threadLists.Add("MapDirectionRendererWorker",
                            _imageDownloadWorkers[_maxImageDownloadWorkder]._mapTileDownloadWorkerThread);
                }

                _mapTileDownloadManagerThread = new Thread(Run) {Name = "MapTileDownloadManager"};
            }
        }


        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * start the manager thread and the worker threads.
         */
        public void Start()
        {
            _stopDownloadManager = false;
            int threadCount = _maxImageDownloadWorkder;
            if (_drawRoute)
            {
                threadCount = _maxImageDownloadWorkder + 1;
            }
            for (int i = 0; i < threadCount; i++)
            {
                _imageDownloadWorkers[i].Start();
            }
            //_mapTileDownloadManagerThread.Priority = ThreadPriority.Lowest;
            _mapTileDownloadManagerThread.Start();
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * restart the worker thread in case worker thread is died.
         */
        public void RestartWorker()
        {

            lock (_threadListMutex)
            {
                _threadLists.Clear();
                for (int i = 0; i < _maxImageDownloadWorkder; i++)
                {
                    if (_imageDownloadWorkers[i] != null)
                    {
                        _imageDownloadWorkers[i].Stop();
                        _imageDownloadWorkers[i] = null;
                    }
                    _imageDownloadWorkers[i] = new MapTileDownloadWorker(this,
                            "MapTileDownloadWorker" + i);
                    _imageDownloadWorkers[i].Start();
                    _threadLists.Add("MapTileDownloadWorker" + i,
                            _imageDownloadWorkers[i]._mapTileDownloadWorkerThread);
                }
                if (_drawRoute)
                {
                    _imageDownloadWorkers[_maxImageDownloadWorkder] = new MapDirectionRendererWorker(this);
                    _threadLists.Add("MapDirectionRendererWorker",
                            _imageDownloadWorkers[_maxImageDownloadWorkder]._mapTileDownloadWorkerThread);
                }
            }
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Stop the manager thread and all the worker threads.
         */
        public void Stop()
        {
            _stopDownloadManager = true;
            lock (_syncObjectManager)
            {
                try
                {
                    Monitor.PulseAll(_syncObjectManager);
                }
                catch (Exception)
                {

                }
            }
            int threadCount = _maxImageDownloadWorkder;
            if (_drawRoute)
            {
                threadCount = _maxImageDownloadWorkder + 1;
            }
            for (int i = 0; i < threadCount; i++)
            {
                if (_imageDownloadWorkers[i] != null)
                {
                    _imageDownloadWorkers[i].Stop();
                }
            }
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return all the worker threads.
         * @return the hashtable Contains all the worker threads.
         */
        public Hashtable GetThreads()
        {
            lock (_threadListMutex)
            {
                return _threadLists;
            }
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * the running method of this manager thread.
         */
        public void Run()
        {
            while (!_stopDownloadManager)
            {
                int size = _imageTileDownloadList.Count;
                if (size > 0)
                {
                    int threadCount = _maxImageDownloadWorkder;
                    if (_drawRoute)
                    {
                        threadCount = _maxImageDownloadWorkder + 1;
                    }
                    for (int i = 0; i < threadCount; i++)
                    {
                        if (_imageDownloadWorkers[i].IsPaused())
                        {
                            _imageDownloadWorkers[i].Resume();
                        }
                    }
                }
                lock (_syncObjectManager)
                {
                    try
                    {
                        Monitor.Wait(_syncObjectManager);
                    }
                    catch (Exception)
                    {

                    }
                }

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Save map image cache to persistent memory.
         */
        internal void SaveMapCache()
        {
            //if (IS_CACHE_ON) {
            //    try {
            //        RecordStore.deleteRecordStore(MAP_DATA_RECORDSTORE_NAME);
            //    } catch (RecordStoreException ingore) {
            //        ingore.printStackTrace();
            //    }
            //    try {

            //        synchronized (imageCache) {
            //            mapDataRecordStore = RecordStore.openRecordStore(MAP_DATA_RECORDSTORE_NAME, true);
            //            int numberOfRecord = Math.Min(MAX_MAP_TILES_NUMBERS, imageCache.size());
            //            for (int i = numberOfRecord - 1; i >= 0; i--) {
            //                string key = (string) imageCache.keyAt(i);

            //                byte[] imageArray = (byte[]) imageCache.get(key);
            //                if (imageArray != null) {
            //                    byte[] recordDate = image2ByteArray(key, imageArray);
            //                    mapDataRecordStore.addRecord(recordDate, 0, recordDate.length);
            //                }

            //            }
            //        }
            //        mapDataRecordStore.closeRecordStore();
            //        mapDataRecordStore = null;

            //    } catch (RecordStoreException e) {
            //        e.printStackTrace();
            //    }
            //}
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Restore map image cache from persistent memory.
         */
        internal void RestoreMapCache()
        {
            //if (IS_CACHE_ON) {
            //    try {
            //        lock (imageCache) {
            //            imageCache.clear();
            //            if (mapDataRecordStore == null) {
            //                mapDataRecordStore = RecordStore.openRecordStore(MAP_DATA_RECORDSTORE_NAME, false);
            //                int numOfRecords = mapDataRecordStore.getNumRecords();
            //                numOfRecords = Math.Min(numOfRecords, MAX_MAP_TILES_NUMBERS);
            //                for (int i = 0; i < numOfRecords; i++) {
            //                    byte[] recordDate = mapDataRecordStore.getRecord(i + 1);
            //                    addOneImageToCacheFromRecordStore(recordDate);

            //                }
            //                mapDataRecordStore.closeRecordStore();
            //                mapDataRecordStore = null;
            //            }
            //        }

            //    } catch (RecordStoreException ignore) {

            //    }
            //}

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the route direction.
         * @return newDirection
         */
        internal MapDirection GetMapDirection()
        {
            return _mapDirectionRenderer.GetMapDirection();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the route direction.
         * @param newDirection
         */
        internal void SetMapDirection(MapDirection newDirection)
        {
            _mapDirectionRenderer.SetMapDirection(newDirection);
        }

        public void RemoveFromImageCache(int mtype, int x, int y, int zoomLevel)
        {
            string mapIndex = mtype + "|" + x + "|" +
                              y + "|" + zoomLevel;
            lock (_imageCache)
            {
                if (_imageCache.ContainsKey(mapIndex))
                {
                    _imageCache.Remove(mapIndex);
                }
            }
        }

        public byte[] GetFromImageCache(int mtype, int x, int y, int zoomLevel)
        {
            string mapIndex = mtype + "|" + x + "|" +
                              y + "|" + zoomLevel;
            lock (_imageCache)
            {
                if (_imageCache.ContainsKey(mapIndex))
                {
                    return (byte[])_imageCache[mapIndex];
                }
                else
                {
                    GetImage(mtype, x, y, zoomLevel);
                }
            }
            return null;
        }
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get a map tile image in cache.
         * @param mtype the map type of the map tile.
         * @param X the X index of the map tile.
         * @param Y the Y index of the map tile.
         * @param zoomLevel the zoom level of the map tile.
         * @return the image at give location.
         */
        internal IImage GetCachedImage(int mtype, int x, int y, int zoomLevel)
        {
            string key = mtype + "|" +
                    x + "|" +
                    y + "|" +
                    zoomLevel;
            IImage image;
            _lastestZoomLevel = zoomLevel;
            byte[] imageArray;
            lock (_imageCache)
            {
                imageArray = (byte[])_imageCache[key];
            }
            if (imageArray == null)
            {
                image = TileDownloading;
            }
            else
            {
                image = MapLayer.GetAbstractGraphicsFactory().
                        CreateImage(imageArray, 0, imageArray.Length);
            }

            return image;
        }
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get a map tile image.
         * @param mtype the map type of the map tile.
         * @param X the X index of the map tile.
         * @param Y the Y index of the map tile.
         * @param zoomLevel the zoom level of the map tile.
         * @return the image at give location.
         */
        internal IImage GetImage(int mtype, int x, int y, int zoomLevel)
        {
            string key = mtype + "|" +
                    x + "|" +
                    y + "|" +
                    zoomLevel;
            IImage image;
            _lastestZoomLevel = zoomLevel;
            byte[] imageArray;
            lock (_imageCache)
            {
                imageArray = (byte[])_imageCache[key];
            }
            if (imageArray == null)
            {
                ImageTileIndex needToDownloadImageTileIndex
                        = new ImageTileIndex();
                needToDownloadImageTileIndex.MapType = mtype;
                needToDownloadImageTileIndex.XIndex = x;
                needToDownloadImageTileIndex.YIndex = y;
                needToDownloadImageTileIndex.MapZoomLevel = zoomLevel;
                AddToImageDownloadList(needToDownloadImageTileIndex);
                image = TileDownloading;
            }
            else
            {
                image = MapLayer.GetAbstractGraphicsFactory().CreateImage(imageArray, 0, imageArray.Length);
            }
            return image;
        }

        //The follow are configuration variables
        /**
         *  the maximum number of map tile to be downloaded in the queue
         */
        private const int MAX_DOWNLOAD_MAP_TILE = 32;
        /**
         * cache size.
         */
        private const int MAX_MAP_TILES_NUMBERS = 256;

        /**
         * Max sizes in the image cache.
         */
        private readonly long _maxBytesInCache;

        /**
         * maximum image download worker thread size
         */
        private readonly int _maxImageDownloadWorkder;

        /**
         * is cache on or not.
         */
        private readonly bool _isCacheOn;

        /**
         * Disable route render or not.
         */
        private readonly bool _drawRoute;

        /**
         * worker thread array ,the extra one is mapdirectionrendererworker
         */
        private readonly MapTileDownloadWorker[] _imageDownloadWorkers;
        /**
         * This image cache stores map tiles downloaded
         */
        private readonly Hashtable _imageCache = new Hashtable(MAX_MAP_TILES_NUMBERS + 1);
        /**
         * the download map tile list.
         */
        private readonly Hashtable _imageTileDownloadList = new Hashtable(MAX_DOWNLOAD_MAP_TILE);


        private readonly object _threadListMutex = new object();
        private readonly IReaderListener _mapDownloadingListener;
        internal IMapTileReadyListener _mapTileReadyListener;

        private readonly Hashtable _threadLists = new Hashtable();

        private readonly Hashtable _assignedImageTileDownloadList = new Hashtable();
        private readonly object _assignedImageTileDownloadListMutex = new object();

        private readonly Hashtable _assignedMapDirectionRenderList = new Hashtable();
        private readonly object _assignedMapDirectionRenderListMutex = new object();


        private volatile bool _stopDownloadManager;
        private readonly Thread _mapTileDownloadManagerThread;
        private volatile int _lastestZoomLevel = -1;

        /**
         * route direction renderer.
         */
        private readonly MapDirectionRenderer _mapDirectionRenderer = new MapDirectionRenderer();

        /**
         * record store
         */
        //private static RecordStore mapDataRecordStore = null;
        /**
         * record store name
         */
        //private static string MAP_DATA_RECORDSTORE_NAME = "Guidebee_MapData";
        /**
         * thread sync object.
         */
        private readonly object _syncObjectManager = new object();

        private static readonly byte[] ImageDownloadingArray = new byte[]{
         0x89,  0x50,  0x4e,  0x47,  0x0d,  0x0a,  0x1a,  0x0a,
         0x00,  0x00,  0x00,  0x0d,  0x49,  0x48,  0x44,  0x52,
         0x00,  0x00,  0x00,  0x40,  0x00,  0x00,  0x00,  0x40,
         0x04,  0x03,  0x00,  0x00,  0x00,  0x58,  0x47,  0x6c,
         0xed,  0x00,  0x00,  0x00,  0x30,  0x50,  0x4c,  0x54,
         0x45,  0xab,  0xa6,  0x9c,  0xaf,  0xaa,  0xa2,  0xb6,
         0xb2,  0xaa,  0xc1,  0xbc,  0xb6,  0xc7,  0xc3,  0xbb,
         0xcd,  0xcb,  0xc5,  0xd5,  0xd1,  0xcc,  0xdc,  0xda,
         0xd5,  0xe1,  0xdf,  0xdc,  0xe7,  0xe4,  0xdf,  0xeb,
         0xea,  0xe7,  0xf0,  0xed,  0xe8,  0xf0,  0xef,  0xee,
         0xf5,  0xf5,  0xf3,  0xfa,  0xfa,  0xf9,  0xff,  0xff,
         0xff,  0xfd,  0xa4,  0xa8,  0x3f,  0x00,  0x00,  0x00,
         0xc9,  0x49,  0x44,  0x41,  0x54,  0x48,  0xc7,  0x63,
         0xf8,  0xff,  0x7f,  0xf7,  0x6e,  0x7c,  0x98,  0x81,
         0x0e,  0x0a,  0xf0,  0x4b,  0xff,  0xff,  0x4f,  0x0f,
         0x05,  0xa3,  0xe1,  0x30,  0x1a,  0x0e,  0x38,  0xc3,
         0xe1,  0x5f,  0x12,  0x83,  0x50,  0x2c,  0x3e,  0x5f,
         0x1c,  0x62,  0x60,  0x30,  0x33,  0xca,  0xc6,  0xad,
         0xe0,  0x8f,  0x22,  0x83,  0xe5,  0xff,  0x7f,  0xee,
         0xb8,  0x15,  0x3c,  0x12,  0x64,  0x01,  0xd2,  0x3b,
         0x71,  0x87,  0x83,  0x03,  0x83,  0x3e,  0x3e,  0x5f,
         0x7c,  0x5f,  0x2b,  0xc0,  0xd0,  0x0f,  0xa2,  0xcf,
         0xe3,  0x50,  0xf0,  0x55,  0x90,  0x81,  0x61,  0xd6,
         0xff,  0xdd,  0xbb,  0x02,  0xed,  0x71,  0x28,  0xf8,
         0xa7,  0xc8,  0xc0,  0x20,  0xd8,  0xbf,  0x7b,  0x92,
         0xa0,  0x18,  0xae,  0x70,  0x28,  0x10,  0x14,  0x14,
         0xdf,  0xfd,  0x0b,  0xa8,  0x6c,  0x3d,  0x0e,  0x5f,
         0x6c,  0x15,  0x14,  0x9e,  0xbd,  0xbb,  0x11,  0x68,
         0x91,  0x1e,  0x2e,  0x6f,  0x1a,  0xc6,  0xed,  0xde,
         0xfd,  0x6b,  0x22,  0x03,  0x83,  0x28,  0x2e,  0x05,
         0xc7,  0x41,  0x74,  0x23,  0x83,  0xe4,  0x72,  0xbc,
         0xe9,  0x41,  0x41,  0x30,  0x1f,  0x67,  0x6c,  0x82,
         0xc2,  0x70,  0x0b,  0x83,  0xd0,  0x7b,  0x9c,  0x0a,
         0xb6,  0x7b,  0xfd,  0xff,  0x6b,  0xc0,  0xa0,  0x83,
         0x27,  0x3d,  0x24,  0x9a,  0x19,  0x32,  0x30,  0x9d,
         0xc7,  0x93,  0x1e,  0xfe,  0x3a,  0x32,  0x08,  0xd7,
         0x8d,  0xe6,  0x8b,  0xd1,  0xf2,  0x61,  0xc4,  0x87,
         0x03,  0x00,  0x95,  0x74,  0xb0,  0xed,  0x65,  0x48,
         0x6d,  0x06,  0x00,  0x00,  0x00,  0x00,  0x49,  0x45,
         0x4e,  0x44,  0xae,  0x42,  0x60,  0x82};

        private static readonly byte[] ImageNoavaiableArray = new byte[]{
         0x89,  0x50,  0x4e,  0x47,  0x0d,  0x0a,  0x1a,  0x0a,
         0x00,  0x00,  0x00,  0x0d,  0x49,  0x48,  0x44,  0x52,
         0x00,  0x00,  0x01,  0x00,  0x00,  0x00,  0x01,  0x00,
         0x08,  0x03,  0x00,  0x00,  0x00,  0x6b,  0xac,  0x58,
         0x54,  0x00,  0x00,  0x00,  0x04,  0x67,  0x41,  0x4d,
         0x41,  0x00,  0x00,  0xb1,  0x8e,  0x7c,  0xfb,  0x51,
         0x93,  0x00,  0x00,  0x00,  0x20,  0x63,  0x48,  0x52,
         0x4d,  0x00,  0x00,  0x7a,  0x25,  0x00,  0x00,  0x80,
         0x83,  0x00,  0x00,  0xf9,  0xff,  0x00,  0x00,  0x80,
         0xe6,  0x00,  0x00,  0x75,  0x2e,  0x00,  0x00,  0xea,
         0x5f,  0x00,  0x00,  0x3a,  0x97,  0x00,  0x00,  0x17,
         0x6f,  0x69,  0xe4,  0xc4,  0x2b,  0x00,  0x00,  0x03,
         0x00,  0x50,  0x4c,  0x54,  0x45,  0xc2,  0xc4,  0xc2,
         0xc6,  0xa2,  0x9e,  0xd9,  0xda,  0xd9,  0xcb,  0xca,
         0xc8,  0xd1,  0xa6,  0x99,  0xd5,  0xa8,  0x99,  0xca,
         0xa8,  0xa1,  0xd5,  0xb2,  0xa3,  0xc2,  0x9d,  0x9a,
         0xca,  0xbb,  0xb8,  0xd2,  0xad,  0xa1,  0xd8,  0xaa,
         0x99,  0xd7,  0xb5,  0xa5,  0xcd,  0xd0,  0xce,  0xd6,
         0xc2,  0xbb,  0xd6,  0xbc,  0xb3,  0xd8,  0xb8,  0xa6,
         0xd5,  0xb5,  0xaa,  0xd7,  0xc9,  0xc3,  0xc7,  0xb0,
         0xad,  0xc0,  0xc0,  0xc0,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,  0x00,
         0x00,  0x00,  0x00,  0x00,  0x00,  0x46,  0xe4,  0xc4,
         0xcb,  0x00,  0x00,  0x00,  0x15,  0x74,  0x52,  0x4e,
         0x53,  0xff,  0xff,  0xff,  0xff,  0xff,  0xff,  0xff,
         0xff,  0xff,  0xff,  0xff,  0xff,  0xff,  0xff,  0xff,
         0xff,  0xff,  0xff,  0xff,  0xff,  0x00,  0x2b,  0xd9,
         0x7d,  0xea,  0x00,  0x00,  0x0a,  0x03,  0x49,  0x44,
         0x41,  0x54,  0x78,  0x9c,  0x62,  0x10,  0x19,  0xe1,
         0x00,  0x20,  0x80,  0x18,  0x06,  0xda,  0x01,  0x03,
         0x0d,  0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,
         0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,
         0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,
         0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,
         0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,
         0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,
         0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,
         0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,
         0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,
         0x7c,  0x00,  0x00,  0x04,  0x10,  0x9e,  0x00,  0x18,
         0x19,  0x61,  0x03,  0x10,  0x40,  0x23,  0x3e,  0x00,
         0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,
         0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,
         0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,
         0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,
         0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,
         0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,
         0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,
         0x00,  0x08,  0xa0,  0x91,  0xe1,  0x4b,  0x3c,  0x00,
         0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,
         0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,
         0x00,  0x10,  0x40,  0xe4,  0x06,  0x00,  0x2f,  0x55,
         0x5d,  0x31,  0x80,  0xf6,  0x03,  0x04,  0x10,  0x79,
         0x01,  0xc0,  0xcb,  0x8c,  0x0e,  0xa8,  0xe5,  0x1e,
         0xba,  0xdb,  0x0f,  0x10,  0x40,  0x64,  0x05,  0x00,
         0x2f,  0x27,  0x23,  0x1a,  0xe0,  0x10,  0x26,  0xdb,
         0x05,  0x03,  0x6c,  0x3f,  0x40,  0x00,  0x91,  0x13,
         0x00,  0xbc,  0x9c,  0x1c,  0x1c,  0xe8,  0x0e,  0xa0,
         0x67,  0x08,  0x50,  0xd5,  0x7e,  0x80,  0x00,  0x22,
         0x23,  0x00,  0x40,  0xf6,  0xb3,  0x09,  0xa0,  0x02,
         0x76,  0x3a,  0x86,  0x00,  0x75,  0xed,  0x07,  0x08,
         0x20,  0xd2,  0x03,  0x00,  0x9b,  0xfd,  0xf4,  0x0c,
         0x01,  0x2a,  0xdb,  0x0f,  0x10,  0x40,  0x24,  0x07,
         0x00,  0xd4,  0x7e,  0x76,  0x6e,  0x64,  0xc0,  0x4e,
         0xbf,  0x10,  0xa0,  0xb6,  0xfd,  0x00,  0x01,  0x44,
         0x6a,  0x00,  0x60,  0xb5,  0x9f,  0x9b,  0x95,  0x8b,
         0x5e,  0x21,  0x40,  0x75,  0xfb,  0x01,  0x02,  0x88,
         0xc4,  0x00,  0xc0,  0x6a,  0x3f,  0x2b,  0x10,  0x70,
         0xf3,  0x08,  0x70,  0xd1,  0x21,  0x04,  0xa8,  0x6f,
         0x3f,  0x40,  0x00,  0x91,  0x16,  0x00,  0xd8,  0xec,
         0x67,  0x85,  0x00,  0x2e,  0x1e,  0x1e,  0xda,  0x87,
         0x00,  0x0d,  0xec,  0x07,  0x08,  0x20,  0x92,  0x02,
         0x00,  0x6c,  0x3f,  0x0f,  0xc8,  0x7e,  0x1e,  0x3e,
         0x14,  0xeb,  0x05,  0xf9,  0xe9,  0x92,  0x06,  0x68,
         0x61,  0x3f,  0x40,  0x00,  0x91,  0x12,  0x00,  0x10,
         0xfb,  0x79,  0x80,  0xf6,  0xb3,  0x33,  0x31,  0xf1,
         0xb1,  0x22,  0x42,  0x5f,  0x90,  0x89,  0x89,  0x9f,
         0x85,  0xf6,  0x69,  0x80,  0x26,  0xf6,  0x03,  0x04,
         0x10,  0x09,  0x01,  0x80,  0x62,  0x3f,  0xc8,  0x05,
         0xac,  0x08,  0xfb,  0xc1,  0x2e,  0xe0,  0x66,  0xa7,
         0x69,  0x08,  0xd0,  0xc6,  0x7e,  0x80,  0x00,  0x22,
         0x3e,  0x00,  0xd0,  0xec,  0x47,  0xb8,  0x40,  0x10,
         0xc2,  0x05,  0xa5,  0x42,  0xa0,  0x0b,  0x68,  0xd6,
         0x2a,  0xa6,  0x91,  0xfd,  0x00,  0x01,  0x44,  0x42,
         0x00,  0x08,  0x43,  0xed,  0xe7,  0x66,  0x62,  0x42,
         0x72,  0x01,  0x8b,  0x20,  0x8c,  0x2b,  0x08,  0x72,
         0x01,  0x3b,  0x17,  0x07,  0x23,  0x6d,  0x7a,  0x46,
         0x34,  0xb2,  0x1f,  0x20,  0x80,  0x48,  0x0c,  0x00,
         0x70,  0xf9,  0xcb,  0x8f,  0xe4,  0x02,  0x84,  0xfd,
         0x42,  0x40,  0x0e,  0x0b,  0x17,  0x8d,  0x03,  0x80,
         0xfa,  0xf6,  0x03,  0x04,  0x10,  0x69,  0x01,  0xc0,
         0xce,  0x05,  0x2e,  0x79,  0x10,  0x2e,  0x40,  0xb6,
         0x9f,  0x05,  0x08,  0x58,  0x69,  0x1b,  0x00,  0x34,
         0xb0,  0x1f,  0x20,  0x80,  0x48,  0x0e,  0x00,  0x70,
         0xb6,  0x83,  0xbb,  0x40,  0x08,  0xce,  0x60,  0x61,
         0xa1,  0x4f,  0x00,  0x50,  0xdb,  0x7e,  0x80,  0x00,
         0x22,  0x35,  0x00,  0xa0,  0x05,  0x0f,  0xdc,  0x05,
         0xa8,  0xf6,  0xd3,  0x3e,  0x00,  0xa8,  0x6e,  0x3f,
         0x40,  0x00,  0x91,  0x19,  0x00,  0x68,  0x2e,  0x80,
         0xd9,  0x0f,  0xac,  0x8c,  0xe9,  0x13,  0x00,  0xd4,
         0xb3,  0x1f,  0x20,  0x80,  0xc8,  0xcb,  0x02,  0xc0,
         0xf2,  0x86,  0x1f,  0x9b,  0xfd,  0xb4,  0x0f,  0x00,
         0xaa,  0xdb,  0x0f,  0x10,  0x40,  0xe4,  0xa6,  0x00,
         0xe4,  0x38,  0x40,  0xd8,  0x0f,  0x2c,  0x86,  0xe9,
         0x94,  0x02,  0xa8,  0x66,  0x3f,  0x40,  0x00,  0x91,
         0x16,  0x00,  0x5c,  0x08,  0x07,  0x20,  0xca,  0x5f,
         0x60,  0x59,  0x4c,  0xaf,  0x00,  0xa0,  0x81,  0xfd,
         0x00,  0x01,  0x44,  0x6e,  0x00,  0x20,  0xd9,  0x8f,
         0xe2,  0x02,  0x2e,  0x36,  0xfa,  0x04,  0x00,  0xd5,
         0xec,  0x07,  0x08,  0x20,  0x32,  0x03,  0x00,  0xc5,
         0x7e,  0x64,  0x17,  0xd0,  0x29,  0x00,  0xa8,  0x67,
         0x3f,  0x40,  0x00,  0x91,  0x14,  0x00,  0x8c,  0x30,
         0x07,  0xa0,  0xd9,  0x8f,  0xe4,  0x02,  0x9a,  0x66,
         0x01,  0x5a,  0xd8,  0x0f,  0x10,  0x40,  0x64,  0x05,
         0x00,  0xc2,  0x7e,  0x5e,  0x74,  0x17,  0xd0,  0x25,
         0x00,  0xa8,  0x69,  0x3f,  0x40,  0x00,  0x91,  0x11,
         0x00,  0x2c,  0x48,  0xed,  0x4f,  0x16,  0x7e,  0x34,
         0x17,  0xd0,  0x21,  0x00,  0xa8,  0x6b,  0x3f,  0x40,
         0x00,  0x91,  0x5c,  0x06,  0xb0,  0xa0,  0xd8,  0xcf,
         0x82,  0xee,  0x02,  0x9a,  0xd7,  0x02,  0xd4,  0xb6,
         0x1f,  0x20,  0x80,  0x48,  0x4e,  0x01,  0xc0,  0xc6,
         0x06,  0xb2,  0xfd,  0x48,  0x2e,  0x10,  0x64,  0xa1,
         0x75,  0x6f,  0x90,  0x16,  0xf6,  0x03,  0x04,  0x10,
         0x89,  0x03,  0x22,  0x60,  0x17,  0x08,  0x22,  0xdb,
         0x0f,  0x77,  0x01,  0x1f,  0xc4,  0x7e,  0x46,  0x0e,
         0x36,  0x92,  0xfd,  0x36,  0x90,  0xf6,  0x03,  0x04,
         0x10,  0xa9,  0x43,  0x62,  0xec,  0x5c,  0x5c,  0x50,
         0x17,  0x20,  0xda,  0x5f,  0xfc,  0xa8,  0xf6,  0xd3,
         0x6a,  0xa6,  0x98,  0x36,  0xf6,  0x03,  0x04,  0x10,
         0xc9,  0x83,  0xa2,  0xec,  0xec,  0x10,  0x17,  0x20,
         0xb7,  0x3f,  0xf9,  0x21,  0xf6,  0xb3,  0x43,  0xec,
         0xa7,  0xd9,  0xd2,  0x01,  0x9a,  0xd8,  0x0f,  0x10,
         0x40,  0xa4,  0x0f,  0x8b,  0x43,  0x5c,  0x80,  0x6c,
         0x3f,  0xd0,  0x05,  0xf4,  0xf0,  0x3f,  0x6d,  0xec,
         0x07,  0x08,  0x20,  0x32,  0x26,  0x46,  0xc0,  0x2e,
         0xc0,  0x04,  0xe0,  0x01,  0x49,  0x9a,  0xfa,  0x9f,
         0x26,  0xf6,  0x03,  0x04,  0x10,  0x39,  0x53,  0x63,
         0x58,  0x5d,  0xc0,  0xce,  0xc3,  0x4e,  0x73,  0xff,
         0xd3,  0xc2,  0x7e,  0x80,  0x00,  0x22,  0x6b,  0x72,
         0x94,  0x07,  0xd3,  0x05,  0xf4,  0xf1,  0x3f,  0x0d,
         0xec,  0x07,  0x08,  0x20,  0xf2,  0xa6,  0xc7,  0x79,
         0x78,  0xb8,  0x06,  0xc6,  0xff,  0xd4,  0xb7,  0x1f,
         0x20,  0x80,  0xc8,  0x5c,  0x20,  0xc1,  0xc3,  0xc3,
         0x8e,  0x0c,  0x78,  0x04,  0xe8,  0xe5,  0x7f,  0xaa,
         0xdb,  0x0f,  0x10,  0x40,  0x64,  0x2e,  0x91,  0xe1,
         0xe2,  0x41,  0x05,  0xf4,  0xf3,  0x3f,  0xb5,  0xed,
         0x07,  0x08,  0x20,  0x2a,  0x2d,  0x92,  0xa2,  0xa3,
         0xff,  0xa9,  0x6c,  0x3f,  0x40,  0x00,  0x91,  0xb9,
         0x4c,  0x8e,  0x03,  0x1d,  0xd0,  0xd1,  0xff,  0xd4,
         0xb5,  0x1f,  0x20,  0x80,  0xc8,  0x5c,  0x28,  0xc9,
         0x89,  0x0e,  0xe8,  0xe9,  0x7f,  0xaa,  0xda,  0x0f,
         0x10,  0x40,  0x64,  0x2e,  0x95,  0xe5,  0x45,  0x07,
         0x64,  0x5a,  0x4f,  0x2e,  0xa0,  0x9e,  0xfd,  0x00,
         0x01,  0x34,  0xe2,  0x17,  0x4b,  0x03,  0x04,  0xd0,
         0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,
         0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,
         0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,
         0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,
         0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,
         0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,
         0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,
         0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,
         0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,
         0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,
         0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,
         0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,
         0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,
         0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,
         0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,
         0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,
         0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,
         0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,
         0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,
         0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,
         0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,
         0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,
         0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,
         0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,
         0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,
         0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,
         0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,
         0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,
         0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,
         0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,
         0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,
         0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,
         0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,
         0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,
         0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,
         0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,
         0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,
         0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,
         0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,
         0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,
         0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,
         0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,
         0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,
         0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,
         0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,
         0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,
         0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,
         0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,
         0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,
         0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,
         0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,
         0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,
         0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,
         0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,
         0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,
         0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,
         0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,
         0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,
         0x00,  0x02,  0x88,  0xdc,  0x00,  0xa0,  0xf7,  0x29,
         0xaa,  0x34,  0xb3,  0x1f,  0x20,  0x80,  0xc8,  0x3c,
         0x56,  0x97,  0x19,  0x1d,  0x50,  0xcb,  0x3d,  0x74,
         0xb7,  0x1f,  0x20,  0x80,  0xc8,  0x3c,  0x58,  0x19,
         0xe3,  0x60,  0x67,  0xba,  0xdd,  0x3d,  0x4f,  0x6d,
         0xfb,  0x01,  0x02,  0x88,  0x4a,  0x47,  0x6b,  0xd3,
         0xed,  0xee,  0x79,  0xaa,  0xdb,  0x0f,  0x10,  0x40,
         0x64,  0x1e,  0xae,  0xce,  0x26,  0x80,  0x0a,  0xe8,
         0x75,  0xf7,  0x3c,  0xf5,  0xed,  0x07,  0x08,  0x20,
         0x32,  0x8f,  0xd7,  0x17,  0x40,  0x07,  0xf4,  0x0b,
         0x01,  0x2a,  0xdb,  0x0f,  0x10,  0x40,  0xe4,  0x5d,
         0xb0,  0x80,  0x7e,  0xff,  0x3b,  0x3b,  0xfd,  0x42,
         0x80,  0xda,  0xf6,  0x03,  0x04,  0x10,  0x59,  0x57,
         0x6c,  0xa0,  0xdb,  0xcf,  0xcd,  0xca,  0x45,  0xaf,
         0x10,  0xa0,  0xba,  0xfd,  0x00,  0x01,  0x44,  0xce,
         0x25,  0x2b,  0xe8,  0xf6,  0x83,  0x2e,  0x80,  0xa3,
         0xc3,  0xdd,  0xf3,  0xb4,  0xb1,  0x1f,  0x20,  0x80,
         0xc8,  0xb8,  0x66,  0x07,  0xcd,  0x7e,  0xe8,  0x15,
         0x88,  0x34,  0xbf,  0x7b,  0x9e,  0x46,  0xf6,  0x03,
         0x04,  0x10,  0xe9,  0x17,  0x2d,  0xf1,  0x80,  0xec,
         0xe7,  0xe1,  0x43,  0xb1,  0x5e,  0x90,  0x9f,  0x2e,
         0x69,  0x80,  0x16,  0xf6,  0x03,  0x04,  0x10,  0xc9,
         0x57,  0x6d,  0xc1,  0xee,  0x7f,  0xe7,  0x63,  0x45,
         0x84,  0xbe,  0x20,  0x13,  0x13,  0x3f,  0x0b,  0xed,
         0xd3,  0x00,  0x4d,  0xec,  0x07,  0x08,  0x20,  0x52,
         0x2f,  0x5b,  0x83,  0xd9,  0x0f,  0xbd,  0xfb,  0x1d,
         0x66,  0x3f,  0xd8,  0x05,  0xdc,  0xec,  0x34,  0x0d,
         0x01,  0xda,  0xd8,  0x0f,  0x10,  0x40,  0x24,  0x5e,
         0xb7,  0x87,  0xb0,  0x1f,  0xe1,  0x02,  0xe8,  0xed,
         0x77,  0xa0,  0x54,  0x08,  0xbe,  0xee,  0x8a,  0x46,
         0x21,  0x40,  0x23,  0xfb,  0x01,  0x02,  0x88,  0xb4,
         0x1b,  0x27,  0xc1,  0xf6,  0x73,  0xc3,  0xef,  0x78,
         0x44,  0xbd,  0xff,  0x53,  0x10,  0xe4,  0x02,  0xda,
         0xde,  0x3d,  0x4e,  0x03,  0xfb,  0x01,  0x02,  0x88,
         0xc4,  0x00,  0x00,  0x97,  0xbf,  0xfc,  0x48,  0x2e,
         0x40,  0xba,  0xff,  0x12,  0x74,  0x1b,  0x28,  0x6d,
         0x6f,  0x9c,  0xa4,  0x85,  0xfd,  0x00,  0x01,  0x44,
         0xf2,  0xdd,  0xe3,  0xa0,  0x92,  0x07,  0xe9,  0x9e,
         0x4f,  0x24,  0xfb,  0x41,  0x57,  0x5e,  0xd1,  0xfc,
         0xf6,  0x79,  0xaa,  0xdb,  0x0f,  0x10,  0x40,  0xe4,
         0x5d,  0xbe,  0x0e,  0x77,  0x81,  0x10,  0x9c,  0x01,
         0xb9,  0xf3,  0x8b,  0xf6,  0x01,  0x40,  0x6d,  0xfb,
         0x01,  0x02,  0x88,  0xcc,  0xcb,  0xd7,  0x91,  0xef,
         0x7e,  0x47,  0xb2,  0x9f,  0xf6,  0x01,  0x40,  0x75,
         0xfb,  0x01,  0x02,  0x88,  0xdc,  0xdb,  0xe7,  0xf9,
         0xb1,  0xda,  0x0f,  0xac,  0x8c,  0xe9,  0x74,  0xfb,
         0x3c,  0xd5,  0xec,  0x07,  0x08,  0x20,  0xf2,  0xb2,
         0x00,  0xb0,  0xbc,  0xe1,  0xc7,  0x66,  0x3f,  0xed,
         0x03,  0x80,  0xea,  0xf6,  0x03,  0x04,  0x10,  0xb9,
         0x29,  0x00,  0x39,  0x0e,  0x90,  0xef,  0xff,  0xa4,
         0x57,  0x0a,  0xa0,  0x9a,  0xfd,  0x00,  0x01,  0x44,
         0xe6,  0xed,  0xf3,  0x28,  0xf7,  0x9f,  0xf3,  0xd1,
         0x2d,  0x00,  0x68,  0x60,  0x3f,  0x40,  0x00,  0x91,
         0x1b,  0x00,  0x28,  0xf7,  0xbf,  0x23,  0xb9,  0x80,
         0x8b,  0x8d,  0x3e,  0x01,  0x40,  0x35,  0xfb,  0x01,
         0x02,  0x88,  0xcc,  0x00,  0x40,  0xb1,  0x1f,  0xd9,
         0x05,  0x74,  0x0a,  0x00,  0xea,  0xd9,  0x0f,  0x10,
         0x40,  0x64,  0xdc,  0x3e,  0x8f,  0x69,  0x3f,  0x92,
         0x0b,  0xe8,  0x70,  0xfb,  0x3c,  0x75,  0xed,  0x07,
         0x08,  0x20,  0xb2,  0x02,  0x00,  0x61,  0x3f,  0x2f,
         0xba,  0x0b,  0xe8,  0x12,  0x00,  0xd4,  0xb4,  0x1f,
         0x20,  0x80,  0xc8,  0x08,  0x00,  0x16,  0xa4,  0xf6,
         0x27,  0x0b,  0x3f,  0x9a,  0x0b,  0xe8,  0x10,  0x00,
         0xd4,  0xb5,  0x1f,  0x20,  0x80,  0x48,  0x2e,  0x03,
         0x58,  0x50,  0xec,  0x67,  0x41,  0x77,  0x01,  0xcd,
         0x6b,  0x01,  0x6a,  0xdb,  0x0f,  0x10,  0x40,  0x24,
         0xa7,  0x00,  0x60,  0x63,  0x03,  0xd9,  0x7e,  0x24,
         0x17,  0x08,  0xb2,  0xd0,  0xba,  0x37,  0x48,  0x0b,
         0xfb,  0x01,  0x02,  0x88,  0xc4,  0x01,  0x11,  0xb0,
         0x0b,  0x04,  0x91,  0xed,  0x87,  0xbb,  0x80,  0x0f,
         0x62,  0x3f,  0x23,  0x07,  0x1b,  0xc9,  0x7e,  0x1b,
         0x48,  0xfb,  0x01,  0x02,  0x88,  0xd4,  0x21,  0x31,
         0x76,  0x2e,  0x2e,  0xa8,  0x0b,  0x10,  0xed,  0x2f,
         0x7e,  0x54,  0xfb,  0x69,  0x35,  0x53,  0x4c,  0x1b,
         0xfb,  0x01,  0x02,  0x88,  0xe4,  0x41,  0x51,  0xf0,
         0xcd,  0xe7,  0x82,  0xa8,  0xed,  0x4f,  0x7e,  0x88,
         0xfd,  0xec,  0xec,  0xb4,  0xbd,  0x7f,  0x9e,  0x26,
         0xf6,  0x03,  0x04,  0x10,  0xe9,  0xc3,  0xe2,  0x10,
         0x17,  0x20,  0xdb,  0x0f,  0x74,  0x01,  0x3d,  0xfc,
         0x4f,  0x1b,  0xfb,  0x01,  0x02,  0x88,  0x8c,  0x89,
         0x11,  0x76,  0xcc,  0xbb,  0xdf,  0x41,  0x00,  0x3c,
         0x20,  0x49,  0xe3,  0xfb,  0x97,  0x69,  0x60,  0x3f,
         0x40,  0x00,  0x91,  0x33,  0x35,  0x86,  0xd5,  0x05,
         0xf4,  0xb9,  0x7f,  0x9e,  0xfa,  0xf6,  0x03,  0x04,
         0x10,  0x59,  0x93,  0xa3,  0x3c,  0x98,  0x2e,  0xa0,
         0x8f,  0xff,  0x69,  0x60,  0x3f,  0x40,  0x00,  0x91,
         0x37,  0x3d,  0xce,  0xc3,  0xc3,  0x35,  0x30,  0xfe,
         0xa7,  0xbe,  0xfd,  0x00,  0x01,  0x44,  0xe6,  0x02,
         0x09,  0x1e,  0x1e,  0x76,  0x64,  0xc0,  0x23,  0x40,
         0x2f,  0xff,  0x53,  0xdd,  0x7e,  0x80,  0x00,  0x22,
         0x73,  0x89,  0x0c,  0x17,  0x0f,  0x2a,  0xa0,  0x9f,
         0xff,  0xa9,  0x6d,  0x3f,  0x40,  0x00,  0x51,  0x69,
         0x91,  0x14,  0x1d,  0xfd,  0x4f,  0x65,  0xfb,  0x01,
         0x02,  0x88,  0xcc,  0x65,  0x72,  0x1c,  0xe8,  0x80,
         0x8e,  0xfe,  0xa7,  0xae,  0xfd,  0x00,  0x01,  0x44,
         0xe6,  0x42,  0x49,  0x4e,  0x74,  0x40,  0x4f,  0xff,
         0x53,  0xd5,  0x7e,  0x80,  0x00,  0x22,  0x73,  0xa9,
         0x2c,  0x2f,  0x3a,  0x20,  0xd3,  0x7a,  0x72,  0x01,
         0xf5,  0xec,  0x07,  0x08,  0xa0,  0x11,  0xbf,  0x58,
         0x1a,  0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,
         0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,
         0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,
         0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,
         0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,
         0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,
         0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,
         0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,
         0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,
         0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,
         0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,
         0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,
         0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,
         0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,
         0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,
         0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,
         0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,
         0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,
         0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,
         0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,
         0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,  0x8d,  0xf8,
         0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,  0x00,  0x00,
         0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,  0x80,  0x46,
         0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,  0x0f,  0x00,
         0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,  0x10,  0x40,
         0x23,  0x3e,  0x00,  0x00,  0x02,  0x68,  0xc4,  0x07,
         0x00,  0x40,  0x00,  0x8d,  0xf8,  0x00,  0x00,  0x08,
         0xa0,  0x11,  0x1f,  0x00,  0x00,  0x01,  0x34,  0xe2,
         0x03,  0x00,  0x20,  0x80,  0x46,  0x7c,  0x00,  0x00,
         0x04,  0xd0,  0x88,  0x0f,  0x00,  0x80,  0x00,  0x1a,
         0xf1,  0x01,  0x00,  0x10,  0x40,  0x23,  0x3e,  0x00,
         0x00,  0x02,  0x68,  0xc4,  0x07,  0x00,  0x40,  0x00,
         0x8d,  0xf8,  0x00,  0x00,  0x08,  0xa0,  0x11,  0x1f,
         0x00,  0x00,  0x01,  0x34,  0xe2,  0x03,  0x00,  0x20,
         0x80,  0x46,  0x7c,  0x00,  0x00,  0x04,  0xd0,  0x88,
         0x0f,  0x00,  0x80,  0x00,  0x1a,  0xf1,  0x01,  0x00,
         0x10,  0x40,  0x23,  0x3e,  0x00,  0x00,  0x02,  0x0c,
         0x00,  0xab,  0x7e,  0xc9,  0x13,  0x29,  0xcc,  0x47,
         0xe1,  0x00,  0x00,  0x00,  0x00,  0x49,  0x45,  0x4e,
         0x44,  0xae,  0x42,  0x60,  0x82};

        /**
         * Intialized the images.
         */
        static MapTileDownloadManager()
        {
            try
            {
                TileNotAvaiable = MapLayer.GetAbstractGraphicsFactory()
                        .CreateImage(new MemoryStream(ImageNoavaiableArray));
                TileDownloading = MapLayer.GetAbstractGraphicsFactory()
                        .CreateImage(new MemoryStream(ImageDownloadingArray));
            }
            catch (Exception)
            {

            }

        }
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add one map tile index to the assigned download list.
         * @param imageTileIndex
         */
        private void AddToAssignedImageDownloadList(ImageTileIndex imageTileIndex)
        {
            lock (_assignedImageTileDownloadListMutex)
            {
                string key = imageTileIndex.MapType + "|" +
                        imageTileIndex.XIndex + "|" +
                        imageTileIndex.YIndex + "|" +
                        imageTileIndex.MapZoomLevel;
                _assignedImageTileDownloadList[key]= imageTileIndex;
            }
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add one map tile index to the download list.
         * @param imageTileIndex
         */
        private void AddToImageDownloadList(ImageTileIndex imageTileIndex)
        {
            lock (_assignedImageTileDownloadListMutex)
            {
                string key = imageTileIndex.MapType + "|" +
                        imageTileIndex.XIndex + "|" +
                        imageTileIndex.YIndex + "|" +
                        imageTileIndex.MapZoomLevel;
                object object3 = _imageCache[key];
                object object1 = _imageTileDownloadList[key];
                object object2 = _assignedImageTileDownloadList[key];

                if (object3 == null && object1 == null && object2 == null)
                {

                    ImageTileIndex newImagetileIndex = new ImageTileIndex();
                    newImagetileIndex.MapType = imageTileIndex.MapType;
                    newImagetileIndex.XIndex = imageTileIndex.XIndex;
                    newImagetileIndex.YIndex = imageTileIndex.YIndex;
                    newImagetileIndex.MapZoomLevel = imageTileIndex.MapZoomLevel;

                    _imageTileDownloadList.Add(key, newImagetileIndex);
                    lock (_syncObjectManager)
                    {
                        Monitor.Pulse(_syncObjectManager);

                    }
                }
            }
        }

        private void OneDownloadImageTileDone(string key)
        {
            lock (_assignedImageTileDownloadListMutex)
            {
                _assignedImageTileDownloadList.Remove(key);
            }
        }

        private ImageTileIndex GetAImageTileIndex()
        {
            lock (_assignedImageTileDownloadListMutex)
            {
                ImageTileIndex imageTileIndex = null;
                if (_imageTileDownloadList.Count > 0)
                {
                    ICollection keys = _imageTileDownloadList.Keys;
                    foreach (object key in keys)
                    {
                        imageTileIndex =
                            (ImageTileIndex)_imageTileDownloadList[key];
                        _imageTileDownloadList.Remove(key);
                        break;
                    }


                }
                return imageTileIndex;

            }
        }

        private void AddToImageCache(string key, byte[] imageArray)
        {
            if (_isCacheOn)
            {
                lock (_imageCache)
                {
                    long bytesInCache = 0;
                    foreach (var o in _imageCache.Values)
                    {
                        byte[] array = (byte[]) o;
                        bytesInCache += array.Length;
                    }


                    if (bytesInCache > _maxBytesInCache)
                    {
                        //imageCache.removeHalfElements();
                        _imageCache.Clear();
                    }
                    _imageCache[key] = imageArray;
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////

        /**
         * Save map image cache to persistent memory.
         */
        //private void addOneImageToCacheFromRecordStore(byte[] imageArray) {
        //    ByteArrayInputStream bais = new ByteArrayInputStream(imageArray);
        //    DataInputStream dis = new DataInputStream(bais);
        //    string key = null;
        //    byte[] image;
        //    try {
        //        key = dis.readUTF();
        //        int imageSize = dis.ReadInt();
        //        image = new byte[imageSize];
        //        dis.read(image);

        //        addToImageCache(key, image);

        //    } catch (Exception ingore) {
        //        ingore.printStackTrace();
        //    }

        //}

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * each record has the format of [Key,imageLength,imagedata]
         */
        //private byte[] image2ByteArray(string key, byte[] image) {
        //    byte[] imageArray = null;
        //    try {
        //        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        //        DataOutputStream dos = new DataOutputStream(baos);
        //        dos.writeUTF(key);
        //        dos.writeInt(image.length);
        //        dos.write(image);
        //        imageArray = baos.toByteArray();
        //        dos.close();
        //        baos.close();
        //    } catch (IOException e) {
        //        e.printStackTrace();
        //    }
        //    return imageArray;
        //}

        internal class MapDirectionRendererWorker : MapTileDownloadWorker
        {
            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * Constructor.
             * @param manager the downloade manager instance.
             * @param threadName the thread name.
             */
            internal MapDirectionRendererWorker(MapTileDownloadManager manager)
                : base(manager, manager._mapDirectionRenderer, "MapDirectionRenderer")
            {

            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * get one map tile.
             * @param imageTileIndex the index of given map tile.
             */
            internal override void GetImage(ImageTileIndex imageTileIndex)
            {
                try
                {
                    string key = imageTileIndex.MapType + "|" +
                            imageTileIndex.XIndex + "|" +
                            imageTileIndex.YIndex + "|" +
                            imageTileIndex.MapZoomLevel;
                    IImage image = TileNotAvaiable;
                    if (_mapTileReader is MapDirectionRenderer)
                    {
                        MapDirectionRenderer mapDirectionRenderer = (MapDirectionRenderer)_mapTileReader;
                        //this is a block methods,it returns when the download is done.
                        if (imageTileIndex.MapZoomLevel == _mapTileDownloadManager._lastestZoomLevel)
                        {
                            image = mapDirectionRenderer.GetImage(
                                    imageTileIndex.XIndex, imageTileIndex.YIndex,
                                    imageTileIndex.MapZoomLevel);
                        }
                        if (image == null)
                        {
                            image = TileNotAvaiable;
                        }
                        _mapTileDownloadManager.OneDownloadImageTileDone(key);
                        _mapTileDownloadManager._mapTileReadyListener.Done(imageTileIndex, image);
                    }

                }
                catch (Exception)
                {

                }

            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * the actually thread run.
             */
            public override void Run()
            {
                while (!_stopDownloadWorker)
                {
                    try
                    {

                        lock (_mapTileDownloadManager._assignedMapDirectionRenderListMutex)
                        {
                            if (_mapTileDownloadManager._assignedMapDirectionRenderList.Count > 0)
                            {
                                ImageTileIndex imageTileIndex = null;


                                ICollection keys = _mapTileDownloadManager._assignedMapDirectionRenderList.Keys;
                                foreach (object key in keys)
                                {
                                    imageTileIndex =
                                        (ImageTileIndex)_mapTileDownloadManager._assignedMapDirectionRenderList[key];
                                    _mapTileDownloadManager._assignedMapDirectionRenderList.Remove(key);

                                    break;
                                }

                                GetImage(imageTileIndex);

                            }
                            else
                            {
                                {
                                    try
                                    {
                                        //System.out.println("mapDirectionRenderer paused");
                                        _pauseDownloadWorker = true;
                                        Monitor.Wait(_mapTileDownloadManager._assignedMapDirectionRenderListMutex,
                                                     _maxWaitingTime * 1000);

                                    }
                                    catch (Exception)
                                    {
                                        Thread.CurrentThread.Interrupt();

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                        //catch whatever exception to make sure the thread is not dead.

                    }
                }
            }
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // --------   -------------------  -------------      ----------------------
        // 19JUN2009  James Shen                 	      Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * map download work thread.
         * <p>
         * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
         * @version     1.00, 03/01/09
         * @author      Guidebee, Inc.
         */
        internal class MapTileDownloadWorker
        {

            /**
             * the map tile downloader actually do the download work.
             */
            protected MapTileAbstractReader _mapTileReader;
            /**
             * Download manager object.
             */
            protected MapTileDownloadManager _mapTileDownloadManager;
            protected volatile bool _stopDownloadWorker;
            protected volatile bool _pauseDownloadWorker;
            internal Thread _mapTileDownloadWorkerThread;
            protected string _threadName;
            protected object _syncObjectWorker = new object();
            /**
             * Max wait time for download an image in seconds.
             */
            protected int _maxWaitingTime =60;

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * Constructor.
             * @param manager the downloade manager instance.
             * @param threadName the thread name.
             */
            internal MapTileDownloadWorker(MapTileDownloadManager manager, string threadName)
            {
                _mapTileDownloadManager = manager;
                _threadName = threadName;
                _mapTileDownloadWorkerThread = new Thread(Run);
                _mapTileDownloadWorkerThread.Name = threadName;
                _mapTileReader = new MapTileDownloader();
                _mapTileReader.SetMapDownloadingListener(manager._mapDownloadingListener);
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * Constructor.
             * @param manager the downloade manager instance.
             * @param threadName the thread name.
             */
            internal MapTileDownloadWorker(MapTileDownloadManager manager,
                    MapTileAbstractReader mapTileReader, string threadName)
            {
                _mapTileDownloadManager = manager;
                _threadName = threadName;
                _mapTileDownloadWorkerThread = new Thread(Run);
                _mapTileDownloadWorkerThread.Name = threadName;
                _mapTileReader = mapTileReader;
                _mapTileReader.SetMapDownloadingListener(manager._mapDownloadingListener);
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * start the worker thread.
             */
            public void Start()
            {
                _stopDownloadWorker = false;
                
                _mapTileDownloadWorkerThread.Start();
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * get one map tile.
             * @param imageTileIndex the index of given map tile.
             */
            internal virtual void GetImage(ImageTileIndex imageTileIndex)
            {
                try
                {
                    string key = imageTileIndex.MapType + "|" +
                            imageTileIndex.XIndex + "|" +
                            imageTileIndex.YIndex + "|" +
                            imageTileIndex.MapZoomLevel;
                    byte[] imageArray;
                    IImage image = TileNotAvaiable;
                    //this is a block methods,it returns when the download is done.

                    _mapTileReader.GetImage(imageTileIndex.MapType,
                            imageTileIndex.XIndex, imageTileIndex.YIndex,
                            imageTileIndex.MapZoomLevel);
                    //if the downloading is successful
                    if (_mapTileReader.ImageArraySize > 0
                            && _mapTileReader.IsImagevalid)
                    {
                        imageArray = _mapTileReader.ImageArray;
                        if (imageArray != null)
                        {
                            try
                            {
                                image = MapLayer.GetAbstractGraphicsFactory()
                                        .CreateImage(imageArray, 0, imageArray.Length);
                            }
                            catch (Exception)
                            {

                                _mapTileReader.IsImagevalid = false;
                                image = TileNotAvaiable;

                            }
                            if (_mapTileReader.IsImagevalid)
                            {
                                _mapTileDownloadManager.AddToImageCache(key, imageArray);
                            }
                        }

                    }
                    _mapTileDownloadManager.OneDownloadImageTileDone(key);
                    _mapTileDownloadManager._mapTileReadyListener.Done(imageTileIndex, image);
                }
                catch (Exception)
                {

                }

            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             *  stop the thread.
             */
            public void Stop()
            {
                _stopDownloadWorker = true;
                _mapTileDownloadWorkerThread.Interrupt();
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * the actually thread run.
             */
            public virtual void Run()
            {
                while (!_stopDownloadWorker)
                {
                    try
                    {
                        //get one map tile index from dowload manager's download list.
                        ImageTileIndex imageTileIndex = _mapTileDownloadManager
                                .GetAImageTileIndex();
                        if (imageTileIndex != null)
                        {
                            _pauseDownloadWorker = false;
                            _mapTileDownloadManager.
                                    AddToAssignedImageDownloadList(imageTileIndex);
                            if (imageTileIndex.MapType == MapType.ROUTING_DIRECTION)
                            {
                                //if it's to render the map direction, just assign
                                //it to  mapDirectionRenderer.
                                lock (_mapTileDownloadManager._assignedMapDirectionRenderListMutex)
                                {
                                    string key = imageTileIndex.MapType + "|" +
                                            imageTileIndex.XIndex + "|" +
                                            imageTileIndex.YIndex + "|" +
                                            imageTileIndex.MapZoomLevel;
                                    if (!_mapTileDownloadManager._assignedMapDirectionRenderList.ContainsKey(key))
                                    {
                                        ImageTileIndex newImagetileIndex = new ImageTileIndex();
                                        newImagetileIndex.MapType = imageTileIndex.MapType;
                                        newImagetileIndex.XIndex = imageTileIndex.XIndex;
                                        newImagetileIndex.YIndex = imageTileIndex.YIndex;
                                        newImagetileIndex.MapZoomLevel = imageTileIndex.MapZoomLevel;
                                        _mapTileDownloadManager._assignedMapDirectionRenderList.Add(key, newImagetileIndex);
                                        Monitor.Pulse(_mapTileDownloadManager._assignedMapDirectionRenderListMutex);
                                    }

                                }
                            }
                            else
                            {
                                GetImage(imageTileIndex);
                            }
                        }
                        else
                        {
                            lock (_syncObjectWorker)
                            {
                                try
                                {
                                    _pauseDownloadWorker = true;
                                    Monitor.Wait(_syncObjectWorker, _maxWaitingTime * 1000);

                                }
                                catch (Exception)
                                {
                                    Thread.CurrentThread.Interrupt();

                                }
                            }

                        }
                    }
                    catch (Exception)
                    {
                        //catch whatever exception to make sure the thread is not dead.

                    }
                }
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * resume the thread.
             */
            public void Resume()
            {
                lock (_syncObjectWorker)
                {
                    _pauseDownloadWorker = false;
                    Monitor.Pulse(_syncObjectWorker);

                }
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * check if the thread is paused or not
             * @return if ture then the thread is paused.
             */
            public bool IsPaused()
            {
                if (_mapTileDownloadWorkerThread != null)
                {
                    return _pauseDownloadWorker;
                }
                return false;
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             * set the worker thread pause status.
             * @param value the pause status.
             */
            public void SetPaused(bool value)
            {
                _pauseDownloadWorker = value;
            }

            //--------------------------------- REVISIONS --------------------------
            // Date       Name                 Tracking #         Description
            // ---------  -------------------  -------------      ------------------
            // 19JUN2009  James Shen                 	          Initial Creation
            ////////////////////////////////////////////////////////////////////////
            /**
             *  check if the thread is alive.
             * @return
             */
            public bool IsAlive()
            {
                if (_mapTileDownloadWorkerThread != null)
                {
                    return _mapTileDownloadWorkerThread.IsAlive;
                }
                return false;
            }
        }
    }



}
