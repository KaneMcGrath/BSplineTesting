using System;
using OpenTK;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{

    public static class VTools
    {
        public static float distance(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt(Math.Pow((v2.X - v1.X), 2) + Math.Pow((v2.Y - v1.Y), 2));
        }

        public static string pack(Vector2 v)
        {
            return "(" + v.X + "," + v.Y +")";
        }

        public static Vector2 unpack(string v)
        {
            string[] values = v.Substring(1).Remove(v.Length - 2).Split(new char[] { ',' });
            return new Vector2(Convert.ToSingle(values[0]), Convert.ToSingle(values[1]));
        }
       
    }
}
