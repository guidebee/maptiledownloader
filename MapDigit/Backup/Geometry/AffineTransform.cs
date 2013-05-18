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
     * The <code>AffineTransform</code> class represents a 2D affine transform
     * that performs a linear mapping from 2D coordinates to other 2D
     * coordinates that preserves the "straightness" and
     * "parallelness" of lines.  Affine transformations can be constructed
     * using sequences of translations, scales, flips, rotations, and shears.
     * <p>
     * Such a coordinate transformation can be represented by a 3 row by
     * 3 column matrix with an implied last row of [ 0 0 1 ].  This matrix
     * transforms source coordinates {@code (x,y)} into
     * destination coordinates {@code (x',y')} by considering
     * them to be a column vector and multiplying the coordinate vector
     * by the matrix according to the following process:
     * <pre>
     *	[ x']   [  m00  m01  m02  ] [ x ]   [ m00x + m01y + m02 ]
     *	[ y'] = [  m10  m11  m12  ] [ y ] = [ m10x + m11y + m12 ]
     *	[ 1 ]   [   0    0    1   ] [ 1 ]   [         1         ]
     * </pre>
     * <p>
     * <a name="quadrantapproximation"><h4>Handling 90-Degree Rotations</h4></a>
     * <p>
     * In some variations of the <code>rotate</code> methods in the
     * <code>AffineTransform</code> class, a double-precision argument
     * specifies the angle of rotation in radians.
     * These methods have special handling for rotations of approximately
     * 90 degrees (including multiples such as 180, 270, and 360 degrees),
     * so that the common case of quadrant rotation is handled more
     * efficiently.
     * This special handling can cause angles very close to multiples of
     * 90 degrees to be treated as if they were exact multiples of
     * 90 degrees.
     * For small multiples of 90 degrees the range of angles treated
     * as a quadrant rotation is approximately 0.00000121 degrees wide.
     * This section explains why such special care is needed and how
     * it is implemented.
     * <p>
     * Since 90 degrees is represented as <code>PI/2</code> in radians,
     * and since PI is a transcendental (and therefore irrational) number,
     * it is not possible to exactly represent a multiple of 90 degrees as
     * an exact double precision Value measured in radians.
     * As a result it is theoretically impossible to describe quadrant
     * rotations (90, 180, 270 or 360 degrees) using these values.
     * Double precision floating point values can get very close to
     * non-zero multiples of <code>PI/2</code> but never close enough
     * for the sine or cosine to be exactly 0.0, 1.0 or -1.0.
     * The implementations of <code>Math.Sin()</code> and
     * <code>Math.Cos()</code> correspondingly never return 0.0
     * for any case other than <code>Math.Sin(0.0)</code>.
     * These same implementations do, however, return exactly 1.0 and
     * -1.0 for some range of numbers around each multiple of 90
     * degrees since the correct answer is so close to 1.0 or -1.0 that
     * the double precision significand cannot represent the difference
     * as accurately as it can for numbers that are near 0.0.
     * <p>
     * The net result of these issues is that if the
     * <code>Math.Sin()</code> and <code>Math.Cos()</code> methods
     * are used to directly generate the values for the matrix modifications
     * during these radian-based rotation operations then the resulting
     * transform is never strictly classifiable as a quadrant rotation
     * even for a simple case like <code>rotate(Math.PI/2.0)</code>,
     * due to minor variations in the matrix caused by the non-0.0 values
     * obtained for the sine and cosine.
     * If these transforms are not classified as quadrant rotations then
     * subsequent code which attempts to optimize further operations based
     * upon the type of the transform will be relegated to its most general
     * implementation.
     * <p>
     * Because quadrant rotations are fairly common,
     * this class should handle these cases reasonably quickly, both in
     * applying the rotations to the transform and in applying the resulting
     * transform to the coordinates.
     * To facilitate this optimal handling, the methods which take an angle
     * of rotation measured in radians attempt to detect angles that are
     * intended to be quadrant rotations and treat them as such.
     * These methods therefore treat an angle <em>theta</em> as a quadrant
     * rotation if either <code>Math.Sin(<em>theta</em>)</code> or
     * <code>Math.Cos(<em>theta</em>)</code> returns exactly 1.0 or -1.0.
     * As a rule of thumb, this property holds true for a range of
     * approximately 0.0000000211 radians (or 0.00000121 degrees) around
     * small multiples of <code>Math.PI/2.0</code>.
     *
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public class AffineTransform
    {

        /**
         * This constant indicates that the transform defined by this object
         * is an identity transform.
         * An identity transform is one in which the output coordinates are
         * always the same as the input coordinates.
         * If this transform is anything other than the identity transform,
         * the type will either be the constant GENERAL_TRANSFORM or a
         * combination of the appropriate flag bits for the various coordinate
         * conversions that this transform performs.
         */
        public const int TYPE_IDENTITY = 0;

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a translation in addition to the conversions indicated
         * by other flag bits.
         * A translation moves the coordinates by a constant amount in x
         * and y without changing the length or angle of vectors.
         */
        public const int TYPE_TRANSLATION = 1;

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a uniform scale in addition to the conversions indicated
         * by other flag bits.
         * A uniform scale multiplies the length of vectors by the same amount
         * in both the x and y directions without changing the angle between
         * vectors.
         * This flag bit is mutually exclusive with the TYPE_GENERAL_SCALE flag.
         */
        public const int TYPE_UNIFORM_SCALE = 2;

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a general scale in addition to the conversions indicated
         * by other flag bits.
         * A general scale multiplies the length of vectors by different
         * amounts in the x and y directions without changing the angle
         * between perpendicular vectors.
         * This flag bit is mutually exclusive with the TYPE_UNIFORM_SCALE flag.
         */
        public const int TYPE_GENERAL_SCALE = 4;

        /**
         * This constant is a bit mask for any of the scale flag bits.
         */
        public const int TYPE_MASK_SCALE = (TYPE_UNIFORM_SCALE |
                               TYPE_GENERAL_SCALE);

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a mirror image flip about some axis which changes the
         * normally right handed coordinate system into a left handed
         * system in addition to the conversions indicated by other flag bits.
         * A right handed coordinate system is one where the positive X
         * axis rotates counterclockwise to overlay the positive Y axis
         * similar to the direction that the fingers on your right hand
         * curl when you stare end on at your thumb.
         * A left handed coordinate system is one where the positive X
         * axis rotates clockwise to overlay the positive Y axis similar
         * to the direction that the fingers on your left hand curl.
         * There is no mathematical way to determine the angle of the
         * original flipping or mirroring transformation since all angles
         * of flip are identical given an appropriate adjusting rotation.
         */
        public const int TYPE_FLIP = 64;
        /* TYPE_FLIP was added after GENERAL_TRANSFORM was in public
         * circulation and the flag bits could no longer be conveniently
         * renumbered without introducing binary incompatibility in outside
         * code.
         */

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a quadrant rotation by some multiple of 90 degrees in
         * addition to the conversions indicated by other flag bits.
         * A rotation changes the angles of vectors by the same amount
         * regardless of the original direction of the vector and without
         * changing the length of the vector.
         * This flag bit is mutually exclusive with the TYPE_GENERAL_ROTATION flag.
         */
        public const int TYPE_QUADRANT_ROTATION = 8;

        /**
         * This flag bit indicates that the transform defined by this object
         * performs a rotation by an arbitrary angle in addition to the
         * conversions indicated by other flag bits.
         * A rotation changes the angles of vectors by the same amount
         * regardless of the original direction of the vector and without
         * changing the length of the vector.
         * This flag bit is mutually exclusive with the
         * TYPE_QUADRANT_ROTATION flag.
         */
        public const int TYPE_GENERAL_ROTATION = 16;

        /**
         * This constant is a bit mask for any of the rotation flag bits.
         */
        public const int TYPE_MASK_ROTATION = (TYPE_QUADRANT_ROTATION |
                              TYPE_GENERAL_ROTATION);

        /**
         * This constant indicates that the transform defined by this object
         * performs an arbitrary conversion of the input coordinates.
         * If this transform can be classified by any of the above constants,
         * the type will either be the constant TYPE_IDENTITY or a
         * combination of the appropriate flag bits for the various coordinate
         * conversions that this transform performs.
         */
        public const int TYPE_GENERAL_TRANSFORM = 32;

        /**
         * This constant is used for the internal state variable to indicate
         * that no calculations need to be performed and that the source
         * coordinates only need to be copied to their destinations to
         * complete the transformation equation of this transform.
         */
        private const int APPLY_IDENTITY = 0;


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>AffineTransform</code> representing the
         * Identity transformation.
         */
        public AffineTransform()
        {
            _m00 = _m11 = 1.0;
            _m01 = _m10 = _m02 = _m12 = 0.0;
            _state = APPLY_IDENTITY;
            _type = TYPE_IDENTITY;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>AffineTransform</code> that is a copy of
         * the specified <code>AffineTransform</code> object.
         * @param Tx the <code>AffineTransform</code> object to copy
         */
        public AffineTransform(AffineTransform tx)
        {
            _m00 = tx._m00;
            _m10 = tx._m10;
            _m01 = tx._m01;
            _m11 = tx._m11;
            _m02 = tx._m02;
            _m12 = tx._m12;
            _state = tx._state;
            _type = tx._type;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>AffineTransform</code> from 6 double
         * precision values representing the 6 specifiable entries of the 3x3
         * transformation matrix.
         *
         * @param m00 the X coordinate scaling element of the 3x3 matrix
         * @param m10 the Y coordinate shearing element of the 3x3 matrix
         * @param m01 the X coordinate shearing element of the 3x3 matrix
         * @param m11 the Y coordinate scaling element of the 3x3 matrix
         * @param m02 the X coordinate translation element of the 3x3 matrix
         * @param m12 the Y coordinate translation element of the 3x3 matrix
         */
        public AffineTransform(double m00, double m10,
                   double m01, double m11,
                   double m02, double m12)
        {
            _m00 = m00;
            _m10 = m10;
            _m01 = m01;
            _m11 = m11;
            _m02 = m02;
            _m12 = m12;
            UpdateState();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Constructs a new <code>AffineTransform</code> from an array of
         * double precision values representing either the 4 non-translation
         * entries or the 6 specifiable entries of the 3x3 transformation
         * matrix. The values are retrieved from the array as
         * {&nbsp;m00&nbsp;m10&nbsp;m01&nbsp;m11&nbsp;[m02&nbsp;m12]}.
         * @param flatmatrix the double array containing the values to be set
         * in the new <code>AffineTransform</code> object. The length of the
         * array is assumed to be at least 4. If the length of the array is
         * less than 6, only the first 4 values are taken. If the length of
         * the array is greater than 6, the first 6 values are taken.
         */
        public AffineTransform(double[] flatmatrix)
        {
            _m00 = flatmatrix[0];
            _m10 = flatmatrix[1];
            _m01 = flatmatrix[2];
            _m11 = flatmatrix[3];
            if (flatmatrix.Length > 5)
            {
                _m02 = flatmatrix[4];
                _m12 = flatmatrix[5];
            }
            UpdateState();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform representing a translation transformation.
         * The matrix representing the returned transform is:
         * <pre>
         *		[   1    0    tx  ]
         *		[   0    1    ty  ]
         *		[   0    0    1   ]
         * </pre>
         * @param tx the distance by which coordinates are translated in the
         * X axis direction
         * @param ty the distance by which coordinates are translated in the
         * Y axis direction
         * @return an <code>AffineTransform</code> object that represents a
         * 	translation transformation, created with the specified vector.
         */
        public static AffineTransform GetTranslateInstance(double tx, double ty)
        {
            var trans = new AffineTransform();
            trans.SetToTranslation(tx, ty);
            return trans;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform representing a rotation transformation.
         * The matrix representing the returned transform is:
         * <pre>
         *		[   Cos(theta)    -Sin(theta)    0   ]
         *		[   Sin(theta)     Cos(theta)    0   ]
         *		[       0              0         1   ]
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         * @param theta the angle of rotation measured in radians
         * @return an <code>AffineTransform</code> object that is a rotation
         *	transformation, created with the specified angle of rotation.
         */
        public static AffineTransform GetRotateInstance(double theta)
        {
            var tx = new AffineTransform();
            tx.SetToRotation(theta);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform that rotates coordinates around an anchor point.
         * This operation is equivalent to translating the coordinates so
         * that the anchor point is at the origin (S1), then rotating them
         * about the new origin (S2), and finally translating so that the
         * intermediate origin is restored to the coordinates of the original
         * anchor point (S3).
         * <p>
         * This operation is equivalent to the following sequence of calls:
         * <pre>
         *     AffineTransform Tx = new AffineTransform();
         *     Tx.translate(anchorx, anchory);    // S3: final translation
         *     Tx.rotate(theta);		      // S2: rotate around anchor
         *     Tx.translate(-anchorx, -anchory);  // S1: translate anchor to origin
         * </pre>
         * The matrix representing the returned transform is:
         * <pre>
         *		[   Cos(theta)    -Sin(theta)    x-x*Cos+y*Sin  ]
         *		[   Sin(theta)     Cos(theta)    y-x*Sin-y*Cos  ]
         *		[       0              0               1        ]
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         *
         * @param theta the angle of rotation measured in radians
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         * @return an <code>AffineTransform</code> object that rotates
         *	coordinates around the specified point by the specified angle of
         *	rotation.
         */
        public static AffineTransform GetRotateInstance(double theta,
                                double anchorx,
                                double anchory)
        {
            var tx = new AffineTransform();
            tx.SetToRotation(theta, anchorx, anchory);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform that rotates coordinates according to
         * a rotation vector.
         * All coordinates rotate about the origin by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * an identity transform is returned.
         * This operation is equivalent to calling:
         * <pre>
         *     AffineTransform.getRotateInstance(Math.Atan2(vecy, vecx));
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         * @return an <code>AffineTransform</code> object that rotates
         *  coordinates according to the specified rotation vector.
         */
        public static AffineTransform GetRotateInstance(double vecx, double vecy)
        {
            var tx = new AffineTransform();
            tx.SetToRotation(vecx, vecy);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform that rotates coordinates around an anchor
         * point accordinate to a rotation vector.
         * All coordinates rotate about the specified anchor coordinates
         * by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * an identity transform is returned.
         * This operation is equivalent to calling:
         * <pre>
         *     AffineTransform.getRotateInstance(Math.Atan2(vecy, vecx),
         *                                       anchorx, anchory);
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         * @return an <code>AffineTransform</code> object that rotates
         *	coordinates around the specified point according to the
         *  specified rotation vector.
         */
        public static AffineTransform GetRotateInstance(double vecx,
                                double vecy,
                                double anchorx,
                                double anchory)
        {
            var tx = new AffineTransform();
            tx.SetToRotation(vecx, vecy, anchorx, anchory);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform that rotates coordinates by the specified
         * number of quadrants.
         * This operation is equivalent to calling:
         * <pre>
         *     AffineTransform.getRotateInstance(numquadrants * Math.PI / 2.0);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         * @param numquadrants the number of 90 degree arcs to rotate by
         * @return an <code>AffineTransform</code> object that rotates
         *  coordinates by the specified number of quadrants.
         */
        public static AffineTransform GetQuadrantRotateInstance(int numquadrants)
        {
            var tx = new AffineTransform();
            tx.SetToQuadrantRotation(numquadrants);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform that rotates coordinates by the specified
         * number of quadrants around the specified anchor point.
         * This operation is equivalent to calling:
         * <pre>
         *     AffineTransform.getRotateInstance(numquadrants * Math.PI / 2.0,
         *                                       anchorx, anchory);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         *
         * @param numquadrants the number of 90 degree arcs to rotate by
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         * @return an <code>AffineTransform</code> object that rotates
         *	coordinates by the specified number of quadrants around the
         *  specified anchor point.
         */
        public static AffineTransform GetQuadrantRotateInstance(int numquadrants,
                                    double anchorx,
                                    double anchory)
        {
            var tx = new AffineTransform();
            tx.SetToQuadrantRotation(numquadrants, anchorx, anchory);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform representing a scaling transformation.
         * The matrix representing the returned transform is:
         * <pre>
         *		[   sx   0    0   ]
         *		[   0    sy   0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param sx the factor by which coordinates are scaled along the
         * X axis direction
         * @param sy the factor by which coordinates are scaled along the
         * Y axis direction
         * @return an <code>AffineTransform</code> object that scales
         *	coordinates by the specified factors.
         */
        public static AffineTransform GetScaleInstance(double sx, double sy)
        {
            var tx = new AffineTransform();
            tx.SetToScale(sx, sy);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a transform representing a shearing transformation.
         * The matrix representing the returned transform is:
         * <pre>
         *		[   1   shx   0   ]
         *		[  shy   1    0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param shx the multiplier by which coordinates are shifted in the
         * direction of the positive X axis as a factor of their Y coordinate
         * @param shy the multiplier by which coordinates are shifted in the
         * direction of the positive Y axis as a factor of their X coordinate
         * @return an <code>AffineTransform</code> object that shears
         *	coordinates by the specified multipliers.
         */
        public static AffineTransform GetShearInstance(double shx, double shy)
        {
            var tx = new AffineTransform();
            tx.SetToShear(shx, shy);
            return tx;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Retrieves the flag bits describing the conversion properties of
         * this transform.
         * The return Value is either one of the constants TYPE_IDENTITY
         * or TYPE_GENERAL_TRANSFORM, or a combination of the
         * appriopriate flag bits.
         * A valid combination of flag bits is an exclusive OR operation
         * that can combine
         * the TYPE_TRANSLATION flag bit
         * in addition to either of the
         * TYPE_UNIFORM_SCALE or TYPE_GENERAL_SCALE flag bits
         * as well as either of the
         * TYPE_QUADRANT_ROTATION or TYPE_GENERAL_ROTATION flag bits.
         * @return the OR combination of any of the indicated flags that
         * apply to this transform
         */
        public int GetTransformType()
        {
            if (_type == TYPE_UNKNOWN)
            {
                CalculateType();
            }
            return _type;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the determinant of the matrix representation of the transform.
         * The determinant is useful both to determine if the transform can
         * be inverted and to get a single Value representing the
         * combined X and Y scaling of the transform.
         * <p>
         * If the determinant is non-zero, then this transform is
         * invertible and the various methods that depend on the inverse
         * transform do not need to throw a
         *  NoninvertibleTransformException.
         * If the determinant is zero then this transform can not be
         * inverted since the transform maps all input coordinates onto
         * a line or a point.
         * If the determinant is near enough to zero then inverse transform
         * operations might not carry enough precision to produce meaningful
         * results.
         * <p>
         * If this transform represents a uniform scale, as indicated by
         * the <code>getType</code> method then the determinant also
         * represents the square of the uniform scale factor by which all of
         * the points are expanded from or contracted towards the origin.
         * If this transform represents a non-uniform scale or more general
         * transform then the determinant is not likely to represent a
         * Value useful for any purpose other than determining if inverse
         * transforms are possible.
         * <p>
         * Mathematically, the determinant is calculated using the formula:
         * <pre>
         *		|  m00  m01  m02  |
         *		|  m10  m11  m12  |  =  m00 * m11 - m01 * m10
         *		|   0    0    1   |
         * </pre>
         *
         * @return the determinant of the matrix used to transform the
         * coordinates.
         */
        public double GetDeterminant()
        {
            switch (_state)
            {
                default:
                    StateError();
                    return _m00 * _m11 - _m01 * _m10;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    return _m00 * _m11 - _m01 * _m10;
                case (APPLY_SHEAR | APPLY_SCALE):
                    return _m00 * _m11 - _m01 * _m10;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    return -(_m01 * _m10);
                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    return _m00 * _m11;
                case (APPLY_TRANSLATE):
                case (APPLY_IDENTITY):
                    return 1.0;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Retrieves the 6 specifiable values in the 3x3 affine transformation
         * matrix and places them into an array of double precisions values.
         * The values are stored in the array as
         * {&nbsp;m00&nbsp;m10&nbsp;m01&nbsp;m11&nbsp;m02&nbsp;m12&nbsp;}.
         * An array of 4 doubles can also be specified, in which case only the
         * first four elements representing the non-transform
         * parts of the array are retrieved and the values are stored into
         * the array as {&nbsp;m00&nbsp;m10&nbsp;m01&nbsp;m11&nbsp;}
         * @param flatmatrix the double array used to store the returned
         * values.
         */
        public void GetMatrix(double[] flatmatrix)
        {
            flatmatrix[0] = _m00;
            flatmatrix[1] = _m10;
            flatmatrix[2] = _m01;
            flatmatrix[3] = _m11;
            if (flatmatrix.Length > 5)
            {
                flatmatrix[4] = _m02;
                flatmatrix[5] = _m12;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate scaling element (m00) of the 3x3
         * affine transformation matrix.
         * @return a double Value that is the X coordinate of the scaling
         *  element of the affine transformation matrix.
         */
        public double GetScaleX()
        {
            return _m00;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate scaling element (m11) of the 3x3
         * affine transformation matrix.
         * @return a double Value that is the Y coordinate of the scaling
         *  element of the affine transformation matrix.
         */
        public double GetScaleY()
        {
            return _m11;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate shearing element (m01) of the 3x3
         * affine transformation matrix.
         * @return a double Value that is the X coordinate of the shearing
         *  element of the affine transformation matrix.
         */
        public double GetShearX()
        {
            return _m01;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate shearing element (m10) of the 3x3
         * affine transformation matrix.
         * @return a double Value that is the Y coordinate of the shearing
         *  element of the affine transformation matrix.
         */
        public double GetShearY()
        {
            return _m10;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the X coordinate of the translation element (m02) of the
         * 3x3 affine transformation matrix.
         * @return a double Value that is the X coordinate of the translation
         *  element of the affine transformation matrix.
         */
        public double GetTranslateX()
        {
            return _m02;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns the Y coordinate of the translation element (m12) of the
         * 3x3 affine transformation matrix.
         * @return a double Value that is the Y coordinate of the translation
         *  element of the affine transformation matrix.
         */
        public double GetTranslateY()
        {
            return _m12;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a translation transformation.
         * This is equivalent to calling concatenate(T), where T is an
         * <code>AffineTransform</code> represented by the following matrix:
         * <pre>
         *		[   1    0    tx  ]
         *		[   0    1    ty  ]
         *		[   0    0    1   ]
         * </pre>
         * @param tx the distance by which coordinates are translated in the
         * X axis direction
         * @param ty the distance by which coordinates are translated in the
         * Y axis direction
         */
        public void Translate(double tx, double ty)
        {
            switch (_state)
            {
                default:
                    StateError();
                    return;
                /* NOTREACHED */
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    _m02 = tx * _m00 + ty * _m01 + _m02;
                    _m12 = tx * _m10 + ty * _m11 + _m12;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SHEAR | APPLY_SCALE;
                        if (_type != TYPE_UNKNOWN)
                        {
                            _type -= TYPE_TRANSLATION;
                        }
                    }
                    return;
                case (APPLY_SHEAR | APPLY_SCALE):
                    _m02 = tx * _m00 + ty * _m01;
                    _m12 = tx * _m10 + ty * _m11;
                    if (_m02 != 0.0 || _m12 != 0.0)
                    {
                        _state = APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE;
                        _type |= TYPE_TRANSLATION;
                    }
                    return;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    _m02 = ty * _m01 + _m02;
                    _m12 = tx * _m10 + _m12;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SHEAR;
                        if (_type != TYPE_UNKNOWN)
                        {
                            _type -= TYPE_TRANSLATION;
                        }
                    }
                    return;
                case (APPLY_SHEAR):
                    _m02 = ty * _m01;
                    _m12 = tx * _m10;
                    if (_m02 != 0.0 || _m12 != 0.0)
                    {
                        _state = APPLY_SHEAR | APPLY_TRANSLATE;
                        _type |= TYPE_TRANSLATION;
                    }
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    _m02 = tx * _m00 + _m02;
                    _m12 = ty * _m11 + _m12;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SCALE;
                        if (_type != TYPE_UNKNOWN)
                        {
                            _type -= TYPE_TRANSLATION;
                        }
                    }
                    return;
                case (APPLY_SCALE):
                    _m02 = tx * _m00;
                    _m12 = ty * _m11;
                    if (_m02 != 0.0 || _m12 != 0.0)
                    {
                        _state = APPLY_SCALE | APPLY_TRANSLATE;
                        _type |= TYPE_TRANSLATION;
                    }
                    return;
                case (APPLY_TRANSLATE):
                    _m02 = tx + _m02;
                    _m12 = ty + _m12;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_IDENTITY;
                        _type = TYPE_IDENTITY;
                    }
                    return;
                case (APPLY_IDENTITY):
                    _m02 = tx;
                    _m12 = ty;
                    if (tx != 0.0 || ty != 0.0)
                    {
                        _state = APPLY_TRANSLATE;
                        _type = TYPE_TRANSLATION;
                    }
                    return;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a rotation transformation.
         * This is equivalent to calling concatenate(R), where R is an
         * <code>AffineTransform</code> represented by the following matrix:
         * <pre>
         *		[   Cos(theta)    -Sin(theta)    0   ]
         *		[   Sin(theta)     Cos(theta)    0   ]
         *		[       0              0         1   ]
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         * @param theta the angle of rotation measured in radians
         */
        public void Rotate(double theta)
        {
            var sin = Math.Sin(theta);
            if (sin == 1.0)
            {
                Rotate90();
            }
            else if (sin == -1.0)
            {
                Rotate270();
            }
            else
            {
                var cos = Math.Cos(theta);
                var m1 = _m01;
                if (cos == -1.0)
                {
                    Rotate180();
                }
                else if (cos != 1.0)
                {
                    var m0 = _m00;
                    _m00 = cos * m0 + sin * m1;
                    _m01 = -sin * m0 + cos * m1;
                    m0 = _m10;
                    m1 = _m11;
                    _m10 = cos * m0 + sin * m1;
                    _m11 = -sin * m0 + cos * m1;
                    UpdateState();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a transform that rotates
         * coordinates around an anchor point.
         * This operation is equivalent to translating the coordinates so
         * that the anchor point is at the origin (S1), then rotating them
         * about the new origin (S2), and finally translating so that the
         * intermediate origin is restored to the coordinates of the original
         * anchor point (S3).
         * <p>
         * This operation is equivalent to the following sequence of calls:
         * <pre>
         *     translate(anchorx, anchory);      // S3: final translation
         *     rotate(theta);                    // S2: rotate around anchor
         *     translate(-anchorx, -anchory);    // S1: translate anchor to origin
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         *
         * @param theta the angle of rotation measured in radians
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void Rotate(double theta, double anchorx, double anchory)
        {
            // REMIND: Simple for now - optimize later
            Translate(anchorx, anchory);
            Rotate(theta);
            Translate(-anchorx, -anchory);
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a transform that rotates
         * coordinates according to a rotation vector.
         * All coordinates rotate about the origin by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * no additional rotation is added to this transform.
         * This operation is equivalent to calling:
         * <pre>
         *          rotate(Math.Atan2(vecy, vecx));
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         */
        public void Rotate(double vecx, double vecy)
        {
            if (vecy == 0.0)
            {
                if (vecx < 0.0)
                {
                    Rotate180();
                }
                // If vecx > 0.0 - no rotation
                // If vecx == 0.0 - undefined rotation - treat as no rotation
            }
            else if (vecx == 0.0)
            {
                if (vecy > 0.0)
                {
                    Rotate90();
                }
                else
                {  // vecy must be < 0.0
                    Rotate270();
                }
            }
            else
            {
                var len = Math.Sqrt(vecx * vecx + vecy * vecy);
                var sin = vecy / len;
                var cos = vecx / len;
                var m0 = _m00;
                var m1 = _m01;
                _m00 = cos * m0 + sin * m1;
                _m01 = -sin * m0 + cos * m1;
                m0 = _m10;
                m1 = _m11;
                _m10 = cos * m0 + sin * m1;
                _m11 = -sin * m0 + cos * m1;
                UpdateState();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a transform that rotates
         * coordinates around an anchor point according to a rotation
         * vector.
         * All coordinates rotate about the specified anchor coordinates
         * by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * the transform is not modified in any way.
         * This method is equivalent to calling:
         * <pre>
         *     rotate(Math.Atan2(vecy, vecx), anchorx, anchory);
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void Rotate(double vecx, double vecy,
                   double anchorx, double anchory)
        {
            // REMIND: Simple for now - optimize later
            Translate(anchorx, anchory);
            Rotate(vecx, vecy);
            Translate(-anchorx, -anchory);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a transform that rotates
         * coordinates by the specified number of quadrants.
         * This is equivalent to calling:
         * <pre>
         *     rotate(numquadrants * Math.PI / 2.0);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         * @param numquadrants the number of 90 degree arcs to rotate by
         */
        public void QuadrantRotate(int numquadrants)
        {
            switch (numquadrants & 3)
            {
                case 0:
                    break;
                case 1:
                    Rotate90();
                    break;
                case 2:
                    Rotate180();
                    break;
                case 3:
                    Rotate270();
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a transform that rotates
         * coordinates by the specified number of quadrants around
         * the specified anchor point.
         * This method is equivalent to calling:
         * <pre>
         *     rotate(numquadrants * Math.PI / 2.0, anchorx, anchory);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         *
         * @param numquadrants the number of 90 degree arcs to rotate by
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void QuadrantRotate(int numquadrants,
                       double anchorx, double anchory)
        {
            switch (numquadrants & 3)
            {
                case 0:
                    return;
                case 1:
                    _m02 += anchorx * (_m00 - _m01) + anchory * (_m01 + _m00);
                    _m12 += anchorx * (_m10 - _m11) + anchory * (_m11 + _m10);
                    Rotate90();
                    break;
                case 2:
                    _m02 += anchorx * (_m00 + _m00) + anchory * (_m01 + _m01);
                    _m12 += anchorx * (_m10 + _m10) + anchory * (_m11 + _m11);
                    Rotate180();
                    break;
                case 3:
                    _m02 += anchorx * (_m00 + _m01) + anchory * (_m01 - _m00);
                    _m12 += anchorx * (_m10 + _m11) + anchory * (_m11 - _m10);
                    Rotate270();
                    break;
            }
            if (_m02 == 0.0 && _m12 == 0.0)
            {
                _state &= ~APPLY_TRANSLATE;
            }
            else
            {
                _state |= APPLY_TRANSLATE;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a scaling transformation.
         * This is equivalent to calling concatenate(S), where S is an
         * <code>AffineTransform</code> represented by the following matrix:
         * <pre>
         *		[   sx   0    0   ]
         *		[   0    sy   0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param sx the factor by which coordinates are scaled along the
         * X axis direction
         * @param sy the factor by which coordinates are scaled along the
         * Y axis direction
         */
        public void Scale(double sx, double sy)
        {
            var currentState = _state;
            switch (currentState)
            {
                default:
                    StateError();
                    /* NOTREACHED */
                    return;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SHEAR | APPLY_SCALE):
                    _m00 *= sx;
                    _m11 *= sy;
                    if (_m01 == 0 && _m10 == 0)
                    {
                        currentState &= APPLY_TRANSLATE;
                        if (_m00 == 1.0 && _m11 == 1.0)
                        {
                            _type = (currentState == APPLY_IDENTITY
                                 ? TYPE_IDENTITY
                                 : TYPE_TRANSLATION);
                        }
                        else
                        {
                            currentState |= APPLY_SCALE;
                            _type = TYPE_UNKNOWN;
                        }
                        _state = currentState;
                    }
                    return;
                /* NOBREAK */
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    _m01 *= sy;
                    _m10 *= sx;
                    if (_m01 == 0 && _m10 == 0)
                    {
                        currentState &= APPLY_TRANSLATE;
                        if (_m00 == 1.0 && _m11 == 1.0)
                        {
                            _type = (currentState == APPLY_IDENTITY
                                 ? TYPE_IDENTITY
                                 : TYPE_TRANSLATION);
                        }
                        else
                        {
                            currentState |= APPLY_SCALE;
                            _type = TYPE_UNKNOWN;
                        }
                        _state = currentState;
                    }
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    _m00 *= sx;
                    _m11 *= sy;
                    if (_m00 == 1.0 && _m11 == 1.0)
                    {
                        _state = (currentState &= APPLY_TRANSLATE);
                        _type = (currentState == APPLY_IDENTITY
                                 ? TYPE_IDENTITY
                                 : TYPE_TRANSLATION);
                    }
                    else
                    {
                        _type = TYPE_UNKNOWN;
                    }
                    return;
                case (APPLY_TRANSLATE):
                case (APPLY_IDENTITY):
                    _m00 = sx;
                    _m11 = sy;
                    if (sx != 1.0 || sy != 1.0)
                    {
                        _state = currentState | APPLY_SCALE;
                        _type = TYPE_UNKNOWN;
                    }
                    return;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates this transform with a shearing transformation.
         * This is equivalent to calling concatenate(SH), where SH is an
         * <code>AffineTransform</code> represented by the following matrix:
         * <pre>
         *		[   1   shx   0   ]
         *		[  shy   1    0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param shx the multiplier by which coordinates are shifted in the
         * direction of the positive X axis as a factor of their Y coordinate
         * @param shy the multiplier by which coordinates are shifted in the
         * direction of the positive Y axis as a factor of their X coordinate
         */
        public void Shear(double shx, double shy)
        {
            var currentState = _state;
            switch (currentState)
            {
                default:
                    StateError();
                    return;
                /* NOTREACHED */
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SHEAR | APPLY_SCALE):
                    double m0 = _m00;
                    double m1 = _m01;
                    _m00 = m0 + m1 * shy;
                    _m01 = m0 * shx + m1;

                    m0 = _m10;
                    m1 = _m11;
                    _m10 = m0 + m1 * shy;
                    _m11 = m0 * shx + m1;
                    UpdateState();
                    return;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    _m00 = _m01 * shy;
                    _m11 = _m10 * shx;
                    if (_m00 != 0.0 || _m11 != 0.0)
                    {
                        _state = currentState | APPLY_SCALE;
                    }
                    _type = TYPE_UNKNOWN;
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    _m01 = _m00 * shx;
                    _m10 = _m11 * shy;
                    if (_m01 != 0.0 || _m10 != 0.0)
                    {
                        _state = currentState | APPLY_SHEAR;
                    }
                    _type = TYPE_UNKNOWN;
                    return;
                case (APPLY_TRANSLATE):
                case (APPLY_IDENTITY):
                    _m01 = shx;
                    _m10 = shy;
                    if (_m01 != 0.0 || _m10 != 0.0)
                    {
                        _state = currentState | APPLY_SCALE | APPLY_SHEAR;
                        _type = TYPE_UNKNOWN;
                    }
                    return;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Resets this transform to the Identity transform.
         */
        public void SetToIdentity()
        {
            _m00 = _m11 = 1.0;
            _m10 = _m01 = _m02 = _m12 = 0.0;
            _state = APPLY_IDENTITY;
            _type = TYPE_IDENTITY;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a translation transformation.
         * The matrix representing this transform becomes:
         * <pre>
         *		[   1    0    tx  ]
         *		[   0    1    ty  ]
         *		[   0    0    1   ]
         * </pre>
         * @param tx the distance by which coordinates are translated in the
         * X axis direction
         * @param ty the distance by which coordinates are translated in the
         * Y axis direction
         */
        public void SetToTranslation(double tx, double ty)
        {
            _m00 = 1.0;
            _m10 = 0.0;
            _m01 = 0.0;
            _m11 = 1.0;
            _m02 = tx;
            _m12 = ty;
            if (tx != 0.0 || ty != 0.0)
            {
                _state = APPLY_TRANSLATE;
                _type = TYPE_TRANSLATION;
            }
            else
            {
                _state = APPLY_IDENTITY;
                _type = TYPE_IDENTITY;
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a rotation transformation.
         * The matrix representing this transform becomes:
         * <pre>
         *		[   Cos(theta)    -Sin(theta)    0   ]
         *		[   Sin(theta)     Cos(theta)    0   ]
         *		[       0              0         1   ]
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         * @param theta the angle of rotation measured in radians
         */
        public void SetToRotation(double theta)
        {
            double sin = Math.Sin(theta);
            double cos;
            if (sin == 1.0 || sin == -1.0)
            {
                cos = 0.0;
                _state = APPLY_SHEAR;
                _type = TYPE_QUADRANT_ROTATION;
            }
            else
            {
                cos = Math.Cos(theta);
                if (cos == -1.0)
                {
                    sin = 0.0;
                    _state = APPLY_SCALE;
                    _type = TYPE_QUADRANT_ROTATION;
                }
                else if (cos == 1.0)
                {
                    sin = 0.0;
                    _state = APPLY_IDENTITY;
                    _type = TYPE_IDENTITY;
                }
                else
                {
                    _state = APPLY_SHEAR | APPLY_SCALE;
                    _type = TYPE_GENERAL_ROTATION;
                }
            }
            _m00 = cos;
            _m10 = sin;
            _m01 = -sin;
            _m11 = cos;
            _m02 = 0.0;
            _m12 = 0.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a translated rotation transformation.
         * This operation is equivalent to translating the coordinates so
         * that the anchor point is at the origin (S1), then rotating them
         * about the new origin (S2), and finally translating so that the
         * intermediate origin is restored to the coordinates of the original
         * anchor point (S3).
         * <p>
         * This operation is equivalent to the following sequence of calls:
         * <pre>
         *     setToTranslation(anchorx, anchory); // S3: final translation
         *     rotate(theta);                      // S2: rotate around anchor
         *     translate(-anchorx, -anchory);      // S1: translate anchor to origin
         * </pre>
         * The matrix representing this transform becomes:
         * <pre>
         *		[   Cos(theta)    -Sin(theta)    x-x*Cos+y*Sin  ]
         *		[   Sin(theta)     Cos(theta)    y-x*Sin-y*Cos  ]
         *		[       0              0               1        ]
         * </pre>
         * Rotating by a positive angle theta rotates points on the positive
         * X axis toward the positive Y axis.
         * also the discussion of
         * <a href="#quadrantapproximation">Handling 90-Degree Rotations</a>
         * above.
         *
         * @param theta the angle of rotation measured in radians
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void SetToRotation(double theta, double anchorx, double anchory)
        {
            SetToRotation(theta);
            var sin = _m10;
            var oneMinusCos = 1.0 - _m00;
            _m02 = anchorx * oneMinusCos + anchory * sin;
            _m12 = anchory * oneMinusCos - anchorx * sin;
            if (_m02 != 0.0 || _m12 != 0.0)
            {
                _state |= APPLY_TRANSLATE;
                _type |= TYPE_TRANSLATION;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a rotation transformation that rotates
         * coordinates according to a rotation vector.
         * All coordinates rotate about the origin by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * the transform is set to an identity transform.
         * This operation is equivalent to calling:
         * <pre>
         *     setToRotation(Math.Atan2(vecy, vecx));
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         */
        public void SetToRotation(double vecx, double vecy)
        {
            double sin, cos;
            if (vecy == 0)
            {
                sin = 0.0;
                if (vecx < 0.0)
                {
                    cos = -1.0;
                    _state = APPLY_SCALE;
                    _type = TYPE_QUADRANT_ROTATION;
                }
                else
                {
                    cos = 1.0;
                    _state = APPLY_IDENTITY;
                    _type = TYPE_IDENTITY;
                }
            }
            else if (vecx == 0)
            {
                cos = 0.0;
                sin = (vecy > 0.0) ? 1.0 : -1.0;
                _state = APPLY_SHEAR;
                _type = TYPE_QUADRANT_ROTATION;
            }
            else
            {
                double len = Math.Sqrt(vecx * vecx + vecy * vecy);
                cos = vecx / len;
                sin = vecy / len;
                _state = APPLY_SHEAR | APPLY_SCALE;
                _type = TYPE_GENERAL_ROTATION;
            }
            _m00 = cos;
            _m10 = sin;
            _m01 = -sin;
            _m11 = cos;
            _m02 = 0.0;
            _m12 = 0.0;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a rotation transformation that rotates
         * coordinates around an anchor point according to a rotation
         * vector.
         * All coordinates rotate about the specified anchor coordinates
         * by the same amount.
         * The amount of rotation is such that coordinates along the former
         * positive X axis will subsequently align with the vector pointing
         * from the origin to the specified vector coordinates.
         * If both <code>vecx</code> and <code>vecy</code> are 0.0,
         * the transform is set to an identity transform.
         * This operation is equivalent to calling:
         * <pre>
         *     setToTranslation(Math.Atan2(vecy, vecx), anchorx, anchory);
         * </pre>
         *
         * @param vecx the X coordinate of the rotation vector
         * @param vecy the Y coordinate of the rotation vector
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void SetToRotation(double vecx, double vecy,
                      double anchorx, double anchory)
        {
            SetToRotation(vecx, vecy);
            var sin = _m10;
            var oneMinusCos = 1.0 - _m00;
            _m02 = anchorx * oneMinusCos + anchory * sin;
            _m12 = anchory * oneMinusCos - anchorx * sin;
            if (_m02 != 0.0 || _m12 != 0.0)
            {
                _state |= APPLY_TRANSLATE;
                _type |= TYPE_TRANSLATION;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a rotation transformation that rotates
         * coordinates by the specified number of quadrants.
         * This operation is equivalent to calling:
         * <pre>
         *     setToRotation(numquadrants * Math.PI / 2.0);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         * @param numquadrants the number of 90 degree arcs to rotate by
         */
        public void SetToQuadrantRotation(int numquadrants)
        {
            switch (numquadrants & 3)
            {
                case 0:
                    _m00 = 1.0;
                    _m10 = 0.0;
                    _m01 = 0.0;
                    _m11 = 1.0;
                    _m02 = 0.0;
                    _m12 = 0.0;
                    _state = APPLY_IDENTITY;
                    _type = TYPE_IDENTITY;
                    break;
                case 1:
                    _m00 = 0.0;
                    _m10 = 1.0;
                    _m01 = -1.0;
                    _m11 = 0.0;
                    _m02 = 0.0;
                    _m12 = 0.0;
                    _state = APPLY_SHEAR;
                    _type = TYPE_QUADRANT_ROTATION;
                    break;
                case 2:
                    _m00 = -1.0;
                    _m10 = 0.0;
                    _m01 = 0.0;
                    _m11 = -1.0;
                    _m02 = 0.0;
                    _m12 = 0.0;
                    _state = APPLY_SCALE;
                    _type = TYPE_QUADRANT_ROTATION;
                    break;
                case 3:
                    _m00 = 0.0;
                    _m10 = -1.0;
                    _m01 = 1.0;
                    _m11 = 0.0;
                    _m02 = 0.0;
                    _m12 = 0.0;
                    _state = APPLY_SHEAR;
                    _type = TYPE_QUADRANT_ROTATION;
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a translated rotation transformation
         * that rotates coordinates by the specified number of quadrants
         * around the specified anchor point.
         * This operation is equivalent to calling:
         * <pre>
         *     setToRotation(numquadrants * Math.PI / 2.0, anchorx, anchory);
         * </pre>
         * Rotating by a positive number of quadrants rotates points on
         * the positive X axis toward the positive Y axis.
         *
         * @param numquadrants the number of 90 degree arcs to rotate by
         * @param anchorx the X coordinate of the rotation anchor point
         * @param anchory the Y coordinate of the rotation anchor point
         */
        public void SetToQuadrantRotation(int numquadrants,
                          double anchorx, double anchory)
        {
            switch (numquadrants & 3)
            {
                case 0:
                    _m00 = 1.0;
                    _m10 = 0.0;
                    _m01 = 0.0;
                    _m11 = 1.0;
                    _m02 = 0.0;
                    _m12 = 0.0;
                    _state = APPLY_IDENTITY;
                    _type = TYPE_IDENTITY;
                    break;
                case 1:
                    _m00 = 0.0;
                    _m10 = 1.0;
                    _m01 = -1.0;
                    _m11 = 0.0;
                    _m02 = anchorx + anchory;
                    _m12 = anchory - anchorx;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SHEAR;
                        _type = TYPE_QUADRANT_ROTATION;
                    }
                    else
                    {
                        _state = APPLY_SHEAR | APPLY_TRANSLATE;
                        _type = TYPE_QUADRANT_ROTATION | TYPE_TRANSLATION;
                    }
                    break;
                case 2:
                    _m00 = -1.0;
                    _m10 = 0.0;
                    _m01 = 0.0;
                    _m11 = -1.0;
                    _m02 = anchorx + anchorx;
                    _m12 = anchory + anchory;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SCALE;
                        _type = TYPE_QUADRANT_ROTATION;
                    }
                    else
                    {
                        _state = APPLY_SCALE | APPLY_TRANSLATE;
                        _type = TYPE_QUADRANT_ROTATION | TYPE_TRANSLATION;
                    }
                    break;
                case 3:
                    _m00 = 0.0;
                    _m10 = -1.0;
                    _m01 = 1.0;
                    _m11 = 0.0;
                    _m02 = anchorx - anchory;
                    _m12 = anchory + anchorx;
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SHEAR;
                        _type = TYPE_QUADRANT_ROTATION;
                    }
                    else
                    {
                        _state = APPLY_SHEAR | APPLY_TRANSLATE;
                        _type = TYPE_QUADRANT_ROTATION | TYPE_TRANSLATION;
                    }
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a scaling transformation.
         * The matrix representing this transform becomes:
         * <pre>
         *		[   sx   0    0   ]
         *		[   0    sy   0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param sx the factor by which coordinates are scaled along the
         * X axis direction
         * @param sy the factor by which coordinates are scaled along the
         * Y axis direction
         */
        public void SetToScale(double sx, double sy)
        {
            _m00 = sx;
            _m10 = 0.0;
            _m01 = 0.0;
            _m11 = sy;
            _m02 = 0.0;
            _m12 = 0.0;
            if (sx != 1.0 || sy != 1.0)
            {
                _state = APPLY_SCALE;
                _type = TYPE_UNKNOWN;
            }
            else
            {
                _state = APPLY_IDENTITY;
                _type = TYPE_IDENTITY;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a shearing transformation.
         * The matrix representing this transform becomes:
         * <pre>
         *		[   1   shx   0   ]
         *		[  shy   1    0   ]
         *		[   0    0    1   ]
         * </pre>
         * @param shx the multiplier by which coordinates are shifted in the
         * direction of the positive X axis as a factor of their Y coordinate
         * @param shy the multiplier by which coordinates are shifted in the
         * direction of the positive Y axis as a factor of their X coordinate
         */
        public void SetToShear(double shx, double shy)
        {
            _m00 = 1.0;
            _m01 = shx;
            _m10 = shy;
            _m11 = 1.0;
            _m02 = 0.0;
            _m12 = 0.0;
            if (shx != 0.0 || shy != 0.0)
            {
                _state = (APPLY_SHEAR | APPLY_SCALE);
                _type = TYPE_UNKNOWN;
            }
            else
            {
                _state = APPLY_IDENTITY;
                _type = TYPE_IDENTITY;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to a copy of the transform in the specified
         * <code>AffineTransform</code> object.
         * @param Tx the <code>AffineTransform</code> object from which to
         * copy the transform
         */
        public void SetTransform(AffineTransform tx)
        {
            _m00 = tx._m00;
            _m10 = tx._m10;
            _m01 = tx._m01;
            _m11 = tx._m11;
            _m02 = tx._m02;
            _m12 = tx._m12;
            _state = tx._state;
            _type = tx._type;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to the matrix specified by the 6
         * double precision values.
         *
         * @param m00 the X coordinate scaling element of the 3x3 matrix
         * @param m10 the Y coordinate shearing element of the 3x3 matrix
         * @param m01 the X coordinate shearing element of the 3x3 matrix
         * @param m11 the Y coordinate scaling element of the 3x3 matrix
         * @param m02 the X coordinate translation element of the 3x3 matrix
         * @param m12 the Y coordinate translation element of the 3x3 matrix
         */
        public void SetTransform(double m00, double m10,
                     double m01, double m11,
                     double m02, double m12)
        {
            _m00 = m00;
            _m10 = m10;
            _m01 = m01;
            _m11 = m11;
            _m02 = m02;
            _m12 = m12;
            UpdateState();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates an <code>AffineTransform</code> <code>Tx</code> to
         * this <code>AffineTransform</code> Cx in the most commonly useful
         * way to provide a new user space
         * that is mapped to the former user space by <code>Tx</code>.
         * Cx is updated to perform the combined transformation.
         * Transforming a point p by the updated transform Cx' is
         * equivalent to first transforming p by <code>Tx</code> and then
         * transforming the result by the original transform Cx like this:
         * Cx'(p) = Cx(Tx(p))
         * In matrix notation, if this transform Cx is
         * represented by the matrix [this] and <code>Tx</code> is represented
         * by the matrix [Tx] then this method does the following:
         * <pre>
         *		[this] = [this] x [Tx]
         * </pre>
         * @param Tx the <code>AffineTransform</code> object to be
         * concatenated with this <code>AffineTransform</code> object.
         */
        public void Concatenate(AffineTransform tx)
        {
            double m0, m1;
            double t01, t10;
            int mystate = _state;
            int txstate = tx._state;
            switch ((txstate << HI_SHIFT) | mystate)
            {

                /* ---------- Tx == IDENTITY cases ---------- */
                case (HI_IDENTITY | APPLY_IDENTITY):
                case (HI_IDENTITY | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SCALE):
                case (HI_IDENTITY | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SHEAR):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_SCALE):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    return;

                /* ---------- this == IDENTITY cases ---------- */
                case (HI_SHEAR | HI_SCALE | HI_TRANSLATE | APPLY_IDENTITY):
                    _m01 = tx._m01;
                    _m10 = tx._m10;
                    _m00 = tx._m00;
                    _m11 = tx._m11;
                    _m02 = tx._m02;
                    _m12 = tx._m12;
                    _state = txstate;
                    _type = tx._type;
                    return;

                case (HI_SCALE | HI_TRANSLATE | APPLY_IDENTITY):
                    _m00 = tx._m00;
                    _m11 = tx._m11;
                    _m02 = tx._m02;
                    _m12 = tx._m12;
                    _state = txstate;
                    _type = tx._type;
                    return;
                case (HI_TRANSLATE | APPLY_IDENTITY):
                    _m02 = tx._m02;
                    _m12 = tx._m12;
                    _state = txstate;
                    _type = tx._type;
                    return;
                case (HI_SHEAR | HI_SCALE | APPLY_IDENTITY):
                    _m01 = tx._m01;
                    _m10 = tx._m10;
                    _m00 = tx._m00;
                    _m11 = tx._m11;
                    _state = txstate;
                    _type = tx._type;
                    return;
                case (HI_SCALE | APPLY_IDENTITY):
                    _m00 = tx._m00;
                    _m11 = tx._m11;
                    _state = txstate;
                    _type = tx._type;
                    return;
                case (HI_SHEAR | HI_TRANSLATE | APPLY_IDENTITY):
                    _m02 = tx._m02;
                    _m12 = tx._m12;
                    _m01 = tx._m01;
                    _m10 = tx._m10;
                    _m00 = _m11 = 0.0;
                    _state = txstate;
                    _type = tx._type;
                    return;
                case (HI_SHEAR | APPLY_IDENTITY):
                    _m01 = tx._m01;
                    _m10 = tx._m10;
                    _m00 = _m11 = 0.0;
                    _state = txstate;
                    _type = tx._type;
                    return;

                /* ---------- Tx == TRANSLATE cases ---------- */
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_SCALE):
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SHEAR):
                case (HI_TRANSLATE | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SCALE):
                case (HI_TRANSLATE | APPLY_TRANSLATE):
                    Translate(tx._m02, tx._m12);
                    return;

                /* ---------- Tx == SCALE cases ---------- */
                case (HI_SCALE | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SHEAR | APPLY_SCALE):
                case (HI_SCALE | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SHEAR):
                case (HI_SCALE | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SCALE):
                case (HI_SCALE | APPLY_TRANSLATE):
                    Scale(tx._m00, tx._m11);
                    return;

                /* ---------- Tx == SHEAR cases ---------- */
                case (HI_SHEAR | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SHEAR | APPLY_SCALE):
                    t01 = tx._m01; t10 = tx._m10;
                    m0 = _m00;
                    _m00 = _m01 * t10;
                    _m01 = m0 * t01;
                    m0 = _m10;
                    _m10 = _m11 * t10;
                    _m11 = m0 * t01;
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SHEAR):
                    _m00 = _m01 * tx._m10;
                    _m01 = 0.0;
                    _m11 = _m10 * tx._m01;
                    _m10 = 0.0;
                    _state = mystate ^ (APPLY_SHEAR | APPLY_SCALE);
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SCALE):
                    _m01 = _m00 * tx._m01;
                    _m00 = 0.0;
                    _m10 = _m11 * tx._m10;
                    _m11 = 0.0;
                    _state = mystate ^ (APPLY_SHEAR | APPLY_SCALE);
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_TRANSLATE):
                    _m00 = 0.0;
                    _m01 = tx._m01;
                    _m10 = tx._m10;
                    _m11 = 0.0;
                    _state = APPLY_TRANSLATE | APPLY_SHEAR;
                    _type = TYPE_UNKNOWN;
                    return;
            }
            // If Tx has more than one attribute, it is not worth optimizing
            // all of those cases...
            double t00 = tx._m00; t01 = tx._m01; double t02 = tx._m02;
            t10 = tx._m10; double t11 = tx._m11; double t12 = tx._m12;
            switch (mystate)
            {
                default:
                    StateError();
                    return;
                case (APPLY_SHEAR | APPLY_SCALE):
                    _state = mystate | txstate;
                    m0 = _m00;
                    m1 = _m01;
                    _m00 = t00 * m0 + t10 * m1;
                    _m01 = t01 * m0 + t11 * m1;
                    _m02 += t02 * m0 + t12 * m1;

                    m0 = _m10;
                    m1 = _m11;
                    _m10 = t00 * m0 + t10 * m1;
                    _m11 = t01 * m0 + t11 * m1;
                    _m12 += t02 * m0 + t12 * m1;
                    _type = TYPE_UNKNOWN;
                    return;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    m0 = _m00;
                    m1 = _m01;
                    _m00 = t00 * m0 + t10 * m1;
                    _m01 = t01 * m0 + t11 * m1;
                    _m02 += t02 * m0 + t12 * m1;

                    m0 = _m10;
                    m1 = _m11;
                    _m10 = t00 * m0 + t10 * m1;
                    _m11 = t01 * m0 + t11 * m1;
                    _m12 += t02 * m0 + t12 * m1;
                    _type = TYPE_UNKNOWN;
                    return;

                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    m0 = _m01;
                    _m00 = t10 * m0;
                    _m01 = t11 * m0;
                    _m02 += t12 * m0;

                    m0 = _m10;
                    _m10 = t00 * m0;
                    _m11 = t01 * m0;
                    _m12 += t02 * m0;
                    break;

                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    m0 = _m00;
                    _m00 = t00 * m0;
                    _m01 = t01 * m0;
                    _m02 += t02 * m0;

                    m0 = _m11;
                    _m10 = t10 * m0;
                    _m11 = t11 * m0;
                    _m12 += t12 * m0;
                    break;

                case (APPLY_TRANSLATE):
                    _m00 = t00;
                    _m01 = t01;
                    _m02 += t02;

                    _m10 = t10;
                    _m11 = t11;
                    _m12 += t12;
                    _state = txstate | APPLY_TRANSLATE;
                    _type = TYPE_UNKNOWN;
                    return;
            }
            UpdateState();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Concatenates an <code>AffineTransform</code> <code>Tx</code> to
         * this <code>AffineTransform</code> Cx
         * in a less commonly used way such that <code>Tx</code> modifies the
         * coordinate transformation relative to the absolute pixel
         * space rather than relative to the existing user space.
         * Cx is updated to perform the combined transformation.
         * Transforming a point p by the updated transform Cx' is
         * equivalent to first transforming p by the original transform
         * Cx and then transforming the result by
         * <code>Tx</code> like this:
         * Cx'(p) = Tx(Cx(p))
         * In matrix notation, if this transform Cx
         * is represented by the matrix [this] and <code>Tx</code> is
         * represented by the matrix [Tx] then this method does the
         * following:
         * <pre>
         *		[this] = [Tx] x [this]
         * </pre>
         * @param Tx the <code>AffineTransform</code> object to be
         * concatenated with this <code>AffineTransform</code> object.
         */
        public void PreConcatenate(AffineTransform tx)
        {
            double m0;
            double t00, t01, t10, t11;
            int mystate = _state;
            int txstate = tx._state;
            switch ((txstate << HI_SHIFT) | mystate)
            {
                case (HI_IDENTITY | APPLY_IDENTITY):
                case (HI_IDENTITY | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SCALE):
                case (HI_IDENTITY | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SHEAR):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_SCALE):
                case (HI_IDENTITY | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    // Tx is IDENTITY...
                    return;

                case (HI_TRANSLATE | APPLY_IDENTITY):
                case (HI_TRANSLATE | APPLY_SCALE):
                case (HI_TRANSLATE | APPLY_SHEAR):
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_SCALE):
                    // Tx is TRANSLATE, this has no TRANSLATE
                    _m02 = tx._m02;
                    _m12 = tx._m12;
                    _state = mystate | APPLY_TRANSLATE;
                    _type |= TYPE_TRANSLATION;
                    return;

                case (HI_TRANSLATE | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_TRANSLATE | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    // Tx is TRANSLATE, this has one too
                    _m02 = _m02 + tx._m02;
                    _m12 = _m12 + tx._m12;
                    return;

                case (HI_SCALE | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_IDENTITY):
                    // Only these two existing states need a new state
                    _state = mystate | APPLY_SCALE;
                    t00 = tx._m00;
                    t11 = tx._m11;
                    if ((mystate & APPLY_SHEAR) != 0)
                    {
                        _m01 = _m01 * t00;
                        _m10 = _m10 * t11;
                        if ((mystate & APPLY_SCALE) != 0)
                        {
                            _m00 = _m00 * t00;
                            _m11 = _m11 * t11;
                        }
                    }
                    else
                    {
                        _m00 = _m00 * t00;
                        _m11 = _m11 * t11;
                    }
                    if ((mystate & APPLY_TRANSLATE) != 0)
                    {
                        _m02 = _m02 * t00;
                        _m12 = _m12 * t11;
                    }
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SCALE | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SHEAR | APPLY_SCALE):
                case (HI_SCALE | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SHEAR):
                case (HI_SCALE | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SCALE | APPLY_SCALE):
                    // Tx is SCALE, this is anything
                    t00 = tx._m00;
                    t11 = tx._m11;
                    if ((mystate & APPLY_SHEAR) != 0)
                    {
                        _m01 = _m01 * t00;
                        _m10 = _m10 * t11;
                        if ((mystate & APPLY_SCALE) != 0)
                        {
                            _m00 = _m00 * t00;
                            _m11 = _m11 * t11;
                        }
                    }
                    else
                    {
                        _m00 = _m00 * t00;
                        _m11 = _m11 * t11;
                    }
                    if ((mystate & APPLY_TRANSLATE) != 0)
                    {
                        _m02 = _m02 * t00;
                        _m12 = _m12 * t11;
                    }
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_SHEAR | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SHEAR):
                    mystate = mystate | APPLY_SCALE;
                    _state = mystate ^ APPLY_SHEAR;
                    // Tx is SHEAR, this is anything
                    t01 = tx._m01;
                    t10 = tx._m10;

                    m0 = _m00;
                    _m00 = _m10 * t01;
                    _m10 = m0 * t10;

                    m0 = _m01;
                    _m01 = _m11 * t01;
                    _m11 = m0 * t10;

                    m0 = _m02;
                    _m02 = _m12 * t01;
                    _m12 = m0 * t10;
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_IDENTITY):
                case (HI_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SCALE):
                    _state = mystate ^ APPLY_SHEAR;
                    // Tx is SHEAR, this is anything
                    t01 = tx._m01;
                    t10 = tx._m10;

                    m0 = _m00;
                    _m00 = _m10 * t01;
                    _m10 = m0 * t10;

                    m0 = _m01;
                    _m01 = _m11 * t01;
                    _m11 = m0 * t10;

                    m0 = _m02;
                    _m02 = _m12 * t01;
                    _m12 = m0 * t10;
                    _type = TYPE_UNKNOWN;
                    return;
                case (HI_SHEAR | APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (HI_SHEAR | APPLY_SHEAR | APPLY_SCALE):
                    // Tx is SHEAR, this is anything
                    t01 = tx._m01;
                    t10 = tx._m10;

                    m0 = _m00;
                    _m00 = _m10 * t01;
                    _m10 = m0 * t10;

                    m0 = _m01;
                    _m01 = _m11 * t01;
                    _m11 = m0 * t10;

                    m0 = _m02;
                    _m02 = _m12 * t01;
                    _m12 = m0 * t10;
                    _type = TYPE_UNKNOWN;
                    return;
            }
            // If Tx has more than one attribute, it is not worth optimizing
            // all of those cases...
            t00 = tx._m00; t01 = tx._m01; double t02 = tx._m02;
            t10 = tx._m10; t11 = tx._m11; double t12 = tx._m12;
            switch (mystate)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    m0 = _m02;
                    double m1 = _m12;
                    t02 += m0 * t00 + m1 * t01;
                    t12 += m0 * t10 + m1 * t11;

                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m00;
                    m1 = _m10;
                    _m00 = m0 * t00 + m1 * t01;
                    _m10 = m0 * t10 + m1 * t11;

                    m0 = _m01;
                    m1 = _m11;
                    _m01 = m0 * t00 + m1 * t01;
                    _m11 = m0 * t10 + m1 * t11;
                    break;
                case (APPLY_SHEAR | APPLY_SCALE):
                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m00;
                    m1 = _m10;
                    _m00 = m0 * t00 + m1 * t01;
                    _m10 = m0 * t10 + m1 * t11;

                    m0 = _m01;
                    m1 = _m11;
                    _m01 = m0 * t00 + m1 * t01;
                    _m11 = m0 * t10 + m1 * t11;
                    break;

                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    m0 = _m02;
                    m1 = _m12;
                    t02 += m0 * t00 + m1 * t01;
                    t12 += m0 * t10 + m1 * t11;

                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m10;
                    _m00 = m0 * t01;
                    _m10 = m0 * t11;

                    m0 = _m01;
                    _m01 = m0 * t00;
                    _m11 = m0 * t10;
                    break;
                case (APPLY_SHEAR):
                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m10;
                    _m00 = m0 * t01;
                    _m10 = m0 * t11;

                    m0 = _m01;
                    _m01 = m0 * t00;
                    _m11 = m0 * t10;
                    break;

                case (APPLY_SCALE | APPLY_TRANSLATE):
                    m0 = _m02;
                    m1 = _m12;
                    t02 += m0 * t00 + m1 * t01;
                    t12 += m0 * t10 + m1 * t11;

                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m00;
                    _m00 = m0 * t00;
                    _m10 = m0 * t10;

                    m0 = _m11;
                    _m01 = m0 * t01;
                    _m11 = m0 * t11;
                    break;
                case (APPLY_SCALE):
                    _m02 = t02;
                    _m12 = t12;

                    m0 = _m00;
                    _m00 = m0 * t00;
                    _m10 = m0 * t10;

                    m0 = _m11;
                    _m01 = m0 * t01;
                    _m11 = m0 * t11;
                    break;

                case (APPLY_TRANSLATE):
                    m0 = _m02;
                    m1 = _m12;
                    t02 += m0 * t00 + m1 * t01;
                    t12 += m0 * t10 + m1 * t11;

                    _m02 = t02;
                    _m12 = t12;

                    _m00 = t00;
                    _m10 = t10;

                    _m01 = t01;
                    _m11 = t11;

                    _state = mystate | txstate;
                    _type = TYPE_UNKNOWN;
                    return;
                case (APPLY_IDENTITY):
                    _m02 = t02;
                    _m12 = t12;

                    _m00 = t00;
                    _m10 = t10;

                    _m01 = t01;
                    _m11 = t11;

                    _state = mystate | txstate;
                    _type = TYPE_UNKNOWN;
                    return;
            }
            UpdateState();
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns an <code>AffineTransform</code> object representing the
         * inverse transformation.
         * The inverse transform Tx' of this transform Tx
         * maps coordinates transformed by Tx back
         * to their original coordinates.
         * In other words, Tx'(Tx(p)) = p = Tx(Tx'(p)).
         * <p>
         * If this transform maps all coordinates onto a point or a line
         * then it will not have an inverse, since coordinates that do
         * not lie on the destination point or line will not have an inverse
         * mapping.
         * The <code>getDeterminant</code> method can be used to determine if this
         * transform has no inverse, in which case an exception will be
         * thrown if the <code>createInverse</code> method is called.
         * @return a new <code>AffineTransform</code> object representing the
         * inverse transformation.
         * @exception NoninvertibleTransformException
         * if the matrix cannot be inverted.
         */
        public AffineTransform CreateInverse()
        {
            double det;
            switch (_state)
            {
                default:
                    StateError();
                    return null;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    det = _m00 * _m11 - _m01 * _m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    return new AffineTransform(_m11 / det, -_m10 / det,
                                   -_m01 / det, _m00 / det,
                                   (_m01 * _m12 - _m11 * _m02) / det,
                                   (_m10 * _m02 - _m00 * _m12) / det,
                                   (APPLY_SHEAR |
                                APPLY_SCALE |
                                APPLY_TRANSLATE));
                case (APPLY_SHEAR | APPLY_SCALE):
                    det = _m00 * _m11 - _m01 * _m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    return new AffineTransform(_m11 / det, -_m10 / det,
                                   -_m01 / det, _m00 / det,
                                    0.0, 0.0,
                                   (APPLY_SHEAR | APPLY_SCALE));
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    if (_m01 == 0.0 || _m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    return new AffineTransform(0.0, 1.0 / _m01,
                                    1.0 / _m10, 0.0,
                                   -_m12 / _m10, -_m02 / _m01,
                                   (APPLY_SHEAR | APPLY_TRANSLATE));
                case (APPLY_SHEAR):
                    if (_m01 == 0.0 || _m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    return new AffineTransform(0.0, 1.0 / _m01,
                                   1.0 / _m10, 0.0,
                                   0.0, 0.0,
                                   (APPLY_SHEAR));
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    if (_m00 == 0.0 || _m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    return new AffineTransform(1.0 / _m00, 0.0,
                                    0.0, 1.0 / _m11,
                                   -_m02 / _m00, -_m12 / _m11,
                                   (APPLY_SCALE | APPLY_TRANSLATE));
                case (APPLY_SCALE):
                    if (_m00 == 0.0 || _m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    return new AffineTransform(1.0 / _m00, 0.0,
                                   0.0, 1.0 / _m11,
                                   0.0, 0.0,
                                   (APPLY_SCALE));
                case (APPLY_TRANSLATE):
                    return new AffineTransform(1.0, 0.0,
                                    0.0, 1.0,
                                   -_m02, -_m12,
                                   (APPLY_TRANSLATE));
                case (APPLY_IDENTITY):
                    return new AffineTransform();
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Sets this transform to the inverse of itself.
         * The inverse transform Tx' of this transform Tx
         * maps coordinates transformed by Tx back
         * to their original coordinates.
         * In other words, Tx'(Tx(p)) = p = Tx(Tx'(p)).
         * <p>
         * If this transform maps all coordinates onto a point or a line
         * then it will not have an inverse, since coordinates that do
         * not lie on the destination point or line will not have an inverse
         * mapping.
         * The <code>getDeterminant</code> method can be used to determine if this
         * transform has no inverse, in which case an exception will be
         * thrown if the <code>invert</code> method is called.
         * @exception NoninvertibleTransformException
         * if the matrix cannot be inverted.
         */
        public void Invert()
        {
            double m00, m01, m02;
            double m10, m11, m12;
            double det;
            switch (_state)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m01 = _m01; m02 = _m02;
                    m10 = _m10; m11 = _m11; m12 = _m12;
                    det = m00 * m11 - m01 * m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    _m00 = m11 / det;
                    _m10 = -m10 / det;
                    _m01 = -m01 / det;
                    _m11 = m00 / det;
                    _m02 = (m01 * m12 - m11 * m02) / det;
                    _m12 = (m10 * m02 - m00 * m12) / det;
                    break;
                case (APPLY_SHEAR | APPLY_SCALE):
                    m00 = _m00; m01 = _m01;
                    m10 = _m10; m11 = _m11;
                    det = m00 * m11 - m01 * m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    _m00 = m11 / det;
                    _m10 = -m10 / det;
                    _m01 = -m01 / det;
                    _m11 = m00 / det;
                    // m02 = 0.0;
                    // m12 = 0.0;
                    break;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    m01 = _m01; m02 = _m02;
                    m10 = _m10; m12 = _m12;
                    if (m01 == 0.0 || m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    // m00 = 0.0;
                    _m10 = 1.0 / m01;
                    _m01 = 1.0 / m10;
                    // m11 = 0.0;
                    _m02 = -m12 / m10;
                    _m12 = -m02 / m01;
                    break;
                case (APPLY_SHEAR):
                    m01 = _m01;
                    m10 = _m10;
                    if (m01 == 0.0 || m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    // m00 = 0.0;
                    _m10 = 1.0 / m01;
                    _m01 = 1.0 / m10;
                    // m11 = 0.0;
                    // m02 = 0.0;
                    // m12 = 0.0;
                    break;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m02 = _m02;
                    m11 = _m11; m12 = _m12;
                    if (m00 == 0.0 || m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    _m00 = 1.0 / m00;
                    // m10 = 0.0;
                    // m01 = 0.0;
                    _m11 = 1.0 / m11;
                    _m02 = -m02 / m00;
                    _m12 = -m12 / m11;
                    break;
                case (APPLY_SCALE):
                    m00 = _m00;
                    m11 = _m11;
                    if (m00 == 0.0 || m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    _m00 = 1.0 / m00;
                    // m10 = 0.0;
                    // m01 = 0.0;
                    _m11 = 1.0 / m11;
                    // m02 = 0.0;
                    // m12 = 0.0;
                    break;
                case (APPLY_TRANSLATE):
                    // m00 = 1.0;
                    // m10 = 0.0;
                    // m01 = 0.0;
                    // m11 = 1.0;
                    _m02 = -_m02;
                    _m12 = -_m12;
                    break;
                case (APPLY_IDENTITY):
                    // m00 = 1.0;
                    // m10 = 0.0;
                    // m01 = 0.0;
                    // m11 = 1.0;
                    // m02 = 0.0;
                    // m12 = 0.0;
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms the specified <code>ptSrc</code> and stores the result
         * in <code>ptDst</code>.
         * If <code>ptDst</code> is <code>null</code>, a new Point
         * object is allocated and then the result of the transformation is
         * stored in this object.
         * In either case, <code>ptDst</code>, which contains the
         * transformed point, is returned for convenience.
         * If <code>ptSrc</code> and <code>ptDst</code> are the same
         * object, the input point is correctly overwritten with
         * the transformed point.
         * @param ptSrc the specified <code>Point</code> to be transformed
         * @param ptDst the specified <code>Point</code> that stores the
         * result of transforming <code>ptSrc</code>
         * @return the <code>ptDst</code> after transforming
         * <code>ptSrc</code> and stroring the result in <code>ptDst</code>.
         */
        public Point Transform(Point ptSrc, Point ptDst)
        {
            if (ptDst == null)
            {
                ptDst = new Point();
            }
            // Copy source coords into local variables in case src == dst
            double x = ptSrc.GetX();
            double y = ptSrc.GetY();
            switch (_state)
            {
                default:
                    StateError();
                    return ptDst;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    ptDst.SetLocation(Round(x * _m00 + y * _m01 + _m02),
                              Round(x * _m10 + y * _m11 + _m12));
                    return ptDst;
                case (APPLY_SHEAR | APPLY_SCALE):
                    ptDst.SetLocation(Round(x * _m00 + y * _m01),
                                Round(x * _m10 + y * _m11));
                    return ptDst;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    ptDst.SetLocation(Round(y * _m01 + _m02),
                                Round(x * _m10 + _m12));
                    return ptDst;
                case (APPLY_SHEAR):
                    ptDst.SetLocation(Round(y * _m01),
                                Round(x * _m10));
                    return ptDst;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    ptDst.SetLocation(Round(x * _m00 + _m02),
                                Round(y * _m11 + _m12));
                    return ptDst;
                case (APPLY_SCALE):
                    ptDst.SetLocation(Round(x * _m00),
                                Round(y * _m11));
                    return ptDst;
                case (APPLY_TRANSLATE):
                    ptDst.SetLocation(Round(x + _m02),
                                Round(y + _m12));
                    return ptDst;
                case (APPLY_IDENTITY):
                    ptDst.SetLocation(Round(x), Round(y));
                    return ptDst;
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms an array of point objects by this transform.
         * If any element of the <code>ptDst</code> array is
         * <code>null</code>, a new <code>Point</code> object is allocated
         * and stored into that element before storing the results of the
         * transformation.
         * <p>
         * that this method does not take any precautions to
         * avoid problems caused by storing results into <code>Point</code>
         * objects that will be used as the source for calculations
         * further down the source array.
         * This method does guarantee that if a specified <code>Point</code>
         * object is both the source and destination for the same single point
         * transform operation then the results will not be stored until
         * the calculations are complete to avoid storing the results on
         * top of the operands.
         * If, however, the destination <code>Point</code> object for one
         * operation is the same object as the source <code>Point</code>
         * object for another operation further down the source array then
         * the original coordinates in that point are overwritten before
         * they can be converted.
         * @param ptSrc the array containing the source point objects
         * @param ptDst the array into which the transform point objects are
         * returned
         * @param srcOff the offset to the first point object to be
         * transformed in the source array
         * @param dstOff the offset to the location of the first
         * transformed point object that is stored in the destination array
         * @param numPts the number of point objects to be transformed
         */
        public void Transform(Point[] ptSrc, int srcOff,
                  Point[] ptDst, int dstOff,
                  int numPts)
        {
            int currentState = _state;
            while (--numPts >= 0)
            {
                // Copy source coords into local variables in case src == dst
                Point src = ptSrc[srcOff++];
                double x = src.GetX();
                double y = src.GetY();
                Point dst = ptDst[dstOff++];
                if (dst == null)
                {
                    dst = new Point();
                    ptDst[dstOff - 1] = dst;
                }
                switch (currentState)
                {
                    default:
                        StateError();
                        break;
                    case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                        dst.SetLocation(Round(x * _m00 + y * _m01 + _m02),
                                Round(x * _m10 + y * _m11 + _m12));
                        break;
                    case (APPLY_SHEAR | APPLY_SCALE):
                        dst.SetLocation(Round(x * _m00 + y * _m01),
                                        Round(x * _m10 + y * _m11));
                        break;
                    case (APPLY_SHEAR | APPLY_TRANSLATE):
                        dst.SetLocation(Round(y * _m01 + _m02),
                                        Round(x * _m10 + _m12));
                        break;
                    case (APPLY_SHEAR):
                        dst.SetLocation(Round(y * _m01), Round(x * _m10));
                        break;
                    case (APPLY_SCALE | APPLY_TRANSLATE):
                        dst.SetLocation(Round(x * _m00 + _m02),
                                        Round(y * _m11 + _m12));
                        break;
                    case (APPLY_SCALE):
                        dst.SetLocation(Round(x * _m00),
                                        Round(y * _m11));
                        break;
                    case (APPLY_TRANSLATE):
                        dst.SetLocation(Round(x + _m02),
                                        Round(y + _m12));
                        break;
                    case (APPLY_IDENTITY):
                        dst.SetLocation(Round(x), Round(y));
                        break;
                }
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms an array of floating point coordinates by this transform.
         * The two coordinate array sections can be exactly the same or
         * can be overlapping sections of the same array without affecting the
         * validity of the results.
         * This method ensures that no source coordinates are overwritten by a
         * previous operation before they can be transformed.
         * The coordinates are stored in the arrays starting at the specified
         * offset in the order <code>[x0, y0, x1, y1, ..., xn, yn]</code>.
         * @param srcPts the array containing the source point coordinates.
         * Each point is stored as a pair of x,&nbsp;y coordinates.
         * @param dstPts the array into which the transformed point coordinates
         * are returned.  Each point is stored as a pair of x,&nbsp;y
         * coordinates.
         * @param srcOff the offset to the first point to be transformed
         * in the source array
         * @param dstOff the offset to the location of the first
         * transformed point that is stored in the destination array
         * @param numPts the number of points to be transformed
         */
        public void Transform(int[] srcPts, int srcOff,
                  int[] dstPts, int dstOff,
                  int numPts)
        {
            double m00, m01, m02, m10, m11, m12;	// For caching
            if (dstPts == srcPts &&
                dstOff > srcOff && dstOff < srcOff + numPts * 2)
            {
                // If the arrays overlap partially with the destination higher
                // than the source and we transform the coordinates normally
                // we would overwrite some of the later source coordinates
                // with results of previous transformations.
                // To get around this we use arraycopy to copy the points
                // to their final destination with correct overwrite
                // handling and then transform them in place in the new
                // safer location.
                Array.Copy(srcPts, srcOff, dstPts, dstOff, numPts * 2);
                // srcPts = dstPts;		// They are known to be equal.
                srcOff = dstOff;
            }
            switch (_state)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m01 = _m01; m02 = _m02;
                    m10 = _m10; m11 = _m11; m12 = _m12;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        double y = srcPts[srcOff++];
                        dstPts[dstOff++] = Round((m00 * x + m01 * y + m02));
                        dstPts[dstOff++] = Round((m10 * x + m11 * y + m12));
                    }
                    return;
                case (APPLY_SHEAR | APPLY_SCALE):
                    m00 = _m00; m01 = _m01;
                    m10 = _m10; m11 = _m11;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        double y = srcPts[srcOff++];
                        dstPts[dstOff++] = Round((m00 * x + m01 * y));
                        dstPts[dstOff++] = Round((m10 * x + m11 * y));
                    }
                    return;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    m01 = _m01; m02 = _m02;
                    m10 = _m10; m12 = _m12;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        dstPts[dstOff++] = Round((m01 * srcPts[srcOff++] + m02));
                        dstPts[dstOff++] = Round((m10 * x + m12));
                    }
                    return;
                case (APPLY_SHEAR):
                    m01 = _m01; m10 = _m10;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        dstPts[dstOff++] = Round((m01 * srcPts[srcOff++]));
                        dstPts[dstOff++] = Round((m10 * x));
                    }
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m02 = _m02;
                    m11 = _m11; m12 = _m12;
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round((m00 * srcPts[srcOff++] + m02));
                        dstPts[dstOff++] = Round((m11 * srcPts[srcOff++] + m12));
                    }
                    return;
                case (APPLY_SCALE):
                    m00 = _m00; m11 = _m11;
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round((m00 * srcPts[srcOff++]));
                        dstPts[dstOff++] = Round((m11 * srcPts[srcOff++]));
                    }
                    return;
                case (APPLY_TRANSLATE):
                    m02 = _m02; m12 = _m12;
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round((srcPts[srcOff++] + m02));
                        dstPts[dstOff++] = Round((srcPts[srcOff++] + m12));
                    }
                    return;
                case (APPLY_IDENTITY):
                    if (srcPts != dstPts || srcOff != dstOff)
                    {
                        Array.Copy(srcPts, srcOff, dstPts, dstOff,
                                 numPts * 2);
                    }
                    return;
            }

            /* NOTREACHED */
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Inverse transforms the specified <code>ptSrc</code> and stores the
         * result in <code>ptDst</code>.
         * If <code>ptDst</code> is <code>null</code>, a new
         * <code>Point</code> object is allocated and then the result of the
         * transform is stored in this object.
         * In either case, <code>ptDst</code>, which contains the transformed
         * point, is returned for convenience.
         * If <code>ptSrc</code> and <code>ptDst</code> are the same
         * object, the input point is correctly overwritten with the
         * transformed point.
         * @param ptSrc the point to be inverse transformed
         * @param ptDst the resulting transformed point
         * @return <code>ptDst</code>, which contains the result of the
         * inverse transform.
         * @exception NoninvertibleTransformException  if the matrix cannot be
         *                                         inverted.
         */
        public Point InverseTransform(Point ptSrc, Point ptDst)
        {
            if (ptDst == null)
            {
                ptDst = new Point();
            }
            // Copy source coords into local variables in case src == dst
            double x = ptSrc.GetX();
            double y = ptSrc.GetY();
            switch (_state)
            {
                default:
                    StateError();
                    return ptDst;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    {
                        x -= _m02;
                        y -= _m12;
                        double det = _m00 * _m11 - _m01 * _m10;
                        if (Math.Abs(det) <= double.MinValue)
                        {
                            throw new NoninvertibleTransformException("Determinant is " +
                                                                      det);
                        }
                        ptDst.SetLocation(Round((x * _m11 - y * _m01) / det),
                                          Round((y * _m00 - x * _m10) / det));
                        return ptDst;
                    }
                case (APPLY_SHEAR | APPLY_SCALE):
                    {
                        double det = _m00 * _m11 - _m01 * _m10;
                        if (Math.Abs(det) <= double.MinValue)
                        {
                            throw new NoninvertibleTransformException("Determinant is " +
                                                                      det);
                        }
                        ptDst.SetLocation(Round((x * _m11 - y * _m01) / det),
                                          Round((y * _m00 - x * _m10) / det));
                        return ptDst;
                    }
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    x -= _m02;
                    y -= _m12;
                    if (_m01 == 0.0 || _m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    ptDst.SetLocation(Round(y / _m10),
                                Round(x / _m01));
                    return ptDst;
                case (APPLY_SHEAR):
                    if (_m01 == 0.0 || _m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    ptDst.SetLocation(Round(y / _m10),
                                Round(x / _m01));
                    return ptDst;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    x -= _m02;
                    y -= _m12;
                    if (_m00 == 0.0 || _m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    ptDst.SetLocation(Round(x / _m00),
                                Round(y / _m11));
                    return ptDst;
                case (APPLY_SCALE):
                    if (_m00 == 0.0 || _m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    ptDst.SetLocation(Round(x / _m00),
                                Round(y / _m11));
                    return ptDst;
                case (APPLY_TRANSLATE):
                    ptDst.SetLocation(Round(x - _m02), Round(y - _m12));
                    return ptDst;
                case (APPLY_IDENTITY):
                    ptDst.SetLocation(Round(x), Round(y));
                    return ptDst;
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Inverse transforms an array of double precision coordinates by
         * this transform.
         * The two coordinate array sections can be exactly the same or
         * can be overlapping sections of the same array without affecting the
         * validity of the results.
         * This method ensures that no source coordinates are
         * overwritten by a previous operation before they can be transformed.
         * The coordinates are stored in the arrays starting at the specified
         * offset in the order <code>[x0, y0, x1, y1, ..., xn, yn]</code>.
         * @param srcPts the array containing the source point coordinates.
         * Each point is stored as a pair of x,&nbsp;y coordinates.
         * @param dstPts the array into which the transformed point
         * coordinates are returned.  Each point is stored as a pair of
         * x,&nbsp;y coordinates.
         * @param srcOff the offset to the first point to be transformed
         * in the source array
         * @param dstOff the offset to the location of the first
         * transformed point that is stored in the destination array
         * @param numPts the number of point objects to be transformed
         * @exception NoninvertibleTransformException  if the matrix cannot be
         *                                         inverted.
         */
        public void InverseTransform(int[] srcPts, int srcOff,
                                     int[] dstPts, int dstOff,
                                     int numPts)
        {
            double m00, m01, m02, m10, m11, m12;	// For caching
            double det;
            if (dstPts == srcPts &&
                dstOff > srcOff && dstOff < srcOff + numPts * 2)
            {
                // If the arrays overlap partially with the destination higher
                // than the source and we transform the coordinates normally
                // we would overwrite some of the later source coordinates
                // with results of previous transformations.
                // To get around this we use arraycopy to copy the points
                // to their final destination with correct overwrite
                // handling and then transform them in place in the new
                // safer location.
                Array.Copy(srcPts, srcOff, dstPts, dstOff, numPts * 2);
                // srcPts = dstPts;		// They are known to be equal.
                srcOff = dstOff;
            }
            switch (_state)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m01 = _m01; m02 = _m02;
                    m10 = _m10; m11 = _m11; m12 = _m12;
                    det = m00 * m11 - m01 * m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++] - m02;
                        double y = srcPts[srcOff++] - m12;
                        dstPts[dstOff++] = Round((x * m11 - y * m01) / det);
                        dstPts[dstOff++] = Round((y * m00 - x * m10) / det);
                    }
                    return;
                case (APPLY_SHEAR | APPLY_SCALE):
                    m00 = _m00; m01 = _m01;
                    m10 = _m10; m11 = _m11;
                    det = m00 * m11 - m01 * m10;
                    if (Math.Abs(det) <= double.MinValue)
                    {
                        throw new NoninvertibleTransformException("Determinant is " +
                                              det);
                    }
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        double y = srcPts[srcOff++];
                        dstPts[dstOff++] = Round((x * m11 - y * m01) / det);
                        dstPts[dstOff++] = Round((y * m00 - x * m10) / det);
                    }
                    return;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    m01 = _m01; m02 = _m02;
                    m10 = _m10; m12 = _m12;
                    if (m01 == 0.0 || m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++] - m02;
                        dstPts[dstOff++] = Round((srcPts[srcOff++] - m12) / m10);
                        dstPts[dstOff++] = Round(x / m01);
                    }
                    return;
                case (APPLY_SHEAR):
                    m01 = _m01; m10 = _m10;
                    if (m01 == 0.0 || m10 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        dstPts[dstOff++] = Round(srcPts[srcOff++] / m10);
                        dstPts[dstOff++] = Round(x / m01);
                    }
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    m00 = _m00; m02 = _m02;
                    m11 = _m11; m12 = _m12;
                    if (m00 == 0.0 || m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round((srcPts[srcOff++] - m02) / m00);
                        dstPts[dstOff++] = Round((srcPts[srcOff++] - m12) / m11);
                    }
                    return;
                case (APPLY_SCALE):
                    m00 = _m00; m11 = _m11;
                    if (m00 == 0.0 || m11 == 0.0)
                    {
                        throw new NoninvertibleTransformException("Determinant is 0");
                    }
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round(srcPts[srcOff++] / m00);
                        dstPts[dstOff++] = Round(srcPts[srcOff++] / m11);
                    }
                    return;
                case (APPLY_TRANSLATE):
                    m02 = _m02; m12 = _m12;
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round(srcPts[srcOff++] - m02);
                        dstPts[dstOff++] = Round(srcPts[srcOff++] - m12);
                    }
                    return;
                case (APPLY_IDENTITY):
                    if (srcPts != dstPts || srcOff != dstOff)
                    {
                        Array.Copy(srcPts, srcOff, dstPts, dstOff,
                                 numPts * 2);
                    }
                    return;
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms the relative distance vector specified by
         * <code>ptSrc</code> and stores the result in <code>ptDst</code>.
         * A relative distance vector is transformed without applying the
         * translation components of the affine transformation matrix
         * using the following equations:
         * <pre>
         *	[  x' ]   [  m00  m01 (m02) ] [  x  ]   [ m00x + m01y ]
         *	[  y' ] = [  m10  m11 (m12) ] [  y  ] = [ m10x + m11y ]
         *	[ (1) ]   [  (0)  (0) ( 1 ) ] [ (1) ]   [     (1)     ]
         * </pre>
         * If <code>ptDst</code> is <code>null</code>, a new
         * <code>Point</code> object is allocated and then the result of the
         * transform is stored in this object.
         * In either case, <code>ptDst</code>, which contains the
         * transformed point, is returned for convenience.
         * If <code>ptSrc</code> and <code>ptDst</code> are the same object,
         * the input point is correctly overwritten with the transformed
         * point.
         * @param ptSrc the distance vector to be delta transformed
         * @param ptDst the resulting transformed distance vector
         * @return <code>ptDst</code>, which contains the result of the
         * transformation.
         */
        public Point DeltaTransform(Point ptSrc, Point ptDst)
        {
            if (ptDst == null)
            {
                ptDst = new Point();
            }
            // Copy source coords into local variables in case src == dst
            double x = ptSrc.GetX();
            double y = ptSrc.GetY();
            switch (_state)
            {
                default:
                    StateError();
                    return ptDst;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SHEAR | APPLY_SCALE):
                    ptDst.SetLocation(Round(x * _m00 + y * _m01),
                                Round(x * _m10 + y * _m11));
                    return ptDst;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    ptDst.SetLocation(Round(y * _m01),
                                Round(x * _m10));
                    return ptDst;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    ptDst.SetLocation(Round(x * _m00), Round(y * _m11));
                    return ptDst;
                case (APPLY_TRANSLATE):
                case (APPLY_IDENTITY):
                    ptDst.SetLocation(Round(x), Round(y));
                    return ptDst;
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Transforms an array of relative distance vectors by this
         * transform.
         * A relative distance vector is transformed without applying the
         * translation components of the affine transformation matrix
         * using the following equations:
         * <pre>
         *	[  x' ]   [  m00  m01 (m02) ] [  x  ]   [ m00x + m01y ]
         *	[  y' ] = [  m10  m11 (m12) ] [  y  ] = [ m10x + m11y ]
         *	[ (1) ]   [  (0)  (0) ( 1 ) ] [ (1) ]   [     (1)     ]
         * </pre>
         * The two coordinate array sections can be exactly the same or
         * can be overlapping sections of the same array without affecting the
         * validity of the results.
         * This method ensures that no source coordinates are
         * overwritten by a previous operation before they can be transformed.
         * The coordinates are stored in the arrays starting at the indicated
         * offset in the order <code>[x0, y0, x1, y1, ..., xn, yn]</code>.
         * @param srcPts the array containing the source distance vectors.
         * Each vector is stored as a pair of relative x,&nbsp;y coordinates.
         * @param dstPts the array into which the transformed distance vectors
         * are returned.  Each vector is stored as a pair of relative
         * x,&nbsp;y coordinates.
         * @param srcOff the offset to the first vector to be transformed
         * in the source array
         * @param dstOff the offset to the location of the first
         * transformed vector that is stored in the destination array
         * @param numPts the number of vector coordinate pairs to be
         * transformed
         */
        public void DeltaTransform(int[] srcPts, int srcOff,
                       int[] dstPts, int dstOff,
                       int numPts)
        {
            double m00, m01, m10, m11;	// For caching
            if (dstPts == srcPts &&
                dstOff > srcOff && dstOff < srcOff + numPts * 2)
            {
                // If the arrays overlap partially with the destination higher
                // than the source and we transform the coordinates normally
                // we would overwrite some of the later source coordinates
                // with results of previous transformations.
                // To get around this we use arraycopy to copy the points
                // to their final destination with correct overwrite
                // handling and then transform them in place in the new
                // safer location.
                Array.Copy(srcPts, srcOff, dstPts, dstOff, numPts * 2);
                // srcPts = dstPts;		// They are known to be equal.
                srcOff = dstOff;
            }
            switch (_state)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SHEAR | APPLY_SCALE):
                    m00 = _m00; m01 = _m01;
                    m10 = _m10; m11 = _m11;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        double y = srcPts[srcOff++];
                        dstPts[dstOff++] = Round(x * m00 + y * m01);
                        dstPts[dstOff++] = Round(x * m10 + y * m11);
                    }
                    return;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                case (APPLY_SHEAR):
                    m01 = _m01; m10 = _m10;
                    while (--numPts >= 0)
                    {
                        double x = srcPts[srcOff++];
                        dstPts[dstOff++] = Round(srcPts[srcOff++] * m01);
                        dstPts[dstOff++] = Round(x * m10);
                    }
                    return;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                case (APPLY_SCALE):
                    m00 = _m00; m11 = _m11;
                    while (--numPts >= 0)
                    {
                        dstPts[dstOff++] = Round(srcPts[srcOff++] * m00);
                        dstPts[dstOff++] = Round(srcPts[srcOff++] * m11);
                    }
                    return;
                case (APPLY_TRANSLATE):
                case (APPLY_IDENTITY):
                    if (srcPts != dstPts || srcOff != dstOff)
                    {
                        Array.Copy(srcPts, srcOff, dstPts, dstOff,
                                 numPts * 2);
                    }
                    return;
            }

            /* NOTREACHED */
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a new  IShape object defined by the geometry of the
         * specified <code>IShape</code> after it has been transformed by
         * this transform.
         * @param pSrc the specified <code>IShape</code> object to be
         * transformed by this transform.
         * @return a new <code>IShape</code> object that defines the geometry
         * of the transformed <code>IShape</code>, or null if {@code pSrc} is null.
         */
        public IShape CreateTransformedShape(IShape pSrc)
        {
            if (pSrc == null)
            {
                return null;
            }
            return new Path(pSrc, this);
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns a <code>string</code> that represents the Value of this
         *  Object.
         * @return a <code>string</code> representing the Value of this
         * <code>Object</code>.
         */
        public override string ToString()
        {
            return ("AffineTransform[["
                + Matround(_m00) + ", "
                + Matround(_m01) + ", "
                + Matround(_m02) + "], ["
                + Matround(_m10) + ", "
                + Matround(_m11) + ", "
                + Matround(_m12) + "]]");
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns <code>true</code> if this <code>AffineTransform</code> is
         * an identity transform.
         * @return <code>true</code> if this <code>AffineTransform</code> is
         * an identity transform; <code>false</code> otherwise.
         */
        public bool IsIdentity()
        {
            return (_state == APPLY_IDENTITY || (GetTransformType() == TYPE_IDENTITY));
        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Returns <code>true</code> if this <code>AffineTransform</code>
         * represents the same affine coordinate transform as the specified
         * argument.
         * @param obj the <code>Object</code> to test for equality with this
         * <code>AffineTransform</code>
         * @return <code>true</code> if <code>obj</code> equals this
         * <code>AffineTransform</code> object; <code>false</code> otherwise.
         */
        public new bool Equals(Object obj)
        {
            if (!(obj is AffineTransform))
            {
                return false;
            }

            var a = (AffineTransform)obj;

            return ((_m00 == a._m00) && (_m01 == a._m01) && (_m02 == a._m02) &&
                (_m10 == a._m10) && (_m11 == a._m11) && (_m12 == a._m12));
        }

        /*
         * This constant is only useful for the cached type field.
         * It indicates that the type has been decached and must be recalculated.
         */
        private const int TYPE_UNKNOWN = -1;

        /**
         * This constant is used for the internal state variable to indicate
         * that the translation components of the matrix (m02 and m12) need
         * to be added to complete the transformation equation of this transform.
         */
        private const int APPLY_TRANSLATE = 1;

        /**
         * This constant is used for the internal state variable to indicate
         * that the scaling components of the matrix (m00 and m11) need
         * to be factored in to complete the transformation equation of
         * this transform.  If the APPLY_SHEAR bit is also set then it
         * indicates that the scaling components are not both 0.0.  If the
         * APPLY_SHEAR bit is not also set then it indicates that the
         * scaling components are not both 1.0.  If neither the APPLY_SHEAR
         * nor the APPLY_SCALE bits are set then the scaling components
         * are both 1.0, which means that the x and y components contribute
         * to the transformed coordinate, but they are not multiplied by
         * any scaling factor.
         */
        private const int APPLY_SCALE = 2;

        /**
         * This constant is used for the internal state variable to indicate
         * that the shearing components of the matrix (m01 and m10) need
         * to be factored in to complete the transformation equation of this
         * transform.  The presence of this bit in the state variable changes
         * the interpretation of the APPLY_SCALE bit as indicated in its
         * documentation.
         */
        private const int APPLY_SHEAR = 4;

        /*
         * For methods which combine together the state of two separate
         * transforms and dispatch based upon the combination, these constants
         * specify how far to shift one of the states so that the two states
         * are mutually non-interfering and provide constants for testing the
         * bits of the shifted (HI) state.  The methods in this class use
         * the convention that the state of "this" transform is unshifted and
         * the state of the "other" or "argument" transform is shifted (HI).
         */
        private const int HI_SHIFT = 3;
        private const int HI_IDENTITY = APPLY_IDENTITY << HI_SHIFT;
        private const int HI_TRANSLATE = APPLY_TRANSLATE << HI_SHIFT;
        private const int HI_SCALE = APPLY_SCALE << HI_SHIFT;
        private const int HI_SHEAR = APPLY_SHEAR << HI_SHIFT;

        /**
         * The X coordinate scaling element of the 3x3
         * affine transformation matrix.
         *
         * @serial
         */
        private double _m00;

        /**
         * The Y coordinate shearing element of the 3x3
         * affine transformation matrix.
         */
        private double _m10;

        /**
         * The X coordinate shearing element of the 3x3
         * affine transformation matrix.
         */
        private double _m01;

        /**
         * The Y coordinate scaling element of the 3x3
         * affine transformation matrix.
         */
        private double _m11;

        /**
         * The X coordinate of the translation element of the
         * 3x3 affine transformation matrix.
         */
        private double _m02;

        /**
         * The Y coordinate of the translation element of the
         * 3x3 affine transformation matrix.
         */
        private double _m12;

        /**
         * This field keeps track of which components of the matrix need to
         * be applied when performing a transformation.
         */
        private int _state;

        /**
         * This field caches the current transformation type of the matrix.
         */
        private int _type;

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        // Round values to sane precision for printing
        // that Math.Sin(Math.PI) has an error of about 10^-16
        private static double Matround(double matval)
        {
            return Rint(matval * 1E15) / 1E15;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        // Round values .
        private static int Round(double a)
        {
            return (int)Math.Floor(a + 0.5);
        }




        private static double Rint(double a)
        {
            return a;
        }

        //[------------------------------ CONSTRUCTOR -----------------------------]
        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private AffineTransform(double m00, double m10,
                    double m01, double m11,
                    double m02, double m12,
                    int state)
        {
            _m00 = m00;
            _m10 = m10;
            _m01 = m01;
            _m11 = m11;
            _m02 = m02;
            _m12 = m12;
            _state = state;
            _type = TYPE_UNKNOWN;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        // Utility methods to optimize rotate methods.
        // These tables translate the flags during predictable quadrant
        // rotations where the shear and scale values are swapped and negated.
        private static readonly int[] Rot90Conversion = new[]{
                                                                     /* IDENTITY => */        APPLY_SHEAR,
                                                                                              /* TRANSLATE (TR) => */  APPLY_SHEAR | APPLY_TRANSLATE,
                                                                                              /* SCALE (SC) => */      APPLY_SHEAR,
                                                                                              /* SC | TR => */         APPLY_SHEAR | APPLY_TRANSLATE,
                                                                                              /* SHEAR (SH) => */      APPLY_SCALE,
                                                                                              /* SH | TR => */         APPLY_SCALE | APPLY_TRANSLATE,
                                                                                              /* SH | SC => */         APPLY_SHEAR | APPLY_SCALE,
                                                                                              /* SH | SC | TR => */    APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE,
                                                                 };

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * This is the utility function to calculate the flag bits when
         * they have not been cached.
         */
        private void CalculateType()
        {
            int ret = TYPE_IDENTITY;
            bool sgn0, sgn1;
            double m0, m1, m2, m3;
            UpdateState();
            switch (_state)
            {
                default:
                    StateError();
                    break;
                case (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE):
                    ret = TYPE_TRANSLATION;
                    if ((m0 = _m00) * (m2 = _m01) + (m3 = _m10) * (m1 = _m11) != 0)
                    {
                        // Transformed unit vectors are not perpendicular...
                        _type = TYPE_GENERAL_TRANSFORM;
                        return;
                    }
                    sgn0 = (m0 >= 0.0);
                    sgn1 = (m1 >= 0.0);
                    if (sgn0 == sgn1)
                    {
                        // sgn(M0) == sgn(M1) therefore sgn(M2) == -sgn(M3)
                        // This is the "unflipped" (right-handed) state
                        if (m0 != m1 || m2 != -m3)
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_GENERAL_SCALE);
                        }
                        else if (m0 * m1 - m2 * m3 != 1.0)
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= TYPE_GENERAL_ROTATION;
                        }
                    }
                    else
                    {
                        // sgn(M0) == -sgn(M1) therefore sgn(M2) == sgn(M3)
                        // This is the "flipped" (left-handed) state
                        if (m0 != -m1 || m2 != m3)
                        {
                            ret |= (TYPE_GENERAL_ROTATION |
                                TYPE_FLIP |
                                TYPE_GENERAL_SCALE);
                        }
                        else if (m0 * m1 - m2 * m3 != 1.0)
                        {
                            ret |= (TYPE_GENERAL_ROTATION |
                                TYPE_FLIP |
                                TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_FLIP);
                        }
                    }
                    break;
                case (APPLY_SHEAR | APPLY_SCALE):
                    if ((m0 = _m00) * (m2 = _m01) + (m3 = _m10) * (m1 = _m11) != 0)
                    {
                        // Transformed unit vectors are not perpendicular...
                        _type = TYPE_GENERAL_TRANSFORM;
                        return;
                    }
                    sgn0 = (m0 >= 0.0);
                    sgn1 = (m1 >= 0.0);
                    if (sgn0 == sgn1)
                    {
                        // sgn(M0) == sgn(M1) therefore sgn(M2) == -sgn(M3)
                        // This is the "unflipped" (right-handed) state
                        if (m0 != m1 || m2 != -m3)
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_GENERAL_SCALE);
                        }
                        else if (m0 * m1 - m2 * m3 != 1.0)
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= TYPE_GENERAL_ROTATION;
                        }
                    }
                    else
                    {
                        // sgn(M0) == -sgn(M1) therefore sgn(M2) == sgn(M3)
                        // This is the "flipped" (left-handed) state
                        if (m0 != -m1 || m2 != m3)
                        {
                            ret |= (TYPE_GENERAL_ROTATION |
                                TYPE_FLIP |
                                TYPE_GENERAL_SCALE);
                        }
                        else if (m0 * m1 - m2 * m3 != 1.0)
                        {
                            ret |= (TYPE_GENERAL_ROTATION |
                                TYPE_FLIP |
                                TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= (TYPE_GENERAL_ROTATION | TYPE_FLIP);
                        }
                    }
                    break;
                case (APPLY_SHEAR | APPLY_TRANSLATE):
                    ret = TYPE_TRANSLATION;
                    sgn0 = ((m0 = _m01) >= 0.0);
                    sgn1 = ((m1 = _m10) >= 0.0);
                    if (sgn0 != sgn1)
                    {
                        // Different signs - simple 90 degree rotation
                        if (m0 != -m1)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION | TYPE_GENERAL_SCALE);
                        }
                        else if (m0 != 1.0 && m0 != -1.0)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION | TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= TYPE_QUADRANT_ROTATION;
                        }
                    }
                    else
                    {
                        // Same signs - 90 degree rotation plus an axis flip too
                        if (m0 == m1)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION |
                                TYPE_FLIP |
                                TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= (TYPE_QUADRANT_ROTATION |
                                TYPE_FLIP |
                                TYPE_GENERAL_SCALE);
                        }
                    }
                    break;
                case (APPLY_SHEAR):
                    sgn0 = ((m0 = _m01) >= 0.0);
                    sgn1 = ((m1 = _m10) >= 0.0);
                    if (sgn0 != sgn1)
                    {
                        // Different signs - simple 90 degree rotation
                        if (m0 != -m1)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION | TYPE_GENERAL_SCALE);
                        }
                        else if (m0 != 1.0 && m0 != -1.0)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION | TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= TYPE_QUADRANT_ROTATION;
                        }
                    }
                    else
                    {
                        // Same signs - 90 degree rotation plus an axis flip too
                        if (m0 == m1)
                        {
                            ret |= (TYPE_QUADRANT_ROTATION |
                                TYPE_FLIP |
                                TYPE_UNIFORM_SCALE);
                        }
                        else
                        {
                            ret |= (TYPE_QUADRANT_ROTATION |
                                TYPE_FLIP |
                                TYPE_GENERAL_SCALE);
                        }
                    }
                    break;
                case (APPLY_SCALE | APPLY_TRANSLATE):
                    ret = TYPE_TRANSLATION;
                    sgn0 = ((m0 = _m00) >= 0.0);
                    sgn1 = ((m1 = _m11) >= 0.0);
                    if (sgn0 == sgn1)
                    {
                        if (sgn0)
                        {
                            // Both scaling factors non-negative - simple scale
                            // Note: APPLY_SCALE implies M0, M1 are not both 1
                            if (m0 == m1)
                            {
                                ret |= TYPE_UNIFORM_SCALE;
                            }
                            else
                            {
                                ret |= TYPE_GENERAL_SCALE;
                            }
                        }
                        else
                        {
                            // Both scaling factors negative - 180 degree rotation
                            if (m0 != m1)
                            {
                                ret |= (TYPE_QUADRANT_ROTATION | TYPE_GENERAL_SCALE);
                            }
                            else if (m0 != -1.0)
                            {
                                ret |= (TYPE_QUADRANT_ROTATION | TYPE_UNIFORM_SCALE);
                            }
                            else
                            {
                                ret |= TYPE_QUADRANT_ROTATION;
                            }
                        }
                    }
                    else
                    {
                        // Scaling factor signs different - flip about some axis
                        if (m0 == -m1)
                        {
                            if (m0 == 1.0 || m0 == -1.0)
                            {
                                ret |= TYPE_FLIP;
                            }
                            else
                            {
                                ret |= (TYPE_FLIP | TYPE_UNIFORM_SCALE);
                            }
                        }
                        else
                        {
                            ret |= (TYPE_FLIP | TYPE_GENERAL_SCALE);
                        }
                    }
                    break;
                case (APPLY_SCALE):
                    sgn0 = ((m0 = _m00) >= 0.0);
                    sgn1 = ((m1 = _m11) >= 0.0);
                    if (sgn0 == sgn1)
                    {
                        if (sgn0)
                        {
                            // Both scaling factors non-negative - simple scale
                            // Note: APPLY_SCALE implies M0, M1 are not both 1
                            if (m0 == m1)
                            {
                                ret |= TYPE_UNIFORM_SCALE;
                            }
                            else
                            {
                                ret |= TYPE_GENERAL_SCALE;
                            }
                        }
                        else
                        {
                            // Both scaling factors negative - 180 degree rotation
                            if (m0 != m1)
                            {
                                ret |= (TYPE_QUADRANT_ROTATION | TYPE_GENERAL_SCALE);
                            }
                            else if (m0 != -1.0)
                            {
                                ret |= (TYPE_QUADRANT_ROTATION | TYPE_UNIFORM_SCALE);
                            }
                            else
                            {
                                ret |= TYPE_QUADRANT_ROTATION;
                            }
                        }
                    }
                    else
                    {
                        // Scaling factor signs different - flip about some axis
                        if (m0 == -m1)
                        {
                            if (m0 == 1.0 || m0 == -1.0)
                            {
                                ret |= TYPE_FLIP;
                            }
                            else
                            {
                                ret |= (TYPE_FLIP | TYPE_UNIFORM_SCALE);
                            }
                        }
                        else
                        {
                            ret |= (TYPE_FLIP | TYPE_GENERAL_SCALE);
                        }
                    }
                    break;
                case (APPLY_TRANSLATE):
                    ret = TYPE_TRANSLATION;
                    break;
                case (APPLY_IDENTITY):
                    break;
            }
            _type = ret;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Manually recalculates the state of the transform when the matrix
         * changes too much to predict the effects on the state.
         * The following table specifies what the various settings of the
         * state field say about the values of the corresponding matrix
         * element fields.
         * Note that the rules governing the SCALE fields are slightly
         * different depending on whether the SHEAR flag is also set.
         * <pre>
         *                     SCALE            SHEAR          TRANSLATE
         *                    m00/m11          m01/m10          m02/m12
         *
         * IDENTITY             1.0              0.0              0.0
         * TRANSLATE (TR)       1.0              0.0          not both 0.0
         * SCALE (SC)       not both 1.0         0.0              0.0
         * TR | SC          not both 1.0         0.0          not both 0.0
         * SHEAR (SH)           0.0          not both 0.0         0.0
         * TR | SH              0.0          not both 0.0     not both 0.0
         * SC | SH          not both 0.0     not both 0.0         0.0
         * TR | SC | SH     not both 0.0     not both 0.0     not both 0.0
         * </pre>
         */
        private void UpdateState()
        {
            if (_m01 == 0.0 && _m10 == 0.0)
            {
                if (_m00 == 1.0 && _m11 == 1.0)
                {
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_IDENTITY;
                        _type = TYPE_IDENTITY;
                    }
                    else
                    {
                        _state = APPLY_TRANSLATE;
                        _type = TYPE_TRANSLATION;
                    }
                }
                else
                {
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SCALE;
                        _type = TYPE_UNKNOWN;
                    }
                    else
                    {
                        _state = (APPLY_SCALE | APPLY_TRANSLATE);
                        _type = TYPE_UNKNOWN;
                    }
                }
            }
            else
            {
                if (_m00 == 0.0 && _m11 == 0.0)
                {
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = APPLY_SHEAR;
                        _type = TYPE_UNKNOWN;
                    }
                    else
                    {
                        _state = (APPLY_SHEAR | APPLY_TRANSLATE);
                        _type = TYPE_UNKNOWN;
                    }
                }
                else
                {
                    if (_m02 == 0.0 && _m12 == 0.0)
                    {
                        _state = (APPLY_SHEAR | APPLY_SCALE);
                        _type = TYPE_UNKNOWN;
                    }
                    else
                    {
                        _state = (APPLY_SHEAR | APPLY_SCALE | APPLY_TRANSLATE);
                        _type = TYPE_UNKNOWN;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /*
         * Convenience method used internally to throw exceptions when
         * a case was forgotten in a switch statement.
         */
        private static void StateError()
        {
            throw new SystemException("missing case in transform state switch");
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private void Rotate90()
        {
            double m0 = _m00;
            _m00 = _m01;
            _m01 = -m0;
            m0 = _m10;
            _m10 = _m11;
            _m11 = -m0;
            int currentState = Rot90Conversion[_state];
            if ((currentState & (APPLY_SHEAR | APPLY_SCALE)) == APPLY_SCALE &&
                _m00 == 1.0 && _m11 == 1.0)
            {
                currentState -= APPLY_SCALE;
            }
            _state = currentState;
            _type = TYPE_UNKNOWN;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private void Rotate180()
        {
            _m00 = -_m00;
            _m11 = -_m11;
            int currentState = _state;
            if ((currentState & (APPLY_SHEAR)) != 0)
            {
                // If there was a shear, then this rotation has no
                // effect on the state.
                _m01 = -_m01;
                _m10 = -_m10;
            }
            else
            {
                // No shear means the SCALE state may toggle when
                // m00 and m11 are negated.
                if (_m00 == 1.0 && _m11 == 1.0)
                {
                    _state = currentState & ~APPLY_SCALE;
                }
                else
                {
                    _state = currentState | APPLY_SCALE;
                }
            }
            _type = TYPE_UNKNOWN;
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        private void Rotate270()
        {
            double m0 = _m00;
            _m00 = -_m01;
            _m01 = m0;
            m0 = _m10;
            _m10 = -_m11;
            _m11 = m0;
            int currentState = Rot90Conversion[_state];
            if ((currentState & (APPLY_SHEAR | APPLY_SCALE)) == APPLY_SCALE &&
                _m00 == 1.0 && _m11 == 1.0)
            {
                currentState -= APPLY_SCALE;
            }
            _state = currentState;
            _type = TYPE_UNKNOWN;
        }




    }

}
