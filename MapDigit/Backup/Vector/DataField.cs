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
     * Defines a filed of a database table.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class DataField
    {

        /**
         * Data type is char(string).
         */
        public const byte TYPE_CHAR = 1;
        /**
         * Data type is integer(4 bytes).
         */
        public const byte TYPE_INTEGER = 2;
        /**
         * Data type is short (2 bytes).
         */
        public const byte TYPE_SMALLINT = 3;
        /**
         * Data type is double.
         */
        public const byte TYPE_DECIMAL = 4;
        /**
         * Data type is float.
         */
        public const byte TYPE_FLOAT = 5;
        /**
         * Data type is date.
         */
        public const byte TYPE_DATE = 6;
        /**
         * Data type is boolean.
         */
        public const byte TYPE_LOGICAL = 7;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param name the name of the field.
         * @param type the type of the filed.
         * @param Width the Width of the field.
         * @param precision the precision of the field.
         */
        public DataField(string name, byte type, int width, short precision)
        {
            _fieldName = name;
            _fieldType = type;
            _fieldWidth = width;
            _fieldPrecision = precision;
            if (_fieldType == 0)
            {
                _fieldType = TYPE_CHAR;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the name of the field.
         * @return the name of the field.
         */
        public string GetName()
        {
            return _fieldName;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the type of the field.
         * @return the type of the field.
         */
        public byte GetFieldType()
        {
            return _fieldType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the Width of the field.
         * @return the Width of the field.
         */
        public int GetWidth()
        {
            return _fieldWidth;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the precision of the field.
         * @return the precision of the field.
         */
        public short GetPrecision()
        {
            return _fieldPrecision;
        }

        /**
         * the name of the field.
         */
        private readonly string _fieldName;
        /**
         * the type of the field.
         */
        private readonly byte _fieldType;
        /**
         * the Width of the fields.
         */
        private readonly int _fieldWidth;
        /**
         * the precision of the field.
         */
        private readonly short _fieldPrecision;
    }

}
