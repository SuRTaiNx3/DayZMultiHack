using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using D3DMenu.Attributes;

namespace D3DMenu.MenuTypes
{
    [ValueField("SelectedColor")]
    public class ColorSelection : MenuItemBase
    {
        public Colors SelectedColor;

        public ColorSelection(string name, string title, Colors color, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.SelectedColor = color;
            this.Save = save;
        }

        public enum Colors
        {
            Green,
            LightGreen,
            YellowGreen,
            GreenYellow,
            Red,
            Cyan,
            LightBrown,
            Blue,
            Yellow,
            Pink,
            PinkViolet,
            Violet,
            Orange,
            OrangeRed,
            LightBlue,
            White,
            Black,
            Gray,
            LightGray
        }

        public Color GetColor()
        {
            switch (SelectedColor)
            {
                case Colors.Green:
                    return new Color(0, 154, 0);
                case Colors.YellowGreen:
                    return new Color(246, 223, 71);
                case Colors.GreenYellow:
                    return new Color(217, 210, 6);
                case Colors.Red:
                    return Color.Red;
                case Colors.Cyan:
                    return Color.Cyan;
                case Colors.LightBrown:
                    return new Color(249, 187, 122);
                case Colors.Blue:
                    return new Color(0, 154, 238);
                case Colors.Yellow:
                    return Color.Yellow;
                case Colors.Pink:
                    return Color.HotPink;
                case Colors.PinkViolet:
                    return new Color(255, 79, 254);
                case Colors.Violet:
                    return new Color(152, 151, 244);
                case Colors.Orange:
                    return new Color(255, 178, 0, 255);
                case Colors.OrangeRed:
                    return new Color(244, 96, 0);
                case Colors.LightBlue:
                    return new Color(152, 209, 244);
                case Colors.White:
                    return Color.White;
                case Colors.Black:
                    return Color.Black;
                case Colors.LightGreen:
                    return new Color(160, 178, 0);
                case Colors.Gray:
                    return Color.DimGray;
                case Colors.LightGray:
                    return new Color(222, 234, 241);
            }
            return Color.Green;
        }

        public override void Next()
        {
            switch (this.SelectedColor)
            {
                case Colors.Green:
                    this.SelectedColor = Colors.LightGreen;
                    break;
                case Colors.LightGreen:
                    this.SelectedColor = Colors.YellowGreen;
                    break;
                case Colors.YellowGreen:
                    this.SelectedColor = Colors.GreenYellow;
                    break;
                case Colors.GreenYellow:
                    this.SelectedColor = Colors.Red;
                    break;
                case Colors.Red:
                    this.SelectedColor = Colors.Cyan;
                    break;
                case Colors.Cyan:
                    this.SelectedColor = Colors.LightBrown;
                    break;
                case Colors.LightBrown:
                    this.SelectedColor = Colors.Blue;
                    break;
                case Colors.Blue:
                    this.SelectedColor = Colors.Yellow;
                    break;
                case Colors.Yellow:
                    this.SelectedColor = Colors.Pink;
                    break;
                case Colors.Pink:
                    this.SelectedColor = Colors.PinkViolet;
                    break;
                case Colors.PinkViolet:
                    this.SelectedColor = Colors.Violet;
                    break;
                case Colors.Violet:
                    this.SelectedColor = Colors.Orange;
                    break;
                case Colors.Orange:
                    this.SelectedColor = Colors.OrangeRed;
                    break;
                case Colors.OrangeRed:
                    this.SelectedColor = Colors.LightBlue;
                    break;
                case Colors.LightBlue:
                    this.SelectedColor = Colors.White;
                    break;
                case Colors.White:
                    this.SelectedColor = Colors.Black;
                    break;
                case Colors.Black:
                    this.SelectedColor = Colors.Gray;
                    break;
                case Colors.Gray:
                    this.SelectedColor = Colors.LightGray;
                    break;
                case Colors.LightGray:
                    this.SelectedColor = Colors.Green;
                    break;
                default:
                    break;
            }
        }

        public override void Previous()
        {
            switch (this.SelectedColor)
            {
                case Colors.Green:
                    this.SelectedColor = Colors.LightGray;
                    break;
                case Colors.LightGreen:
                    this.SelectedColor = Colors.Green;
                    break;
                case Colors.YellowGreen:
                    this.SelectedColor = Colors.LightGreen;
                    break;
                case Colors.GreenYellow:
                    this.SelectedColor = Colors.YellowGreen;
                    break;
                case Colors.Red:
                    this.SelectedColor = Colors.GreenYellow;
                    break;
                case Colors.Cyan:
                    this.SelectedColor = Colors.Red;
                    break;
                case Colors.LightBrown:
                    this.SelectedColor = Colors.Cyan;
                    break;
                case Colors.Blue:
                    this.SelectedColor = Colors.LightBrown;
                    break;
                case Colors.Yellow:
                    this.SelectedColor = Colors.Blue;
                    break;
                case Colors.Pink:
                    this.SelectedColor = Colors.Yellow;
                    break;
                case Colors.PinkViolet:
                    this.SelectedColor = Colors.Pink;
                    break;
                case Colors.Violet:
                    this.SelectedColor = Colors.PinkViolet;
                    break;
                case Colors.Orange:
                    this.SelectedColor = Colors.Violet;
                    break;
                case Colors.OrangeRed:
                    this.SelectedColor = Colors.Orange;
                    break;
                case Colors.LightBlue:
                    this.SelectedColor = Colors.OrangeRed;
                    break;
                case Colors.White:
                    this.SelectedColor = Colors.LightBlue;
                    break;
                case Colors.Black:
                    this.SelectedColor = Colors.White;
                    break;
                case Colors.Gray:
                    this.SelectedColor = Colors.Black;
                    break;
                case Colors.LightGray:
                    this.SelectedColor = Colors.Gray;
                    break;
                default:
                    break;
            }
        }
    }
}
