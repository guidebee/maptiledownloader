//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 13JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.DrawingFP
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines an object used to draw lines and curves.
     * This class cannot be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class PenFP
    {

        /**
         * Specifies a butt line cap.
         */
        public const int LINECAP_BUTT = 1;

        /**
         * Specifies a round line cap.
         */
        public const int LINECAP_ROUND = 2;

        /**
         * Specifies a square line cap.
         */
        public const int LINECAP_SQUARE = 3;

        /**
         * Specifies a mitered join. This produces a sharp corner or a clipped
         * corner, depending on whether the length of the miter exceeds the miter
         * limit.
         */
        public const int LINEJOIN_MITER = 1;

        /**
         * Specifies a circular join. This produces a smooth, circular arc
         * between the lines.
         */
        public const int LINEJOIN_ROUND = 2;

        /**
         * Specifies a beveled join. This produces a diagonal corner.
         */
        public const int LINEJOIN_BEVEL = 3;    //public int Color;


        /**
         * the stroke width of the pen.
         */
        public int Width;

        /**
         * the line join for this pen.
         */
        public int LineJoin;

        /**
         * the brush
         */
        public BrushFP Brush;

        /**
         * cap style used at the beginning of lines drawn with this Pen.
         */
        public int StartCap;

        /**
         * cap style used at the edning of lines drawn with this Pen.
         */
        public int EndCap;

        /**
         * the dash Array ,and if dash array is not null,
         *  then startCap = PenFP.LINECAP_BUTT;
         *  endCap = PenFP.LINECAP_BUTT;
         *  and  lineJoin = PenFP.LINEJOIN_BEVEL;
         */
        public int[] DashArray;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param color the color of this pen.
         */
        public PenFP(int color):this(color, SingleFP.ONE)
        {
           
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor
         * @param color the color of this pen.
         * @param ff_width the width of this pen.
         */
        public PenFP(int color, int ffWidth)
            : this(color, ffWidth, LINECAP_BUTT, LINECAP_BUTT, LINEJOIN_MITER)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param color   the color of the pen
         * @param ff_width  the width of the pen
         * @param linecap  the start cap style of this pen.
         * @param linejoin the end cap style of this pen.
         */
        public PenFP(int color, int ffWidth, int linecap, int linejoin)
            : this(color, ffWidth, linecap, linecap, linejoin)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constrcutor.
         * @param brush   the brush.
         * @param ff_width the width of this pen.
         * @param linecap  the start cap style of this pen.
         * @param linejoin the end cap style of this pen.
         */
        public PenFP(BrushFP brush, int ffWidth, int linecap, int linejoin)
            : this(brush, ffWidth, linecap, linecap, linejoin)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param color  the color.
         * @param ff_width
         * @param startlinecap
         * @param endlinecap
         * @param linejoin
         */
        public PenFP(int color, int ffWidth, int startlinecap, int endlinecap,
                int linejoin)
            : this(new SolidBrushFP(color), ffWidth, startlinecap, endlinecap,
                linejoin)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param brush
         * @param ff_width
         * @param startlinecap
         * @param endlinecap
         * @param linejoin
         */
        public PenFP(BrushFP brush, int ffWidth, int startlinecap,
                int endlinecap, int linejoin)
        {
            Brush = brush;
            Width = ffWidth;
            StartCap = startlinecap;
            EndCap = endlinecap;
            LineJoin = linejoin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the dash array for this pen.
         * @param dashArray
         * @param offset
         */
        public void SetDashArray(int[] dashArrays, int offset)
        {
            int len = DashArray.Length - offset;
            DashArray = null;
            if (len > 1)
            {
                DashArray = new int[len];
                Array.Copy(dashArrays, offset, DashArray, 0, len);
            }
        }
    }
}
