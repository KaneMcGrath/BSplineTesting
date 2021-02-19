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
        public static FRect RectToFRect(Rectangle r)
        {
            float width = ((float)r.Width / (float)Program.mainWindow.Width) * 2f;
            float height = (((float)r.Height / (float)Program.mainWindow.Height) * 2f);
            float left = (((float)r.X / (float)Screen.Width) * 2f) - 1f;
            float top = ((1f - ((float)r.Y / (float)Screen.Height) * 2f) - height);
            FRect rect = new FRect(left, top , width, height, false);
            return rect;
        }


        public static Rectangle FRectToRect(FRect r)
        {
            int width = ((int)(r.width / 2f) * Screen.Width);
            int height = ((int)(r.height / 2f) * Screen.Height);
            int x = ((int)(r.left / 2f) * Screen.Width);
            int y = ((int)(((1f - r.top)  ) / 2f) * Screen.Height);
            return new Rectangle();
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
        /// Takes a Screen Coordinate FRect and adjusts its x position and width to remain consistant
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static FRect ApplyAspect(FRect rect)
        {
            return new FRect(rect.left * aspectX, rect.top, rect.width * aspectX, rect.height);
        }


        public static Vector2 RemoveAspect(Vector2 v)
        {
            return new Vector2(v.X / aspectX, v.Y);
        }

        public static FRect RemoveAspect(FRect rect)
        {
            return new FRect(rect.left / aspectX, rect.top, rect.width / aspectX, rect.height);
        }
    }
}
