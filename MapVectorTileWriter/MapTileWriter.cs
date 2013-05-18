using System;
using System.IO;
using System.Threading;
using MapVectorTileWriter;

namespace MapTileDownloader
{
    class MapTileWriter
    {
        private int startIndexX;
        private int startIndexY;
        private int endIndexX;
        private int endIndexY;
        private int zoomLevel;
        private int mapType;
        private readonly MapTileDownloadManager mapTileDownloadManager;
        private byte[] notavaiablePng = null;

        public bool[] zoomLevelSelected;

        public MapTileWriter(int startX, int startY, int endX, int endY, int level, int type, MapTileDownloadManager manager)
        {
            startIndexX = startX;
            startIndexY = startY;
            endIndexX = endX;
            endIndexY = endY;
            zoomLevel = level;
            mapType = type;
            mapTileDownloadManager = manager;
            try
            {
                FileStream notAvaiable = new FileStream("tile-na.png",FileMode.Open);
                notavaiablePng = new byte[notAvaiable.Length];
                notAvaiable.Read(notavaiablePng, 0, notavaiablePng.Length);
                notAvaiable.Close();
                

            }catch(Exception ex)
            {
                
            }
        }

        private int CalculateHowManyTiles()
        {
            int selectedMapIndex = 0;
            selectedMapIndex = (endIndexX + 1 - startIndexX) * (endIndexY - startIndexY + 1);
            int howManyLevel = 0;
            for (int level = zoomLevel; level < 18; level++)
            {
                if (zoomLevelSelected[level])
                {
                    howManyLevel += (int)Math.Pow(4, level - zoomLevel);
                }
            }

            int howMayTiles = howManyLevel * selectedMapIndex;
            return howMayTiles;
            
        }

        public void WriteMapTileFile()
        {

            string fileName = startIndexX + "_" + startIndexY + "_" + endIndexX + "_" + endIndexY + "_" + zoomLevel +
                              "_" + mapType + ".map";
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            FileStream mapFile = new FileStream(fileName, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(mapFile);
            JavaBinaryWriter javaWriter = new JavaBinaryWriter(writer);
            int headSize = 256;
            int levelSize = 1024;
            int howManyTiles = CalculateHowManyTiles()+10;
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
            javaWriter.Write((int)mapType); //PNG type
            int zoomCount = 0;
            for (int zoom = zoomLevel; zoom < 18; zoom++)
            {
                 if (zoomLevelSelected[zoom])
                 {
                     zoomCount++;
                 }
            }
            javaWriter.Write((int)zoomCount);
            javaWriter.Write((double)-90.0);
            javaWriter.Write((double)-180.0);
            javaWriter.Write((double)90.0);
            javaWriter.Write((double)180.0);
            mapFile.Seek(256, SeekOrigin.Begin);
            
            int levelOffset = headSize + levelSize;
            int pngOffset = headSize + levelSize + indexSize;
            zoomCount = 0;
            int imageIndex = 0;
            for (int zoom = zoomLevel; zoom < 18; zoom++)
            {

                if (zoomLevelSelected[zoom])
                {
                    int zoomPower = (int)Math.Pow(2, zoom - zoomLevel);

                    int levelLength = 0;

                    int pngLenght = 0;

                    
                    for (int i = startIndexX * zoomPower; i < (endIndexX + 1) * zoomPower; i++)
                    {
                        for (int j = startIndexY * zoomPower; j < (endIndexY + 1) * zoomPower; j++)
                        {
                            MapTileIndex mapTileIndex=new MapTileIndex();
                            mapTileIndex.MapType = mapType;
                            mapTileIndex.ZoomLevel = 17 - zoom;
                            mapTileIndex.XIndex = i;
                            mapTileIndex.YIndex = j ;



                            byte[] pngImage = mapTileDownloadManager.GetFromImageCache(mapTileIndex);
                            int tryCount = 0;
                            while (pngImage==null && tryCount<3)
                            {
                                Thread.Sleep(30000);
                                pngImage = mapTileDownloadManager.GetFromImageCache(mapTileIndex);
                                tryCount++;
                            }

                            if(pngImage==null)
                            {
                                pngImage = notavaiablePng;
    
                            }else
                            {
                                mapTileDownloadManager.RemoveFromImageCache(mapTileIndex);
                            }
                           
                            pngLenght = pngImage.Length;
                            mapFile.Seek(headSize + levelSize + imageIndex*8
                                         , SeekOrigin.Begin);
                            javaWriter.Write(pngOffset);
                            javaWriter.Write(pngLenght);
                            mapFile.Seek(pngOffset
                                         , SeekOrigin.Begin);
                            writer.Write(pngImage);
                            pngOffset += pngLenght;

                            imageIndex++;


                        }
                    }
                    levelLength = (endIndexX + 1 - startIndexX) * (endIndexY + 1 - startIndexY) * zoomPower * zoomPower * 8;
                    //write level offset
                    mapFile.Seek(headSize + zoomCount * 28
                                 , SeekOrigin.Begin);
                    javaWriter.Write(zoom);
                    javaWriter.Write((int)startIndexX * zoomPower);
                    javaWriter.Write((int)startIndexY * zoomPower);
                    javaWriter.Write((endIndexX + 1) * zoomPower - 1);
                    javaWriter.Write((endIndexY + 1) * zoomPower - 1);
                    javaWriter.Write(levelOffset);
                    javaWriter.Write(levelLength);

                    levelOffset += levelLength;
                    zoomCount++;



                }
            }

            javaWriter.Close();
            mapFile.Close();
            mapTileDownloadManager.Done();


        }

    }
}
