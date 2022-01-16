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
        public Maneuver Attack, Mascara, Mandioca, Vacina;

        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;

        private Polimon MainPlayer;
        public Polimon mainPlayer { get { return MainPlayer; } }

        private Polimon GenericMonster;
        public Polimon genericMonster { get { return GenericMonster; } }

        private StateManager _stateManager;
        public StateManager stateManager { get { return _stateManager; } }
        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        public PlayerChoiceScreen ChoiceState { get; private set; }
        public GamePlayState gamePlayState { get; private set; }
        public Texture2D explosionTex, light, vacina, mandioca;
        public Animation explosion, lightAnim, vacinaAnim, mandiocaAnim, poopAnim, scratchAnim;
        public AnimationController animController;
        public bool playPlayerAnim = false;
        public bool playMonsterAnim = false;
        public float numf = 0;
        public bool animationIsPlaying = false;
        public AnimationKey currentAnimation;
        public Texture2D playerTex, monsterTex, backgroundBattleBottom, backgroundBattle;
        public Texture2D battlepadPlayer, battlepadEnemy, playerBar, enemyBar, logoPT;
        public Texture2D poop, scratch;
        public Texture2D clickIcon;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _stateManager = new StateManager(this);
            Components.Add(_stateManager);
            gamePlayState = new GamePlayState(this);
            ChoiceState = new PlayerChoiceScreen(this);
            //_stateManager.PushState(gamePlayState);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 450;
            _graphics.PreferredBackBufferHeight = 680;
            _graphics.ApplyChanges();

            previousKeyboardState = Keyboard.GetState();
            playerTex = Content.Load<Texture2D>("DilmaCostas");
            monsterTex = Content.Load<Texture2D>("JanainaFrente");
            backgroundBattleBottom = Content.Load<Texture2D>("backgroundBottomBattle");
            backgroundBattle = Content.Load<Texture2D>("backgroundBattleDay");
            battlepadPlayer = Content.Load<Texture2D>("battlepadPlayer");
            battlepadEnemy = Content.Load<Texture2D>("battlepadEnemy");
            playerBar = Content.Load<Texture2D>("playerBar");
            enemyBar = Content.Load<Texture2D>("enemyBar");
            logoPT = Content.Load<Texture2D>("logoPT");
            poop = Content.Load<Texture2D>("poopAnim");
            scratch = Content.Load<Texture2D>("scratchAnim");
            clickIcon = Content.Load<Texture2D>("clickIcon");

            animController = new AnimationController(this);
            explosionTex = Content.Load<Texture2D>("explosion");
            light = Content.Load<Texture2D>("light7");
            vacina = Content.Load<Texture2D>("vacinaAnim");
            mandioca = Content.Load<Texture2D>("mandiocaAnim");
            lightAnim = new Animation(light, 192, 192, 70, false, new Rectangle(258, 0, 192, 192));
            explosion = new Animation(explosionTex, 192, 192, 50, false, new Rectangle(258, 0, 192, 192));
            vacinaAnim = new Animation(vacina, 450, 340, 150, false, new Rectangle(0, 0, 450, 340));
            mandiocaAnim = new Animation(mandioca, 450, 340, 50, false, new Rectangle(0, 0, 450, 340));
            poopAnim = new Animation(poop, 450, 340, 50, false, new Rectangle(0, 0, 450, 340));
            scratchAnim = new Animation(scratch, 192, 192, 60, false, new Rectangle(258, 0, 192, 192));
            animController.AddAnimation(AnimationKey.Explosion, explosion);
            animController.AddAnimation(AnimationKey.Light, lightAnim);
            animController.AddAnimation(AnimationKey.Vacina, vacinaAnim);
            animController.AddAnimation(AnimationKey.Mandioca, mandiocaAnim);
            animController.AddAnimation(AnimationKey.Poop, poopAnim);
            animController.AddAnimation(AnimationKey.Scratch, scratchAnim);

            _stateManager.PushState(gamePlayState);

            base.Initialize();
        }

        public void AttackMethod()
        {
            gamePlayState.st = "Você ataca o oponente com " + MainPlayer.weapon + " causando \n" + MainPlayer.power + " pontos de dano.";
            gamePlayState.drawTextSlow.NextSentence(gamePlayState.st);

        }

        public void WaitMethod()
        {
            gamePlayState.st = "Você se caga de medo e \n tenta sair de fininho.";
            gamePlayState.drawTextSlow.NextSentence(gamePlayState.st);

        }

        public void VacinaMethod()
        {
            gamePlayState.st = "Você atira uma injeção de \nCoronavac contra " + genericMonster.name;
            gamePlayState.drawTextSlow.NextSentence(gamePlayState.st);
        }

        protected override void LoadContent()
        {
            MainPlayer = new Polimon("Dilma", 100, 50, "tapa", 20, 2, 100)
            {
                level = 24,
            };
            GenericMonster = new Polimon("Janaína", 150, 30, "tapa", 2, 0, 150)
            {
                level = 100
            };

            Attack = new Maneuver("Tapa Frouxo", mainPlayer.name + " ataca usando " + mainPlayer.weapon + ".", 50, AttackMethod, AnimationKey.Scratch);
            Mascara = new Maneuver("Mandioca", "Problematizar um ataque.", 0, WaitMethod, AnimationKey.Poop);
            Mandioca = new Maneuver("Saco de Vento", "Problematizar um ataque.", 30, WaitMethod, AnimationKey.Mandioca);
            Vacina = new Maneuver("Vacina", mainPlayer.name + " atira uma injeção de Coronavac \ncontra " + genericMonster.name + ".", 30, VacinaMethod, AnimationKey.Vacina);

            MainPlayer.listManeuvers.Add(Attack);
            MainPlayer.listManeuvers.Add(Mascara);
            MainPlayer.listManeuvers.Add(Mandioca);
            MainPlayer.listManeuvers.Add(Vacina);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            dialogueBox = Content.Load<Texture2D>("dialogueBar");

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update(gameTime);

            animController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
