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
using MapDigit.Util;
using MathFP = MapDigit.DrawingFP.MathFP;

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
     * The {@code LinearGradientBrush} class provides a way to fill
     * a IShape with a linear color gradient pattern.  The user
     * may specify two or more gradient colors, and this brush will provide an
     * interpolation between each color.  The user also specifies start and end
     * points which define where in user space the color gradient should begin 
     * and end.
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
     * The user may also select what action the {@code LinearGradientBrush}
     * should take when filling color outside the start and end points.
     * If no cycle method is specified, {@code NO_CYCLE} will be chosen by
     * default, which means the endpoint colors will be used to fill the
     * remaining area.
     * <p>
     * The following code demonstrates typical usage of
     * {@code LinearGradientBrush}:
     * <p>
     * <pre>
     *     Point start = new Point(0, 0);
     *     Point end = new Point(50, 50);
     *     int[] dist = {0, 100f, 255};
     *     Color[] colors = {Color.RED, Color.WHITE, Color.BLUE};
     *     LinearGradientBrush p =
     *         new LinearGradientBrush(start, end, dist, colors);
     * </pre>
     * <p>
     * This code will create a {@code LinearGradientBrush} which interpolates
     * between red and white for the first 20% of the gradient and between white
     * and blue for the remaining 80%.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 15/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class LinearGradientBrush : Brush
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code LinearGradientBrush} with a default 
         * {@code NO_CYCLE} repeating method and {@code SRGB} color space.
         *
         * @param startX the X coordinate of the gradient axis start point 
         *               in user space
         * @param startY the Y coordinate of the gradient axis start point 
         *               in user space
         * @param endX   the X coordinate of the gradient axis end point 
         *               in user space
         * @param endY   the Y coordinate of the gradient axis end point 
         *               in user space
         * @param fractions numbers ranging from 0 to 255 specifying the 
         *                  distribution of colors along the gradient
         * @param colors array of colors corresponding to each fractional Value
         *     
         * @throws NullPointerException
         * if {@code fractions} array is null,
         * or {@code colors} array is null,
         * @throws IllegalArgumentException
         * if start and end points are the same points,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public LinearGradientBrush(int startX, int startY,
                int endX, int endY,
                int[] fractions, Color[] colors, int fillType)
            : this(new Point(startX, startY),
                new Point(endX, endY),
                fractions,
                colors, fillType)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code LinearGradientBrush} with a default
         * {@code NO_CYCLE} repeating method and {@code SRGB} color space.
         *
         * @param start the gradient axis start {@code Point} in user space
         * @param end the gradient axis end {@code Point} in user space
         * @param fractions numbers ranging from 0 to 255 specifying the
         *                  distribution of colors along the gradient
         * @param colors array of colors corresponding to each fractional Value
         *
         * @throws NullPointerException
         * if one of the points is null,
         * or {@code fractions} array is null,
         * or {@code colors} array is null
         * @throws IllegalArgumentException
         * if start and end points are the same points,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public LinearGradientBrush(Point start, Point end,
                int[] fractions, Color[] colors)
            : this(start, end,
                fractions, colors,
                NO_CYCLE)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code LinearGradientBrush} with a default {@code SRGB}
         * color space.
         *
         * @param start the gradient axis start {@code Point} in user space
         * @param end the gradient axis end {@code Point} in user space
         * @param fractions numbers ranging from 0 to 255 specifying the 
         *                  distribution of colors along the gradient
         * @param colors array of colors corresponding to each fractional Value
         * @param fillType either {@code NO_CYCLE}, {@code REFLECT},
         *                    or {@code REPEAT}
         *   
         * @throws NullPointerException
         * if one of the points is null,
         * or {@code fractions} array is null,
         * or {@code colors} array is null,
         * or {@code cycleMethod} is null
         * @throws IllegalArgumentException
         * if start and end points are the same points,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 100 or greater than 0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public LinearGradientBrush(Point start, Point end,
                int[] fractions, Color[] colors,
                int fillType)
            : this(start, end,
                fractions, colors,
                new AffineTransform(), fillType)
        {

        }

        public LinearGradientBrush(Rectangle rect, Color color1, Color color2,
                float angle)
            : this(rect, color1, color2, angle, NO_CYCLE)
        {

        }

        public LinearGradientBrush(Rectangle rect, Color color1, Color color2,
                float angle, int fillType)
        {
            _start = new Point(rect.X, rect.Y);
            _end = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            _gradientTransform = new AffineTransform();
            _fractions = new[] { 0, 100 };
            _colors = new[] { color1, color2 };
            bool opaque = true;
            for (int i = 0; i < _colors.Length; i++)
            {
                opaque = opaque && (_colors[i].GetAlpha() == 0xff);
            }
            _transparency = opaque ? Color.OPAQUE : Color.TRANSLUCENT;
            RectangleFP r = Utils.ToRectangleFP(rect);
            _wrappedBrushFP = new LinearGradientBrushFP(r.GetLeft(), r.GetTop(),
                    r.GetRight(), r.GetBottom(),
                    MathFP.ToRadians(SingleFP.FromFloat(angle)));
            for (int i = 0; i < _colors.Length; i++)
            {
                ((LinearGradientBrushFP)_wrappedBrushFP)
                        .SetGradientColor(SingleFP.FromFloat(_fractions[i]
                        / 255.0f),
                        _colors[i]._value);
            }
            ((LinearGradientBrushFP)_wrappedBrushFP).UpdateGradientTable();
            _wrappedBrushFP.SetMatrix(Utils.ToMatrixFP(_gradientTransform));
            _wrappedBrushFP.FillMode = fillType;


        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a {@code LinearGradientBrush}.
         *
         * @param start the gradient axis start {@code Point} in user space
         * @param end the gradient axis end {@code Point} in user space
         * @param fractions numbers ranging from 0 to 255 specifying the 
         *                  distribution of colors along the gradient
         * @param colors array of colors corresponding to each fractional Value
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
         * if start and end points are the same points,
         * or {@code fractions.length != colors.length},
         * or {@code colors} is less than 2 in size,
         * or a {@code fractions} Value is less than 0.0 or greater than 1.0,
         * or the {@code fractions} are not provided in strictly increasing order
         */
        public LinearGradientBrush(Point start, Point end,
                int[] fractions, Color[] colors,
                AffineTransform gradientTransform, int fillType)
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

            this._fractions = new int[len];
            Array.Copy(fractions, 0, this._fractions, off, fractions.Length);
            this._colors = new Color[len];
            Array.Copy(colors, 0, this._colors, off, colors.Length);

            if (fixFirst)
            {
                this._fractions[0] = 0;
                this._colors[0] = colors[0];
            }
            if (fixLast)
            {
                this._fractions[len - 1] = 255;
                this._colors[len - 1] = colors[colors.Length - 1];
            }

            // copy the gradient transform
            this._gradientTransform = new AffineTransform(gradientTransform);

            // determine transparency
            bool opaque = true;
            for (int i = 0; i < colors.Length; i++)
            {
                opaque = opaque && (colors[i].GetAlpha() == 0xff);
            }
            _transparency = opaque ? Color.OPAQUE : Color.TRANSLUCENT;

            // check input parameters
            if (start == null || end == null)
            {
                throw new NullReferenceException("Start and end points must be" +
                        "non-null");
            }

            if (start.Equals(end))
            {
                throw new ArgumentException("Start point cannot equal" +
                        "endpoint");
            }

            // copy the points...
            this._start = new Point(start.GetX(), start.GetY());
            this._end = new Point(end.GetX(), end.GetY());

            Rectangle rectangle = new Rectangle(start, end);
            float dx = start.X - end.X;
            float dy = start.Y - end.Y;
            double angle = MathEx.Atan2(dy, dx);
            int intAngle = SingleFP.FromDouble(angle);
            RectangleFP r = Utils.ToRectangleFP(rectangle);
            _wrappedBrushFP = new LinearGradientBrushFP(r.GetLeft(), r.GetTop(),
                    r.GetRight(), r.GetBottom(),
                    intAngle);
            for (int i = 0; i < colors.Length; i++)
            {
                ((LinearGradientBrushFP)_wrappedBrushFP).SetGradientColor
                        (SingleFP.FromFloat(fractions[i] / 100.0f),
                        colors[i]._value);
            }
            ((LinearGradientBrushFP)_wrappedBrushFP).UpdateGradientTable();
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
         * Returns a copy of the start point of the gradient axis.
         *
         * @return a {@code Point} object that is a copy of the point
         * that anchors the first color of this {@code LinearGradientBrush}
         */
        public Point GetStartPoint()
        {
            return new Point(_start.GetX(), _start.GetY());
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 15JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a copy of the end point of the gradient axis.
         *
         * @return a {@code Point} object that is a copy of the point
         * that anchors the last color of this {@code LinearGradientBrush}
         */
        public Point GetEndPoint()
        {
            return new Point(_end.GetX(), _end.GetY());
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

        /** Gradient start and end points. */
        private readonly Point _start;
        private readonly Point _end;
        private readonly AffineTransform _gradientTransform;
        /** Gradient keyframe values in the range 0 to 1. */
        private readonly int[] _fractions;
        /** Gradient colors. */
        private readonly Color[] _colors;
        /** The transparency of this paint object. */
        readonly int _transparency;
    }

}
