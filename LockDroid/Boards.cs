using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockDroid
{
    public class Boards
    {
        public static bool checkForHit(Position position, List<Position>occupiedPos)
        {
            foreach (Position oP in occupiedPos)
            {
                if (position.xCord == oP.xCord && position.yCord == oP.yCord)
                    return true;
            }
            return false;
        }
    }
}
