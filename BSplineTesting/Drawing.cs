using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    class Drawing
    {
        public List<Line> lines = new List<Line>();

        public Drawing(Line[] lines)
        {
            this.lines.AddRange(lines);
        }
        
        public Line[] getLines()
        {
            return this.lines.ToArray();
        }
    }
}
