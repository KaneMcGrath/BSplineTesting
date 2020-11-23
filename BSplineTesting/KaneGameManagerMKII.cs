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

        
        Drawing d = new Drawing();
        public static KaneGameManagerMKII instance;
        public Vector2 mOffset = new Vector2();
        public Spline mHeldSpline = null;
        public Spline LastHeldSpline = null;
        public int lineIndex = 0;
        public int mControlPoint = 0;
        public static bool hide;

        public KaneGameManagerMKII()
        {
            
            
        }
        public void Render()
        {
            int mouseX = IM.MouseX();
            int mouseY = IM.MouseY();


            Color c = Color.White;
            if (IM.GetMouseButton(MouseButton.Left))
            {

                c = Color.Red;
            }
            else
            {
                
                foreach (Line l in d.getLines())
                {
                    foreach (Spline s in l.splines)
                    {
                        if (IM.FisMouseInRect(new FRect(s.position, 0.04f)) || IM.FisMouseInRect(new FRect(s.leftPos, 0.04f)) || IM.FisMouseInRect(new FRect(s.rightPos, 0.04f)))
                        {
                            c = Color.Magenta;
                        }
                    }
                   
                }

            }
            Renderer.LineCircle(IM.FmouseVec(), 0.004f, 20, c);

            if (!hide)
            {
                Line[] array = d.getLines();
                for (int i = 0; i < array.Length; i++)
                {
                    Line l = array[i];
                    if (i == lineIndex)
                    {
                        foreach (Spline s in l.splines)
                        {
                            Renderer.drawSpline(s);
                        }
                    }
                    else
                    {
                        foreach (Spline s in l.splines)
                        {
                            Renderer.drawSpline(s, Color.Gray);
                        }
                    }
                }
            }
            foreach (Line l in d.getLines())
            {
                Renderer.drawLineCurve(l, 40);
            }
        }

        public void Update()
        {
            int xx = Mouse.GetCursorState().X - Program.mainWindow.X;
            int yy = Mouse.GetCursorState().Y - Program.mainWindow.Y - 32;
            if ((xx > 0 && xx < Program.mainWindow.X + Program.mainWindow.Width) || (yy > 0 && yy < Program.mainWindow.Y + Program.mainWindow.Height)) Program.mainWindow.Cursor = MouseCursor.Empty;
            foreach (Line l in d.getLines())
            {
                SplineManipulator(l);
            }
            if (IM.KeyDown(Key.H)) hide = !hide;
            if (IM.KeyDown(Key.Space))
            {
                if (d.lines.Count == 0)
                {
                    d.lines.Add(new Line());
                }
                d.lines[lineIndex].splines.Add(new Spline(IM.FmouseVec()));
            }
            if (IM.KeyDown(Key.Enter))
            {
                d.lines.Add(new Line());
                lineIndex++;
            }
            if (IM.KeyDown(Key.BackSpace))
            {
                d.lines[lineIndex].splines.RemoveAt(d.lines[lineIndex].splines.Count - 1);
            }if (IM.KeyDown(Key.C))
            {
                d.lines[lineIndex].complete = !d.lines[lineIndex].complete;
            }

            /*
            if (IM.KeyDown(Key.P))
            {
                string[] s = new string[l.splines.Count];

                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = l.splines[i].position.ToString() + l.splines[i].leftPos.ToString() + l.splines[i].rightPos.ToString();
                }
                File.WriteAllLines("Line.txt", s);
            }
            */
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
                            lineIndex = d.lines.IndexOf(l);
                        }
                        if (IM.FisMouseInRect(new FRect(s.GlobalRight(), 0.04f)))
                        {
                            mOffset = new Vector2(s.GlobalRight().X - IM.FmouseX(), s.GlobalRight().Y - IM.FmouseY());
                            mHeldSpline = s;
                            mControlPoint = 2;
                            lineIndex = d.lines.IndexOf(l);
                        }
                        else if (IM.FisMouseInRect(new FRect(s.position, 0.04f)))
                        {
                            
                            mOffset = new Vector2(s.position.X - IM.FmouseX(), s.position.Y - IM.FmouseY());
                            mHeldSpline = s;
                            mControlPoint = 0;
                            lineIndex = d.lines.IndexOf(l);
                        }
                    }
                }
            }
        }

    }
}
