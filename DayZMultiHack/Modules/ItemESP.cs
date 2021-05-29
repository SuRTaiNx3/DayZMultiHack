using DayZMultiHack.Models;
using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class ItemESP : BaseModule
    {
        public ItemESP(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            
        }

        public void Draw()
        {
            foreach (Item item in MainModule.Items)
            {
                Vector3 d = MainModule.LocalEntity.Position - item.Position;
                float dist = (float)Math.Sqrt((d.X * d.X) + (d.Y * d.Y) + (d.Z * d.Z));

                DrawItem(item.RealName, item.ObjectName, item.Type, dist, item.ScreenPosition, item.IsRuined);
            }
        }

        private void DrawItem(string itemName, string objectName, string itemType, float dist, Vector3 position, bool isRuined)
        {
            if (position.Z < 0.02f || dist > 2500)
                return;

            objectName = objectName.ToLower();

            SharpDX.Color itemColor = SharpDX.Color.White;
            bool draw = false;

            if (ItemFilter.Weapons.Contains(objectName))
            {
                if (Menu.WeaponsOption.State)
                {
                    itemColor = Menu.WeaponsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Attachments.Contains(objectName))
            {
                if (Menu.AttachmentsOption.State)
                {
                    itemColor = Menu.AttachmentsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Mags.Contains(objectName))
            {
                if (Menu.MagsOption.State)
                {
                    itemColor = Menu.MagsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Ammo.Contains(objectName))
            {
                if (Menu.AmmoOption.State)
                {
                    itemColor = Menu.AmmoColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Grenades.Contains(objectName))
            {
                if (Menu.GrenadesOption.State)
                {
                    itemColor = Menu.GrenadesColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Pistols.Contains(objectName))
            {
                if (Menu.PistolsOption.State)
                {
                    itemColor = Menu.PistolsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Meele.Contains(objectName))
            {
                if (Menu.MeeleOption.State)
                {
                    itemColor = Menu.MeeleColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Consumables.Contains(objectName))
            {
                if (Menu.ConsumablesOption.State)
                {
                    itemColor = Menu.ConsumablesColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Vests.Contains(objectName))
            {
                if (Menu.VestsOption.State)
                {
                    itemColor = Menu.VestsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Eyewear.Contains(objectName))
            {
                if (Menu.EyewearOption.State)
                {
                    itemColor = Menu.EyewearColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Gloves.Contains(objectName))
            {
                if (Menu.GlovesOption.State)
                {
                    itemColor = Menu.GlovesColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Headgear.Contains(objectName))
            {
                if (Menu.HeadgearOption.State)
                {
                    itemColor = Menu.HeadgearColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Masks.Contains(objectName))
            {
                if (Menu.MasksOption.State)
                {
                    itemColor = Menu.MasksColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Pants.Contains(objectName))
            {
                if (Menu.PantsOption.State)
                {
                    itemColor = Menu.PantsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Shoes.Contains(objectName))
            {
                if (Menu.ShoesOption.State)
                {
                    itemColor = Menu.ShoesColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.ShirtsJackets.Contains(objectName))
            {
                if (Menu.ShirtJacketsOption.State)
                {
                    itemColor = Menu.ShirtJacketsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Backpacks.Contains(objectName))
            {
                if (Menu.BackpacksOption.State)
                {
                    itemColor = Menu.BackpacksColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Container.Contains(objectName))
            {
                if (Menu.ContainerOption.State)
                {
                    itemColor = Menu.ContainerColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Medical.Contains(objectName))
            {
                if (Menu.MedicalOption.State)
                {
                    itemColor = Menu.MedicalColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Drink.Contains(objectName))
            {
                if (Menu.DrinkOption.State)
                {
                    itemColor = Menu.DrinkColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Food.Contains(objectName))
            {
                if (Menu.FoodOption.State)
                {
                    itemColor = Menu.FoodColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Tools.Contains(objectName))
            {
                if (Menu.ToolsOption.State)
                {
                    itemColor = Menu.ToolsColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Crafting.Contains(objectName))
            {
                if (Menu.CraftingOption.State)
                {
                    itemColor = Menu.CraftingColorOption.GetColor();
                    draw = true;
                }
            }
            else if (ItemFilter.Misc.Contains(objectName))
            {
                if (Menu.MiscOption.State)
                {
                    itemColor = Menu.MiscColorOption.GetColor();
                    draw = true;
                }
            }
            else if(ItemFilter.Crashes.Contains(objectName))
            {
                if (Menu.EspWreckOption.State)
                {
                    itemColor = Menu.WreckColorOption.GetColor();
                    draw = true;
                    itemName = objectName;
                }
            }
            else if(ItemFilter.Ignore.Contains(objectName))
            {
                draw = false;
            }
            else
            {
                if (Menu.UnkownOption.State)
                {
                    itemColor = Menu.UnknownColorOption.GetColor();
                    draw = true;
                }
                //NonBlockingConsole.WriteLine(objectName);
            }

            if (draw)
            {
                string text = string.Empty;
                if (isRuined)
                    text = string.Format("{0}[{1}] RUINED", itemName, dist.ToString("00"));
                else
                    text = string.Format("{0}[{1}]", itemName, dist.ToString("00"));

                DirectXUI.DrawShadowText(DirectXUI.UIFont, text, position.X - (itemName.Length + dist.ToString().Length), position.Y, itemColor);
            }
        }
    }
}
