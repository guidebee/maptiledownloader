//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 07JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.AJAX.JSON
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 07JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * JSONPath defines a simple expression language which works a lot like a very 
     * small subset of XPath. - the expression syntax uses the dot character 
     * for sub-elements and square brackets for arrays. Some sample expressions are,
     * for example - "photos.photo[1].title", "[0].location", "[1].status.text", etc 
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 08/06/09
     * @author      Guidebee, Inc. & JSON.org
     */
    public abstract class JSONPath
    {

        /**
         * Dot character used as the separator.
         */
        public const char SEPARATOR = '.';

        /**
         * Array start character.
         */
        public const char ARRAY_START = '[';

        /**
         * Array end character.
         */
        public const char ARRAY_END = ']';

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the boolean value based on the path.
         * @param path the path string.
         * @return the boolean values.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to boolean.
         */
        public abstract bool GetAsBoolean(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the integer value based on the path.
         * @param path the path string.
         * @return the integer values.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to integer.
         */
        public abstract int GetAsInteger(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the long value based on the path.
         * @param path the path string.
         * @return the long values.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to long integer.
         */
        public abstract long GetAsLong(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the double value based on the path.
         * @param path the path string.
         * @return the double values.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to double.
         */
        public abstract double GetAsDouble(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the String value based on the path.
         * @param path the path string.
         * @return the string values.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to string.
         */
        public abstract string GetAsString(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the size of the array.
         * @param path the path string.
         * @return the size of a given arrary.
         * @throws JSONException if the path is invalid or the value cannot be 
         * casted to array.
         */
        public abstract int GetSizeOfArray(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the a string array.
         * @param path the path string.
         * @return a string array.
         * @throws JSONException if the path is invalid .
         */
        public abstract string[] GetAsStringArray(string path);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the a integer array.
         * @param path the path string.
         * @return a integer array.
         * @throws JSONException if the path is invalid .
         */
        public abstract int[] GetAsIntegerArray(string path);

    }
}
