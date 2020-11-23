using System;
using OpenTK;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{

    public static class VTools
    {
        static float distance(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt(Math.Pow((v2.X - v1.X), 2) + Math.Pow((v2.Y - v1.Y), 2));
        }

       
    }
}
