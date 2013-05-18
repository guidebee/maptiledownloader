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
using System;
using MapDigit.Drawing;
using MapDigit.GIS.Drawing;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Raster
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * map configuration.
     * <p>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class MapConfiguration
    {

        /**
         * is cache on or not.
         */
        public const int IS_CACHE_ON = 1;

        /**
         * when draw route, only draw waypoint.
         */
        public const int DRAW_ROUTE_WAYPOINT_ONLY = 2;

        /**
         * the no of worker thread.
         */
        public const int WORKER_THREAD_NUMBER = 3;

        /**
         * the map cache size.
         */
        public const int MAP_CACHE_SIZE_IN_BYTES = 4;

        /**
         * the direction render size = 256/MAP_DIRECTION_RENDER_BLOCKS;
         */
        public const int MAP_DIRECTION_RENDER_BLOCKS = 5;

        /**
         * draw route or not.
         */
        public const int DISABLE_ROUTE_RENDER = 6;

        /**
         * route start icon.
         */
        public const int ROUTE_START_ICON = 7;

        /**
         * route middle icon.
         */
        public const int ROUTE_MIDDLE_ICON = 8;

        /**
         * route end icon.
         */
        public const int ROUTE_END_ICON = 9;

        /**
         * route draw pen.
         */
        public const int ROUTE_DRAW_PEN = 10;



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set map configuration parameter.
         * @param field
         * @param value
         */
        public static void SetParameter(int field, bool value)
        {
            switch (field)
            {
                case IS_CACHE_ON:
                    IsCacheOn = value;
                    break;
                case DRAW_ROUTE_WAYPOINT_ONLY:
                    DrawRouteWaypointOnly = value;
                    break;
                case DISABLE_ROUTE_RENDER:
                    DrawRouting = true;
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Set map configuration parameter.
         * @param field
         * @param value
         */
        public static void SetParameter(int field, int value)
        {
            switch (field)
            {
                case WORKER_THREAD_NUMBER:
                    if (value < 0 || value > 8)
                    {
                        throw new ArgumentException("Thread no should between 1 and 8");
                    }
                    WorkerThreadNumber = value;
                    break;
                case MAP_CACHE_SIZE_IN_BYTES:
                    if (value < 0 && IsCacheOn)
                    {
                        throw new ArgumentException("Cache size shall be great than 0");
                    }
                    MapCacheSizeInBytes = value * 1024;
                    break;
                case MAP_DIRECTION_RENDER_BLOCKS:
                    if (!(value == 1 || value == 2 ||
                            value == 4))
                    {
                        throw new ArgumentException("block size should be 1, or 2, or 4");
                    }
                    MapDirectionRenderBlocks = value;
                    break;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        public static void SetParameter(int field, Object value)
        {
            switch (field)
            {
                case ROUTE_START_ICON:
                    if (value is IImage)
                    {
                        StartIcon = (IImage)value;
                    }
                    break;
                case ROUTE_MIDDLE_ICON:
                    if (value is IImage)
                    {
                        MiddleIcon = (IImage)value;
                    }
                    break;
                case ROUTE_END_ICON:
                    if (value is IImage)
                    {
                        EndIcon = (IImage)value;
                    }
                    break;
                case ROUTE_DRAW_PEN:
                    if (value is Pen)
                    {
                        RoutePen = (Pen)value;
                    }
                    break;


            }
        }


        public static void SetParameters(Pen pen, IImage start,
                IImage middle, IImage end)
        {
            if (pen != null)
            {
                RoutePen = pen;
            }
            if (start != null)
            {
                StartIcon = start;
            }
            if (middle != null)
            {
                MiddleIcon = middle;
            }
            if (end != null)
            {
                EndIcon = end;
            }

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Reset map configuration parameters. the resetting parameters should before
         * the initialization of DigitalMap ,and MapTileDownloaderManager.
         * @param cacheOn if cache is on, Digital Map will appy an internal cache
         * which can save some loaded map tile to speed up map rendering, but it'll
         * consume some memory whose Max size is speicfied by cachesize.
         * @param workerThreadNo how many worker thread,default is 4, these threads
         * are in charge of downloading/reading/render map tiles from server,stored
         * map tile file or vector map file.the thread no should between 1 and 8.
         * @param cacheSize the Max size of internal map tile caches.
         * @param drawRoute draw route line or not.
         * @param onlyWayPoint when draw route only draw the way points.
         * @param directionRenderBlocks when render direction, it uses an internal
         *  vector picture engine to draw the polyline, which requires memory ,the
         * memory size is determined by the block size, the default render picture
         * size is 256X256 ,which takes 256X256X4 bytes(256K), for memory constraints
         * device, speicify a small block size requires smaller memory useage. but
         * it effects the render performace,the valid value for directionRenderBlocks
         * is 1,2,4, whose corrosponing block size is 256X256X4 bytes(256K)(default)
         * 128X128X4 bytes(64K) and 64X64X4 bytes(16K).
         */
        public static void SetParameters(bool cacheOn,
                int workerThreadNo,
                long cacheSize,
                bool drawRoute,
                bool onlyWayPoint,
                int directionRenderBlocks)
        {
            IsCacheOn = cacheOn;
            DrawRouteWaypointOnly = onlyWayPoint;
            DrawRouting = drawRoute;
            if (workerThreadNo < 0 || workerThreadNo > 8)
            {
                throw new ArgumentException("Thread no should between 1 and 8");
            }
            WorkerThreadNumber = workerThreadNo;
            if (cacheSize < 0 && cacheOn)
            {
                throw new ArgumentException("Cache size shall be great than 0");
            }
            MapCacheSizeInBytes = cacheSize;
            if (!(directionRenderBlocks == 1 || directionRenderBlocks == 2 ||
                    directionRenderBlocks == 4))
            {
                throw new ArgumentException("block size should be 1, or 2, or 4");
            }
            MapDirectionRenderBlocks = directionRenderBlocks;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * private constructor.
         */
        private MapConfiguration()
        {

        }

        /**
         * is cache on not.
         */
        internal static bool IsCacheOn = true;

        /**
         * when draw route, only draw waypoint.
         */
        internal static bool DrawRouteWaypointOnly;

        /**
         * the no of worker thread.
         */
        internal static int WorkerThreadNumber = 16;

        /**
         * the map cache size.
         */
        internal static long MapCacheSizeInBytes = 1024 * 1024;

        /**
         * the direction render size = 256/MAP_DIRECTION_RENDER_BLOCKS;
         */
        internal static int MapDirectionRenderBlocks = 1;

        /**
         * disable route render,
         */
        internal static bool DrawRouting = true;

        /**
         * default route drawing pen.
         */
        internal static Pen RoutePen = new Pen(new Color(0x7F00FF00, false), 4);

        /**
         * start route icon.
         */
        internal static IImage StartIcon;

        /**
         * start route icon.
         */
        internal static IImage MiddleIcon;


        /**
         * start route icon.
         */
        internal static IImage EndIcon;

    }

}
