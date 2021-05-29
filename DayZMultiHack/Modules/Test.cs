using ExternalD3D11;
using ExternalD3D11.ClickableMenu.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class Test : BaseModule
    {
        #region Globals

        #endregion

        #region Constructor

        public Test(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            ListMenu listMenu = new ListMenu(ui.BaseFont);
            listMenu.X = 100;
            listMenu.Y = 100;

            ListMenuItem listMenuItem1 = new ListMenuItem(ui.BaseFont);
            listMenuItem1.Text = "Item1";
            listMenu.ListItems.Add(listMenuItem1);

            ListMenuItem listMenuItem2 = new ListMenuItem(ui.BaseFont);
            listMenuItem2.Text = "Test Item #2";
            listMenu.ListItems.Add(listMenuItem2);

            ListMenuItem listMenuItem3 = new ListMenuItem(ui.BaseFont);
            listMenuItem3.Text = "Dies ist ein Test #3";
            listMenu.ListItems.Add(listMenuItem3);

            ListMenuItem listMenuItem4 = new ListMenuItem(ui.BaseFont);
            listMenuItem4.Text = "Test #4";
            listMenu.ListItems.Add(listMenuItem4);

            DirectXUI.ClickableArea.Controls.Add(listMenu);
        }

        #endregion

        #region Methods

        public void Draw()
        {

        }

        #endregion
    }
}
