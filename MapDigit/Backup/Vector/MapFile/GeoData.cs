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
using System.IO;
using MapDigit.Util;
using MapDigit.GIS.Geometry;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Vector.MapFile
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 21JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * geo data section of the map file.
     * <P>
     * <hr>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 21/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoData : Section
    {

        /**
         * when store latitude/longitude , it store as integer.
         * to Convert to an interget ,it muliple by DOUBLE_PRECISION.
         */
        private const double DOUBLE_PRECISION = 10000000.0;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * constructor.
         */
        public GeoData(BinaryReader reader, long offset, long size)
            : base(reader, offset, size)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map object at given index.
         */
        public MapObject GetMapObject(RecordIndex recordIndex)
        {
            DataReader.Seek(_reader, recordIndex.RecordOffset);
            MapObject mapObject = null;
            switch (recordIndex.MapObjectType)
            {
                case MapObject.NONE:
                    mapObject = new MapNoneObject();
                    break;
                case MapObject.POINT:
                    mapObject = GetMapPoint(recordIndex);
                    break;
                case MapObject.MULTIPOINT:
                    mapObject = GetMapMultiPoint(recordIndex);
                    break;
                case MapObject.PLINE:
                    mapObject = GetMapPline(recordIndex);
                    break;
                case MapObject.MULTIPLINE:
                    mapObject = GetMapMultiPline(recordIndex);
                    break;
                case MapObject.REGION:
                    mapObject = GetMapRegion(recordIndex);
                    break;
                case MapObject.MULTIREGION:
                    mapObject = GetMapMultiRegion(recordIndex);
                    break;
                case MapObject.COLLECTION:
                    mapObject = GetMapCollection(recordIndex);
                    break;
                case MapObject.TEXT:
                    mapObject = GetMapText(recordIndex);
                    break;

            }
            return mapObject;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map point object at given index.
         */
        private static MapPoint GetMapPoint(RecordIndex recordIndex)
        {
            MapPoint mapPoint = new MapPoint
                                    {
                                        SymbolType =
                                            {
                                                Shape = recordIndex.Param1,
                                                Color = recordIndex.Param2,
                                                Size = recordIndex.Param3
                                            },
                                        Point =
                                            {
                                                X = recordIndex.MinX/DOUBLE_PRECISION,
                                                Y = recordIndex.MinY/DOUBLE_PRECISION
                                            }
                                    };
            mapPoint.Bounds.X = mapPoint.Point.X;
            mapPoint.Bounds.Y = mapPoint.Point.Y;
            mapPoint.Bounds.Width = 0;
            mapPoint.Bounds.Height = 0;
            return mapPoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map multipoint object at given index.
         */
        private MapMultiPoint GetMapMultiPoint(RecordIndex recordIndex)
        {
            MapMultiPoint mapMultiPoint = new MapMultiPoint
                                              {
                                                  SymbolType =
                                                      {
                                                          Shape = recordIndex.Param1,
                                                          Color = recordIndex.Param2,
                                                          Size = recordIndex.Param3
                                                      },
                                                  Bounds =
                                                      {
                                                          X = recordIndex.MinX/DOUBLE_PRECISION,
                                                          Y = recordIndex.MinY/DOUBLE_PRECISION,
                                                          Width = (recordIndex.MaxX - recordIndex.MinX)/DOUBLE_PRECISION,
                                                          Height =
                                                              (recordIndex.MaxY - recordIndex.MinY)/DOUBLE_PRECISION
                                                      }
                                              };
            int numberOfPoints = DataReader.ReadInt(_reader);
            mapMultiPoint.Points = new GeoLatLng[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                int x = DataReader.ReadInt(_reader);
                int y = DataReader.ReadInt(_reader);
                mapMultiPoint.Points[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                        x / DOUBLE_PRECISION);
            }
            return mapMultiPoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map pline object at given index.
         */
        private MapPline GetMapPline(RecordIndex recordIndex)
        {
            MapPline mapPline = new MapPline
                                    {
                                        PenStyle =
                                            {
                                                Pattern = recordIndex.Param1,
                                                Width = recordIndex.Param2,
                                                Color = recordIndex.Param3
                                            },
                                        Bounds =
                                            {
                                                X = recordIndex.MinX/DOUBLE_PRECISION,
                                                Y = recordIndex.MinY/DOUBLE_PRECISION,
                                                Width = (recordIndex.MaxX - recordIndex.MinX)/DOUBLE_PRECISION,
                                                Height = (recordIndex.MaxY - recordIndex.MinY)/DOUBLE_PRECISION
                                            }
                                    };
            int numberOfPoints = DataReader.ReadInt(_reader);
            GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                int x = DataReader.ReadInt(_reader);
                int y = DataReader.ReadInt(_reader);
                latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                        x / DOUBLE_PRECISION);
            }
            mapPline.Pline = new GeoPolyline(latLngs, mapPline.PenStyle.Color,
                    mapPline.PenStyle.Width, mapPline.PenStyle.Pattern);
            return mapPline;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map multipline object at given index.
         */
        private MapMultiPline GetMapMultiPline(RecordIndex recordIndex)
        {
            MapMultiPline mapMultiPline = new MapMultiPline
                                              {
                                                  PenStyle =
                                                      {
                                                          Pattern = recordIndex.Param1,
                                                          Width = recordIndex.Param2,
                                                          Color = recordIndex.Param3
                                                      },
                                                  Bounds =
                                                      {
                                                          X = recordIndex.MinX/DOUBLE_PRECISION,
                                                          Y = recordIndex.MinY/DOUBLE_PRECISION,
                                                          Width = (recordIndex.MaxX - recordIndex.MinX)/DOUBLE_PRECISION,
                                                          Height =
                                                              (recordIndex.MaxY - recordIndex.MinY)/DOUBLE_PRECISION
                                                      }
                                              };
            int numberOfPart = DataReader.ReadInt(_reader);
            mapMultiPline.Plines = new GeoPolyline[numberOfPart];
            for (int j = 0; j < numberOfPart; j++)
            {

                int numberOfPoints = DataReader.ReadInt(_reader);
                GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
                for (int i = 0; i < numberOfPoints; i++)
                {
                    int x = DataReader.ReadInt(_reader);
                    int y = DataReader.ReadInt(_reader);
                    latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                            x / DOUBLE_PRECISION);
                }
                mapMultiPline.Plines[j] = new GeoPolyline(latLngs,
                        mapMultiPline.PenStyle.Color,
                        mapMultiPline.PenStyle.Width,
                        mapMultiPline.PenStyle.Pattern);
            }
            return mapMultiPline;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map region object at given index.
         */
        private MapRegion GetMapRegion(RecordIndex recordIndex)
        {
            MapRegion mapRegion = new MapRegion
                                      {
                                          BrushStyle =
                                              {
                                                  Pattern = recordIndex.Param1,
                                                  ForeColor = recordIndex.Param2,
                                                  BackColor = recordIndex.Param3
                                              }
                                      };
            mapRegion.PenStyle.Pattern = DataReader.ReadInt(_reader);
            mapRegion.PenStyle.Width = DataReader.ReadInt(_reader);
            mapRegion.PenStyle.Color = DataReader.ReadInt(_reader);
            mapRegion.Bounds.X = recordIndex.MinX / DOUBLE_PRECISION;
            mapRegion.Bounds.Y = recordIndex.MinY / DOUBLE_PRECISION;
            mapRegion.Bounds.Width =
                    (recordIndex.MaxX - recordIndex.MinX) / DOUBLE_PRECISION;
            mapRegion.Bounds.Height =
                    (recordIndex.MaxY - recordIndex.MinY) / DOUBLE_PRECISION;
            int centerX = DataReader.ReadInt(_reader);
            int centerY = DataReader.ReadInt(_reader);
            mapRegion.CenterPt.X = centerX / DOUBLE_PRECISION;
            mapRegion.CenterPt.Y = centerY / DOUBLE_PRECISION;
            int numberOfPoints = DataReader.ReadInt(_reader);
            GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                int x = DataReader.ReadInt(_reader);
                int y = DataReader.ReadInt(_reader);
                latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                        x / DOUBLE_PRECISION);
            }
            mapRegion.Region = new GeoPolygon(latLngs, mapRegion.PenStyle.Color,
                    mapRegion.PenStyle.Width, mapRegion.PenStyle.Pattern,
                    mapRegion.BrushStyle.ForeColor, mapRegion.BrushStyle.Pattern);
            return mapRegion;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map multiregion object at given index.
         */
        private MapMultiRegion GetMapMultiRegion(RecordIndex recordIndex)
        {
            MapMultiRegion mapMultiRegion = new MapMultiRegion();
            mapMultiRegion.BrushStyle.Pattern = recordIndex.Param1;
            mapMultiRegion.BrushStyle.ForeColor = recordIndex.Param2;
            mapMultiRegion.BrushStyle.BackColor = recordIndex.Param3;
            mapMultiRegion.PenStyle.Pattern = DataReader.ReadInt(_reader);
            mapMultiRegion.PenStyle.Width = DataReader.ReadInt(_reader);
            mapMultiRegion.PenStyle.Color = DataReader.ReadInt(_reader);
            mapMultiRegion.Bounds.X = recordIndex.MinX / DOUBLE_PRECISION;
            mapMultiRegion.Bounds.Y = recordIndex.MinY / DOUBLE_PRECISION;
            mapMultiRegion.Bounds.Width =
                    (recordIndex.MaxX - recordIndex.MinX) / DOUBLE_PRECISION;
            mapMultiRegion.Bounds.Height =
                    (recordIndex.MaxY - recordIndex.MinY) / DOUBLE_PRECISION;
            int centerX = DataReader.ReadInt(_reader);
            int centerY = DataReader.ReadInt(_reader);
            mapMultiRegion.CenterPt.X = centerX / DOUBLE_PRECISION;
            mapMultiRegion.CenterPt.Y = centerY / DOUBLE_PRECISION;
            int numberOfPart = DataReader.ReadInt(_reader);
            mapMultiRegion.Regions = new GeoPolygon[numberOfPart];
            for (int j = 0; j < numberOfPart; j++)
            {
                int numberOfPoints = DataReader.ReadInt(_reader);
                GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
                for (int i = 0; i < numberOfPoints; i++)
                {
                    int x = DataReader.ReadInt(_reader);
                    int y = DataReader.ReadInt(_reader);
                    latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                            x / DOUBLE_PRECISION);
                }
                mapMultiRegion.Regions[j] = new GeoPolygon(latLngs,
                        mapMultiRegion.PenStyle.Color,
                        mapMultiRegion.PenStyle.Width,
                        mapMultiRegion.PenStyle.Pattern,
                        mapMultiRegion.BrushStyle.ForeColor,
                        mapMultiRegion.BrushStyle.Pattern);
            }
            return mapMultiRegion;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map collection object at given index.
         */
        private MapCollection GetMapCollection(RecordIndex recordIndex)
        {
            MapCollection mapCollection = new MapCollection();
            int regionPart = recordIndex.Param1;
            int plinePart = recordIndex.Param2;
            int multiPointPart = recordIndex.Param3;
            mapCollection.Bounds.X = recordIndex.MinX / DOUBLE_PRECISION;
            mapCollection.Bounds.Y = recordIndex.MinY / DOUBLE_PRECISION;
            mapCollection.Bounds.Width =
                    (recordIndex.MaxX - recordIndex.MinX) / DOUBLE_PRECISION;
            mapCollection.Bounds.Height =
                    (recordIndex.MaxY - recordIndex.MinY) / DOUBLE_PRECISION;
            if (regionPart > 0)
            {
                mapCollection.MultiRegion = new MapMultiRegion();
                mapCollection.MultiRegion.BrushStyle.Pattern = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.BrushStyle.ForeColor = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.BrushStyle.BackColor = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.PenStyle.Pattern = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.PenStyle.Width = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.PenStyle.Color = DataReader.ReadInt(_reader);
                int centerX = DataReader.ReadInt(_reader);
                int centerY = DataReader.ReadInt(_reader);
                mapCollection.MultiRegion.CenterPt.X = centerX / DOUBLE_PRECISION;
                mapCollection.MultiRegion.CenterPt.Y = centerY / DOUBLE_PRECISION;
                mapCollection.MultiRegion.Regions = new GeoPolygon[regionPart];
                for (int j = 0; j < regionPart; j++)
                {
                    int numberOfPoints = DataReader.ReadInt(_reader);
                    GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        int x = DataReader.ReadInt(_reader);
                        int y = DataReader.ReadInt(_reader);
                        latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                                x / DOUBLE_PRECISION);
                    }
                    mapCollection.MultiRegion.Regions[j] = new GeoPolygon(latLngs,
                            0,
                            0,
                            0,
                            0,
                            0);
                }
            }
            if (plinePart > 0)
            {
                mapCollection.MultiPline = new MapMultiPline();
                mapCollection.MultiPline.PenStyle.Pattern = DataReader.ReadInt(_reader);
                mapCollection.MultiPline.PenStyle.Width = DataReader.ReadInt(_reader);
                mapCollection.MultiPline.PenStyle.Color = DataReader.ReadInt(_reader);
                mapCollection.MultiPline.Plines = new GeoPolyline[plinePart];
                for (int j = 0; j < plinePart; j++)
                {

                    int numberOfPoints = DataReader.ReadInt(_reader);
                    GeoLatLng[] latLngs = new GeoLatLng[numberOfPoints];
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        int x = DataReader.ReadInt(_reader);
                        int y = DataReader.ReadInt(_reader);
                        latLngs[i] = new GeoLatLng(y / DOUBLE_PRECISION,
                                x / DOUBLE_PRECISION);
                    }
                    mapCollection.MultiPline.Plines[j] = new GeoPolyline(latLngs,
                            0,
                            0,
                            0);
                }
            }
            if (multiPointPart > 0)
            {
                mapCollection.MultiPoint = new MapMultiPoint();
                mapCollection.MultiPoint.SymbolType.Shape = DataReader.ReadInt(_reader);
                mapCollection.MultiPoint.SymbolType.Color = DataReader.ReadInt(_reader);
                mapCollection.MultiPoint.SymbolType.Size = DataReader.ReadInt(_reader);
                multiPointPart = DataReader.ReadInt(_reader);
                mapCollection.MultiPoint.Points = new GeoLatLng[multiPointPart];
                for (int i = 0; i < multiPointPart; i++)
                {
                    int x = DataReader.ReadInt(_reader);
                    int y = DataReader.ReadInt(_reader);
                    mapCollection.MultiPoint.Points[i] =
                            new GeoLatLng(y / DOUBLE_PRECISION,
                            x / DOUBLE_PRECISION);
                }
            }
            return mapCollection;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 21JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * get a map text object at given index.
         */
        private MapText GetMapText(RecordIndex recordIndex)
        {
            MapText mapText = new MapText();
            mapText.Angle = recordIndex.Param1;
            mapText.ForeColor = recordIndex.Param2;
            mapText.BackColor = recordIndex.Param3;
            mapText.Bounds.X = recordIndex.MinX / DOUBLE_PRECISION;
            mapText.Bounds.Y = recordIndex.MinY / DOUBLE_PRECISION;
            mapText.Bounds.Width =
                    (recordIndex.MaxX - recordIndex.MinX) / DOUBLE_PRECISION;
            mapText.Bounds.Height =
                    (recordIndex.MaxY - recordIndex.MinY) / DOUBLE_PRECISION;
            mapText.Justification = DataReader.ReadInt(_reader);
            mapText.Spacing = DataReader.ReadInt(_reader);
            mapText.LineType = DataReader.ReadInt(_reader);
            mapText.TextString = DataReader.ReadString(_reader);
            return mapText;
        }
    }

}
