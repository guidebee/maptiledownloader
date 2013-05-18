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
using System.Collections;
using System.IO;
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
     * GB2312 get Pinyin code for a chinese.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class Gb2312
    {

        /**
         * the data input reader for the map file.
         */
        private readonly BinaryReader _reader;

        /**
         * how many chinese character.
         */
        private const int NUMBER_OF_CHINESE = 27954;

        /**
         * the size of each record.
         */
        private const int RECORDSIZE = 16;

        /**
         * the first Pinyin letter;
         */
        private string _firstLetter;


        /**
         * the first Pinyin letter;
         */
        private static string StaticFirstLetter;



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public Gb2312(BinaryReader reader)
        {
            _reader = reader;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * binary search (Chinese).
         * @param queryValue query string.
         * @return the string recrod ID.
         */
        public int BinarySearch(string queryValue)
        {
            int left = 0;
            int right = NUMBER_OF_CHINESE - 1;
            while (left <= right)
            {
                int middle = (int)Math.Floor((left + right) / 2.0);
                {
                    DataReader.Seek(_reader, middle * RECORDSIZE);
                    string middleValuePinYin = DataReader.ReadString(_reader);
                    DataReader.Seek(_reader, middle * RECORDSIZE + 8);
                    string middleValue = DataReader.ReadString(_reader);

                    DataReader.Seek(_reader, left * RECORDSIZE);
                    DataReader.ReadString(_reader);
                    DataReader.Seek(_reader, left * RECORDSIZE + 8);
                    string leftValue = DataReader.ReadString(_reader);

                    DataReader.Seek(_reader, right * RECORDSIZE);
                    DataReader.ReadString(_reader);
                    DataReader.Seek(_reader, right * RECORDSIZE + 8);
                    string rightValue = DataReader.ReadString(_reader);

                    if (leftValue.Length > queryValue.Length)
                        leftValue = leftValue.Substring(0, queryValue.Length);
                    if (middleValue.Length > queryValue.Length)
                        middleValue = middleValue.Substring(0, queryValue.Length);
                    if (rightValue.Length > queryValue.Length)
                        rightValue = rightValue.Substring(0, queryValue.Length);

                    if (queryValue.CompareTo(middleValue) == 0)
                    {
                        _firstLetter = middleValuePinYin.Substring(0, 1);
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
            _firstLetter = queryValue.Substring(0, 1);
            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * binary search (Chinese).
         * @param queryValue query string.
         * @return the string recrod ID.
         */
        public static int BinarySearch(string queryValue, BinaryReader reader)
        {
            int left = 0;
            int right = NUMBER_OF_CHINESE - 1;
            while (left <= right)
            {
                int middle = (int)Math.Floor((left + right) / 2.0);
                {
                    DataReader.Seek(reader, middle * RECORDSIZE);
                    string middleValue = DataReader.ReadString(reader);
                    DataReader.Seek(reader, middle * RECORDSIZE + 8);
                    string middleValuePinYin = DataReader.ReadString(reader);

                    DataReader.Seek(reader, left * RECORDSIZE);
                    string leftValue = DataReader.ReadString(reader);
                    DataReader.Seek(reader, left * RECORDSIZE + 8);
                    DataReader.ReadString(reader);

                    DataReader.Seek(reader, right * RECORDSIZE);
                    string rightValue = DataReader.ReadString(reader);
                    DataReader.Seek(reader, right * RECORDSIZE + 8);
                    DataReader.ReadString(reader);

                    if (leftValue.Length > queryValue.Length)
                        leftValue = leftValue.Substring(0, queryValue.Length);
                    if (middleValue.Length > queryValue.Length)
                        middleValue = middleValue.Substring(0, queryValue.Length);
                    if (rightValue.Length > queryValue.Length)
                        rightValue = rightValue.Substring(0, queryValue.Length);

                    if (queryValue.CompareTo(middleValue) == 0)
                    {
                        StaticFirstLetter = middleValuePinYin;
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
            StaticFirstLetter = queryValue;
            return -1;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get pinyin code for given chinese string. the pinyin code is consists with
         * first letter of pinyin for each Chinese character.
         * @param chinese the Chinese string.
         * @return pinyin code.
         */
        public string GetPinyinCode(string chinese)
        {
            string ret = "";
            try
            {
                for (int i = 0; i < chinese.Length; i++)
                {
                    string keyValue = chinese.Substring(i, i + 1);
                    BinarySearch(keyValue);
                    ret += _firstLetter;
                }

            }
            catch (IOException)
            {
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
         * Get pinyin code for given chinese string. the pinyin code is consists with
         * first letter of pinyin for each Chinese character.
         * @param chinese the Chinese string.
         * @return pinyin code.
         */
        public static string GetPinyinCode(string chinese, BinaryReader reader)
        {
            string ret = "";
            try
            {
                for (int i = 0; i < chinese.Length; i++)
                {
                    string keyValue = chinese.Substring(i, i + 1);
                    BinarySearch(keyValue, reader);
                    ret += StaticFirstLetter;
                }

            }
            catch (IOException)
            {
            }

            return ret;
        }

        private static string GetPinYinAtPosition(int chineseId, string queryValue,
                BinaryReader reader)
        {
            DataReader.Seek(reader, chineseId * RECORDSIZE);
            string retValue = DataReader.ReadString(reader);
            DataReader.Seek(reader, chineseId * RECORDSIZE + 8);
            string retValuePinYin = DataReader.ReadString(reader);
            if (retValue.CompareTo(queryValue) == 0)
            {
                return retValuePinYin;
            }
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get pinyin code for given chinese string. the pinyin code is consists with
         * first letter of pinyin for each Chinese character.
         * @param chinese the Chinese string.
         * @return pinyin code.
         */
        public static ArrayList[] GetPinyinCodes(string chinese, BinaryReader reader)
        {
            int strLen = chinese.Length;
            ArrayList[] pinyinCodes = new ArrayList[strLen];
            try
            {
                for (int i = 0; i < strLen; i++)
                {
                    pinyinCodes[i] = new ArrayList();
                    string keyValue = chinese.Substring(i, i + 1);
                    int chineseId = BinarySearch(keyValue, reader);
                    if (chineseId != -1)
                    {
                        int checkId = chineseId;
                        string pinyin = GetPinYinAtPosition(checkId, keyValue, reader);
                        while (pinyin != null)
                        {
                            pinyinCodes[i].Add(pinyin);
                            checkId++;
                            pinyin = GetPinYinAtPosition(checkId, keyValue, reader);
                        }
                        checkId = chineseId - 1;
                        pinyin = GetPinYinAtPosition(checkId, keyValue, reader);
                        while (pinyin != null)
                        {
                            pinyinCodes[i].Add(pinyin);
                            checkId--;
                            pinyin = GetPinYinAtPosition(checkId, keyValue, reader);
                        }

                    }
                    else
                    {
                        pinyinCodes[i].Add(keyValue);
                    }
                }

            }
            catch (IOException)
            {
            }

            return pinyinCodes;
        }
    }

}
