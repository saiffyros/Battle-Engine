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
        private SpriteFont font;
        private Maneuver SelectedManeuver;
        private Texture2D buttonTexture;
        Texture2D _pixel;

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
            buttonTexture = gameRef.Content.Load<Texture2D>("Button");

            var label = new Label(gameRef, "Choose an action!", font, new Vector2(300, 5));
            LocalComponents.Add(label);

            foreach (Maneuver maneuver in gameRef.mainPlayer.listManeuvers)
            {
                var AttackBtn = new Button(gameRef, buttonTexture, font)
                {
                    Position = new Vector2((gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) + 1) * 150, 200),
                    Text = maneuver.Name,
                };

                AttackBtn.Click += delegate { SelectedManeuver = maneuver; BackToPlayState(AttackBtn); };

                LocalComponents.Add(AttackBtn); // GameState.LocalComponents, not Game.Components
            }

            base.LoadContent();
        }

        public void BackToPlayState(Button button) //can be improved. A callback maybe?
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
            //foreach (var component in _gameComponents)
            //{
            //    component.Update(gameTime);
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.GraphicsDevice.Clear(Color.CadetBlue);
            gameRef.SpriteBatch.Begin();

            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.name, new Vector2(50, 50), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.mainPlayer.health.ToString(), new Vector2(50, 70), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(50, 90, 120, 5), Color.LightGray); ;
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(50, 90, (int)(((float)gameRef.mainPlayer.health / (float)gameRef.mainPlayer.maxHealth) * 120.0f), 5), Color.Green);

            gameRef.SpriteBatch.Draw(gameRef.playerTex, new Vector2(15, 150), Color.White);
            gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Vector2(540, 15), Color.White);

            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.name, new Vector2(620, 50), Color.Black);
            gameRef.SpriteBatch.DrawString(font, gameRef.genericMonster.health.ToString(), new Vector2(620, 70), Color.Black);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(620, 90, 120, 5), Color.LightGray);
            gameRef.SpriteBatch.Draw(_pixel, new Rectangle(620, 90, (int)(((float)gameRef.genericMonster.health / (float)gameRef.genericMonster.maxHealth) * 120.0f), 5), Color.Green);

            //foreach (var component in _gameComponents)
            //{
            //    component.Draw(gameTime, GameRef.SpriteBatch);
            //}

            gameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
