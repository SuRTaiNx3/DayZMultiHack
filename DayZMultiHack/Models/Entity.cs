using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Models
{
    public struct Entity
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
        public Vector3 ScreenHeadPosition;
        public float DirectionX;
        public float DirectionY;
        public int Distance;
        public string Name;
        public string ObjectName;
        public string Stance;
        public string State;
        public Types Type;
        public string TypeString;
        public string WeaponInHand;
        public bool IsDead;
        public string Locked;
        public bool IsTN;
        public EntityValues Stats;
        public bool Reused;
        public bool IsValid;
        public IntPtr DriverPointer;
        public Driver Driver;

        public override string ToString()
        {
            return Name;
        }

        public enum Types
        {
            Soldier,
            Zombie,
            Car,
            Ship,
            Helicopter,
            Airplane,
            Unknown
        }

        public static Types ParseType(string type)
        {
            switch (type.ToLower())
            {
                case "zombie":
                    return Types.Zombie;
                case "soldier":
                    return Types.Soldier;
                case "car":
                    return Types.Car;
                case "ship":
                    return Types.Ship;
                case "helicopt":
                    return Types.Helicopter;
                case "airplane":
                    return Types.Airplane;
                default:
                    return Types.Unknown;
            }
        }
    }
}
