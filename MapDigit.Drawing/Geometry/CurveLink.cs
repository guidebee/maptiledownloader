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
    internal class CurveLink
    {
        readonly Curve _curve;
        double _ytop;
        double _ybot;
        readonly int _etag;
        CurveLink _next;

        public CurveLink(Curve curve, double ystart, double yend, int etag)
        {
            _curve = curve;
            _ytop = ystart;
            _ybot = yend;
            _etag = etag;
            if (_ytop < curve.GetYTop() || _ybot > curve.GetYBot())
            {
                throw new SystemException("bad curvelink [" + _ytop + "=>" + _ybot + "] for " + curve);
            }
        }

        public bool Absorb(CurveLink link)
        {
            return Absorb(link._curve, link._ytop, link._ybot, link._etag);
        }

        public bool Absorb(Curve curve, double ystart, double yend, int etag)
        {
            if (_curve != curve || _etag != etag ||
                    _ybot < ystart || _ytop > yend)
            {
                return false;
            }
            if (ystart < curve.GetYTop() || yend > curve.GetYBot())
            {
                throw new SystemException("bad curvelink [" + ystart + "=>" + yend + "] for " + curve);
            }
            _ytop = Math.Min(_ytop, ystart);
            _ybot = Math.Max(_ybot, yend);
            return true;
        }

        public bool IsEmpty()
        {
            return (_ytop == _ybot);
        }

        public Curve GetCurve()
        {
            return _curve;
        }

        public Curve GetSubCurve()
        {
            if (_ytop == _curve.GetYTop() && _ybot == _curve.GetYBot())
            {
                return _curve.GetWithDirection(_etag);
            }
            return _curve.GetSubCurve(_ytop, _ybot, _etag);
        }

        public Curve GetMoveto()
        {
            return new Order0(GetXTop(), GetYTop());
        }

        public double GetXTop()
        {
            return _curve.XforY(_ytop);
        }

        public double GetYTop()
        {
            return _ytop;
        }

        public double GetXBot()
        {
            return _curve.XforY(_ybot);
        }

        public double GetYBot()
        {
            return _ybot;
        }

        public double GetX()
        {
            return _curve.XforY(_ytop);
        }

        public int GetEdgeTag()
        {
            return _etag;
        }

        public void SetNext(CurveLink link)
        {
            _next = link;
        }

        public CurveLink GetNext()
        {
            return _next;
        }
    }

}
