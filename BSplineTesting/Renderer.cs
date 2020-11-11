using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

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
        public static void drawLine(Vector2[] points,Color c) //Draws a connected line segment between points
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(c);
            foreach (Vector2 point in points)
            {
                GL.Vertex2(point);
            }
            GL.Color3(Color.White);
            GL.End();
        }

        public static void drawSpline(Spline s)
        {

            drawLine(new Vector2[] { s.GlobalLeft(), s.position, s.GlobalRight() });
            LineCircle(s.position, 0.01f, 10);
            LineCircle(s.GlobalLeft(), 0.01f, 10);
            LineCircle(s.GlobalRight(), 0.01f, 10);

        }

        public static void drawLineCurve(Line l, int resolution)
        {
            Vector2[] v = l.getPoints();
            for(int i = 0; i < v.Length - 1; i += 3)
            {
                drawLine(Bezier.SimpleBezierCurve(v[i], v[i + 1], v[i + 2], v[i + 3], resolution));
            }
        }
    }
}
