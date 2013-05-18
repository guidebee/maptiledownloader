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
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The <code>NoninvertibleTransformException</code> class represents
     * an exception that is thrown if an operation is performed requiring
     * the inverse of an {@link AffineTransform} object but the 
     * <code>AffineTransform</code> is in a non-invertible state.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class NoninvertibleTransformException : Exception
    {
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs an instance of
         * <code>NoninvertibleTransformException</code>
         * with the specified detail message.
         * @param   s     the detail message
         */
        public NoninvertibleTransformException(String s)
            : base(s)
        {

        }
    }

}
