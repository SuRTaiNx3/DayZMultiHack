using D3DMenu.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    [ValueField("SelectedType")]
    public class MurderModeType : MenuItemBase
    {
        public Type SelectedType;

        public MurderModeType(string name, string title, Type type, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.SelectedType = type;
            this.Save = save;
        }

        public enum Type
        {
            Soldier,
            Zombie,
            Unknown
        }

        public override void Next()
        {
            switch (this.SelectedType)
            {
                case Type.Unknown:
                    this.SelectedType = Type.Soldier;
                    break;
                case Type.Soldier:
                    this.SelectedType = Type.Zombie;
                    break;
                case Type.Zombie:
                    this.SelectedType = Type.Unknown;
                    break;
                default:
                    break;
            }
        }

        public override void Previous()
        {
            switch (this.SelectedType)
            {
                case Type.Unknown:
                    this.SelectedType = Type.Zombie;
                    break;
                case Type.Soldier:
                    this.SelectedType = Type.Unknown;
                    break;
                case Type.Zombie:
                    this.SelectedType = Type.Soldier;
                    break;
                default:
                    break;
            }
        }
    }
}
