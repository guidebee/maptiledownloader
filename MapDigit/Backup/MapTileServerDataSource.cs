using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MapDigit.GIS.Raster;

namespace MapDigit.MapTile
{
    public class MapTileServerDataSource : MapTileDataSource
    {
        private readonly MapTileDownloader _mapTileDownloader=new MapTileDownloader();

        public override void SetMapDownloadingListener(IReaderListener listener)
        {
            base.SetMapDownloadingListener(listener);
            _mapTileDownloader.SetMapDownloadingListener(listener);
        }

   

        protected override void ForceGetImage(int mtype, int x, int y, int zoomLevel)
        {
            _mapTileDownloader.GetImage(mtype,x,y,zoomLevel);
            ImageArray = _mapTileDownloader.ImageArray;
            IsImagevalid = _mapTileDownloader.IsImagevalid;
            ImageArraySize = _mapTileDownloader.ImageArraySize;
        }

  
    }
}
