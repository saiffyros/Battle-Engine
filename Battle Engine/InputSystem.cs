using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Battle_Engine
{
    public class InputSystem : GameComponent
    {
        public InputSystem (Game game) : base(game)
        {

        }

        private static KeyboardState _previousKeyboardState;

        public static KeyboardState previousKeyboardState
        {
            get { return _previousKeyboardState; }
        }

        public static bool CheckKeyPressed(Keys key)
        {
            KeyboardState keyboardStateNew = Keyboard.GetState();

            bool state;
            if (keyboardStateNew.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key))
            {
                state = true;
            }
            else
            {
                state = false;
            }

            _previousKeyboardState = keyboardStateNew;
            return state;
        }

        public override void Initialize()
        {
            _previousKeyboardState = Keyboard.GetState();
        }

        //public override void Update(GameTime gameTime)
        //{

        //}
    }
}
