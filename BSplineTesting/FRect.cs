using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    /// <summary>
    /// Rectangle struct used for drawing rectangles relative to the center of the screen.  
    /// </summary>
    public struct FRect
    {
        /// <summary>
        /// X position of the left edge of the rectangle
        /// </summary>
        public float left;

        /// <summary>
        /// Y position of the top edge of the rectangle
        /// </summary>
        public float top;

        /// <summary>
        /// Width of the rectangle
        /// </summary>
        public float width;

        /// <summary>
        /// Height of the rectangle
        /// </summary>
        public float height;

        /// <summary>
        /// Whether the rectangle should have its position adjusted to the center square of the screen.  
        /// -1 on the X axis will no longer be the edge of the screen
        /// </summary>
        public bool shouldAdjustToAspect;


        /// <summary>
        /// Rectangle struct used for drawing rectangles relative to the center of the screen.  usually in the range of -1 to 1.
        /// </summary>
        /// <param name="left">X position of the left edge of the rectangle</param>
        /// <param name="top">Y position of the top edge of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="adjustToAspectRatio">Whether the rectangle should have its position adjusted to the center square of the screen.  
        /// -1 on the X axis will no longer be the edge of the screen</param>
        /// <example>new FRect(-0.4f,0.3f,0.5f,0.2f)</example>
        public FRect(float left, float top, float width, float height, bool adjustToAspectRatio = true)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            shouldAdjustToAspect = adjustToAspectRatio;
        }


        /// <summary>
        /// Rectangle struct used for drawing rectangles relative to the center of the screen.
        /// This configuration creates a square around the center point with 2 * radius being the length of each side
        /// </summary>
        /// <param name="left">X position of the center of the rectangle</param>
        /// <param name="top">Y position of the center of the rectangle</param>
        /// <param name="radius">Radius from the center to the edge.  Also defines the width and height as 2 * radius</param>
        public FRect(float left, float top, float radius)
        {
            this.left = left - radius;
            this.top = top - radius;
            this.width = radius;
            this.height = radius;
            shouldAdjustToAspect = true;
        }

        
        /// <summary>
        /// Rectangle struct used for drawing rectangles relative to the center of the screen.
        /// This configuration creates a square around the center point with 2 * radius being the length of each side
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="radius"></param>
        public FRect(Vector2 pos, float radius)
        {
            float num = radius / 2f;
            this.left = pos.X - num;
            this.top = pos.Y - num;
            this.width = 2 * radius;
            this.height = 2 * radius;
            shouldAdjustToAspect = true;
        }

        /// <summary>
        /// Gets the center point of the rectangle
        /// </summary>
        /// <returns>Vector2 point representing the center of the rectangle</returns>
        public Vector2 Center()
        {
            return new Vector2(left + width / 2f, top + height / 2);
        }

        /// <summary>
        /// Gets all of the corners of the rectangle as Vector2 points in clockwise order starting from the top left
        /// </summary>
        /// <returns>Vector2[] of length 4 with the 4 corners of the rectangle in clockwise order starting from the top left</returns>
        public Vector2[] GetCorners()
        {
            return new Vector2[]
            {
                new Vector2(left,top),
                new Vector2(left + width,top),
                new Vector2(left+width,top+height),
                new Vector2(left,top+height)
            };
        }


        /// <summary>
        /// Gets a single corner from the rectangle,
        ///     [0 = top left]
        ///     [1 = top right]
        ///     [2 = bottom right]
        ///     [3 = bottom left]
        /// </summary>
        /// <param name="index">index of which corner of the rectangle to get
        /// Starting with 0 at the top left corner and going clockwise to 3
        /// </param>
        /// <returns>Vector2 point of the specified corner</returns>
        public Vector2 GetCorner(int index)
        {
            return GetCorners()[index];
        }


        public override string ToString()
        {
            return "(" + left.ToString() + "," + top.ToString() + "," + width.ToString() + "," + height.ToString() + ")";
        }
    }
}
