using D3DMenu.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    [ValueField("SelectedType")]
    public class CrosshairType : MenuItemBase
    {
        public Type SelectedType;

        public CrosshairType(string name, string title, Type type, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.SelectedType = type;
            this.Save = save;
        }

        public enum Type
        {
            Type1,
            Type2,
            Type3,
            Type4,
            Type5
        }

        public override void Next()
        {
            switch (this.SelectedType)
            {
                case Type.Type1:
                    this.SelectedType = Type.Type2;
                    break;
                case Type.Type2:
                    this.SelectedType = Type.Type3;
                    break;
                case Type.Type3:
                    this.SelectedType = Type.Type4;
                    break;
                case Type.Type4:
                    this.SelectedType = Type.Type5;
                    break;
                case Type.Type5:
                    this.SelectedType = Type.Type1;
                    break;
                default:
                    break;
            }
        }

        public override void Previous()
        {
            switch (this.SelectedType)
            {
                case Type.Type1:
                    this.SelectedType = Type.Type5;
                    break;
                case Type.Type2:
                    this.SelectedType = Type.Type1;
                    break;
                case Type.Type3:
                    this.SelectedType = Type.Type2;
                    break;
                case Type.Type4:
                    this.SelectedType = Type.Type3;
                    break;
                case Type.Type5:
                    this.SelectedType = Type.Type4;
                    break;
                default:
                    break;
            }
        }
    }
}
