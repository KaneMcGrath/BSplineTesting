using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSplineTesting
{
    class KaneGameManagerMKII
    {
        public Spline S1;
        public Spline S2;
        public Spline S3;
        public static KaneGameManagerMKII instance;

        public KaneGameManagerMKII()
        {
            S1 = new Spline(new Vector2(0f, 0f));
            S2 = new Spline(new Vector2(0f, 0f));
            S3 = new Spline(new Vector2(0f, 0f));
        }
        public void Render()
        {
            int mouseX = IM.MouseX();
            int mouseY = IM.MouseY();

            Renderer.LineCircle(IM.Pix2Vec(mouseX,mouseY), 0.5f, 50);
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
