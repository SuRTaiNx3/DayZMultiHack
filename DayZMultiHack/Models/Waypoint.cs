using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Models
{
    public class Waypoint
    {
        public string Name;
        public Vector3 Position;
        public bool IsVisible;

        public Vector3 ScreenPosition;
        public float Distance;
    }
}
