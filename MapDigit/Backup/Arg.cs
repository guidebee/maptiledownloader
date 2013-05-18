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
     * Arg defines HTTP header/value pair.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 12/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class Arg
    {

        // some commonly used http header names
        /**
         * Http header authorization.
         */
        public const string AUTHORIZATION = "authorization";

        /**
         * Http header content-length.
         */
        public const string CONTENT_LENGTH = "content-length";

        /**
         * Http header content-type.
         */
        public const string CONTENT_TYPE = "content-type";

        /**
         * Http header content-disposition.
         */
        public const string CONTENT_DISPOSITION = "content-disposition";

        /**
         * Http header content-transfer-encoding.
         */
        public const string CONTENT_TRANSFER_ENCODING
                = "content-transfer-encoding";

        /**
         * Http header last-modified.
         */
        public const string LAST_MODIFIED = "last-modified";

        /**
         * Http header if-modified-since.
         */
        public const string IF_MODIFIED_SINCE = "if-modified-since";

        /**
         * Http header etag.
         */
        public const string ETAG = "etag";

        /**
         * Http header if-none-match.
         */
        public const string IF_NONE_MATCH = "if-none-match";

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.  value is allowed to be null
         * @param k http header name.
         * @param v http header value.
         */
        public Arg(string k, string v)
        {
            if (string.IsNullOrEmpty(k))
            {
                throw new ArgumentException("invalid key");
            }
            _key = k;
            _value = v;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the http header name.
         * @return the http header name.
         */
        public string GetKey()
        {
            return _key;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the http header value.
         * @return the http header value.
         */
        public string GetValue()
        {
            return _value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a hash code value for the object. 
         * @return the hash code.
         */
        public int HashCode()
        {
            return _value == null ? _key.GetHashCode()
                    : _key.GetHashCode() ^ _value.GetHashCode();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Indicates whether some other object is "equal to" this one. 
         * @param other the reference object with which to compare. 
         * @return true if this object is the same as the obj argument; false
         * otherwise.
         */
        public new bool Equals(object other)
        {
            if (other is Arg)
            {
                var oa = ((Arg)other);
                return _value == null ? _key.Equals(oa._key) :
                    _key.Equals(oa._key) && _value.Equals(oa._value);
            }
            return false;
        }

        /**
         * the http header key.
         */
        private readonly string _key;

        /**
         * the http header value.
         */
        private readonly string _value;

    }
}
