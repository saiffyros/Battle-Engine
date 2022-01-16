using Microsoft.Xna.Framework;
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
        public float letterTime = 0.08f;
        public float speed = 5.0f;
        public bool done = false;
        public string baseText = "";
        public char[] ch = new char[1];
        public int index = 0;
        public int sentenceSize;
        public bool showDialogue = true;
        public float delay = 0.0f;
        public bool endedWriting = false;

        public DrawTextSlow(Game1 game)
        {
            gameRef = game;
            ch[0] = ' ';
            sentenceSize = 0;
        }

        public void NextSentence(string text)
        {
            ResetWritter();
            sentenceSize = text.Length;

            ch = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                ch[i] = text[i];
            }
        }

        private void ResetWritter()
        {
            baseText = "";
            done = false;
            endedWriting = false;
            index = 0;
            delay = 0;
        }

        public override void Update(GameTime gameTime)
        {
            cronometro += (float)gameTime.ElapsedGameTime.TotalSeconds * speed;

            if (cronometro > letterTime && endedWriting == false)
            {
                baseText += ch[index];
                cronometro = 0;
                index += 1;

                if (index == sentenceSize)
                {
                    endedWriting = true;
                }
            }

            if (endedWriting)
            {
                delay += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (delay > 0.5f)
                {
                    done = true;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (showDialogue == true)
            {
                gameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(15, 255), Color.White);
                gameRef.SpriteBatch.DrawString(Game1.font, baseText, new Vector2(50, 280), Color.Black);

                if (done)
                {
                    gameRef.SpriteBatch.Draw(gameRef.clickIcon, new Vector2(415, 323), Color.White);

                }
            }

            base.Draw(spriteBatch);
        }
    }
}
