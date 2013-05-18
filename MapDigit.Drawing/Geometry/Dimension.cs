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
namespace MapDigit.Drawing.Geometry
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The <code>Dimension</code> class encapsulates the width and
     * height of a component (in integer=) in a single object.
     * The class is
     * associated with certain properties of components. Several methods
     * defined by the <code>Component</code> class and the
     * <code>LayoutManager</code> interface return a
     * <code>Dimension</code> object.
     * <p>
     * Normally the values of <code>width</code>
     * and <code>height</code> are non-negative integers.
     * The constructors that allow you to create a dimension do
     * not prevent you from setting a negative Value for these properties.
     * If the Value of <code>width</code> or <code>height</code> is
     * negative, the behavior of some methods defined by other objects is
     * undefined.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Dimension
    {

        /**
         * The width dimension; negative values can be used.
         */
        public int Width;
        /**
         * The height dimension; negative values can be used.
         */
        public int Height;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an instance of <code>Dimension</code> with a width
         * of zero and a height of zero.
         */
        public Dimension()
            : this(0, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an instance of <code>Dimension</code> whose width
         * and height are the same as for the specified dimension.
         *
         * @param    d   the specified dimension for the
         *               <code>width</code> and
         *               <code>height</code> values
         */
        public Dimension(Dimension d)
        {
            Width = d.Width;
            Height = d.Height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a <code>Dimension</code> and initializes
         * it to the specified width and specified height.
         *
         * @param width the specified width
         * @param height the specified height
         */
        public Dimension(int width, int height)
        {
            Width = width;
            Height = height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the width of this <code>Dimension</code> in double
         * precision.
         * @return the width of this <code>Dimension</code>.
         */
        public int GetWidth()
        {
            return Width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the height of this <code>Dimension</code> in double
         * precision.
         * @return the height of this <code>Dimension</code>.
         */
        public int GetHeight()
        {
            return Height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the size of this <code>Dimension</code> object to
         * the specified width and height in int precision.
         * Note that if <code>width</code> or <code>height</code>
         * are larger than <code>Integer.MAX_VALUE</code>, they will
         * be reset to <code>Integer.MAX_VALUE</code>.
         *
         * @param width  the new width for the <code>Dimension</code> object
         * @param height the new height for the <code>Dimension</code> object
         */
        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the size of this <code>Dimension</code> object.
         * This method is included for completeness, to parallel the
         * <code>getSize</code> method defined by <code>Component</code>.
         *
         * @return   the size of this dimension, a new instance of
         *           <code>Dimension</code> with the same width and height
         */
        public Dimension GetSize()
        {
            return new Dimension(Width, Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the size of this <code>Dimension</code> object to the specified size.
         * This method is included for completeness, to parallel the
         * <code>setSize</code> method defined by <code>Component</code>.
         * @param    d  the new size for this <code>Dimension</code> object
         */
        public void SetSize(Dimension d)
        {
            Width = d.Width;
            Height = d.Height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Checks whether two dimension objects have equal values.
         * @param obj object to compare
         * @return true,if they have same size.
         */
        public new bool Equals(Object obj)
        {
            if (obj is Dimension)
            {
                Dimension d = (Dimension)obj;
                return (Width == d.Width) && (Height == d.Height);
            }
            return false;
        }

        /**
         * Set the width of the dimension
         *
         * @param width the dimention width
         */
        public void SetWidth(int width)
        {
            Width = width;
        }

        /**
         * Set the height of the dimension
         *
         * @param height the dimention height
         */
        public void SetHeight(int height)
        {
            Height = height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the hash code for this <code>Dimension</code>.
         *
         * @return    a hash code for this <code>Dimension</code>
         */
        public int HashCode()
        {
            int sum = Width + Height;
            return (sum * (sum + 1) / 2 + Width);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of the values of this
         * <code>Dimension</code> object's <code>height</code> and
         * <code>width</code> fields. This method is intended to be used only
         * for debugging purposes, and the content and format of the returned
         * string may vary between implementations. The returned string may be
         * empty but may not be <code>null</code>.
         *
         * @return  a string representation of this <code>Dimension</code>
         *          object
         */
        public override string ToString()
        {
            return "SIZE [width=" + Width + ",height=" + Height + "]";
        }
    }

}
