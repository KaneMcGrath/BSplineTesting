using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BSplineTesting
{
    class window : GameWindow
    {

        test T;
        test T2;
        float y = 0f;
        bool hide;
        bool hide2;
        int segments = 3;
        Drawing testDrawing = new Drawing(new Line[] 
        { 
            new Line(new Vector2[]
            {
                new Vector2(0.2f, 0.2f),
                new Vector2(0.4f, 0.3f),
                new Vector2(0.5f, 0.4f),
                new Vector2(0.3f, 0.5f),
            }),
            new Line(new Vector2[]
            {
                new Vector2(-0.2f, -0.2f),
                new Vector2(-0.4f, -0.3f),
                new Vector2(-0.5f, -0.4f),
                new Vector2(-0.3f, -0.5f),
            })
        });
        public window() : base(1280, 1280, OpenTK.Graphics.GraphicsMode.Default, "Test")
        {
            TargetRenderFrequency = 120;
            TargetUpdateFrequency = 120;
            T = new test();
            T2 = new test();
            

        }
        
        private void drawTris()
        {
            GL.Begin(PrimitiveType.Triangles);
            
            float xx = T.x;
            float yy = T.y;

            
            GL.Vertex2(xx - 0.2, yy - 0.2);
            
            GL.Vertex2(xx + 0.2, yy - 0.2);

            GL.Vertex2(xx, yy + 0.1);


            GL.End();
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

        private void PointerTriangle(Vector2 p, Color color, float size = 0.1f)
        {
            GL.Begin(PrimitiveType.Triangles);
            //data
            float xx = p.X;
            float yy = p.Y;

            GL.Color3(color);
            GL.Vertex2(xx, yy);

            GL.Vertex2(xx - size, yy - size);

            GL.Vertex2(xx + size, yy - size);
            GL.Color3(Color.White);

            GL.End();
        }


        private void drawDrawing(Drawing d)
        {
            
            Line[] lines = d.getLines();
            foreach(Line l in lines)
            {
                GL.Begin(PrimitiveType.LineStrip);
                Vector2[] points = l.getPoints();
                foreach(Vector2 point in points)
                {
                    GL.Vertex2(point);
                }
                GL.End();
            }
            
            
        }

        private void drawLine(Vector2[] points)
        {
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Vector2 point in points)
            {
                GL.Vertex2(point);
            }
            GL.End();
        }
        private void drawLine(Vector2[] points, Color color)
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

            int mx = IM.MouseX();
            int my = IM.MouseY();
            Vector2 A = new Vector2(T.x, T.y);
            Vector2 B = IM.Pix2Vec(mx, my);
            Vector2 C = new Vector2(T2.x, T2.y);
            Vector2 D = new Vector2(0.6f, -0.6f);
            Vector2 E = new Vector2(-0.6f, -0.6f);
            Vector2 L1 = CurveDrawer.LS(A, B, y);
            Vector2 L2 = CurveDrawer.LS(B, C, y);
            Vector2 L3 = CurveDrawer.LS(C, D, y);
            Vector2 p = CurveDrawer.LS(L1, L2, y);
            Vector2 p2 = CurveDrawer.LS(L2, L3, y);
            Vector2 p3 = CurveDrawer.LS(p, p2, y);
            if (hide)
            {
                drawLine(new Vector2[] { A, B }, Color.Blue);
                drawLine(new Vector2[] { B, C }, Color.Green);
               
                drawLine(new Vector2[] { L1, L2 }, Color.Orange);
                
                testTriangle(p, Color.Magenta, 0.01f);
               
                testTriangle(L1, Color.Cyan, 0.01f);
                testTriangle(L2, Color.Lavender, 0.01f);

                if (segments == 4)
                {
                    drawLine(new Vector2[] { C, D }, Color.Blue);
                    drawLine(new Vector2[] { L2, L3 }, Color.Orange);
                    drawLine(new Vector2[] { p, p2 }, Color.Coral);
                    testTriangle(p2, Color.DeepSkyBlue, 0.01f);
                    testTriangle(p3, Color.Cyan, 0.01f);
                    testTriangle(L3, Color.Lavender, 0.01f);

                }
            }
            if (!hide2) { 
                if (segments == 3) drawLine(CurveDrawer.SimpleBezierCurve(A, B, C, 70), Color.White);
                else if (segments == 4) drawLine(Bezier.SimpleBezierCurve(A, B, C, D, 100), Color.White);
                
            }
            testTriangle(A, Color.Red, 0.01f);
            PointerTriangle(B, Color.Red, 0.01f);
            testTriangle(C, Color.Red, 0.01f);
            if (segments == 4)
            {
                testTriangle(D, Color.Red, 0.01f);
            }
            
                int xx = Mouse.GetCursorState().X - Program.mainWindow.X;
                int yy = Mouse.GetCursorState().Y - Program.mainWindow.Y - 32;
            if ((xx > 0 && xx < Program.mainWindow.X + Program.mainWindow.Width) || (yy > 0 && yy < Program.mainWindow.Y + Program.mainWindow.Height)) Program.mainWindow.Cursor = MouseCursor.Empty;
           
                
            

           
                
               
            


            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            IM.Update();
            KaneGameManagerMKII.Update();
            KaneGameManagerMKII.LateUpdate();
            if (IM.GetKey(Key.W)) T.accel(0f, 0.01f);
            if (IM.GetKey(Key.A)) T.accel(-0.01f, 0f);
            if (IM.GetKey(Key.S)) T.accel(0f, -0.01f);
            if (IM.GetKey(Key.D)) T.accel(0.01f, 0f);
            if (IM.GetKey(Key.Up)) T2.accel(0f, 0.01f);
            if (IM.GetKey(Key.Left)) T2.accel(-0.01f, 0f);
            if (IM.GetKey(Key.Down)) T2.accel(0f, -0.01f);
            if (IM.GetKey(Key.Right)) T2.accel(0.01f, 0f);
            if (IM.GetKey(Key.Q))
            {
                y += 0.001f;
                if (y >= 1f)
                {
                    y = 0f;
                }
            }
            if (IM.KeyDown(Key.F))
            {

                hide = !hide;
            }
            if (IM.KeyDown(Key.G))
            {
                hide2 = !hide2;
            }
            if (IM.KeyDown(Key.Number3))
            {
                segments = 3;
            }
            if (IM.KeyDown(Key.Number4))
            {
                segments = 4;
            }

            if (IM.GetKey(Key.E))
            {

                y -= 0.001f;
                if (y <= 0f)
                {
                    y = 1f;
                }
            }
            if (IM.GetKey(Key.Escape)) System.Environment.Exit(1);
            T.Update();
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

        private void test()
        {
            
        }
    }
}
