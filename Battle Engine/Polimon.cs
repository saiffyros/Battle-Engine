using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class Polimon// : Monster
    {
        public string name { get; set; }
        public int health { get; set; }
        public int power { get; set; }
        public int luck { get; set; }
        public int maxHealth { get; set; }
        public string weapon { get; set; }
        public int xp { get; set; }

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
