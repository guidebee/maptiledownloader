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
using System;
using System.IO;
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector.RTree
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * A page file that stores all pages into Persistent storage. It uses a
     * RandomAccessFile to store node data.
     * The format of the page file is as follows. First, a header is writen
     * that stores important information
     * about the RTree. The header format is as shown below:
     * <br>
     * &nbsp;&nbsp;int dimension<br>
     * &nbsp;&nbsp;double fillFactor<br>
     * &nbsp;&nbsp;int nodeCapacity<br>
     * &nbsp;&nbsp;int pageSize<br>
     * &nbsp;&nbsp;int treeType<br>
     * <p>
     * All the pages are stored after the header, with the following format:
     * <br>
     * &nbsp;&nbsp;int parent<br>
     * &nbsp;&nbsp;int level<br>
     * &nbsp;&nbsp;int usedSpace<br>
     * &nbsp;&nbsp;// HyperCubes<br>
     * &nbsp;&nbsp;for (i in usedSpace)<br>
     * &nbsp;&nbsp;&nbsp;&nbsp;for (j in dimension) {<br>
     * &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;double p(i)1 [j]<br>
     * &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;double p(i)2 [j]<br>
     * &nbsp;&nbsp;&nbsp;&nbsp;}<br>
     * &nbsp;&nbsp;&nbsp;&nbsp;int branch<br>
     * &nbsp;&nbsp;}
     * <p>
     * Deleted pages are stored into a Stack. If a new entry is inserted it
     * is placed in the last deleted page.
     * That way the page file does not grow for ever.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class PersistentPageFile : PageFile
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public PersistentPageFile(BinaryReader reader, long offset,
                long size)
        {
            this._reader = reader;
            this._offset = offset;
            this._size = size;
            if (size >= HEADER_SIZE)
            {
                DataReader.Seek(reader, offset);
                Dimension = DataReader.ReadInt(reader);
                FillFactor = DataReader.ReadDouble(reader);
                NodeCapacity = DataReader.ReadInt(reader);
                PageSize = DataReader.ReadInt(reader);
                TreeType = DataReader.ReadInt(reader);

            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Close the file.
         */
        public void Close()
        {
            _reader.Close();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        public long Size()
        {
            return _size;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        internal override AbstractNode ReadNode(int page)
        {
            if (page < 0)
            {
                throw new ArgumentException("Page number cannot be negative.");
            }

            try
            {
                DataReader.Seek(_reader, _offset + HEADER_SIZE + page * PageSize);

                byte[] b = new byte[PageSize];
                int l = _reader.Read(b, 0, b.Length);
                if (-1 == l)
                {
                    throw new PageFaultException("EOF found while trying to read page "
                            + page + ".");
                }

                BinaryReader ds = new BinaryReader(new MemoryStream(b));

                int parent = DataReader.ReadInt(ds);
                if (parent == EMPTY_PAGE)
                {
                    throw new PageFaultException("Page " + page + " is empty.");
                }

                int level = DataReader.ReadInt(ds);
                int usedSpace = DataReader.ReadInt(ds);

                AbstractNode n;
                if (level != 0)
                {
                    n = new Index(Tree, parent, page, level);
                }
                else
                {
                    n = new Leaf(Tree, parent, page);
                }

                n.Parent = parent;
                n.Level = level;
                n.UsedSpace = usedSpace;

                double[] p1 = new double[Dimension];
                double[] p2 = new double[Dimension];

                for (int i = 0; i < usedSpace; i++)
                {
                    for (int j = 0; j < Dimension; j++)
                    {
                        p1[j] = DataReader.ReadDouble(ds);
                        p2[j] = DataReader.ReadDouble(ds);
                    }

                    n.Data[i] = new HyperCube(new Point(p1), new Point(p2));
                    n.Branches[i] = DataReader.ReadInt(ds);
                }

                return n;
            }
            catch (IOException)
            {

                return null;
            }

        }

        private const int EMPTY_PAGE = -2;
        /**
         * Stores node data into Persistent storage.
         */
        private readonly BinaryReader _reader;
        /**
         * Header size calculated using the following formula:
         * headerSize = dimension + fillFactor + nodeCapacity + pageSize + treeType
         */
        private const int HEADER_SIZE = 24;
        private readonly long _offset;
        private readonly long _size;
    }
}
