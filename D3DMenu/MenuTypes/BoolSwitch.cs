using D3DMenu.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    [ValueField("State")]
    public class BoolSwitch : MenuItemBase
    {
        public bool State;

        public BoolSwitch(string name, string title, bool state, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.State = state;
            this.Save = save;
        }

        public override void Next()
        {
            this.State = !this.State;
        }

        public override void Previous()
        {
            this.State = !this.State;
        }
    }
}
