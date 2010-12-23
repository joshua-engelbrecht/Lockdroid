using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;

namespace LockDroid.Ships
{
    public class AircraftCarrier : Ship
    {
        public AircraftCarrier()
        {
            SpotsOccupied = new List<Position>();

            HitPoints = 250;
            SizeLength = 5;
        }

    }
}
