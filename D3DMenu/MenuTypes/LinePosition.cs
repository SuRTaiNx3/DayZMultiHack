using D3DMenu.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    [ValueField("SelectedPosition")]
    public class LinePosition : MenuItemBase
    {
        public Position SelectedPosition;

        public LinePosition(string name, string title, Position position, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.SelectedPosition = position;
            this.Save = save;
        }

        public enum Position
        {
            Top,
            Bottom
        }

        public override void Next()
        {
            switch (this.SelectedPosition)
            {
                case Position.Bottom:
                    this.SelectedPosition = Position.Top;
                    break;
                case Position.Top:
                    this.SelectedPosition = Position.Bottom;
                    break;
                default:
                    break;
            }
        }

        public override void Previous()
        {
            switch (this.SelectedPosition)
            {
                case Position.Top:
                    this.SelectedPosition = Position.Bottom;
                    break;
                case Position.Bottom:
                    this.SelectedPosition = Position.Top;
                    break;
                default:
                    break;
            }
        }
    }
}
