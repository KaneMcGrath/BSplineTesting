using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    public struct FRect
    {
        public float left;
        public float top;
        public float width;
        public float height;

        public FRect(float left, float top, float width, float height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
        }
        public FRect(float left, float top, float radius)
        {
            float num = radius / 2f;
            this.left = left - num;
            this.top = top - num;
            this.width = radius;
            this.height = radius;
        }
        public FRect(Vector2 pos, float radius)
        {
            float num = radius / 2f;
            this.left = pos.X - num;
            this.top = pos.Y - num;
            this.width = radius;
            this.height = radius;
        }


        public Vector2 Center()
        {
            return new Vector2(left + width / 2f, top + height / 2);
        }

        public Vector2[] GetCorners()
        {
            return new Vector2[]
            {
                new Vector2(left,top),
                new Vector2(left + width,top),
                new Vector2(left+width,top+height),
                new Vector2(left,top+height)
            };
        }

        public Vector2 GetCorner(int index)
        {
            return GetCorners()[index];
        }
    }
}
