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
        private ActionManager actionManager;
        public string dialogueText = "Let's Fight!";
        private bool PlayerAlive = true;
        private bool MonsterAlive = true;
        private SpriteFont font;

        public GamePlayState(Game game) : base(game)
        {

        } //add enemy as a parameter

        public override void Initialize()
        {
            Game1.previousKeyboardState = Keyboard.GetState();
            
            actionManager = new ActionManager();
            font = GameRef.Content.Load<SpriteFont>("font");

            actionManager.SetAction(() => InitialText());
            actionManager.SetAction(() => ShowChoiceMenu());
            actionManager.SetAction(() => PlayerAction());
            actionManager.SetAction(() => CheckMonsterHealth());
            actionManager.SetAction(() => MonsterAttack());
            actionManager.SetAction(() => CheckPlayerHealth());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (listIndex > 4)
                listIndex = 0;

            if (InputSystem.CheckKeyPressed(Keys.Q))
            {
                if (PlayerAlive && MonsterAlive)
                {
                    listIndex += 1;
                    actionManager.InvokeAction(listIndex);
                }
                else
                {
                    dialogueText = "Game is over";
                    GameRef.Exit();
                }
            }

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

            GameRef.SpriteBatch.Draw(Game1.dialogueBox, new Vector2(0, 300), Color.White);
            GameRef.SpriteBatch.DrawString(font, dialogueText, new Vector2(200, 400), Color.White);

            //foreach (string text in GameRef.messages)
            //{
            //    GameRef.SpriteBatch.DrawString(font, text, new Vector2(200, 450), Color.White);
            //}

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void InitialText() { dialogueText = "Let's fight!!"; }

        public void ShowChoiceMenu()
        {
            dialogueText = "Checking action!";

            StateManager stateManager = (StateManager)GameRef.Services.GetService(typeof(IStateManager));
            stateManager.PushState(GameRef.ChoiceState);
            
            //I CAN RETRIVE THE SERVICE OR USE THE GAMEREF ONE

            //GameRef.stateManager.PushState(GameRef.ChoiceState);
        }

        public void PlayerAction()
        {
            Maneuver action = GameRef.ChoiceState.selectedManeuver;
            action.ManeuverAction.Invoke(); //null check?
        }

        public void CheckMonsterHealth()
        {
            if (GameRef.genericMonster.health > 0)
            {
                dialogueText = "Monster is still alive.";
            }
            else
            {
                dialogueText = "Monster died.";
                MonsterAlive = false;
            }
        }

        public void MonsterAttack()
        {
            GameRef.mainPlayer.health -= GameRef.genericMonster.power;
            //GameRef.messages.Clear();
            //GameRef.messages.Enqueue("Monster attack you causing  " + Game1.GenericMonster.power + " damage points");
            dialogueText = "Monster attack you causing " + GameRef.genericMonster.power + " damage points.";
        }

        public void CheckPlayerHealth()
        {
            if (GameRef.mainPlayer.health < 1)
            {
                dialogueText = "You died. Enemy won.";
                PlayerAlive = false;
            }
            else
            {
                dialogueText = "You have " + GameRef.mainPlayer.health.ToString() + " health points.";
            }
        }
    }
}
