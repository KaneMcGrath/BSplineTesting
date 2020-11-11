using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace BSplineTesting
{
    class Bezier //Utilities for drawing Bezier curves
    {
        public static Vector2[] SimpleBezierCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, int resolution) //Returns array of points coresponding to a bezier curve made with the 4 given points
        {
            float t;
            Vector2[] result = new Vector2[resolution + 1];
            for (int i = 0; i < resolution; i++)
            {
                //gets i out of resolution in a range up to 1
                t = ((float)(i) / resolution);

                //exponents for lazy people
                float t2 = t * t;
                float t3 = t2 * t;

                //math
                float x = -a.X * t3 + 3 * b.X * t3 - 3 * c.X * t3 + d.X * t3 + 3 * a.X * t2 - 6 * b.X * t2 + 3 * c.X * t2 - 3 * a.X * t + 3 * b.X * t + a.X;
                float y = -a.Y * t3 + 3 * b.Y * t3 - 3 * c.Y * t3 + d.Y * t3 + 3 * a.Y * t2 - 6 * b.Y * t2 + 3 * c.Y * t2 - 3 * a.Y * t + 3 * b.Y * t + a.Y;
                
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

    public class Spline //Spline used to make continuous curves with a single point and two locked controlpoints   o---o---o
    {
        public Spline(Vector2 pos, Vector2 left, Vector2 right) //left and right are relative to the center point
        {
            position = pos;
            this.leftPos = left;
            this.rightPos = right;
        }
        public Spline(Vector2 pos)
        {
            position = pos;
            this.leftPos = new Vector2(-0.1f, 0f);
            this.rightPos = new Vector2(0.1f, 0f);
        }
        public Vector2 position;
        public Vector2 leftPos;
        public Vector2 rightPos;

        public void moveControlPoint(Vector2 pos, bool left) //moves opposing controlpoint as well
        {

        }
        public Vector2 GlobalLeft()
        {
            return new Vector2(position.X + leftPos.X, position.Y + leftPos.Y);
        }

        public Vector2 GlobalRight()
        {
            return new Vector2(position.X + rightPos.X, position.Y + rightPos.Y);
        }
        public Vector2 GlobalControlPoint(bool left)
        {
            if (left)
            {
                return new Vector2(position.X + leftPos.X, position.Y + leftPos.Y);
            }
            return new Vector2(position.X + rightPos.X, position.Y + rightPos.Y);
        }

        public Vector2[] getPoints()
        {
            return new Vector2[] { GlobalLeft(), position, GlobalRight() };
        }
    }
}
