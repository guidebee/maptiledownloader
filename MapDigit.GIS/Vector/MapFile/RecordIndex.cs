//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 21JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.IO;
using MapDigit.Util;
using BinaryReader = System.IO.BinaryReader;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector.MapFile
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Record index section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class RecordIndex : Section
    {

        /**
         * The type of the map object.
         */
        public byte MapObjectType;
        /**
         * the offset of the record.
         */
        public int RecordOffset;
        /**
         * the lenght of the record.
         */
        public int RecordLength;
        /**
         *  the MinX of the MBR.
         */
        public int MinX;
        /**
         *  the MinY of the MBR.
         */
        public int MinY;
        /**
         *  the MaxX of the MBR.
         */
        public int MaxX;
        /**
         *  the MaxY of the MBR.
         */
        public int MaxY;
        /**
         * Map object param1 (depends on map object type).
         */
        public int Param1;
        /**
         * Map object param2 (depends on map object type).
         */
        public int Param2;
        /**
         * Map object param3 (depends on map object type).
         */
        public int Param3;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public RecordIndex(BinaryReader reader, long offset, long size)
            : base(reader, offset, size)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get one record (index) of give mapInfo ID.
         */
        public void GetRecord(int mapInfoID)
        {
            int recordID = mapInfoID - 1;
            if (mapInfoID < 1)
            {
                throw new IOException("MapInfo ID starts from 1");
            }
            _currentIndex = recordID;
            ReadOneRecord();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get next record (index).
         */
        public void MovePrevious()
        {
            _currentIndex--;
            ReadOneRecord();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get previous record (index).
         */
        public void MoveNext()
        {
            _currentIndex++;
            ReadOneRecord();
        }

        /**
         * the size of the record index record.
         */
        private const int RECORDSIZE = 37;
        /**
         * current index id.
         */
        private int _currentIndex;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * read current record.
         */
        private void ReadOneRecord()
        {
            DataReader.Seek(_reader, _offset + _currentIndex * RECORDSIZE);
            MapObjectType = _reader.ReadByte();
            RecordOffset = DataReader.ReadInt(_reader);
            RecordLength = DataReader.ReadInt(_reader);
            MinX = DataReader.ReadInt(_reader);
            MinY = DataReader.ReadInt(_reader);
            MaxX = DataReader.ReadInt(_reader);
            MaxY = DataReader.ReadInt(_reader);
            Param1 = DataReader.ReadInt(_reader);
            Param2 = DataReader.ReadInt(_reader);
            Param3 = DataReader.ReadInt(_reader);
        }
    }

}
