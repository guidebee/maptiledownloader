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
using System.Collections;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing.Geometry
{
    internal abstract class Crossings
    {


        int _limit;
        double[] _yranges = new double[10];
        readonly double _xlo;
        readonly double _ylo;
        readonly double _xhi;
        readonly double _yhi;

        internal Crossings(double xlo, double ylo, double xhi, double yhi)
        {
            _xlo = xlo;
            _ylo = ylo;
            _xhi = xhi;
            _yhi = yhi;
        }

        public double GetXLo()
        {
            return _xlo;
        }

        public double GetYLo()
        {
            return _ylo;
        }

        public double GetXHi()
        {
            return _xhi;
        }

        public double GetYHi()
        {
            return _yhi;
        }

        public abstract void Record(double ystart, double yend, int direction);



        public bool IsEmpty()
        {
            return (_limit == 0);
        }

        public abstract bool Covers(double ystart, double yend);

        public static Crossings FindCrossings(ArrayList curves,
                double xlo, double ylo,
                double xhi, double yhi)
        {
            Crossings cross = new EvenOdd(xlo, ylo, xhi, yhi);
            IEnumerator enumerator = curves.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Curve c = (Curve)enumerator.Current;
                if (c.AccumulateCrossings(cross))
                {
                    return null;
                }
            }

            return cross;
        }

        public static Crossings FindCrossings(PathIterator pi,
                double xlo, double ylo,
                double xhi, double yhi)
        {
            Crossings cross;
            if (pi.GetWindingRule() == PathIterator.WIND_EVEN_ODD)
            {
                cross = new EvenOdd(xlo, ylo, xhi, yhi);
            }
            else
            {
                cross = new NonZero(xlo, ylo, xhi, yhi);
            }
            // coords array is big enough for holding:
            //     coordinates returned from currentSegment (6)
            //     OR
            //         two subdivided quadratic curves (2+4+4=10)
            //         AND
            //             0-1 horizontal splitting parameters
            //             OR
            //             2 parametric equation derivative coefficients
            //     OR
            //         three subdivided cubic curves (2+6+6+6=20)
            //         AND
            //             0-2 horizontal splitting parameters
            //             OR
            //             3 parametric equation derivative coefficients
            var coords = new int[23];
            double movx = 0;
            double movy = 0;
            double curx = 0;
            double cury = 0;
            while (!pi.IsDone())
            {
                int type = pi.CurrentSegment(coords);
                double newx;
                double newy;
                switch (type)
                {
                    case PathIterator.SEG_MOVETO:
                        if (movy != cury &&
                                cross.AccumulateLine(curx, cury, movx, movy))
                        {
                            return null;
                        }
                        movx = curx = coords[0];
                        movy = cury = coords[1];
                        break;
                    case PathIterator.SEG_LINETO:
                        newx = coords[0];
                        newy = coords[1];
                        if (cross.AccumulateLine(curx, cury, newx, newy))
                        {
                            return null;
                        }
                        curx = newx;
                        cury = newy;
                        break;
                    case PathIterator.SEG_QUADTO:
                        {
                            newx = coords[2];
                            newy = coords[3];
                            var dblCoords = new double[coords.Length];
                            for (int i = 0; i < coords.Length; i++)
                            {
                                dblCoords[i] = coords[i];
                            }
                            if (cross.AccumulateQuad(curx, cury, dblCoords))
                            {
                                return null;
                            }
                            curx = newx;
                            cury = newy;
                        }
                        break;
                    case PathIterator.SEG_CUBICTO:
                        {
                            newx = coords[4];
                            newy = coords[5];
                            var dblCoords = new double[coords.Length];
                            for (int i = 0; i < coords.Length; i++)
                            {
                                dblCoords[i] = coords[i];
                            }
                            if (cross.AccumulateCubic(curx, cury, dblCoords))
                            {
                                return null;
                            }
                            curx = newx;
                            cury = newy;
                            break;
                        }
                    case PathIterator.SEG_CLOSE:
                        if (movy != cury &&
                                cross.AccumulateLine(curx, cury, movx, movy))
                        {
                            return null;
                        }
                        curx = movx;
                        cury = movy;
                        break;
                }
                pi.Next();
            }
            if (movy != cury)
            {
                if (cross.AccumulateLine(curx, cury, movx, movy))
                {
                    return null;
                }
            }

            return cross;
        }

        public bool AccumulateLine(double x0, double y0,
                double x1, double y1)
        {
            if (y0 <= y1)
            {
                return AccumulateLine(x0, y0, x1, y1, 1);
            }
            return AccumulateLine(x1, y1, x0, y0, -1);
        }

        public bool AccumulateLine(double x0, double y0,
            double x1, double y1,
            int direction)
        {
            if (_yhi <= y0 || _ylo >= y1)
            {
                return false;
            }
            if (x0 >= _xhi && x1 >= _xhi)
            {
                return false;
            }
            if (y0 == y1)
            {
                return (x0 >= _xlo || x1 >= _xlo);
            }
            double xstart, ystart, xend, yend;
            double dx = (x1 - x0);
            double dy = (y1 - y0);
            if (y0 < _ylo)
            {
                xstart = x0 + (_ylo - y0) * dx / dy;
                ystart = _ylo;
            }
            else
            {
                xstart = x0;
                ystart = y0;
            }
            if (_yhi < y1)
            {
                xend = x0 + (_yhi - y0) * dx / dy;
                yend = _yhi;
            }
            else
            {
                xend = x1;
                yend = y1;
            }
            if (xstart >= _xhi && xend >= _xhi)
            {
                return false;
            }
            if (xstart > _xlo || xend > _xlo)
            {
                return true;
            }
            Record(ystart, yend, direction);
            return false;
        }
        private readonly ArrayList _tmp = new ArrayList();

        public bool AccumulateQuad(double x0, double y0, double[] coords)
        {
            if (y0 < _ylo && coords[1] < _ylo && coords[3] < _ylo)
            {
                return false;
            }
            if (y0 > _yhi && coords[1] > _yhi && coords[3] > _yhi)
            {
                return false;
            }
            if (x0 > _xhi && coords[0] > _xhi && coords[2] > _xhi)
            {
                return false;
            }
            if (x0 < _xlo && coords[0] < _xlo && coords[2] < _xlo)
            {
                if (y0 < coords[3])
                {
                    Record(Math.Max(y0, _ylo), Math.Min(coords[3], _yhi), 1);
                }
                else if (y0 > coords[3])
                {
                    Record(Math.Max(coords[3], _ylo), Math.Min(y0, _yhi), -1);
                }
                return false;
            }
            Curve.InsertQuad(_tmp, x0, y0, coords);
            IEnumerator enumerator = _tmp.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Curve c = (Curve)enumerator.Current;
                if (c.AccumulateCrossings(this))
                {
                    return true;
                }
            }
            _tmp.Clear();
            return false;
        }

        public bool AccumulateCubic(double x0, double y0, double[] coords)
        {
            if (y0 < _ylo && coords[1] < _ylo &&
                    coords[3] < _ylo && coords[5] < _ylo)
            {
                return false;
            }
            if (y0 > _yhi && coords[1] > _yhi &&
                    coords[3] > _yhi && coords[5] > _yhi)
            {
                return false;
            }
            if (x0 > _xhi && coords[0] > _xhi &&
                    coords[2] > _xhi && coords[4] > _xhi)
            {
                return false;
            }
            if (x0 < _xlo && coords[0] < _xlo &&
                    coords[2] < _xlo && coords[4] < _xlo)
            {
                if (y0 <= coords[5])
                {
                    Record(Math.Max(y0, _ylo), Math.Min(coords[5], _yhi), 1);
                }
                else
                {
                    Record(Math.Max(coords[5], _ylo), Math.Min(y0, _yhi), -1);
                }
                return false;
            }
            Curve.InsertCubic(_tmp, x0, y0, coords);
            IEnumerator enumerator = _tmp.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Curve c = (Curve)enumerator.Current;
                if (c.AccumulateCrossings(this))
                {
                    return true;
                }
            }
            _tmp.Clear();
            return false;
        }

        internal class EvenOdd : Crossings
        {

            public EvenOdd(double xlo, double ylo, double xhi, double yhi)
                : base(xlo, ylo, xhi, yhi)
            {

            }

            public override bool Covers(double ystart, double yend)
            {
                return (_limit == 2 && _yranges[0] <= ystart && _yranges[1] >= yend);
            }

            public override void Record(double ystart, double yend, int direction)
            {
                if (ystart >= yend)
                {
                    return;
                }
                int from = 0;
                // Quickly jump over all pairs that are completely "above"
                while (from < _limit && ystart > _yranges[from + 1])
                {
                    from += 2;
                }
                int to = from;
                while (from < _limit)
                {
                    double yrlo = _yranges[from++];
                    double yrhi = _yranges[from++];
                    if (yend < yrlo)
                    {
                        // Quickly handle insertion of the new range
                        _yranges[to++] = ystart;
                        _yranges[to++] = yend;
                        ystart = yrlo;
                        yend = yrhi;
                        continue;
                    }
                    // The ranges overlap - sort, collapse, insert, iterate
                    double yll, ylh, yhl, yhh;
                    if (ystart < yrlo)
                    {
                        yll = ystart;
                        ylh = yrlo;
                    }
                    else
                    {
                        yll = yrlo;
                        ylh = ystart;
                    }
                    if (yend < yrhi)
                    {
                        yhl = yend;
                        yhh = yrhi;
                    }
                    else
                    {
                        yhl = yrhi;
                        yhh = yend;
                    }
                    if (ylh == yhl)
                    {
                        ystart = yll;
                        yend = yhh;
                    }
                    else
                    {
                        if (ylh > yhl)
                        {
                            ystart = yhl;
                            yhl = ylh;
                            ylh = ystart;
                        }
                        if (yll != ylh)
                        {
                            _yranges[to++] = yll;
                            _yranges[to++] = ylh;
                        }
                        ystart = yhl;
                        yend = yhh;
                    }
                    if (ystart >= yend)
                    {
                        break;
                    }
                }
                if (to < from && from < _limit)
                {
                    Array.Copy(_yranges, from, _yranges, to, _limit - from);
                }
                to += (_limit - from);
                if (ystart < yend)
                {
                    if (to >= _yranges.Length)
                    {
                        var newranges = new double[to + 10];
                        Array.Copy(_yranges, 0, newranges, 0, to);
                        _yranges = newranges;
                    }
                    _yranges[to++] = ystart;
                    _yranges[to++] = yend;
                }
                _limit = to;
            }
        }

        internal class NonZero : Crossings
        {

            private int[] _crosscounts;

            public NonZero(double xlo, double ylo, double xhi, double yhi)
                : base(xlo, ylo, xhi, yhi)
            {

                _crosscounts = new int[_yranges.Length / 2];
            }

            public override bool Covers(double ystart, double yend)
            {
                int i = 0;
                while (i < _limit)
                {
                    double ylo = _yranges[i++];
                    double yhi = _yranges[i++];
                    if (ystart >= yhi)
                    {
                        continue;
                    }
                    if (ystart < ylo)
                    {
                        return false;
                    }
                    if (yend <= yhi)
                    {
                        return true;
                    }
                    ystart = yhi;
                }
                return (ystart >= yend);
            }

            public void Remove(int cur)
            {
                _limit -= 2;
                int rem = _limit - cur;
                if (rem > 0)
                {
                    Array.Copy(_yranges, cur + 2, _yranges, cur, rem);
                    Array.Copy(_crosscounts, cur / 2 + 1,
                            _crosscounts, cur / 2,
                            rem / 2);
                }
            }

            public void Insert(int cur, double lo, double hi, int dir)
            {
                int rem = _limit - cur;
                double[] oldranges = _yranges;
                int[] oldcounts = _crosscounts;
                if (_limit >= _yranges.Length)
                {
                    _yranges = new double[_limit + 10];
                    Array.Copy(oldranges, 0, _yranges, 0, cur);
                    _crosscounts = new int[(_limit + 10) / 2];
                    Array.Copy(oldcounts, 0, _crosscounts, 0, cur / 2);
                }
                if (rem > 0)
                {
                    Array.Copy(oldranges, cur, _yranges, cur + 2, rem);
                    Array.Copy(oldcounts, cur / 2,
                            _crosscounts, cur / 2 + 1,
                            rem / 2);
                }
                _yranges[cur + 0] = lo;
                _yranges[cur + 1] = hi;
                _crosscounts[cur / 2] = dir;
                _limit += 2;
            }

            public override void Record(double ystart, double yend, int direction)
            {
                if (ystart >= yend)
                {
                    return;
                }
                int cur = 0;
                // Quickly jump over all pairs that are completely "above"
                while (cur < _limit && ystart > _yranges[cur + 1])
                {
                    cur += 2;
                }
                if (cur < _limit)
                {
                    int rdir = _crosscounts[cur / 2];
                    double yrlo = _yranges[cur + 0];
                    double yrhi = _yranges[cur + 1];
                    if (yrhi == ystart && rdir == direction)
                    {
                        // Remove the range from the list and collapse it
                        // into the range being inserted.  Note that the
                        // new combined range may overlap the following range
                        // so we must not simply combine the ranges in place
                        // unless we are at the last range.
                        if (cur + 2 == _limit)
                        {
                            _yranges[cur + 1] = yend;
                            return;
                        }
                        Remove(cur);
                        ystart = yrlo;
                        rdir = _crosscounts[cur / 2];
                        yrlo = _yranges[cur + 0];
                        yrhi = _yranges[cur + 1];
                    }
                    if (yend < yrlo)
                    {
                        // Just insert the new range at the current location
                        Insert(cur, ystart, yend, direction);
                        return;
                    }
                    if (yend == yrlo && rdir == direction)
                    {
                        // Just prepend the new range to the current one
                        _yranges[cur] = ystart;
                        return;
                    }
                    // The ranges must overlap - (yend > yrlo && yrhi > ystart)
                    if (ystart < yrlo)
                    {
                        Insert(cur, ystart, yrlo, direction);
                        cur += 2;
                        ystart = yrlo;
                    }
                    else if (yrlo < ystart)
                    {
                        Insert(cur, yrlo, ystart, rdir);
                        cur += 2;
                    }
                    // assert(yrlo == ystart);
                    int newdir = rdir + direction;
                    double newend = Math.Min(yend, yrhi);
                    if (newdir == 0)
                    {
                        Remove(cur);
                    }
                    else
                    {
                        _crosscounts[cur / 2] = newdir;
                        _yranges[cur++] = ystart;
                        _yranges[cur++] = newend;
                    }
                    ystart = yrlo = newend;
                    if (yrlo < yrhi)
                    {
                        Insert(cur, yrlo, yrhi, rdir);
                    }
                }
                if (ystart < yend)
                {
                    Insert(cur, ystart, yend, direction);
                }
            }
        }
    }

}
