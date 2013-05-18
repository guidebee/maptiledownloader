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
using System;
using System.Collections;
using System.IO;
using System.Text;

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
     * A JSONArray is an ordered sequence of values. Its external text form is a
     * string wrapped in square brackets with commas separating the values. The
     * internal form is an object having <code>get</code> and <code>opt</code>
     * methods for accessing the values by index, and <code>put</code> methods for
     * adding or replacing values. The values can be any of these types:
     * <code>Boolean</code>, <code>JSONArray</code>, <code>JSONObject</code>,
     * <code>Number</code>, <code>string</code>, or the
     * <code>JSONObject.NULL object</code>.
     * <p>
     * The constructor can Convert a JSON text into a Java object. The
     * <code>toString</code> method converts to JSON text.
     * <p>
     * A <code>get</code> method returns a value if one can be found, and throws an
     * exception if one cannot be found. An <code>opt</code> method returns a
     * default value instead of throwing an exception, and so is useful for
     * obtaining optional values.
     * <p>
     * The generic <code>get()</code> and <code>opt()</code> methods return an
     * object which you can cast or query for type. There are also typed
     * <code>get</code> and <code>opt</code> methods that do type checking and type
     * coersion for you.
     * <p>
     * The texts produced by the <code>toString</code> methods strictly conform to
     * JSON syntax rules. The constructors are more forgiving in the texts they will
     * accept:
     * <ul>
     * <li>An extra <code>,</code>&nbsp;<small>(comma)</small> may appear just
     *     before the closing bracket.</li>
     * <li>The <code>null</code> value will be inserted when there
     *     is <code>,</code>&nbsp;<small>(comma)</small> elision.</li>
     * <li>Strings may be quoted with <code>'</code>&nbsp;<small>(single
     *     quote)</small>.</li>
     * <li>Strings do not need to be quoted at all if they do not begin with a quote
     *     or single quote, and if they do not contain leading or trailing spaces,
     *     and if they do not contain any of these characters:
     *     <code>{ } [ ] / \ : , = ; #</code> and if they do not look like numbers
     *     and if they are not the reserved words <code>true</code>,
     *     <code>false</code>, or <code>null</code>.</li>
     * <li>Values can be separated by <code>;</code> <small>(semicolon)</small> as
     *     well as by <code>,</code> <small>(comma)</small>.</li>
     * <li>Numbers may have the <code>0-</code> <small>(octal)</small> or
     *     <code>0x-</code> <small>(hex)</small> prefix.</li>
     * <li>Comments written in the slashshlash, slashstar, and hash conventions
     *     will be ignored.</li>
     * </ul>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 07/06/00
     * @author      Guidebee, Inc. & JSON.org
     */
    public class JSONArray : JSONPath
    {
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct an empty JSONArray.
         */
        public JSONArray()
        {
            _myArrayList = new ArrayList();
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct a JSONArray from a source sJSON text.
         * @param string     A string that begins with
         * <code>[</code>&nbsp;<small>(left bracket)</small>
         *  and ends with <code>]</code>&nbsp;<small>(right bracket)</small>.
         *  @ If there is a syntax error.
         */
        public JSONArray(string str)
            : this(new JSONTokener(str))
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct a JSONArray from a Collection.
         * @param collection     A Collection.
         */
        public JSONArray(ArrayList collection)
        {
            if (collection == null)
            {
                _myArrayList = new ArrayList();
            }
            else
            {
                int size = collection.Count;
                _myArrayList = new ArrayList(size);
                for (int i = 0; i < size; i++)
                {
                    _myArrayList.Add(collection[i]);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct a JSONArray from a JSONTokener.
         * @param x A JSONTokener
         * @ If there is a syntax error.
         */
        internal JSONArray(JSONTokener x)
            : this()
        {

            if (x.NextClean() != '[')
            {
                throw x.SyntaxError("A JSONArray text must start with '['");
            }
            if (x.NextClean() == ']')
            {
                return;
            }
            x.Back();
            for (; ; )
            {
                if (x.NextClean() == ',')
                {
                    x.Back();
                    _myArrayList.Add(null);
                }
                else
                {
                    x.Back();
                    _myArrayList.Add(x.NextValue());
                }
                switch (x.NextClean())
                {
                    case ';':
                    case ',':
                        if (x.NextClean() == ']')
                        {
                            return;
                        }
                        x.Back();
                        break;
                    case ']':
                        return;
                    default:
                        throw x.SyntaxError("Expected a ',' or ']'");
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the object value associated with an index.
         * @param index The index must be between 0 and length() - 1.
         * @return An object value.
         * @ If there is no value for the index.
         */
        public object Get(int index)
        {
            var o = Opt(index);
            if (o == null)
            {
                throw new JSONException("JSONArray[" + index + "] not found.");
            }
            return o;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the boolean value associated with an index.
         * The string values "true" and "false" are converted to boolean.
         * @param index The index must be between 0 and length() - 1.
         * @return      The truth.
         * @ If there is no value for the index or if the
         *  value is not convertable to boolean.
         */
        public bool GetBoolean(int index)
        {
            var o = Get(index);
            if (o.Equals(true) ||
                o.Equals(JSONObject.FALSE) ||
                    (o is string &&
                    ((string)o).ToLower().Equals("false")))
            {
                return false;

            }
            if (o.Equals(true) ||
                       o.Equals(JSONObject.TRUE) ||
                      (o is string &&
                      ((string)o).ToLower().Equals("true")))
            {
                return true;
            }
            throw new JSONException("JSONArray[" + index + "] is not a Boolean.");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the double value associated with an index.
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         * @throws   JSONException If the key is not found or if the value cannot
         *  be converted to a number.
         */
        public double GetDouble(int index)
        {
            var o = Get(index);
            try
            {
                return double.Parse((string)o);
            }
            catch (Exception)
            {
                throw new JSONException("JSONArray[" + index +
                    "] is not a number.");
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the int value associated with an index.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         * @throws   JSONException If the key is not found or if the value cannot
         *  be converted to a number.
         *  if the value cannot be converted to a number.
         */
        public int GetInt(int index)
        {
            Get(index);
            return (int)GetDouble(index);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the JSONArray associated with an index.
         * @param index The index must be between 0 and length() - 1.
         * @return      A JSONArray value.
         * @ If there is no value for the index. or if the
         * value is not a JSONArray
         */
        public JSONArray GetJSONArray(int index)
        {
            var o = Get(index);
            if (o is JSONArray)
            {
                return (JSONArray)o;
            }
            throw new JSONException("JSONArray[" + index +
                    "] is not a JSONArray.");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the JSONObject associated with an index.
         * @param index subscript
         * @return      A JSONObject value.
         * @ If there is no value for the index or if the
         * value is not a JSONObject
         */
        public JSONObject GetJSONObject(int index)
        {
            var o = Get(index);
            if (o is JSONObject)
            {
                return (JSONObject)o;
            }
            throw new JSONException("JSONArray[" + index +
                "] is not a JSONObject.");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the long value associated with an index.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         * @throws   JSONException If the key is not found or if the value cannot
         *  be converted to a number.
         */
        public long GetLong(int index)
        {
            Get(index);
            return (long)GetDouble(index);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the string associated with an index.
         * @param index The index must be between 0 and length() - 1.
         * @return      A string value.
         * @ If there is no value for the index.
         */
        public string GetString(int index)
        {
            return Get(index).ToString();
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determine if the value is null.
         * @param index The index must be between 0 and length() - 1.
         * @return true if the value at the index is null, or if there is no value.
         */
        public bool IsNull(int index)
        {
            return JSONObject.NULL.Equals(Opt(index));
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a string from the contents of this JSONArray. The
         * <code>separator</code> string is inserted between each element.
         * Warning: This method assumes that the data structure is acyclical.
         * @param separator A string that will be inserted between the elements.
         * @return a string.
         * @ If the array contains an invalid number.
         */
        public string Join(string separator)
        {
            var len = Length();
            var sb = new StringBuilder();

            for (var i = 0; i < len; i += 1)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(JSONObject.ValueToString(_myArrayList[i]));
            }
            return sb.ToString();
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the number of elements in the JSONArray, included nulls.
         *
         * @return The length (or size).
         */
        public int Length()
        {
            return _myArrayList.Count;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional object value associated with an index.
         * @param index The index must be between 0 and length() - 1.
         * @return      An object value, or null if there is no
         *              object at that index.
         */
        public object Opt(int index)
        {
            return (index < 0 || index >= Length()) ?
                null : _myArrayList[index];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional boolean value associated with an index.
         * It returns false if there is no value at that index,
         * or if the value is not Boolean.TRUE or the string "true".
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The truth.
         */
        public bool OptBoolean(int index)
        {
            return OptBoolean(index, false);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional boolean value associated with an index.
         * It returns the defaultValue if there is no value at that index or if
         * it is not a Boolean or the string "true" or "false" (case insensitive).
         *
         * @param index The index must be between 0 and length() - 1.
         * @param defaultValue     A boolean default.
         * @return      The truth.
         */
        public bool OptBoolean(int index, bool defaultValue)
        {
            try
            {
                return GetBoolean(index);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional double value associated with an index.
         * NaN is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         */
        public double OptDouble(int index)
        {
            return OptDouble(index, Double.NaN);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional double value associated with an index.
         * The defaultValue is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         *
         * @param index subscript
         * @param defaultValue     The default value.
         * @return      The value.
         */
        public double OptDouble(int index, double defaultValue)
        {
            try
            {
                return GetDouble(index);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional int value associated with an index.
         * Zero is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         */
        public int OptInt(int index)
        {
            return OptInt(index, 0);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional int value associated with an index.
         * The defaultValue is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         * @param index The index must be between 0 and length() - 1.
         * @param defaultValue     The default value.
         * @return      The value.
         */
        public int OptInt(int index, int defaultValue)
        {
            try
            {
                return GetInt(index);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional JSONArray associated with an index.
         * @param index subscript
         * @return      A JSONArray value, or null if the index has no value,
         * or if the value is not a JSONArray.
         */
        public JSONArray OptJSONArray(int index)
        {
            var o = Opt(index);
            return o is JSONArray ? (JSONArray)o : null;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional JSONObject associated with an index.
         * Null is returned if the key is not found, or null if the index has
         * no value, or if the value is not a JSONObject.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      A JSONObject value.
         */
        public JSONObject OptJSONObject(int index)
        {
            var o = Opt(index);
            return o is JSONObject ? (JSONObject)o : null;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional long value associated with an index.
         * Zero is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      The value.
         */
        public long OptLong(int index)
        {
            return OptLong(index, 0);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional long value associated with an index.
         * The defaultValue is returned if there is no value for the index,
         * or if the value is not a number and cannot be converted to a number.
         * @param index The index must be between 0 and length() - 1.
         * @param defaultValue     The default value.
         * @return      The value.
         */
        public long OptLong(int index, long defaultValue)
        {
            try
            {
                return GetLong(index);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional string value associated with an index. It returns an
         * empty string if there is no value at that index. If the value
         * is not a string and is not null, then it is coverted to a string.
         *
         * @param index The index must be between 0 and length() - 1.
         * @return      A string value.
         */
        public string OptString(int index)
        {
            return OptString(index, "");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the optional string associated with an index.
         * The defaultValue is returned if the key is not found.
         *
         * @param index The index must be between 0 and length() - 1.
         * @param defaultValue     The default value.
         * @return      A string value.
         */
        public string OptString(int index, string defaultValue)
        {
            var o = Opt(index);
            return o != null ? o.ToString() : defaultValue;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a boolean value. This increases the array's length by one.
         *
         * @param value A boolean value.
         * @return this.
         */
        public JSONArray Put(bool value)
        {
            Put(value ? true : false);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put a value in the JSONArray, where the value will be a
         * JSONArray which is produced from a Collection.
         * @param value	A Collection value.
         * @return		this.
         */
        public JSONArray Put(ArrayList value)
        {
            Put(new JSONArray(value));
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a double value. This increases the array's length by one.
         *
         * @param value A double value.
         * @ if the value is not finite.
         * @return this.
         */
        public JSONArray Put(double value)
        {
            JSONObject.TestValidity(value);
            Put(value);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append an int value. This increases the array's length by one.
         *
         * @param value An int value.
         * @return this.
         */
        public JSONArray Put(int value)
        {
            Put(value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append an long value. This increases the array's length by one.
         *
         * @param value A long value.
         * @return this.
         */
        public JSONArray Put(long value)
        {
            Put(value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put a value in the JSONArray, where the value will be a
         * JSONObject which is produced from a Map.
         * @param value	A Map value.
         * @return		this.
         */
        public JSONArray Put(Hashtable value)
        {
            Put(new JSONObject(value));
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append an object value. This increases the array's length by one.
         * @param value An object value.  The value should be a
         *  Boolean, Double, Integer, JSONArray, JSONObject, Long, or string, or the
         *  JSONObject.NULL object.
         * @return this.
         */
        public JSONArray Put(object value)
        {
            _myArrayList.Add(value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put or replace a boolean value in the JSONArray. If the index is greater
         * than the length of the JSONArray, then null elements will be added as
         * necessary to pad it out.
         * @param index The subscript.
         * @param value A boolean value.
         * @return this.
         * @ If the index is negative.
         */
        public JSONArray Put(int index, bool value)
        {
            Put(index, value ? true : false);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put a value in the JSONArray, where the value will be a
         * JSONArray which is produced from a Collection.
         * @param index The subscript.
         * @param value	A Collection value.
         * @return		this.
         * @ If the index is negative or if the value is
         * not finite.
         */
        public JSONArray Put(int index, ArrayList value)
        {
            Put(index, new JSONArray(value));
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put or replace a double value. If the index is greater than the length of
         *  the JSONArray, then null elements will be added as necessary to pad
         *  it out.
         * @param index The subscript.
         * @param value A double value.
         * @return this.
         * @ If the index is negative or if the value is
         * not finite.
         */
        public JSONArray Put(int index, double value)
        {
            Put(index, value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put or replace an int value. If the index is greater than the length of
         *  the JSONArray, then null elements will be added as necessary to pad
         *  it out.
         * @param index The subscript.
         * @param value An int value.
         * @return this.
         * @ If the index is negative.
         */
        public JSONArray Put(int index, int value)
        {
            Put(index, value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put or replace a long value. If the index is greater than the length of
         *  the JSONArray, then null elements will be added as necessary to pad
         *  it out.
         * @param index The subscript.
         * @param value A long value.
         * @return this.
         * @ If the index is negative.
         */
        public JSONArray Put(int index, long value)
        {
            Put(index, value);
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put a value in the JSONArray, where the value will be a
         * JSONObject which is produced from a Map.
         * @param index The subscript.
         * @param value	The Map value.
         * @return		this.
         * @ If the index is negative or if the the value is
         *  an invalid number.
         */
        public JSONArray Put(int index, Hashtable value)
        {
            Put(index, new JSONObject(value));
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Put or replace an object value in the JSONArray. If the index is greater
         *  than the length of the JSONArray, then null elements will be added as
         *  necessary to pad it out.
         * @param index The subscript.
         * @param value The value to put into the array. The value should be a
         *  Boolean, Double, Integer, JSONArray, JSONObject, Long, or string, or the
         *  JSONObject.NULL object.
         * @return this.
         * @ If the index is negative or if the the value is
         *  an invalid number.
         */
        public JSONArray Put(int index, object value)
        {
            JSONObject.TestValidity(value);
            if (index < 0)
            {
                throw new JSONException("JSONArray[" + index + "] not found.");
            }
            if (index < Length())
            {
                _myArrayList[index]=value;
            }
            else
            {
                while (index != Length())
                {
                    Put(JSONObject.NULL);
                }
                Put(value);
            }
            return this;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Produce a JSONObject by combining a JSONArray of names with the values
         * of this JSONArray.
         * @param names A JSONArray containing a list of key strings. These will be
         * paired with the values.
         * @return A JSONObject, or null if there are no names or if this JSONArray
         * has no values.
         * @ If any of the names are null.
         */
        public JSONObject ToJSONObject(JSONArray names)
        {
            if (names == null || names.Length() == 0 || Length() == 0)
            {
                return null;
            }
            var jo = new JSONObject();
            for (int i = 0; i < names.Length(); i += 1)
            {
                jo.Put(names.GetString(i), Opt(i));
            }
            return jo;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a JSON text of this JSONArray. For compactness, no
         * unnecessary whitespace is added. If it is not possible to produce a
         * syntactically correct JSON text then null will be returned instead. This
         * could occur if the array contains an invalid number.
         * <p>
         * Warning: This method assumes that the data structure is acyclical.
         *
         * @return a printable, displayable, transmittable
         *  representation of the array.
         */
        public override string ToString()
        {
            try
            {
                return '[' + Join(",") + ']';
            }
            catch (Exception)
            {
                return null;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a prettyprinted JSON text of this JSONArray.
         * Warning: This method assumes that the data structure is acyclical.
         * @param indentFactor The number of spaces to add to each level of
         *  indentation.
         * @return a printable, displayable, transmittable
         *  representation of the object, beginning
         *  with <code>[</code>&nbsp;<small>(left bracket)</small> and ending
         *  with <code>]</code>&nbsp;<small>(right bracket)</small>.
         * @
         */
        public string ToString(int indentFactor)
        {
            return ToString(indentFactor, 0);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a prettyprinted JSON text of this JSONArray.
         * Warning: This method assumes that the data structure is acyclical.
         * @param indentFactor The number of spaces to add to each level of
         *  indentation.
         * @param indent The indention of the top level.
         * @return a printable, displayable, transmittable
         *  representation of the array.
         * @
         */
        internal string ToString(int indentFactor, int indent)
        {
            var len = Length();
            if (len == 0)
            {
                return "[]";
            }
            var sb = new StringBuilder("[");
            if (len == 1)
            {
                sb.Append(JSONObject.ValueToString(_myArrayList[0], indentFactor, indent));
            }
            else
            {
                int newindent = indent + indentFactor;
                sb.Append('\n');
                int i;
                for (i = 0; i < len; i += 1)
                {
                    if (i > 0)
                    {
                        sb.Append(",\n");
                    }
                    for (int j = 0; j < newindent; j += 1)
                    {
                        sb.Append(' ');
                    }
                    sb.Append(JSONObject.ValueToString(_myArrayList[i],
                            indentFactor, newindent));
                }
                sb.Append('\n');
                for (i = 0; i < indent; i += 1)
                {
                    sb.Append(' ');
                }
            }
            sb.Append(']');
            return sb.ToString();
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Write the contents of the JSONArray as JSON text to a writer.
         * For compactness, no whitespace is added.
         * <p>
         * Warning: This method assumes that the data structure is acyclical.
         *
         * @return The writer.
         * @
         */
        public StringWriter Write(StringWriter writer)
        {
            try
            {
                var b = false;
                var len = Length();

                writer.Write('[');

                for (var i = 0; i < len; i += 1)
                {
                    if (b)
                    {
                        writer.Write(',');
                    }
                    var v = _myArrayList[i];
                    if (v is JSONObject)
                    {
                        ((JSONObject)v).Write(writer);
                    }
                    else if (v is JSONArray)
                    {
                        ((JSONArray)v).Write(writer);
                    }
                    else
                    {
                        writer.Write(JSONObject.ValueToString(v));
                    }
                    b = true;
                }
                writer.Write(']');
                return writer;
            }
            catch (IOException e)
            {
                throw new JSONException(e.Message);
            }
        }

        internal static JSONObject Apply(JSONArray start, ArrayList tokens,
                 int firstToken)
        {

            if (start == null)
            {
                return null;
            }

            var nTokens = tokens.Count;
            for (var i = firstToken; i < nTokens; )
            {
                var tok1 = (string)tokens[i];
                var t1 = tok1[0];
                switch (t1)
                {
                    case SEPARATOR:
                        throw new JSONException("Syntax error: must start with an "
                                + "array: " + tok1);

                    case ARRAY_START:
                        if (i + 1 >= nTokens)
                        {
                            throw new JSONException("Syntax error: array must be " +
                                    "followed by a dimension: " + tok1);
                        }
                        var tok2 = (string)tokens[i + 1];
                        int dim ;
                        try
                        {
                            dim = int.Parse(tok2);
                        }
                        catch (FormatException)
                        {
                            throw new JSONException("Syntax error: illegal " +
                                    "array dimension: " + tok2);
                        }
                        if (i + 2 >= nTokens)
                        {
                            throw new JSONException("Syntax error: array " +
                                    "dimension must be closed: " + tok2);
                        }
                        var tok3 = (string)tokens[i + 2];
                        if (tok3.Length != 1 && tok3[0] != ARRAY_END)
                        {
                            throw new JSONException("Syntax error: illegal " +
                                    "close of array dimension: " + tok3);
                        }
                        if (i + 3 >= nTokens)
                        {
                            throw new JSONException("Syntax error: array close " +
                                    "must be followed by a separator or " +
                                    "array open: " + tok3);
                        }
                        var tok4 = (string)tokens[i + 3];
                        if (tok4.Length != 1 && tok4[0]
                            != SEPARATOR && tok4[0] != ARRAY_START)
                        {
                            throw new JSONException("Syntax error: illegal " +
                                    "separator after array: " + tok4);
                        }
                        i += 4;
                        if (tok4[0] == SEPARATOR)
                        {
                            return JSONObject.Apply(start.OptJSONObject(dim), tokens, i);
                        }
                        if (tok4[0] == ARRAY_START)
                        {
                            return Apply(start.OptJSONArray(dim), tokens, i);
                        }
                        throw new JSONException("Syntax error: illegal" +
                                " token after array: " + tok4);

                    default:
                        throw new JSONException("Syntax error: unknown" +
                                " delimiter: " + tok1);
                }
            }

            return null;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        private JSONArray GetAsArray(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            if (tokens.Count==0)
            {
                return this;
            }
            var obj = Apply(this, tokens, 0);
            return obj == null ? null : obj.OptJSONArray((string)tokens[tokens.Count-1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override bool GetAsBoolean(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            var obj = Apply(this, tokens, 0);
            return obj == null ? false : obj.OptBoolean((string)tokens[tokens.Count-1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int GetAsInteger(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            var obj = Apply(this, tokens, 0);
            return obj == null ? 0 : obj.OptInt((string)tokens[tokens.Count - 1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override long GetAsLong(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            var obj = Apply(this, tokens, 0);
            return obj == null ? 0 : obj.OptLong((string)tokens[tokens.Count - 1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override double GetAsDouble(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            var obj = Apply(this, tokens, 0);
            return obj == null ? 0 : obj.OptDouble((string)tokens[tokens.Count - 1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override string GetAsString(string path)
        {
            var tokens = new JSONPathTokenizer(path).Tokenize();
            var obj = Apply(this, tokens, 0);
            return obj == null ? null : obj.OptString((string)tokens[tokens.Count - 1]);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int GetSizeOfArray(string path)
        {
            var array = GetAsArray(path);
            return array == null ? 0 : array.Length();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override string[] GetAsStringArray(string path)
        {
            var jarr = GetAsArray(path);
            var arr = new string[jarr == null ? 0 : jarr.Length()];
            for (var i = 0; i < arr.Length; i++)
            {
                if (jarr != null) arr[i] = jarr.Opt(i) as string;
            }
            return arr;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         */
        public override int[] GetAsIntegerArray(string path)
        {
            var jarr = GetAsArray(path);
            var arr = new int[jarr == null ? 0 : jarr.Length()];
            for (var i = 0; i < arr.Length; i++)
            {
                if (jarr != null) arr[i] = ((int)jarr.Opt(i));
            }
            return arr;
        }


        /**
         * The ArrayList where the JSONArray's properties are kept.
         */
        private readonly ArrayList _myArrayList;


    }
}
