using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    public class ExitApp : MenuItemBase
    {
        public bool ExitApplication;

        public ExitApp(string title)
        {
            this.Title = title;
        }

        public override void Next()
        {
            this.ExitApplication = true;
        }

        public override void Previous()
        {
            this.ExitApplication = true;
        }
    }
}
