//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 18JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Geometry
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The <code>GeoSize</code> class encapsulates the Width and 
     * Height of a component (in integer precision) in a single object. 
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoSize
    {

        /**
         * The Width GeoSize; negative values can be used.
         */
        public double Width;
        /**
         * The Height GeoSize; negative values can be used.
         */
        public double Height;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /** 
         * Creates an instance of <code>GeoSize</code> with a Width 
         * of zero and a Height of zero. 
         */
        public GeoSize()
            : this(0, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /** 
         * Creates an instance of <code>GeoSize</code> whose Width  
         * and Height are the same as for the specified GeoSize.
         *
         * @param    size   the specified GeoSize for the 
         *               <code>Width</code> and 
         *               <code>Height</code> values
         */
        public GeoSize(GeoSize size)
            : this(size.Width, size.Height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /** 
         * Constructs a <code>GeoSize</code> and initializes
         * it to the specified Width and specified Height.
         *
         * @param Width the specified Width 
         * @param Height the specified Height
         */
        public GeoSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the Width.
         * @return the Width of the geo size.
         */
        public double GetWidth()
        {
            return Width;
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the size of this <code>GeoSize</code> object to
         * match the specified size.
         * This method is included for completeness, to parallel the
         * <code>getSize</code> method of <code>Component</code>.
         * @param d  the new size for the <code>GeoSize</code>
         * object
         */
        public void SetSize(GeoSize d)
        {
            SetSize(d.GetWidth(), d.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the Height.
         * @return the Height of the geo size.
         */
        public double GetHeight()
        {
            return Height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the size of this <code>GeoSize</code> object to
         * the specified Width and Height in double precision.
         * Note that if <code>Width</code> or <code>Height</code>
         * are larger than <code>Integer.MAX_VALUE</code>, they will
         * be reset to <code>Integer.MAX_VALUE</code>.
         *
         * @param Width  the new Width for the <code>GeoSize</code> object
         * @param Height the new Height for the <code>GeoSize</code> object
         */
        public void SetSize(double width, double height)
        {
            Width = (int)Math.Ceiling(width);
            Height = (int)Math.Ceiling(height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Gets the size of this <code>GeoSize</code> object.
         * This method is included for completeness, to parallel the
         * <code>getSize</code> method defined by <code>Component</code>.
         *
         * @return   the size of this GeoSize, a new instance of
         *           <code>GeoSize</code> with the same Width and Height
         */
        public GeoSize GetSize()
        {
            return new GeoSize(Width, Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the size of this <code>GeoSize</code> object
         * to the specified Width and Height.
         * This method is included for completeness, to parallel the
         * <code>setSize</code> method defined by <code>Component</code>.
         *
         * @param    Width   the new Width for this <code>GeoSize</code> object
         * @param    Height  the new Height for this <code>GeoSize</code> object
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
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Checks whether two GeoSize objects have equal values.
         * @param obj the object to compare
         * @return ture if they have same size.
         */
        public new bool Equals(object obj)
        {
            if (obj is GeoSize)
            {
                GeoSize d = (GeoSize)obj;
                return (Width == d.Width) && (Height == d.Height);
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the hash code for this <code>GeoSize</code>.
         *
         * @return    a hash code for this <code>GeoSize</code>
         */
        public int HashCode()
        {
            double sum = Width + Height;
            return (int)(sum * (sum + 1) / 2 + Width);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of the values of this
         * <code>GeoSize</code> object's <code>Height</code> and
         * <code>Width</code> fields. This method is intended to be used only
         * for debugging purposes, and the content and format of the returned
         * string may vary between implementations. The returned string may be
         * empty but may not be <code>null</code>.
         *
         * @return  a string representation of this <code>GeoSize</code>
         *          object
         */
        public override string ToString()
        {
            return "[Width=" + Width + ",Height=" + Height + "]";
        }
    }

}
