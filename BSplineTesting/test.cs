using System;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    public class test
    {
        public float x = 0f;
        public float y = 0f;
        float vx = 0f;
        float vy = 0f;

        public test()
        {

        }
        public test(float x, float y)
        {
            this.x = x;
            this.y = y;

        }

        public void Update()
        {
            
        }

        public void accel(float sx, float sy)
        {
            x += sx;
            y += sy;

        }
    }
}      
