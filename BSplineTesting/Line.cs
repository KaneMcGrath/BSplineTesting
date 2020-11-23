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
        public List<Spline> splines = new List<Spline>();
        public Color color;
        public bool visible;
        public bool complete;

        public Line(Spline[] splines) 
        {
            this.splines.AddRange(splines);
        }
        public Line()
        {
            
        }

        public void AddSpline(Spline s)
        {

        }

        public Spline[] getSplines()
        {
            return splines.ToArray();
        }

        public Vector2[] getPoints()
        {
            // really lazy fix later

            if (splines.Count == 0) return new Vector2[0];

            Vector2[] s = new Vector2[splines.Count * 3];
            for (int i = 0; i < splines.Count; i++)
            {
                s[i * 3] = splines[i].GlobalLeft();
                s[i * 3 + 1] = splines[i].position;
                s[i * 3 + 2] = splines[i].GlobalRight();
            }
            Vector2[] result = new Vector2[s.Length - 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s[i + 1];
            }
            if (!complete)
            {
                return result;
            }
            else
            {
                Vector2[] result2 = new Vector2[result.Length+3];

                for (int i = 0; i < result.Length; i++)
                {
                    result2[i] = result[i];
                }
                Spline[] s2 = getSplines();
                
                result2[result.Length] = s2[s2.Length-1].GlobalRight();
                result2[result.Length + 1] = s2[0].GlobalLeft();
                result2[result.Length + 2] = s2[0].position;
                
                return result2;
            }
        }

        public Vector2[] getCompletePoints()
        {
            // really lazy fix later
            Vector2[] points = getPoints();
            Vector2[] result = new Vector2[points.Length + 3];

            for (int i = 0; i < points.Length; i++)
            {
                result[i] = points[i];
            }
            Spline[] s = getSplines();
            result[points.Length] = s[s.Length - 1].GlobalRight();
            result[points.Length + 1] = s[0].GlobalLeft();
            result[points.Length + 2] = s[0].position;

            return result;

        }
    }

}
