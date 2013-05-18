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
     * Implements basic functions of Node interface. Also implements splitting
     * functions.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public abstract class AbstractNode : INode
    {

        /**
         * Level of this node. Leaves always have a level equal to 0.
         */
        public int Level;
        /**
         * Parent of all nodes.
         */
        public RTree Tree;
        /**
         * The pageNumber where the parent of this node is stored.
         */
        public int Parent;
        /**
         * The pageNumber where this node is stored.
         */
        public int PageNumber;
        /**
         * All node data are stored into this array. It must have a size of
         * <B>nodeCapacity + 1</B> to hold
         * all data plus an overflow HyperCube, when the node must be split.
         */
        public HyperCube[] Data;
        /**
         * Holds the pageNumbers containing the children of this node.
         * Always has same size with the data array.
         * If this is a Leaf node, than all branches should point to the real
         * data objects.
         */
        public int[] Branches;
        /**
         * How much space is used up into this node. If equal to nodeCapacity
         * then node is full.
         */
        public int UsedSpace;


        //
        // Node interface.
        //
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the node level. Always zero for leaf nodes.
         *
         * @return Level of node.
         */
        public int GetLevel()
        {
            return Level;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  Returns true if this node is the root node.
         */
        public bool IsRoot()
        {
            return (Parent == RTree.NIL);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  Returns true if this node is an Index. Root node is an Index too, 
         *  unless it is a Leaf.
         */
        public bool IsIndex()
        {
            return (Level != 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns true if this node is a Leaf. Root may be a Leaf too.
         */
        public bool IsLeaf()
        {
            return (Level == 0);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the mbb of all HyperCubes present in this node.
         *
         * @return A new HyperCube object, representing the mbb of this node.
         */
        public HyperCube GetNodeMbb()
        {
            if (UsedSpace > 0)
            {
                HyperCube[] h = new HyperCube[UsedSpace];
                Array.Copy(Data, 0, h, 0, UsedSpace);
                return HyperCube.GetUnionMbb(h);
            }
            return new HyperCube(new Point(new double[] { 0, 0 }),
                                 new Point(new double[] { 0, 0 }));
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a unique id for this node. The page number is unique for every 
         * node.
         *
         * @return A string representing a unique id for this node.
         */
        public string GetUniqueId()
        {
            return PageNumber.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the parent of this node. If there is a parent, it must be an 
         * Index.If this node is the root, returns null. This function loads one 
         * disk page into main memory.
         */
        public AbstractNode GetParent()
        {
            if (IsRoot())
            {
                return null;
            }
            return Tree._file.ReadNode(Parent);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Return a copy of the HyperCubes present in this node.
         * @return An array of HyperCubes containing copies of the original data.
         */
        public HyperCube[] GetHyperCubes()
        {
            HyperCube[] h = new HyperCube[UsedSpace];

            for (int i = 0; i < UsedSpace; i++)
            {
                h[i] = (HyperCube)Data[i].Clone();
            }

            return h;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * to a string.
         */
        public override string ToString()
        {
            string s = "< Page: " + PageNumber + ", Level: "
                    + Level + ", UsedSpace: " + UsedSpace
                    + ", Parent: " + Parent + " >\n";

            for (int i = 0; i < UsedSpace; i++)
            {
                s += "  " + (i + 1) + ") " + Data[i].ToString()
                        + " --> " + " page: " + Branches[i] + "\n";
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        protected AbstractNode(RTree tree, int parent, int pageNumber, int level)
        {
            Parent = parent;
            Tree = tree;
            PageNumber = pageNumber;
            Level = level;
            Data = new HyperCube[tree.GetNodeCapacity() + 1];
            Branches = new int[tree.GetNodeCapacity() + 1];
            UsedSpace = 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * chooseLeaf finds the most appropriate leaf where the given HyperCube 
         * should be stored.
         *
         * @param h The new HyperCube.
         *
         * @return The leaf where the new HyperCube should be inserted.
         */
        internal abstract Leaf ChooseLeaf(HyperCube h);

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21DEC2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * findLeaf returns the leaf that Contains the given hypercube, null if the
         * hypercube is not contained in any of the leaves of this node.
         *
         * @param h The HyperCube to search for.
         *
         * @return The leaf where the HyperCube is contained, null if such a leaf is not found.
         */
        internal abstract Leaf FindLeaf(HyperCube h);
    }



}
