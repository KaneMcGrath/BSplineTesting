using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;

namespace BSplineTesting
{
    class window : GameWindow
    {
        KaneGameManagerMKII MKII;
        public window() : base(1280, 1280, OpenTK.Graphics.GraphicsMode.Default, "Test")
        {
            TargetRenderFrequency = 120;
            TargetUpdateFrequency = 120;
            MKII = new KaneGameManagerMKII();
            KaneGameManagerMKII.instance = MKII;
        }
        
        
        private void testTriangle(Vector2 p, Color color, float size = 0.1f)
        {
            GL.Begin(PrimitiveType.Triangles);
            //data
            float xx = p.X;
            float yy = p.Y;

            GL.Color3(color);
            GL.Vertex2(xx - size, yy - size);

            GL.Vertex2(xx + size, yy - size);

            GL.Vertex2(xx, yy + (size/2f));
            GL.Color3(Color.White);

            GL.End();
        }


        public static void drawLine(Vector2[] points, Color color)
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(color);
            foreach (Vector2 point in points)
            {
                GL.Vertex2(point);
            }
            GL.Color3(Color.White);
            GL.End();
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            MKII.Render();
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            IM.Update();
            MKII.Update();
            MKII.LateUpdate();
            IM.LateUpdate();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            IM.OnKeyDown(e.Key);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            IM.OnKeyUp(e.Key);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            IM.OnMouseButtonDown(e.Button);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            IM.OnMouseButtonUp(e.Button);
        }
    }
}
