using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Engine
{
    public class GamePlayState : GameState
    {
        public static int listIndex = 0;
        public static ActionManager actionManager;
        public string dialogueText = "Let's Fight!";
        private bool PlayerAlive = true;
        private bool MonsterAlive = true;
        private SpriteFont font;
        private Rectangle windowArea = new Rectangle(0, 0, 800, 600);
        private Rectangle mouseRectangle = new Rectangle(0, 0, 1, 1);
        MouseState previousMouseState;
        Texture2D _pixel;
        private bool done = false;
        private int index;
        private string baseText = "";
        private char[] ch = new char[1];
        
        private float cronometro = 0;
        private float letterTime = 0.05f;
        private float speed = 5f;
        public string st = "";
        private int sentenceSize = 0;

        public GamePlayState(Game game) : base(game)
        {
            
        } //add enemy as a parameter

        public override void Initialize()
        {
            previousMouseState = Mouse.GetState();
            
            actionManager = new ActionManager();
            font = gameRef.Content.Load<SpriteFont>("font");
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            _pixel.SetData(data);
            ch[0] = ' ';
            actionManager.SetAction(() => InitialText());
            actionManager.SetAction(() => ShowChoiceMenu());
            actionManager.SetAction(() => PlayerAnimation());
            actionManager.SetAction(() => PlayerAction());
            actionManager.SetAction(() => CheckMonsterHealth());
            actionManager.SetAction(() => MonsterAttack());
            actionManager.SetAction(() => CheckPlayerHealth());

            st = "Let's fight";
            NextLineMethod(st);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            mouseRectangle.X = mouseState.X;
            mouseRectangle.Y = mouseState.Y;
            //if (windowArea.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1)))
            if (windowArea.Intersects(mouseRectangle) && gameRef.animationIsPlaying == false && done == true) { 
                if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    Console.WriteLine(listIndex);

                    if (listIndex > 5)
                        listIndex = 0;

                    if (PlayerAlive && MonsterAlive)
                    {
                        listIndex += 1;
                        actionManager.InvokeAction(listIndex);
                    }
                    else
                    {
                        st = "Game is over";
                        NextLineMethod(st);
                        //dialogueText = "Game is over";
                        gameRef.Exit();
                    }
                }
            }
        
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

            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.GraphicsDevice.Clear(Color.CadetBlue);

            gameRef.SpriteBatch.Begin();

            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.name, new Vector2(50, 50), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.health.ToString(), new Vector2(50, 70), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(50, 90, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(50, 90, (int)(((float)gameRef.mainPlayer.health / (float)gameRef.mainPlayer.maxHealth) * 120.0f), 5), Color.Green);

            gameRef.SpriteBatch.Draw(gameRef.playerTex, new Vector2(15, 150), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Vector2(540, 15), Color.White);

            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.name, new Vector2(620, 50), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.health.ToString(), new Vector2(620, 70), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(620, 90, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(620, 90, (int)(((float)gameRef.genericMonster.health / (float)gameRef.genericMonster.maxHealth) * 120.0f), 5), Color.Green);



            if (gameRef.animationIsPlaying == false)
            {
                gameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(0, 300), Color.White);
                gameRef.SpriteBatch.DrawString(font, baseText, new Vector2(200, 400), Color.White);

                //GameRef.SpriteBatch.DrawString(font, dialogueText, new Vector2(200, 400), Color.White);
            }



            //foreach (string text in GameRef.messages)
            //{
            //    GameRef.SpriteBatch.DrawString(font, text, new Vector2(200, 450), Color.White);
            //}

            //########## testing #####################################################

            if (gameRef.playAnim)
            {
                //receive animation to play
                //GameRef.animationIsPlaying = true;
                gameRef.numf += (float)gameTime.TotalGameTime.TotalSeconds;
                gameRef._spriteBatch.Draw(
                    gameRef.currentAnimation.sprite, 
                    new Rectangle(200, 200, 150, 150),
                    gameRef.currentAnimation.ListaRetangulos[gameRef.currentAnimation.FrameAtual], 
                    Color.White);

                if (gameRef.numf > 500.0f)
                {
                    gameRef.playAnim = false;
                    gameRef.numf = 0.0f;
                    gameRef.animationIsPlaying = false;
                }
            }

            //############################################################################

            gameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void InitialText()
        {
            st = "Let's fight!!";
            NextLineMethod(st);
            dialogueText = "Let's fight!!";
        }

        public void ShowChoiceMenu()
        {
            //dialogueText = "Checking action!";

            StateManager stateManager = (StateManager)gameRef.Services.GetService(typeof(IStateManager));
            stateManager.PushState(gameRef.ChoiceState);
            
            //I CAN RETRIVE THE SERVICE OR USE THE GAMEREF ONE (Cynthia that the said last one is better, no GC)

            //GameRef.stateManager.PushState(GameRef.ChoiceState);
        }

        public void PlayerAnimation()
        {
            gameRef.playAnim = true;
            gameRef.animationIsPlaying = true;
            listIndex += 1;
            actionManager.InvokeAction(listIndex);
        }

        public void PlayerAction()
        {
            Maneuver action = gameRef.ChoiceState.selectedManeuver;
            action.ManeuverAction.Invoke(); //null check?
        }

        public void CheckMonsterHealth()
        {
            if (gameRef.genericMonster.health > 0)
            {
                st = "Monster prepares to attack";
                NextLineMethod(st);
                //dialogueText = "Monster prepares to attack";
            }
            else
            {
                st = "Monster died.";
                NextLineMethod(st);
                //dialogueText = "Monster died.";
                MonsterAlive = false;
            }
        }

        public void MonsterAttack()
        {
            st = "Monster attack you causing " + gameRef.genericMonster.power + " damage points.";
            NextLineMethod(st);
            //sentenceSize = st.Length;

            gameRef.mainPlayer.health -= gameRef.genericMonster.power;

            //dialogueText = "Monster attack you causing " + GameRef.genericMonster.power + " damage points.";
        }

        public void CheckPlayerHealth()
        {
            if (gameRef.mainPlayer.health < 1)
            {
                st = "You died. Enemy won.";
                NextLineMethod(st);
                //dialogueText = "You died. Enemy won.";
                PlayerAlive = false;
            }
            else
            {
                st = "You have " + gameRef.mainPlayer.health.ToString() + " health points.";
                NextLineMethod(st);
                //dialogueText = "You have " + GameRef.mainPlayer.health.ToString() + " health points.";
            }
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
    }
}
