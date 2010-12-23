using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LockDroid.Weapons;

namespace LockDroid.Ships
{
    public class Ship
    {
        public int SizeLength { get; set; }
        public int SizeWitdth { get; set; }
        public int HitPoints { get; set; }
        public List<Weapon> Weapons { get; set; }
        public List<Position> SpotsOccupied { get; set; }
        public int Level { get; set; }
        
        private List<string> alpha = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        bool takeDamage(int amount)
        {
            HitPoints = HitPoints - amount;
            return checkHitPoints();
        }

        private bool checkHitPoints()
        {
            if (HitPoints < 1)
                return true;
            return false;
        }

        /// <summary>
        /// Update the location of the ship
        /// </summary>
        /// <param name="startPos">Position Class</param>
        /// <param name="alignment">True for horizontal, false for vertical</param>
        public IEnumerable<Position> updateLocation(Position startPos, bool horizontal)
        {
            SpotsOccupied.Add(startPos);
            var xCordNum = alpha.FindIndex(delegate(string s)
            {
                return string.Equals(s, startPos.xCord, StringComparison.CurrentCultureIgnoreCase);
            });

            if (horizontal)
            {
                for (var x = 1; x < SizeLength; x++)
                {
                    SpotsOccupied.Add(new Position
                    {
                        xCord = alpha[xCordNum + x],
                        yCord = startPos.yCord
                    });
                }
            }
            else
            {
                for (var x = 1; x < SizeLength; x++)
                {
                    SpotsOccupied.Add(new Position
                    {
                        xCord = startPos.xCord,
                        yCord = startPos.yCord + x
                    });
                }
            }
            IEnumerable<Position> myEnumeration = SpotsOccupied;
            return myEnumeration;
        }
    }
}
