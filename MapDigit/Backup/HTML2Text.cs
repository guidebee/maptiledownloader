using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace MapDigit.Util
{
    public sealed class Html2Text
    {


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Remove double "\\" and change to one "\"
         * @param source the string need to change
         * @return the result string.
         */
        public static string RemoveDoubleBackSlash(string source)
        {
            StringBuilder result2 = new StringBuilder();
            StringGetter input = new StringGetter(source);

            try
            {
                int c = input.Read();

                while (c != -1) // Convert until EOF
                {
                    string text;
                    if (c == '\\')
                    {
                        c = input.Read();
                        if (c == '\\')
                        {
                            text = "\\";
                        }
                        else
                        {
                            text = "\\" + (char)c;
                        }


                    }
                    else
                    {
                        text = "" + (char)c;
                    }

                    StringBuilder s = result2;
                    s.Append(text);

                    c = input.Read();
                }
            }
            catch 
            {
                input.Close();

            }


            return result2.ToString().Trim();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert \\uxxxx to it's string format
         * @param source string to Convert.
         * @return result string.
         */
        public static string ConvertUTF8(string source)
        {

            StringBuilder result2 = new StringBuilder();
            StringGetter input = new StringGetter(source);

            try
            {
                int c = input.Read();

                while (c != -1) // Convert until EOF
                {
                    string text;
                    if (c == '\\')
                    {
                        c = input.Read();
                        switch ((char)c)
                        {
                            case 'u':
                                text = "";
                                for (int i = 0; i < 4; i++)
                                {
                                    text += ((char)input.Read()).ToString();
                                }
                                text = int.Parse(text, NumberStyles.HexNumber).ToString();
                                break;
                            case 'x':
                                text = "";
                                for (int i = 0; i < 2; i++)
                                {
                                    text += ((char)input.Read());
                                }
                                text = int.Parse(text, NumberStyles.HexNumber).ToString();
                                break;
                            default:
                                text = "\\" + (char)c;
                                break;

                        }

                    }
                    else
                    {
                        text = "" + (char)c;
                    }

                    StringBuilder s = result2;
                    s.Append(text);

                    c = input.Read();
                }
            }
            catch 
            {
                input.Close();

            }


            return result2.ToString().Trim();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert to utf8 string
         * @param b byte array
         * @return result string.
         */
        public static string Encodeutf8(byte[] b)
        {
            string result = "";
            for (int i = 0; i < b.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(b[i]);
                result +=
                       "%" + BitConverter.ToString(bytes).Substring(0, 2);
            }
            return result.ToLower();
        }
        //[------------------------------ PUBLIC METHODS --------------------------]
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 04JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert html to plain text
         * @param source HTML string.
         * @return plain text.
         */
        public string Convert(string source)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder result2 = new StringBuilder();
            StringGetter input = new StringGetter(source);

            try
            {
                int c = input.Read();

                while (c != -1) // Convert until EOF
                {
                    string text;
                    if (c == '<') // It's a tag!!
                    {
                        string currentTag = GetTag(input); // Get the rest of the tag
                        text = ConvertTag(currentTag);
                    }
                    else if (c == '&')
                    {
                        string specialchar = GetSpecial(input);
                        if (specialchar.Equals("lt;") || specialchar.Equals("#60"))
                        {
                            text = "<";
                        }
                        else if (specialchar.Equals("gt;") || specialchar.Equals("#62"))
                        {
                            text = ">";
                        }
                        else if (specialchar.Equals("amp;") || specialchar.Equals("#38"))
                        {
                            text = "&";
                        }
                        else if (specialchar.Equals("nbsp;"))
                        {
                            text = " ";
                        }
                        else if (specialchar.Equals("quot;") || specialchar.Equals("#34"))
                        {
                            text = "\"";
                        }
                        else if (specialchar.Equals("copy;") || specialchar.Equals("#169"))
                        {
                            text = "[Copyright]";
                        }
                        else if (specialchar.Equals("reg;") || specialchar.Equals("#174"))
                        {
                            text = "[Registered]";
                        }
                        else if (specialchar.Equals("trade;") || specialchar.Equals("#153"))
                        {
                            text = "[Trademark]";
                        }
                        else
                        {
                            text = "&" + specialchar;
                        }
                    }
                    else if (!_pre && IsWhitespace((char)c))
                    {
                        StringBuilder s = _inBody ? result : result2;
                        if (s.Length > 0 && IsWhitespace(s[s.Length - 1]))
                        {
                            text = "";
                        }
                        else
                        {
                            text = " ";
                        }
                    }
                    else
                    {
                        text = "" + (char)c;
                    }

                    StringBuilder s2 = _inBody ? result : result2;
                    s2.Append(text);

                    c = input.Read();
                }
            }
            catch 
            {
                input.Close();

            }

            StringBuilder s1 = _bodyFound ? result : result2;
            return s1.ToString().Trim();
        }


        private bool _bodyFound;
        private bool _inBody;
        private bool _pre;
        private string _href = "";

        private static string GetTag(StringGetter r)
        {
            StringBuilder result = new StringBuilder();
            int level = 1;

            result.Append('<');
            while (level > 0)
            {
                int c = r.Read();
                if (c == -1)
                {
                    break; // EOF
                } // EOF
                result.Append((char)c);
                if (c == '<')
                {
                    level++;
                }
                else if (c == '>')
                {
                    level--;
                }
            }

            return result.ToString();
        }

        private static string GetSpecial(StringGetter r)
        {
            StringBuilder result = new StringBuilder();
            r.Mark(1);//Mark the present position in the stream
            int c = r.Read();

            while (IsLetter((char)c))
            {
                result.Append((char)c);
                r.Mark(1);
                c = r.Read();
            }

            if (c == ';')
            {
                result.Append(';');
            }
            else
            {
                r.Reset();
            }

            return result.ToString();
        }



        private static bool IsTag(string s1, string s2)
        {
            s1 = s1.ToLower();
            string t1 = "<" + s2.ToLower() + ">";
            string t2 = "<" + s2.ToLower() + " ";

            return s1.StartsWith(t1) || s1.StartsWith(t2);
        }

        private string ConvertTag(string t)
        {
            string result = "";

            if (IsTag(t, "body"))
            {
                _inBody = true; _bodyFound = true;
            }
            else if (IsTag(t, "/body"))
            {
                _inBody = false; result = "";
            }
            else if (IsTag(t, "center"))
            {
                result = "";
            }
            else if (IsTag(t, "/center"))
            {
                result = "";
            }
            else if (IsTag(t, "_pre"))
            {
                result = ""; _pre = true;
            }
            else if (IsTag(t, "/_pre"))
            {
                result = ""; _pre = false;
            }
            else if (IsTag(t, "p"))
            {
                result = "";
            }
            else if (IsTag(t, "br"))
            {
                result = "";
            }
            else if (IsTag(t, "h1") || IsTag(t, "h2") || IsTag(t, "h3") || IsTag(t, "h4") || IsTag(t, "h5") || IsTag(t, "h6") || IsTag(t, "h7"))
            {
                result = "";
            }
            else if (IsTag(t, "/h1") || IsTag(t, "/h2") || IsTag(t, "/h3") || IsTag(t, "/h4") || IsTag(t, "/h5") || IsTag(t, "/h6") || IsTag(t, "/h7"))
            {
                result = "";
            }
            else if (IsTag(t, "/dl"))
            {
                result = "";
            }
            else if (IsTag(t, "dd"))
            {
                result = "  * ";
            }
            else if (IsTag(t, "dt"))
            {
                result = "      ";
            }
            else if (IsTag(t, "li"))
            {
                result = "  * ";
            }
            else if (IsTag(t, "/ul"))
            {
                result = "";
            }
            else if (IsTag(t, "/ol"))
            {
                result = "";
            }
            else if (IsTag(t, "hr"))
            {
                result = "_________________________________________";
            }
            else if (IsTag(t, "table"))
            {
                result = "";
            }
            else if (IsTag(t, "/table"))
            {
                result = "";
            }
            else if (IsTag(t, "form"))
            {
                result = "";
            }
            else if (IsTag(t, "/form"))
            {
                result = "";
            }
            else if (IsTag(t, "b"))
            {
                result = "";
            }
            else if (IsTag(t, "/b"))
            {
                result = "";
            }
            else if (IsTag(t, "i"))
            {
                result = "\"";
            }
            else if (IsTag(t, "/i"))
            {
                result = "\"";
            }
            else if (IsTag(t, "img"))
            {
                int idx = t.IndexOf("alt=\"");
                if (idx != -1)
                {
                    idx += 5;
                    int idx2 = t.IndexOf("\"", idx);
                    result = t.Substring(idx, idx2 - idx);
                }
            }
            else if (IsTag(t, "a"))
            {
                int idx = t.IndexOf("_href=\"");
                if (idx != -1)
                {
                    idx += 6;
                    int idx2 = t.IndexOf("\"", idx);
                    _href = t.Substring(idx, idx2 - idx);
                }
                else
                {
                    _href = "";
                }
            }
            else if (IsTag(t, "/a"))
            {
                if (_href.Length > 0)
                {
                    result = " [ " + _href + " ]";
                    _href = "";
                }
            }

            return result;
        }

        private static bool IsWhitespace(char ch)
        {
            if (ch == ' ' || ch == '\t' || ch == '\n')
            {
                return true;
            }
            return false;

        }

        private static bool IsLetter(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
            {
                return true;
            }
            return false;
        }


    }


    class StringGetter
    {

        private string _str;
        private readonly int _length;
        private int _next;
        private int _mark;
        private readonly object _stringLock = new object();

        /**
         * Creates a new string reader.
         *
         * @param s  string providing the character stream.
         */
        public StringGetter(string s)
        {
            _str = s;
            _length = s.Length;
        }

        /** Check to make sure that the stream has not been closed */
        private void EnsureOpen()
        {
            if (_str == null)
            {
                throw new IOException("Stream closed");
            }
        }

        /**
         * Reads a single character.
         *
         * @return     The character Read, or -1 if the end of the stream has been
         *             reached
         *
         * @exception  IOException  If an I/O error occurs
         */
        public int Read()
        {
            lock (_stringLock)
            {
                EnsureOpen();
                if (_next >= _length)
                {
                    return -1;
                }
                return _str[_next++];
            }
        }

        /**
         * Reads characters into a portion of an array.
         *
         * @param      cbuf  Destination buffer
         * @param      off   Offset at which to start writing characters
         * @param      len   Maximum number of characters to Read
         *
         * @return     The number of characters Read, or -1 if the end of the
         *             stream has been reached
         *
         * @exception  IOException  If an I/O error occurs
         */
        public int Read(char[] cbuf, int off, int len)
        {
            lock (_stringLock)
            {
                EnsureOpen();
                if ((off < 0) || (off > cbuf.Length) || (len < 0) ||
                        ((off + len) > cbuf.Length) || ((off + len) < 0))
                {
                    throw new IndexOutOfRangeException();
                }
                if (len == 0)
                {
                    return 0;
                }
                if (_next >= _length)
                {
                    return -1;
                }
                int n = Math.Min(_length - _next, len);
                char[] temp = _str.ToCharArray(_next, n);
                Array.Copy(temp, 0, cbuf, off, temp.Length);
                _next += n;
                return n;
            }
        }

        /**
         * Skips the specified number of characters in the stream. Returns
         * the number of characters that were skipped.
         *
         * <p>The <code>ns</code> parameter may be negative, even though the
         * <code>Skip</code> method of the {@link Reader} superclass throws
         * an exception in this case. Negative values of <code>ns</code> cause the
         * stream to Skip backwards. Negative return values indicate a Skip
         * backwards. It is not possible to Skip backwards past the beginning of
         * the string.
         *
         * <p>If the entire string has been Read or skipped, then this method has
         * no effect and always returns 0.
         *
         * @exception  IOException  If an I/O error occurs
         */
        public long Skip(long ns)
        {
            lock (_stringLock)
            {
                EnsureOpen();
                if (_next >= _length)
                {
                    return 0;
                }
                // Bound Skip by beginning and end of the source
                long n = Math.Min(_length - _next, ns);
                n = Math.Max(-_next, n);
                _next += (int)n;
                return n;
            }
        }

        /**
         * Tells whether this stream is Ready to be Read.
         *
         * @return True if the _next Read() is guaranteed not to block for input
         *
         * @exception  IOException  If the stream is closed
         */
        public bool Ready()
        {
            lock (_stringLock)
            {
                EnsureOpen();
                return true;
            }
        }

        /**
         * Tells whether this stream supports the Mark() operation, which it does.
         */
        public bool MarkSupported()
        {
            return true;
        }

        /**
         * Marks the present position in the stream.  Subsequent calls to Reset()
         * will reposition the stream to this point.
         *
         * @param  readAheadLimit  Limit on the number of characters that may be
         *                         Read while still preserving the Mark.  Because
         *                         the stream's input comes from a string, there
         *                         is no actual limit, so this argument must not
         *                         be negative, but is otherwise ignored.
         *
         * @exception  IllegalArgumentException  If readAheadLimit is < 0
         * @exception  IOException  If an I/O error occurs
         */
        public void Mark(int readAheadLimit)
        {
            if (readAheadLimit < 0)
            {
                throw new ArgumentException("Read-ahead limit < 0");
            }
            lock (_stringLock)
            {
                EnsureOpen();
                _mark = _next;
            }
        }

        /**
         * Resets the stream to the most recent Mark, or to the beginning of the
         * string if it has never been marked.
         *
         * @exception  IOException  If an I/O error occurs
         */
        public void Reset()
        {
            lock (_stringLock)
            {
                EnsureOpen();
                _next = _mark;
            }
        }

        /**
         * Closes the stream and releases any system resources associated with
         * it. Once the stream has been closed, further Read(),
         * Ready(), Mark(), or Reset() invocations will throw an IOException.
         * Closing a previously closed stream has no effect.
         */
        public void Close()
        {
            _str = null;
        }
    }


}


