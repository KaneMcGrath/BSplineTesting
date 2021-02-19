using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace BSplineTesting
{
    static class Renderer
    {

        public static void DrawTexture(FRect position, Texture2D tex)
        {
            Vector2[] points = Coordinates.ApplyAspect(position).GetCorners();
            Vector2[] texCoords = {
                new Vector2(0f,1f),
                new Vector2(1f,1f),
                new Vector2(1f,0f),
                new Vector2(0f,0f)
                };

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.BindTexture(TextureTarget.Texture2D, tex.id);
            GL.Enable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.White);
            GL.TexCoord2(texCoords[0]);
            GL.Vertex2(points[0]);
            GL.TexCoord2(texCoords[1]);
            GL.Vertex2(points[1]);
            GL.TexCoord2(texCoords[2]);
            GL.Vertex2(points[2]);

            GL.TexCoord2(texCoords[0]);
            GL.Vertex2(points[0]);
            GL.TexCoord2(texCoords[2]);
            GL.Vertex2(points[2]);
            GL.TexCoord2(texCoords[3]);
            GL.Vertex2(points[3]);
            GL.Color3(Color.White);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
        }

        public static void DrawTexture(FRect position, Texture2D tex, FRect UV, Color color)
        {
            Vector2[] points = Coordinates.ApplyAspect(position).GetCorners();
            Vector2[] texCoords = {
                UV.GetCorner(3),
                UV.GetCorner(2),
                UV.GetCorner(1),
                UV.GetCorner(0)
                };

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.BindTexture(TextureTarget.Texture2D, tex.id);
            GL.Enable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);
            GL.TexCoord2(texCoords[0]);
            GL.Vertex2(points[0]);
            GL.TexCoord2(texCoords[1]);
            GL.Vertex2(points[1]);
            GL.TexCoord2(texCoords[2]);
            GL.Vertex2(points[2]);

            GL.TexCoord2(texCoords[0]);
            GL.Vertex2(points[0]);
            GL.TexCoord2(texCoords[2]);
            GL.Vertex2(points[2]);
            GL.TexCoord2(texCoords[3]);
            GL.Vertex2(points[3]);
            GL.Color3(Color.White);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
        }


        //draws a circle out of lines centered on pos, with rad radius and with res number of points
        public static void LineCircle(Vector2 pos, float rad, int res, bool aspect = true) 
        {

            Vector2[] points = new Vector2[res + 1];
            Vector2 start = new Vector2(pos.X + rad, pos.Y + rad * 0f);
            points[0] = start;
            points[res] = start;
            for (int i = 1; i <= res; i++)
            {
                double t = ((double)(i) / res) * 2f * Math.PI;
                double x = pos.X + rad * Math.Cos(t);
                double y = pos.Y + rad * Math.Sin(t);
                points[i] = new Vector2((float)x, (float)y);
            }
            drawLine(points, aspect);
        }

        //draws a circle out of lines centered on pos, with rad radius and with res number of points
        public static void LineCircle(Vector2 pos, float rad, int res, Color c, bool aspect = true) 
        {

            Vector2[] points = new Vector2[res + 1];
            Vector2 start = new Vector2(pos.X + rad, pos.Y + rad * 0f);
            points[0] = start;
            points[res] = start;
            for (int i = 1; i <= res; i++)
            {
                double t = ((double)(i) / res) * 2f * Math.PI;
                double x = pos.X + rad * Math.Cos(t);
                double y = pos.Y + rad * Math.Sin(t);
                points[i] = new Vector2((float)x, (float)y);
            }
            drawLine(points, c, aspect);
        }


        //Draws a connected line segment between points
        public static void drawLine(Vector2[] points, bool aspect = true) 
        {
            GL.Begin(PrimitiveType.LineStrip);
            
            foreach (Vector2 point in points)
            {
                GL.Vertex2(aspect ? Coordinates.ApplyAspect(point) : point);
            }
            GL.End();
        }

        //Draws a connected line segment between points now in color
        public static void drawLine(Vector2[] points,Color c, bool aspect = true) 
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(c);
            foreach (Vector2 point in points)
            {
                GL.Vertex2(aspect ? Coordinates.ApplyAspect(point) : point);
            }
            GL.Color3(Color.White);
            GL.End();
        }

        //draws a rectangle with two triangles because apparently quads were deprecated or something i dont know.
        public static void drawRect(FRect rect, Color c)
        {

            FRect r = rect.shouldAdjustToAspect ? Coordinates.ApplyAspect(rect) : rect;
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(c);
            GL.Vertex2(new Vector2(r.left,r.top));
            GL.Vertex2(new Vector2(r.left+r.width,r.top + r.height));
            GL.Vertex2(new Vector2(r.left,r.top + r.height));
            GL.Vertex2(new Vector2(r.left,r.top));
            GL.Vertex2(new Vector2(r.left+r.width,r.top));
            GL.Vertex2(new Vector2(r.left + r.width, r.top + r.height));
            GL.Color3(Color.White);
            GL.End();
        }

        public static void drawSpline(Spline s)
        {

            drawLine(new Vector2[] { s.GlobalLeft(), s.position, s.GlobalRight() });
            LineCircle(s.position, 0.01f, 10);
            LineCircle(s.GlobalLeft(), 0.01f, 10);
            LineCircle(s.GlobalRight(), 0.01f, 10);

        }

        public static void drawSpline(Spline s, Color c)
        {

            drawLine(new Vector2[] { s.GlobalLeft(), s.position, s.GlobalRight() }, c);
            LineCircle(s.position, 0.01f, 10, c);
            LineCircle(s.GlobalLeft(), 0.01f, 10, c);
            LineCircle(s.GlobalRight(), 0.01f, 10, c);

        }

        public static void drawLineCurve(Line l, int resolution)
        {

            Vector2[] v = l.getPoints();
            for(int i = 0; i < v.Length - 1; i += 3)
            {
                drawLine(Bezier.SimpleBezierCurve(v[i], v[i + 1], v[i + 2], v[i + 3], resolution));
            }
        }
        public static void drawLineCurve(Line l, int resolution, Color c)
        {

            Vector2[] v = l.getPoints();
            for (int i = 0; i < v.Length - 1; i += 3)
            {
                drawLine(Bezier.SimpleBezierCurve(v[i], v[i + 1], v[i + 2], v[i + 3], resolution), c);
            }
        }
        public static void drawShape(Shape s, int resolution)
        {
            Vector2[] v = s.getPoints();
            for (int i = 0; i < v.Length - 1; i += 3)
            {
                drawLine(Bezier.SimpleBezierCurve(v[i], v[i + 1], v[i + 2], v[i + 3], resolution));
            }
        }

        public static void drawDrawing(Drawing d, int resolution)
        {
            Line[] l = d.getLines();
            for (int i = 0; i < l.Length; i++)
            {
                drawLineCurve(l[i], resolution);
            }
        }
    }
}
