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

        public Line(Spline[] splines) 
        {
            this.splines.AddRange(splines);
        }

        public Spline[] getSplines()
        {
            return splines.ToArray();
        }

        public Vector2[] getPoints()
        {
            // really lazy fix later

            if (splines.Count == 0) return new Vector2[0];

            Vector2[] s = new Vector2[splines.Count*3];
            for(int i = 0; i < splines.Count; i++)
            {
                s[i * 3] = splines[i].GlobalLeft();
                s[i * 3 + 1] = splines[i].position;
                s[i * 3 + 2] = splines[i].GlobalRight();
            }
            Vector2[] result = new Vector2[s.Length - 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s[i+1];
            }
            
           
            return result;
        }
    }

}
