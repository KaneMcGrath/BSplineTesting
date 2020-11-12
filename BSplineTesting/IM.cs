using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace BSplineTesting
{
    public class IM  //InputManager
    {
        //Dictionarys for 3 states of each key
        //Down and up keys should be set true at the beginning of the Update, and set false at the end in LateUpdate
        //Although im not sure how the up keys will work like that so for those i'll just set it on the event and hope for the best
        private static Dictionary<Key, bool> pressedKeys = new Dictionary<Key, bool>();
        private static Dictionary<Key, bool> DownKeys = new Dictionary<Key, bool>();
        private static Dictionary<Key, bool> UpKeys = new Dictionary<Key, bool>();
        private static Dictionary<MouseButton, bool> mouseButtons = new Dictionary<MouseButton, bool>();
        private static Dictionary<MouseButton, bool> downMouseButtons = new Dictionary<MouseButton, bool>();


        static IM()
        {
            registerAll();
        }


        //Adds all keys to the dictionarys
        private static void registerAll()
        {
            var allKeys = Enum.GetValues(typeof(Key)).Cast<Key>();
            var allMouse = Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>();
            foreach (Key k in allKeys)
            {
                if (!pressedKeys.ContainsKey(k)) pressedKeys.Add(k, false);
                if (!DownKeys.ContainsKey(k)) DownKeys.Add(k, false);
                if (!UpKeys.ContainsKey(k)) UpKeys.Add(k, false);

            }
            foreach (MouseButton b in allMouse)
            {
                if (!mouseButtons.ContainsKey(b)) mouseButtons.Add(b, false);
                if (!downMouseButtons.ContainsKey(b)) downMouseButtons.Add(b, false);
                

            }
        }


        //gets key down and key up events from the window
        public static void OnKeyDown(Key k)
        {
            if (!pressedKeys[k])
                DownKeys[k] = true;
            pressedKeys[k] = true;
        }
        public static void OnKeyUp(Key k)
        {
            pressedKeys[k] = false;
        }
        public static void OnMouseButtonDown(MouseButton b)
        {
            if (!mouseButtons[b])
                downMouseButtons[b] = true;
            mouseButtons[b] = true;
        }
        public static void OnMouseButtonUp(MouseButton b)
        {
            mouseButtons[b] = false;
        }



        //Called before the game manager update
        //sets down keys if they are pressed
        public static void Update()
        {
           // foreach (Key k in pressedKeys.Keys.ToArray())
            
            
        }


        //called after game manager update
        //sets down and upkeys to false
        public static void LateUpdate()
        {
            foreach (Key k in DownKeys.Keys.ToArray())
            {
                if (DownKeys[k])
                {
                    DownKeys[k] = false;
                }
            }
            foreach (MouseButton b in downMouseButtons.Keys.ToArray())
            {
                if (downMouseButtons[b])
                {
                    downMouseButtons[b] = false;
                }
            }
            foreach (Key k in UpKeys.Keys.ToArray())
            {
                if (UpKeys[k])
                {
                    UpKeys[k] = false;
                }
            }
        }

        

        //usable functions for inputs

        //returns true if a key is held down
        public static bool GetKey(Key k)
        {
            return pressedKeys[k];
        }

        //returns true if the key was pressed down on this frame
        public static bool KeyDown(Key k)
        {
            return DownKeys[k];
        }

        //returns true if the key is released on this frame although might not be exact due to the implementation
        public static bool KeyUp(Key k)
        {
            return UpKeys[k];
        }

        public static bool MouseButtonDown(MouseButton b)
        {

            return downMouseButtons[b];
        }

        public static bool MouseButtonUp(MouseButton b)
        {

            return true;
        }

        public static bool GetMouseButton(MouseButton b)
        {

            return mouseButtons[b];
        }

        public static int MouseX() // gets mouse x position in window pixels
        {
            Rectangle bounds = Program.mainWindow.ClientRectangle;
            int x = Mouse.GetCursorState().X - Program.mainWindow.X;
            if (x < 0) x = 0;
            else if (x > bounds.Width) x = bounds.X + bounds.Width;
            return x;
        }

        public static int MouseY() // gets mouse x position in window pixels
        {
            Rectangle bounds = Program.mainWindow.ClientRectangle;
            int y = Mouse.GetCursorState().Y - Program.mainWindow.Y - (Program.mainWindow.Bounds.Height - bounds.Height);
            if (y < 0) y = 0;
            else if (y > bounds.Height) y = bounds.Y + bounds.Height;
            return y;
        }

        public static float FmouseX() // gets mouse x position in absolute Fcoordinates (-1.0 to 1.0)
        {
            Rectangle bounds = Program.mainWindow.ClientRectangle;
            int x = Mouse.GetCursorState().X - Program.mainWindow.X;
            if (x < 0) x = 0;
            else if (x > bounds.Width) x = bounds.X + bounds.Width;
            return ((((float)x / Program.mainWindow.Width) - 0.5f) * 2f);
        }

        public static float FmouseY() // gets mouse x position in absolute Fcoordinates (-1.0 to 1.0)
        {
            Rectangle bounds = Program.mainWindow.ClientRectangle;
            int y = Mouse.GetCursorState().Y - Program.mainWindow.Y - (Program.mainWindow.Bounds.Height - bounds.Height);
            if (y < 0) y = 0;
            else if (y > bounds.Height) y = bounds.Y + bounds.Height;
            return (-((float)y / Program.mainWindow.Height) + 0.5f) * 2f;
        }

        public static Vector2 FmouseVec()
        {
            return new Vector2(FmouseX(), FmouseY());
        }

        public static Vector2 Pix2Vec(int x, int y) //Converts Window pixels into absolute Fcoordinates (-1.0 to 1.0)
        {
            var width = Program.mainWindow.Width;
            var height = Program.mainWindow.Height;
            return new Vector2(((((float)x / width)-0.5f)*2f),(-((float) y / height)+0.5f)*2f);
        }

        public static bool FisMouseInRect(FRect rect)
        {
            float x = FmouseX();
            float y = FmouseY();
            if (x > rect.left && x < rect.left + rect.width && y > rect.top && y < rect.top + rect.height) return true;
            return false;
        }
    }
}
