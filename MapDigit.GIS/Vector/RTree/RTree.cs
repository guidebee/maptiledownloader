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

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector.RTree
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * To create a new RTree use the first two constructors. You must specify the
     * dimension, the fill factor as a float between 0 and 0.5 (0 to 50% capacity)
     * and the variant of the RTree which is one of:
     * <ul>
     *  <li>RTREE_QUADRATIC</li>
     * </ul>
     * The first constructor creates by default a new memory resident page file.
     * The second constructor takes
     * the page file as an argument. If the given page file is not empty,
     * then all data are deleted.
     * <p>
     * The third constructor initializes the RTree from an already filled page file.
     * Thus, you may store the
     * RTree into a persistent page file and recreate it again at any time.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class RTree
    {

        /**
        * version
        */
        public const String VERSION = "2.009";
        /**
         * date
         */
        public const String DATE = "December 21st 2008";

        /**
         * Page file where data is stored.
         */
        internal PageFile _file;

        /**
         * static identifier used for the parent of the root node.
         */
        public const int NIL = -1;

        /**
         * Available RTree variants
         */
        public const int RTREE_LINEAR = 0;
        public const int RTREE_QUADRATIC = 1;
        public const int RTREE_EXPONENTIAL = 2;
        public const int RSTAR = 3;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Creates an rtree from an already initialized page file, probably stored
         * into persistent storage.
         */
        public RTree(PageFile file)
        {
            if (file.Tree != null)
            {
                throw new ArgumentException
                        ("PageFile already in use by another rtree instance.");
            }

            if (file.TreeType == -1)
            {
                throw new ArgumentException
                        ("PageFile is empty. Use some other RTree constructor.");
            }

            file.Tree = this;
            _file = file;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Retruns the maximun capacity of each Node.
         */
        public int GetNodeCapacity()
        {
            return _file.NodeCapacity;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the percentage between 0 and 0.5, used to calculate minimum 
         * number of entries present in each node.
         */
        public double GetFillFactor()
        {
            return _file.FillFactor;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the data dimension.
         */
        public int GetDimension()
        {
            return _file.Dimension;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the page Length.
         */
        public int GetPageSize()
        {
            return _file.PageSize;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the level of the root Node, which signifies the level of the 
         * whole tree. Loads one page into main memory.
         */
        public int GetTreeLevel()
        {
            return _file.ReadNode(0).GetLevel();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the RTree variant used.
         */
        public int GetTreeType()
        {
            return _file.TreeType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a ArrayList containing all tree nodes from bottom to top, left to 
         * right.
         * CAUTION: If the tree is not memory resident, all nodes will be loaded 
         * into main memory.
         *
         * @param root The node from which the traverse should begin.
         * @return A ArrayList containing all Nodes in the correct order.
         */
        public ArrayList TraverseByLevel(AbstractNode root)
        {
            if (root == null)
            {
                throw new ArgumentException("Node cannot be null.");
            }

            ArrayList ret = new ArrayList();
            ArrayList v = TraversePostOrder(root);

            for (int i = 0; i <= GetTreeLevel(); i++)
            {
                ArrayList a = new ArrayList();
                for (int j = 0; j < v.Count; j++)
                {
                    INode n = (INode)v[j];
                    if (n.GetLevel() == i)
                    {
                        a.Add(n);
                    }
                }
                for (int j = 0; j < a.Count; j++)
                {
                    ret.Add(a[j]);
                }
            }

            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an Enumeration containing all tree nodes from bottom to top, 
         * left to right.
         *
         * @return An Enumeration containing all Nodes in the correct order.
         */
        class ByLevelEnum : IEnumeration
        {
            // there is at least one node, the root node.
            private bool _hasNext = true;

            private readonly ArrayList _nodes;

            private int _index;

            public ByLevelEnum(RTree tree)
            {
                AbstractNode root = tree._file.ReadNode(0);
                _nodes = tree.TraverseByLevel(root);
            }

            public bool HasMoreElements()
            {
                return _hasNext;
            }

            public Object NextElement()
            {
                if (!_hasNext)
                {
                    throw new ArgumentOutOfRangeException("traver" + "seByLevel");
                }

                Object n = _nodes[_index];
                _index++;
                if (_index == _nodes.Count)
                {
                    _hasNext = false;
                }
                return n;
            }
        };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Post order traverse of tree nodes.
         * CAUTION: If the tree is not memory resident, all nodes will be loaded 
         * into main memory.
         *
         * @param root The node where the traversing should begin.
         * @return A ArrayList containing all tree nodes in the correct order.
         */
        public ArrayList TraversePostOrder(AbstractNode root)
        {
            if (root == null)
            {
                throw new ArgumentException("Node cannot be null.");
            }

            ArrayList v = new ArrayList {root};

            if (root.IsLeaf())
            {
            }
            else
            {
                for (int i = 0; i < root.UsedSpace; i++)
                {
                    ArrayList a = TraversePostOrder(((Index)root).GetChild(i));
                    for (int j = 0; j < a.Count; j++)
                    {
                        v.Add(a[j]);
                    }
                }
            }
            return v;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Post order traverse of all tree nodes, begging with root.
         * CAUTION: If the tree is not memory resident, all nodes will be loaded 
         * into main memory.
         *
         * @return An Enumeration containing all tree nodes in the correct order.
         */
        class PostOrderEnum : IEnumeration
        {
            private bool _hasNext = true;

            private readonly ArrayList _nodes;

            private int _index;

            public PostOrderEnum(RTree tree)
            {
                AbstractNode root = tree._file.ReadNode(0);
                _nodes = tree.TraversePostOrder(root);
            }

            public bool HasMoreElements()
            {
                return _hasNext;
            }

            public Object NextElement()
            {
                if (!_hasNext)
                {
                    throw new ArgumentOutOfRangeException("trav" + "erse PostOrder");
                }

                Object n = _nodes[_index];
                _index++;
                if (_index == _nodes.Count)
                {
                    _hasNext = false;
                }
                return n;
            }
        };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Pre order traverse of tree nodes.
         * CAUTION: If the tree is not memory resident, all nodes will be loaded 
         * into main memory.
         *
         * @param root The node where the traversing should begin.
         * @return A ArrayList containing all tree nodes in the correct order.
         */
        public ArrayList TraversePreOrder(AbstractNode root)
        {
            if (root == null)
            {
                throw new ArgumentException("Node cannot be null.");
            }

            ArrayList v = new ArrayList();

            if (root.IsLeaf())
            {
                v.Add(root);
            }
            else
            {
                for (int i = 0; i < root.UsedSpace; i++)
                {
                    ArrayList a = TraversePreOrder(((Index)root).GetChild(i));
                    for (int j = 0; j < a.Count; j++)
                    {
                        v.Add(a[j]);
                    }
                }
                v.Add(root);
            }
            return v;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Pre order traverse of all tree nodes, begging with root.
         * CAUTION: If the tree is not memory resident, all nodes will be loaded 
         * into main memory.
         *
         * @return An Enumeration containing all tree nodes in the correct order.
         */
        class PreOrderEnum : IEnumeration
        {
            private bool _hasNext = true;
            private readonly ArrayList _nodes;
            private int _index;
            public PreOrderEnum(RTree tree)
            {
                AbstractNode root = tree._file.ReadNode(0);
                _nodes = tree.TraversePreOrder(root);
            }

            public bool HasMoreElements()
            {
                return _hasNext;
            }

            public Object NextElement()
            {
                if (!_hasNext)
                {
                    throw new ArgumentOutOfRangeException("traverseP" + "reOrder");
                }

                Object n = _nodes[_index];
                _index++;
                if (_index == _nodes.Count)
                {
                    _hasNext = false;
                }
                return n;
            }
        };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a ArrayList with all nodes that Intersect with the given HyperCube.
         * The nodes are returned in post order traversing
         *
         * @param h The given HyperCube that is tested for overlapping.
         * @param root The node where the search should begin.
         * @return A ArrayList containing the appropriate nodes in the correct order.
         */
        public ArrayList Intersection(HyperCube h, AbstractNode root)
        {
            if (h == null || root == null)
            {
                throw new ArgumentException("Arguments cannot be null.");
            }

            if (h.GetDimension() != _file.Dimension)
            {
                throw new ArgumentException
                        ("HyperCube dimension different than RTree dimension.");
            }

            ArrayList v = new ArrayList();

            if (root.GetNodeMbb().Intersection(h))
            {
                v.Add(root);

                if (!root.IsLeaf())
                {
                    for (int i = 0; i < root.UsedSpace; i++)
                    {
                        if (root.Data[i].Intersection(h))
                        {
                            ArrayList a = Intersection(h, ((Index)root).GetChild(i));
                            for (int j = 0; j < a.Count; j++)
                            {
                                v.Add(a[j]);
                            }
                        }
                    }
                }
            }
            return v;
        }

        ////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS --------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ------------------
        // 21JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////
        /**
        * Returns an Enumeration with all Hypercubes present in the tree that contain the given
        * HyperCube. The HyperCubes are returned in post order traversing, according to the Nodes where
        * they belong.
        *
        * @param h The given HyperCube.
        * @param root The node where the search should begin.
        * @return An Enumeration containing the appropriate HyperCubes in the correct order.
        */
        public IEnumeration Enclosure(HyperCube h)
        {

            return new ContainEnum(h, this);
        }

        ////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS --------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ------------------
        // 21JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////
        /**
        * Returns an Enumeration containing all tree nodes from bottom to top, left to right.
        *
        * @return An Enumeration containing all Nodes in the correct order.
        */

        public IEnumeration TraverseByLevel()
        {
            return new ByLevelEnum(this);
        }

        ////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS --------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ------------------
        // 21JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////
        /**
        * Post order traverse of all tree nodes, begging with root.
        * CAUTION: If the tree is not memory resident, all nodes will be loaded into main memory.
        *
        * @return An Enumeration containing all tree nodes in the correct order.
        */
        public IEnumeration TraversePostOrder()
        {
            return new PostOrderEnum(this);
        }

        ////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS --------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ------------------
        // 21JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////
        /**
        * Pre order traverse of all tree nodes, begging with root.
        * CAUTION: If the tree is not memory resident, all nodes will be loaded into main memory.
        *
        * @return An Enumeration containing all tree nodes in the correct order.
        */
        public IEnumeration TraversePreOrder()
        {
            return new PreOrderEnum(this);
        }

        ////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS --------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ------------------
        // 21JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////
        /**
        * Returns an Enumeration with all nodes present in the tree that Intersect with the given
        * HyperCube. The nodes are returned in post order traversing
        *
        * @param h The given HyperCube that is tested for overlapping.
        * @return An Enumeration containing the appropriate nodes in the correct order.
        */
        public IEnumeration Intersection(HyperCube h)
        {
            return new IntersectionEnum(h, this);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an Enumeration with all nodes present in the tree that Intersect 
         * with the given HyperCube. The nodes are returned in post order traversing
         *
         * @param h The given HyperCube that is tested for overlapping.
         * @return An Enumeration containing the appropriate nodes in the correct 
         * order.
         */
        class IntersectionEnum : IEnumeration
        {
            private bool _hasNext = true;

            private readonly ArrayList _nodes;

            private int _index;

            public IntersectionEnum(HyperCube hh, RTree tree)
            {
                _nodes = tree.Intersection(hh, tree._file.ReadNode(0));
                if (_nodes.Count == 0)
                {
                    _hasNext = false;
                }
            }

            public bool HasMoreElements()
            {
                return _hasNext;
            }

            public Object NextElement()
            {
                if (!_hasNext)
                {
                    throw new ArgumentOutOfRangeException("intersec" + "tion");
                }

                Object c = _nodes[_index];
                _index++;
                if (_index == _nodes.Count)
                {
                    _hasNext = false;
                }
                return c;
            }
        };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a ArrayList with all Hypercubes that completely contain HyperCube 
         * <B>h</B>.
         * The HyperCubes are returned in post order traversing, according to the 
         * Nodes where they belong.
         *
         * @param h The given HyperCube.
         * @param root The node where the search should begin.
         * @return A ArrayList containing the appropriate HyperCubes in the correct 
         * order.
         */
        public ArrayList Enclosure(HyperCube h, AbstractNode root)
        {
            if (h == null || root == null) throw new
                    ArgumentException("Arguments cannot be null.");

            if (h.GetDimension() != _file.Dimension) throw new
                    ArgumentException("HyperCube dimension " +
                    "different than RTree dimension.");

            ArrayList v = new ArrayList();

            if (root.GetNodeMbb().Enclosure(h))
            {
                v.Add(root);

                if (!root.IsLeaf())
                {
                    for (int i = 0; i < root.UsedSpace; i++)
                    {
                        if (root.Data[i].Enclosure(h))
                        {
                            ArrayList a = Enclosure(h, ((Index)root).GetChild(i));
                            for (int j = 0; j < a.Count; j++)
                            {
                                v.Add(a[j]);
                            }
                        }
                    }
                }
            }
            return v;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an Enumeration with all Hypercubes present in the tree that 
         * contain the given HyperCube. The HyperCubes are returned in post order 
         * traversing, according to the Nodes where they belong.
         *
         * @param h The given HyperCube.
         * @return An Enumeration containing the appropriate HyperCubes in the
         * correct order.
         */
        class ContainEnum : IEnumeration
        {
            private bool _hasNext = true;

            private readonly ArrayList _cubes;

            private int _index;

            public ContainEnum(HyperCube hh, RTree tree)
            {
                _cubes = tree.Enclosure(hh, tree._file.ReadNode(0));
                if (_cubes.Count == 0)
                {
                    _hasNext = false;
                }
            }

            public bool HasMoreElements()
            {
                return _hasNext;
            }

            public Object NextElement()
            {
                if (!_hasNext)
                {
                    throw new
                        ArgumentOutOfRangeException("encl" + "osure");
                }

                Object c = _cubes[_index];
                _index++;
                if (_index == _cubes.Count)
                {
                    _hasNext = false;
                }
                return c;
            }
        };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a ArrayList with all Hypercubes that completely contain point 
         * <B>p</B>.
         * The HyperCubes are returned in post order traversing, according to the
         * Nodes where they belong.
         *
         * @param p The given point.
         * @param root The node where the search should begin.
         * @return A ArrayList containing the appropriate HyperCubes in the correct 
         * order.
         */
        public ArrayList Enclosure(Point p, AbstractNode root)
        {
            return Enclosure(new HyperCube(p, p), root);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an Enumeration with all Hypercubes present in the tree that 
         * contain the given
         * point. The HyperCubes are returned in post order traversing, according 
         * to the Nodes where they belong.
         *
         * @param p The query point.
         * @return An Enumeration containing the appropriate HyperCubes in the correct order.
         */
        public IEnumeration Enclosure(Point p)
        {
            return Enclosure(new HyperCube(p, p));
        }

    }

}
