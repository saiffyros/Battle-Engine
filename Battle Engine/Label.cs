using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Engine
{
    public class Label : DrawableGameComponent
    {
        Game1 gameRef;
        private SpriteFont _font;
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        //public Rectangle Rectangle
        //{
        //    get
        //    {
        //        return new Rectangle((int)Position.X, (int)Position.Y, Text.Length, 10);
        //    }
        //}


        public Label(Game game, string text, SpriteFont font, Vector2 position) : base(game)
        {
            this.gameRef = (Game1)game;
            Text = text;
            _font = font;
            PenColour = Color.Black;
            Position = position;

        }

        public override void Draw(GameTime gameTime)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                //var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                //var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                gameRef.SpriteBatch.Begin();
                gameRef.SpriteBatch.DrawString(_font, Text, Position, PenColour);
                gameRef.SpriteBatch.End();
            }

        }
    }
}

