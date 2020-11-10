using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    static class Renderer
    {
        public static void LineCircle(Vector2 pos, float rad, int res) //draws a circle out of lines centered on pos, with rad radius and with res number of points
        {

            Vector2[] points = new Vector2[res + 1];
            Vector2 start = new Vector2(pos.X + rad, pos.Y + rad * 0f);
            points[0] = start;
            points[res] = start;
            for (int i = 1; i <= res; i++)
            {
                double t = ((double)(i) / res) * 2f * Math.PI;
                double x = pos.X + rad * Math.Cos(t);
                double y = pos.Y + rad * Math.Sin(t);
                points[i] = new Vector2((float)x, (float)y);
            }
            drawLine(points);
        }



        public static void drawLine(Vector2[] points) //Draws a connected line segment between points
        {
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Vector2 point in points)
            {
                GL.Vertex2(point);
            }
            GL.End();
        }
    }
}
