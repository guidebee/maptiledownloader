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
using System.Collections;

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
     * Defines a find condition collection when seach for records.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class FindConditions
    {

        /**
         * condition is OR operation.
         */
        public const int LOGICAL_OR = 0;
        /**
         * condition is AND operation.
         */
        public const int LOGICAL_AND = 1;
        /**
         * the Max matching records, default 100;
         */
        public static int MaxMatchRecord = 100;
        /**
         * the table field defintion.
         */
        public DataField[] Fields;
        /**
         * the total conditions.
         */
        private readonly ArrayList _findConditions;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public FindConditions()
        {
            _findConditions = new ArrayList();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * clear all conditaions.
         */
        public void ClearCondition()
        {
            _findConditions.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Add one condition.
         * @param fieldIndex the index of column(field) in the table.
         * @param matchString sting to be matched (start with).
         */
        public void AddCondition(int fieldIndex, string matchString)
        {
            FindCondition condition = new FindCondition(fieldIndex, matchString);
            _findConditions.Add(condition);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Return the condition list.
         * @return the condition list.
         */
        public ArrayList GetConditions()
        {
            return _findConditions;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param fieldName the name of columm in the table. If no match, the first
         *  column is selected.
         * @param matchString string to be matched.
         */
        public void AddCondition(string fieldName, string matchString)
        {
            int fieldIndex = 0;
            if (Fields != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    if (Fields[i].GetName().ToLower().Equals(fieldName.ToLower()))
                    {
                        fieldIndex = i;
                        break;
                    }
                }
            }
            FindCondition condition = new FindCondition(fieldIndex, matchString);
            _findConditions.Add(condition);
        }
    }

}
