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
     * The <code>Ellipse</code> class defines an ellipse specified
     * in <code>Double</code> precision.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class Ellipse : RectangularShape
    {

        /**
         * The X coordinate of the upper-left corner of the
         * framing rectangle of this {@code Ellipse}.
         */
        public double X;

        /**
         * The Y coordinate of the upper-left corner of the
         * framing rectangle of this {@code Ellipse}.
         */
        public double Y;

        /**
         * The overall width of this <code>Ellipse</code>.
         */
        public double Width;

        /**
         * The overall height of the <code>Ellipse</code>.
         */
        public double Height;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>Ellipse</code>, initialized to
         * location (0,&nbsp;0) and size (0,&nbsp;0).
         */
        public Ellipse()
        {
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes an <code>Ellipse</code> from the
         * specified coordinates.
         *
         * @param x the X coordinate of the upper-left corner
         *        of the framing rectangle
         * @param y the Y coordinate of the upper-left corner
         *        of the framing rectangle
         * @param w the width of the framing rectangle
         * @param h the height of the framing rectangle
         */
        public Ellipse(double x, double y, double w, double h)
        {
            SetFrame((int)(x + .5),
                    (int)(y + .5), (int)(w + .5),
                    (int)(h + .5));
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
         * {@inheritDoc}
         */
        public override sealed void SetFrame(int x, int y, int w, int h)
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
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public override Rectangle GetBounds()
        {
            return new Rectangle(
                    (int)(X + .5),
                    (int)(Y + .5),
                    (int)(Width + .5),
                    (int)(Height + .5));
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
            return (normx * normx + normy * normy) < 0.25;
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
        public override bool Intersects(int x, int y, int w, int h)
        {
            if (w <= 0.0 || h <= 0.0)
            {
                return false;
            }
            // Normalize the rectangular coordinates compared to the ellipse
            // having a center at 0,0 and a radius of 0.5.
            double ellw = GetWidth();
            if (ellw <= 0.0)
            {
                return false;
            }
            double normx0 = (x - GetX()) / ellw - 0.5;
            double normx1 = normx0 + w / ellw;
            double ellh = GetHeight();
            if (ellh <= 0.0)
            {
                return false;
            }
            double normy0 = (y - GetY()) / ellh - 0.5;
            double normy1 = normy0 + h / ellh;
            // find nearest x (left edge, right edge, 0.0)
            // find nearest y (top edge, bottom edge, 0.0)
            // if nearest x,y is inside circle of radius 0.5, then intersects
            double nearx, neary;
            if (normx0 > 0.0)
            {
                // center to left of X extents
                nearx = normx0;
            }
            else if (normx1 < 0.0)
            {
                // center to right of X extents
                nearx = normx1;
            }
            else
            {
                nearx = 0.0;
            }
            if (normy0 > 0.0)
            {
                // center above Y extents
                neary = normy0;
            }
            else if (normy1 < 0.0)
            {
                // center below Y extents
                neary = normy1;
            }
            else
            {
                neary = 0.0;
            }
            return (nearx * nearx + neary * neary) < 0.25;
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
        public override bool Contains(int x, int y, int w, int h)
        {
            return (Contains(x, y) &&
                Contains(x + w, y) &&
                Contains(x, y + h) &&
                Contains(x + w, y + h));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iteration object that defines the boundary of this
         * <code>Ellipse</code>.
         * The iterator for this class is multi-threaded safe, which means
         * that this <code>Ellipse</code> class guarantees that
         * modifications to the geometry of this <code>Ellipse</code>
         * object do not affect any iterations of that geometry that
         * are already in process.
         * @param at an optional <code>AffineTransform</code> to be applied to
         * the coordinates as they are returned in the iteration, or
         * <code>null</code> if untransformed coordinates are desired
         * @return    the <code>IPathIterator</code> object that returns the
         *          geometry of the outline of this <code>Ellipse</code>,
         *		one segment at a time.
         */
        public override PathIterator GetPathIterator(AffineTransform at)
        {
            return new EllipseIterator(this, at);
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not the specified <code>Object</code> is
         * equal to this <code>Ellipse</code>.  The specified
         * <code>Object</code> is equal to this <code>Ellipse</code>
         * if it is an instance of <code>Ellipse</code> and if its
         * location and size are the same as this <code>Ellipse</code>.
         * @param obj  an <code>Object</code> to be compared with this
         *             <code>Ellipse</code>.
         * @return  <code>true</code> if <code>obj</code> is an instance
         *          of <code>Ellipse</code> and has the same values;
         *          <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is Ellipse)
            {
                Ellipse e2D = (Ellipse)obj;
                return ((GetX() == e2D.GetX()) &&
                        (GetY() == e2D.GetY()) &&
                        (GetWidth() == e2D.GetWidth()) &&
                        (GetHeight() == e2D.GetHeight()));
            }
            return false;
        }
    }
}
