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

        public static Texture2D tex;

        private static FRect screenBounds = new FRect(-0.98f, -0.98f, 1.96f, 1.96f);
        private static Vector2[] screenBoundPoints = new Vector2[]
        {
            screenBounds.GetCorner(0),
            screenBounds.GetCorner(1),
            screenBounds.GetCorner(2),
            screenBounds.GetCorner(3),
            screenBounds.GetCorner(0)
        };



        public KaneGameManagerMKII()
        {
            
            
        }
        public void Render()
        {
            int mouseX = IM.MouseX();
            int mouseY = IM.MouseY();


           
            Renderer.drawRect(new FRect(-1f, -1f, 2f, 2f), Color.LightCoral);
            Renderer.DrawTexture(new FRect(0f, 0f, 0.3f, 0.3f), tex, new FRect(0f,0f,1f,1f));

            Renderer.drawLine(screenBoundPoints, Color.Black);


            Color c = Color.Black;
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
                            Renderer.drawSpline(s,Color.Black);
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
                Renderer.drawLineCurve(l, 40, Color.Black);
            }
        }



        public void OnLoad()
        {
            tex = Texture2D.LoadTexture("Data/TextAtlas.png");
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

            
            if (IM.KeyDown(Key.P))
            {
                if (mHeldSpline != null)
                {

                    
                    File.WriteAllText("point.txt", VTools.pack(mHeldSpline.position));
                }
                
            }
            if (IM.KeyDown(Key.L))
            {
                    string s = File.ReadAllText("point.txt");
                    File.WriteAllText("pointRead.txt", VTools.pack(VTools.unpack(s)));
                

            }

        }

        private Vector2 lastMousePos;
        private void ClickDrag()
        {
            
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
