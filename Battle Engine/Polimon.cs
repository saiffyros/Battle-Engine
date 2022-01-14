using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class Polimon// : Monster
    {
        public string name;
        public int health;
        public int power;
        public int luck;
        public int maxHealth;
        public string weapon;
        public int xp;
        public Texture2D sprite;
        public int level;

        public List<Maneuver> listManeuvers = new List<Maneuver>();

        public Polimon(string _name, int _health, int _power, string _weapon, int _xp, int _luck, int _maxHealth)
        {
            name = _name;
            health = _health;
            power = _power;
            weapon = _weapon;
            xp = _xp;
            luck = _luck;
            maxHealth = _maxHealth;
            level = 1;
        }

        public Polimon()
        {

        }
    }
}

//class player(object):
//        def __init__(self, name):
//            self.name = name
//            self.health = 100
//            self.power = 50
//            self.xp = 20
//            self.weapon = "punho"
//            self.luck = 2
