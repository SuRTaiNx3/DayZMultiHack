using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    public class MurderModePosition : MenuItemBase
    {
        public Positions SelectedPosition;

        public MurderModePosition(string name, string title, Positions position, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.SelectedPosition = position;
            this.Save = save;
        }

        public enum Positions
        {
            Head,
            Chest,
            Feet
        }

        public override void Next()
        {
            switch (this.SelectedPosition)
            {
                case Positions.Head:
                    this.SelectedPosition = Positions.Chest;
                    break;
                case Positions.Chest:
                    this.SelectedPosition = Positions.Feet;
                    break;
                case Positions.Feet:
                    this.SelectedPosition = Positions.Head;
                    break;
                default:
                    break;
            }
        }

        public override void Previous()
        {
            switch (this.SelectedPosition)
            {
                case Positions.Head:
                    this.SelectedPosition = Positions.Feet;
                    break;
                case Positions.Feet:
                    this.SelectedPosition = Positions.Chest;
                    break;
                case Positions.Chest:
                    this.SelectedPosition = Positions.Head;
                    break;
                default:
                    break;
            }
        }
    }
}
