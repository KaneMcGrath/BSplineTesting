using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSplineTesting
{
    class KaneGameManagerMKII
    {
        public Spline S1;
        public Spline S2;
        public Spline S3;
        public Spline S4;
        public Line l;
        public static KaneGameManagerMKII instance;

        public KaneGameManagerMKII()
        {
            S1 = new Spline(new Vector2(-0.8f, 0f));
            S2 = new Spline(new Vector2(-0.1f, -0.4f));
            S3 = new Spline(new Vector2(0.6f, 0.4f));
            S4 = new Spline(new Vector2(0.8f, -0.4f),new Vector2(-0.02f,0.1f),new Vector2(0.02f,-0.1f));
            l = new Line(new Spline[]{ S1,S2,S3,S4});
        }
        public void Render()
        {
            int mouseX = IM.MouseX();
            int mouseY = IM.MouseY();

            Renderer.LineCircle(IM.Pix2Vec(mouseX,mouseY), 0.004f, 20);

            Renderer.drawSpline(S1);
            Renderer.drawSpline(S2);
            Renderer.drawSpline(S3);
            Renderer.drawSpline(S4);

            Renderer.drawLineCurve(l, 30);
        }

        public void Update()
        {
            int xx = Mouse.GetCursorState().X - Program.mainWindow.X;
            int yy = Mouse.GetCursorState().Y - Program.mainWindow.Y - 32;
            if ((xx > 0 && xx < Program.mainWindow.X + Program.mainWindow.Width) || (yy > 0 && yy < Program.mainWindow.Y + Program.mainWindow.Height)) Program.mainWindow.Cursor = MouseCursor.Empty;
            
        }

        public void LateUpdate()
        {

        }

    }
}
