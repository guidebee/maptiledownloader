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

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.DrawingFP
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * a 2D rectangle class.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class RectangleFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the bottom.
         * @return
         */
        public int GetBottom()
        {
            return _ffYmax;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the top.
         * @return
         */
        public int GetTop()
        {
            return _ffYmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the left.
         * @return
         */
        public int GetLeft()
        {
            return _ffXmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the right.
         * @return
         */
        public int GetRight()
        {
            return _ffXmax;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the width.
         * @return
         */
        public int GetWidth()
        {
            return _ffXmax - _ffXmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the width.
         * @param Value
         */
        public void SetWidth(int value)
        {
            if (value < 0)
            {
                return;
            }
            _ffXmax = _ffXmin + value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the height.
         * @return
         */
        public int GetHeight()
        {
            return _ffYmax - _ffYmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * set the height.
         * @param Value
         */
        public void SetHeight(int value)
        {

            if (value < 0)
            {
                return;
            }
            _ffYmax = _ffYmin + value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get the X.
         * @return
         */
        public int GetX()
        {
            return _ffXmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the X.
         * @param Value
         */
        public void SetX(int value)
        {
            _ffXmin = value;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the Y.
         * @return
         */
        public int GetY()
        {
            return _ffYmin;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the Y.
         * @param Value
         */
        public void SetY(int value)
        {
            _ffYmin = value;

        }
        /**
         * The empty rectangle.
         */
        public static readonly RectangleFP Empty = new RectangleFP();
        private int _ffXmin;
        private int _ffXmax;
        private int _ffYmin;
        private int _ffYmax;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public RectangleFP()
        {
            _ffXmin = _ffXmax = _ffYmin = _ffYmax = SingleFP.NOT_A_NUMBER;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param r
         */
        public RectangleFP(RectangleFP r)
        {
            Reset(r);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructor.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         */
        public RectangleFP(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            Reset(ffXmin, ffYmin, ffXmax, ffYmax);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Reset the rectangle.
         * @param ff_xmin
         * @param ff_ymin
         * @param ff_xmax
         * @param ff_ymax
         * @return
         */
        public RectangleFP Reset(int ffXmin, int ffYmin, int ffXmax, int ffYmax)
        {
            _ffXmin = MathFP.Min(ffXmin, ffXmax);
            _ffXmax = MathFP.Max(ffXmin, ffXmax);
            _ffYmin = MathFP.Min(ffYmin, ffYmax);
            _ffYmax = MathFP.Max(ffYmin, ffYmax);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * reset the rectangle.
         * @param r
         * @return
         */
        public RectangleFP Reset(RectangleFP r)
        {
            return Reset(r._ffXmin, r._ffYmin, r._ffXmax, r._ffYmax);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check if the rectangle is empty.
         * @return
         */
        public bool IsEmpty()
        {
            return _ffXmin == SingleFP.NOT_A_NUMBER || _ffXmax == SingleFP.NOT_A_NUMBER ||
                    _ffYmin == SingleFP.NOT_A_NUMBER || _ffYmax == SingleFP.NOT_A_NUMBER;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Translate the rectangle.
         * @param ff_dx
         * @param ff_dy
         */
        public void Offset(int ffDx, int ffDy)
        {
            if (!IsEmpty())
            {
                _ffXmin += ffDx;
                _ffXmax += ffDx;
                _ffYmin += ffDy;
                _ffYmax += ffDy;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Calculate the union of the two rectangle.
         * @param r
         * @return
         */
        public RectangleFP Union(RectangleFP r)
        {
            if (!r.IsEmpty())
            {
                if (IsEmpty())
                {
                    Reset(r);
                }
                else
                {
                    Reset(MathFP.Min(_ffXmin, r._ffXmin),
                            MathFP.Max(_ffXmax, r._ffXmax),
                            MathFP.Min(_ffYmin, r._ffYmin),
                            MathFP.Max(_ffYmax, r._ffYmax));
                }
            }
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * return the union of the rectangle and the given point.
         * @param p
         * @return
         */
        public RectangleFP Union(PointFP p)
        {
            if (!IsEmpty())
            {
                Reset(MathFP.Min(_ffXmin, p.X), MathFP.Max(_ffXmax, p.X),
                        MathFP.Min(_ffYmin, p.Y), MathFP.Max(_ffYmax, p.Y));
            }
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see this rectange intersect with given rectange.
         * @param r
         * @return
         */
        public bool IntersectsWith(RectangleFP r)
        {
            return _ffXmin <= r._ffXmax && r._ffXmin <= _ffXmax &&
                    _ffYmin <= r._ffYmax && r._ffYmin <= _ffYmax;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see if this rectangle contains given point.
         * @param p
         * @return
         */
        public bool Contains(PointFP p)
        {
            return _ffXmin <= p.X && p.X <= _ffXmax
                    && _ffYmin <= p.Y && p.Y <= _ffYmax;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Check to see two rect has same location.
         * @param o
         * @return
         */
        public new bool Equals(object o)
        {
            if (o is RectangleFP)
            {
                var r = (RectangleFP)o;
                return r._ffXmax == _ffXmax && r._ffXmin == _ffXmin
                       && r._ffYmax == _ffYmax && r._ffYmin == _ffYmin;
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
         * Returns the hashcode for this <code>Rectangle</code>.
         * @return the hashcode for this <code>Rectangle</code>.
         */
        public int HashCode()
        {
            var bits = (int)(_ffXmin & 0xFFFF0000 + _ffYmin & 0x0000FFFF);

            return bits;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to string.
         * @return
         */
        public override string ToString()
        {
            return "Rectangle" + " (" + new SingleFP(_ffXmin) + "," +
                    new SingleFP(_ffYmin) + ")-(" + new SingleFP(_ffXmax) + "," +
                    new SingleFP(_ffYmax) + ")";
        }
    }
}
