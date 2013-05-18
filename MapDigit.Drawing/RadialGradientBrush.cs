//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 15JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using MapDigit.Drawing.Geometry;
using MapDigit.DrawingFP;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.Drawing
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 15JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * The {@code RadialGradientBrush} class provides a way to fill a shape with
     * a circular radial color gradient pattern. The user may specify 2 or more
     * gradient colors, and this paint will provide an interpolation between
     * each color.
     * <p>
     * The user must specify the circle controlling the gradient pattern,
     * which is described by a center point and a radius.  The user can also
     * specify a separate focus point within that circle, which controls the
     * location of the first color of the gradient.  By default the focus is
     * set to be the center of the circle.
     * <p>
     * This paint will map the first color of the gradient to the focus point,
     * and the last color to the perimeter of the circle, interpolating
     * smoothly for any in-between colors specified by the user.  Any line drawn
     * from the focus point to the circumference will thus span all the gradient
     * colors.
     * <p>
     * Specifying a focus point outside of the circle's radius will result in the
     * focus being set to the intersection point of the focus-center line and the
     * perimeter of the circle.
     * <p>
     * The user must provide an array of integers specifying how to distribute the
     * colors along the gradient.  These values should range from 0 to 255 and
     * act like keyframes along the gradient (they mark where the gradient should
     * be exactly a particular color).
     * <p>
     * In the event that the user does not set the first keyframe Value equal
     * to 0 and/or the last keyframe Value equal to 255, keyframes will be created
     * at these positions and the first and last colors will be replicated there.
     * So, if a user specifies the following arrays to construct a gradient:<br>
     * <pre>
     *     {Color.BLUE, Color.RED}, {100, 140}
     * </pre>
     * this will be converted to a gradient with the following keyframes:<br>
     * <pre>
     *     {Color.BLUE, Color.BLUE, Color.RED, Color.RED}, {0, 100, 140, 255}
     * </pre>
     *
     * <p>
     * The user may also select what action the {@code RadialGradientBrush}
     * should take when filling color outside the bounds of the circle's radius.
     * If no cycle method is specified, {@code NO_CYCLE} will be chosen by
     * default, which means the the last keyframe color will be used to fill the
     * remaining area.
     * <p>
     * The following code demonstrates typical usage of
     * {@code RadialGradientBrush}, where the center and focus points are
     * the same:
     * <p>
     * <pre>
     *     Point center = new Point(50, 50);
     *     int radius = 25;
     *     int[] dist = {0, 52, 255};
     *     Color[] colors = {Color.RED, Color.WHITE, Color.BLUE};
     *     RadialGradientBrush p =
     *         new RadialGradientBrush(center, radius, dist, colors);
     * </pre>
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class RadialGradientBrush : Brush
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code RadialGradientBrush} with a default
         * {@code NO_CYCLE} repeating method and {@code SRGB} color space,
         * using the center as the focus point.
         *
         * @param cx the X coordinate in user space of the center point of the
         *           circle defining the gradient.  The last color of the
         *           gradient is mapped to the perimeter of this circle.
         * @param cy the Y coordinate in user space of the center point of the
         *           circle defining the gradient.  The last color of the
         *           gradient is mapped to the perimeter of this circle.
         * @param radius the radius of the circle defining the extents of the
         *               color gradient
         * @param fractions numbers ranging from 0.0 to 1.0 specifying the
         *                  distribution of colors along the gradient
         * @param colors array of colors to use in the gradient.  The first color
         *               is used at the focus point, the last color around the
         *               perimeter of the circle.
         *
         * @throws NullPointerException
         * if {@code fractions} array is null,
         * or {@code colors} array is null
         * @throws IllegalArgumentException
         * if {@code radius} is non-positive,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public RadialGradientBrush(int cx, int cy, int radius,
                int[] fractions, Color[] colors)
            : this(new Point(cx, cy),
                radius,
                fractions,
                colors,
                NO_CYCLE, new AffineTransform())
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code RadialGradientBrush} with a default
         * {@code NO_CYCLE} repeating method and {@code SRGB} color space,
         * using the center as the focus point.
         *
         * @param center the center point, in user space, of the circle defining
         *               the gradient
         * @param radius the radius of the circle defining the extents of the
         *               color gradient
         * @param fractions numbers ranging from 0.0 to 1.0 specifying the
         *                  distribution of colors along the gradient
         * @param colors array of colors to use in the gradient.  The first color
         *               is used at the focus point, the last color around the
         *               perimeter of the circle.
         *
         * @throws NullPointerException
         * if {@code center} point is null,
         * or {@code fractions} array is null,
         * or {@code colors} array is null
         * @throws IllegalArgumentException
         * if {@code radius} is non-positive,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public RadialGradientBrush(Point center, int radius,
                int[] fractions, Color[] colors)
            : this(center,
                radius,
                fractions,
                colors,
                NO_CYCLE, new AffineTransform())
        {

        }


        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code RadialGradientBrush}.
         *
         * @param center the center point in user space of the circle defining the
         *               gradient.  The last color of the gradient is mapped to
         *               the perimeter of this circle.
         * @param radius the radius of the circle defining the extents of the
         *               color gradient
         * @param fractions numbers ranging from 0.0 to 1.0 specifying the
         *                  distribution of colors along the gradient
         * @param colors array of colors to use in the gradient.  The first color
         *               is used at the focus point, the last color around the
         *               perimeter of the circle.
         * @param fillType either {@code NO_CYCLE}, {@code REFLECT},
         *                    or {@code REPEAT}
         * @param gradientTransform transform to apply to the gradient
         *
         * @throws NullPointerException
         * if one of the points is null,
         * or {@code fractions} array is null,
         * or {@code colors} array is null,
         * or {@code cycleMethod} is null,
         * or {@code colorSpace} is null,
         * or {@code gradientTransform} is null
         * @throws IllegalArgumentException
         * if {@code radius} is non-positive,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public RadialGradientBrush(Point center,
                int radius,
                int[] fractions, Color[] colors,
                int fillType,
                AffineTransform gradientTransform)
        {
            if (fractions == null)
            {
                throw new NullReferenceException("Fractions array cannot be null");
            }

            if (colors == null)
            {
                throw new NullReferenceException("Colors array cannot be null");
            }

            if (gradientTransform == null)
            {
                throw new NullReferenceException("Gradient transform cannot be " +
                        "null");
            }

            if (fractions.Length != colors.Length)
            {
                throw new ArgumentException("Colors and fractions must " +
                        "have equal size");
            }

            if (colors.Length < 2)
            {
                throw new ArgumentException("User must specify at least " +
                        "2 colors");
            }

            // check that values are in the proper range and progress
            // in increasing order from 0 to 1
            int previousFraction = -255;
            for (int i = 0; i < fractions.Length; i++)
            {
                int currentFraction = fractions[i];
                if (currentFraction < 0 || currentFraction > 255)
                {
                    throw new ArgumentException("Fraction values must " +
                            "be in the range 0 to 255: " +
                            currentFraction);
                }

                if (currentFraction <= previousFraction)
                {
                    throw new ArgumentException("Keyframe fractions " +
                            "must be increasing: " +
                            currentFraction);
                }

                previousFraction = currentFraction;
            }

            // We have to deal with the cases where the first gradient stop is not
            // equal to 0 and/or the last gradient stop is not equal to 1.
            // In both cases, create a new point and replicate the previous
            // extreme point's color.
            bool fixFirst = false;
            bool fixLast = false;
            int len = fractions.Length;
            int off = 0;

            if (fractions[0] != 0)
            {
                // first stop is not equal to zero, fix this condition
                fixFirst = true;
                len++;
                off++;
            }
            if (fractions[fractions.Length - 1] != 255)
            {
                // last stop is not equal to one, fix this condition
                fixLast = true;
                len++;
            }

            _fractions = new int[len];
            Array.Copy(fractions, 0, _fractions, off, fractions.Length);
            _colors = new Color[len];
            Array.Copy(colors, 0, _colors, off, colors.Length);

            if (fixFirst)
            {
                _fractions[0] = 0;
                _colors[0] = colors[0];
            }
            if (fixLast)
            {
                _fractions[len - 1] = 255;
                _colors[len - 1] = colors[colors.Length - 1];
            }

            // copy the gradient transform
            _gradientTransform = new AffineTransform(gradientTransform);

            // determine transparency
            bool opaque = true;
            for (int i = 0; i < colors.Length; i++)
            {
                opaque = opaque && (colors[i].GetAlpha() == 0xff);
            }
            _transparency = opaque ? Color.OPAQUE : Color.TRANSLUCENT;


            // check input arguments
            if (center == null)
            {
                throw new NullReferenceException("Center point must be non-null");
            }


            if (radius <= 0)
            {
                throw new ArgumentException("Radius must be greater " +
                        "than zero");
            }

            // copy parameters
            _center = new Point(center.X, center.Y);
            _radius = radius;
            _fillType = fillType;


            _wrappedBrushFP = new RadialGradientBrushFP(SingleFP.FromInt(center.X),
                    SingleFP.FromInt(center.Y), SingleFP.FromInt(radius), 0);
            for (int i = 0; i < colors.Length; i++)
            {
                ((RadialGradientBrushFP)_wrappedBrushFP).SetGradientColor(SingleFP.FromFloat(fractions[i] / 100.0f),
                        colors[i]._value);
            }
            ((RadialGradientBrushFP)_wrappedBrushFP).UpdateGradientTable();
            _wrappedBrushFP.SetMatrix(Utils.ToMatrixFP(gradientTransform));
            _wrappedBrushFP.FillMode = fillType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a copy of the center point of the radial gradient.
         *
         * @return a {@code Point} object that is a copy of the center point
         */
        public Point GetCenterPoint()
        {
            return new Point(_center.X, _center.Y);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the radius of the circle defining the radial gradient.
         *
         * @return the radius of the circle defining the radial gradient
         */
        public int GetRadius()
        {
            return _radius;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        public int GetFillType()
        {
            return _fillType;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a copy of the transform applied to the gradient.
         *
         * @return a copy of the transform applied to the gradient
         */
        public AffineTransform GetTransform()
        {
            return _gradientTransform;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a copy of the array of floats used by this gradient
         * to calculate color distribution.
         * The returned array always has 0 as its first Value and 1 as its
         * last Value, with increasing values in between.
         *
         * @return a copy of the array of floats used by this gradient to
         * calculate color distribution
         */
        public int[] GetFractions()
        {
            return _fractions;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a copy of the array of colors used by this gradient.
         * The first color maps to the first Value in the fractions array,
         * and the last color maps to the last Value in the fractions array.
         *
         * @return a copy of the array of colors used by this gradient
         */
        public Color[] GetColors()
        {
            return _colors;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the transparency mode for this <code>Color</code>.  This is
         * required to implement the <code>Paint</code> interface.
         * @return this <code>Color</code> object's transparency mode.
         */
        public override int GetTransparency()
        {
            return _transparency;
        }

        /** Center of the circle defining the 100% gradient stop X coordinate. */
        private readonly Point _center;
        /** Radius of the outermost circle defining the 100% gradient stop. */
        private readonly int _radius;
        private readonly int _fillType;
        private readonly AffineTransform _gradientTransform;
        /** Gradient keyframe values in the range 0 to 1. */
        private readonly int[] _fractions;
        /** Gradient colors. */
        private readonly Color[] _colors;
        /** The transparency of this paint object. */
        readonly int _transparency;
    }

}
