using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSplineTesting
{
    class Coordinates
    {
        public static float aspectX;

        /// <summary>
        /// Converts a rectangle with integer values coresponding to the Window width and Height in pixels
        /// to an aspect ratio adjusted Screen Cooridnate that can be used with OpenGL
        /// </summary>
        /// <param name="r"> Rectangle in pixel coordinates </param>
        /// <returns> Screen Coordinate FRect adjusted to the aspect ratio </returns>
        public static FRect PixelCoordsToScreenCoords(Rectangle r)
        {

            return new FRect();

        }


        /// <summary>
        /// Takes a Vector2 screen coordinate and adjusts the position to avoid stretching, 
        /// the coordinate will be a square in the center stretched to the Y height of the screen
        /// and the x value will be able to continue past 1
        /// </summary>
        /// <param name="v">  </param>
        /// <returns>  </returns>
        public static Vector2 ApplyAspect(Vector2 v)
        {
            return new Vector2(v.X * aspectX, v.Y);
        }



        /// <summary>
        /// Takes a Screen Coordinate FRect and adjusts its x position and width
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static FRect ApplyAspect(FRect rect)
        {

            return new FRect(rect.left * aspectX, rect.top, rect.width * aspectX, rect.height);

        }

    }
}
