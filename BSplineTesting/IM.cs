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

        //private static Dictionary<Key, bool> pressedKeys = new Dictionary<Key, bool>();
        //private static Dictionary<Key, bool> DownKeys = new Dictionary<Key, bool>();
        //private static Dictionary<Key, bool> UpKeys = new Dictionary<Key, bool>();
        //private static Dictionary<MouseButton, bool> mouseButtons = new Dictionary<MouseButton, bool>();
        //private static Dictionary<MouseButton, bool> downMouseButtons = new Dictionary<MouseButton, bool>();

        private static Dictionary<Key, KeySettings> keys = new Dictionary<Key, KeySettings>();
        private static Dictionary<MouseButton, MBSettings> mouseButtons = new Dictionary<MouseButton, MBSettings>();

        public static char lastTyped;

        class KeySettings
        {
            public KeySettings(Key key)
            {
                this.key = key;
                pressed = false;
                queueDown = false;
                down = false;
                queueUp = false;
                up = false;
            }

            public Key key;
            
            public bool pressed;
            public bool queueDown;
            public bool down;
            public bool queueUp;
            public bool up;
            
            
        }

        class MBSettings
        {
            public MBSettings(MouseButton button)
            {
                this.button = button;
                pressed = false;
                queueDown = false;
                down = false;
                queueUp = false;
                up = false;
            }
            public MouseButton button;

            public bool pressed;
            public bool queueDown;
            public bool down;
            public bool queueUp;
            public bool up;

        }


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
                if (!keys.ContainsKey(k)) keys.Add(k, new KeySettings(k));
            }
            foreach (MouseButton b in allMouse)
            {
                if (!mouseButtons.ContainsKey(b)) mouseButtons.Add(b, new MBSettings(b));
            }
        }


        //gets key down and key up events from the window
        public static void OnKeyDown(Key k)
        {
            if (!keys[k].pressed)
                keys[k].queueDown = true;
            keys[k].pressed = true;

            KaneGameManagerMKII.instance.onKeyPressed(k.ToString());
        }
        public static void OnKeyUp(Key k)
        {
            keys[k].pressed = false;
            keys[k].queueUp = true;
        }
        public static void OnMouseButtonDown(MouseButton b)
        {
            if (!mouseButtons[b].pressed)
                mouseButtons[b].queueDown = true;
            mouseButtons[b].pressed = true;
        }
        public static void OnMouseButtonUp(MouseButton b)
        {
            mouseButtons[b].pressed = false;
            mouseButtons[b].queueUp = true;
        }



        //Called before the game manager update
        //sets down keys if they are pressed
        public static void Update()
        {
            // foreach (Key k in pressedKeys.Keys.ToArray())

            foreach (Key k in keys.Keys.ToArray())
            {
                if (keys[k].queueDown)
                {
                    keys[k].queueDown = false;
                    keys[k].down = true;
                }
                if (keys[k].queueUp)
                {
                    keys[k].queueUp = false;
                    keys[k].up = true;
                }
            }
            foreach (MouseButton b in mouseButtons.Keys.ToArray())
            {
                if (mouseButtons[b].queueDown)
                {
                    mouseButtons[b].queueDown = false;
                    mouseButtons[b].down = true;
                }
                if (mouseButtons[b].queueUp)
                {
                    mouseButtons[b].queueUp = false;
                    mouseButtons[b].up = true;
                }
            }

        }


        //called after game manager update
        //sets down and upkeys to false
        public static void PostRender()
        {
            foreach (Key k in keys.Keys.ToArray())
            {
                if (keys[k].down)
                {
                    keys[k].down = false;
                }
                if (keys[k].up)
                {
                    keys[k].up = false;
                }
            }
            foreach (MouseButton b in mouseButtons.Keys.ToArray())
            {
                if (mouseButtons[b].down)
                {
                    mouseButtons[b].down = false;
                }
                if (mouseButtons[b].up)
                {
                    mouseButtons[b].up = false;
                }
            }
        }

        

        //usable functions for inputs

        //returns true if a key is held down
        public static bool GetKey(Key k)
        {
            return keys[k].pressed;
        }

        //returns true if the key was pressed down on this frame
        public static bool KeyDown(Key k)
        {
            return keys[k].down;
        }

        //returns true if the key is released on this frame although might not be exact due to the implementation
        public static bool KeyUp(Key k)
        {
            return keys[k].up;
        }

        public static bool MouseButtonDown(MouseButton b)
        {
            return mouseButtons[b].down;
        }

        public static bool MouseButtonUp(MouseButton b)
        {
            return mouseButtons[b].up;
        }

        public static bool GetMouseButton(MouseButton b)
        {
            return mouseButtons[b].pressed;
        }

        public static int MouseX() // gets mouse x position in window pixels
        {
            Rectangle bounds = Program.mainWindow.ClientRectangle;
            int x = Mouse.GetCursorState().X - Program.mainWindow.X;
            if (x < 0) x = 0;
            else if (x > bounds.Width) x = bounds.X + bounds.Width;
            return (int)(x * Coordinates.aspectX);
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
            return ((((float)x / Program.mainWindow.Width) - 0.5f) * 2f) / Coordinates.aspectX;
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

            FRect r = !rect.shouldAdjustToAspect ? Coordinates.RemoveAspect(rect) : rect;

            if (x > r.left && x < r.left + r.width && y > r.top && y < r.top + r.height) return true;
            return false;
        }

        public static bool isMouseInRect(Rectangle rectangle)
        {
            FRect r = Coordinates.RectToFRect(rectangle);
            return FisMouseInRect(r);
        }
    }
}
