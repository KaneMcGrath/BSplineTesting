using OpenTK;
using OpenTK.Input;
using System;
using System.IO;
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
        public Vector2 mOffset = new Vector2();
        public Spline mHeldSpline = null;
        public Spline LastHeldSpline = null;
        public int mControlPoint = 0;
        public static bool hide;

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


            if (IM.GetMouseButton(MouseButton.Left))
            {

                Renderer.LineCircle(IM.FmouseVec(), 0.004f, 20, Color.Red);
            }
            else
            {
                bool flag = false;
                foreach (Spline s in l.splines)
                {
                    if (IM.FisMouseInRect(new FRect(s.position, 0.04f)) || IM.FisMouseInRect(new FRect(s.leftPos, 0.04f)) || IM.FisMouseInRect(new FRect(s.rightPos, 0.04f)))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    Renderer.LineCircle(IM.FmouseVec(), 0.004f, 10, Color.Purple);
                }
                else
                {
                    Renderer.LineCircle(IM.FmouseVec(), 0.004f, 10);
                }

            }

            if (!hide)
            {
                foreach (Spline s in l.splines)
                {
                    Renderer.drawSpline(s);
                }
            }
            Renderer.drawLineCurve(l, 40);
        }

        public void Update()
        {
            int xx = Mouse.GetCursorState().X - Program.mainWindow.X;
            int yy = Mouse.GetCursorState().Y - Program.mainWindow.Y - 32;
            if ((xx > 0 && xx < Program.mainWindow.X + Program.mainWindow.Width) || (yy > 0 && yy < Program.mainWindow.Y + Program.mainWindow.Height)) Program.mainWindow.Cursor = MouseCursor.Empty;
            SplineManipulator(l);
            if (IM.KeyDown(Key.H)) hide = !hide;
            if (IM.KeyDown(Key.Space))
            {
                l.splines.Add(new Spline(IM.FmouseVec()));
            }
            if (IM.KeyDown(Key.BackSpace))
            {
                l.splines.RemoveAt(l.splines.Count - 1);
            }
            if (IM.KeyDown(Key.P))
            {
                string[] s = new string[l.splines.Count];

                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = l.splines[i].position.ToString();
                }
                File.WriteAllLines("Line.txt", s);
            }

        }

        public void LateUpdate()
        {

        }

        private void SplineManipulator(Line l)
        {
            if (mHeldSpline != null)
            {
                
                if (IM.GetMouseButton(MouseButton.Left))
                {
                    if (mControlPoint == 0)
                    {
                        mHeldSpline.position = new Vector2(IM.FmouseX() + mOffset.X, IM.FmouseY() + mOffset.Y);
                    }
                    else if (mControlPoint == 1)
                    {
                        mHeldSpline.moveControlPoint(new Vector2(IM.FmouseX() + mOffset.X, IM.FmouseY() + mOffset.Y), true);
                    }
                    else
                    {
                        mHeldSpline.moveControlPoint(new Vector2(IM.FmouseX() + mOffset.X, IM.FmouseY() + mOffset.Y), false);
                    }
                }
                else
                {
                    mHeldSpline = null;
                }
                
                
            }
            else
            {
                foreach (Spline s in l.splines)
                {

                    if (IM.MouseButtonDown(MouseButton.Left))
                    {
                        if (IM.FisMouseInRect(new FRect(s.GlobalLeft(), 0.04f)))
                        {
                            mOffset = new Vector2(s.GlobalLeft().X - IM.FmouseX(), s.GlobalLeft().Y - IM.FmouseY());
                            mHeldSpline = s;
                            mControlPoint = 1;
                        }
                        if (IM.FisMouseInRect(new FRect(s.GlobalRight(), 0.04f)))
                        {
                            mOffset = new Vector2(s.GlobalRight().X - IM.FmouseX(), s.GlobalRight().Y - IM.FmouseY());
                            mHeldSpline = s;
                            mControlPoint = 2;
                        }
                        else if (IM.FisMouseInRect(new FRect(s.position, 0.04f)))
                        {
                            
                            mOffset = new Vector2(s.position.X - IM.FmouseX(), s.position.Y - IM.FmouseY());
                            mHeldSpline = s;
                            mControlPoint = 0;
                        }
                    }
                }
            }
        }

    }
}
