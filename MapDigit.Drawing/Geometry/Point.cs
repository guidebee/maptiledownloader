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
     * A point representing a location in {@code (x,y)} coordinate space,
     * specified in integer.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Point
    {

        /**
         * The X coordinate of this <code>Point</code>.
         * If no X coordinate is set it will default to 0.
         */
        public int X;

        /**
         * The Y coordinate of this <code>Point</code>. 
         * If no Y coordinate is set it will default to 0.
         */
        public int Y;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the origin 
         * (0,&nbsp;0) of the coordinate space. 
         */
        public Point()
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
         * Constructs and initializes a point with the same location as
         * the specified <code>Point</code> object.
         * @param       p a point
         */
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the specified 
         * {@code (x,y)} location in the coordinate space. 
         * @param x the X coordinate of the newly constructed <code>Point</code>
         * @param y the Y coordinate of the newly constructed <code>Point</code>
         */
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of this <code>Point</code>.
         * @return the X coordinate of this <code>Point</code>.
         */
        public int GetX()
        {
            return X;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of this <code>Point</code>.
         * @return the Y coordinate of this <code>Point</code>.
         */
        public int GetY()
        {
            return Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the location of this point.
         * This method is included for completeness, to parallel the
         * <code>getLocation</code> method of <code>Component</code>.
         * @return      a copy of this point, at the same location
         */
        public Point GetLocation()
        {
            return new Point(X, Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location of the point to the specified location.
         * This method is included for completeness, to parallel the
         * <code>setLocation</code> method of <code>Component</code>.
         * @param       p  a point, the new location for this point
         */
        public void SetLocation(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Changes the point to have the specified location.
         * <p>
         * This method is included for completeness, to parallel the
         * <code>setLocation</code> method of <code>Component</code>.
         * Its behavior is identical with <code>move(int,&nbsp;int)</code>.
         * @param       x the X coordinate of the new location
         * @param       y the Y coordinate of the new location
         */
        public void SetLocation(int x, int y)
        {
            X = x;
            Y = y;
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Moves this point to the specified location in the 
         * {@code (x,y)} coordinate plane. This method
         * is identical with <code>setLocation(int,&nbsp;int)</code>.
         * @param       x the X coordinate of the new location
         * @param       y the Y coordinate of the new location
         */
        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Translates this point, at location {@code (x,y)}, 
         * by {@code dx} along the {@code x} axis and {@code dy} 
         * along the {@code y} axis so that it now represents the point 
         * {@code (x+dx,y+dy)}.
         *
         * @param       dx   the distance to move this point 
         *                            along the X axis
         * @param       dy    the distance to move this point 
         *                            along the Y axis
         */
        public void Translate(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not two points are equal. Two instances of
         * <code>Point</code> are equal if the values of their
         * <code>x</code> and <code>y</code> member fields, representing
         * their position in the coordinate space, are the same.
         * @param obj an object to be compared with this <code>Point</code>
         * @return <code>true</code> if the object to be compared is
         *         an instance of <code>Point</code> and has
         *         the same values; <code>false</code> otherwise.
         */
        public new bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point pt = (Point)obj;
                return (X == pt.X) && (Y == pt.Y);
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string representation of this point and its location 
         * in the {@code (x,y)} coordinate space. This method is 
         * intended to be used only for debugging purposes, and the content 
         * and format of the returned string may vary between implementations. 
         * The returned string may be empty but may not be <code>null</code>.
         * 
         * @return  a string representation of this point
         */
        public override String ToString()
        {
            return "POINT [" + X +
                        "," + Y + "]";
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance between two points.
         *
         * @param x1 the X coordinate of the first specified point
         * @param y1 the Y coordinate of the first specified point
         * @param X2 the X coordinate of the second specified point
         * @param y2 the Y coordinate of the second specified point
         * @return the square of the distance between the two
         * sets of specified coordinates.
         */
        public static int DistanceSq(int x1, int y1,
                        int x2, int y2)
        {
            x1 -= x2;
            y1 -= y2;
            return (x1 * x1 + y1 * y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance between two points.
         *
         * @param x1 the X coordinate of the first specified point
         * @param y1 the Y coordinate of the first specified point
         * @param X2 the X coordinate of the second specified point
         * @param y2 the Y coordinate of the second specified point
         * @return the distance between the two sets of specified
         * coordinates.
         */
        public static int Distance(int x1, int y1,
                      int x2, int y2)
        {
            x1 -= x2;
            y1 -= y2;
            long disSq = x1 * x1 + y1 * y1;
            long dis = MathFP.Sqrt(disSq << MathFP.DEFAULT_PRECISION);
            return MathFP.ToInt(dis);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from this
         * <code>Point</code> to a specified point.
         *
         * @param px the X coordinate of the specified point to be measured
         *           against this <code>Point</code>
         * @param py the Y coordinate of the specified point to be measured
         *           against this <code>Point</code>
         * @return the square of the distance between this
         * <code>Point</code> and the specified point.
         */
        public int DistanceSq(int px, int py)
        {
            px -= GetX();
            py -= GetY();
            return (px * px + py * py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the square of the distance from this
         * <code>Point</code> to a specified <code>Point</code>.
         *
         * @param pt the specified point to be measured
         *           against this <code>Point</code>
         * @return the square of the distance between this
         * <code>Point</code> to a specified <code>Point</code>.
         */
        public int DistanceSq(Point pt)
        {
            int px = pt.GetX() - GetX();
            int py = pt.GetY() - GetY();
            return (px * px + py * py);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from this <code>Point</code> to
         * a specified point.
         *
         * @param px the X coordinate of the specified point to be measured
         *           against this <code>Point</code>
         * @param py the Y coordinate of the specified point to be measured
         *           against this <code>Point</code>
         * @return the distance between this <code>Point</code>
         * and a specified point.
         */
        public int Distance(int px, int py)
        {
            px -= GetX();
            py -= GetY();
            long disSq = px * px + py * py;
            long dis = MathFP.Sqrt(disSq << MathFP.DEFAULT_PRECISION);
            return MathFP.ToInt(dis);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the distance from this <code>Point</code> to a
         * specified <code>Point</code>.
         *
         * @param pt the specified point to be measured
         *           against this <code>Point</code>
         * @return the distance between this <code>Point</code> and
         * the specified <code>Point</code>.
         */
        public int Distance(Point pt)
        {
            int px = pt.GetX() - GetX();
            int py = pt.GetY() - GetY();
            long disSq = px * px + py * py;
            long dis = MathFP.Sqrt(disSq << MathFP.DEFAULT_PRECISION);
            return MathFP.ToInt(dis);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the hashcode for this <code>Point</code>.
         * @return      a hash code for this <code>Point</code>.
         */
        public int HashCode()
        {
            int bits = (int)((GetX() << 16) & 0xFFFF0000);
            bits ^= GetY();
            return bits;
        }


    }

}
