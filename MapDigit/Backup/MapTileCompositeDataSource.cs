using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDigit.MapTile
{
    public class MapTileCompositeDataSource : MapTileDataSource
    {
        protected override void ForceGetImage(int mtype, int x, int y, int zoomLevel)
        {
            throw new NotImplementedException();
        }
    }
}
