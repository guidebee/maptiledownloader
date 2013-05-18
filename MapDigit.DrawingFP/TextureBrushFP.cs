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

//--------------------------------- PACKAGE ------------------------------------
using System;

namespace MapDigit.DrawingFP
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 13JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * Defines a brush of a single color. Brushes are used to fill graphics shapes,
     * such as rectangles, ellipses, pies, polygons, and paths. This class cannot
     * be inherited.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 13/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class TextureBrushFP : BrushFP
    {

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * Always return true.
         */
        public override bool IsMonoColor()
        {
            return false;
        }


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Default constructor.default color is white.
         */
        public TextureBrushFP(int[] image, int width, int height)
        {
            _textureBuffer = new int[image.Length];
            Array.Copy(image, 0, _textureBuffer, 0, _textureBuffer.Length);
            this._width = width;
            this._height = height;

        }



        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @param x the x coordinate
         * @param y the y coordinate
         * @param singlePoint  single point
         * @return the color Value at given location.
         */
        public override int GetColorAt(int x, int y, bool singlePoint)
        {
            var p = new PointFP(x << SingleFP.DECIMAL_BITS,
                    y << SingleFP.DECIMAL_BITS);
            _nextPt.X = p.X + SingleFP.ONE;
            _nextPt.Y = p.Y;
            if (_finalMatrix != null)
            {
                p.Transform(_finalMatrix);

            }
            var xPos = (p.X >> SingleFP.DECIMAL_BITS) % _width;
            var yPos = (p.Y >> SingleFP.DECIMAL_BITS) % _height;

            if (xPos < 0) xPos += _width;
            if (yPos < 0) yPos += _height;


            return _textureBuffer[(xPos + yPos * _width)];
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 13JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * @inheritDoc
         * @return next color.
         */
        public override int GetNextColor()
        {
            var p = new PointFP(_nextPt);

            _nextPt.X += SingleFP.ONE;
            _nextPt.Y = p.Y;

            if (_finalMatrix != null)
            {
                p.Transform(_finalMatrix);

            }
            var xPos = (p.X >> SingleFP.DECIMAL_BITS) % _width;
            var yPos = (p.Y >> SingleFP.DECIMAL_BITS) % _height;

            if (xPos < 0) xPos += _width;
            if (yPos < 0) yPos += _height;

            return _textureBuffer[xPos + yPos * _width];
        }


        /**
         * the width of the texture
         */
        private readonly int _width = 1;

        /**
         * the height of the texture brush
         */
        private readonly int _height = 1;

        /**
         * the texture buffer
         */
        private readonly int[] _textureBuffer;

        /**
         * next point position.
         */
        private readonly PointFP _nextPt = new PointFP(0, 0);
    }

}
