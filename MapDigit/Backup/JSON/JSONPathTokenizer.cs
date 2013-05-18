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
    class JSONPathTokenizer
    {

        private readonly String _expression;
        private readonly int _length;

        private int _pos;

        internal JSONPathTokenizer(String expr)
        {
            if (expr == null)
            {
                throw new ArgumentException("path cannot be null");
            }

            _expression = expr;
            _length = _expression.Length;
            _pos = 0;
        }

        internal  ArrayList Tokenize()
        {
            var tokens = new ArrayList();
            String tok;
            for (_pos = 0, tok = Next(); !"".Equals(tok); tok = Next())
            {
                tokens.Add(tok);
            }
            return tokens;
        }

        private String Next()
        {
            var sbuf = new StringBuilder();

            if (_pos >= _length)
            {
                return sbuf.ToString();
            }

            var del = _expression[_pos];
            if (IsDelimiter(del))
            {
                _pos++;
                sbuf.Append(del);
                return sbuf.ToString();
            }

            for (var i = _pos; i < _length; i++)
            {
                var ch = _expression[i];
                if (IsDelimiter(ch))
                {
                    _pos = i;
                    return sbuf.ToString();
                }
                sbuf.Append(ch);
            }

            _pos = _length;
            return sbuf.ToString();
        }

        internal static bool IsDelimiter(char ch)
        {
            switch (ch)
            {
                case JSONPath.SEPARATOR:
                case JSONPath.ARRAY_START:
                case JSONPath.ARRAY_END:
                    return true;
            }
            return false;
        }
    }

}
