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
        private bool PlayerAlive = true;
        private bool MonsterAlive = true;
        private SpriteFont font, titleFont;
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
            titleFont = gameRef.Content.Load<SpriteFont>("titleFont");
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
            actionManager.SetAction(() => MonsterAnimation());
            actionManager.SetAction(() => MonsterAttack());
            actionManager.SetAction(() => CheckPlayerHealth());

            st = "Que comece a batalha!";
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

                    if (listIndex > 6)
                        listIndex = 0;

                    if (PlayerAlive && MonsterAlive)
                    {
                        listIndex += 1;
                        actionManager.InvokeAction(listIndex);
                    }
                    else
                    {
                        st = "Fim do jogo.";
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

            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.name, new Vector2(280, 200), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.health.ToString(), new Vector2(280, 220), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(280, 240, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(280, 240, (int)(((float)gameRef.mainPlayer.health / (float)gameRef.mainPlayer.maxHealth) * 120.0f), 5), Color.Green);

            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.name, new Vector2(80, 50), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.health.ToString(), new Vector2(80, 70), Color.Black);

            gameRef.SpriteBatch.Draw(gameRef.playerTex, new Vector2(15, 150), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Vector2(340, 15), Color.White);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(80, 90, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(80, 90, (int)(((float)gameRef.genericMonster.health / (float)gameRef.genericMonster.maxHealth) * 120.0f), 5), Color.Green);


            if (gameRef.animationIsPlaying == false)
            {
                gameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(15, 260), Color.White);
                gameRef.SpriteBatch.DrawString(font, baseText, new Vector2(50, 290), Color.Black);

                //GameRef.SpriteBatch.DrawString(font, dialogueText, new Vector2(200, 400), Color.White);
            }

            //########## testing #####################################################



            if (gameRef.playPlayerAnim || gameRef.playMonsterAnim)
            {

                gameRef.numf += (float)gameTime.ElapsedGameTime.TotalSeconds * speed * 35;

                Rectangle rec;
                if (gameRef.playPlayerAnim)
                {
                    rec = new Rectangle(300, 40, 150, 150);
                }
                else
                {
                    rec = new Rectangle(20, 200, 150, 150);
                }

                gameRef._spriteBatch.Draw(
                gameRef.animController.currentAnimation.sprite,
                rec,
                gameRef.animController.currentAnimation.ListaRetangulos[gameRef.animController.currentAnimation.FrameAtual],
                Color.White);



                if (gameRef.numf > 250.0f)
                {
                    Console.WriteLine("Reseting gameRef.numf " + gameRef.numf);
                    gameRef.playPlayerAnim = false;
                    gameRef.playMonsterAnim = false;
                    gameRef.numf = 0.0f;
                    gameRef.animationIsPlaying = false;
                }
            }

            if (gameRef.numf > 0)
                Console.WriteLine(gameRef.numf);

            //############################################################################

            gameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void InitialText()
        {
            st = "Que comece a batalha!";
            NextLineMethod(st);
        }

        public void ShowChoiceMenu()
        {
            StateManager stateManager = (StateManager)gameRef.Services.GetService(typeof(IStateManager));
            stateManager.PushState(gameRef.ChoiceState);
            
            //I CAN RETRIVE THE SERVICE OR USE THE GAMEREF ONE (Cynthia that the said last one is better, no GC)

            //GameRef.stateManager.PushState(GameRef.ChoiceState);
        }

        public void PlayerAnimation()
        {
            gameRef.playPlayerAnim = true;
            gameRef.animationIsPlaying = true;
            listIndex += 1;
            actionManager.InvokeAction(listIndex);
            gameRef.animController.SetAnimation(AnimationKey.Explosion);
            gameRef.animController.PlayAnimation(AnimationKey.Explosion);
        }

        public void MonsterAnimation()
        {
            gameRef.playMonsterAnim = true;
            gameRef.animationIsPlaying = true;

            gameRef.animController.SetAnimation(AnimationKey.Explosion);
            gameRef.animController.PlayAnimation(AnimationKey.Explosion);

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
                st = "O oponente se prepara para atacar.";
                NextLineMethod(st);

            }
            else
            {
                st = "O oponente desmaiou.";
                NextLineMethod(st);
                //dialogueText = "Monster died.";
                MonsterAlive = false;
            }
        }

        public void MonsterAttack()
        {
            st = "Oponente te ataca causando " + gameRef.genericMonster.power + " pontos de dano.";
            NextLineMethod(st);
            gameRef.mainPlayer.health -= gameRef.genericMonster.power;
        }

        public void CheckPlayerHealth()
        {
            if (gameRef.mainPlayer.health < 1)
            {
                st = "Seu Polimon desmaiou, o oponente venceu.";
                NextLineMethod(st);
                PlayerAlive = false;
            }
            else
            {
                st = "Você tem " + gameRef.mainPlayer.health.ToString() + " pontos de vida.";
                NextLineMethod(st);
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
