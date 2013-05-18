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
     * The JSONXMLTokener extends the JSONTokener to provide additional methods
     * for the parsing of JSONXML texts.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 07/06/09
     * @author      Guidebee, Inc. & JSON.org
     */
    internal class JSONXMLTokener : JSONTokener
    {

        /** 
         * The table of entity values. It initially contains Character values for
         * amp, apos, gt, lt, quot.
         */
        public static Hashtable Entity;

        static JSONXMLTokener()
        {
            Entity = new Hashtable(8)
                         {
                             {"amp", JSONXML.AMP},
                             {"apos", JSONXML.APOS},
                             {"gt", JSONXML.GT},
                             {"lt", JSONXML.LT},
                             {"quot", JSONXML.QUOT}
                         };
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Construct an JSONXMLTokener from a string.
         * @param s A source string.
         */
        public JSONXMLTokener(string s)
            : base(s)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the text in the CDATA block.
         * @return The string up to the <code>]]&gt;</code>.
         * @ If the <code>]]&gt;</code> is not found.
         */
        public string NextCDATA()
        {
            var sb = new StringBuilder();
            for (; ; )
            {
                var c = Next();
                if (c == 0)
                {
                    throw SyntaxError("Unclosed CDATA.");
                }
                sb.Append(c);
                var i = sb.Length - 3;
                if (i >= 0 && sb[i] == ']' &&
                              sb[i + 1] == ']' && sb[i + 2] == '>')
                {
                    sb.Length = i;
                    return sb.ToString();
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the next JSONXML outer token, trimming whitespace. There are two kinds
         * of tokens: the '<' character which begins a markup tag, and the content
         * text between markup tags.
         *
         * @return  A string, or a '<' Character, or null if there is no more
         * source text.
         * @
         */
        public object NextContent()
        {
            char c;
            StringBuilder sb;
            do
            {
                c = Next();
            } while (IsWhitespace(c));
            if (c == 0)
            {
                return null;
            }
            if (c == '<')
            {
                return JSONXML.LT;
            }
            sb = new StringBuilder();
            for (; ; )
            {
                if (c == '<' || c == 0)
                {
                    Back();
                    return sb.ToString().Trim();
                }
                if (c == '&')
                {
                    sb.Append(NextEntity(c));
                }
                else
                {
                    sb.Append(c);
                }
                c = Next();
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Return the next entity. These entities are translated to Characters:
         *     <code>&amp;  &apos;  &gt;  &lt;  &quot;</code>.
         * @param a An ampersand character.
         * @return  A Character or an entity string if the entity is not recognized.
         * @ If missing ';' in JSONXML entity.
         */
        public object NextEntity(char a)
        {
            var sb = new StringBuilder();
            for (; ; )
            {
                var c = Next();
                if (IsLetterOrDigit(c) || c == '#')
                {
                    sb.Append(char.ToLower(c));
                }
                else if (c == ';')
                {
                    break;
                }
                else
                {
                    throw SyntaxError("Missing ';' in JSONXML entity: &" + sb);
                }
            }
            var s = sb.ToString();
            var e = Entity[s];
            return e != null ? e : a + s + ";";
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the next JSONXML meta token. This is used for skipping over <!...>
         * and <?...?> structures.
         * @return Syntax characters (<code>< > / = ! ?</code>) are returned as
         *  Character, and strings and names are returned as Boolean. We don't care
         *  what the values actually are.
         * @ If a string is not properly closed or if the JSONXML
         *  is badly structured.
         */
        public object NextMeta()
        {
            char c;
            do
            {
                c = Next();
            } while (IsWhitespace(c));
            switch (c)
            {
                case '\0':
                    throw SyntaxError("Misshaped meta tag.");
                case '<':
                    return JSONXML.LT;
                case '>':
                    return JSONXML.GT;
                case '/':
                    return JSONXML.SLASH;
                case '=':
                    return JSONXML.EQ;
                case '!':
                    return JSONXML.BANG;
                case '?':
                    return JSONXML.QUEST;
                case '"':
                case '\'':
                    var q = c;
                    for (; ; )
                    {
                        c = Next();
                        if (c == 0)
                        {
                            throw SyntaxError("Unterminated string.");
                        }
                        if (c == q)
                        {
                            return true;
                        }
                    }
                default:
                    for (; ; )
                    {
                        c = Next();
                        if (IsWhitespace(c))
                        {
                            return true;
                        }
                        switch (c)
                        {
                            case '\0':
                            case '<':
                            case '>':
                            case '/':
                            case '=':
                            case '!':
                            case '?':
                            case '"':
                            case '\'':
                                Back();

                                return true;
                        }
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
         * Get the next JSONXML Token. These tokens are found inside of angle
         * brackets. It may be one of these characters: <code>/ > = ! ?</code> or it
         * may be a string wrapped in single quotes or double quotes, or it may be a
         * name.
         * @return a string or a Character.
         * @ If the JSONXML is not well formed.
         */
        public object NextToken()
        {
            char c;
            StringBuilder sb;
            do
            {
                c = Next();
            } while (IsWhitespace(c));
            switch (c)
            {
                case '\0':
                    throw SyntaxError("Misshaped element.");
                case '<':
                    throw SyntaxError("Misplaced '<'.");
                case '>':
                    return JSONXML.GT;
                case '/':
                    return JSONXML.SLASH;
                case '=':
                    return JSONXML.EQ;
                case '!':
                    return JSONXML.BANG;
                case '?':
                    return JSONXML.QUEST;

                // Quoted string

                case '"':
                case '\'':
                    var q = c;
                    sb = new StringBuilder();
                    for (; ; )
                    {
                        c = Next();
                        if (c == 0)
                        {
                            throw SyntaxError("Unterminated string.");
                        }
                        if (c == q)
                        {
                            return sb.ToString();
                        }
                        if (c == '&')
                        {
                            sb.Append(NextEntity(c));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                default:

                    // Name

                    sb = new StringBuilder();
                    for (; ; )
                    {
                        sb.Append(c);
                        c = Next();
                        if (IsWhitespace(c))
                        {
                            return sb.ToString();
                        }
                        switch (c)
                        {
                            case '\0':
                            case '>':
                            case '/':
                            case '=':
                            case '!':
                            case '?':
                            case '[':
                            case ']':
                                Back();
                                return sb.ToString();
                            case '<':
                            case '"':
                            case '\'':
                                throw SyntaxError("Bad character in a name.");
                        }
                    }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private static bool IsWhitespace(char c)
        {
            switch (c)
            {
                case ' ':
                case '\r':
                case '\n':
                case '\t':
                    return true;
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private static bool IsLetterOrDigit(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':

                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':

                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    return true;
            }
            return false;
        }

    }
}
