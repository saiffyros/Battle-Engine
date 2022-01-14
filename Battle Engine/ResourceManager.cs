using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Engine
{
    public class ResourceManager : GameComponent
    {
        public static Texture2D dialogueBox;
        public Texture2D explosionTex, light, vacina;
        public Texture2D playerTex, monsterTex, backgroundBattleBottom, backgroundBattle, battlepadPlayer, battlepadEnemy, playerBar, enemyBar, logoPT;



        public ResourceManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {

            base.Initialize();
        }
    }
}
