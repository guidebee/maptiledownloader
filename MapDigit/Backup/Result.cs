//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 12JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using MapDigit.AJAX.JSON;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.AJAX
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 12JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Http result object (JSONObject or JSONArray).
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 12/06/09
     * @author      Guidebee, Inc.
     */
    public class Result : JSONPath
    {

        /**
         * text/javascript content type.
         */
        public const string JS_CONTENT_TYPE = "text/javascript";

        /**
         * application/json content type.
         */
        public const string JSON_CONTENT_TYPE = "application/json";

        /**
         * text/plain content type.
         */
        public const string PLAIN_TEXT_CONTENT_TYPE = "text/plain";

        /**
         * text/xml content type.
         */
        public const string TEXT_XML_CONTENT_TYPE = "text/xml";

        /**
         * application/xml content type.
         */
        public const string APPLICATION_XML_CONTENT_TYPE = "application/xml";


        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the hash code.
         * @return the hash code of this object.
         */
        public int HashCode()
        {
            return _isArray ? _array.GetHashCode() : _json.GetHashCode();
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Indicates whether some other object is "equal to" this one. 
         * @param other the reference object with which to compare. 
         * @return true if this object is the same as the obj argument;
         * false otherwise.
         */
        public new bool Equals(object other)
        {
            if (other is Result)
            {
                return _isArray ? _array.Equals(other) : _json.Equals(other);
            }
            return false;
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of the object.
         * @return a string representation of the object.
         */
        public override string ToString()
        {
            try
            {
                return _isArray ? _array.ToString(2) :
                    _json.ToString(2);
            }
            catch (Exception)
            {
                return _json.ToString();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override bool GetAsBoolean(string path)
        {
            return _isArray ? _array.GetAsBoolean(path) : _json.GetAsBoolean(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int GetAsInteger(string path)
        {
            return _isArray ? _array.GetAsInteger(path) : _json.GetAsInteger(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override long GetAsLong(string path)
        {
            return _isArray ? _array.GetAsLong(path) : _json.GetAsLong(path);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override double GetAsDouble(string path)
        {
            return _isArray ? _array.GetAsDouble(path) : _json.GetAsDouble(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override string GetAsString(string path)
        {
            return _isArray ? _array.GetAsString(path) : _json.GetAsString(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int GetSizeOfArray(string path)
        {
            return _isArray ? _array.GetSizeOfArray(path) : _json.GetSizeOfArray(path);
        }

        // TODO: add array accessors for other types, or parameterize by type
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override string[] GetAsStringArray(string path)
        {
            return _isArray ? _array.GetAsStringArray(path) : _json.GetAsStringArray(path);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int[] GetAsIntegerArray(string path)
        {
            return _isArray ? _array.GetAsIntegerArray(path)
                    : _json.GetAsIntegerArray(path);
        }

        // TODO: add a cache mapping subpaths to objects to improve performance
        private readonly JSONObject _json;
        private readonly JSONArray _array;
        private readonly bool _isArray;

        internal static Result FromContent(string content,
             string contentType)
        {

            if (content == null)
            {
                throw new ArgumentException("content cannot be null");
            }

            if (JS_CONTENT_TYPE.Equals(contentType) ||
                JSON_CONTENT_TYPE.Equals(contentType) ||
                // some sites return JSON with the plain text content type
                PLAIN_TEXT_CONTENT_TYPE.Equals(contentType))
            {
                try
                {
                    return content.StartsWith("[") ?
                        new Result(new JSONArray(content)) :
                        new Result(new JSONObject(content));
                }
                catch (Exception ex)
                {
                    throw new JSONException(ex.Message);
                }
            }

            if (TEXT_XML_CONTENT_TYPE.Equals(contentType) ||
                 APPLICATION_XML_CONTENT_TYPE.Equals(contentType) ||
                // default to XML if content type is not specified
                 contentType == null)
            {
                try
                {
                    return new Result(JSONObject.FromXMLString(content));
                }
                catch (Exception ex)
                {
                    throw new JSONException(ex.Message);
                }
            }
            throw new JSONException("Unsupported content-type: " + contentType);
        }

        private Result(JSONObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("json object cannot be null");
            }
            _isArray = false;
            _json = obj;
            _array = null;
        }

        private Result(JSONArray obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("json object cannot be null");
            }
            _isArray = true;
            _json = null;
            _array = obj;
        }

    }

}
