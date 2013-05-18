using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MapDigit.GIS.Raster;

namespace MapDigit.MapTile
{
    public abstract class  MapTileDataSource : MapTileAbstractReader
    {
        protected Guid _guid;

        protected string _uri;

        protected Thread _dataReadThread;

        protected readonly Hashtable _imageCache = new Hashtable();

      


        public override void GetImage(int mtype, int x, int y, int zoomLevel)
        {
            string key = mtype + "|" + x + "|" + y + "|" + zoomLevel;
            lock(_imageCache)
            {
                if(_imageCache.ContainsKey(key))
                {
                    IsImagevalid = true;
                    ImageArray = (byte[]) _imageCache[key];
                    ImageArraySize = ImageArray.Length;

                }else
                {
                    ForceGetImage(mtype, x, y, zoomLevel);
                    
                    if(IsImagevalid)
                    {
                        if (_imageCache.Count > 64) _imageCache.Clear();
                        _imageCache[key] = ImageArray;
                    }
                }
            }
                                              
        }

        protected abstract void ForceGetImage(int mtype, int x, int y, int zoomLevel);

        public Guid Guid
        {
            get
            {
                return _guid;
            }

            set
            {
                _guid = value;
            }
        }


        public string Uri
        {
            get
            {
                return _uri;
            }

            set
            {
                _uri = value;
            }
        }



    }
}
