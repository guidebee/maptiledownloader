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
     * GeoLatLngBounds is a bound in geographical coordinates longitude and latitude.
     * Note: the positive of North is from top to bottom instead of from bottom to 
     * top internally.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoLatLngBounds : GeoBounds
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner 
         * is at (0,&nbsp;0) in the coordinate space, and whose Width and 
         * Height are both zero. 
         */
        public GeoLatLngBounds()
            : this(0, 0, 0, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code>, initialized to match 
         * the values of the specified <code>GeoBounds</code>.
         * @param r  the <code>GeoBounds</code> from which to copy initial values
         *           to a newly constructed <code>GeoBounds</code>
         */
        public GeoLatLngBounds(GeoLatLngBounds r)
            : this(r.X, r.Y, r.Width, r.Height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner is 
         * specified as
         * {@code (X,Y)} and whose Width and Height 
         * are specified by the arguments of the same name. 
         * @param     X the specified X coordinate
         * @param     Y the specified Y coordinate
         * @param     Width    the Width of the <code>GeoBounds</code>
         * @param     Height   the Height of the <code>GeoBounds</code>
         */
        public GeoLatLngBounds(double x, double y, double width, double height)
            : base(x, y, width, height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner 
         * is at (0,&nbsp;0) in the coordinate space, and whose Width and 
         * Height are specified by the arguments of the same name. 
         * @param Width the Width of the <code>GeoBounds</code>
         * @param Height the Height of the <code>GeoBounds</code>
         */
        public GeoLatLngBounds(int width, int height)
            : this(0, 0, width, height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner is 
         * specified by the GeoPoint argument, and
         * whose Width and Height are specified by the 
         * {@link GeoSize} argument.
         * @param p a <code>GeoPoint</code> that is the upper-left corner of 
         * the <code>GeoBounds</code>
         * @param size a <code>GeoSize</code>, representing the 
         * Width and Height of the <code>GeoBounds</code>
         */
        public GeoLatLngBounds(GeoPoint p, GeoSize size)
            : this(p.X, p.Y, size.Width, size.Height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner is the  
         * specified <code>GeoPoint</code>, and whose Width and Height are both zero. 
         * @param p a <code>GeoPoint</code> that is the top left corner 
         * of the <code>GeoBounds</code>
         */
        public GeoLatLngBounds(GeoPoint p)
            : this(p.X, p.Y, 0, 0)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose top left corner is  
         * (0,&nbsp;0) and whose Width and Height are specified  
         * by the <code>GeoSize</code> argument. 
         * @param size a <code>GeoSize</code>, specifying Width and Height
         */
        public GeoLatLngBounds(GeoSize size)
            : this(0, 0, size.Width, size.Height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a rectangle from the points at its south-west and north-east 
         * corners.
         * @param sw  south-west point of the rectangle.
         * @param ne  north-east point of the rectangle.
         */
        public GeoLatLngBounds(GeoLatLng sw, GeoLatLng ne)
            : this(sw.X, sw.Y, ne.X - sw.X, ne.Y - sw.Y)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if the geographical coordinates of the point lie within 
         * this rectangle
         * @param latlng  the given point.
         * @return  if the geographical coordinates of the point lie within 
         * this rectangle
         */
        public bool ContainsLatLng(GeoLatLng latlng)
        {
            return Contains(latlng.X, latlng.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the IShape Intersects the interior of a 
         * specified rectangular area.
         * @param other  the given rectangle.
         * @return  true if the interior of the IShape and the interior of the 
         * rectangular area Intersect.
         */
        public bool Intersects(GeoLatLngBounds other)
        {
            return Intersects(other.X, other.Y, other.Width, other.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the interior of the IShape entirely Contains the specified 
         * rectangular area. 
         * @param other  the given rectangle.
         * @return  true if the interior of the IShape entirely Contains the 
         * specified rectangular area; 
         */
        public bool ContainsBounds(GeoLatLngBounds other)
        {
            return Contains(other.X, other.Y, other.Width, other.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Enlarges this rectangle such that it Contains the given point. 
         * @param latlng  the new GeoLatLng to Add to this rectangle.
         */
        public void Extend(GeoLatLng latlng)
        {
            Add(latlng.X, latlng.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the point at the south-west corner of the rectangle.
         * @return the point at the south-west corner of the rectangle.
         */
        public GeoLatLng GetSouthWest()
        {
            return new GeoLatLng(Y, X);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the point at the north-east corner of the rectangle.
         * @return the point at the north-east corner of the rectangle.
         */
        public GeoLatLng GetNorthEast()
        {
            return new GeoLatLng(Y + Height, X + Width);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a GLatLng whose cordinates represent the size of this rectangle.
         * @return the point whose cordinates represent the size of this rectangle.
         */
        public GeoLatLng ToSpan()
        {
            return new GeoLatLng(Width, Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if this rectangle extends from the south pole to the north pole.
         * @return true if this rectangle extends from the south pole to the north pole.
         */
        public bool IsFullLat()
        {
            return (Y == -90 && Height == 180);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if this rectangle extends fully around the earth in the
         * longitude direction.
         * @return true if this rectangle extends fully around the earth in the
         * longitude direction.
         */
        public bool IsFullLng()
        {
            return (Y == -180 && Height == 360);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if this rectangle is empty.
         * @return true if this rectangle is empty.
         */
        public override bool IsEmpty()
        {
            return (Width <= 0) || (Height <= 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the point at the center of the rectangle.
         * @return the point at the center of the rectangle.
         */
        public GeoLatLng GetCenter()
        {
            GeoPoint pt1 = MapLayer.FromLatLngToPixel(GetSouthWest(), 15);
            GeoPoint pt2 = MapLayer.FromLatLngToPixel(GetNorthEast(), 15);
            GeoPoint pt = new GeoPoint();
            pt.X = pt1.X + (pt2.X - pt1.X) / 2;
            pt.Y = pt1.Y + (pt2.Y - pt1.Y) / 2;
            return MapLayer.FromPixelToLatLng(pt, 15);
        }
    }

}
