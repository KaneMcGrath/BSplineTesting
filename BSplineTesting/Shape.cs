using System;
using OpenTK;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    class Shape
    {
        public Line outline;

        public Vector2[] getPoints()
        {
            // really lazy fix later
            Vector2[] points = outline.getPoints();
            Vector2[] result = new Vector2[points.Length + 3];
            
            for (int i = 0; i < points.Length; i++)
            {
                result[i] = points[i];
            }
            Spline[] s = outline.getSplines();
            result[points.Length] = s[s.Length - 1].GlobalRight();
            result[points.Length + 1] = s[0].GlobalLeft();
            result[points.Length + 2] = s[0].position;

            return result;
            
        }



    }
}
