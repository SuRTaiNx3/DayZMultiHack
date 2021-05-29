using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary;

namespace DayZMultiHack.Modules
{
    public class BaseModule
    {
        #region Properties

        public Main MainModule { get; set; }
        public Menu Menu;
        public UI DirectXUI { get; set; }
        public ProcessMemory Mem { get; set; }

        #endregion

        #region Constructor

        public BaseModule(Main main, UI directXUI, Menu menu)
        {
            MainModule = main;
            DirectXUI = directXUI;
            Menu = menu;
            Mem = main.mem;
        }

        #endregion
    }
}
