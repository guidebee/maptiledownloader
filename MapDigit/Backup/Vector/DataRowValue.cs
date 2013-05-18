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


//--------------------------------- PACKAGE ------------------------------------
using System;

namespace MapDigit.GIS.Vector
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines a row of a database table.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class DataRowValue
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * default constructor.
         */
        public DataRowValue(string[] fieldValues)
        {
            this._fieldValues = fieldValues;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the value of the specified column as a string.
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a string.
         */
        public string GetString(int ordinal)
        {
            if (ordinal >= 0 && ordinal < _fieldValues.Length)
            {
                return _fieldValues[ordinal];
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
         * Gets the value of the specified column as a integer.
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a integer.
         */
        public int GetInt(int ordinal)
        {
            try
            {
                if (ordinal >= 0 && ordinal < _fieldValues.Length)
                {
                    return int.Parse(_fieldValues[ordinal]);
                }
            }
            catch (Exception)
            {
                //ingore the exception.
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
         * Gets the value of the specified column as a short.
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a short.
         */
        public short GetShort(int ordinal)
        {
            try
            {
                if (ordinal >= 0 && ordinal < _fieldValues.Length)
                {
                    return short.Parse(_fieldValues[ordinal]);
                }
            }
            catch (Exception)
            {
                //ingore the exception.
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
         * Gets the value of the specified column as a double.
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a double.
         */
        public double GetDouble(int ordinal)
        {
            try
            {
                if (ordinal >= 0 && ordinal < _fieldValues.Length)
                {
                    return double.Parse(_fieldValues[ordinal]);
                }
            }
            catch (Exception)
            {
                //ingore the exception.
            }

            return 0.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the value of the specified column as a string(Date).
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a string.
         */
        public string GetDate(int ordinal)
        {
            try
            {
                if (ordinal >= 0 && ordinal < _fieldValues.Length)
                {
                    return _fieldValues[ordinal];
                }
            }
            catch (Exception)
            {
                //ingore the exception.
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
         * Gets the value of the specified column as a bool.
         * @param ordinal The zero-based column ordinal.
         * @return The value of the specified column as a bool.
         */
        public bool GetBoolean(int ordinal)
        {
            try
            {
                if (ordinal >= 0 && ordinal < _fieldValues.Length)
                {
                    if (byte.Parse(_fieldValues[ordinal]) != 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                //ingore the exception.
            }

            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert to string.
         */
        public override string ToString()
        {
            string retStr = "";
            for (int i = 0; i < _fieldValues.Length - 1; i++)
            {
                if (IsNumber(_fieldValues[i]))
                {
                    retStr += _fieldValues[i] + ",";
                }
                else
                {
                    retStr += "\"" + _fieldValues[i] + "\"" + ",";
                }
            }
            if (IsNumber(_fieldValues[_fieldValues.Length - 1]))
            {
                retStr += _fieldValues[_fieldValues.Length - 1];
            }
            else
            {
                retStr += "\"" + _fieldValues[_fieldValues.Length - 1] + "\"";
            }
            return retStr;
        }


        /**
         * internal store all field values in string format.
         */
        private readonly string[] _fieldValues;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if the string can be converted to a number.
         */
        private static bool IsNumber(string strValue)
        {
            try
            {
                double.Parse(strValue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
