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
     * tabular data section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class TabularData : Section
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public TabularData(BinaryReader reader, long offset, long size,
                DataField[] fields, StringData stringData, StringIndex stringIndex)
            : base(reader, offset, size)
        {
            this._fields = fields;
            this._stringData = stringData;
            this._stringIndex = stringIndex;
            int numberOfField = fields.Length;
            _recordSize = 0;
            for (int i = 0; i < numberOfField; i++)
            {
                switch (fields[i].GetFieldType())
                {
                    case DataField.TYPE_CHAR://char
                        _recordSize += 4;
                        break;
                    case DataField.TYPE_INTEGER://int
                        _recordSize += 4;
                        break;
                    case DataField.TYPE_SMALLINT://shot
                        _recordSize += 2;
                        break;
                    case DataField.TYPE_FLOAT://float
                        _recordSize += 8;
                        break;
                    case DataField.TYPE_DECIMAL://float
                        _recordSize += 8;
                        break;
                    case DataField.TYPE_DATE://date
                        _recordSize += 4;
                        break;
                    case DataField.TYPE_LOGICAL://bool
                        _recordSize += 1;
                        break;
                }
            }
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
        public DataRowValue GetRecord(int mapInfoID)
        {
            int recordID = mapInfoID - 1;
            if (mapInfoID < 1)
            {
                throw new IOException("MapInfo ID starts from 1");
            }
            DataReader.Seek(_reader, _offset + recordID * _recordSize);
            string[] fieldValues = new string[_fields.Length];

            int intValue;
            int shortValue;
            int stringID;
            byte boolValue;
            double doubleValue;
            for (int i = 0; i < fieldValues.Length; i++)
            {
                switch (_fields[i].GetFieldType())
                {
                    case DataField.TYPE_CHAR://char
                    case DataField.TYPE_DATE://date
                        stringID = DataReader.ReadInt(_reader);
                        fieldValues[i] = stringID.ToString();
                        break;
                    case DataField.TYPE_INTEGER://int
                        intValue = DataReader.ReadInt(_reader);
                        fieldValues[i] = intValue.ToString();
                        break;
                    case DataField.TYPE_SMALLINT://short
                        shortValue = DataReader.ReadShort(_reader);
                        fieldValues[i] = shortValue.ToString();
                        break;
                    case DataField.TYPE_DECIMAL://short
                    case DataField.TYPE_FLOAT://float
                        doubleValue = DataReader.ReadDouble(_reader);
                        fieldValues[i] = doubleValue.ToString();
                        break;
                    case DataField.TYPE_LOGICAL://bool
                        boolValue = _reader.ReadByte();
                        fieldValues[i] = boolValue.ToString();
                        break;
                }
            }

            //read string data.
            for (int i = 0; i < fieldValues.Length; i++)
            {
                switch (_fields[i].GetFieldType())
                {
                    case DataField.TYPE_CHAR://char
                    case DataField.TYPE_DATE://date
                        stringID = int.Parse(fieldValues[i]);
                        if (stringID != -1)
                        {
                            _stringIndex.GetRecord(stringID);
                            fieldValues[i] = _stringData.GetRecord(_stringIndex.RecordOffset);
                        }
                        else
                        {
                            fieldValues[i] = "";
                        }
                        break;
                }
            }
            var ret = new DataRowValue(fieldValues);
            return ret;
        }

        /**
         * table field defintion.
         */
        private readonly DataField[] _fields;
        /**
         * the lenght of one record.
         */
        private readonly int _recordSize;
        /**
         * string data section object.
         */
        private readonly StringData _stringData;
        /**
         * string index section object.
         */
        private readonly StringIndex _stringIndex;
    }
}

