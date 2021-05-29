using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Models
{
    public struct Driver
    {
        public int ID;
        public IntPtr BasePointer;
        public IntPtr Pointer;
        public IntPtr VisualStatePointer;
        public IntPtr WeaponPointer;
        public IntPtr WeaponObjectClassPointer;
        public Vector3 Position;
        public Vector3 HeadPosition;
        public Vector3 ScreenFootPosition;
        public float DirectionX;
        public float DirectionY;
        public int Distance;
        public string Name;
        public string ObjectName;
        public string Stance;
        public string TypeString;
        public string WeaponInHand;
        public bool IsDead;
        public string Locked;
        public bool IsTN;
        public EntityValues Stats;
        public bool Reused;
        public bool IsValid;
    }
}
