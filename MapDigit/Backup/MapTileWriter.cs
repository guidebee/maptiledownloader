using System;
using System.IO;
using System.Threading;
using MapDigit.GIS.Raster;

namespace MapDigit.MapTileWriter
{
    public class MapTileWriter
    {
        private readonly int _startIndexX;
        private readonly int _startIndexY;
        private readonly int _endIndexX;
        private readonly int _endIndexY;
        private readonly int _zoomLevel;
        private readonly int _mapType;
        private readonly MapTileDownloadManager _mapTileDownloadManager;
        private readonly byte[] _notavaiablePng;
        private IWritingProgressListener writingProgressListener;

        public bool[] ZoomLevelSelected;

        public MapTileWriter(int startX, int startY, int endX, int endY, int level, int type, MapTileDownloadManager manager)
        {
            _startIndexX = startX;
            _startIndexY = startY;
            _endIndexX = endX;
            _endIndexY = endY;
            _zoomLevel = level;
            _mapType = type;
            _mapTileDownloadManager = manager;
            try
            {
                FileStream notAvaiable = new FileStream("tile-na.png", FileMode.Open);
                _notavaiablePng = new byte[notAvaiable.Length];
                notAvaiable.Read(_notavaiablePng, 0, _notavaiablePng.Length);
                notAvaiable.Close();


            }
            catch (Exception)
            {

            }
        }

        public void SetWritingProgressListener(IWritingProgressListener listener)
        {
            writingProgressListener = listener;
        }

        public int CalculateHowManyTiles()
        {
            int selectedMapIndex = (_endIndexX + 1 - _startIndexX) * (_endIndexY - _startIndexY + 1);
            int howManyLevel = 0;
            for (int level = _zoomLevel; level < 18; level++)
            {
                if (ZoomLevelSelected[level])
                {
                    howManyLevel += (int)Math.Pow(4, level - _zoomLevel);
                }
            }

            int howMayTiles = howManyLevel * selectedMapIndex;
            return howMayTiles;

        }

        public void WriteMapTileFile()
        {

            string fileName = _startIndexX + "_" + _startIndexY + "_" + _endIndexX + "_" + _endIndexY + "_" + _zoomLevel +
                              "_" + _mapType + ".map";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            FileStream mapFile = new FileStream(fileName, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(mapFile);
            JavaBinaryWriter javaWriter = new JavaBinaryWriter(writer);
            int headSize = 256;
            int levelSize = 1024;
            int howManyTiles = CalculateHowManyTiles() + 10;
            int indexSize = howManyTiles * 8;
            for (int i = 0; i <= headSize + levelSize + indexSize; i++)
                javaWriter.Write((byte)0);
            mapFile.Seek(0, SeekOrigin.Begin);
            javaWriter.Write("GUIDEBEE MAP");
            mapFile.Seek(16, SeekOrigin.Begin);
            javaWriter.Write("JAVA");
            mapFile.Seek(32, SeekOrigin.Begin);
            javaWriter.Write("TILE");
            mapFile.Seek(48, SeekOrigin.Begin);
            javaWriter.Write(_mapType); //PNG type
            int zoomCount = 0;
            for (int zoom = _zoomLevel; zoom < 18; zoom++)
            {
                if (ZoomLevelSelected[zoom])
                {
                    zoomCount++;
                }
            }
            javaWriter.Write(zoomCount);
            javaWriter.Write(-90.0);
            javaWriter.Write(-180.0);
            javaWriter.Write(90.0);
            javaWriter.Write(180.0);
            mapFile.Seek(256, SeekOrigin.Begin);

            int levelOffset = headSize + levelSize;
            int pngOffset = headSize + levelSize + indexSize;
            zoomCount = 0;
            long imageIndex = 0;
            for (int zoom = _zoomLevel; zoom < 18; zoom++)
            {

                if (ZoomLevelSelected[zoom])
                {
                    int zoomPower = (int)Math.Pow(2, zoom - _zoomLevel);

                    int pngLenght;


                    for (int i = _startIndexX * zoomPower; i < (_endIndexX + 1) * zoomPower; i++)
                    {
                        for (int j = _startIndexY * zoomPower; j < (_endIndexY + 1) * zoomPower; j++)
                        {
                            MapTileIndex mapTileIndex = new MapTileIndex();
                            mapTileIndex.MapType = _mapType;
                            mapTileIndex.ZoomLevel =  zoom;
                            mapTileIndex.XIndex = i;
                            mapTileIndex.YIndex = j;



                            byte[] pngImage = _mapTileDownloadManager.GetFromImageCache(mapTileIndex.MapType, mapTileIndex.XIndex, mapTileIndex.YIndex, mapTileIndex.ZoomLevel);
                            int tryCount = 0;
                            bool failed = false;
                            while (pngImage == null && tryCount < 15)
                            {
                                Thread.Sleep(2000);
                                pngImage = _mapTileDownloadManager.GetFromImageCache(mapTileIndex.MapType, mapTileIndex.XIndex, mapTileIndex.YIndex, mapTileIndex.ZoomLevel);
                                tryCount++;
                            }

                            if (pngImage == null)
                            {
                                pngImage = _notavaiablePng;
                                failed = true;

                            }
                            else
                            {
                                _mapTileDownloadManager.RemoveFromImageCache(mapTileIndex.MapType, mapTileIndex.XIndex, mapTileIndex.YIndex, mapTileIndex.ZoomLevel);
                            }

                            pngLenght = pngImage.Length;
                            mapFile.Seek(headSize + levelSize + imageIndex * 8
                                         , SeekOrigin.Begin);
                            javaWriter.Write(pngOffset);
                            javaWriter.Write(pngLenght);
                            mapFile.Seek(pngOffset
                                         , SeekOrigin.Begin);
                            writer.Write(pngImage);
                            pngOffset += pngLenght;
                            if (writingProgressListener!=null)
                            {
                                writingProgressListener.Progress(imageIndex, i, j, zoom, failed);
                            }
                            imageIndex++;


                        }
                    }
                    int levelLength = (_endIndexX + 1 - _startIndexX) * (_endIndexY + 1 - _startIndexY) * zoomPower * zoomPower * 8;
                    //write level offset
                    mapFile.Seek(headSize + zoomCount * 28
                                 , SeekOrigin.Begin);
                    javaWriter.Write(zoom);
                    javaWriter.Write(_startIndexX * zoomPower);
                    javaWriter.Write(_startIndexY * zoomPower);
                    javaWriter.Write((_endIndexX + 1) * zoomPower - 1);
                    javaWriter.Write((_endIndexY + 1) * zoomPower - 1);
                    javaWriter.Write(levelOffset);
                    javaWriter.Write(levelLength);

                    levelOffset += levelLength;
                    zoomCount++;



                }
            }

            javaWriter.Close();
            mapFile.Close();

            if (writingProgressListener != null)
            {
                writingProgressListener.FinishWriting();
            }

        }

    }
}
