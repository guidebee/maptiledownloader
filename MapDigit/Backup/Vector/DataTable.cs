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
using MapDigit.GIS.Vector.MapFile;

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
     * Define one database table.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class DataTable
    {

        /**
         * Data table definition.
         */
        public DataField[] Fields;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        internal DataTable(TabularData tabularData, DataField[] fields, int recordCount)
        {
            _tabularData = tabularData;
            Fields = fields;
            _recordCount = recordCount;
            _currentIndex = 1;
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
            _currentIndex = mapInfoID;
            return _tabularData.GetRecord(mapInfoID);
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
            return _recordCount;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get next record (index).
         */
        public DataRowValue MoveFirst()
        {
            _currentIndex = 1;
            return GetRecord(_currentIndex);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get next record (index).
         */
        public DataRowValue MoveLast()
        {
            _currentIndex = _recordCount;
            return GetRecord(_currentIndex);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get next record (index).
         */
        public DataRowValue MovePrevious()
        {
            _currentIndex--;
            if (_currentIndex == 0)
            {
                throw new IOException("Passed the first record!");
            }
            return ReadOneRecord();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get previous record (index).
         */
        public DataRowValue MoveNext()
        {
            _currentIndex++;
            if (_currentIndex > _recordCount)
            {
                throw new IOException("Passed the last the record!");
            }
            return ReadOneRecord();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the column index of given column name.
         * @param fieldName the name of the column.
         * @return the index of the column(field) or -1 if not found.
         */
        public int GetFieldIndex(string fieldName)
        {
            int ret = -1;
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i].GetName().ToLower().Equals(fieldName.ToLower()))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        /**
         * current index id.
         */
        private int _currentIndex;
        /**
         * Tabular data
         */
        private readonly TabularData _tabularData;
        /**
         * total record Count;
         */
        private readonly int _recordCount;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * read current record.
         */
        private DataRowValue ReadOneRecord()
        {
            return GetRecord(_currentIndex);
        }
    }

}
