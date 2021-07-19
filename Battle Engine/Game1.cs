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
        private SpriteBatch _spriteBatch;

        //public Employee test;
        public static SpriteFont font;
        //private Player PlayerTest;
        //private Monster MonsterTest;
        public static Texture2D dialogueBox;
        public Maneuver Attack;
        public Maneuver Wait;

        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;

        private Player MainPlayer;
        public Player mainPlayer { get { return MainPlayer; } }

        private Monster GenericMonster;
        public Monster genericMonster { get { return GenericMonster; } }

        private Queue<string> _messages = new Queue<string>();
        public Queue<string> messages { get { return _messages; } }

        private StateManager _stateManager;
        public StateManager stateManager { get { return _stateManager; } }
        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        public InputSystem inputSystem;
        public PlayerChoiceScreen ChoiceState { get; private set; }
        public GamePlayState gamePlayState { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _stateManager = new StateManager(this);
            //Components.Add(_stateManager);
            gamePlayState = new GamePlayState(this);
            ChoiceState = new PlayerChoiceScreen(this);
            inputSystem = new InputSystem(this);
            _stateManager.PushState(gamePlayState);
        }

        protected override void Initialize()
        {
            //test = new Employee();
            dialogueBox = new Texture2D(GraphicsDevice, 10, 10);
            previousKeyboardState = Keyboard.GetState();

            base.Initialize();
        }

        public void AttackMethod()
        {
            _stateManager.ChangeState(gamePlayState);
            GenericMonster.health -= MainPlayer.power;
            gamePlayState.dialogueText = "You attack the enemy with " + MainPlayer.weapon + " causing " + MainPlayer.power + " damage points.";
        }

        public void WaitMethod()
        {
            //gamePlayState.dialogueText = "You skipped your turn"; //not working
            _stateManager.ChangeState(gamePlayState);   //2 clicks problem
        }

        protected override void LoadContent()
        {
            //test = new Employee() { FirstName = "Jonas", LastName = "Oliveira" };
            //Serializer.Serialize("testXML3", test);

            Attack = new Maneuver("Attack", "A simple attack", 50, AttackMethod);
            Wait = new Maneuver("Wait", "Skipping the turn", 0, WaitMethod);

            MainPlayer = new Player("Player", 100, 50, "fist", 20, 2);

            MainPlayer.listManeuvers.Add(Attack);
            MainPlayer.listManeuvers.Add(Wait);
            //Serializer.Serialize("player", PlayerTest);
            //Serializer.Serialize("monster", MonsterTest);

            //MainPlayer = Content.Load<Player>("player");
            //GenericMonster = Content.Load<Monster>("monster");
            GenericMonster = new Monster("Monster", 150, 30, 2);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            dialogueBox = Content.Load<Texture2D>("dialogueBox");
            Components.Add(inputSystem);
            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (string text in _messages)
            {
                _spriteBatch.DrawString(font, text, new Vector2(50, 200), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
