using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Engine
{
    public class Button
    {
        Game1 gameRef;
        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle;
        //public bool _inWorld = false;

        public string Text { get; set; }

        public bool _active = true;

        public Button(Game game, string buttonText, Vector2 position)
        {
            this.gameRef = (Game1)game;
            PenColour = Color.Black;
            //_font = gameRef.font;
            _font = gameRef.Content.Load<SpriteFont>("buttonFont");
            _texture = gameRef.Content.Load<Texture2D>("Button");
            Text = buttonText;
            Position = position;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)_font.MeasureString(Text).X + 15, (int)_font.MeasureString(Text).Y + 6);
        }

        public Button(Game game, string buttonText, Vector2 position, string texture)
        {
            this.gameRef = (Game1)game;
            PenColour = Color.Black;
            //_font = gameRef.font;
            _font = gameRef.Content.Load<SpriteFont>("buttonFont");
            _texture = gameRef.Content.Load<Texture2D>(texture);
            Text = buttonText;
            Position = position;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle;

                mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle) && _active == true)
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    //Click?.Invoke(this, new EventArgs());
                    Click?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Draw(GameTime gameTime, Matrix? transform)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            if (_active == true)
            {

                gameRef._spriteBatch.Begin(transformMatrix: transform);
                gameRef._spriteBatch.Draw(_texture, Rectangle, colour);
                gameRef._spriteBatch.End();

                if (!string.IsNullOrEmpty(Text))
                {
                    var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                    var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                    gameRef._spriteBatch.Begin(transformMatrix: transform);
                    gameRef._spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
                    gameRef._spriteBatch.End();
                }
            }
        }
    }
}

