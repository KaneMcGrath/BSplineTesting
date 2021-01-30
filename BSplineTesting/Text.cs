using System;

using System.Collections.Generic;
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

        static Text()
        {
            indexer['A'] = new XY(0, 0);
            indexer['B'] = new XY(1, 0);
            indexer['C'] = new XY(2, 0);
            indexer['D'] = new XY(3, 0);
            indexer['E'] = new XY(4, 0);
            indexer['F'] = new XY(5, 0);
            indexer['G'] = new XY(6, 0);
            indexer['H'] = new XY(7, 0);
            indexer['I'] = new XY(0, 1);
            indexer['J'] = new XY(1, 1);
            indexer['K'] = new XY(2, 1);
            indexer['L'] = new XY(3, 1);
            indexer['M'] = new XY(4, 1);
            indexer['N'] = new XY(5, 1);
            indexer['O'] = new XY(6, 1);
            indexer['P'] = new XY(7, 1);
            indexer['Q'] = new XY(0, 2);
            indexer['R'] = new XY(1, 2);
            indexer['S'] = new XY(2, 2);
            indexer['T'] = new XY(3, 2);
            indexer['U'] = new XY(4, 2);
            indexer['V'] = new XY(5, 2);
            indexer['W'] = new XY(6, 2);
            indexer['X'] = new XY(7, 2);
            indexer['Y'] = new XY(0, 3);
            indexer['Z'] = new XY(1, 3);
            indexer['a'] = new XY(2, 3);
            indexer['b'] = new XY(3, 3);
            indexer['c'] = new XY(4, 3);
            indexer['d'] = new XY(5, 3);
            indexer['e'] = new XY(6, 3);
            indexer['f'] = new XY(7, 3);
            indexer['g'] = new XY(0, 4);
            indexer['h'] = new XY(1, 4);
            indexer['i'] = new XY(2, 4);
            indexer['j'] = new XY(3, 4);
            indexer['k'] = new XY(4, 4);
            indexer['l'] = new XY(5, 4);
            indexer['m'] = new XY(6, 4);
            indexer['n'] = new XY(7, 4);
            indexer['o'] = new XY(0, 5);
            indexer['p'] = new XY(1, 5);
            indexer['q'] = new XY(2, 5);
            indexer['r'] = new XY(3, 5);
            indexer['s'] = new XY(4, 5);
            indexer['t'] = new XY(5, 5);
            indexer['u'] = new XY(6, 5);
            indexer['v'] = new XY(7, 5);
            indexer['w'] = new XY(0, 6);
            indexer['x'] = new XY(1, 6);
            indexer['y'] = new XY(2, 6);
            indexer['z'] = new XY(3, 6);
            indexer['.'] = new XY(4, 6);
            indexer[','] = new XY(5, 6);
            indexer['!'] = new XY(6, 6);
            indexer['?'] = new XY(7, 6);
            indexer['"'] = new XY(0, 7);
            indexer['\''] = new XY(1, 7);
            indexer['('] = new XY(2, 7);
            indexer[')'] = new XY(3, 7);
            indexer['/'] = new XY(4, 7);
            indexer['\\'] = new XY(5, 7);
            indexer[';'] = new XY(6, 7);
            indexer[':'] = new XY(7, 7);
            indexer['1'] = new XY(0, 8);
            indexer['2'] = new XY(1, 8);
            indexer['3'] = new XY(2, 8);
            indexer['4'] = new XY(3, 8);
            indexer['5'] = new XY(4, 8);
            indexer['6'] = new XY(5, 8);
            indexer['7'] = new XY(6, 8);
            indexer['8'] = new XY(7, 8);
            indexer['9'] = new XY(0, 9);
            indexer['0'] = new XY(1, 9);
            indexer['+'] = new XY(2, 9);
            indexer['-'] = new XY(3, 9);
            indexer['='] = new XY(4, 9);
            indexer['['] = new XY(5, 9);
            indexer[']'] = new XY(6, 9);
            indexer['{'] = new XY(7, 9);
            indexer['}'] = new XY(0, 10);
            indexer['%'] = new XY(1, 10);
            indexer['<'] = new XY(2, 10);
            indexer['>'] = new XY(3, 10);
            indexer['&'] = new XY(4, 10);
            indexer['$'] = new XY(5, 10);
            indexer['#'] = new XY(6, 10);
            indexer['^'] = new XY(7, 10);
            indexer['*'] = new XY(0, 11);
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
        }

        public static FRect GetUVForChar(char c)
        {
            if (indexer.ContainsKey(c))
            {

                 return new FRect(0.125f * indexer[c].x, 0.0625f * indexer[c].y, 1f / 8f, 1f / 16f);

            }

            return new FRect();

                

        }


        public static void DrawString(Vector2 pos,float size, string s)
        {
            float width = s.Length * size;
            
            for (int i = 0; i < s.Length; i++)
            {
                Renderer.DrawTexture(new FRect(pos.X + (size * i), pos.Y, size, size), fullAtlas, GetUVForChar(s[i]));
            }
        }


        private struct XY
        {
            public int x;
            public int y;

            public XY(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

    }
}
