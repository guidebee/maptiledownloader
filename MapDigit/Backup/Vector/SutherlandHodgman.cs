//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 21JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using System.Collections;
using MapDigit.GIS.Geometry;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Class SutherlandHodgman stands for Sutherland-Hodgmanclip algorithem.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class SutherlandHodgman
    {

        private readonly OutputStage _stageOut;
        private readonly ClipStage _stageBottom;
        private readonly ClipStage _stageLeft;
        private readonly ClipStage _stageTop;
        private readonly ClipStage _stageRight;
        private readonly GeoLatLngBounds _rectBounds;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         * @param clipRect
         * 	clip region
         */
        public SutherlandHodgman(GeoLatLngBounds clipRect)
        {
            _stageOut = new OutputStage();
            _stageBottom = new ClipStage(_stageOut, 3, (clipRect.Y + clipRect.Height));
            _stageLeft = new ClipStage(_stageBottom, 4, clipRect.X);
            _stageTop = new ClipStage(_stageLeft, 1, clipRect.Y);
            _stageRight = new ClipStage(_stageTop, 2, (clipRect.X + clipRect.Width));
            _rectBounds = new GeoLatLngBounds(clipRect);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * clip the a pline.
         * @param input the pline to be clipped.
         * @return the clipped pline.
         */

        public ArrayList ClipPline(GeoLatLng[] input)
        {
            ArrayList clipped = new ArrayList();
            GeoLatLng p, prev = null;
            bool isInsidePrev = false;
            clipped.Clear();
            for (int i = 0; i < input.Length; i++)
            {
                p = input[i];
                bool isInside = _rectBounds.Contains(p);
                if (isInside)
                {
                    if (!isInsidePrev && (((clipped.Count != 0) &&
                            (!prev.Equals(clipped[clipped.Count - 1]))) ||
                            ((clipped.Count == 0 && (prev != null)))))
                    {
                        clipped.Add(prev);
                    }
                    clipped.Add(p);
                }
                else if (isInsidePrev)
                {
                    clipped.Add(p);
                }
                else if (prev != null)
                {

                    GeoLatLngBounds rect = new GeoLatLngBounds(Math.Min(p.X, prev.X),
                            Math.Min(p.Y, prev.Y),
                            Math.Max(p.X, prev.X) - Math.Min(p.X, prev.X),
                            Math.Max(p.Y, prev.Y) - Math.Min(p.Y, prev.Y));

                    if (rect.Intersects(_rectBounds))
                    {

                        ArrayList line1 = new ArrayList();
                        line1.Add(prev);
                        line1.Add(p);

                        ArrayList line2 = new ArrayList();
                        line2.Add(new GeoLatLng(_rectBounds.Y, _rectBounds.X));
                        line2.Add(new GeoLatLng(
                                (_rectBounds.Y + _rectBounds.Height),
                                (_rectBounds.X + _rectBounds.Width)));

                        ArrayList line3 = new ArrayList();
                        line3.Add(new GeoLatLng((_rectBounds.Y + _rectBounds.Height),
                                _rectBounds.X));
                        line3.Add(new GeoLatLng(
                                _rectBounds.Y,
                                (_rectBounds.X + _rectBounds.Width)));

                        if (IsLineInter(line1, line2) ||
                                IsLineInter(line1, line3))
                        {

                            clipped.Add(prev);
                            clipped.Add(p);
                        }
                    }
                }
                isInsidePrev = isInside;
                prev = p;

            }

            return clipped;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * clip the a pline.
         * @param input the pline to be clipped.
         * @return the clipped pline.
         */
        public ArrayList ClipPline(ArrayList input)
        {
            ArrayList clipped = new ArrayList();
            GeoLatLng p, prev = null;
            bool isInsidePrev = false;
            clipped.Clear();
            for (int i = 0; i < input.Count; i++)
            {
                p = (GeoLatLng)input[i];
                bool isInside = _rectBounds.Contains(p);
                if (isInside)
                {
                    if (!isInsidePrev && (((clipped.Count != 0) &&
                            (!prev.Equals(clipped[clipped.Count - 1]))) ||
                            ((clipped.Count == 0 && (prev != null)))))
                    {
                        clipped.Add(prev);
                    }
                    clipped.Add(p);
                }
                else if (isInsidePrev)
                {
                    clipped.Add(p);
                }
                else if (prev != null)
                {

                    GeoLatLngBounds rect = new GeoLatLngBounds(Math.Min(p.X, prev.X),
                            Math.Min(p.Y, prev.Y),
                            Math.Max(p.X, prev.X) - Math.Min(p.X, prev.X),
                            Math.Max(p.Y, prev.Y) - Math.Min(p.Y, prev.Y));

                    if (rect.Intersects(_rectBounds))
                    {

                        ArrayList line1 = new ArrayList();
                        line1.Add(prev);
                        line1.Add(p);

                        ArrayList line2 = new ArrayList();
                        line2.Add(new GeoLatLng(_rectBounds.Y, _rectBounds.X));
                        line2.Add(new GeoLatLng(
                                (_rectBounds.Y + _rectBounds.Height),
                                (_rectBounds.X + _rectBounds.Width)));

                        ArrayList line3 = new ArrayList();
                        line3.Add(new GeoLatLng((_rectBounds.Y + _rectBounds.Height),
                                _rectBounds.X));
                        line3.Add(new GeoLatLng(
                                _rectBounds.Y,
                                (_rectBounds.X + _rectBounds.Width)));

                        if (IsLineInter(line1, line2) ||
                                IsLineInter(line1, line3))
                        {

                            clipped.Add(prev);
                            clipped.Add(p);
                        }
                    }
                }
                isInsidePrev = isInside;
                prev = p;

            }

            return clipped;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * clip one region.
         *
         * @param input
         *            the region to be clipped.
         * @return the clipped region.
         */
        public ArrayList ClipRegion(GeoLatLng[] input)
        {

            ArrayList clipped = new ArrayList();
            clipped.Clear();
            _stageOut.SetDestination(clipped);

            for (int i = 0; i < input.Length; i++)
            {
                _stageRight.HandleVertex(input[i]);
            }
            // Do the final step.
            _stageRight.Finish();

            return clipped;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 14JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * clip one region.
         *
         * @param input
         *            the region to be clipped.
         * @return the clipped region.
         */
        public ArrayList ClipRegion(ArrayList input)
        {

            ArrayList clipped = new ArrayList();
            clipped.Clear();
            _stageOut.SetDestination(clipped);

            for (int i = 0; i < input.Count; i++)
            {
                _stageRight.HandleVertex((GeoLatLng)input[i]);
            }
            // Do the final step.
            _stageRight.Finish();

            return clipped;
        }

        private static double CrossMulti(Object object1, Object object2, Object object0)
        {
            double x1, y1, x2, y2;
            GeoLatLng p1 = (GeoLatLng)object1;
            GeoLatLng p2 = (GeoLatLng)object2;
            GeoLatLng p0 = (GeoLatLng)object0;
            x1 = p1.X - p0.X;
            y1 = p1.Y - p0.Y;
            x2 = p2.X - p0.X;
            y2 = p2.Y - p0.Y;
            return x1 * y2 - x2 * y1;
        }

        private static bool IsLineInter(ArrayList line1, ArrayList line2)
        {
            double t1 = CrossMulti(line2[0], line1[line1.Count - 1],
                    line1[0]);
            double t2 = CrossMulti(line2[line2.Count - 1], line1[line1.Count - 1],
                    line1[0]);
            double t = t1 * t2;
            if (t > 0.0)
            {
                return false;
            }

            t1 = CrossMulti(line1[0], line2[line2.Count - 1],
                    line2[0]);

            t2 = CrossMulti(line1[line1.Count - 1], line2[line2.Count - 1],
                    line2[0]);
            t = t1 * t2;
            if (t > 0.0)
            {
                return false;
            }

            return true;
        }
    }

    class ClipStage
    {

        /**
         * The next stage
         */
        private readonly ClipStage _nextStage;
        /**
         * true if no vertices have been handled.
         */
        private bool _bFirst;
        /**
         * the first vertex.
         */
        private GeoLatLng _pntFirst;
        /**
         * the previous vertex.
         */
        private GeoLatLng _pntPrevious;
        /**
         * true if the previous vertex was inside the Boundary.
         */
        private bool _bPreviousInside;
        private readonly int _intDirection;
        private readonly double _dblX;
        private readonly double _dblY;

        protected ClipStage()
        {
        }

        //	1-Top,2-Right,3-Bottom,4-Left,5 out
        public ClipStage(ClipStage nextStage, int direction, double cord)
        {
            _nextStage = nextStage;
            _bFirst = true;
            _intDirection = direction;
            switch (_intDirection)
            {
                case 1:
                case 3:
                    _dblY = cord;
                    break;
                case 2:
                case 4:
                    _dblX = cord;
                    break;
            }
        }

        private bool IsInside(GeoLatLng pt)
        {
            bool ret = false;
            switch (_intDirection)
            {
                case 1://Top
                    ret = pt.Y >= _dblY;
                    break;
                case 2://Right
                    ret = pt.X < _dblX;
                    break;
                case 3://Bottom
                    ret = pt.Y < _dblY;
                    break;
                case 4://Left
                    ret = pt.X >= _dblX;
                    break;
            }
            return ret;
        }

        //	 Function to handle one vertex
        public virtual void HandleVertex(GeoLatLng pntCurrent)
        {
            {
                if (pntCurrent == null)
                {
                    return;
                }
                bool bCurrentInside = IsInside(pntCurrent);
                // See if vertex is inside the boundary.

                if (_bFirst) // If this is the first vertex...
                {
                    _pntFirst = pntCurrent;	// ... just remember it,...

                    _bFirst = false;
                }
                else // Common cases, not the first vertex.
                {
                    if (bCurrentInside) // If this vertex is inside...
                    {
                        if (!_bPreviousInside) // ... and the previous one was outside
                        {
                            _nextStage.HandleVertex(Intersect(_pntPrevious, pntCurrent));
                        }
                        // ... first output the intersection point.

                        _nextStage.HandleVertex(pntCurrent);	// Output the current vertex.
                    }
                    else if (_bPreviousInside) // If this vertex is outside, and the previous one was inside...
                    {
                        _nextStage.HandleVertex(Intersect(_pntPrevious, pntCurrent));
                    }
                    // ... output the intersection point.

                    // If neither current vertex nor the previous one are inside, output nothing.
                }
                _pntPrevious = pntCurrent;		// Be prepared for next vertex.
                _bPreviousInside = bCurrentInside;
            }
        }

        public virtual void Finish()
        {
            HandleVertex(_pntFirst);		// Close the polygon.
            _nextStage.Finish();			// Delegate to the next stage.
            _bFirst = true;
        }

        private GeoLatLng Intersect(GeoLatLng p0, GeoLatLng p1)
        {
            GeoLatLng r = new GeoLatLng(0, 0);
            GeoLatLng d;
            switch (_intDirection)
            {
                case 1:
                case 3:
                    d = new GeoLatLng(p1.Y - p0.Y, p1.X - p0.X);
                    double xslope = d.X / d.Y;
                    r.Y = _dblY;
                    r.X = p0.X + xslope * (_dblY - p0.Y);
                    break;
                case 2:
                case 4:
                    d = new GeoLatLng(p1.Y - p0.Y, p1.X - p0.X);
                    double yslope = d.Y / d.X;
                    r.X = _dblX;
                    r.Y = p0.Y + yslope * (_dblX - p0.X);
                    break;
            }
            return r;
        }
    }

    class OutputStage : ClipStage
    {

        private ArrayList _pDest;

        public void SetDestination(ArrayList pDest)
        {
            _pDest = pDest;
        }

        // Append the vertex to the output container.
        public override void HandleVertex(GeoLatLng pnt)
        {
            if (_pDest.Count == 0 || !pnt.Equals(_pDest[_pDest.Count - 1]))
            {
                _pDest.Add(pnt);
            }
        }

        //	 Do nothing.
        public  override void  Finish()
        {
        }
    }




}
