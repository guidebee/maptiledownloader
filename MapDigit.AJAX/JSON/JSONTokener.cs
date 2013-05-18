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
using System.Globalization;
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
     * A JSONTokener takes a source string and extracts characters and tokens from
     * it. It is used by the JSONObject and JSONArray constructors to parse
     * JSON source strings.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 07/06/09
     * @author      Guidebee, Inc. & JSON.org
     */
    internal class JSONTokener
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct a JSONTokener from a string.
         *
         * @param s     A source string.
         */
        public JSONTokener(string s)
        {
            _myIndex = 0;
            _mySource = s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Back up one character. This provides a sort of lookahead capability,
         * so that you can test for a digit or letter before attempting to parse
         * the next number or identifier.
         */
        public void Back()
        {
            if (_myIndex > 0)
            {
                _myIndex -= 1;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the hex value of a character (base16).
         * @param c A character between '0' and '9' or between 'A' and 'F' or
         * between 'a' and 'f'.
         * @return  An int between 0 and 15, or -1 if c was not a hex digit.
         */
        public static int Dehexchar(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            if (c >= 'A' && c <= 'F')
            {
                return c - ('A' - 10);
            }
            if (c >= 'a' && c <= 'f')
            {
                return c - ('a' - 10);
            }
            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determine if the source string still contains characters that next()
         * can consume.
         * @return true if not yet at the end of the source.
         */
        public bool More()
        {
            return _myIndex < _mySource.Length;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next character in the source string.
         *
         * @return The next character, or 0 if past the end of the source string.
         */
        public char Next()
        {
            if (More())
            {
                char c = _mySource[_myIndex];
                _myIndex += 1;
                return c;
            }
            return (char)0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Consume the next character, and check that it matches a specified
         * character.
         * @param c The character to match.
         * @return The character.
         * @throws JSONException if the character does not match.
         */
        public char Next(char c)
        {
            char n = Next();
            if (n != c)
            {
                throw SyntaxError("Expected '" + c + "' and instead saw '" +
                    n + "'.");
            }
            return n;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next n characters.
         *
         * @param n     The number of characters to take.
         * @return      A string of n characters.
         * @throws JSONException
         *   Substring bounds error if there are not
         *   n characters remaining in the source string.
         */
        public string Next(int n)
        {
            int i = _myIndex;
            int j = i + n;
            if (j >= _mySource.Length)
            {
                throw SyntaxError("Substring bounds error");
            }
            _myIndex += n;
            return _mySource.Substring(i, n);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next char in the string, skipping whitespace
         * and comments (slashslash, slashstar, and hash).
         * @throws JSONException
         * @return  A character, or 0 if there are no more characters.
         */
        public char NextClean()
        {
            for (; ; )
            {
                char c = Next();
                if (c == '/')
                {
                    switch (Next())
                    {
                        case '/':
                            do
                            {
                                c = Next();
                            } while (c != '\n' && c != '\r' && c != 0);
                            break;
                        case '*':
                            for (; ; )
                            {
                                c = Next();
                                if (c == 0)
                                {
                                    throw SyntaxError("Unclosed comment.");
                                }
                                if (c == '*')
                                {
                                    if (Next() == '/')
                                    {
                                        break;
                                    }
                                    Back();
                                }
                            }
                            break;
                        default:
                            Back();
                            return '/';
                    }
                }
                else if (c == '#')
                {
                    do
                    {
                        c = Next();
                    } while (c != '\n' && c != '\r' && c != 0);
                }
                else if (c == 0 || c > ' ')
                {
                    return c;
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
         * Return the characters up to the next close quote character.
         * Backslash processing is done. The formal JSON format does not
         * allow strings in single quotes, but an implementation is allowed to
         * accept them.
         * @param quote The quoting character, either
         *      <code>"</code>&nbsp;<small>(double quote)</small> or
         *      <code>'</code>&nbsp;<small>(single quote)</small>.
         * @return      A string.
         * @throws JSONException Unterminated string.
         */
        public string NextString(char quote)
        {
            var sb = new StringBuilder();
            for (; ; )
            {
                char c = Next();
                switch (c)
                {
                    case '\0':
                    case '\n':
                    case '\r':
                        throw SyntaxError("Unterminated string");
                    case '\\':
                        c = Next();
                        switch (c)
                        {
                            case 'b':
                                sb.Append('\b');
                                break;
                            case 't':
                                sb.Append('\t');
                                break;
                            case 'n':
                                sb.Append('\n');
                                break;
                            case 'f':
                                sb.Append('\f');
                                break;
                            case 'r':
                                sb.Append('\r');
                                break;
                            case 'u':
                                sb.Append((char)int.Parse(Next(4), NumberStyles.HexNumber));
                                break;
                            case 'x':
                                sb.Append((char)int.Parse(Next(2), NumberStyles.HexNumber));
                                break;
                            default:
                                sb.Append(c);
                                break;
                        }
                        break;
                    default:
                        if (c == quote)
                        {
                            return sb.ToString();
                        }
                        sb.Append(c);
                        break;
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
         * Get the text up but not including the specified character or the
         * end of line, whichever comes first.
         * @param  d A delimiter character.
         * @return   A string.
         */
        public string NextTo(char d)
        {
            var sb = new StringBuilder();
            for (; ; )
            {
                var c = Next();
                if (c == d || c == 0 || c == '\n' || c == '\r')
                {
                    if (c != 0)
                    {
                        Back();
                    }
                    return sb.ToString().Trim();
                }
                sb.Append(c);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the text up but not including one of the specified delimeter
         * characters or the end of line, whichever comes first.
         * @param delimiters A set of delimiter characters.
         * @return A string, trimmed.
         */
        public string NextTo(string delimiters)
        {
            var sb = new StringBuilder();
            for (; ; )
            {
                var c = Next();
                if (delimiters.IndexOf(c) >= 0 || c == 0 ||
                    c == '\n' || c == '\r')
                {
                    if (c != 0)
                    {
                        Back();
                    }
                    return sb.ToString().Trim();
                }
                sb.Append(c);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next value. The value can be a Boolean, Double, Integer,
         * JSONArray, JSONObject, Long, or string, or the JSONObject.NULL object.
         * @throws JSONException If syntax error.
         *
         * @return An object.
         */
        public object NextValue()
        {
            var c = NextClean();

            switch (c)
            {
                case '"':
                case '\'':
                    return NextString(c);
                case '{':
                    Back();
                    return new JSONObject(this);
                case '[':
                    Back();
                    return new JSONArray(this);
            }

            /*
             * Handle unquoted text. This could be the values true, false, or
             * null, or it can be a number. An implementation (such as this one)
             * is allowed to also accept non-standard forms.
             *
             * Accumulate characters until we reach the end of the text or a
             * formatting character.
             */

            var sb = new StringBuilder();
            var b = c;
            while (c >= ' ' && ",:]}/\\\"[{;=#".IndexOf(c) < 0)
            {
                sb.Append(c);
                c = Next();
            }
            Back();

            /*
             * If it is true, false, or null, return the proper value.
             */

            var s = sb.ToString().Trim();
            if (s.Equals(""))
            {
                throw SyntaxError("Missing value.");
            }
            if (s.ToLower().Equals("true"))
            {
                return true;
            }
            if (s.ToLower().Equals("false"))
            {
                return false;
            }
            if (s.ToLower().Equals("null"))
            {
                return JSONObject.NULL;
            }

            /*
             * If it might be a number, try converting it. We support the 0- and 0x-
             * conventions. If a number cannot be produced, then the value will just
             * be a string. note that the 0-, 0x-, plus, and implied string
             * conventions are non-standard. A JSON parser is free to accept
             * non-JSON forms as long as it accepts all correct JSON forms.
             */

            if ((b >= '0' && b <= '9') || b == '.' || b == '-' || b == '+')
            {
                if (b == '0')
                {
                    if (s.Length > 2 &&
                        (s[1] == 'x' || s[1] == 'X'))
                    {
                        try
                        {
                            return int.Parse(s.Substring(2), NumberStyles.HexNumber);
                        }
                        catch (Exception)
                        {
                            /* Ignore the error */
                        }
                    }
                    else
                    {
                        try
                        {
                            return int.Parse(s, NumberStyles.HexNumber);
                        }
                        catch (Exception )
                        {
                            
                        }
                    }
                }
                try
                {
                    return int.Parse(s);
                }
                catch (Exception)
                {
                    try
                    {
                        return long.Parse(s);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            return double.Parse(s);
                        }
                        catch (Exception)
                        {
                            return s;
                        }
                    }
                }
            }
            return s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Skip characters until the next character is the requested character.
         * If the requested character is not found, no characters are skipped.
         * @param to A character to skip to.
         * @return The requested character, or zero if the requested character
         * is not found.
         */
        public char SkipTo(char to)
        {
            char c;
            var index = _myIndex;
            do
            {
                c = Next();
                if (c != 0) continue;
                _myIndex = index;
                return c;
            } while (c != to);
            Back();
            return c;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Skip characters until past the requested string.
         * If it is not found, we are left at the end of the source.
         * @param to A string to skip past.
         */
        public void SkipPast(string to)
        {
            _myIndex = _mySource.IndexOf(to, _myIndex);
            if (_myIndex < 0)
            {
                _myIndex = _mySource.Length;
            }
            else
            {
                _myIndex += to.Length;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a JSONException to signal a syntax error.
         *
         * @param message The error message.
         * @return  A JSONException object, suitable for throwing
         */
        public JSONException SyntaxError(string message)
        {
            return new JSONException(message + ToString());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Make a printable string of this JSONTokener.
         *
         * @return " at character [this.myIndex] of [this.mySource]"
         */
        public override string ToString()
        {
            return " at character " + _myIndex + " of " + _mySource;
        }


        /**
         * The index of the next character.
         */
        private int _myIndex;


        /**
         * The source string being tokenized.
         */
        private readonly string _mySource;

    }
}
