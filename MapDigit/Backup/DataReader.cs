//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 25JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System.IO;

namespace MapDigit.Util
{
    public class DataReader
    {

        /**
         * is the data format java or windows( big endian or little endian).
         */
        public static bool IsNet;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Read a double value.
         * @param in data input stream.
         * @return a double value.
         * @throws IOException
         */
        public static double ReadDouble(BinaryReader reader)
        {
            double ret;
            if (!IsNet)
            {

                byte[] buffer = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    buffer[7 - i] = reader.ReadByte();
                }
                MemoryStream bais = new MemoryStream(buffer);
                BinaryReader dis = new BinaryReader(bais);

                ret = dis.ReadDouble();
                dis.Close();
                bais.Close();
            }
            else
            {
                ret = reader.ReadDouble();
            }
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Read a long value.
         * @param in data input stream.
         * @return a long value.
         * @throws IOException
         */
        public static long ReadLong(BinaryReader reader)
        {
            long ret;
            if (!IsNet)
            {
                byte[] buffer = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    buffer[7 - i] = reader.ReadByte();
                }
                MemoryStream bais = new MemoryStream(buffer);
                BinaryReader dis = new BinaryReader(bais);
                ret = dis.ReadInt64();
                dis.Close();
                bais.Close();
            }
            else
            {
                ret = reader.ReadInt64();
            }

            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Read a integer value.
         * @param in data input stream.
         * @return a integer value.
         * @throws IOException
         */
        public static int ReadInt(BinaryReader reader)
        {
            int ret;
            if (!IsNet)
            {
                byte[] buffer = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    buffer[3 - i] = reader.ReadByte();
                }
                MemoryStream bais = new MemoryStream(buffer);
                BinaryReader dis = new BinaryReader(bais);
                ret = dis.ReadInt32();
                dis.Close();
                bais.Close();
            }
            else
            {
                ret = reader.ReadInt32();
            }
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Read a short integer value.
         * @param in data input stream.
         * @return a short integer value.
         * @throws IOException
         */
        public static short ReadShort(BinaryReader reader)
        {
            short ret;
            if (!IsNet)
            {
                byte[] buffer = new byte[2];
                for (int i = 0; i < 2; i++)
                {
                    buffer[1 - i] = reader.ReadByte();
                }
                MemoryStream bais = new MemoryStream(buffer);
                BinaryReader dis = new BinaryReader(bais);
                ret = dis.ReadInt16();
                dis.Close();
                bais.Close();
            }
            else
            {
                ret = reader.ReadInt16();
            }
            return ret;
        }


        protected static void Write7BitEncodedInt(int value, BinaryWriter writer)
        {
            // Write out an int 7 bits at a time. The high bit of the byte,
            // when on, tells reader to continue reading more bytes.
            uint v = (uint)value; // support negative numbers
            while (v >= 0x80)
            {
                writer.Write((byte)(v | 0x80));
                v >>= 7;
            }
            writer.Write((byte)v);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Read a string value.
         * @param in data input stream.
         * @return a string value.
         * @throws IOException
         */
        public static string ReadString(BinaryReader reader)
        {
            string retStr;
            if (!IsNet)
            {
                short len = ReadShort(reader);
                byte[] buffer = reader.ReadBytes(len);
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms);
                Write7BitEncodedInt(len, bw);
                bw.Write(buffer);
                BinaryReader bd = new BinaryReader(ms);
                ms.Seek(0, SeekOrigin.Begin);
                retStr = bd.ReadString();
                bd.Close();
                bw.Close();
                ms.Close();

            }
            else
            {
                retStr = reader.ReadString();
            }
            return retStr;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * move the file pointer to the given position.
         * @param in data input stream.
         * @param offset offset to move the file pointer.
         * @throws IOException 
         */
        public static void Seek(BinaryReader reader, long offset)
        {

            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

        }
    }

}
