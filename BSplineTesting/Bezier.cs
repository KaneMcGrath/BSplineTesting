using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace BSplineTesting
{
    class Bezier
    {
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


    }

    public class spline
    {
        public spline(Vector2 pos, Vector2 left, Vector2 right)
        {
            position = pos;
            this.left = left;
            this.right = right;
        }
        public spline(Vector2 pos)
        {
            position = pos;
            this.left = new Vector2(pos.X - 0.1f, pos.Y);
            this.right = new Vector2(pos.X + 0.1f, pos.Y);
        }
        Vector2 position;
        Vector2 left;
        Vector2 right;
    }
}
