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
        public bool PlayerAlive = true;
        public bool MonsterAlive = true;
        public SpriteFont font, titleFont, smallFont;
        public Texture2D _pixel;
        public string st = "";
        public DrawTextSlow drawTextSlow;
        public Maneuver SelectedManeuver;
        public bool battleHasStarted = false;

        //######################### Modules ############################
        public InitialTextModule initialTextModule;
        public ShowChoiceMenuModule showChoiceMenuModule;
        public PlayerAnimationModule playerAnimationModule;
        public MonsterBlinkModule monsterBlinkModule;
        public MonsterLifeBarModule monsterLifeBarModule;
        public PlayerActionModule playerActionModule;
        public CheckMonsterHealthModule CheckMonsterHealthModule;
        public MonsterAnimationModule monsterAnimationModule;
        public PlayerLifeBarModule playerLifeBarModule;
        public MonsterAttackModule MonsterAttackModule;
        public CheckPlayerHealthModule CheckPlayerHealthModule;
        public GameOverModule gameOverModule;

        public GamePlayState(Game game) : base(game)
        {

        } //add enemy as a parameter

        public override void Initialize()
        {
            drawTextSlow = new DrawTextSlow(gameRef);
            initialTextModule = new InitialTextModule(gameRef);
            showChoiceMenuModule = new ShowChoiceMenuModule(gameRef);
            playerAnimationModule = new PlayerAnimationModule(gameRef);
            monsterBlinkModule = new MonsterBlinkModule(gameRef);
            monsterLifeBarModule = new MonsterLifeBarModule(gameRef);
            playerActionModule = new PlayerActionModule(gameRef);
            CheckMonsterHealthModule = new CheckMonsterHealthModule(gameRef);
            monsterAnimationModule = new MonsterAnimationModule(gameRef);
            playerLifeBarModule = new PlayerLifeBarModule(gameRef);
            MonsterAttackModule = new MonsterAttackModule(gameRef);
            CheckPlayerHealthModule = new CheckPlayerHealthModule(gameRef);
            gameOverModule = new GameOverModule(gameRef);

            ModuleManager.AddModule(ModuleKey.InitialText, initialTextModule);
            ModuleManager.AddModule(ModuleKey.ShowChoiceMenu, showChoiceMenuModule);
            ModuleManager.AddModule(ModuleKey.PlayerAnimation, playerAnimationModule);
            ModuleManager.AddModule(ModuleKey.MonsterBlink, monsterBlinkModule);
            ModuleManager.AddModule(ModuleKey.MonsterLifeBar, monsterLifeBarModule);
            ModuleManager.AddModule(ModuleKey.PlayerAction, playerActionModule);
            ModuleManager.AddModule(ModuleKey.CheckMonsterHealth, CheckMonsterHealthModule);
            ModuleManager.AddModule(ModuleKey.MonsterAnimation, monsterAnimationModule);
            ModuleManager.AddModule(ModuleKey.PlayerLifeBar, playerLifeBarModule);
            ModuleManager.AddModule(ModuleKey.MonsterAttack, MonsterAttackModule);
            ModuleManager.AddModule(ModuleKey.CheckPlayerHealth, CheckPlayerHealthModule);
            ModuleManager.AddModule(ModuleKey.GameOverModule, gameOverModule);

            font = gameRef.Content.Load<SpriteFont>("font");
            titleFont = gameRef.Content.Load<SpriteFont>("titleFont");
            smallFont = gameRef.Content.Load<SpriteFont>("smallFont");
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            _pixel.SetData(data);

            ModuleManager.ActivateModule(ModuleKey.InitialText);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            drawTextSlow.Update(gameTime);

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

            gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Rectangle(250, 0, 192, 192), Color.White);

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

            ModuleManager.Draw(gameRef.SpriteBatch);

            gameRef.SpriteBatch.End();

            drawTextSlow.Draw(gameRef.SpriteBatch);

            base.Draw(gameTime);
        }
    }

    public class InitialTextModule : Module
    {
        Game1 gameRef;
        string st = "";
        public InitialTextModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            st = "Um polimon selvagem apareceu! \nVamos batalhar!";
            gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
        }

        public override void Update(GameTime gameTime)
        {
            if (gameRef.gamePlayState.drawTextSlow.done && Input.GetMousePressed())
            {
                ModuleManager.ActivateModule(ModuleKey.ShowChoiceMenu);
                Console.WriteLine("Opening Choices");
            }

            base.Update(gameTime);
        }
    }

    public class ShowChoiceMenuModule : Module
    {
        Game1 gameRef;
        public ShowChoiceMenuModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            gameRef.stateManager.PushState(gameRef.ChoiceState);
        }
    }

    public class PlayerAnimationModule : Module
    {
        Game1 gameRef;
        Rectangle rec;
        float maneuverDuration = 0;
        float timer = 0;
        public PlayerAnimationModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            Maneuver action = gameRef.gamePlayState.SelectedManeuver;
            gameRef.animController.SetAnimation(action.ManeuverAnimation);
            gameRef.animController.PlayAnimation(action.ManeuverAnimation);
            maneuverDuration = (gameRef.animController.ListaAnimations[gameRef.gamePlayState.SelectedManeuver.ManeuverAnimation].NumeroAnimations *
                   gameRef.animController.ListaAnimations[gameRef.gamePlayState.SelectedManeuver.ManeuverAnimation].Duration) + 500;
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //or totalseconds

            rec = gameRef.animController.ListaAnimations[gameRef.gamePlayState.SelectedManeuver.ManeuverAnimation].Position;

            if (timer > maneuverDuration)
            {
                Console.WriteLine("Manuever duration: " + maneuverDuration);
                gameRef.playPlayerAnim = false;
                timer = 0.0f;
                ModuleManager.ActivateModule(ModuleKey.MonsterBlink);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            gameRef._spriteBatch.Draw(
            gameRef.animController.currentAnimation.sprite,
            rec,
            gameRef.animController.currentAnimation.ListaRetangulos[gameRef.animController.currentAnimation.FrameAtual],
            Color.White);
        }
    }


    public class MonsterBlinkModule : Module
    {
        Game1 gameRef;
        float monsterBlinkTimer = 0;
        float monsterBlinkEnd = 0;
        public MonsterBlinkModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            monsterBlinkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            monsterBlinkEnd += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (monsterBlinkTimer < 0.2f)
            {

            }
            else
            {
                gameRef.SpriteBatch.Draw(gameRef.monsterTex, new Rectangle(250, 0, 192, 192), Color.Red);
            }

            if (monsterBlinkTimer > 0.3f)
            {
                monsterBlinkTimer = 0.0f;
            }
            if (monsterBlinkEnd > 0.9)
            {
                monsterBlinkTimer = 0.0f;
                monsterBlinkEnd = 0.0f;

                ModuleManager.ActivateModule(ModuleKey.MonsterLifeBar);
            }
        }
    }

    public class MonsterLifeBarModule : Module
    {
        Game1 gameRef;
        float cronometroBar = 0;
        int damageTaken = 0;
        int damageToTake = 0;

        public MonsterLifeBarModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();
            damageToTake = gameRef.gamePlayState.SelectedManeuver.Damage;
            Console.WriteLine("Monster life bar animation");
        }

        public override void Update(GameTime gameTime)
        {
            cronometroBar += (float)gameTime.ElapsedGameTime.TotalSeconds * 50f;

            if (cronometroBar > 3.0f)
            {
                gameRef.genericMonster.health -= 1;
                damageTaken += 1;
                cronometroBar = 0;
            }

            if (damageTaken >= damageToTake)
            {
                damageTaken = 0;
                ModuleManager.ActivateModule(ModuleKey.PlayerAction);
            }

            base.Update(gameTime);
        }
    }

    public class PlayerActionModule : Module
    {
        Game1 gameRef;
        string st = "";

        public PlayerActionModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            st = gameRef.gamePlayState.SelectedManeuver.Description;
            gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameRef.gamePlayState.drawTextSlow.done && Input.GetMousePressed())
            {
                ModuleManager.ActivateModule(ModuleKey.CheckMonsterHealth);
                Console.WriteLine("Going to check monster animation");
            }
        }
    }

    public class CheckMonsterHealthModule : Module
    {
        Game1 gameRef;
        string st = "";
        public CheckMonsterHealthModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            if (gameRef.genericMonster.health > 0)
            {
                st = "O oponente se prepara para atacar.";
                gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
            }
            else
            {
                st = "O oponente desmaiou.";
                gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
                gameRef.gamePlayState.MonsterAlive = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameRef.gamePlayState.drawTextSlow.done && Input.GetMousePressed())
            {
                if (gameRef.gamePlayState.MonsterAlive == false)
                {
                    ModuleManager.ActivateModule(ModuleKey.GameOverModule);
                }
                else
                {
                    ModuleManager.ActivateModule(ModuleKey.MonsterAnimation);
                    Console.WriteLine("Going to monster animation");
                }
            }
        }
    }

    public class MonsterAnimationModule : Module
    {
        Game1 gameRef;
        Rectangle rec;
        float maneuverDuration = 0;
        float timer = 0;
        public MonsterAnimationModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            gameRef.animController.SetAnimation(AnimationKey.Explosion);
            gameRef.animController.PlayAnimation(AnimationKey.Explosion);
            maneuverDuration = 750;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //or totalseconds

            rec = new Rectangle(20, 130, 150, 150);

            if (timer > maneuverDuration)
            {
                Console.WriteLine("Monster Manuever duration: " + maneuverDuration);
                timer = 0.0f;
                ModuleManager.ActivateModule(ModuleKey.PlayerLifeBar);
            }         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            gameRef._spriteBatch.Draw(
            gameRef.animController.currentAnimation.sprite,
            rec,
            gameRef.animController.currentAnimation.ListaRetangulos[gameRef.animController.currentAnimation.FrameAtual],
            Color.White);
        }
    }

    public class PlayerLifeBarModule : Module
    {
        Game1 gameRef;
        float cronometroBar = 0;
        int damageTaken = 0;
        int damageToTake = 0;

        public PlayerLifeBarModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();
            damageToTake = gameRef.genericMonster.power;
        }

        public override void Update(GameTime gameTime)
        {
            cronometroBar += (float)gameTime.ElapsedGameTime.TotalSeconds * 50f;

            if (cronometroBar > 3.0f)
            {
                gameRef.mainPlayer.health -= 1;
                damageTaken += 1;
                cronometroBar = 0;
            }

            if (damageTaken >= damageToTake)
            {
                damageTaken = 0;
                Console.WriteLine("Monster attack description");
                ModuleManager.ActivateModule(ModuleKey.MonsterAttack);
            }
            base.Update(gameTime);
        }
    }

    public class MonsterAttackModule : Module
    {
        Game1 gameRef;
        string st = "";

        public MonsterAttackModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            st = gameRef.genericMonster.name + " ataca usando " + gameRef.genericMonster.weapon + ".";
            gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameRef.gamePlayState.drawTextSlow.done == true)
            {
                if (Input.GetMousePressed())
                {
                    ModuleManager.ActivateModule(ModuleKey.CheckPlayerHealth);
                    Console.WriteLine("Going to check player health");
                }
            }
        }
    }

    public class CheckPlayerHealthModule : Module
    {
        Game1 gameRef;
        string st = "";
        public CheckPlayerHealthModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            if (gameRef.mainPlayer.health < 1)
            {
                st = gameRef.mainPlayer.name + " desmaiou, o oponente venceu.";
                gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
                gameRef.gamePlayState.PlayerAlive = false;
            }
            else
            {
                st = "Você tem " + gameRef.mainPlayer.health.ToString() + " pontos de vida.";
                gameRef.gamePlayState.drawTextSlow.NextLineMethod(st);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameRef.gamePlayState.drawTextSlow.done && Input.GetMousePressed())
            {
                if (gameRef.gamePlayState.PlayerAlive == false)
                {
                    ModuleManager.ActivateModule(ModuleKey.GameOverModule);
                }
                else
                {
                    ModuleManager.ActivateModule(ModuleKey.ShowChoiceMenu);
                    Console.WriteLine("Rotation done, going to choice menu");
                }
            }
        }
    }

    public class GameOverModule : Module
    {
        Game1 gameRef;
        float timer = 0;
        public GameOverModule(Game1 game)
        {
            gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 4.0f)
            {
                gameRef.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            gameRef.gamePlayState.drawTextSlow.showDialogue = false;
            gameRef.SpriteBatch.Draw(gameRef.gamePlayState._pixel, new Rectangle(0, 0, 450, 680), Color.Black);
            gameRef.SpriteBatch.DrawString(gameRef.gamePlayState.font, "Fim de Jogo", new Vector2(150, 340), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }
    }
}

