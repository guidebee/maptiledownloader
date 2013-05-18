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
    internal abstract class AreaOp
    {

        internal abstract class CAGOp : AreaOp
        {

            bool _inLeft;
            bool _inRight;
            bool _inResult;

            public override void NewRow()
            {
                _inLeft = false;
                _inRight = false;
                _inResult = false;
            }

            public override int Classify(Edge e)
            {
                if (e.GetCurveTag() == CTAG_LEFT)
                {
                    _inLeft = !_inLeft;
                }
                else
                {
                    _inRight = !_inRight;
                }
                bool newClass = NewClassification(_inLeft, _inRight);
                if (_inResult == newClass)
                {
                    return ETAG_IGNORE;
                }
                _inResult = newClass;
                return (newClass ? ETAG_ENTER : ETAG_EXIT);
            }

            public override int GetState()
            {
                return (_inResult ? RSTAG_INSIDE : RSTAG_OUTSIDE);
            }

            public abstract bool NewClassification(bool inLeft,
                    bool inRight);
        }

        internal class AddOp : CAGOp
        {

            public override bool NewClassification(bool inLeft, bool inRight)
            {
                return (inLeft || inRight);
            }
        }

        internal class SubOp : CAGOp
        {

            public override bool NewClassification(bool inLeft, bool inRight)
            {
                return (inLeft && !inRight);
            }
        }

        internal class IntOp : CAGOp
        {

            public override bool NewClassification(bool inLeft, bool inRight)
            {
                return (inLeft && inRight);
            }
        }

        internal class XorOp : CAGOp
        {

            public override bool NewClassification(bool inLeft, bool inRight)
            {
                return (inLeft != inRight);
            }
        }

        internal class NzWindOp : AreaOp
        {

            private int _count;

            public override void NewRow()
            {
                _count = 0;
            }

            public override int Classify(Edge e)
            {
                // Note: the right curves should be an empty set with this op...
                // assert(e.getCurveTag() == CTAG_LEFT);
                int newCount = _count;
                int type = (newCount == 0 ? ETAG_ENTER : ETAG_IGNORE);
                newCount += e.GetCurve().GetDirection();
                _count = newCount;
                return (newCount == 0 ? ETAG_EXIT : type);
            }

            public override int GetState()
            {
                return ((_count == 0) ? RSTAG_OUTSIDE : RSTAG_INSIDE);
            }
        }

        internal class EoWindOp : AreaOp
        {

            private bool _inside;

            public override void NewRow()
            {
                _inside = false;
            }

            public override int Classify(Edge e)
            {
                // Note: the right curves should be an empty set with this op...
                // assert(e.getCurveTag() == CTAG_LEFT);
                bool newInside = !_inside;
                _inside = newInside;
                return (newInside ? ETAG_ENTER : ETAG_EXIT);
            }

            public override int GetState()
            {
                return (_inside ? RSTAG_INSIDE : RSTAG_OUTSIDE);
            }
        }

        private AreaOp()
        {
        }

        /* Constants to tag the left and right curves in the edge list */
        public const int CTAG_LEFT = 0;
        public const int CTAG_RIGHT = 1;

        /* Constants to classify edges */
        public const int ETAG_IGNORE = 0;
        public const int ETAG_ENTER = 1;
        public const int ETAG_EXIT = -1;

        /* Constants used to classify result state */
        public const int RSTAG_INSIDE = 1;
        public const int RSTAG_OUTSIDE = -1;

        public abstract void NewRow();

        public abstract int Classify(Edge e);

        public abstract int GetState();

        public ArrayList Calculate(ArrayList left, ArrayList right)
        {
            ArrayList edges = new ArrayList();
            AddEdges(edges, left, CTAG_LEFT);
            AddEdges(edges, right, CTAG_RIGHT);
            edges = PruneEdges(edges);

            return edges;
        }

        private static void AddEdges(ArrayList edges, ArrayList curves, int curvetag)
        {
            IEnumerator enumerator = curves.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Curve c = (Curve)enumerator.Current;
                if (c.GetOrder() > 0)
                {
                    edges.Add(new Edge(c, curvetag));
                }
            }
        }

        private class YXTopComparatorClass : IComparer
        {
            public int Compare(Object o1, Object o2)
            {
                Curve c1 = ((Edge)o1).GetCurve();
                Curve c2 = ((Edge)o2).GetCurve();
                double v1, v2;
                if ((v1 = c1.GetYTop()) == (v2 = c2.GetYTop()))
                {
                    if ((v1 = c1.GetXTop()) == (v2 = c2.GetXTop()))
                    {
                        return 0;
                    }
                }
                if (v1 < v2)
                {
                    return -1;
                }
                return 1;
            }
        }

        private static readonly IComparer YXTopComparator = new YXTopComparatorClass();

        private ArrayList PruneEdges(ArrayList edges)
        {
            var numedges = edges.Count;
            if (numedges < 2)
            {
                return edges;
            }
            var temp = edges.ToArray();
            Edge[] edgelist = new Edge[temp.Length];
            for (int i = 0; i < temp.Length;i++ )
            {
                edgelist[i] = temp[i] as Edge;
            }
            Array.Sort(edgelist, YXTopComparator);

            Edge e;
            var left = 0;
            var right = 0;
            var yrange = new double[2];
            var subcurves = new ArrayList();
            var chains = new ArrayList();
            var links = new ArrayList();
            // Active edges are between left (inclusive) and right (exclusive)
            while (left < numedges)
            {
                double y = yrange[0];
                // Prune active edges that fall off the top of the active y range
                int next;
                int cur;
                for (cur = next = right - 1; cur >= left; cur--)
                {
                    e = edgelist[cur];
                    if (e.GetCurve().GetYBot() > y)
                    {
                        if (next > cur)
                        {
                            edgelist[next] = e;
                        }
                        next--;
                    }
                }
                left = next + 1;
                // Grab a new "top of Y range" if the active edges are empty
                if (left >= right)
                {
                    if (right >= numedges)
                    {
                        break;
                    }
                    y = edgelist[right].GetCurve().GetYTop();
                    if (y > yrange[0])
                    {
                        FinalizeSubCurves(subcurves, chains);
                    }
                    yrange[0] = y;
                }
                // Incorporate new active edges that enter the active y range
                while (right < numedges)
                {
                    e = edgelist[right];
                    if (e.GetCurve().GetYTop() > y)
                    {
                        break;
                    }
                    right++;
                }
                // Sort the current active edges by their X values and
                // determine the maximum valid Y range where the X ordering
                // is correct
                yrange[1] = edgelist[left].GetCurve().GetYBot();
                if (right < numedges)
                {
                    y = edgelist[right].GetCurve().GetYTop();
                    if (yrange[1] > y)
                    {
                        yrange[1] = y;
                    }
                }

                // Note: We could start at left+1, but we need to make
                // sure that edgelist[left] has its equivalence set to 0.
                int nexteq = 1;
                for (cur = left; cur < right; cur++)
                {
                    e = edgelist[cur];
                    e.SetEquivalence(0);
                    for (next = cur; next > left; next--)
                    {
                        Edge prevedge = edgelist[next - 1];
                        int ordering = e.CompareTo(prevedge, yrange);
                        if (yrange[1] <= yrange[0])
                        {
                            throw new SystemException("backstepping to " + yrange[1] +
                                    " from " + yrange[0]);
                        }
                        if (ordering >= 0)
                        {
                            if (ordering == 0)
                            {
                                // If the curves are equal, mark them to be
                                // deleted later if they cancel each other
                                // out so that we avoid having extraneous
                                // curve segments.
                                int eq = prevedge.GetEquivalence();
                                if (eq == 0)
                                {
                                    eq = nexteq++;
                                    prevedge.SetEquivalence(eq);
                                }
                                e.SetEquivalence(eq);
                            }
                            break;
                        }
                        edgelist[next] = prevedge;
                    }
                    edgelist[next] = e;
                }

                // Now prune the active edge list.
                // For each edge in the list, determine its classification
                // (entering shape, exiting shape, ignore - no change) and
                // record the current Y range and its classification in the
                // Edge object for use later in constructing the new outline.
                NewRow();
                double ystart = yrange[0];
                double yend = yrange[1];
                for (cur = left; cur < right; cur++)
                {
                    e = edgelist[cur];
                    int etag;
                    int eq = e.GetEquivalence();
                    if (eq != 0)
                    {
                        // Find one of the segments in the "equal" range
                        // with the right transition state and prefer an
                        // edge that was either active up until ystart
                        // or the edge that extends the furthest downward
                        // (i.e. has the most potential for continuation)
                        int origstate = GetState();
                        etag = (origstate == RSTAG_INSIDE
                                ? ETAG_EXIT
                                : ETAG_ENTER);
                        Edge activematch = null;
                        Edge longestmatch = e;
                        double furthesty = yend;
                        do
                        {
                            // Note: classify() must be called
                            // on every edge we consume here.
                            Classify(e);
                            if (activematch == null &&
                                    e.IsActiveFor(ystart, etag))
                            {
                                activematch = e;
                            }
                            y = e.GetCurve().GetYBot();
                            if (y > furthesty)
                            {
                                longestmatch = e;
                                furthesty = y;
                            }
                        } while (++cur < right &&
                                (e = edgelist[cur]).GetEquivalence() == eq);
                        --cur;
                        if (GetState() == origstate)
                        {
                            etag = ETAG_IGNORE;
                        }
                        else
                        {
                            e = (activematch != null ? activematch : longestmatch);
                        }
                    }
                    else
                    {
                        etag = Classify(e);
                    }
                    if (etag != ETAG_IGNORE)
                    {
                        e.Record(yend, etag);
                        links.Add(new CurveLink(e.GetCurve(), ystart, yend, etag));
                    }
                }
                // assert(getState() == AreaOp.RSTAG_OUTSIDE);
                if (GetState() != RSTAG_OUTSIDE)
                {


                    for (cur = left; cur < right; cur++)
                    {
                        e = edgelist[cur];

                        if (e != null) e.GetEquivalence();
                    }
                }

                ResolveLinks(subcurves, chains, links);
                links.Clear();
                // Finally capture the bottom of the valid Y range as the top
                // of the next Y range.
                yrange[0] = yend;
            }
            FinalizeSubCurves(subcurves, chains);
            ArrayList ret = new ArrayList();
            IEnumerator enumerator = subcurves.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CurveLink link = (CurveLink)enumerator.Current;
                ret.Add(link.GetMoveto());
                CurveLink nextlink = link;
                while ((nextlink = nextlink.GetNext()) != null)
                {
                    if (!link.Absorb(nextlink))
                    {
                        ret.Add(link.GetSubCurve());
                        link = nextlink;
                    }
                }
                ret.Add(link.GetSubCurve());
            }
            return ret;
        }

        public static void FinalizeSubCurves(ArrayList subcurves, ArrayList chains)
        {
            int numchains = chains.Count;
            if (numchains == 0)
            {
                return;
            }
            if ((numchains & 1) != 0)
            {
                throw new SystemException("Odd number of chains!");
            }
            var tempArray = chains.ToArray();

            ChainEnd[] endlist = new ChainEnd[tempArray.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                endlist[i] = tempArray[i] as ChainEnd;
            }

            for (int i = 1; i < numchains; i += 2)
            {
                ChainEnd open = endlist[i - 1];
                ChainEnd close = endlist[i];
                CurveLink subcurve = open.LinkTo(close);
                if (subcurve != null)
                {
                    subcurves.Add(subcurve);
                }
            }
            chains.Clear();
        }
        private static readonly CurveLink[] EmptyLinkList = new CurveLink[2];
        private static readonly ChainEnd[] EmptyChainList = new ChainEnd[2];

        public static void ResolveLinks(ArrayList subcurves,
                ArrayList chains,
                ArrayList links)
        {
            int numlinks = links.Count;
            CurveLink[] linklist;
            if (numlinks == 0)
            {
                linklist = EmptyLinkList;
            }
            else
            {
                if ((numlinks & 1) != 0)
                {
                    throw new SystemException("Odd number of new curves!");
                }
   
                var tempArray = links.ToArray();

                linklist = new CurveLink[numlinks + 2];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    linklist[i] = tempArray[i] as CurveLink;
                }

            }
            int numchains = chains.Count;
            ChainEnd[] endlist;
            if (numchains == 0)
            {
                endlist = EmptyChainList;
            }
            else
            {
                if ((numchains & 1) != 0)
                {
                    throw new SystemException("Odd number of chains!");
                }

                var tempArray = chains.ToArray();

                endlist = new ChainEnd[numchains+2];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    endlist[i] = tempArray[i] as ChainEnd;
                }

            }
            int curchain = 0;
            int curlink = 0;
            chains.Clear();
            ChainEnd chain = endlist[0];
            ChainEnd nextchain = endlist[1];
            CurveLink link = linklist[0];
            CurveLink nextlink = linklist[1];
            while (chain != null || link != null)
            {
                /*
                 * Strategy 1:
                 * Connect chains or links if they are the only things left...
                 */
                bool connectchains = (link == null);
                bool connectlinks = (chain == null);

                if (!connectchains && !connectlinks)
                {
                    // assert(link != null && chain != null);
                    /*
                             * Strategy 2:
                             * Connect chains or links if they close off an open area...
                             */
                    if (nextchain != null)
                        connectchains = ((curchain & 1) == 0 &&
                                         chain.GetX() == nextchain.GetX());
                    if (nextlink != null)
                    {
                        connectlinks = ((curlink & 1) == 0 &&
                                        link.GetX() == nextlink.GetX());

                        if (!connectchains && !connectlinks)
                        {
                            /*
                         * Strategy 3:
                         * Connect chains or links if their successor is
                         * between them and their potential connectee...
                         */
                            double cx = chain.GetX();
                            double lx = link.GetX();
                            connectchains =
                                (nextchain != null && cx < lx &&
                                 Obstructs(nextchain.GetX(), lx, curchain));
                            connectlinks =
                                (lx < cx &&
                                 Obstructs(nextlink.GetX(), cx, curlink));
                        }
                    }
                }
                if (connectchains)
                {
                    CurveLink subcurve = chain.LinkTo(nextchain);
                    if (subcurve != null)
                    {
                        subcurves.Add(subcurve);
                    }
                    curchain += 2;
                    chain = endlist[curchain];
                    nextchain = endlist[curchain + 1];
                }
                if (connectlinks)
                {
                    ChainEnd openend = new ChainEnd(link, null);
                    ChainEnd closeend = new ChainEnd(nextlink, openend);
                    openend.SetOtherEnd(closeend);
                    chains.Add(openend);
                    chains.Add(closeend);
                    curlink += 2;
                    link = linklist[curlink];
                    nextlink = linklist[curlink + 1];
                }
                if (!connectchains && !connectlinks)
                {
                    // assert(link != null);
                    // assert(chain != null);
                    // assert(chain.getEtag() == link.getEtag());
                    chain.AddLink(link);
                    chains.Add(chain);
                    curchain++;
                    chain = nextchain;
                    nextchain = endlist[curchain + 1];
                    curlink++;
                    link = nextlink;
                    nextlink = linklist[curlink + 1];
                }
            }
        }

        /*
         * Does the position of the next edge at v1 "obstruct" the
         * connectivity between current edge and the potential
         * partner edge which is positioned at v2?
         *
         * Phase tells us whether we are testing for a transition
         * into or out of the interior part of the resulting area.
         *
         * Require 4-connected continuity if this edge and the partner
         * edge are both "entering into" type edges
         * Allow 8-connected continuity for "exiting from" type edges
         */
        public static bool Obstructs(double v1, double v2, int phase)
        {
            return (((phase & 1) == 0) ? (v1 <= v2) : (v1 < v2));
        }
    }

}
