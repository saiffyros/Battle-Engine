using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class Maneuver
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Damage { get; private set; }

        public Action ManeuverAction { get; set; }

        public Maneuver(string _name, string _description, int _damage, Action _maneuver)
        {
            Name = _name;
            Description = _description;
            Damage = _damage;
            ManeuverAction = _maneuver;
        }
    }
}
