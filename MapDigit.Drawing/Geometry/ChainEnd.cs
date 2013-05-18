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
    internal class ChainEnd
    {

        CurveLink _head;
        CurveLink _tail;
        ChainEnd _partner;
        int _etag;

        public ChainEnd(CurveLink first, ChainEnd partner)
        {
            _head = first;
            _tail = first;
            _partner = partner;
            _etag = first.GetEdgeTag();
        }

        public CurveLink GetChain()
        {
            return _head;
        }

        public void SetOtherEnd(ChainEnd partner)
        {
            _partner = partner;
        }

        public ChainEnd GetPartner()
        {
            return _partner;
        }

        /*
         * Returns head of a complete chain to be added to subcurves
         * or null if the links did not complete such a chain.
         */
        public CurveLink LinkTo(ChainEnd that)
        {
            if (_etag == AreaOp.ETAG_IGNORE ||
                    that._etag == AreaOp.ETAG_IGNORE)
            {
                throw new SystemException("ChainEnd linked more than once!");
            }
            if (_etag == that._etag)
            {
                throw new SystemException("Linking chains of the same type!");
            }
            ChainEnd enter, exit;
            // assert(partner.etag != that.partner.etag);
            if (_etag == AreaOp.ETAG_ENTER)
            {
                enter = this;
                exit = that;
            }
            else
            {
                enter = that;
                exit = this;
            }
            // Now make sure these ChainEnds are not linked to any others...
            _etag = AreaOp.ETAG_IGNORE;
            that._etag = AreaOp.ETAG_IGNORE;
            // Now link everything up...
            enter._tail.SetNext(exit._head);
            enter._tail = exit._tail;
            if (_partner == that)
            {
                // Curve has closed on itself...
                return enter._head;
            }
            // Link this chain into one end of the chain formed by the partners
            ChainEnd otherenter = exit._partner;
            ChainEnd otherexit = enter._partner;
            otherenter._partner = otherexit;
            otherexit._partner = otherenter;
            if (enter._head.GetYTop() < otherenter._head.GetYTop())
            {
                enter._tail.SetNext(otherenter._head);
                otherenter._head = enter._head;
            }
            else
            {
                otherexit._tail.SetNext(enter._head);
                otherexit._tail = enter._tail;
            }
            return null;
        }

        public void AddLink(CurveLink newlink)
        {
            if (_etag == AreaOp.ETAG_ENTER)
            {
                _tail.SetNext(newlink);
                _tail = newlink;
            }
            else
            {
                newlink.SetNext(_head);
                _head = newlink;
            }
        }

        public double GetX()
        {
            if (_etag == AreaOp.ETAG_ENTER)
            {
                return _tail.GetXBot();
            }
            return _head.GetXBot();
        }
    }

}
