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
     * string data section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class StringData : Section
    {

        public int FieldCount;
        public int[] MapInfoID;
        public int[] FieldID;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public StringData(BinaryReader reader, long offset, long size)
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
         * Get one string value.
         */
        public string GetRecord(long offset)
        {
            DataReader.Seek(_reader, offset);
            string ret = DataReader.ReadString(_reader);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get one string value.
         */
        public string GetMapInfoIDAndField(long offset)
        {
            DataReader.Seek(_reader, offset);
            string ret = DataReader.ReadString(_reader);
            FieldCount = DataReader.ReadShort(_reader);
            MapInfoID = new int[FieldCount];
            FieldID = new int[FieldCount];
            for (int i = 0; i < FieldCount; i++)
            {
                MapInfoID[i] = DataReader.ReadInt(_reader);
                FieldID[i] = _reader.ReadByte();
            }
            return ret;
        }
    }

}
