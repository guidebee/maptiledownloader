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
using MapDigit.Util;

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
     * This class defines an arc specified in {@code Double} precision.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Arc : RectangularShape
    {

        /**
         * The closure type for an open arc with no path segments
         * connecting the two ends of the arc segment.
         */
        public const int OPEN = 0;

        /**
         * The closure type for an arc closed by drawing a straight
         * line segment from the start of the arc segment to the end of the
         * arc segment.
         */
        public const int CHORD = 1;

        /**
         * The closure type for an arc closed by drawing straight line
         * segments from the start of the arc segment to the center
         * of the full ellipse and from that point to the end of the arc segment.
         */
        public const int PIE = 2;

        private int _type;

        /**
         * The X coordinate of the upper-left corner of the framing
         * rectangle of the arc.
         */
        public double X;

        /**
         * The Y coordinate of the upper-left corner of the framing
         * rectangle of the arc.
         */
        public double Y;

        /**
         * The overall width of the full ellipse of which this arc is
         * a partial section (not considering the angular extents).
         */
        public double Width;

        /**
         * The overall height of the full ellipse of which this arc is
         * a partial section (not considering the angular extents).
         */
        public double Height;

        /**
         * The starting angle of the arc in degrees.
         */
        public double Start;

        /**
         * The angular extent of the arc in degrees.
         */
        public double Extent;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * This is an abstract class that cannot be instantiated directly.
         * Type-specific implementation subclasses are available for
         * instantiation and provide a number of formats for storing
         * the information necessary to satisfy the various accessor
         * methods below.
         * <p>
         * This constructor creates an object with a default closure
         * type of {@link #OPEN}.  It is provided only to enable
         * serialization of subclasses.
         */
        public Arc()
            : this(OPEN)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * This is an abstract class that cannot be instantiated directly.
         * Type-specific implementation subclasses are available for
         * instantiation and provide a number of formats for storing
         * the information necessary to satisfy the various accessor
         * methods below.
         *
         * @param type The closure type of this arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public Arc(int type)
        {
            SetArcType(type);
        }

        ///////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new arc, initialized to the specified location,
         * size, angular extents, and closure type.
         *
         * @param ellipseBounds The framing rectangle that defines the
         * outer boundary of the full ellipse of which this arc is a
         * partial section.
         * @param start The starting angle of the arc in degrees.
         * @param extent The angular extent of the arc in degrees.
         * @param type The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public Arc(Rectangle ellipseBounds,
                      double start, double extent, int type)
            : this(type)
        {

            X = ellipseBounds.GetX();
            Y = ellipseBounds.GetY();
            Width = ellipseBounds.GetWidth();
            Height = ellipseBounds.GetHeight();
            Start = start;
            Extent = extent;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * Note that the arc
         * <a href="Arc.html#inscribes">partially inscribes</a>
         * the framing rectangle of this {@code RectangularShape}.
         */
        public override int GetX()
        {
            return (int)(X + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * Note that the arc
         * <a href="Arc.html#inscribes">partially inscribes</a>
         * the framing rectangle of this {@code RectangularShape}.
         */
        public override int GetY()
        {
            return (int)(Y + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * Note that the arc
         * <a href="Arc.html#inscribes">partially inscribes</a>
         * the framing rectangle of this {@code RectangularShape}.
         */
        public override int GetWidth()
        {
            return (int)(Width + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * Note that the arc
         * <a href="Arc.html#inscribes">partially inscribes</a>
         * the framing rectangle of this {@code RectangularShape}.
         */
        public override int GetHeight()
        {
            return (int)(Height + .5);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the start angle.
         * @return the start angle.
         */
        public double GetAngleStart()
        {
            return Start;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the extent angle.
         * @return the angle extent.
         */
        public double GetAngleExtent()
        {
            return Extent;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override bool IsEmpty()
        {
            return (Width <= 0.0 || Height <= 0.0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location, size, angular extents, and closure type of
         * this arc to the specified double values.
         *
         * @param x The X coordinate of the upper-left corner of the arc.
         * @param y The Y coordinate of the upper-left corner of the arc.
         * @param w The overall width of the full ellipse of which
         *          this arc is a partial section.
         * @param h The overall height of the full ellipse of which
         *          this arc is a partial section.
         * @param angSt The starting angle of the arc in degrees.
         * @param angExt The angular extent of the arc in degrees.
         * @param closure The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public void SetArc(double x, double y, double w, double h,
                           double angSt, double angExt, int closure)
        {
            SetArcType(closure);
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Start = angSt;
            Extent = angExt;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the starting angle of this arc to the specified double
         * Value.
         *
         * @param angSt The starting angle of the arc in degrees.
         */
        public void SetAngleStart(double angSt)
        {
            Start = angSt;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the angular extent of this arc to the specified double
         * Value.
         *
         * @param angExt The angular extent of the arc in degrees.
         */
        public void SetAngleExtent(double angExt)
        {
            Extent = angExt;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a <code>Rectangle2D</code> of the appropriate precision
         * to hold the parameters calculated to be the framing rectangle
         * of this arc.
         *
         * @param x The X coordinate of the upper-left corner of the
         * framing rectangle.
         * @param y The Y coordinate of the upper-left corner of the
         * framing rectangle.
         * @param w The width of the framing rectangle.
         * @param h The height of the framing rectangle.
         * @return a <code>Rectangle2D</code> that is the framing rectangle
         *     of this arc.
         */
        protected static Rectangle MakeBounds(double x, double y,
                                         double w, double h)
        {
            return new Rectangle((int)(x + .5),
                    (int)(y + .5),
                    (int)(w + .5),
                    (int)(h + .5));

        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new arc, initialized to the specified location,
         * size, angular extents, and closure type.
         *
         * @param x The X coordinate of the upper-left corner
         *          of the arc's framing rectangle.
         * @param y The Y coordinate of the upper-left corner
         *          of the arc's framing rectangle.
         * @param w The overall width of the full ellipse of which this
         *          arc is a partial section.
         * @param h The overall height of the full ellipse of which this
         *          arc is a partial section.
         * @param start The starting angle of the arc in degrees.
         * @param extent The angular extent of the arc in degrees.
         * @param type The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public Arc(double x, double y, double w, double h,
                      double start, double extent, int type)
            : this(type)
        {

            X = x;
            Y = y;
            Width = w;
            Height = h;
            Start = start;
            Extent = extent;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the arc closure type of the arc: {@link #OPEN},
         * {@link #CHORD}, or {@link #PIE}.
         * @return One of the integer constant closure types defined
         * in this class.
         */
        public int GetArcType()
        {
            return _type;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the starting point of the arc.  This point is the
         * intersection of the ray from the center defined by the
         * starting angle and the elliptical boundary of the arc.
         *
         * @return A <CODE>Point</CODE> object representing the
         * x,y coordinates of the starting point of the arc.
         */
        public Point GetStartPoint()
        {
            double angle = MathEx.ToRadians(-GetAngleStart());
            double xp = GetX() + (MathEx.Cos(angle) * 0.5 + 0.5) * GetWidth();
            double yp = GetY() + (MathEx.Sin(angle) * 0.5 + 0.5) * GetHeight();
            return new Point((int)(xp + .5), (int)(yp + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the ending point of the arc.  This point is the
         * intersection of the ray from the center defined by the
         * starting angle plus the angular extent of the arc and the
         * elliptical boundary of the arc.
         *
         * @return A <CODE>Point</CODE> object representing the
         * x,y coordinates  of the ending point of the arc.
         */
        public Point GetEndPoint()
        {
            double angle = MathEx.ToRadians(-GetAngleStart() - GetAngleExtent());
            double xp = GetX() + (MathEx.Cos(angle) * 0.5 + 0.5) * GetWidth();
            double yp = GetY() + (MathEx.Sin(angle) * 0.5 + 0.5) * GetHeight();
            return new Point((int)(xp + .5), (int)(yp + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location, size, angular extents, and closure type of
         * this arc to the specified values.
         *
         * @param loc The <CODE>Point</CODE> representing the coordinates of
         * the upper-left corner of the arc.
         * @param size The <CODE>Dimension</CODE> representing the width
         * and height of the full ellipse of which this arc is
         * a partial section.
         * @param angSt The starting angle of the arc in degrees.
         * @param angExt The angular extent of the arc in degrees.
         * @param closure The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public void SetArc(Point loc, Dimension size,
                   double angSt, double angExt, int closure)
        {
            SetArc(loc.GetX(), loc.GetY(), size.GetWidth(), size.GetHeight(),
                   angSt, angExt, closure);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // 13JUN2009  -------------------  -------------      ----------------------
        // 08NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location, size, angular extents, and closure type of
         * this arc to the specified values.
         *
         * @param rect The framing rectangle that defines the
         * outer boundary of the full ellipse of which this arc is a
         * partial section.
         * @param angSt The starting angle of the arc in degrees.
         * @param angExt The angular extent of the arc in degrees.
         * @param closure The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public void SetArc(Rectangle rect, double angSt, double angExt,
                   int closure)
        {
            SetArc(rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight(),
                   angSt, angExt, closure);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this arc to be the same as the specified arc.
         *
         * @param a The <CODE>Arc</CODE> to use to set the arc's values.
         */
        public void SetArc(Arc a)
        {
            SetArc(a.GetX(), a.GetY(), a.GetWidth(), a.GetHeight(),
                   a.GetAngleStart(), a.GetAngleExtent(), a._type);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the position, bounds, angular extents, and closure type of
         * this arc to the specified values. The arc is defined by a center
         * point and a radius rather than a framing rectangle for the full ellipse.
         *
         * @param x The X coordinate of the center of the arc.
         * @param y The Y coordinate of the center of the arc.
         * @param radius The radius of the arc.
         * @param angSt The starting angle of the arc in degrees.
         * @param angExt The angular extent of the arc in degrees.
         * @param closure The closure type for the arc:
         * {@link #OPEN}, {@link #CHORD}, or {@link #PIE}.
         */
        public void SetArcByCenter(double x, double y, double radius,
                       double angSt, double angExt, int closure)
        {
            SetArc(x - radius, y - radius, radius * 2.0, radius * 2.0,
                   angSt, angExt, closure);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the position, bounds, and angular extents of this arc to the
         * specified Value. The starting angle of the arc is tangent to the
         * line specified by points (p1, p2), the ending angle is tangent to
         * the line specified by points (p2, p3), and the arc has the
         * specified radius.
         *
         * @param p1 The first point that defines the arc. The starting
         * angle of the arc is tangent to the line specified by points (p1, p2).
         * @param p2 The second point that defines the arc. The starting
         * angle of the arc is tangent to the line specified by points (p1, p2).
         * The ending angle of the arc is tangent to the line specified by
         * points (p2, p3).
         * @param p3 The third point that defines the arc. The ending angle
         * of the arc is tangent to the line specified by points (p2, p3).
         * @param radius The radius of the arc.
         */
        public void SetArcByTangent(Point p1, Point p2, Point p3,
                    double radius)
        {
            double ang1 = MathEx.Atan2(p1.GetY() - p2.GetY(),
                         p1.GetX() - p2.GetX());
            double ang2 = MathEx.Atan2(p3.GetY() - p2.GetY(),
                         p3.GetX() - p2.GetX());
            double diff = ang2 - ang1;
            if (diff > MathEx.PI)
            {
                ang2 -= MathEx.PI * 2.0;
            }
            else if (diff < -MathEx.PI)
            {
                ang2 += MathEx.PI * 2.0;
            }
            double bisect = (ang1 + ang2) / 2.0;
            double theta = MathEx.Abs(ang2 - bisect);
            double dist = radius / MathEx.Sin(theta);
            double xp = p2.GetX() + dist * MathEx.Cos(bisect);
            double yp = p2.GetY() + dist * MathEx.Sin(bisect);
            // REMIND: This needs some work...
            if (ang1 < ang2)
            {
                ang1 -= MathEx.PI / 2.0;
                ang2 += MathEx.PI / 2.0;
            }
            else
            {
                ang1 += MathEx.PI / 2.0;
                ang2 -= MathEx.PI / 2.0;
            }
            ang1 = MathEx.ToDegrees(-ang1);
            ang2 = MathEx.ToDegrees(-ang2);
            diff = ang2 - ang1;
            if (diff < 0)
            {
                diff += 360;
            }
            else
            {
                diff -= 360;
            }
            SetArcByCenter(xp, yp, radius, ang1, diff, _type);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the starting angle of this arc to the angle that the
         * specified point defines relative to the center of this arc.
         * The angular extent of the arc will remain the same.
         *
         * @param p The <CODE>Point</CODE> that defines the starting angle.
         */
        public void SetAngleStart(Point p)
        {
            // Bias the dx and dy by the height and width of the oval.
            double dx = GetHeight() * (p.GetX() - GetCenterX());
            double dy = GetWidth() * (p.GetY() - GetCenterY());
            SetAngleStart(-MathEx.ToDegrees(MathEx.Atan2(dy, dx)));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the starting angle and angular extent of this arc using two
         * sets of coordinates. The first set of coordinates is used to
         * determine the angle of the starting point relative to the arc's
         * center. The second set of coordinates is used to determine the
         * angle of the end point relative to the arc's center.
         * The arc will always be non-empty and extend counterclockwise
         * from the first point around to the second point.
         *
         * @param x1 The X coordinate of the arc's starting point.
         * @param y1 The Y coordinate of the arc's starting point.
         * @param X2 The X coordinate of the arc's ending point.
         * @param y2 The Y coordinate of the arc's ending point.
         */
        public void SetAngles(double x1, double y1, double x2, double y2)
        {
            double xp = GetCenterX();
            double yp = GetCenterY();
            double w = GetWidth();
            double h = GetHeight();
            // Note: reversing the Y equations negates the angle to adjust
            // for the upside down coordinate system.
            // Also we should bias atans by the height and width of the oval.
            double ang1 = MathEx.Atan2(w * (yp - y1), h * (x1 - xp));
            double ang2 = MathEx.Atan2(w * (yp - y2), h * (x2 - xp));
            ang2 -= ang1;
            if (ang2 <= 0.0)
            {
                ang2 += MathEx.PI * 2.0;
            }
            SetAngleStart(MathEx.ToDegrees(ang1));
            SetAngleExtent(MathEx.ToDegrees(ang2));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the starting angle and angular extent of this arc using
         * two points. The first point is used to determine the angle of
         * the starting point relative to the arc's center.
         * The second point is used to determine the angle of the end point
         * relative to the arc's center.
         * The arc will always be non-empty and extend counterclockwise
         * from the first point around to the second point.
         *
         * @param p1 The <CODE>Point</CODE> that defines the arc's
         * starting point.
         * @param p2 The <CODE>Point</CODE> that defines the arc's
         * ending point.
         */
        public void SetAngles(Point p1, Point p2)
        {
            SetAngles(p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the closure type of this arc to the specified Value:
         * <CODE>OPEN</CODE>, <CODE>CHORD</CODE>, or <CODE>PIE</CODE>.
         *
         * @param type The integer constant that represents the closure
         * type of this arc: {@link #OPEN}, {@link #CHORD}, or
         * {@link #PIE}.
         *
         * @throws IllegalArgumentException if <code>type</code> is not
         * 0, 1, or 2.+
         */
        public void SetArcType(int type)
        {
            if (type < OPEN || type > PIE)
            {
                throw new ArgumentException("invalid type for Arc: " + type);
            }
            _type = type;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // 13JUN2009  -------------------  -------------      ----------------------
        // 08NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         * Note that the arc
         * <a href="Arc.html#inscribes">partially inscribes</a>
         * the framing rectangle of this {@code RectangularShape}.
         */
        public override void SetFrame(int x, int y, int w, int h)
        {
            SetArc(x, y, w, h, GetAngleStart(), GetAngleExtent(), _type);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the high-precision framing rectangle of the arc.  The framing
         * rectangle contains only the part of this <code>Arc</code> that is
         * in between the starting and ending angles and contains the pie
         * wedge, if this <code>Arc</code> has a <code>PIE</code> closure type.
         * <p>
         * This method differs from the
         * {@link RectangularShape#getBounds() getBounds} in that the
         * <code>getBounds</code> method only returns the bounds of the
         * enclosing ellipse of this <code>Arc</code> without considering
         * the starting and ending angles of this <code>Arc</code>.
         *
         * @return the <CODE>Rectangle</CODE> that represents the arc's
         * framing rectangle.
         */
        public override Rectangle GetBounds()
        {
            if (IsEmpty())
            {
                return MakeBounds(GetX(), GetY(), GetWidth(), GetHeight());
            }
            double x1, y1, x2, y2;
            if (GetArcType() == PIE)
            {
                x1 = y1 = x2 = y2 = 0.0;
            }
            else
            {
                x1 = y1 = 1.0;
                x2 = y2 = -1.0;
            }
            double angle = 0.0;
            for (int i = 0; i < 6; i++)
            {
                if (i < 4)
                {
                    // 0-3 are the four quadrants
                    angle += 90.0;
                    if (!ContainsAngle(angle))
                    {
                        continue;
                    }
                }
                else if (i == 4)
                {
                    // 4 is start angle
                    angle = GetAngleStart();
                }
                else
                {
                    // 5 is end angle
                    angle += GetAngleExtent();
                }
                double rads = MathEx.ToRadians(-angle);
                double xe = MathEx.Cos(rads);
                double ye = MathEx.Sin(rads);
                x1 = MathEx.Min(x1, xe);
                y1 = MathEx.Min(y1, ye);
                x2 = MathEx.Max(x2, xe);
                y2 = MathEx.Max(y2, ye);
            }
            double w = GetWidth();
            double h = GetHeight();
            x2 = (x2 - x1) * 0.5 * w;
            y2 = (y2 - y1) * 0.5 * h;
            x1 = GetX() + (x1 * 0.5 + 0.5) * w;
            y1 = GetY() + (y1 * 0.5 + 0.5) * h;
            return MakeBounds(x1, y1, x2, y2);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /*
         * Normalizes the specified angle into the range -180 to 180.
         */
        internal static double NormalizeDegrees(double angle)
        {
            if (angle > 180.0)
            {
                if (angle <= (180.0 + 360.0))
                {
                    angle = angle - 360.0;
                }
                else
                {
                    angle = MathEx.IEEEremainder(angle, 360.0);
                    // IEEEremainder can return -180 here for some input values...
                    if (angle == -180.0)
                    {
                        angle = 180.0;
                    }
                }
            }
            else if (angle <= -180.0)
            {
                if (angle > (-180.0 - 360.0))
                {
                    angle = angle + 360.0;
                }
                else
                {
                    angle = MathEx.IEEEremainder(angle, 360.0);
                    // IEEEremainder can return -180 here for some input values...
                    if (angle == -180.0)
                    {
                        angle = 180.0;
                    }
                }
            }
            return angle;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified angle is within the
         * angular extents of the arc.
         *
         * @param angle The angle to test.
         *
         * @return <CODE>true</CODE> if the arc contains the angle,
         * <CODE>false</CODE> if the arc doesn't contain the angle.
         */
        public bool ContainsAngle(double angle)
        {
            double angExt = GetAngleExtent();
            bool backwards = (angExt < 0.0);
            if (backwards)
            {
                angExt = -angExt;
            }
            if (angExt >= 360.0)
            {
                return true;
            }
            angle = NormalizeDegrees(angle) - NormalizeDegrees(GetAngleStart());
            if (backwards)
            {
                angle = -angle;
            }
            if (angle < 0.0)
            {
                angle += 360.0;
            }


            return (angle >= 0.0) && (angle < angExt);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified point is inside the boundary
         * of the arc.
         *
         * @param x The X coordinate of the point to test.
         * @param y The Y coordinate of the point to test.
         *
         * @return <CODE>true</CODE> if the point lies within the bound of
         * the arc, <CODE>false</CODE> if the point lies outside of the
         * arc's bounds.
         */
        public override bool Contains(int x, int y)
        {
            // Normalize the coordinates compared to the ellipse
            // having a center at 0,0 and a radius of 0.5.
            double ellw = GetWidth();
            if (ellw <= 0.0)
            {
                return false;
            }
            double normx = (x - GetX()) / ellw - 0.5;
            double ellh = GetHeight();
            if (ellh <= 0.0)
            {
                return false;
            }
            double normy = (y - GetY()) / ellh - 0.5;
            double distSq = (normx * normx + normy * normy);
            if (distSq >= 0.25)
            {
                return false;
            }
            double angExt = MathEx.Abs(GetAngleExtent());
            if (angExt >= 360.0)
            {
                return true;
            }
            bool inarc = ContainsAngle(-MathEx.ToDegrees(MathEx.Atan2(normy,
                                         normx)));
            if (_type == PIE)
            {
                return inarc;
            }
            // CHORD and OPEN behave the same way
            if (inarc)
            {
                if (angExt >= 180.0)
                {
                    return true;
                }
                // point must be outside the "pie triangle"
            }
            else
            {
                if (angExt <= 180.0)
                {
                    return false;
                }
                // point must be inside the "pie triangle"
            }
            // The point is inside the pie triangle iff it is on the same
            // side of the line connecting the ends of the arc as the center.
            double angle = MathEx.ToRadians(-GetAngleStart());
            double x1 = MathEx.Cos(angle);
            double y1 = MathEx.Sin(angle);
            angle += MathEx.ToRadians(-GetAngleExtent());
            double x2 = MathEx.Cos(angle);
            double y2 = MathEx.Sin(angle);
            bool inside = (Line.RelativeCCW((int)(x1 + .5),
                         (int)(y1 + .5),
                         (int)(x2 + .5),
                         (int)(y2 + .5),
                         (int)(2 * normx + .5),
                         (int)(2 * normy + .5)) *
                      Line.RelativeCCW((int)(x1 + .5),
                                  (int)(y1 + .5),
                                  (int)(x2 + .5),
                                  (int)(y2 + .5), 0, 0) >= 0);
            return inarc ? !inside : inside;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the interior of the arc intersects
         * the interior of the specified rectangle.
         *
         * @param x The X coordinate of the rectangle's upper-left corner.
         * @param y The Y coordinate of the rectangle's upper-left corner.
         * @param w The width of the rectangle.
         * @param h The height of the rectangle.
         *
         * @return <CODE>true</CODE> if the arc intersects the rectangle,
         * <CODE>false</CODE> if the arc doesn't intersect the rectangle.
         */
        public override bool Intersects(int x, int y, int w, int h)
        {

            double aw = GetWidth();
            double ah = GetHeight();

            if (w <= 0 || h <= 0 || aw <= 0 || ah <= 0)
            {
                return false;
            }
            double ext = GetAngleExtent();
            if (ext == 0)
            {
                return false;
            }

            double ax = GetX();
            double ay = GetY();
            double axw = ax + aw;
            double ayh = ay + ah;
            double xw = x + w;
            double yh = y + h;

            // check bbox
            if (x >= axw || y >= ayh || xw <= ax || yh <= ay)
            {
                return false;
            }

            // extract necessary data
            double axc = GetCenterX();
            double ayc = GetCenterY();
            Point sp = GetStartPoint();
            Point ep = GetEndPoint();
            double sx = sp.GetX();
            double sy = sp.GetY();
            double ex = ep.GetX();
            double ey = ep.GetY();

            /*
             * Try to catch rectangles that intersect arc in areas
             * outside of rectagle with left top corner coordinates
             * (Min(center x, start point x, end point x),
             *  Min(center y, start point y, end point y))
             * and rigth bottom corner coordinates
             * (Max(center x, start point x, end point x),
             *  Max(center y, start point y, end point y)).
             * So we'll check axis segments outside of rectangle above.
             */
            if (ayc >= y && ayc <= yh)
            { // 0 and 180
                if ((sx < xw && ex < xw && axc < xw &&
                     axw > x && ContainsAngle(0)) ||
                    (sx > x && ex > x && axc > x &&
                     ax < xw && ContainsAngle(180)))
                {
                    return true;
                }
            }
            if (axc >= x && axc <= xw)
            { // 90 and 270
                if ((sy > y && ey > y && ayc > y &&
                     ay < yh && ContainsAngle(90)) ||
                    (sy < yh && ey < yh && ayc < yh &&
                     ayh > y && ContainsAngle(270)))
                {
                    return true;
                }
            }

            /*
             * For PIE we should check intersection with pie slices;
             * also we should do the same for arcs with extent is greater
             * than 180, because we should cover case of rectangle, which
             * situated between center of arc and chord, but does not
             * intersect the chord.
             */
            Rectangle rect = new Rectangle((int)(x + .5),
                        (int)(y + .5),
                        (int)(w + .5),
                        (int)(h + .5));
            if (_type == PIE || MathEx.Abs(ext) > 180)
            {
                // for PIE: try to find intersections with pie slices
                if (rect.IntersectsLine((int)(axc + .5),
                            (int)(ayc + .5),
                            (int)(sx + .5),
                            (int)(sy + .5)) ||
                rect.IntersectsLine((int)(axc + .5),
                        (int)(ayc + .5),
                        (int)(ex + .5),
                        (int)(ey + .5)))
                {
                    return true;
                }
            }
            else
            {
                // for CHORD and OPEN: try to find intersections with chord
                if (rect.IntersectsLine((int)(sx + .5),
                            (int)(sy + .5), (int)(ex + .5),
                            (int)(ey + .5)))
                {
                    return true;
                }
            }

            // finally check the rectangle corners inside the arc
            if (Contains(x, y) || Contains(x + w, y) ||
                Contains(x, y + h) || Contains(x + w, y + h))
            {
                return true;
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
         * Determines whether or not the interior of the arc entirely contains
         * the specified rectangle.
         *
         * @param x The X coordinate of the rectangle's upper-left corner.
         * @param y The Y coordinate of the rectangle's upper-left corner.
         * @param w The width of the rectangle.
         * @param h The height of the rectangle.
         *
         * @return <CODE>true</CODE> if the arc contains the rectangle,
         * <CODE>false</CODE> if the arc doesn't contain the rectangle.
         */
        public override bool Contains(int x, int y, int w, int h)
        {
            return Contains(x, y, w, h, null);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the interior of the arc entirely contains
         * the specified rectangle.
         *
         * @param r The <CODE>Rectangle</CODE> to test.
         *
         * @return <CODE>true</CODE> if the arc contains the rectangle,
         * <CODE>false</CODE> if the arc doesn't contain the rectangle.
         */
        public override bool Contains(Rectangle r)
        {
            return Contains(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight(), r);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private bool Contains(int x, int y, int w, int h,
                     Rectangle origrect)
        {
            if (!(Contains(x, y) &&
                  Contains(x + w, y) &&
                  Contains(x, y + h) &&
                  Contains(x + w, y + h)))
            {
                return false;
            }
            // If the shape is convex then we have done all the testing
            // we need.  Only PIE arcs can be concave and then only if
            // the angular extents are greater than 180 degrees.
            if (_type != PIE || MathEx.Abs(GetAngleExtent()) <= 180.0)
            {
                return true;
            }
            // For a PIE shape we have an additional test for the case where
            // the angular extents are greater than 180 degrees and all four
            // rectangular corners are inside the shape but one of the
            // rectangle edges spans across the "missing wedge" of the arc.
            // We can test for this case by checking if the rectangle intersects
            // either of the pie angle segments.
            if (origrect == null)
            {
                origrect = new Rectangle((int)(x + .5),
                            (int)(y + .5),
                            (int)(w + .5),
                            (int)(h + .5));
            }
            double halfW = GetWidth() / 2.0;
            double halfH = GetHeight() / 2.0;
            double xc = GetX() + halfW;
            double yc = GetY() + halfH;
            double angle = MathEx.ToRadians(-GetAngleStart());
            double xe = xc + halfW * MathEx.Cos(angle);
            double ye = yc + halfH * MathEx.Sin(angle);
            if (origrect.IntersectsLine((int)(xc + .5),
                        (int)(yc + .5),
                        (int)(xe + .5),
                        (int)(ye + .5)))
            {
                return false;
            }
            angle += MathEx.ToRadians(-GetAngleExtent());
            xe = xc + halfW * MathEx.Cos(angle);
            ye = yc + halfH * MathEx.Sin(angle);
            return !origrect.IntersectsLine((int)(xc + .5),
                        (int)(yc + .5),
                        (int)(xe + .5),
                        (int)(ye + .5));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of the
         * arc.
         * This iterator is multithread safe.
         * <code>Arc</code> guarantees that
         * modifications to the geometry of the arc
         * do not affect any iterations of that geometry that
         * are already in process.
         *
         * @param at an optional <CODE>AffineTransform</CODE> to be applied
         * to the coordinates as they are returned in the iteration, or null
         * if the untransformed coordinates are desired.
         *
         * @return A <CODE>IPathIterator</CODE> that defines the arc's boundary.
         */
        public override PathIterator GetPathIterator(AffineTransform at)
        {
            return new ArcIterator(this, at);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified <code>Object</code> is
         * equal to this <code>Arc</code>.  The specified
         * <code>Object</code> is equal to this <code>Arc</code>
         * if it is an instance of <code>Arc</code> and if its
         * location, size, arc extents and type are the same as this
         * <code>Arc</code>.
         * @param obj  an <code>Object</code> to be compared with this
         *             <code>Arc</code>.
         * @return  <code>true</code> if <code>obj</code> is an instance
         *          of <code>Arc</code> and has the same values;
         *          <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is Arc)
            {
                Arc a2d = (Arc)obj;
                return ((GetX() == a2d.GetX()) &&
                        (GetY() == a2d.GetY()) &&
                        (GetWidth() == a2d.GetWidth()) &&
                        (GetHeight() == a2d.GetHeight()) &&
                        (GetAngleStart() == a2d.GetAngleStart()) &&
                        (GetAngleExtent() == a2d.GetAngleExtent()) &&
                        (GetArcType() == a2d.GetArcType()));
            }
            return false;
        }


    }
}
