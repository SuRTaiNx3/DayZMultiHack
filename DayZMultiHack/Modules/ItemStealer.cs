using DayZMultiHack.Models;
using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class ItemStealer : BaseModule
    {
        public ItemStealer(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }

        public void Run(Entity entity)
        {
            if (entity.Pointer == IntPtr.Zero)
                return;

            int offset = 0;
            if (Menu.ItemStealerChestOption.State)
            {
                offset = Offsets.Entity.Inventory.CHEST;
                Menu.ItemStealerChestOption.State = false;
            }
            else if (Menu.ItemStealerPrimaryOption.State)
            {
                offset = Offsets.Entity.Inventory.PRIMARY;
                Menu.ItemStealerPrimaryOption.State = false;
            }
            else if (Menu.ItemStealerMeleeOption.State)
            {
                offset = Offsets.Entity.Inventory.MELEE;
                Menu.ItemStealerMeleeOption.State = false;
            }
            else if (Menu.ItemStealerHelmetOption.State)
            {
                offset = Offsets.Entity.Inventory.HELMET;
                Menu.ItemStealerHelmetOption.State = false;
            }
            else if (Menu.ItemStealerMaskOption.State)
            {
                offset = Offsets.Entity.Inventory.MASK;
                Menu.ItemStealerMaskOption.State = false;
            }
            else if (Menu.ItemStealerGlassesOption.State)
            {
                offset = Offsets.Entity.Inventory.GLASSES;
                Menu.ItemStealerGlassesOption.State = false;
            }
            else if (Menu.ItemStealerPantsOption.State)
            {
                offset = Offsets.Entity.Inventory.PANTS;
                Menu.ItemStealerPantsOption.State = false;
            }
            else if (Menu.ItemStealerShoesOption.State)
            {
                offset = Offsets.Entity.Inventory.SHOES;
                Menu.ItemStealerShoesOption.State = false;
            }
            else if (Menu.ItemStealerVestOption.State)
            {
                offset = Offsets.Entity.Inventory.VEST;
                Menu.ItemStealerVestOption.State = false;
            }
            else if (Menu.ItemStealerGlovesOption.State)
            {
                offset = Offsets.Entity.Inventory.GLOVES;
                Menu.ItemStealerGlovesOption.State = false;
            }
            else if (Menu.ItemStealerBackpackOption.State)
            {
                offset = Offsets.Entity.Inventory.BACKPACK;
                Menu.ItemStealerBackpackOption.State = false;
            }

            if (offset < 1)
                return;


            // Take mode
            if (Mem.Read<IntPtr>(MainModule.LocalEntity.Pointer + offset) == IntPtr.Zero &&
                Mem.Read<int>(entity.Pointer + offset) > 0)
            {
                Mem.Write<int>(MainModule.LocalEntity.Pointer + offset, Mem.Read<int>(entity.Pointer + offset));
                Mem.Write<int>(entity.Pointer + offset, 0);
            }
        }
    }
}
