using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public static class Input
    {
        public static MouseState previousMouseState;

        public static void Update(GameTime gameTime)
        {

        }

        public static bool GetMousePressed()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                previousMouseState = mouseState; 
                return false;
            }

            
        }
    }
}
