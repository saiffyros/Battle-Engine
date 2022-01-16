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
        private Texture2D buttonTexture;
        Texture2D _pixel;
        private List<Button> buttonList = new List<Button>();
        float timer;

        public PlayerChoiceScreen(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            _pixel.SetData(data);
            timer = 0.0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = gameRef.Content.Load<SpriteFont>("buttonFont");
            titleFont = gameRef.Content.Load<SpriteFont>("titleFont");
            buttonTexture = gameRef.Content.Load<Texture2D>("Button");

            //var label = new Label(gameRef, "Escolha uma ação!", titleFont, new Vector2(150, 5));
            //LocalComponents.Add(label);

            foreach (Maneuver maneuver in gameRef.mainPlayer.listManeuvers)
            {
                Vector2 posBtn;
                if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 0)
                {
                    posBtn = new Vector2(20, 395);
                }
                else if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 1)
                {
                    posBtn = new Vector2(240, 395);
                }
                else if (gameRef.mainPlayer.listManeuvers.IndexOf(maneuver) == 2)
                {
                    posBtn = new Vector2(20, 500);
                }
                else
                {
                    posBtn = new Vector2(240, 500);
                }

                Button AttackBtn = new Button(gameRef, maneuver.Name, posBtn, "buttonTexture");

                AttackBtn.Click += delegate { gameRef.gamePlayState.SelectedManeuver = maneuver; BackToPlayState(); };
                buttonList.Add(AttackBtn);
            }

            Button cancelBtn = new Button(gameRef, "Voltar", new Vector2(50, 600), "cancelBtn")
            {
                PenColour = Color.White,
            };
            //MUDAR!!!!!!!!!!!!!
            cancelBtn.Click += delegate { gameRef.gamePlayState.SelectedManeuver = gameRef.mainPlayer.listManeuvers[1]; BackToPlayState(); }; 
            buttonList.Add(cancelBtn);

            base.LoadContent();
        }

        public void BackToPlayState()
        {
            Console.WriteLine("Manuever chosen: " + gameRef.gamePlayState.SelectedManeuver.Description);

            gameRef.currentAnimation = gameRef.gamePlayState.SelectedManeuver.ManeuverAnimation;
            //gameRef.stateManager.ChangeState(gameRef.gamePlayState);
            gameRef.stateManager.PopState();
            ModuleManager.ActivateModule(ModuleKey.PlayerAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 0.7f)
            {
                foreach (Button button in buttonList)
                {
                    button.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // gameRef.GraphicsDevice.Clear(Color.CadetBlue);

            gameRef.SpriteBatch.Begin();

            //gameRef.SpriteBatch.Draw(gameRef.backgroundBattleBottom, new Rectangle(0, 340, 450, 340), Color.White);
            gameRef.SpriteBatch.End();

            if (timer > 0.7f)
            {
                foreach (Button b in buttonList)
                {
                    b.Draw(gameTime, null);
                }
            }

            base.Draw(gameTime);
        }
    }
}
