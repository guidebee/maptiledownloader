using System;

namespace MapDigit.Drawing.Geometry
{
    public abstract class RectangularShape : IShape
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the upper-left corner of 
         * the framing rectangle in <code>long</code> precision.
         * @return the X coordinate of the upper-left corner of
         * the framing rectangle.
         */
        public abstract int GetX();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the upper-left corner of 
         * the framing rectangle in <code>long</code> precision.
         * @return the Y coordinate of the upper-left corner of
         * the framing rectangle.
         */
        public abstract int GetY();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the width of the framing rectangle in 
         * <code>long</code> precision.
         * @return the width of the framing rectangle.
         */
        public abstract int GetWidth();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the height of the framing rectangle
         * in <code>long</code> precision.
         * @return the height of the framing rectangle.
         */
        public abstract int GetHeight();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smallest X coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the smallest X coordinate of the framing 
         * 		rectangle of the <code>IShape</code>.
         */
        public int GetMinX()
        {
            return GetX();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the smallest Y coordinate of the framing
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the smallest Y coordinate of the framing 
         * 		rectangle of the <code>IShape</code>.
         */
        public int GetMinY()
        {
            return GetY();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the largest X coordinate of the framing 
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the largest X coordinate of the framing
         * 		rectangle of the <code>IShape</code>.
         */
        public int GetMaxX()
        {
            return GetX() + GetWidth();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the largest Y coordinate of the framing 
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the largest Y coordinate of the framing 
         *		rectangle of the <code>IShape</code>.
         */
        public int GetMaxY()
        {
            return GetY() + GetHeight();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the center of the framing
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the X coordinate of the center of the framing rectangle 
         * 		of the <code>IShape</code>.
         */
        public int GetCenterX()
        {
            return GetX() + GetWidth() / 2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the center of the framing 
         * rectangle of the <code>IShape</code> in <code>long</code>
         * precision.
         * @return the Y coordinate of the center of the framing rectangle 
         * 		of the <code>IShape</code>.
         */
        public int GetCenterY()
        {
            return GetY() + GetHeight() / 2;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the framing {@link Rectangle}
         * that defines the overall shape of this object.
         * @return a <code>Rectangle</code>, specified in
         * <code>long</code> coordinates.
         */
        public Rectangle GetFrame()
        {
            return new Rectangle(GetX(), GetY(), GetWidth(), GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether the <code>RectangularShape</code> is empty.
         * When the <code>RectangularShape</code> is empty, it encloses no
         * area.
         * @return <code>true</code> if the <code>RectangularShape</code> is empty; 
         * 		<code>false</code> otherwise.
         */
        public abstract bool IsEmpty();

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location and size of the framing rectangle of this
         * <code>IShape</code> to the specified rectangular values.
         *
         * @param x the X coordinate of the upper-left corner of the
         * 	        specified rectangular shape
         * @param y the Y coordinate of the upper-left corner of the
         * 	        specified rectangular shape
         * @param w the width of the specified rectangular shape
         * @param h the height of the specified rectangular shape
         */
        public abstract void SetFrame(int x, int y, int w, int h);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the location and size of the framing rectangle of this 
         * <code>IShape</code> to the specified {@link Point} and
         * {@link Dimension}, respectively.  The framing rectangle is used
         * by the subclasses of <code>RectangularShape</code> to define 
         * their geometry.
         * @param loc the specified <code>Point</code>
         * @param size the specified <code>Dimension</code>
         */
        public void SetFrame(Point loc, Dimension size)
        {
            SetFrame(loc.GetX(), loc.GetY(), size.GetWidth(), size.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the framing rectangle of this <code>IShape</code> to 
         * be the specified <code>Rectangle</code>.  The framing rectangle is
         * used by the subclasses of <code>RectangularShape</code> to define
         * their geometry.
         * @param r the specified <code>Rectangle</code>
         */
        public void SetFrame(Rectangle r)
        {
            SetFrame(r.GetX(), r.GetY(), r.GetWidth(), r.GetHeight());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
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
        public void SetFrameFromDiagonal(int x1, int y1,
                         int x2, int y2)
        {
            if (x2 < x1)
            {
                int t = x1;
                x1 = x2;
                x2 = t;
            }
            if (y2 < y1)
            {
                int t = y1;
                y1 = y2;
                y2 = t;
            }
            SetFrame(x1, y1, x2 - x1, y2 - y1);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the diagonal of the framing rectangle of this <code>IShape</code> 
         * based on two specified <code>Point</code> objects.  The framing
         * rectangle is used by the subclasses of <code>RectangularShape</code> 
         * to define their geometry.
         *
         * @param p1 the start <code>Point</code> of the specified diagonal
         * @param p2 the end <code>Point</code> of the specified diagonal
         */
        public void SetFrameFromDiagonal(Point p1, Point p2)
        {
            SetFrameFromDiagonal(p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
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
        public void SetFrameFromCenter(int centerX, int centerY,
                       int cornerX, int cornerY)
        {
            int halfW = Math.Abs(cornerX - centerX);
            int halfH = Math.Abs(cornerY - centerY);
            SetFrame(centerX - halfW, centerY - halfH, halfW * 2, halfH * 2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets the framing rectangle of this <code>IShape</code> based on a 
         * specified center <code>Point</code> and corner
         * <code>Point</code>.  The framing rectangle is used by the subclasses
         * of <code>RectangularShape</code> to define their geometry.
         * @param center the specified center <code>Point</code>
         * @param corner the specified corner <code>Point</code>
         */
        public void SetFrameFromCenter(Point center, Point corner)
        {
            SetFrameFromCenter(center.GetX(), center.GetY(),
                       corner.GetX(), corner.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public abstract bool Contains(int x, int y);

        public virtual bool Contains(Point p)
        {
            return Contains(p.X, p.Y);
        }

        public abstract bool Contains(int x, int y, int w, int h);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public abstract bool Intersects(int x, int y, int w, int h);

        public virtual bool Intersects(Rectangle r)
        {
            return Intersects(r.X, r.Y, r.Width, r.Height);
        }

        public abstract PathIterator GetPathIterator(AffineTransform at);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public virtual bool Contains(Rectangle r)
        {
            return Contains(r.X, r.Y, r.Width, r.Height);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * {@inheritDoc}
         */
        public virtual Rectangle GetBounds()
        {
            long width = GetWidth();
            long height = GetHeight();
            if (width < 0 || height < 0)
            {
                return new Rectangle();
            }
            long x = GetX();
            long y = GetY();
            long x1 = MathFP.Floor(x);
            long y1 = MathFP.Floor(y);
            long x2 = MathFP.Ceil(x + width);
            long y2 = MathFP.Ceil(y + height);
            return new Rectangle((int)x1, (int)y1,
                              (int)(x2 - x1), (int)(y2 - y1));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 02NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an iterator object that iterates along the 
         * <code>IShape</code> object's boundary and provides access to a
         * flattened view of the outline of the <code>IShape</code>
         * object's geometry.
         * <p>
         * Only SEG_MOVETO, SEG_LINETO, and SEG_CLOSE point types will
         * be returned by the iterator.
         * <p>
         * The amount of subdivision of the curved segments is controlled
         * by the <code>flatness</code> parameter, which specifies the
         * maximum distance that any point on the unflattened transformed
         * curve can deviate from the returned flattened path segments.
         * An optional {@link AffineTransform} can
         * be specified so that the coordinates returned in the iteration are
         * transformed accordingly.
         * @param at an optional <code>AffineTransform</code> to be applied to the
         * 		coordinates as they are returned in the iteration, 
         *		or <code>null</code> if untransformed coordinates are desired.
         * @param flatness the maximum distance that the line segments used to
         *          approximate the curved segments are allowed to deviate
         *          from any point on the original curve
         * @return a <code>IPathIterator</code> object that provides access to 
         * 		the <code>IShape</code> object's flattened geometry.
         */
        public virtual PathIterator GetPathIterator(AffineTransform at, int flatness)
        {
            return new FlatteningPathIterator(GetPathIterator(at), flatness);
        }


    }

}
