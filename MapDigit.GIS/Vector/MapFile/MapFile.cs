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
using MapDigit.GIS.Geometry;
using System.Collections;
using MapDigit.GIS.Vector.RTree;
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
     * Guidebee Map file reader.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapFile
    {

        /**
         * file header section object.
         */
        public Header Header;

        /**
         * tabular data section.
         */
        public TabularData TabularData;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param reader  the input stream of the map file.
         */
        public MapFile(BinaryReader reader)
        {
            this._reader = reader;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Open the map file.
         */
        public void Open()
        {


            if (Header != null)
            {
                return;
            }

            Header = new Header(_reader, 0, 0);
            _recordIndex = new RecordIndex(_reader, Header.IndexOffset,
                    Header.IndexLength);
            _stringIndex = new StringIndex(_reader, Header.StringIndexOffset,
                    Header.StringIndexLength);
            _stringData = new StringData(_reader, Header.StringDataOffset,
                    Header.StringDataLength);
            _geoData = new GeoData(_reader, Header.GeoDataOffset,
                    Header.GeoDataLength);
            TabularData = new TabularData(_reader, Header.TabularDataOffset,
                    Header.TabularDataLength, Header.Fields,
                    _stringData, _stringIndex);
            _rtreeIndex = new RTreeIndex(_reader, Header.RtreeIndexOffset,
                    Header.RtreeIndexLength);
            _tree = new RTree.RTree(_rtreeIndex.File);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Close the map file.
         */
        public void Close()
        {
            if (_reader != null)
            {
                _reader.Close();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the total record number.
         * @return the total record number.
         */
        public int GetRecordCount()
        {
            if (Header != null)
            {
                return Header.RecordCount;
            }
            return 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get MapObject at given mapInfoID.
         * @param mapInfoID the index of the record(MapInfoID).
         * @return MapObject at given mapInfoID.
         */
        public MapObject GetMapObject(int mapInfoID)
        {
            _recordIndex.GetRecord(mapInfoID);
            MapObject mapObject = _geoData.GetMapObject(_recordIndex);
            mapObject.MapInfoID = mapInfoID;
            return mapObject;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get tabular record at given mapInfo ID.
         * @param mapInfoID the index of the record(MapInfoID).
         * @return tabular record at given mapInfoID.
         */
        public DataRowValue GetDataRowValue(int mapInfoID)
        {
            return TabularData.GetRecord(mapInfoID);
        }

        /**
         * reader to read the data from the map file.
         */
        private readonly BinaryReader _reader;

        /**
         *  record index section.
         */
        private RecordIndex _recordIndex;
        /**
         * rtree index section.
         */
        private RTreeIndex _rtreeIndex;
        /**
         * string index section.
         */
        private StringIndex _stringIndex;
        /**
         * string data section.
         */
        private StringData _stringData;
        /**
         * geo data section.
         */
        private GeoData _geoData;

        /**
         * when store latitude/longitude , it store as integer.
         * to Convert to an interget ,it muliple by DOUBLE_PRECISION.
         */
        private const double DOUBLE_PRECISION = 10000000.0;
        /**
         * Rtree index file.
         */
        private RTree.RTree _tree;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * binary search (English).
         * @param queryValue query string.
         * @return the string recrod ID.
         */
        private int BinarySearch(string queryValue)
        {
            int left = 0;
            int right = (int)(_stringIndex._size / StringIndex.RECORDSIZE) - 1;
            while (left <= right)
            {
                int middle = (int)Math.Floor((left + right) / 2.0);
                {
                    _stringIndex.GetRecord(middle);
                    string middleValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    _stringIndex.GetRecord(left);
                    string leftValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    _stringIndex.GetRecord(right);
                    string rightValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    if (leftValue.Length > queryValue.Length)
                    {
                        leftValue = leftValue.Substring(0, queryValue.Length);
                    }
                    if (middleValue.Length > queryValue.Length)
                    {
                        middleValue = middleValue.Substring(0, queryValue.Length);
                    }
                    if (rightValue.Length > queryValue.Length)
                    {
                        rightValue = rightValue.Substring(0, queryValue.Length);
                    }

                    if (queryValue.CompareTo(middleValue) == 0)
                    {
                        return middle;
                    }
                    if (queryValue.CompareTo(middleValue) > 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        right = middle - 1;
                    }
                }

            }
            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * binary search (Chinese Pinyin).
         * @param queryValue query string.
         * @param GB2312File  the GB2312file.
         * @return the string recrod ID.
         */
        private int BinarySearch(string queryValue, BinaryReader gb2312File)
        {
            int left = 0;
            int right = (int)(_stringIndex._size / StringIndex.RECORDSIZE) - 1;
            string queryValuePinYin = Gb2312.GetPinyinCode(queryValue, gb2312File);
            while (left <= right)
            {
                int middle = (int)Math.Floor((left + right) / 2.0);
                {
                    _stringIndex.GetRecord(middle);
                    string middleValue =
                            _stringData.GetRecord(_stringIndex.RecordOffset);
                    string middleValuePinYin =
                            Gb2312.GetPinyinCode(middleValue, gb2312File);
                    _stringIndex.GetRecord(left);
                    string leftValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    string leftValuePinYin =
                            Gb2312.GetPinyinCode(leftValue, gb2312File);
                    _stringIndex.GetRecord(right);
                    string rightValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    string rightValuePinYin =
                            Gb2312.GetPinyinCode(rightValue, gb2312File);
                    if (leftValuePinYin.Length > queryValuePinYin.Length)
                    {
                        leftValuePinYin =
                                leftValuePinYin.Substring(0, queryValuePinYin.Length);
                    }
                    if (middleValuePinYin.Length > queryValuePinYin.Length)
                    {
                        middleValuePinYin = middleValuePinYin.Substring(0, queryValuePinYin.Length);
                    }
                    if (rightValuePinYin.Length > queryValuePinYin.Length)
                    {
                        rightValuePinYin = rightValuePinYin.Substring(0, queryValuePinYin.Length);
                    }

                    if (queryValuePinYin.CompareTo(middleValuePinYin) == 0)
                    {
                        return middle;
                    }
                    if (queryValuePinYin.CompareTo(middleValuePinYin) > 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        right = middle - 1;
                    }
                }

            }
            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * binary search (Chinese Pinyin).
         * @param queryValue query string.
         * @param GB2312File  the GB2312file.
         * @return the string recrod ID.
         */
        private int BinarySearchPinYin(string queryValue, BinaryReader gb2312File)
        {
            int left = 0;
            int right = (int)(_stringIndex._size / StringIndex.RECORDSIZE) - 1;
            string queryValuePinYin = queryValue;
            while (left <= right)
            {
                int middle = (int)Math.Floor((left + right) / 2.0);
                {
                    _stringIndex.GetRecord(middle);
                    string middleValue =
                            _stringData.GetRecord(_stringIndex.RecordOffset);
                    string middleValuePinYin =
                            Gb2312.GetPinyinCode(middleValue, gb2312File);
                    _stringIndex.GetRecord(left);
                    string leftValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    string leftValuePinYin =
                            Gb2312.GetPinyinCode(leftValue, gb2312File);
                    _stringIndex.GetRecord(right);
                    string rightValue = _stringData.GetRecord(_stringIndex.RecordOffset);
                    string rightValuePinYin =
                            Gb2312.GetPinyinCode(rightValue, gb2312File);
                    if (leftValuePinYin.Length > queryValuePinYin.Length)
                    {
                        leftValuePinYin =
                                leftValuePinYin.Substring(0, queryValuePinYin.Length);
                    }
                    if (middleValuePinYin.Length > queryValuePinYin.Length)
                    {
                        middleValuePinYin = middleValuePinYin.Substring(0,
                                queryValuePinYin.Length);
                    }
                    if (rightValuePinYin.Length > queryValuePinYin.Length)
                    {
                        rightValuePinYin = rightValuePinYin.Substring(0,
                                queryValuePinYin.Length);
                    }

                    if (queryValuePinYin.CompareTo(middleValuePinYin) == 0)
                    {
                        return middle;
                    }
                    if (queryValuePinYin.CompareTo(middleValuePinYin) > 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        right = middle - 1;
                    }
                }

            }
            return -1;
        }




        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get all records based on search condition.
         * @param findConditions the search condition.
         * @return a hashtable of all matched record.the key is the mapInfo ID.
         */
        public Hashtable Search(FindConditions findConditions)
        {
            Hashtable retTable = new Hashtable();
            ArrayList allCondition = findConditions.GetConditions();
            for (int i = 0; i < allCondition.Count; i++)
            {
                FindCondition findCondition = (FindCondition)allCondition[i];
                int stringID = BinarySearch(findCondition.MatchString);
                int fieldIndex = findCondition.FieldIndex;
                if (stringID != -1)
                {
                    bool bDone = false;
                    _stringIndex.GetRecord(stringID);
                    string strValue = _stringData.GetMapInfoIDAndField(_stringIndex.RecordOffset);
                    while (strValue.StartsWith(findCondition.MatchString) && (!bDone))
                    {
                        int fieldCount = _stringData.FieldCount;
                        for (int j = 0; j < fieldCount; j++)
                        {
                            if (_stringData.FieldID[j] == fieldIndex)
                            {
                                int mapInfoID = _stringData.MapInfoID[j];
                                if (!retTable.ContainsKey(mapInfoID))
                                {
                                    retTable.Add(mapInfoID,
                                            strValue);
                                }
                                if (retTable.Count > FindConditions.MaxMatchRecord)
                                {
                                    return retTable;
                                }
                            }
                        }
                        bDone = _stringIndex.MovePrevious();
                        if (!bDone)
                        {
                            strValue = _stringData.GetMapInfoIDAndField(_stringIndex.RecordOffset);
                        }
                    }
                    bDone = false;
                    _stringIndex.GetRecord(stringID);
                    strValue = _stringData.GetMapInfoIDAndField(_stringIndex.RecordOffset);
                    while (strValue.StartsWith(findCondition.MatchString) && (!bDone))
                    {
                        int fieldCount = _stringData.FieldCount;
                        for (int j = 0; j < fieldCount; j++)
                        {
                            if (_stringData.FieldID[j] == fieldIndex)
                            {
                                int mapInfoID = _stringData.MapInfoID[j];
                                if (!retTable.ContainsKey(mapInfoID))
                                {
                                    retTable.Add(mapInfoID,
                                            strValue);
                                }
                                if (retTable.Count > FindConditions.MaxMatchRecord)
                                {
                                    return retTable;
                                }
                            }
                        }
                        bDone = _stringIndex.MoveNext();
                        if (!bDone)
                        {
                            strValue = _stringData.GetMapInfoIDAndField(_stringIndex.RecordOffset);
                        }
                    }
                }

            }
            return retTable;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get table field definition.
         * @return table field definition.
         */
        public DataField[] GetFields()
        {
            return Header.Fields;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get all records based on given rectangle.
         * @param rectGeo the boundary..
         * @return a hashtable of all matched record.the key is the mapInfo ID.
         */
        public Hashtable SearchMapObjectsInRect(GeoLatLngBounds rectGeo)
        {
            Point pt1, pt2;
            pt1 = new Point(new[]{
                    (int) (rectGeo.X * DOUBLE_PRECISION+0.5),
                    (int) (rectGeo.Y * DOUBLE_PRECISION+0.5)});
            pt2 = new Point(new int[]{
                    (int) ((rectGeo.X + rectGeo.Width) * DOUBLE_PRECISION+0.5),
                    (int) ((rectGeo.Y + rectGeo.Height) * DOUBLE_PRECISION+0.5)});
            HyperCube h1 = new HyperCube(pt1, pt2);
            Hashtable retArrayList = new Hashtable();
            Point p11, p12;
            for (IEnumeration e1 = _tree.Intersection(h1); e1.HasMoreElements(); )
            {

                AbstractNode node = (AbstractNode)(e1.NextElement());
                if (node.IsLeaf())
                {
                    int index = 0;
                    HyperCube[] data = node.GetHyperCubes();
                    HyperCube cube;
                    for (int cubeIndex = 0; cubeIndex < data.Length; cubeIndex++)
                    {
                        cube = data[cubeIndex];
                        if (cube.Intersection(h1))
                        {
                            p11 = cube.GetP1();
                            p12 = cube.GetP2();
                            int mapinfoId = ((Leaf)node).GetDataPointer(index);
                            int mapInfoId = mapinfoId;
                            GeoLatLngBounds mbr = new GeoLatLngBounds();
                            mbr.X = p11.GetFloatCoordinate(0) / DOUBLE_PRECISION;
                            mbr.Y = p11.GetFloatCoordinate(1) / DOUBLE_PRECISION;
                            mbr.Width = ((p12.GetFloatCoordinate(0) - p11.GetFloatCoordinate(0))) / DOUBLE_PRECISION;
                            mbr.Height = ((p12.GetFloatCoordinate(1) - p11.GetFloatCoordinate(1))) / DOUBLE_PRECISION;
                            if (!retArrayList.Contains(mapInfoId))
                            {
                                retArrayList.Add(mapInfoId, mbr);
                            }

                        }
                        index++;

                    }
                }
            }
            return retArrayList;
        }
    }

}
