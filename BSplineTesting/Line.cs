using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace BSplineTesting
{
    public class Line
    {
        public List<Vector2> points = new List<Vector2>();
        public Color color;
        public bool visible;

        public Line(Vector2[] points) 
        {
            this.points.AddRange(points);
        }

        public Vector2[] getPoints()
        {
            return points.ToArray();
        }
    }

    public class CurveDrawer
    {
        public static Random Random = new Random(69);

        public static Vector2[] SimpleBezierCurve(Vector2 a, Vector2 b, Vector2 c, int resolution)
        {

            float t;
            Vector2[] result = new Vector2[resolution + 1];
            for (int i = 0; i < resolution; i++)
            {
               t = ((float)(i)/resolution);
                result[i] = CurveDrawer.LS(CurveDrawer.LS(a, b, t), CurveDrawer.LS(b, c, t), t);

            }
            result[resolution] = c;
            return result;
        }
        


        public static Vector2[] SimpleBezierCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, int resolution)
        {

            float t;
            Vector2[] result = new Vector2[resolution + 1];
            for (int i = 0; i < resolution; i++)
            {
                t = ((float)(i) / resolution);

                float x = (((a.X + ((b.X - a.X) * t)) + (((b.X + ((c.X - b.X) * t)) - (a.X + ((b.X - a.X) * t))) * t)) + ((((b.X + ((c.X - b.X) * t)) + (((c.X + ((d.X - c.X) * t)) - (b.X + ((c.X - b.X) * t))) * t)) - ((a.X + ((b.X - a.X) * t)) + (((b.X + ((c.X - b.X) * t)) - (a.X + ((b.X - a.X) * t))) * t))) * t));
                float y = (((a.Y + ((b.Y - a.Y) * t)) + (((b.Y + ((c.Y - b.Y) * t)) - (a.Y + ((b.Y - a.Y) * t))) * t)) + ((((b.Y + ((c.Y - b.Y) * t)) + (((c.Y + ((d.Y - c.Y) * t)) - (b.Y + ((c.Y - b.Y) * t))) * t)) - ((a.Y + ((b.Y - a.Y) * t)) + (((b.Y + ((c.Y - b.Y) * t)) - (a.Y + ((b.Y - a.Y) * t))) * t))) * t));
                result[i] = new Vector2(x, y);
            }
            result[resolution] = d;
            return result;
        }

        public static Vector2 LS(Vector2 A, Vector2 B, float t)
        {
            float x = A.X + ((B.X - A.X) * t);
            float y = A.Y + ((B.Y - A.Y) * t);
            return new Vector2(x, y);
        }
    }
}
