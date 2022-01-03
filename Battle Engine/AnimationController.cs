using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Engine
{
    public class AnimationController : DrawableGameComponent
    {
        Game1 gameRef;
        public Animation currentAnimation;
        public Dictionary<AnimationKey, Animation> ListaAnimations = new Dictionary<AnimationKey, Animation>();

        public AnimationController(Game game) : base(game)
        {
            gameRef = (Game1)game;
        }

        public void AddAnimation(AnimationKey name, Animation anim)
        {
            ListaAnimations.Add(name, anim);
        }

        public void PlayAnimation(AnimationKey name)
        {
            ListaAnimations[name].Ativa = true;
        }

        public void SetAnimation(AnimationKey anim)
        {
            foreach (KeyValuePair<AnimationKey, Animation> animations in ListaAnimations)
            {
                animations.Value.Ativa = false;
            }
            currentAnimation = ListaAnimations[anim];
            ListaAnimations[anim].Ativa = true;
        }

        public void StopAnimation(AnimationKey name)
        {
            ListaAnimations[name].Ativa = false;
        }

        public void SetVectorAnimation(Vector2 direction, AnimationKey animUp, AnimationKey animDown, AnimationKey animRight, AnimationKey animLeft)
        {
            switch (direction.X, direction.Y)
            {
                case (0, -60):
                    SetAnimation(animUp);
                    break;
                case (0, 60):
                    SetAnimation(animDown);
                    break;
                case (60, 0):
                    SetAnimation(animRight);
                    break;
                case (-60, 0):
                    SetAnimation(animLeft);
                    break;
            }
        }

        public void SetVectorAnimation(Vector2 direction, AnimationKey animRight, AnimationKey animLeft)
        {
            switch (direction.X)
            {
                case (60):
                    SetAnimation(animRight);
                    break;
                case (-60):
                    SetAnimation(animLeft);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {

            foreach (KeyValuePair<AnimationKey, Animation> animValues in ListaAnimations)
            {
                if (animValues.Value.Ativa == true)
                {
                    animValues.Value.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef._spriteBatch.Begin();

            foreach (KeyValuePair<AnimationKey, Animation> animValues in ListaAnimations)
            {
                if (animValues.Value.Drawing == true)
                {
                    animValues.Value.Draw(gameRef._spriteBatch) ;
                }
            }

            gameRef._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
