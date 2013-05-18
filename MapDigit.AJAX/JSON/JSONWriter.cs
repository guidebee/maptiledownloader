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
using System.IO;

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
     * JSONWriter provides a quick and convenient way of producing JSON text.
     * The texts produced strictly conform to JSON syntax rules. No whitespace is
     * added, so the results are ready for transmission or storage. Each instance of
     * JSONWriter can produce one JSON text.
     * <p>
     * A JSONWriter instance provides a <code>value</code> method for appending
     * values to the
     * text, and a <code>key</code>
     * method for adding keys before values in objects. There are <code>array</code>
     * and <code>endArray</code> methods that make and bound array values, and
     * <code>object</code> and <code>endObject</code> methods which make and bound
     * object values. All of these methods return the JSONWriter instance,
     * permitting a cascade style. For example, <pre>
     * new JSONWriter(myWriter)
     *     .object()
     *         .key("JSON")
     *         .value("Hello, World!")
     *     .endObject();</pre> which writes <pre>
     * {"JSON":"Hello, World!"}</pre>
     * <p>
     * The first method called must be <code>array</code> or <code>object</code>.
     * There are no methods for adding commas or colons. JSONWriter adds them for
     * you. Objects and arrays can be nested up to 20 levels deep.
     * <p>
     * This can sometimes be easier than using a JSONObject to build a string.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 07/06/09
     * @author      Guidebee, Inc. & JSON.org
     */
    public class JSONWriter
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a fresh JSONWriter. It can be used to build one JSON text.
         */
        public JSONWriter(StringWriter w)
        {
            _comma = false;
            _mode = 'i';
            _stack = new char[MAXDEPTH];
            _top = 0;
            _writer = w;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a value.
         * @param s A string value.
         * @return this
         * @throws JSONException If the value is out of sequence.
         */
        private JSONWriter Append(String s)
        {
            if (s == null)
            {
                throw new JSONException("Null pointer");
            }
            if (_mode == 'o' || _mode == 'a')
            {
                try
                {
                    if (_comma && _mode == 'a')
                    {
                        _writer.Write(',');
                    }
                    _writer.Write(s);
                }
                catch (IOException e)
                {
                    throw new JSONException(e.Message);
                }
                if (_mode == 'o')
                {
                    _mode = 'k';
                }
                _comma = true;
                return this;
            }
            throw new JSONException("Value out of sequence.");
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Begin appending a new array. All values until the balancing
         * <code>endArray</code> will be appended to this array. The
         * <code>endArray</code> method must be called to mark the array's end.
         * @return this
         * @throws JSONException If the nesting is too deep, or if the object is
         * started in the wrong place (for example as a key or after the end of the
         * outermost array or object).
         */
        public JSONWriter Array()
        {
            if (_mode == 'i' || _mode == 'o' || _mode == 'a')
            {
                Push('a');
                Append("[");
                _comma = false;
                return this;
            }
            throw new JSONException("Misplaced array.");
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * End something.
         * @param m Mode
         * @param c Closing character
         * @return this
         * @throws JSONException If unbalanced.
         */
        private JSONWriter End(char m, char c)
        {
            if (_mode != m)
            {
                throw new JSONException(m == 'o' ? "Misplaced endObject." :
                    "Misplaced endArray.");
            }
            Pop(m);
            try
            {
                _writer.Write(c);
            }
            catch (IOException e)
            {
                throw new JSONException(e.Message);
            }
            _comma = true;
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * End an array. This method most be called to balance calls to
         * <code>array</code>.
         * @return this
         * @throws JSONException If incorrectly nested.
         */
        public JSONWriter EndArray()
        {
            return End('a', ']');
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * End an object. This method most be called to balance calls to
         * <code>object</code>.
         * @return this
         * @throws JSONException If incorrectly nested.
         */
        public JSONWriter EndObject()
        {
            return End('k', '}');
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a key. The key will be associated with the next value. In an
         * object, every value must be preceded by a key.
         * @param s A key string.
         * @return this
         * @throws JSONException If the key is out of place. For example, keys
         *  do not belong in arrays or if the key is null.
         */
        public JSONWriter Key(String s)
        {
            if (s == null)
            {
                throw new JSONException("Null key.");
            }
            if (_mode == 'k')
            {
                try
                {
                    if (_comma)
                    {
                        _writer.Write(',');
                    }
                    _writer.Write(JSONObject.Quote(s));
                    _writer.Write(':');
                    _comma = false;
                    _mode = 'o';
                    return this;
                }
                catch (IOException e)
                {
                    throw new JSONException(e.Message);
                }
            }
            throw new JSONException("Misplaced key.");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Begin appending a new object. All keys and values until the balancing
         * <code>endObject</code> will be appended to this object. The
         * <code>endObject</code> method must be called to mark the object's end.
         * @return this
         * @throws JSONException If the nesting is too deep, or if the object is
         * started in the wrong place (for example as a key or after the end of the
         * outermost array or object).
         */
        public JSONWriter Object()
        {
            if (_mode == 'i')
            {
                _mode = 'o';
            }
            if (_mode == 'o' || _mode == 'a')
            {
                Append("{");
                Push('k');
                _comma = false;
                return this;
            }
            throw new JSONException("Misplaced object.");

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Pop an array or object scope.
         * @param c The scope to close.
         * @throws JSONException If nesting is wrong.
         */
        private void Pop(char c)
        {
            if (_top <= 0 || _stack[_top - 1] != c)
            {
                throw new JSONException("Nesting error.");
            }
            _top -= 1;
            _mode = _top == 0 ? 'd' : _stack[_top - 1];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Push an array or object scope.
         * @param c The scope to open.
         * @throws JSONException If nesting is too deep.
         */
        private void Push(char c)
        {
            if (_top >= MAXDEPTH)
            {
                throw new JSONException("Nesting too deep.");
            }
            _stack[_top] = c;
            _mode = c;
            _top += 1;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append either the value <code>true</code> or the value
         * <code>false</code>.
         * @param b A boolean.
         * @return this
         * @throws JSONException
         */
        public JSONWriter Value(bool b)
        {
            return Append(b ? "true" : "false");
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a double value.
         * @param d A double.
         * @return this
         * @throws JSONException If the number is not finite.
         */
        public JSONWriter Value(double d)
        {
            return Value(d.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append a long value.
         * @param l A long.
         * @return this
         * @throws JSONException
         */
        public JSONWriter Value(long l)
        {
            return Append(l.ToString());
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Append an object value.
         * @param o The object to append. It can be null, or a Boolean, Number,
         *   String, JSONObject, or JSONArray, or an object with a toJSONString()
         *   method.
         * @return this
         * @throws JSONException If the value is out of sequence.
         */
        public JSONWriter Value(object o)
        {
            return Append(JSONObject.ValueToString(o));
        }


        private const int MAXDEPTH = 20;

        /**
         * The comma flag determines if a comma should be output before the next
         * value.
         */
        private bool _comma;

        /**
         * The current mode. Values:
         * 'a' (array),
         * 'd' (done),
         * 'i' (initial),
         * 'k' (key),
         * 'o' (object).
         */
        protected char _mode;

        /**
         * The object/array stack.
         */
        private readonly char[] _stack;

        /**
         * The stack top index. A value of 0 indicates that the stack is empty.
         */
        private int _top;

        /**
         * The writer that will receive the output.
         */
        protected StringWriter _writer;

    }
}
