using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class Monster : Component
    {
        [ContentSerializer]
        public string name { get; set; }
        [ContentSerializer]
        public int health { get; set; }
        [ContentSerializer]
        public int power { get; set; }
        [ContentSerializer]
        public int luck { get; set; }

        public Monster(string a, int b, int c, int f)
        {
            name = a;
            health = b;
            power = c;
            luck = f;
        }

        public Monster()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}

//##########################################################

//class monster(object):
//        def __init__(self, health, power, name, luck):
//            self.name = name
//            self.health = health
//            self.power = power
//            self.luck = luck
