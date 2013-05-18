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
     * GeoBounds is a rectangular area of the map in pixel coordinates
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoBounds
    {

        /**
         * The bitmask that indicates that a point lies to the left of
         * this <code>GeoBounds</code>.
         */
        public const int OUT_LEFT = 1;
        /**
         * The bitmask that indicates that a point lies above
         * this <code>GeoBounds</code>.
         */
        public const int OUT_TOP = 2;
        /**
         * The bitmask that indicates that a point lies to the right of
         * this <code>GeoBounds</code>.
         */
        public const int OUT_RIGHT = 4;
        /**
         * The bitmask that indicates that a point lies below
         * this <code>GeoBounds</code>.
         */
        public const int OUT_BOTTOM = 8;
        /**
         * The X coordinate of this <code>GeoBounds</code>.
         */
        public double X;
        /**
         * The Y coordinate of this <code>GeoBounds</code>.
         */
        public double Y;
        /**
         * The Width of this <code>GeoBounds</code>.
         */
        public double Width;
        /**
         * The Height of this <code>GeoBounds</code>.
         */
        public double Height;
        /**
         * The X coordinate of the left edge of the rectangle.
         */
        public double MinX;
        /**
         * The Y coordinate of the top edge of the rectangle.
         */
        public double MinY;
        /**
         * The X coordinate of the right edge of the rectangle.
         */
        public double MaxX;
        /**
         * The Y coordinate of the bottom edge of the rectangle.
         */
        public double MaxY;

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
        public GeoBounds()
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
         * get the top left X coordinate.
         * @return the top left X coordinate.
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
         * get the top left Y coordinate.
         * @return the top left Y coordinate.
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
         * get the Width of the geo bound.
         * @return the Width of the geo bound.
         */
        public double GetWidth()
        {
            return Width;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the Height of the geo bound.
         * @return the Height of the geo bound.
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
         * check if the geo bound is a empty rectangle.
         * @return true,it's empty.
         */
        public virtual bool IsEmpty()
        {
            return (Width <= 0.0) || (Height <= 0.0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Reset the geo bound with new position and size.
         * @param X the X coordinate
         * @param Y the Y coordinate
         * @param w the Width
         * @param h the Height.
         */
        public void SetRect(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Reset the geo bound with same position and size with given geo bound.
         * @param r the geo bound to copy from.
         */
        public void SetRect(GeoBounds r)
        {
            X = r.GetX();
            Y = r.GetY();
            Width = r.GetWidth();
            Height = r.GetHeight();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check give (X,Y)'s relative postion to this geo bound
         * @param X the X coordinate of the point
         * @param Y the Y coordinate of the point
         * @return the relative position of the point.
         */
        public int Outcode(double x, double y)
        {
            int outValue = 0;
            if (Width <= 0)
            {
                outValue |= OUT_LEFT | OUT_RIGHT;
            }
            else if (x < X)
            {
                outValue |= OUT_LEFT;
            }
            else if (x > X + Width)
            {
                outValue |= OUT_RIGHT;
            }
            if (Height <= 0)
            {
                outValue |= OUT_TOP | OUT_BOTTOM;
            }
            else if (y < Y)
            {
                outValue |= OUT_TOP;
            }
            else if (y > Y + Height)
            {
                outValue |= OUT_BOTTOM;
            }
            return outValue;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Create the intersection rectangle between this rectangle and r rectangle.
         * @param r the other rectangle
         * @return the intersection rectangle.
         */
        public GeoBounds CreateIntersection(GeoBounds r)
        {
            GeoBounds dest = new GeoBounds();
            Intersect(this, r, dest);
            return dest;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * create the Union rectangle of the two rectangles.
         * @param r the other rectangle.
         * @return Union rectangle of the two rectangles.
         */
        public GeoBounds CreateUnion(GeoBounds r)
        {
            GeoBounds dest = new GeoBounds();
            Union(this, r, dest);
            return dest;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a rectangle that Contains all the given points.
         * @param points an array of points.
         */
        public GeoBounds(GeoPoint[] points)
            : this()
        {

            if (points == null)
            {
                SetRect(0, 0, 0, 0);
            }
            if (points != null)
            {
                int count = points.Length;
                switch (count)
                {
                    case 0:
                        SetRect(0, 0, 0, 0);
                        break;
                    case 1:
                        SetRect(points[0].X, points[0].Y, 0, 0);
                        break;
                    case 2:
                        {
                            double x1 = Math.Min(points[0].X, points[1].X);
                            double x2 = Math.Max(points[0].X, points[1].X);
                            double y1 = Math.Min(points[0].Y, points[1].Y);
                            double y2 = Math.Max(points[0].Y, points[1].Y);
                            SetRect(x1, y1, x2 - x1, y2 - y1);
                        }
                        break;
                    default:
                        {
                            double x1 = Math.Min(points[0].X, points[1].X);
                            double x2 = Math.Max(points[0].X, points[1].X);
                            double y1 = Math.Min(points[0].Y, points[1].Y);
                            double y2 = Math.Max(points[0].Y, points[1].Y);
                            SetRect(x1, y1, x2 - x1, y2 - y1);
                        }
                        for (int i = 2; i < count; i++)
                        {
                            Add(points[i].X, points[i].Y);
                        }
                        break;
                }
            }
            MinX = X;
            MinY = Y;
            MaxX = X + Width;
            MaxY = Y + Height;
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
        public GeoBounds(GeoBounds r)
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
        public GeoBounds(double x, double y, double width, double height)
        {
            SetRect(x, y, width, height);
            MinX = X;
            MinY = Y;
            MaxX = X + Width;
            MaxY = Y + Height;
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
        public GeoBounds(double width, double height)
            : this(0, 0, width, height)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>GeoBounds</code> whose upper-left corner is 
         * specified by the {@link GeoPoint} argument, and
         * whose Width and Height are specified by the 
         * {@link GeoSize} argument. 
         * @param p a <code>GeoPoint</code> that is the upper-left corner of 
         * the <code>GeoBounds</code>
         * @param size a <code>GeoSize</code>, representing the 
         * Width and Height of the <code>GeoBounds</code>
         */
        public GeoBounds(GeoPoint p, GeoSize size)
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
        public GeoBounds(GeoPoint p)
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
        public GeoBounds(GeoSize size)
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
         * Returns the pixel coordinates of the center of the rectangular area.
         * @return the center point of the GeoBounds.
         */
        public GeoPoint Mid()
        {
            GeoPoint point = new GeoPoint((MinX + MaxX) / 2, (MinY + MaxY) / 2);
            return point;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the pixel coordinates of the upper left corner of the rectangular
         *  area.
         * @return the  upper left corner of the rectangular area.
         */
        public GeoPoint Min()
        {
            GeoPoint point = new GeoPoint(MinX, MinY);
            return point;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the pixel coordinates of the lower right corner of the 
         * rectangular area.
         * @return the  upper lower right of the rectangular area.
         */
        public GeoPoint Max()
        {
            GeoPoint point = new GeoPoint(MaxX, MaxY);
            return point;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Tests if the specified line segment Intersects the interior of this
         * <code>GeoBounds</code>.
         *
         * @param x1 the X coordinate of the start point of the specified
         *           line segment
         * @param y1 the Y coordinate of the start point of the specified
         *           line segment
         * @param X2 the X coordinate of the end point of the specified
         *           line segment
         * @param y2 the Y coordinate of the end point of the specified
         *           line segment
         * @return <code>true</code> if the specified line segment Intersects
         * the interior of this <code>GeoBounds</code>; <code>false</code>
         * otherwise.
         */
        public bool IntersectsLine(double x1, double y1, double x2, double y2)
        {
            int out1, out2;
            if ((out2 = Outcode(x2, y2)) == 0)
            {
                return true;
            }
            while ((out1 = Outcode(x1, y1)) != 0)
            {
                if ((out1 & out2) != 0)
                {
                    return false;
                }
                if ((out1 & (OUT_LEFT | OUT_RIGHT)) != 0)
                {
                    double tempX = GetX();
                    if ((out1 & OUT_RIGHT) != 0)
                    {
                        tempX += GetWidth();
                    }
                    y1 = y1 + (tempX - x1) * (y2 - y1) / (x2 - x1);
                    x1 = tempX;
                }
                else
                {
                    double tempY = GetY();
                    if ((out1 & OUT_BOTTOM) != 0)
                    {
                        tempY += GetHeight();
                    }
                    x1 = x1 + (tempY - y1) * (x2 - x1) / (y2 - y1);
                    y1 = tempY;
                }
            }
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines where the specified {@link GeoPoint} lies with
         * respect to this <code>GeoBounds</code>.
         * This method computes a binary OR of the appropriate mask values
         * indicating, for each side of this <code>GeoBounds</code>,
         * whether or not the specified <code>GeoPoint</code> is on the same
         * side of the edge as the rest of this <code>GeoBounds</code>.
         * @param p the specified <code>GeoPoint</code>
         * @return the logical OR of all appropriate out codes.
         */
        public int Outcode(GeoPoint p)
        {
            return Outcode(p.GetX(), p.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location and size of the outer bounds of this
         * <code>GeoBounds</code> to the specified rectangular values.
         *
         * @param X the X coordinate of the upper-left corner
         *          of this <code>GeoBounds</code>
         * @param Y the Y coordinate of the upper-left corner
         *          of this <code>GeoBounds</code>
         * @param w the Width of this <code>GeoBounds</code>
         * @param h the Height of this <code>GeoBounds</code>
         */
        public void SetFrame(double x, double y, double w, double h)
        {
            SetRect(x, y, w, h);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Contains given point.
         * @param X the X coordinate of the given point.
         * @param Y the Y coordinate of the given point.
         * @return true if this rectangle Contains given point.
         */
        public bool Contains(double x, double y)
        {
            double x0 = GetX();
            double y0 = GetY();
            return (x >= x0 &&
                    y >= y0 &&
                    x < x0 + GetWidth() &&
                    y < y0 + GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Intersects with given rectangle.
         * @param X the X coordinate of the other rectangle.
         * @param Y the Y coordinate of the other rectangle.
         * @param w the Width of the other rectangle.
         * @param h the Height of the other rectangle.
         * @return true, if they Intersect.
         */
        public bool Intersects(double x, double y, double w, double h)
        {
            if (IsEmpty() || w <= 0 || h <= 0)
            {
                return false;
            }
            double x0 = GetX();
            double y0 = GetY();
            return (x + w > x0 &&
                    y + h > y0 &&
                    x < x0 + GetWidth() &&
                    y < y0 + GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Contains given rectangle.
         * @param X the X coordinate of the other rectangle.
         * @param Y the Y coordinate of the other rectangle.
         * @param w the Width of the other rectangle.
         * @param h the Height of the other rectangle.
         * @return true, if it totally Contains other rectangle.
         */
        public bool Contains(double x, double y, double w, double h)
        {
            if (IsEmpty() || w <= 0 || h <= 0)
            {
                return false;
            }
            double x0 = GetX();
            double y0 = GetY();
            return (x >= x0 &&
                    y >= y0 &&
                    (x + w) <= x0 + GetWidth() &&
                    (y + h) <= y0 + GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Intersects the pair of specified source <code>GeoBounds</code>
         * objects and puts the result into the specified destination
         * <code>GeoBounds</code> object.  One of the source rectangles
         * can also be the destination to avoid creating a third GeoBounds
         * object, but in this case the original points of this source
         * rectangle will be overwritten by this method.
         * @param src1 the first of a pair of <code>GeoBounds</code>
         * objects to be intersected with each other
         * @param src2 the second of a pair of <code>GeoBounds</code>
         * objects to be intersected with each other
         * @param dest the <code>GeoBounds</code> that holds the
         * results of the intersection of <code>src1</code> and
         * <code>src2</code>
         */
        public static void Intersect(GeoBounds src1,
                GeoBounds src2,
                GeoBounds dest)
        {
            double x1 = Math.Max(src1.GetMinX(), src2.GetMinX());
            double y1 = Math.Max(src1.GetMinY(), src2.GetMinY());
            double x2 = Math.Min(src1.GetMaxX(), src2.GetMaxX());
            double y2 = Math.Min(src1.GetMaxY(), src2.GetMaxY());
            dest.SetFrame(x1, y1, x2 - x1, y2 - y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Unions the pair of source <code>GeoBounds</code> objects
         * and puts the result into the specified destination
         * <code>GeoBounds</code> object.  One of the source rectangles
         * can also be the destination to avoid creating a third GeoBounds
         * object, but in this case the original points of this source
         * rectangle will be overwritten by this method.
         * @param src1 the first of a pair of <code>GeoBounds</code>
         * objects to be combined with each other
         * @param src2 the second of a pair of <code>GeoBounds</code>
         * objects to be combined with each other
         * @param dest the <code>GeoBounds</code> that holds the
         * results of the Union of <code>src1</code> and
         * <code>src2</code>
         */
        public static void Union(GeoBounds src1,
                GeoBounds src2,
                GeoBounds dest)
        {
            double x1 = Math.Min(src1.GetMinX(), src2.GetMinX());
            double y1 = Math.Min(src1.GetMinY(), src2.GetMinY());
            double x2 = Math.Max(src1.GetMaxX(), src2.GetMaxX());
            double y2 = Math.Max(src1.GetMaxY(), src2.GetMaxY());
            dest.SetFrameFromDiagonal(x1, y1, x2, y2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smallest X coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the smallest X coordinate of the framing
         * 		rectangle of the <code>IShape</code>.
         */
        public double GetMinX()
        {
            return GetX();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smallest Y coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the smallest Y coordinate of the framing
         * 		rectangle of the <code>IShape</code>.
         */
        public double GetMinY()
        {
            return GetY();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the largest X coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the largest X coordinate of the framing
         * 		rectangle of the <code>IShape</code>.
         */
        public double GetMaxX()
        {
            return GetX() + GetWidth();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the largest Y coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the largest Y coordinate of the framing
         *		rectangle of the <code>IShape</code>.
         */
        public double GetMaxY()
        {
            return GetY() + GetHeight();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the center of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the X coordinate of the center of the framing rectangle
         * 		of the <code>IShape</code>.
         */
        public double GetCenterX()
        {
            return GetX() + GetWidth() / 2.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the center of the framing
         * rectangle of the <code>IShape</code> in <code>double</code>
         * precision.
         * @return the Y coordinate of the center of the framing rectangle
         * 		of the <code>IShape</code>.
         */
        public double GetCenterY()
        {
            return GetY() + GetHeight() / 2.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the framing {@link GeoBounds}
         * that defines the overall shape of this object.
         * @return a <code>GeoBounds</code>, specified in
         * <code>double</code> coordinates.
         */
        public GeoBounds GetFrame()
        {
            return new GeoBounds(GetX(), GetY(), GetWidth(), GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location and size of the framing rectangle of this
         * <code>IShape</code> to the specified {@link GeoPoint} and
         * {@link GeoSize}, respectively.  The framing rectangle is used
         * by the subclasses of <code>RectangularShape</code> to define
         * their geometry.
         * @param loc the specified <code>GeoPoint</code>
         * @param size the specified <code>GeoSize</code>
         */
        public void SetFrame(GeoPoint loc, GeoSize size)
        {
            SetFrame(loc.GetX(), loc.GetY(), size.GetWidth(), size.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the framing rectangle of this <code>IShape</code> to
         * be the specified <code>GeoBounds</code>.  The framing rectangle is
         * used by the subclasses of <code>RectangularShape</code> to define
         * their geometry.
         * @param r the specified <code>GeoBounds</code>
         */
        public void SetFrame(GeoBounds r)
        {
            SetFrame(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the diagonal of the framing rectangle of this <code>IShape</code>
         * based on the two specified coordinates.  The framing rectangle is
         * used by the subclasses of <code>RectangularShape</code> to define
         * their geometry.
         *
         * @param x1 the X coordinate of the start point of the specified diagonal
         * @param y1 the Y coordinate of the start point of the specified diagonal
         * @param X2 the X coordinate of the end point of the specified diagonal
         * @param y2 the Y coordinate of the end point of the specified diagonal
         */
        public void SetFrameFromDiagonal(double x1, double y1,
                double x2, double y2)
        {
            if (x2 < x1)
            {
                double t = x1;
                x1 = x2;
                x2 = t;
            }
            if (y2 < y1)
            {
                double t = y1;
                y1 = y2;
                y2 = t;
            }
            SetFrame(x1, y1, x2 - x1, y2 - y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the diagonal of the framing rectangle of this <code>IShape</code>
         * based on two specified <code>GeoPoint</code> objects.  The framing
         * rectangle is used by the subclasses of <code>RectangularShape</code>
         * to define their geometry.
         *
         * @param p1 the start <code>GeoPoint</code> of the specified diagonal
         * @param p2 the end <code>GeoPoint</code> of the specified diagonal
         */
        public void SetFrameFromDiagonal(GeoPoint p1, GeoPoint p2)
        {
            SetFrameFromDiagonal(p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the framing rectangle of this <code>IShape</code>
         * based on the specified center point coordinates and corner point
         * coordinates.  The framing rectangle is used by the subclasses of
         * <code>RectangularShape</code> to define their geometry.
         *
         * @param centerX the X coordinate of the specified center point
         * @param centerY the Y coordinate of the specified center point
         * @param cornerX the X coordinate of the specified corner point
         * @param cornerY the Y coordinate of the specified corner point
         */
        public void SetFrameFromCenter(double centerX, double centerY,
                double cornerX, double cornerY)
        {
            double halfW = Math.Abs(cornerX - centerX);
            double halfH = Math.Abs(cornerY - centerY);
            SetFrame(centerX - halfW, centerY - halfH, halfW * 2.0, halfH * 2.0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the framing rectangle of this <code>IShape</code> based on a
         * specified center <code>GeoPoint</code> and corner
         * <code>GeoPoint</code>.  The framing rectangle is used by the subclasses
         * of <code>RectangularShape</code> to define their geometry.
         * @param center the specified center <code>GeoPoint</code>
         * @param corner the specified corner <code>GeoPoint</code>
         */
        public void SetFrameFromCenter(GeoPoint center, GeoPoint corner)
        {
            SetFrameFromCenter(center.GetX(), center.GetY(),
                    corner.GetX(), corner.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Contains given point.
         * @param p the point to be checked
         * @return true,it Contains given point.
         */
        public bool Contains(GeoPoint p)
        {
            return Contains(p.GetX(), p.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Intersects given rectangle.
         * @param r the rectangle to be checked.
         * @return true, it Intersects given rectangle.
         */
        public bool Intersects(GeoBounds r)
        {
            return Intersects(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * check if this rectangle Contains given rectangle.
         * @param r the rectangle to be checked.
         * @return true, it totally Contains given rectangle.
         */
        public bool Contains(GeoBounds r)
        {
            return Contains(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return a new geo bounds of this rectangle.
         * @return a new copy of this geo bound.
         */
        public GeoBounds GetBounds()
        {
            double tempWidth = GetWidth();
            double tempHeight = GetHeight();
            if (tempWidth < 0 || tempHeight < 0)
            {
                return new GeoBounds();
            }
            double tempX = GetX();
            double tempY = GetY();
            double x1 = Math.Floor(tempX);
            double y1 = Math.Floor(tempY);
            double x2 = Math.Ceiling(tempX + tempWidth);
            double y2 = Math.Ceiling(tempY + tempHeight);
            return new GeoBounds((int)x1, (int)y1,
                    (int)(x2 - x1), (int)(y2 - y1));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////

        /**
         * Adds a point, specified by the double precision arguments
         * <code>newx</code> and <code>newy</code>, to this
         * <code>GeoBounds</code>.  The resulting <code>GeoBounds</code>
         * is the smallest <code>GeoBounds</code> that
         * Contains both the original <code>GeoBounds</code> and the
         * specified point.
         * <p>
         * After adding a point, a call to <code>Contains</code> with the
         * added point as an argument does not necessarily return
         * <code>true</code>. The <code>Contains</code> method does not
         * return <code>true</code> for points on the right or bottom
         * edges of a rectangle. Therefore, if the added point falls on
         * the left or bottom edge of the enlarged rectangle,
         * <code>Contains</code> returns <code>false</code> for that point.
         * @param newx the X coordinate of the new point
         * @param newy the Y coordinate of the new point
         */
        public void Add(double newx, double newy)
        {
            double x1 = Math.Min(GetMinX(), newx);
            double x2 = Math.Max(GetMaxX(), newx);
            double y1 = Math.Min(GetMinY(), newy);
            double y2 = Math.Max(GetMaxY(), newy);
            SetRect(x1, y1, x2 - x1, y2 - y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds the <code>GeoPoint</code> object <code>pt</code> to this
         * <code>GeoBounds</code>.
         * The resulting <code>GeoBounds</code> is the smallest
         * <code>GeoBounds</code> that Contains both the original
         * <code>GeoBounds</code> and the specified <code>GeoPoint</code>.
         * <p>
         * After adding a point, a call to <code>Contains</code> with the
         * added point as an argument does not necessarily return
         * <code>true</code>. The <code>Contains</code>
         * method does not return <code>true</code> for points on the right
         * or bottom edges of a rectangle. Therefore, if the added point falls
         * on the left or bottom edge of the enlarged rectangle,
         * <code>Contains</code> returns <code>false</code> for that point.
         * @param     pt the new <code>GeoPoint</code> to Add to this
         * <code>GeoBounds</code>.
         */
        public void Add(GeoPoint pt)
        {
            Add(pt.GetX(), pt.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Adds a <code>GeoBounds</code> object to this
         * <code>GeoBounds</code>.  The resulting <code>GeoBounds</code>
         * is the Union of the two <code>GeoBounds</code> objects.
         * @param r the <code>GeoBounds</code> to Add to this
         * <code>GeoBounds</code>.
         */
        public void Add(GeoBounds r)
        {
            double x1 = Math.Min(GetMinX(), r.GetMinX());
            double x2 = Math.Max(GetMaxX(), r.GetMaxX());
            double y1 = Math.Min(GetMinY(), r.GetMinY());
            double y2 = Math.Max(GetMaxY(), r.GetMaxY());
            SetRect(x1, y1, x2 - x1, y2 - y1);
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified <code>object</code> is
         * equal to this <code>GeoBounds</code>.  The specified
         * <code>object</code> is equal to this <code>GeoBounds</code>
         * if it is an instance of <code>GeoBounds</code> and if its
         * location and size are the same as this <code>GeoBounds</code>.
         * @param obj an <code>object</code> to be compared with this
         * <code>GeoBounds</code>.
         * @return     <code>true</code> if <code>obj</code> is an instance
         *                     of <code>GeoBounds</code> and has
         *                     the same values; <code>false</code> otherwise.
         */
        public new bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is GeoBounds)
            {
                GeoBounds r2D = (GeoBounds)obj;
                return ((GetX() == r2D.GetX()) &&
                        (GetY() == r2D.GetY()) &&
                        (GetWidth() == r2D.GetWidth()) &&
                        (GetHeight() == r2D.GetHeight()));
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
         * Sets the bounding <code>GeoSize</code> of this <code>GeoSize</code>
         * to match the specified <code>GeoSize</code>.
         * <p>
         * This method is included for completeness, to parallel the
         * <code>SetBounds</code> method of <code>Component</code>.
         * @param r the specified <code>GeoSize</code>
         */
        public void SetBounds(GeoBounds r)
        {
            SetBounds(r.X, r.Y, r.Width, r.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the bounding <code>GeoSize</code> of this
         * <code>GeoSize</code> to the specified
         * <code>X</code>, <code>Y</code>, <code>Width</code>,
         * and <code>Height</code>.
         * <p>
         * This method is included for completeness, to parallel the
         * <code>SetBounds</code> method of <code>Component</code>.
         * @param X the new X coordinate for the upper-left
         *                    corner of this <code>GeoSize</code>
         * @param Y the new Y coordinate for the upper-left
         *                    corner of this <code>GeoSize</code>
         * @param Width the new Width for this <code>GeoSize</code>
         * @param Height the new Height for this <code>GeoSize</code>
         */
        public void SetBounds(double x, double y, double width, double height)
        {
            Reshape(x, y, width, height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the bounding <code>GeoSize</code> of this
         * <code>GeoSize</code> to the specified
         * <code>X</code>, <code>Y</code>, <code>Width</code>,
         * and <code>Height</code>.
         * <p>
         * @param X the new X coordinate for the upper-left
         *                    corner of this <code>GeoSize</code>
         * @param Y the new Y coordinate for the upper-left
         *                    corner of this <code>GeoSize</code>
         * @param Width the new Width for this <code>GeoSize</code>
         * @param Height the new Height for this <code>GeoSize</code>
         */
        private void Reshape(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
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
         * Check whether the current rectangle entirely Contains other rectangle.
         * @param other the other rectangle.
         * @return true if the passed rectangular area is entirely contained 
         * in this rectangular area.
         */
        public bool ContainsBounds(GeoBounds other)
        {
            SetBounds(MinX, MinY, MaxX - MinX, MaxY - MinY);
            return Contains(other.MinX, other.MinY,
                    other.MaxX - other.MinX,
                    other.MaxY - other.MinY);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check whether the current rectangle Contains given point.
         * @param point the point object.
         * @return true if the rectangular area (inclusively) Contains the pixel 
         * coordinates. 
         */
        public bool ContainsPoint(GeoPoint point)
        {
            SetBounds(MinX, MinY, MaxX - MinX, MaxY - MinY);
            return Contains(point.X, point.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Enlarges this box so that the point is also contained in this box.
         * @param point the point object.
         */
        public void Extend(GeoPoint point)
        {
            SetBounds(MinX, MinY, MaxX - MinX, MaxY - MinY);
            Add(point.X, point.Y);
            MinX = X;
            MinY = Y;
            MaxX = X + Width;
            MaxY = Y + Height;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a <code>string</code> representing this 
         * <code>GBounds</code> and its values.
         * @return Returns a string that Contains the coordinates of the upper left 
         * and the lower right corner points of the box, in this order, separated 
         * by comma, surrounded by parentheses.
         */
        public override string ToString()
        {
            return MinX + "," + MinY + "," + MaxX + "," + MaxY;
        }
    }

}
