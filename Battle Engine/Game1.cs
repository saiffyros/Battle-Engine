using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Engine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public static SpriteFont font;

        public static Texture2D dialogueBox;
        public Maneuver Attack;
        public Maneuver Wait;

        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;

        private Player MainPlayer;
        public Player mainPlayer { get { return MainPlayer; } }

        private Monster GenericMonster;
        public Monster genericMonster { get { return GenericMonster; } }

        private StateManager _stateManager;
        public StateManager stateManager { get { return _stateManager; } }
        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        public InputSystem inputSystem;
        public PlayerChoiceScreen ChoiceState { get; private set; }
        public GamePlayState gamePlayState { get; private set; }
        public Texture2D explosionTex, light;
        public Animation explosion, lightAnim;
        public AnimationController animController;
        public bool playPlayerAnim = false;
        public bool playMonsterAnim = false;
        public float numf = 0;
        public bool animationIsPlaying = false;
        public AnimationKey currentAnimation;
        public Texture2D playerTex, monsterTex, backgroundBattle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _stateManager = new StateManager(this);
            Components.Add(_stateManager);
            gamePlayState = new GamePlayState(this);
            ChoiceState = new PlayerChoiceScreen(this);
            inputSystem = new InputSystem(this);
            _stateManager.PushState(gamePlayState);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 450;
            _graphics.PreferredBackBufferHeight = 680;
            _graphics.ApplyChanges();

            dialogueBox = new Texture2D(GraphicsDevice, 10, 10);
            previousKeyboardState = Keyboard.GetState();
            playerTex = Content.Load<Texture2D>("playerTex");
            monsterTex = Content.Load<Texture2D>("monsterTex");
            backgroundBattle = Content.Load<Texture2D>("backgroundBattle");

            animController = new AnimationController(this);
            //Components.Add(animController);
            explosionTex = Content.Load<Texture2D>("explosion");
            light = Content.Load<Texture2D>("light7");
            lightAnim = new Animation(light, 192, 192, 70, false);
            explosion = new Animation(explosionTex, 192, 192, 50, false);
            animController.AddAnimation(AnimationKey.Explosion, explosion);
            animController.AddAnimation(AnimationKey.Light, lightAnim);

            base.Initialize();
        }

        public void AttackMethod()
        {
            //GenericMonster.health -= MainPlayer.power;
            gamePlayState.st = "Você ataca o oponente com " + MainPlayer.weapon + " causando \n" + MainPlayer.power + " pontos de dano.";
            gamePlayState.NextLineMethod(gamePlayState.st);

        }

        public void WaitMethod()
        {
            gamePlayState.st = "Você problematiza um assunto qualquer \n sem efeito algum.";
            gamePlayState.NextLineMethod(gamePlayState.st);
        }

        protected override void LoadContent()
        {
            Attack = new Maneuver("Tapa Frouxo", "Um tapa frouxo", 50, AttackMethod, AnimationKey.Explosion);
            Wait = new Maneuver("Problematizar", "Problematizar um ataque", 0, WaitMethod, AnimationKey.Light);

            MainPlayer = new Player("Hillary", 100, 50, "tapa", 20, 2, 100);

            

            MainPlayer.listManeuvers.Add(Attack);
            MainPlayer.listManeuvers.Add(Wait);

            GenericMonster = new Monster("Trump", 150, 30, 2, 150);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            dialogueBox = Content.Load<Texture2D>("dialogueBar");
            Components.Add(inputSystem);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            animController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //_spriteBatch.Begin();

            //foreach (string text in _messages)
            //{
            //    _spriteBatch.DrawString(font, text, new Vector2(50, 200), Color.White);
            //}

          

            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
