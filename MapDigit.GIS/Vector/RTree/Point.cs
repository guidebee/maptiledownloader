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
     * A point in the n-dimensional space. All dimensions are stored in an array of
     * doubles.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00,21/06/09
     * @author      Guidebee, Inc.
     */
    public class Point {
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * constructor.
     */
    public Point(double[] d) {
        if (d == null) throw new
                ArgumentException("Coordinates cannot be null.");
        
        if (d.Length < 2) throw new
                ArgumentException
                ("Point dimension should be greater than 1.");
        
        data = new double[d.Length];
        Array.Copy(d, 0, data, 0, d.Length);
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
    public Point(int[] d) {
        if (d == null) throw new
                ArgumentException("Coordinates cannot be null.");
        
        if (d.Length < 2) throw new
                ArgumentException
                ("Point dimension should be greater than 1.");
        
        data = new double[d.Length];
        
        for (int i = 0; i < d.Length; i++) {
            data[i] = d[i];
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * getdoubleCoordinate.
     */
    public double GetFloatCoordinate(int i) {
        return data[i];
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * getIntCoordinate.
     */
    public int GetIntCoordinate(int i) {
        return (int) data[i];
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////
    /**
     * getDimension.
     */
    public int GetDimension() {
        return data.Length;
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Equals.
     */
    public bool Equals(Point p) {
        if (p.GetDimension() != GetDimension()) {
            throw new ArgumentException("Points must be of equal dimensions to be compared.");
        }
        
        bool ret = true;
        for (int i = 0; i < GetDimension(); i++) {
            if (GetFloatCoordinate(i) != p.GetFloatCoordinate(i)) {
                ret = false;
                break;
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
     * Clone.
     */
    public object Clone() {
        double[] f = new double[data.Length];
        Array.Copy(data, 0, f, 0, data.Length);
        
        return new Point(f);
    }
    
    ////////////////////////////////////////////////////////////////////////////
    //--------------------------------- REVISIONS ------------------------------
    // Date       Name                 Tracking #         Description
    // ---------  -------------------  -------------      ----------------------
    // 21DEC2008  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * ToString.
     */
    public override string ToString() {
        string s = "(" + data[0];
        
        for (int i = 1; i < data.Length; i++) {
            s += ", " + data[i];
        }
        
        return s + ")";
    }


    /**
     * dimensions data.
     */
    private double[] data;

    
}

}
