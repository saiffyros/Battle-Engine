﻿using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class Player : Monster
    {
        [ContentSerializer]
        public string weapon { get; set; }
        [ContentSerializer]
        public int xp { get; set; }
        public List<Maneuver> listManeuvers = new List<Maneuver>();

        public Player(string _name, int _health, int _power, string _weapon, int _xp, int _luck)
        {
            name = _name;
            health = _health;
            power = _power;
            weapon = _weapon;
            xp = _xp;
            luck = _luck;
        }

        public Player()
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
