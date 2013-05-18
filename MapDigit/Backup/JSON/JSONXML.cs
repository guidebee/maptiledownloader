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
     * This provides static methods to Convert an JSONXML text into a JSONObject,
     * and to covert a JSONObject into an JSONXML text.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 07/06/09
     * @author      Guidebee, Inc. & JSON.org
     */
    internal class JSONXML
    {

        /** 
         * The Character '&'. 
         */
        public const char AMP = '&';

        /** 
         * The Character '''. 
         */
        public const char APOS = '\\';

        /** 
         * The Character '!'. 
         */
        public const char BANG = '!';

        /** 
         * The Character '='. 
         */
        public const char EQ = '=';

        /** 
         * The Character '>'. 
         */
        public const char GT = '>';

        /** 
         * The Character '<'. 
         */
        public const char LT = '<';

        /** 
         * The Character '?'. 
         */
        public const char QUEST = '?';

        /** 
         * The Character '"'. 
         */
        public const char QUOT = '"';

        /** 
         * The Character '/'. 
         */
        public const char SLASH = '/';

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Replace special characters with JSONXML escapes:
         * <pre>
         * &amp; <small>(ampersand)</small> is replaced by &amp;amp;
         * &lt; <small>(less than)</small> is replaced by &amp;lt;
         * &gt; <small>(greater than)</small> is replaced by &amp;gt;
         * &quot; <small>(double quote)</small> is replaced by &amp;quot;
         * </pre>
         * @param str The str to be escaped.
         * @return The escaped str.
         */
        public static string Escape(string str)
        {
            var sb = new StringBuilder();
            for (int i = 0, len = str.Length; i < len; i++)
            {
                var c = str[i];
                switch (c)
                {
                    case '&':
                        sb.Append("&amp;");
                        break;
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    default:
                        sb.Append(c);
                        break;

                }
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
         * Convert a well-formed (but not necessarily valid) JSONXML str into a
         * JSONObject. Some information may be lost in this transformation
         * because JSON is a data format and JSONXML is a document format. JSONXML uses
         * elements, attributes, and content text, while JSON uses unordered
         * collections of name/value pairs and arrays of values. JSON does not
         * does not like to distinguish between elements and attributes.
         * Sequences of similar elements are represented as JSONArrays. Content
         * text may be placed in a "content" member. Comments, prologs, DTDs, and
         * <code>&lt;[ [ ]]></code> are ignored.
         * @param str The source str.
         * @return A JSONObject containing the structured data from the JSONXML str.
         * @
         */
        public static JSONObject ToJSONObject(string str)
        {
            var o = new JSONObject();
            var x = new JSONXMLTokener(str);
            while (x.More())
            {
                x.SkipPast("<");
                Parse(x, o, null);
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
         * Convert a JSONObject into a well-formed, element-normal JSONXML str.
         * @param o A JSONObject.
         * @return  A str.
         * @throws  JSONException
         */
        public static string ToString(object o)
        {
            return ToString(o, null);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert a JSONObject into a well-formed, element-normal JSONXML str.
         * @param o A JSONObject.
         * @param tagName The optional name of the enclosing tag.
         * @return A str.
         * @
         */
        public static string ToString(object o, string tagName)
        {
            var b = new StringBuilder();
            int i;
            JSONArray ja;
            JSONObject jo;
            string k;
            int len;
            string s;
            object v;
            if (o is JSONObject)
            {

                // Emit <tagName>

                if (tagName != null)
                {
                    b.Append('<');
                    b.Append(tagName);
                    b.Append('>');
                }

                // Loop thru the keys.

                jo = (JSONObject)o;
                var keyset = jo.Keys();
                foreach (var c in keyset)
                {
                    k = c.ToString();
                    v = jo.Get(k);
                    if (v is string)
                    {
                    }

                    // Emit content in body

                    if (k.Equals("content"))
                    {
                        if (v is JSONArray)
                        {
                            ja = (JSONArray)v;
                            len = ja.Length();
                            for (i = 0; i < len; i += 1)
                            {
                                if (i > 0)
                                {
                                    b.Append('\n');
                                }
                                b.Append(Escape(ja.Get(i).ToString()));
                            }
                        }
                        else
                        {
                            b.Append(Escape(v.ToString()));
                        }

                        // Emit an array of similar keys

                    }
                    else if (v is JSONArray)
                    {
                        ja = (JSONArray)v;
                        len = ja.Length();
                        for (i = 0; i < len; i += 1)
                        {
                            b.Append(ToString(ja.Get(i), k));
                        }
                    }
                    else if (v.Equals(""))
                    {
                        b.Append('<');
                        b.Append(k);
                        b.Append("/>");

                        // Emit a new tag <k>

                    }
                    else
                    {
                        b.Append(ToString(v, k));
                    }
                }


                if (tagName != null)
                {

                    // Emit the </tagname> close tag

                    b.Append("</");
                    b.Append(tagName);
                    b.Append('>');
                }
                return b.ToString();

                // JSONXML does not have good support for arrays. If an array appears in a place
                // where JSONXML is lacking, synthesize an <array> element.

            }
            if (o is JSONArray)
            {
                ja = (JSONArray)o;
                len = ja.Length();
                for (i = 0; i < len; ++i)
                {
                    b.Append(ToString(
                        ja.Opt(i), (tagName == null) ? "array" : tagName));
                }
                return b.ToString();
            }
            s = (o == null) ? "null" : Escape(o.ToString());
            return (tagName == null) ? "\"" + s + "\"" :
                (s.Length == 0) ? "<" + tagName + "/>" :
                "<" + tagName + ">" + s + "</" + tagName + ">";
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 07JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Scan the content following the named tag, attaching it to the context.
         * @param x       The JSONXMLTokener containing the source str.
         * @param context The JSONObject that will include the new material.
         * @param name    The tag name.
         * @return true if the close tag is processed.
         * @
         */
        private static bool Parse(JSONXMLTokener x, JSONObject context,
                                     string name)
        {
            JSONObject o;
            string s;

            // Test for and skip past these forms:
            //      <!-- ... -->
            //      <!   ...   >
            //      <![  ... ]]>
            //      <?   ...  ?>
            // Report errors for these forms:
            //      <>
            //      <=
            //      <<

            var t = x.NextToken();

            // <!

            if (t == (object)BANG)
            {
                var c = x.Next();
                if (c == '-')
                {
                    if (x.Next() == '-')
                    {
                        x.SkipPast("-->");
                        return false;
                    }
                    x.Back();
                }
                else if (c == '[')
                {
                    t = x.NextToken();
                    if (t.Equals("CDATA"))
                    {
                        if (x.Next() == '[')
                        {
                            s = x.NextCDATA();
                            if (s.Length > 0)
                            {
                                context.Accumulate("content", s);
                            }
                            return false;
                        }
                    }
                    throw x.SyntaxError("Expected 'CDATA['");
                }
                var i = 1;
                do
                {
                    t = x.NextMeta();
                    if (t == null)
                    {
                        throw x.SyntaxError("Missing '>' after '<!'.");
                    }
                    if (t == (object)LT)
                    {
                        i += 1;
                    }
                    else if (t == (object)GT)
                    {
                        i -= 1;
                    }
                } while (i > 0);
                return false;
            }
            if (t == (object)QUEST)
            {

                // <?

                x.SkipPast("?>");
                return false;
            }
            if (t == (object)SLASH)
            {

                // Close tag </

                if (name == null || !x.NextToken().Equals(name))
                {
                    throw x.SyntaxError("Mismatched close tag");
                }
                if (x.NextToken() != (object)GT)
                {
                    throw x.SyntaxError("Misshaped close tag");
                }
                return true;

            }
            if (t is char)
            {
                throw x.SyntaxError("Misshaped tag");

                // Open tag <

            }
            string n = (string)t;
            t = null;
            o = new JSONObject();
            for (; ; )
            {
                if (t == null)
                {
                    t = x.NextToken();
                }

                // attribute = value

                if (t is string)
                {
                    s = (string)t;
                    t = x.NextToken();
                    if (t == (object)EQ)
                    {
                        t = x.NextToken();
                        if (!(t is string))
                        {
                            throw x.SyntaxError("Missing value");
                        }
                        o.Accumulate(s, t);
                        t = null;
                    }
                    else
                    {
                        o.Accumulate(s, "");
                    }

                    // Empty tag <.../>

                }
                else if (t == (object)SLASH)
                {
                    if (x.NextToken() != (object)GT)
                    {
                        throw x.SyntaxError("Misshaped tag");
                    }
                    context.Accumulate(n, o);
                    return false;

                    // Content, between <...> and </...>

                }
                else if (t == (object)GT)
                {
                    for (; ; )
                    {
                        t = x.NextContent();
                        if (t == null)
                        {
                            if (name != null)
                            {
                                throw x.SyntaxError("Unclosed tag " + name);
                            }
                            return false;
                        }
                        if (t is string)
                        {
                            s = (string)t;
                            if (s.Length > 0)
                            {
                                o.Accumulate("content", s);
                            }

                            // Nested element

                        }
                        else if (t == (object)LT)
                        {
                            if (Parse(x, o, n))
                            {
                                if (o.Length() == 0)
                                {
                                    context.Accumulate(n, "");
                                }
                                else if (o.Length() == 1 &&
                                         o.Opt("content") != null)
                                {
                                    context.Accumulate(n, o.Opt("content"));
                                }
                                else
                                {
                                    context.Accumulate(n, o);
                                }
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    throw x.SyntaxError("Misshaped tag");
                }
            }
        }


    }
}
