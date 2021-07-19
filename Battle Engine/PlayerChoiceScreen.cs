﻿using Microsoft.Xna.Framework;
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
        //private List<Button> _gameComponents = new List<Button>();
        private SpriteFont font;
        private Maneuver SelectedManeuver;
        private Texture2D buttonTexture;

        public Maneuver selectedManeuver { get { return SelectedManeuver; } }

        public PlayerChoiceScreen(Game game) : base(game)
        {

        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = GameRef.Content.Load<SpriteFont>("font");
            buttonTexture = GameRef.Content.Load<Texture2D>("Button");

            foreach (Maneuver maneuver in GameRef.mainPlayer.listManeuvers)
            {
                var AttackBtn = new Button(GameRef, buttonTexture, font)
                {
                    Position = new Vector2((GameRef.mainPlayer.listManeuvers.IndexOf(maneuver) + 1) * 150, 200),
                    Text = maneuver.Name,
                };

                AttackBtn.Click += delegate { BackToPlayState(AttackBtn); };

                LocalComponents.Add(AttackBtn); // GameState.Components, not Game.Components
            }

            base.LoadContent();
        }

        public void BackToPlayState(Button button)
        {
            foreach (Maneuver maneuver in GameRef.mainPlayer.listManeuvers)
            {
                if (maneuver.Name == button.Text)
                {
                    SelectedManeuver = maneuver;
                }
            }

            GameRef.stateManager.ChangeState(GameRef.gamePlayState);
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
            GameRef.GraphicsDevice.Clear(Color.CadetBlue);
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.DrawString(font, GameRef.mainPlayer.name, new Vector2(50, 50), Color.Black);
            GameRef.SpriteBatch.DrawString(font, GameRef.mainPlayer.health.ToString(), new Vector2(50, 70), Color.Black);

            GameRef.SpriteBatch.DrawString(font, GameRef.genericMonster.name, new Vector2(680, 50), Color.Black);
            GameRef.SpriteBatch.DrawString(font, GameRef.genericMonster.health.ToString(), new Vector2(680, 70), Color.Black);

            //foreach (var component in _gameComponents)
            //{
            //    component.Draw(gameTime, GameRef.SpriteBatch);
            //}

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
