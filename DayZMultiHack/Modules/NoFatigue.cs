using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class NoFatigue : BaseModule
    {
        public NoFatigue(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Update()
        {
            if (Menu.NoFatigueOption.State)
            {
                Mem.Write<float>(MainModule.LocalEntity.Pointer + Offsets.Entity.NO_FATIGUE_BASE, 0f);
                Mem.Write<float>(MainModule.LocalEntity.Pointer + Offsets.Entity.NO_FATIGUE_DIFF, 1f);
            }
        }
    }
}
