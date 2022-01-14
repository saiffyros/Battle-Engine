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
        private SpriteFont font, titleFont, smallFont;
        private Rectangle windowArea = new Rectangle(0, 0, 800, 600);
        private Rectangle mouseRectangle = new Rectangle(0, 0, 1, 1);
        MouseState previousMouseState;
        Texture2D _pixel;
        //private float speed = 5f;
        public string st = "";
        private int damage = 0;
        private float cronometroBar = 0;
        private bool barAnimation = false;
        private bool barAnimation2 = false;
        int numm = 0;
        bool monsterBlink = false;
        float monsterBlinkTimer = 0.0f;
        float monsterBlinkEnd = 0.0f;
        public DrawTextSlow drawTextSlow;
        public Maneuver SelectedManeuver;


        public GamePlayState(Game game) : base(game)
        {
            
        } //add enemy as a parameter

        public override void Initialize()
        {
            previousMouseState = Mouse.GetState();
            drawTextSlow = new DrawTextSlow(gameRef);

            actionManager = new ActionManager();
            font = gameRef.Content.Load<SpriteFont>("font");
            titleFont = gameRef.Content.Load<SpriteFont>("titleFont");
            smallFont = gameRef.Content.Load<SpriteFont>("smallFont");
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            _pixel.SetData(data);
            actionManager.SetAction(() => InitialText());
            actionManager.SetAction(() => ShowChoiceMenu());
            actionManager.SetAction(() => PlayerAnimation());
            actionManager.SetAction(() => MonsterBlink());
            actionManager.SetAction(() => PlayerLifeBar());
            actionManager.SetAction(() => PlayerAction());
            actionManager.SetAction(() => CheckMonsterHealth());
            actionManager.SetAction(() => MonsterAnimation());
            actionManager.SetAction(() => MonsterLifeBar());
            actionManager.SetAction(() => MonsterAttack());
            actionManager.SetAction(() => CheckPlayerHealth());

            st = "Que comece a batalha!";
            drawTextSlow.baseText = "Que comece a Batalha";
            drawTextSlow.NextLineMethod(st);
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
            if (windowArea.Intersects(mouseRectangle) && gameRef.animationIsPlaying == false && drawTextSlow.done == true) { 
                if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    Console.WriteLine(listIndex);

                    if (listIndex > 9)
                        listIndex = 0;

                    if (PlayerAlive && MonsterAlive)
                    {
                        listIndex += 1;
                        actionManager.InvokeAction(listIndex);
                    }
                    else
                    {
                        st = "Fim do jogo.";
                        drawTextSlow.baseText = "Fim do jogo";
                        drawTextSlow.NextLineMethod(st);
                        gameRef.Exit();
                    }
                }
            }

            if (barAnimation2)
            {
                gameRef.animationIsPlaying = true;
                cronometroBar += (float)gameTime.ElapsedGameTime.TotalSeconds * 50f;

                if (cronometroBar > 3.0f)
                {
                    gameRef.genericMonster.health -= 1;
                    numm += 1;
                    cronometroBar = 0;
                }

                if (numm >= damage)
                {
                    barAnimation2 = false;
                    gameRef.animationIsPlaying = false;
                    numm = 0;
                    listIndex += 1;
                    actionManager.InvokeAction(listIndex);
                    Console.WriteLine("done " + cronometroBar);
                }
            }

            if (barAnimation)
            {
                gameRef.animationIsPlaying = true;
                cronometroBar += (float)gameTime.ElapsedGameTime.TotalSeconds * 50f;

                if (cronometroBar > 3.0f)
                {
                    gameRef.mainPlayer.health -= 1;
                    numm += 1;
                    cronometroBar = 0;
                }

                if (numm >= damage)
                {
                    barAnimation = false;
                    gameRef.animationIsPlaying = false;
                    numm = 0;
                    listIndex += 1;
                    actionManager.InvokeAction(listIndex);
                    Console.WriteLine("done " + cronometroBar);
                }
            }

            drawTextSlow.Update(gameTime);

            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.GraphicsDevice.Clear(Color.White);

            gameRef.SpriteBatch.Begin();

            gameRef.SpriteBatch.Draw(gameRef.backgroundBattle, new Rectangle(0, 0, 450, 340), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.backgroundBattleBottom, new Rectangle(0, 340, 450, 340), Color.White);

            gameRef.SpriteBatch.Draw(gameRef.battlepadEnemy, new Rectangle(245, 165, 200, 50), Color.White);

            gameRef.SpriteBatch.Draw(gameRef.playerTex, new Rectangle(25, 75, 192, 192), Color.White);

            if (monsterBlink)
            {
                monsterBlinkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                monsterBlinkEnd += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (monsterBlinkTimer < 0.2f)
                {

                }
                else
                {
                    gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Rectangle(250, 0, 192, 192), Color.White);
                }

                if (monsterBlinkTimer > 0.3f)
                {
                    monsterBlinkTimer = 0.0f;
                }
                if(monsterBlinkEnd > 0.9)
                {
                    monsterBlink = false;
                    monsterBlinkTimer = 0.0f;
                    monsterBlinkEnd = 0.0f;
                    listIndex += 1;
                    actionManager.InvokeAction(listIndex);
                }
            }
            else
            {
                gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Rectangle(250, 0, 192, 192), Color.White);
            }
            gameRef.SpriteBatch.Draw(gameRef.playerBar, new Rectangle(250, 195, 200, 62), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.enemyBar, new Rectangle(0, 30, 200, 52), Color.White);

            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(0, 257, 450, 85), Color.Black);

            gameRef.SpriteBatch.DrawString(titleFont, gameRef.mainPlayer.name, new Vector2(275, 200), Color.Black);
            gameRef.SpriteBatch.DrawString(smallFont, gameRef.mainPlayer.maxHealth + "     " + gameRef.mainPlayer.health.ToString(), new Vector2(370, 235), Color.Black);
            gameRef.SpriteBatch.DrawString(smallFont, gameRef.mainPlayer.level.ToString(), new Vector2(415, 205), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(352, 226, (int)(((float)gameRef.mainPlayer.health / (float)gameRef.mainPlayer.maxHealth) * 80.0f), 5), Color.GreenYellow);

            gameRef.SpriteBatch.DrawString(titleFont, gameRef.genericMonster.name, new Vector2(8, 40), Color.Black);
            gameRef.SpriteBatch.DrawString(smallFont, gameRef.genericMonster.level.ToString(), new Vector2(145, 43), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(83, 66, (int)(((float)gameRef.genericMonster.health / (float)gameRef.genericMonster.maxHealth) * 80.0f), 5), Color.GreenYellow);

            if (gameRef.playPlayerAnim || gameRef.playMonsterAnim)
            {
                gameRef.numf += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //or totalseconds

                Rectangle rec;
                if (gameRef.playPlayerAnim)
                {

                    rec = gameRef.animController.ListaAnimations[SelectedManeuver.ManeuverAnimation].Position;
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

                float dur = (gameRef.animController.ListaAnimations[SelectedManeuver.ManeuverAnimation].NumeroAnimations *
                    gameRef.animController.ListaAnimations[SelectedManeuver.ManeuverAnimation].Duration) + 500;
                //if (gameRef.numf > 250.0f)
                
                if (gameRef.numf > dur)
                {
                    Console.WriteLine("Reseting gameRef.numf " + gameRef.numf);
                    gameRef.playPlayerAnim = false;
                    gameRef.playMonsterAnim = false;
                    gameRef.numf = 0.0f;
                    listIndex += 1;
                    actionManager.InvokeAction(listIndex);
                }
            }

            if (gameRef.numf > 0)
                Console.WriteLine(gameRef.numf);

            //############################################################################

            gameRef.SpriteBatch.End();

            drawTextSlow.Draw(gameRef.SpriteBatch);


            base.Draw(gameTime);
        }

        public void InitialText()
        {
            st = "Que comece a batalha!";
            drawTextSlow.NextLineMethod(st);
        }

        public void ShowChoiceMenu()
        {
            gameRef.stateManager.PushState(gameRef.ChoiceState);
        }

        public void PlayerAnimation()
        {
            gameRef.playPlayerAnim = true;
            gameRef.animationIsPlaying = true;

            Maneuver action = SelectedManeuver;
            gameRef.animController.SetAnimation(action.ManeuverAnimation);
            gameRef.animController.PlayAnimation(action.ManeuverAnimation);
        }

        public void MonsterAnimation()
        {
            gameRef.playMonsterAnim = true;
            gameRef.animationIsPlaying = true;

            gameRef.animController.SetAnimation(AnimationKey.Explosion);
            gameRef.animController.PlayAnimation(AnimationKey.Explosion);
        }

        public void MonsterLifeBar()
        {
            damage = gameRef.genericMonster.power; //change error damage
            barAnimation = true;
        }

        public void PlayerLifeBar()
        {
            damage = SelectedManeuver.Damage;
            barAnimation2 = true;
        }

        public void PlayerAction()
        {
            Maneuver action = SelectedManeuver;
            action.ManeuverAction.Invoke(); //null check?
        }

        public void CheckMonsterHealth()
        {
            if (gameRef.genericMonster.health > 0)
            {
                st = "O oponente se prepara para atacar.";
                drawTextSlow.NextLineMethod(st);
            }
            else
            {
                st = "O oponente desmaiou.";
                drawTextSlow.NextLineMethod(st);
                MonsterAlive = false;
            }
        }

        public void MonsterAttack()
        {
            st = "Oponente te ataca causando " + gameRef.genericMonster.power + " pontos de dano.";
            drawTextSlow.NextLineMethod(st);
        }

        public void CheckPlayerHealth()
        {
            if (gameRef.mainPlayer.health < 1)
            {
                st = gameRef.mainPlayer.name + " desmaiou, o oponente venceu.";
                drawTextSlow.NextLineMethod(st);
                PlayerAlive = false;
            }
            else
            {
                st = "Você tem " + gameRef.mainPlayer.health.ToString() + " pontos de vida.";
                drawTextSlow.NextLineMethod(st);
            }
        }

        public void MonsterBlink()
        {
            monsterBlink = true;
            gameRef.animationIsPlaying = true;
        }
    }
}
