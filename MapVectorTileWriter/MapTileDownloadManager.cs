using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MapVectorTileWriter
{
    class MapTileDownloadManager
    {
        public readonly Hashtable imageCache = new Hashtable();

        public readonly IList<MapTileIndex> taskList =new List<MapTileIndex>();

        public readonly IList<MapTileIndex> retryList = new List<MapTileIndex>();

        public volatile bool stopManager = false;

        public volatile int DownloadedCount = 0;

        private frmMain mainForm;
        public MapTileDownloadManager(frmMain main)
        {
            mainForm = main;
        }


        public void Done()
        {
            mainForm.doneWithDownloading();
        }

        public void AddMessage(string message)
        {
            mainForm.AddMessage(message);
        }

        private const int MAX_THREAD_NUM = 1;

        public void AddToImageCache(MapTileIndex mapTileIndex,byte []imageData)
        {
            string mapIndex = mapTileIndex.MapType + "|" + mapTileIndex.XIndex + "|" +
                              mapTileIndex.YIndex + "|" + mapTileIndex.ZoomLevel;
            lock (imageCache)
            {
                if(!imageCache.ContainsKey(mapIndex))
                {
                    imageCache.Add(mapIndex,imageData);
                }
            }
        }

        public byte[] GetFromImageCache(MapTileIndex mapTileIndex)
        {
            string mapIndex = mapTileIndex.MapType + "|" + mapTileIndex.XIndex + "|" +
                              mapTileIndex.YIndex + "|" + mapTileIndex.ZoomLevel;
            lock (imageCache)
            {
                if (imageCache.ContainsKey(mapIndex))
                {
                    return (byte[])imageCache[mapIndex];
                }
            }
            return null;
        }

        public void RemoveFromImageCache(MapTileIndex mapTileIndex)
        {
            string mapIndex = mapTileIndex.MapType + "|" + mapTileIndex.XIndex + "|" +
                              mapTileIndex.YIndex + "|" + mapTileIndex.ZoomLevel;
            lock (imageCache)
            {
                if (imageCache.ContainsKey(mapIndex))
                {
                    imageCache.Remove(mapIndex);
                }
            }
        }

        public void AddToTaskList(MapTileIndex mapTileIndex)
        {
            lock(taskList)
            {
                taskList.Add(mapTileIndex);
            }
        }

        public MapTileIndex  GetOneTask()
        {
            lock(taskList)
            {
                MapTileIndex mapTileIndex = null;
                if (taskList.Count > 0)
                {

                    mapTileIndex = taskList[0];
                    if (mapTileIndex != null)
                    {
                        taskList.Remove(mapTileIndex);
                    }
                   
                }
                return mapTileIndex;
            }
            
           
        }

        public void AddToRetryList(MapTileIndex mapTileIndex)
        {
            lock (retryList)
            {
                retryList.Add(mapTileIndex);
            }
        }

        public MapTileIndex GetOneTaskFromRetryList()
        {
            lock (retryList)
            {
                MapTileIndex mapTileIndex = null;

                if (retryList.Count > 0)
                {

                    mapTileIndex = retryList[retryList.Count - 1];
                    if (mapTileIndex != null)
                    {
                        retryList.Remove(mapTileIndex);
                    }

                }
                return mapTileIndex;
            }
        }

        public void ProcessTaskList()
        {
            while (!stopManager)
            {
                lock (taskList)
                {
                    int count = Math.Min(taskList.Count, MAX_THREAD_NUM);
                    for (int i = 0; i < count; i++)
                    {
                        MapTileIndex mapTileIndex = GetOneTask();
                        MapTileDownloadWorker mapTileDownloadWorker = new MapTileDownloadWorker(this, mapTileIndex);
                        mapTileDownloadWorker.DownloadMapTile();
                        //Thread downloadThread = new Thread(new ThreadStart(mapTileDownloadWorker.DownloadMapTile));
                        //downloadThread.Start();
                    }
                }

                lock (retryList)
                {
                    int count = Math.Min(retryList.Count, MAX_THREAD_NUM);
                    for (int i = 0; i < count; i++)
                    {
                        MapTileIndex mapTileIndex = GetOneTaskFromRetryList();
                        MapTileDownloadWorker mapTileDownloadWorker = new MapTileDownloadWorker(this, mapTileIndex);
                        mapTileDownloadWorker.DownloadMapTile();
                        //Thread downloadThread = new Thread(new ThreadStart(mapTileDownloadWorker.DownloadMapTile));
                        //downloadThread.Start();
                    }
                }
            }
        }
    }
}