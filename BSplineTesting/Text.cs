using System;

using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace BSplineTesting
{
   

    static class Text
    {
        private static Dictionary<char, XY> indexer = new Dictionary<char, XY>();
        public static Texture2D fullAtlas;
        public static int trim = 4;
        public static Color defaultTextColor = Color.White;

        static Text()
        {
            indexer['A'] = new XY(0, 0);
            indexer['B'] = new XY(1, 0, 6);
            indexer['C'] = new XY(2, 0);
            indexer['D'] = new XY(3, 0, 5);
            indexer['E'] = new XY(4, 0, 6);
            indexer['F'] = new XY(5, 0, 6);
            indexer['G'] = new XY(6, 0);
            indexer['H'] = new XY(7, 0, 5);
            indexer['I'] = new XY(0, 1, 6);
            indexer['J'] = new XY(1, 1, 5);
            indexer['K'] = new XY(2, 1, 6);
            indexer['L'] = new XY(3, 1, 7);
            indexer['M'] = new XY(4, 1);
            indexer['N'] = new XY(5, 1);
            indexer['O'] = new XY(6, 1);
            indexer['P'] = new XY(7, 1, 5);
            indexer['Q'] = new XY(0, 2);
            indexer['R'] = new XY(1, 2, 5);
            indexer['S'] = new XY(2, 2, 5);
            indexer['T'] = new XY(3, 2);
            indexer['U'] = new XY(4, 2);
            indexer['V'] = new XY(5, 2);
            indexer['W'] = new XY(6, 2);
            indexer['X'] = new XY(7, 2);
            indexer['Y'] = new XY(0, 3);
            indexer['Z'] = new XY(1, 3);
            indexer['a'] = new XY(2, 3, 6);
            indexer['b'] = new XY(3, 3, 6);
            indexer['c'] = new XY(4, 3, 7);
            indexer['d'] = new XY(5, 3, 6);
            indexer['e'] = new XY(6, 3, 7);
            indexer['f'] = new XY(7, 3, 7);
            indexer['g'] = new XY(0, 4, 7);
            indexer['h'] = new XY(1, 4, 7);
            indexer['i'] = new XY(2, 4, 10);
            indexer['j'] = new XY(3, 4, 9);
            indexer['k'] = new XY(4, 4, 8);
            indexer['l'] = new XY(5, 4, 11);
            indexer['m'] = new XY(6, 4, 6);
            indexer['n'] = new XY(7, 4, 7);
            indexer['o'] = new XY(0, 5, 6);
            indexer['p'] = new XY(1, 5, 6);
            indexer['q'] = new XY(2, 5, 5);
            indexer['r'] = new XY(3, 5, 8);
            indexer['s'] = new XY(4, 5, 8);
            indexer['t'] = new XY(5, 5, 8);
            indexer['u'] = new XY(6, 5, 7);
            indexer['v'] = new XY(7, 5, 6);
            indexer['w'] = new XY(0, 6);
            indexer['x'] = new XY(1, 6, 8);
            indexer['y'] = new XY(2, 6, 8);
            indexer['z'] = new XY(3, 6, 8);
            indexer['.'] = new XY(4, 6, 11);
            indexer[','] = new XY(5, 6, 11);
            indexer['!'] = new XY(6, 6, 11);
            indexer['?'] = new XY(7, 6, 6);
            indexer['"'] = new XY(0, 7, 8);
            indexer['\''] = new XY(1, 7, 11);
            indexer['('] = new XY(2, 7, 10);
            indexer[')'] = new XY(3, 7, 10);
            indexer['/'] = new XY(4, 7, 8);
            indexer['\\'] = new XY(5, 7, 8);
            indexer[';'] = new XY(6, 7, 11);
            indexer[':'] = new XY(7, 7, 11);
            indexer['1'] = new XY(0, 8, 8);
            indexer['2'] = new XY(1, 8, 6);
            indexer['3'] = new XY(2, 8, 6);
            indexer['4'] = new XY(3, 8, 5);
            indexer['5'] = new XY(4, 8, 6);
            indexer['6'] = new XY(5, 8, 6);
            indexer['7'] = new XY(6, 8, 6);
            indexer['8'] = new XY(7, 8, 6);
            indexer['9'] = new XY(0, 9, 6);
            indexer['0'] = new XY(1, 9);
            indexer['+'] = new XY(2, 9);
            indexer['-'] = new XY(3, 9);
            indexer['='] = new XY(4, 9);
            indexer['['] = new XY(5, 9, 9);
            indexer[']'] = new XY(6, 9, 9);
            indexer['{'] = new XY(7, 9, 7);
            indexer['}'] = new XY(0, 10, 7);
            indexer['%'] = new XY(1, 10, 2);
            indexer['<'] = new XY(2, 10, 7);
            indexer['>'] = new XY(3, 10, 7);
            indexer['&'] = new XY(4, 10);
            indexer['$'] = new XY(5, 10, 7);
            indexer['#'] = new XY(6, 10, 1);
            indexer['^'] = new XY(7, 10, 6);
            indexer['*'] = new XY(0, 11, 8);
            indexer['_'] = new XY(1, 11);
            indexer['☺'] = new XY(2, 11);
            indexer['☻'] = new XY(3, 11);
            indexer['♥'] = new XY(4, 11);
            indexer['♦'] = new XY(5, 11);
            indexer['♣'] = new XY(6, 11);
            indexer['♠'] = new XY(7, 11);
            indexer['•'] = new XY(0, 12);
            indexer['◘'] = new XY(1, 12);
            indexer['○'] = new XY(2, 12);
            indexer['◙'] = new XY(3, 12);
            indexer['♂'] = new XY(4, 12);
            indexer['♀'] = new XY(5, 12);
            indexer['♪'] = new XY(6, 12);
            indexer['♫'] = new XY(7, 12);
            indexer[' '] = new XY(7, 15, 8);

        }

        public static FRect GetUVForChar(char c)
        {
            if (indexer.ContainsKey(c))
            {
                return new FRect(0.125f * (indexer[c].x), 0.0625f * indexer[c].y, ((16f - (float)indexer[c].t) / 128f), 1f / 16f);
            }
            return new FRect();
        }

        public static FRect DrawStringR(Vector2 pos, float size, string s, Color c)
        {
            float x = pos.X;
            for (int i = 0; i < s.Length; i++)
            {
                FRect UV = GetUVForChar(s[i]);
                float w = UV.width * size;
                float h = UV.height * size * 2;
                Renderer.DrawTexture(new FRect(x, pos.Y, w, h), fullAtlas, UV, c);
                x += w;
            }
            return new FRect(pos.X, pos.Y, x - pos.X, (1f / 16f) * size * 2f);
        }

        public static void DrawString(Vector2 pos,float size, string s)
        {
            
            float x = pos.X;
            for (int i = 0; i < s.Length; i++)
            {
                FRect UV = GetUVForChar(s[i]);
                float w = UV.width * size;
                float h = UV.height * size;
                Renderer.DrawTexture(new FRect(x, pos.Y, w, h*2), fullAtlas, UV, Color.White);
                x += w;
            }
        }

        public static void DrawString(Vector2 pos, float size, string s, Color c)
        {
            float x = pos.X;
            for (int i = 0; i < s.Length; i++)
            {
                FRect UV = GetUVForChar(s[i]);
                float w = UV.width * size;
                float h = UV.height * size;
                Renderer.DrawTexture(new FRect(x, pos.Y, w, h), fullAtlas, UV, c);
                x += w;
            }
        }


        private struct XY
        {
            public int x;
            public int y;


            /// <summary>
            /// Units needed to trim from the right side to have 3 pixels of space between each letter
            /// Defaults to 4
            /// </summary>
            public int t;

            public XY(int x, int y)
            {
                this.x = x;
                this.y = y;
                t = 4;
            }
            public XY(int x, int y, int t)
            {
                this.x = x;
                this.y = y;
                this.t = t;
            }
        }

    }
}
