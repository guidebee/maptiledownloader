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
using BinaryReader = System.IO.BinaryReader;
using MapDigit.Util;

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
     * string index section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class StringIndex : Section
    {

        /**
         * the offset of the record.
         */
        public int RecordOffset;
        /**
         * the lenght of the record.
         */
        public int RecordLength;
        /**
         * the size of the record index record.
         */
        public const int RECORDSIZE = 6;

        /**
         * total record count;
         */
        public int RecordCount;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public StringIndex(BinaryReader reader, long offset, long size)
            : base(reader, offset, size)
        {

            RecordCount = (int)(size / RECORDSIZE);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get one record (index) of give record ID.
         */
        public void GetRecord(int recordID)
        {
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
        public bool MovePrevious()
        {
            bool ret = false;
            _currentIndex--;
            if (_currentIndex < 0)
            {
                ret = true;
            }
            else
            {
                ReadOneRecord();
            }
            return ret;
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
        public bool MoveNext()
        {
            bool ret = false;
            _currentIndex++;
            if (_currentIndex >= RecordCount)
            {
                ret = true;
            }
            else
            {
                ReadOneRecord();
            }
            return ret;
        }

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
            RecordOffset = DataReader.ReadInt(_reader);
            RecordLength = DataReader.ReadInt(_reader);
        }
    }

}
