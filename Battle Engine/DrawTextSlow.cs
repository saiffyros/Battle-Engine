﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class DrawTextSlow : Module
    {
        Game1 gameRef;
        public float cronometro;
        public float letterTime = 0.05f;
        public float speed = 5.0f;
        public bool done = false;
        public string baseText = "";
        public char[] ch = new char[1];
        public int index = 0;
        public int sentenceSize;

        public DrawTextSlow(Game1 game)
        {
            gameRef = game;
            ch[0] = ' ';
            sentenceSize = 0;
        }


        public void NextLineMethod(string text)
        {
            sentenceSize = text.Length;
            done = false;
            index = 0;
            baseText = "";

            ch = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                ch[i] = text[i];
            }
        }

        public override void Update(GameTime gameTime)
        {
            cronometro += (float)gameTime.ElapsedGameTime.TotalSeconds * speed;

            if (cronometro > letterTime && done == false && gameRef.animationIsPlaying == false)
            {
                baseText += ch[index];
                cronometro = 0;
                index += 1;

                if (index >= sentenceSize)
                {
                    done = true;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (gameRef.animationIsPlaying == false)
            {
                gameRef.SpriteBatch.Begin();
                gameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(15, 255), Color.White);
                gameRef.SpriteBatch.DrawString(Game1.font, baseText, new Vector2(50, 280), Color.Black); ;
                gameRef.SpriteBatch.End();
            }

            base.Draw(spriteBatch);
        }
    }
}
