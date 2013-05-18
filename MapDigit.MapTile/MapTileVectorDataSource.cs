using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MapDigit.GIS;
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Vector;

namespace MapDigit.MapTile
{
    public class MapTileVectorDataSource : MapTileDataSource,IDisposable
    {
        private readonly GeoSet _getSet;
        private readonly FileStream _geoStream;
        private readonly FileStream[] _layerStreams;
        private readonly VectorMapRenderer _vectorMapRenderer;
        private readonly object _syncObject=new object();


        public GeoSet GetGeoSet()
        {
            return _getSet;
        }

        //public MapTileVectorDataSource(string url)
        //{
        //    Uri = url;
        //    _geoStream = new FileStream(url, FileMode.Open);
        //    byte[] bufferGeo = new byte[_geoStream.Length];
        //    _geoStream.Read(bufferGeo, 0, bufferGeo.Length);
        //    _geoStream.Close();
        //    MemoryStream baisGeo = new MemoryStream(bufferGeo);
        //    _getSet = new GeoSet(new BinaryReader(baisGeo));
        //    string filePath = Path.GetDirectoryName(url);
        //    string[] layerNames = _getSet.GetLayerNames();
        //    _layerStreams = new FileStream[layerNames.Length];
        //    for (int i = 0; i < layerNames.Length; i++)
        //    {
        //        string layerName = filePath + "\\" + layerNames[i];
        //        _layerStreams[i] = new FileStream(layerName, FileMode.Open);
        //        MapFeatureLayer layer = new MapFeatureLayer(new BinaryReader(_layerStreams[i]));
        //        layer.FontColor = 0x000000;
        //        GeoSet.MapFeatureLayerInfo layerInfo = _getSet.GetMapMapFeatureLayerInfo(layerNames[i]);
        //        if (layerInfo != null)
        //        {
        //            layer.ZoomLevel = layerInfo.ZoomLevel;
        //            layer.ZoomMin = layerInfo.ZoomMin;
        //            layer.ZoomMax = layerInfo.ZoomMax;
        //            layer.Description = layerInfo.Description;
        //            layer.Visible = layerInfo.Visible;
        //            layer.LayerName = layerInfo.LayerName;
        //        }
        //        _getSet.AddMapFeatureLayer(layer);
        //    }

        //    _vectorMapRenderer = new VectorMapRenderer(_getSet);
        //    Font font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Regular);
        //    IFont newFont = MapLayer.GetAbstractGraphicsFactory().CreateFont(font);
        //    _vectorMapRenderer.SetFont(newFont);
        //    _getSet.Open();

        //}


        public MapTileVectorDataSource(string url)
        {
            Uri = url;
            _geoStream = new FileStream(url, FileMode.Open);
            byte[] bufferGeo = new byte[_geoStream.Length];
            _geoStream.Read(bufferGeo, 0, bufferGeo.Length);
            _geoStream.Close();
            MemoryStream baisGeo = new MemoryStream(bufferGeo);
            _getSet = new GeoSet();
            string filePath = @"C:\shenjing\map";
            string[] layerNames = new string[] {"3.lyr","1.lyr", "2.lyr" };
            _layerStreams = new FileStream[layerNames.Length];
            for (int i = 0; i < layerNames.Length; i++)
            {
                string layerName = filePath + "\\" + layerNames[i];
                _layerStreams[i] = new FileStream(layerName, FileMode.Open);
                MapFeatureLayer layer = new MapFeatureLayer(new BinaryReader(_layerStreams[i]));
                layer.FontColor = 0x000000;
                GeoSet.MapFeatureLayerInfo layerInfo = _getSet.GetMapMapFeatureLayerInfo(layerNames[i]);
                if (layerInfo != null)
                {
                    layer.ZoomLevel = layerInfo.ZoomLevel;
                    layer.ZoomMin = layerInfo.ZoomMin;
                    layer.ZoomMax = layerInfo.ZoomMax;
                    layer.Description = layerInfo.Description;
                    layer.Visible = layerInfo.Visible;
                    layer.LayerName = layerInfo.LayerName;
                }
                _getSet.AddMapFeatureLayer(layer);
            }

            _vectorMapRenderer = new VectorMapRenderer(_getSet);
            Font font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Regular);
            IFont newFont = MapLayer.GetAbstractGraphicsFactory().CreateFont(font);
            _vectorMapRenderer.SetFont(newFont);
            _getSet.Open();

        }

        

        protected override void ForceGetImage(int mtype, int x, int y, int zoomLevel)
        {
            lock (_syncObject)
            {
                _vectorMapRenderer.GetImage(mtype, x, y, zoomLevel);
                ImageArray = _vectorMapRenderer.ImageArray;
                IsImagevalid = _vectorMapRenderer.IsImagevalid;
                ImageArraySize = _vectorMapRenderer.ImageArraySize;
            }
        }

        public void Dispose()
        {
            if(_getSet!=null)
            {
                _getSet.Close();
            }
            if(_layerStreams!=null)
            {
                for(int i=0;i<_layerStreams.Length;i++)
                {
                    if(_layerStreams[i]!=null)
                    {
                        _layerStreams[i].Close();
                    }
                }
            }
        }
    }
}
