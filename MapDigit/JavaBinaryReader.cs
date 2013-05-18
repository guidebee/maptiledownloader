using System;
using System.IO;

namespace MapDigit
{
    public class JavaBinaryReader
    {
        private readonly BinaryReader _reader;

        public JavaBinaryReader(BinaryReader reader)
        {
            _reader = reader;
        }

        public byte ReadByte()
        {
            return _reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return _reader.ReadBytes(count);
        }

        public double ReadDouble()
        {
            byte[] buffer = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                buffer[7 - i] = _reader.ReadByte();
            }
            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader bd = new BinaryReader(ms);
            double ret = bd.ReadDouble();
            ms.Close();
            bd.Close();
            return ret;

        }

        public short ReadInt16()
        {
            byte[] buffer = new byte[2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[buffer.Length - 1 - i] = _reader.ReadByte();
            }
            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader bd = new BinaryReader(ms);
            short ret = bd.ReadInt16();
            ms.Close();
            bd.Close();
            return ret;
        }

        public int ReadInt32()
        {
            byte[] buffer = new byte[4];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[buffer.Length - 1 - i] = _reader.ReadByte();
            }
            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader bd = new BinaryReader(ms);
            int ret = bd.ReadInt32();
            ms.Close();
            bd.Close();
            return ret;
        }

        protected void Write7BitEncodedInt(int value, BinaryWriter writer)
        {
            // Write out an int 7 bits at a time. The high bit of the byte,
            // when on, tells _reader to continue reading more bytes.
            uint v = (uint)value; // support negative numbers
            while (v >= 0x80)
            {
                writer.Write((byte)(v | 0x80));
                v >>= 7;
            }
            writer.Write((byte)v);
        }


        public long ReadInt64()
        {
            byte[] buffer = new byte[8];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[buffer.Length - 1 - i] = _reader.ReadByte();
            }
            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader bd = new BinaryReader(ms);
            long ret = bd.ReadInt64();
            ms.Close();
            bd.Close();
            return ret;
        }

        public string ReadString()
        {
            short len = ReadInt16();
            byte[] buffer = _reader.ReadBytes(len);
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            Write7BitEncodedInt(len, bw);
            bw.Write(buffer);
            BinaryReader bd = new BinaryReader(ms);
            ms.Seek(0, SeekOrigin.Begin);
            String ret = bd.ReadString();
            bd.Close();
            bw.Close();
            ms.Close();
            return ret;

        }

        public void Close()
        {
            _reader.Close();
        }

    }

    public class JavaBinaryWriter
    {
        private readonly BinaryWriter _writer;


        protected int Read7BitEncodedInt(BinaryReader reader)
        {
            // Read out an int 7 bits at a time. The high bit
            // of the byte when on means to continue reading more bytes.
            int count = 0;
            int shift = 0;
            byte b;
            do
            {
                b = reader.ReadByte();
                count |= (b & 0x7F) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);
            return count;
        }


        public JavaBinaryWriter(BinaryWriter writer)
        {
            _writer = writer;
        }

        public void Write(byte value)
        {
            _writer.Write(value);
        }

        public void Write(byte[] buffer)
        {
            _writer.Write(buffer);
        }

        public void Write(double value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(value);


            byte[] bytArray = ms.GetBuffer();
            byte[] buffer = new byte[ms.Length];
            for (int i = 0; i < ms.Length; i++)
            {
                buffer[ms.Length - 1 - i] = bytArray[i];
            }
            _writer.Write(buffer);
            bw.Close();
            ms.Close();
        }

        public void Write(Int16 value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(value);


            byte[] bytArray = ms.GetBuffer();
            byte[] buffer = new byte[ms.Length];
            for (int i = 0; i < ms.Length; i++)
            {
                buffer[ms.Length - 1 - i] = bytArray[i];
            }
            _writer.Write(buffer);
            bw.Close();
            ms.Close();
        }

        public void Write(Int32 value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(value);


            byte[] bytArray = ms.GetBuffer();
            byte[] buffer = new byte[ms.Length];
            for (int i = 0; i < ms.Length; i++)
            {
                buffer[ms.Length - 1 - i] = bytArray[i];
            }
            _writer.Write(buffer);
            bw.Close();
            ms.Close();
        }

        public void Write(Int64 value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(value);


            byte[] bytArray = ms.GetBuffer();
            byte[] buffer = new byte[ms.Length];
            for (int i = 0; i < ms.Length; i++)
            {
                buffer[ms.Length - 1 - i] = bytArray[i];
            }
            _writer.Write(buffer);
            bw.Close();
            ms.Close();
        }


        public void Write(String value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(value);
            BinaryReader br = new BinaryReader(ms);
            ms.Seek(0, SeekOrigin.Begin);
            short len = (short)Read7BitEncodedInt(br);
            Write(len);
            byte[] bytArray = ms.GetBuffer();
            if (len > 127)
            {
                _writer.Write(bytArray, 2, len);
            }
            else
            {
                _writer.Write(bytArray, 1, len);
            }

        }

        public void Close()
        {
            _writer.Close();
        }



    }


}
