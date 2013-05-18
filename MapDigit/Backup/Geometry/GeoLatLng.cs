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
using MapDigit.Util;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.GIS.Geometry
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 18JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * GeoLatLng is a point in geographical coordinates longitude and latitude.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 18/06/09
     * @author      Guidebee, Inc.
     */
    public class GeoLatLng : GeoPoint
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the origin 
         * (0,&nbsp;0) of the geographical coordinate space. 
         */
        public GeoLatLng()
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point with the same location as
         * the specified <code>GeoLatLng</code> object.
         * @param       p a point
         */
        public GeoLatLng(GeoLatLng p)
            : this(p.Lat(), p.Lng())
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the specified 
         * {@code (Lat,Lng)} location in the coordinate space. 
         * @param Lat the latitude coordinate.
         * @param Lng the longitute coordinate.
         */
        public GeoLatLng(double lat, double lng)
            : this(lat, lng, true)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs and initializes a point at the specified 
         * {@code (Lat,Lng)} location in the coordinate space. 
         * @param Lat the latitude coordinate.
         * @param Lng the longitute coordinate.
         * @param _unbounded whether the point of bounded or not.
         */
        public GeoLatLng(double lat, double lng, bool unbounded)
        {
            double lat1 = lat;
            double lng1 = lng;
            if (!unbounded)
            {

                lng1 = lng1 - (((int)(lng1)) / 360) * 360;

                if (lng1 < 0)
                {
                    lng1 += 360;
                }
                if (lat1 < -90)
                {
                    lat1 = -90;
                }
                else if (-90 <= lat1 && lat1 < 90)
                {
                    //lat1 = lat1;
                }
                else if (90 <= lat1)
                {
                    lat1 = 90;
                }
                if (0 <= lng1 && lng1 < 180)
                {
                    //lng1 = lng1;
                }
                else
                {
                    lng1 = lng1 - 360;
                }
            }
            SetLocation(lng1, lat1);

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the latitude coordinate in degrees, as a number between -90 and 
         * +90. If the _unbounded flag was set in the constructor,
         * this coordinate can be outside this interval.
         * @return  the latitude coordinate in degrees.
         */
        public double Lat()
        {
            return Y;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the longitude coordinate in degrees, as a number between -180 and 
         * +180. If the _unbounded flag was set in the constructor,
         * this coordinate can be outside this interval.
         * @return  the longitude coordinate in degrees.
         */
        public double Lng()
        {
            return X;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the latitude coordinate in radians, as a number between -PI/2 
         * and +PI/2. If the _unbounded flag was set in the constructor, 
         * this coordinate can be outside this interval.
         * @return  the latitude coordinate in radians.
         */
        public double LatRadians()
        {
            return MathEx.ToRadians(Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the longitude coordinate in radians, as a number between -PI 
         * and +PI. If the _unbounded flag was set in the constructor, 
         * this coordinate can be outside this interval.
         * @return  the longitude coordinate in radians.
         */
        public double LngRadians()
        {
            return MathEx.ToRadians(X);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Determines whether or not two points are equal. Two instances of
         * <code>GeoLatLng</code> are equal if the values of their 
         * <code>X</code> and <code>Y</code> member fields, representing
         * their position in the coordinate space, are the same.
         * @param obj an object to be compared with this <code>GeoLatLng</code>
         * @return <code>true</code> if the object to be compared is
         *         an instance of <code>GeoLatLng</code> and has
         *         the same values; <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (obj is GeoLatLng)
            {
                GeoLatLng p2D = (GeoLatLng)obj;
                return (GetX() == p2D.GetX()) && (GetY() == p2D.GetY());
            }
            return base.Equals(obj);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Distance, in meters, from this point to the given point. 
         * By default, this Distance is calculated given the default equatorial 
         * earth radius of 6378137 meters. The earth is approximated as a sphere,
         * hence the Distance could be off as much as 0.3%,
         * especially in the polar extremes. 
         * @param pt1 the first point.
         * @param pt2 the other point.
         * @return the Distance, in kilo meters.
         */
        public static double Distance(GeoLatLng pt1, GeoLatLng pt2)
        {
            GreateCircleCalculator cal = new GreateCircleCalculator(
                    GreateCircleCalculator.EARTH_MODEL_WGS84,
                    GreateCircleCalculator.UNIT_KM);
            return cal.CalculateDistance(pt1, pt2);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Distance, in meters, from this point to the given point. 
         * By default, this Distance is calculated given the default equatorial 
         * earth radius of 6378137 meters. The earth is approximated as a sphere,
         * hence the Distance could be off as much as 0.3%,
         * especially in the polar extremes. 
         * @param other the other point.
         * @return the Distance, in meters.
         */
        public double DistanceFrom(GeoLatLng other)
        {
            GreateCircleCalculator cal = new GreateCircleCalculator(
                    GreateCircleCalculator.EARTH_MODEL_WGS84,
                    GreateCircleCalculator.UNIT_KM);
            return cal.CalculateDistance(this, other);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 18JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a string that represents this point in a format suitable 
         * for use as a URL parameter value, separated by a comma, without 
         * whitespace. By default, precision is returned to 6 digits, 
         * which corresponds to a resolution to 4 inches/ 11 centimeters. 
         * An optional precision parameter allows you to specify a lower 
         * precision to reduce server load.
         * @param precision the precision of the output.
         * @return string that represents this point.
         */
        public String ToUrlValue(int precision)
        {
            long multiple = 1;
            double lat = Y;
            double lng = X;
            if (precision < 0)
            {
                precision = 6;
            }
            for (int i = 0; i < precision; i++)
            {
                multiple *= 10;
            }
            lat = (((int)(lat * multiple + 0.5)) / ((double)multiple));
            lng = (((int)(lng * multiple + 0.5)) / ((double)multiple));
            return lat + "," + lng;
        }
    }

}
