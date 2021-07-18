using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Battle_Engine
{
    public class InputSystem
    {
        public KeyboardState keyboardState;
        public KeyboardState previousKeyboardState;

        public bool CheckKeyPressed(Keys key)
        {
            previousKeyboardState = keyboardState;
            KeyboardState keyboardStateNew = Keyboard.GetState();

            if (keyboardStateNew.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        public void Initialize()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
        }
    }
}
