//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 15JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using MapDigit.DrawingFP;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 15JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The <code>Pen</code> class defines a basic set of rendering
     * attributes for the outlines of graphics primitives, which are rendered
     * with a {@link Graphics2D} object that has its Stroke attribute set to
     * this <code>Pen</code>.
     * The rendering attributes defined by <code>Pen</code> describe
     * the shape of the mark made by a pen drawn along the outline of a
     * IShape and the decorations applied at the ends and joins of
     * path segments of the <code>IShape</code>.
     * These rendering attributes include:
     * <dl compact>
     * <dt><i>width</i>
     * <dd>The pen width, measured perpendicularly to the pen trajectory.
     * <dt><i>end caps</i>
     * <dd>The decoration applied to the ends of unclosed subpaths and
     * dash segments.  Subpaths that start and end on the same point are
     * still considered unclosed if they do not have a CLOSE segment.
     * <dd>The limit to trim a line join that has a JOIN_MITER decoration.
     * A line join is trimmed when the ratio of miter length to stroke
     * width is greater than the miterlimit Value.  The miter length is
     * the diagonal length of the miter, which is the distance between
     * the inside corner and the outside corner of the intersection.
     * The smaller the angle formed by two line segments, the longer
     * the miter length and the sharper the angle of intersection.  The
     * default miterlimit Value of 10 causes all angles less than
     * 11 degrees to be trimmed.  Trimming miters converts
     * the decoration of the line join to bevel.
     * <dt><i>dash attributes</i>
     * <dd>The definition of how to make a dash pattern by alternating
     * between opaque and transparent sections.
     * </dl>
     * For more information on the user space coordinate system and the
     * rendering process, see the <code>Graphics2D</code> class comments.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public class Pen
    {

        //[------------------------------ CONSTANTS -------------------------------]
        /**
         * Joins path segments by extending their outside edges until
         * they meet.
         */
        public const int JOIN_MITER = PenFP.LINEJOIN_MITER;
        /**
         * Joins path segments by rounding off the corner at a radius
         * of half the line width.
         */
        public const int JOIN_ROUND = PenFP.LINEJOIN_ROUND;
        /**
         * Joins path segments by connecting the outer corners of their
         * wide outlines with a straight segment.
         */
        public const int JOIN_BEVEL = PenFP.LINEJOIN_BEVEL;
        /**
         * Ends unclosed subpaths and dash segments with no added
         * decoration.
         */
        public const int CAP_BUTT = PenFP.LINECAP_BUTT;
        /**
         * Ends unclosed subpaths and dash segments with a Round
         * decoration that has a radius equal to half of the width
         * of the pen.
         */
        public const int CAP_ROUND = PenFP.LINECAP_ROUND;
        /**
         * Ends unclosed subpaths and dash segments with a square
         * projection that extends beyond the end of the segment
         * to a distance equal to half of the line width.
         */
        public const int CAP_SQUARE = PenFP.LINECAP_SQUARE;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>Pen</code> with the specified
         * attributes.
         * @param brush brush used to construct the pen object.
         * @param width the width of this <code>Pen</code>.  The
         *         width must be greater than or equal to 0.  If width is
         *         set to 0, the stroke is rendered as the thinnest
         *         possible line for the target device and the antialias
         *         hint setting.
         * @param cap the decoration of the ends of a <code>Pen</code>
         * @param join the decoration applied where path segments meet
         * @param dash the array representing the dashing pattern
         * @param dash_phase the offset to start the dashing pattern
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(int width, int cap, int join, int[] dash, int dashPhase)
            : this(Color.Black, width, cap, join, dash, dashPhase)
        {


        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>Pen</code> with the specified
         * attributes.
         * @param color the color of the pen.
         * @param width the width of this <code>Pen</code>.  The
         *         width must be greater than or equal to 0.  If width is
         *         set to 0, the stroke is rendered as the thinnest
         *         possible line for the target device and the antialias
         *         hint setting.
         * @param cap the decoration of the ends of a <code>Pen</code>
         * @param join the decoration applied where path segments meet
         * @param dash the array representing the dashing pattern
         * @param dash_phase the offset to start the dashing pattern
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(Color color, int width, int cap, int join,
                int[] dash, int dashPhase)
        {
            if (width < 0)
            {
                throw new ArgumentException("negative width");
            }
            if (cap != CAP_BUTT && cap != CAP_ROUND && cap != CAP_SQUARE)
            {
                throw new ArgumentException("illegal end cap Value");
            }
            if (join != JOIN_ROUND && join != JOIN_BEVEL && join != JOIN_MITER)
            {
                throw new ArgumentException("illegal line join Value");
            }
            if (dash != null)
            {
                if (dashPhase < 0)
                {
                    throw new ArgumentException("negative dash phase");
                }
                bool allzero = true;
                for (int i = 0; i < dash.Length; i++)
                {
                    int d = dash[i];
                    if (d > 0)
                    {
                        allzero = false;
                    }
                    else if (d < 0)
                    {
                        throw new ArgumentException("negative dash length");
                    }
                }
                if (allzero)
                {
                    throw new ArgumentException("dash lengths all zero");
                }
            }
            _width = width;
            _cap = cap;
            _join = join;
            _color = color;
            if (dash != null)
            {
                _dash = dash;
            }

            _dashPhase = dashPhase;
            _wrappedPenFP = new PenFP(color._value, width << SingleFP.DECIMAL_BITS,
                    cap, cap, join);
            if (dash != null)
            {
                int[] newDash = new int[dash.Length];
                for (int i = 0; i < newDash.Length; i++)
                {
                    newDash[i] = dash[i] << SingleFP.DECIMAL_BITS;
                }
                _wrappedPenFP.SetDashArray(newDash, dashPhase);
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a solid <code>Pen</code> with the specified
         * attributes.
         * @param brush brush used to create the pen.
         * @param width the width of the <code>Pen</code>
         * @param cap the decoration of the ends of a <code>Pen</code>
         * @param join the decoration applied where path segments meet
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(int width, int cap, int join)
            : this(width, cap, join, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a solid <code>Pen</code> with the specified
         * attributes.
         * @param color the color of the <code>Pen</code>
         * @param width the width of the <code>Pen</code>
         * @param cap the decoration of the ends of a <code>Pen</code>
         * @param join the decoration applied where path segments meet
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(Color color, int width, int cap, int join)
            : this(color, width, cap, join, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a solid <code>Pen</code> with the specified
         * line width and with default values for the cap and join
         * styles.
         * @param brush the brush used to create the pen.
         * @param width the width of the <code>Pen</code>
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(int width)
            : this(width, CAP_SQUARE, JOIN_MITER, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a solid <code>Pen</code> with the specified
         * line width and with default values for the cap and join
         * styles.
         * @param color the color of the <code>Pen</code>
         * @param width the width of the <code>Pen</code>
         * @throws IllegalArgumentException if <code>width</code> is negative
         */
        public Pen(Color color, int width)
            : this(color, width, CAP_SQUARE, JOIN_MITER, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>Pen</code> with defaults for all
         * attributes.
         * The default attributes are a solid line of width 1.0, CAP_SQUARE,
         * JOIN_MITER, a miter limit of 10.
         * @param brush brush used to create the pen.
         */
        public Pen()
            : this(1, CAP_SQUARE, JOIN_MITER, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>Pen</code> with defaults for all
         * attributes.
         * The default attributes are a solid line of width 1.0, CAP_SQUARE,
         * JOIN_MITER, a miter limit of 10.
         * @param color the color of the <code>Pen</code>
         */
        public Pen(Color color)
            : this(color, 1, CAP_SQUARE, JOIN_MITER, null, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the line width.  Line width is represented in user space,
         * which is the default-coordinate space used by Java 2D.  See the
         * <code>Graphics2D</code> class comments for more information on
         * the user space coordinate system.
         * @return the line width of this <code>Pen</code>.
         */
        public int GetLineWidth()
        {
            return _width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the pen color.
         * @return the color of this <code>Pen</code>.
         */
        public Color GetPenColor()
        {
            return _color;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the end cap style.
         * @return the end cap style of this <code>Pen</code> as one
         * of the static <code>int</code> values that define possible end cap
         * styles.
         */
        public int GetEndCap()
        {
            return _cap;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the line join style.
         * @return the line join style of the <code>Pen</code> as one
         * of the static <code>int</code> values that define possible line
         * join styles.
         */
        public int GetLineJoin()
        {
            return _join;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the array representing the lengths of the dash segments.
         * Alternate entries in the array represent the user space lengths
         * of the opaque and transparent segments of the dashes.
         * As the pen moves along the outline of the <code>IShape</code>
         * to be stroked, the user space
         * distance that the pen travels is accumulated.  The distance
         * Value is used to index into the dash array.
         * The pen is opaque when its current cumulative distance maps
         * to an even element of the dash array and transparent otherwise.
         * @return the dash array.
         */
        public int[] GetDashArray()
        {
            if (_dash == null)
            {
                return null;
            }
            return _dash;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the current dash phase.
         * The dash phase is a distance specified in user coordinates that
         * represents an offset into the dashing pattern. In other words, the dash
         * phase defines the point in the dashing pattern that will correspond to
         * the beginning of the stroke.
         * @return the dash phase as a <code>int</code> Value.
         */
        public int GetDashPhase()
        {
            return _dashPhase;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if this Pen represents the same
         * stroking operation as the given argument.
         */
        /**
         * Tests if a specified object is equal to this <code>Pen</code>
         * by first testing if it is a <code>Pen</code> and then comparing
         * its width, join, cap, miter limit, dash, and dash phase attributes with
         * those of this <code>Pen</code>.
         * @param  obj the specified object to compare to this
         *              <code>Pen</code>
         * @return <code>true</code> if the width, join, cap, miter limit, dash, and
         *            dash phase are the same for both objects;
         *            <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (!(obj is Pen))
            {
                return false;
            }

            Pen bs = (Pen)obj;
            if (_width != bs._width)
            {
                return false;
            }

            if (_join != bs._join)
            {
                return false;
            }

            if (_cap != bs._cap)
            {
                return false;
            }


            if (_color != bs._color)
            {
                return false;
            }

            if (_dash != null)
            {
                if (_dashPhase != bs._dashPhase)
                {
                    return false;
                }

                if (!Equals(_dash, bs._dash))
                {
                    return false;
                }
            }
            else if (bs._dash != null)
            {
                return false;
            }

            return true;
        }

        internal readonly int _width;
        internal readonly int _join;
        internal readonly int _cap;
        internal readonly int[] _dash;
        internal readonly int _dashPhase;
        internal readonly Color _color;
        internal Brush _brush;
        internal readonly PenFP _wrappedPenFP;
    }

}
