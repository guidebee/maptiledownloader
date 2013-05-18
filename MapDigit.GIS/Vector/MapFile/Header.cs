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
using MapDigit.GIS.Geometry;

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
     * Header section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class Header : Section
    {

        public string FileVersion;
        public string FileFormat;
        public string DominantType;
        public int IndexOffset;
        public int IndexLength;
        public int RtreeIndexOffset;
        public int RtreeIndexLength;
        public int StringIndexOffset;
        public int StringIndexLength;
        public int StringDataOffset;
        public int StringDataLength;
        public int GeoDataOffset;
        public int GeoDataLength;
        public int TabularDataOffset;
        public int TabularDataLength;
        public int RecordCount;
        public GeoLatLngBounds MapBounds = new GeoLatLngBounds();
        public int NumberOfFields;
        public bool IsJava;
        public DataField[] Fields;

        public readonly byte[] JavaArray = new byte[] { 0x00, 0x04, 0x4a, 0x41, 0x56, 0x41 };
        /**
         * when store latitude/longitude , it store as integer.
         * to Convert to an interget ,it muliple by DOUBLE_PRECISION.
         */
        private const double DOUBLE_PRECISION = 10000000.0;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public Header(BinaryReader reader, long offset, long size)
            : base(reader, offset, size)
        {


            if (!IsJavaFormat())
            {
                throw new IOException("Invalid map file format!");
            }
            DataReader.Seek(reader, 0);
            FileVersion = DataReader.ReadString(reader);
            DataReader.Seek(reader, 16);
            FileFormat = DataReader.ReadString(reader);
            DataReader.Seek(reader, 32);
            DominantType = DataReader.ReadString(reader);
            DataReader.Seek(reader, 48);
            RecordCount = DataReader.ReadInt(reader);
            int minX, minY, maxX, maxY;
            minX = DataReader.ReadInt(reader);
            minY = DataReader.ReadInt(reader);
            maxX = DataReader.ReadInt(reader);
            maxY = DataReader.ReadInt(reader);
            MapBounds.X = minX / DOUBLE_PRECISION;
            MapBounds.Y = minY / DOUBLE_PRECISION;
            MapBounds.Width = (maxX - minX) / DOUBLE_PRECISION;
            MapBounds.Height = (maxY - minY) / DOUBLE_PRECISION;
            IndexOffset = DataReader.ReadInt(reader);
            IndexLength = DataReader.ReadInt(reader);
            RtreeIndexOffset = DataReader.ReadInt(reader);
            RtreeIndexLength = DataReader.ReadInt(reader);
            StringIndexOffset = DataReader.ReadInt(reader);
            StringIndexLength = DataReader.ReadInt(reader);
            StringDataOffset = DataReader.ReadInt(reader);
            StringDataLength = DataReader.ReadInt(reader);
            GeoDataOffset = DataReader.ReadInt(reader);
            GeoDataLength = DataReader.ReadInt(reader);
            TabularDataOffset = DataReader.ReadInt(reader);
            TabularDataLength = DataReader.ReadInt(reader);
            NumberOfFields = DataReader.ReadInt(reader);
            Fields = new DataField[NumberOfFields];
            for (int i = 0; i < NumberOfFields; i++)
            {
                string fieldName = DataReader.ReadString(reader);
                byte fieldType = reader.ReadByte();
                int fieldWidth = DataReader.ReadInt(reader);
                short fieldPrecision = DataReader.ReadShort(reader);
                Fields[i] = new DataField(fieldName, fieldType, fieldWidth, fieldPrecision);
            }
            this._size = (NumberOfFields + 1) * 256;


        }


        private bool IsJavaFormat()
        {
            byte[] buffer = new byte[JavaArray.Length];
            DataReader.Seek(_reader, 16);
            _reader.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] != JavaArray[i])
                {
                    return false;
                }
            }
            return true;


        }

    }

}
