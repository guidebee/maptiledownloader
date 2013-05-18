//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 18JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Class MapCollection stands for a collection of map objects.
     * <p></p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class MapCollection : MapObject
    {

        /**
         * The multiPoint part of the collection.
         */
        public MapMultiPoint MultiPoint;

        /**
         * The multiPline part of the collection.
         */
        public MapMultiPline MultiPline;

        /**
         * The multiRegion part of the collection.
         */
        public MapMultiRegion MultiRegion;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Copy constructor.
         * @param mapCollection     map object copy from.
         */
        public MapCollection(MapCollection mapCollection)
            : base(mapCollection)
        {

            SetMapObjectType(COLLECTION);
            MultiPline = null;
            MultiPoint = null;
            MultiRegion = null;
            if (mapCollection.MultiPline != null)
            {
                MultiPline = new MapMultiPline(mapCollection.MultiPline);
            }
            if (mapCollection.MultiPoint != null)
            {
                MultiPoint = new MapMultiPoint(mapCollection.MultiPoint);
            }
            if (mapCollection.MultiRegion != null)
            {
                MultiRegion = new MapMultiRegion(mapCollection.MultiRegion);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.
         */
        public MapCollection()
        {

            SetMapObjectType(COLLECTION);
            MultiPline = null;
            MultiPoint = null;
            MultiRegion = null;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the multipoint part the collection.
         * @return the multipoint part the collection
         */
        public MapMultiPoint GetMultiPoint()
        {
            return MultiPoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the multipoint part the collection.
         * @param multiPoint  the multipoint part the collection.
         */
        public void SetMultiPoint(MapMultiPoint multiPoint)
        {
            MultiPoint = multiPoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the MultiPline part the collection.
         * @return the MultiPline part the collection
         */
        public MapMultiPline GetMultiPline()
        {
            return MultiPline;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the multiPline part the collection.
         * @param multiPline  the multiPline part the collection.
         */
        public void SetMultiPline(MapMultiPline multiPline)
        {
            MultiPline = multiPline;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the multiRegion part the collection.
         * @return the multiRegion part the collection
         */
        public MapMultiRegion GetMultiRegion()
        {
            return MultiRegion;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set the multiRegion part the collection.
         * @param multiRegion  the multiRegion part the collection.
         */
        public void SetMultiRegion(MapMultiRegion multiRegion)
        {
            MultiRegion = multiRegion;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Convert to MapInfo string.
         * @return a MapInfo MIF string.
         */
        public  override string ToString()
        {
            string retStr = "COLLECTION ";
            int collectionPart = 0;
            if (MultiPoint != null)
            {
                collectionPart++;
            }
            if (MultiPline != null)
            {
                collectionPart++;
            }
            if (MultiRegion != null)
            {
                collectionPart++;
            }

            retStr += collectionPart + CRLF;
            if (MultiRegion != null)
            {
                retStr += MultiRegion.ToString();
            }

            if (MultiPline != null)
            {
                retStr += MultiPline.ToString();
            }
            if (MultiPoint != null)
            {
                retStr += MultiPoint.ToString();
            }
            return retStr;
        }

    }

}
