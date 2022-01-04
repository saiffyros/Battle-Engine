using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Battle_Engine
{
    public class PlayerChoiceScreen : GameState
    {
        private SpriteFont font, titleFont;
        private Maneuver SelectedManeuver;
        private Texture2D buttonTexture;
        Texture2D _pixel;
        private List<Button> buttonList = new List<Button>();

        public Maneuver selectedManeuver { get { return SelectedManeuver; } }

        public PlayerChoiceScreen(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            _pixel.SetData(data);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = gameRef.Content.Load<SpriteFont>("font");
            titleFont = gameRef.Content.Load<SpriteFont>("titleFont");
            buttonTexture = gameRef.Content.Load<Texture2D>("Button");

            var label = new Label(gameRef, "Escolha uma ação!", titleFont, new Vector2(150, 5));
            LocalComponents.Add(label);

            foreach (Maneuver maneuver in gameRef.mainPlayer.listManeuvers)
            {
                Vector2 posBtn;
                if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 0)
                {
                    posBtn = new Vector2(50, 400);
                }
                else if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 1)
                {
                    posBtn = new Vector2(250, 400);
                }
                else if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 2)
                {
                    posBtn = new Vector2(50, 480);
                }
                else
                {
                    posBtn = new Vector2(250, 480);
                }

                Button AttackBtn = new Button(gameRef, maneuver.Name, posBtn, "buttonTexture");

                AttackBtn.Click += delegate { SelectedManeuver = maneuver; BackToPlayState(); };
                buttonList.Add(AttackBtn);
            }

            Button cancelBtn = new Button(gameRef, "Voltar", new Vector2(50, 600), "cancelBtn");
            //MUDAR!!!!!!!!!!!!!
            cancelBtn.Click += delegate { SelectedManeuver = gameRef.mainPlayer.listManeuvers[1]; BackToPlayState(); }; 
            buttonList.Add(cancelBtn);

            base.LoadContent();
        }

        public void BackToPlayState() //can be improved. A callback maybe?
        {
            //foreach (Maneuver maneuver in GameRef.mainPlayer.listManeuvers)
            //{
            //    if (maneuver.Name == button.Text)
            //    {
            //        SelectedManeuver = maneuver;
            //    }
            //}

            gameRef.currentAnimation = selectedManeuver.ManeuverAnimation;
            gameRef.stateManager.ChangeState(gameRef.gamePlayState);
            GamePlayState.listIndex += 1;
            Console.WriteLine("From choice " + GamePlayState.listIndex);
            GamePlayState.actionManager.InvokeAction(GamePlayState.listIndex);

        }

        public override void Update(GameTime gameTime)
        {
            foreach(Button button in buttonList)
            {
                button.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.GraphicsDevice.Clear(Color.CadetBlue);
            gameRef.SpriteBatch.Begin();

            gameRef.SpriteBatch.DrawString(titleFont, gameRef.mainPlayer.name, new Vector2(280, 195), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.maxHealth + " / " + gameRef.mainPlayer.health.ToString(), new Vector2(280, 220), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(280, 240, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(280, 240, (int)(((float)gameRef.mainPlayer.health / (float)gameRef.mainPlayer.maxHealth) * 120.0f), 5), Color.Green);

            gameRef.SpriteBatch.DrawString(titleFont, gameRef.genericMonster.name, new Vector2(80, 45), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.maxHealth + " / " + gameRef.genericMonster.health.ToString(), new Vector2(80, 70), Color.Black);

            gameRef.SpriteBatch.Draw(gameRef.playerTex, new Vector2(15, 150), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Vector2(340, 15), Color.White);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(80, 90, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(80, 90, (int)(((float)gameRef.genericMonster.health / (float)gameRef.genericMonster.maxHealth) * 120.0f), 5), Color.Green);

            gameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(15, 260), Color.White);
            gameRef.SpriteBatch.DrawString(font, "O que " + gameRef.mainPlayer.name + " vai fazer?", new Vector2(50, 295), Color.Black);


            gameRef.SpriteBatch.End();


            gameRef.SpriteBatch.Begin();

            gameRef.SpriteBatch.Draw(gameRef.backgroundBattle, new Rectangle(0, 340, 450, 340), Color.White);
            gameRef.SpriteBatch.End();

            foreach (Button b in buttonList)
            {
                b.Draw(gameTime, null);
            }

            base.Draw(gameTime);
        }
    }
}
