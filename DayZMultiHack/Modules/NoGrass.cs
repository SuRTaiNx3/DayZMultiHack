using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class NoGrass : BaseModule
    {
        public NoGrass(Main main, UI ui, Menu menu) 
            : base(main, ui, menu){}


        public void Update()
        {
            if (Menu.NoGrassOption.State)
                Mem.Write<float>(MainModule.WorldBase + Offsets.World.NO_GRASS, 50f);
            else
                Mem.Write<float>(MainModule.WorldBase + Offsets.World.NO_GRASS, 1f);
        }
    }
}
