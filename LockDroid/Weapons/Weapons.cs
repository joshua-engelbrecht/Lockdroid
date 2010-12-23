using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockDroid.Weapons
{
    public class Weapon
    {
        public string Name { get; set; }
        public string Damage { get; set; }
        public int ReloadTime { get; set; }
        public int VisibleLevel { get; set; }
        public int Level { get; set; }
    }

}
