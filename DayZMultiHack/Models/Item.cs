using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Models
{
    public struct Item
    {
        public IntPtr Pointer;
        public string ObjectName;
        public string RealName;
        public IntPtr VisualStatePointer;
        public Vector3 Position;
        public Vector3 ScreenPosition;
        public string Type;
        public bool HasContainer;
        public List<Item> Container;
        public bool IsRuined;

        public Item(IntPtr pointer, string realName, string objectName, IntPtr visualStatePointer, Vector3 position, Vector3 screenPosition, string type, bool hasContainer = false, bool isRuined = false)
        {
            Pointer = pointer;
            RealName = realName;
            ObjectName = objectName;
            VisualStatePointer = visualStatePointer;
            Position = position;
            Type = type;
            HasContainer = hasContainer;
            IsRuined = isRuined;
            ScreenPosition = screenPosition;
            Container = new List<Item>();
        }
    }
}
