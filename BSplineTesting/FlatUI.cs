using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK.Input;
using OpenTK;

namespace BSplineTesting
{
    class FlatUI
    {
        public static Color primaryColor;
        public static Color outlineColor;
        public static Color darkColor;
        public static Color lightColor;
        public static int outLineThickness = 4;

        public static void Initialize()
        {
            primaryColor = Color.FromArgb(90, 90, 90);
            outlineColor = Color.FromArgb(10, 10, 10);
            darkColor = Color.FromArgb(50, 50, 50);
            lightColor = Color.FromArgb(130, 130, 130);
        }
    

        public static class GUI
        {
            public static void Box(Rectangle rectangle, Color color)
            {
                Renderer.drawRect(Coordinates.RectToFRect(rectangle), color);
            }

            public static void OutlineBox(Rectangle rectangle, Color insideColor, Color outsideColor)
            {
                Box(rectangle, outsideColor);
                Box(new Rectangle(rectangle.X + outLineThickness, rectangle.Y + outLineThickness, rectangle.Width - (outLineThickness * 2), rectangle.Height - (outLineThickness * 2)), insideColor);
            }

            public static void OutlineBox(Rectangle rectangle, Color insideColor, Color outsideColor, bool swap)
            {
                if (swap)
                {
                    Box(rectangle, insideColor);
                    Box(new Rectangle(rectangle.X + outLineThickness, rectangle.Y + outLineThickness, rectangle.Width - (outLineThickness * 2), rectangle.Height - (outLineThickness * 2)), outsideColor);
                }
                else
                {
                    Box(rectangle, outsideColor);
                    Box(new Rectangle(rectangle.X + outLineThickness, rectangle.Y + outLineThickness, rectangle.Width - (outLineThickness * 2), rectangle.Height - (outLineThickness * 2)), insideColor);
                }
            }
        }


        public static bool button(Rectangle rectangle, string text)
        {
            
            GUI.OutlineBox(rectangle, primaryColor, outlineColor, IM.isMouseInRect(rectangle) && IM.GetMouseButton(MouseButton.Left));
            Vector2 v = Coordinates.RemoveAspect(Coordinates.RectToFRect(rectangle).GetCorner(0));
            Text.DrawString(v, 0.4f, v.ToString());
            
            if (IM.isMouseInRect(rectangle) && IM.MouseButtonDown(MouseButton.Left))
            {

                return true;
            }
            return false;
        }
    }
}
