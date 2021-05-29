using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class NoRecoil : BaseModule
    {
        public NoRecoil(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Update()
        {
            return;

            if (Menu.NoRecoilOption.State)
                Mem.Write<float>(MainModule.LocalEntity.Pointer + Offsets.Entity.NO_RECOIL, 0f);
            else
                Mem.Write<float>(MainModule.LocalEntity.Pointer + Offsets.Entity.NO_RECOIL, 1f);
        }
    }
}
