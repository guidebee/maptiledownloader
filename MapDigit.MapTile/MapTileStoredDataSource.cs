using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MapDigit.GIS.Raster;

namespace MapDigit.MapTile
{
    public class MapTileStoredDataSource : MapTileDataSource,IDisposable
    {
        private readonly FileStream _fileStream;
        private readonly MapTileStreamReader _mapTileStreamReader;
        private readonly object _syncObject = new object();


        public MapTileStoredDataSource(string url)
        {
            Uri = url;
            _fileStream = new FileStream(url, FileMode.Open);
            MapTiledZone mapTiledZone = new MapTiledZone(_fileStream);
            _mapTileStreamReader = new MapTileStreamReader();
            _mapTileStreamReader.AddZone(mapTiledZone);
            _mapTileStreamReader.Open();

        }

        protected override void ForceGetImage(int mtype, int x, int y, int zoomLevel)
        {
            lock(_syncObject)
            {
                _mapTileStreamReader.GetImage(mtype, x, y, zoomLevel);
                ImageArray = _mapTileStreamReader.ImageArray;
                IsImagevalid = _mapTileStreamReader.IsImagevalid;
                ImageArraySize = _mapTileStreamReader.ImageArraySize;
            }
        }

        public void Dispose()
        {
            if(_fileStream!=null)
            {
               _fileStream.Close();
            }
        }
    }
}
