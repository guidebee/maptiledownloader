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
     * Internal node of the RTree. Used to access Leaf nodes, where real data lies.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */ 
    public class Index : AbstractNode {

    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Retrieves the <B>i-th</B> child node. Loads one page into main memory.
     *
     * @param  i The index of the child in the data array.
     * @return The i-th child.
     */
    public AbstractNode GetChild(int i) {
        if (i < 0 || i >= UsedSpace) {
            throw new IndexOutOfRangeException("" + i);
        }

        return Tree._file.ReadNode(Branches[i]);
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
    internal Index(RTree tree, int parent, int pageNumber, int level):base(tree, parent, pageNumber, level) {
     
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
    internal override Leaf ChooseLeaf(HyperCube h)
    {
        int i;
        
        switch (Tree.GetTreeType()) {
            case RTree.RTREE_LINEAR:
            case RTree.RTREE_QUADRATIC:
            case RTree.RTREE_EXPONENTIAL:
                i = FindLeastEnlargement(h);
                break;
            case RTree.RSTAR:
                if (Level == 1) {
                    // if this node points to leaves...
                    i = FindLeastOverlap(h);
                } else {
                    i = FindLeastEnlargement(h);
                }
                break;
            default:
                throw new Exception("Invalid tree type.");
        }
        
        return GetChild(i).ChooseLeaf(h);
    }
    
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Add the new HyperCube to all mbbs present in this node. Calculate the 
     * area difference and choose the entry with the least enlargement. Based 
     * on that metric we choose the path that leads to the leaf that will 
     * hold the new HyperCube.
     * [A. Guttman 'R-trees a dynamic index structure for spatial searching']
     *
     * @return The index of the branch of the path that leads to the Leaf where 
     * the new HyperCube should be inserted.
     */
    private int FindLeastEnlargement(HyperCube h) {
        double area = Double.PositiveInfinity;
        int sel = -1;
        
        for (int i = 0; i < UsedSpace; i++) {
            double enl = Data[i].GetUnionMbb(h).GetArea() - Data[i].GetArea();
            if (enl < area) {
                area = enl;
                sel = i;
            } else if (enl == area) {
                sel = (Data[sel].GetArea() <= Data[i].GetArea()) ? sel : i;
            }
        }
        return sel;
    }
    
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * R*-tree criterion for choosing the best branch to follow.
     * [Beckmann, Kriegel, Schneider, Seeger 'The R*-tree: An efficient and 
     * Robust Access Method for Points and Rectangles]
     *
     * @return The index of the branch of the path that leads to the Leaf where 
     * the new HyperCube should be inserted.
     */
    private int FindLeastOverlap(HyperCube h) {
        float overlap = float.PositiveInfinity;
        int sel = -1;
        
        for (int i = 0; i < UsedSpace; i++) {
            AbstractNode n = GetChild(i);
            float o = 0;
            for (int j = 0; j < n.Data.Length; j++) {
                o += (float)h.IntersectingArea(n.Data[j]);
            }
            if (o < overlap) {
                overlap = o;
                sel = i;
            } else if (o == overlap) {
                double area1 = Data[i].GetUnionMbb(h).GetArea() 
                    - Data[i].GetArea();
                double area2 = Data[sel].GetUnionMbb(h).GetArea()
                    - Data[sel].GetArea();
                
                if (area1 == area2) {
                    sel = (Data[sel].GetArea() <= Data[i].GetArea()) ? sel : i;
                } else {
                    sel = (area1 < area2) ? i : sel;
                }
            }
        }
        return sel;
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * findLeaf returns the leaf that Contains the given hypercube, null if the
     * hypercube is not contained in any of the leaves of this node.
     * @param h The HyperCube to search for.
     * @return The leaf where the HyperCube is contained, null if such a leaf 
     * is not found.
     */
    internal override Leaf FindLeaf(HyperCube h)
    {
        for (int i = 0; i < UsedSpace; i++) {
            if (Data[i].Enclosure(h)) {
                Leaf l = GetChild(i).FindLeaf(h);
                if (l != null) {
                    return l;
                }
            }
        }
        
        return null;
    }
    
    
}


}
