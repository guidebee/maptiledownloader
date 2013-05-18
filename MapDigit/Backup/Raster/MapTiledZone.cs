//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 20JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.IO;
using MapDigit.GIS.Geometry;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 20JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Define a tiled map zone.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 20/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapTiledZone
    {

        /**
         * the boundary of this map zone.
         */
        public GeoLatLngBounds Bounds;

        /**
         * the map type of this map zone.
         */
        public int MapType;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param reader  the input stream of the map file.
         */
        public MapTiledZone(Stream stream)
        {
            _reader = new BinaryReader(stream);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Open the map file.
         * @throws IOException
         */
        public void Open()
        {
            if (_opened)
            {
                return;
            }

            _opened = true;
            DataReader.Seek(_reader, 0);
            DataReader.ReadString(_reader);
            DataReader.Seek(_reader, 16);
            DataReader.ReadString(_reader);

            DataReader.Seek(_reader, 32);
            string mapTile = DataReader.ReadString(_reader);
            if (!mapTile.ToUpper().Equals("TILE"))
            {
                throw new IOException("Invalid map format!");
            }
            DataReader.Seek(_reader, 48);
            MapType = DataReader.ReadInt(_reader);
            int numOfLevel = DataReader.ReadInt(_reader);
            double minY = DataReader.ReadDouble(_reader);
            double minX = DataReader.ReadDouble(_reader);
            double maxY = DataReader.ReadDouble(_reader);
            double maxX = DataReader.ReadDouble(_reader);
            Bounds = new GeoLatLngBounds(minX, minY, maxX - minX, maxY - minY);
            DataReader.Seek(_reader, HEADSIZE);
            _levelInfos = new LevelInfo[numOfLevel];
            for (int i = 0; i < numOfLevel; i++)
            {
                _levelInfos[i] = new LevelInfo
                                    {
                                        LevelNo = DataReader.ReadInt(_reader),
                                        MinX = DataReader.ReadInt(_reader),
                                        MinY = DataReader.ReadInt(_reader),
                                        MaxX = DataReader.ReadInt(_reader),
                                        MaxY = DataReader.ReadInt(_reader),
                                        Offset = DataReader.ReadInt(_reader),
                                        Length = DataReader.ReadInt(_reader)
                                    };
            }

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get image at given location
         * @param level zoom level.
         * @param X X index of the map tile.
         * @param Y Y index of the map tile.
         * @return the map tile image.
         * @throws java.io.IOException
         */
        public byte[] GetImage(int level, int x, int y)
        {
            LevelInfo levelInfo = GetLevelInfo(level);
            byte[] buffer = null;
            if (levelInfo != null)
            {
                int rows = levelInfo.MaxY + 1 - levelInfo.MinY;
                if (x <= levelInfo.MaxX && x >= levelInfo.MinX &&
                        y <= levelInfo.MaxY && y >= levelInfo.MinY)
                {
                    int imageIndex = (x - levelInfo.MinX) * rows + y - levelInfo.MinY;
                    DataReader.Seek(_reader, levelInfo.Offset + imageIndex * 8);
                    int offset = DataReader.ReadInt(_reader);
                    int length = DataReader.ReadInt(_reader);
                    buffer = new byte[length];
                    DataReader.Seek(_reader, offset);

                    int howManyKs = length / 1024;
                    int remainBytes = length - howManyKs * 1024;
                    for (int i = 0; i < howManyKs; i++)
                    {
                        _reader.Read(buffer, i * 1024, 1024);
                        if (_readListener != null)
                        {
                            _readListener.readProgress(i * 1024, length);
                        }
                    }
                    if (remainBytes > 0)
                    {
                        _reader.Read(buffer, howManyKs * 1024, remainBytes);
                        if (_readListener != null)
                        {
                            _readListener.readProgress(length, length);
                        }

                    }
                }
            }
            return buffer;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 20JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Close the map file.
         * @throws IOException
         */
        public void Close()
        {
            if (_reader != null)
            {
                _reader.Close();
            }
        }



        /**
         * reader to read the data from the map file.
         */
        private readonly BinaryReader _reader;


        /**
         * check if it's opened;
         */
        private bool _opened;

        /**
         * level info.
         */
        private LevelInfo[] _levelInfos;

        /**
         * the header size.
         */
        private const int HEADSIZE = 256;


        /**
         * downloader listener
         */
        internal IReaderListener _readListener;




        private LevelInfo GetLevelInfo(int level)
        {
            for (int i = 0; i < _levelInfos.Length; i++)
            {
                if (_levelInfos[i].LevelNo == level)
                {
                    return _levelInfos[i];
                }
            }
            return null;
        }

        private class LevelInfo
        {

            public int LevelNo;
            public int MinX;
            public int MinY;
            public int MaxX;
            public int MaxY;
            public int Offset;
            public int Length;

        }



    }


}
