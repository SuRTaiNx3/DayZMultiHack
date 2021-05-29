using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    public class Separator : MenuItemBase
    {
        public Separator(string title = "separator")
        {
            this.Title = title;
            this.DrawMe = false;
            this.Height = 4;
        }
    }
}
