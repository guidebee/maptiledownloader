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
using MapDigit.Util;

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
     * A point representing a location in {@code (X,Y)} coordinate space,
     * specified in integer precision.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoPoint
    {

        /**
         * The X coordinate of this <code>GeoPoint</code>.
         */
        public double X;
        /**
         * The Y coordinate of this <code>GeoPoint</code>.
         */
        public double Y;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the origin 
         * (0,&nbsp;0) of the coordinate space. 
         */
        public GeoPoint()
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point with the same location as
         * the specified <code>Point</code> object.
         * @param       p a point
         */
        public GeoPoint(GeoPoint p)
            : this(p.X, p.Y)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the specified 
         * {@code (X,Y)} location in the coordinate space. 
         * @param X the X coordinate of the newly constructed <code>Point</code>
         * @param Y the Y coordinate of the newly constructed <code>Point</code>
         */
        public GeoPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of this point and its location 
         * in the {@code (X,Y)} coordinate space. This method is 
         * intended to be used only for debugging purposes, and the content 
         * and format of the returned string may vary between implementations. 
         * The returned string may be empty but may not be <code>null</code>.
         * 
         * @return  Returns a string that Contains the X and Y coordinates, 
         * in this order, separated by a comma.
         */
        public override string ToString()
        {
            return X + "," + Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the X coordinate.
         * @return the X coordinate(longitude)
         */
        public double GetX()
        {
            return X;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  return the Y coordinate.
         * @return the Y coordinate(longitude)
         */
        public double GetY()
        {
            return Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of this <code>GeoPoint</code> to the same
         * coordinates as the specified <code>GeoPoint</code> object.
         * @param p the specified <code>GeoPoint</code> to which to set
         * this <code>GeoPoint</code>
         */
        public void SetLocation(GeoPoint p)
        {
            SetLocation(p.GetX(), p.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the Distance between two points.
         *
         * @param x1 the X coordinate of the first specified point
         * @param y1 the Y coordinate of the first specified point
         * @param X2 the X coordinate of the second specified point
         * @param y2 the Y coordinate of the second specified point
         * @return the square of the Distance between the two
         * sets of specified coordinates.
         */
        public static double DistanceSq(double x1, double y1,
                double x2, double y2)
        {
            x1 -= x2;
            y1 -= y2;
            return (x1 * x1 + y1 * y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Distance between two points.
         *
         * @param x1 the X coordinate of the first specified point
         * @param y1 the Y coordinate of the first specified point
         * @param X2 the X coordinate of the second specified point
         * @param y2 the Y coordinate of the second specified point
         * @return the Distance between the two sets of specified
         * coordinates.
         */
        public static double Distance(double x1, double y1,
                double x2, double y2)
        {
            x1 -= x2;
            y1 -= y2;
            return MathEx.Sqrt(x1 * x1 + y1 * y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the Distance from this
         * <code>GeoPoint</code> to a specified point.
         *
         * @param px the X coordinate of the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @param py the Y coordinate of the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @return the square of the Distance between this
         * <code>GeoPoint</code> and the specified point.
         */
        public double DistanceSq(double px, double py)
        {
            px -= GetX();
            py -= GetY();
            return (px * px + py * py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the Distance from this
         * <code>GeoPoint</code> to a specified <code>GeoPoint</code>.
         *
         * @param pt the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @return the square of the Distance between this
         * <code>GeoPoint</code> to a specified <code>GeoPoint</code>.
         */
        public double DistanceSq(GeoPoint pt)
        {
            double px = pt.GetX() - GetX();
            double py = pt.GetY() - GetY();
            return (px * px + py * py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Distance from this <code>GeoPoint</code> to
         * a specified point.
         *
         * @param px the X coordinate of the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @param py the Y coordinate of the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @return the Distance between this <code>GeoPoint</code>
         * and a specified point.
         */
        public double Distance(double px, double py)
        {
            px -= GetX();
            py -= GetY();
            return MathEx.Sqrt(px * px + py * py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Distance from this <code>GeoPoint</code> to a
         * specified <code>GeoPoint</code>.
         *
         * @param pt the specified point to be measured
         *           against this <code>GeoPoint</code>
         * @return the Distance between this <code>GeoPoint</code> and
         * the specified <code>GeoPoint</code>.
         */
        public double Distance(GeoPoint pt)
        {
            double px = pt.GetX() - GetX();
            double py = pt.GetY() - GetY();
            return MathEx.Sqrt(px * px + py * py);
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not two points are equal. Two instances of
         * <code>GeoPoint</code> are equal if the values of their
         * <code>X</code> and <code>Y</code> member fields, representing
         * their position in the coordinate space, are the same.
         * @param obj an object to be compared with this <code>GeoPoint</code>
         * @return <code>true</code> if the object to be compared is
         *         an instance of <code>GeoPoint</code> and has
         *         the same values; <code>false</code> otherwise.
         */
        public new bool Equals(object obj)
        {
            if (obj is GeoPoint)
            {
                GeoPoint p2d = (GeoPoint)obj;
                return (GetX() == p2d.GetX()) && (GetY() == p2d.GetY());
            }
            return base.Equals(obj);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the new location.
         * @param X new X coordinate.
         * @param Y new Y coordinate.
         */
        public void SetLocation(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

}
