using DayZMultiHack.Models;
using ExternalD3D11;
using ExternalD3D11.Console;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class InventoryDisplay : BaseModule
    {
        #region Globals

        private ConsoleCommand inventoryCommands;

        #endregion

        #region Constructor

        public InventoryDisplay(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            InitializeCommands();
        }

        #endregion

        #region Methods

        #region Commands

        private void InitializeCommands()
        {
            inventoryCommands = new ConsoleCommand();
            inventoryCommands.Command = "inventories";
            inventoryCommands.Description = "Toggles some properties for the inventories module.";
            inventoryCommands.Syntax = "inventories enable <true|false>";
            inventoryCommands.Receiver = new ConsoleCommand.OnCommandEventHandler(InventoriesCommandExecuted);
            inventoryCommands.AutoCompleteCollection = new List<string>() { "enable" };
            DirectXUI.Console.Commands.Add(inventoryCommands);
        }

        private void InventoriesCommandExecuted(ConsoleCommandArgs e)
        {
            if (e.CommandData.Length < 2)
            {
                e.Callback("Paramter missing! Syntax: " + e.Command.Syntax);
                return;
            }
            else if (e.CommandData[1] != "true" && e.CommandData[1] != "false")
            {
                e.Callback("Wrong parameter '" + e.CommandData[1] + "'! true or false expected!");
                return;
            }

            bool value = bool.Parse(e.CommandData[1]);
            Menu.InventoriesEnabledOption.State = value;
            e.Callback(string.Format("Show inventory enabled: '{0}'.", e.CommandData[1]));
        }

        #endregion

        public Entity Draw()
        {

            Entity entity = MainModule.EntitiesList.Entities.FirstOrDefault(e => Menu.InventoriesSelectedPlayerOption.SelectedEnemy != null && 
            e.Pointer == Menu.InventoriesSelectedPlayerOption.SelectedEnemy.Pointer);

            if (!Menu.InventoriesEnabledOption.State || entity.Pointer == IntPtr.Zero)
                return entity;

            float boxY = 55;
            float boxWidth = 200;
            float boxX = DirectXUI.Width - boxWidth - 205;
            float textX = boxX + 3;
            float height = DirectXUI.Height - boxY - 50;

            DirectXUI.DrawBox(boxX, boxY, boxWidth, height, 1, Color.Black);
            DirectXUI.DrawTransparentBox(boxX, boxY, boxWidth, height, Color.White, 100);

            //Content box
            DirectXUI.DrawFilledBox(boxX, boxY, boxWidth, height, new Color(216, 216, 216));
            DirectXUI.DrawBox(boxX, boxY, boxWidth, height, 1, new Color(60, 60, 60));

            //Title
            DirectXUI.DrawTransparentBox(boxX, boxY, boxWidth, 23, new Color(40, 40, 40), 248);
            DirectXUI.DrawBox(boxX, boxY, boxWidth, 23, 1, Color.Black);
            DirectXUI.DrawShadowText(DirectXUI.HeadlineFont, "Inv of " + entity.Name, boxX + 5, boxY + 3, Color.White);

            // Layout values
            float initialY = 80;
            float startY = initialY;
            float tabbedText = 20;
            float tabbedSubText = tabbedText + 20;
            float slotTextHeight = DirectXUI.MeasureText("Test", DirectXUI.BaseFont).Height - 3;

            Inventory inventory = GetInventory(entity.Pointer);

            DirectXUI.DrawShadowText(DirectXUI.HeadlineFont, entity.Name + " [" + entity.Distance + "]", boxX, startY, Color.Black);
            startY += 15;

            // Primary
            if (inventory.Primary.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Primary", textX, startY, Color.Black);
                startY += slotTextHeight;

                //if(inventory.Primary.IsRuined)
                //    D3D.DrawStrikeoutText
                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Primary.RealName, textX + tabbedText, startY, GetItemColor(inventory.Primary.ObjectName));
                startY += slotTextHeight;

                if (inventory.Primary.HasContainer && inventory.Primary.Container != null)
                {
                    foreach (Item item in inventory.Primary.Container)
                    {
                        DirectXUI.DrawBaseText(DirectXUI.UIFont, item.RealName, textX + tabbedSubText, startY, GetItemColor(item.ObjectName));
                        startY += slotTextHeight;
                    }
                }
            }

            // Melee
            if (inventory.Meele.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Meele", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Meele.RealName, textX + tabbedText, startY, GetItemColor(inventory.Meele.ObjectName));
                startY += slotTextHeight;
            }

            // Helmet
            if (inventory.Helmet.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Helmet", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Helmet.RealName, textX + tabbedText, startY, GetItemColor(inventory.Helmet.ObjectName));
                startY += slotTextHeight;
            }

            // Mask
            if (inventory.Mask.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Mask", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Mask.RealName, textX + tabbedText, startY, GetItemColor(inventory.Mask.ObjectName));
                startY += slotTextHeight;
            }

            // Glasses
            if (inventory.Glasses.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Glasses", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Glasses.RealName, textX + tabbedText, startY, GetItemColor(inventory.Glasses.ObjectName));
                startY += slotTextHeight;
            }

            // Chest
            if (inventory.Chest.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Chest", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Chest.RealName, textX + tabbedText, startY, GetItemColor(inventory.Chest.ObjectName));
                startY += slotTextHeight;

                if (inventory.Chest.HasContainer && inventory.Chest.Container != null)
                {
                    foreach (Item item in inventory.Chest.Container)
                    {
                        if (item.Pointer != IntPtr.Zero)
                        {
                            DirectXUI.DrawBaseText(DirectXUI.UIFont, item.RealName, textX + tabbedSubText, startY, GetItemColor(item.ObjectName));
                            startY += slotTextHeight;
                        }
                    }
                }
            }

            // Pants
            if (inventory.Pants.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Pants", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Pants.RealName, textX + tabbedText, startY, GetItemColor(inventory.Pants.ObjectName));
                startY += slotTextHeight;

                if (inventory.Pants.HasContainer && inventory.Pants.Container != null)
                {
                    foreach (Item item in inventory.Pants.Container)
                    {
                        if (item.Pointer != IntPtr.Zero)
                        {
                            DirectXUI.DrawBaseText(DirectXUI.UIFont, item.RealName, textX + tabbedSubText, startY, GetItemColor(item.ObjectName));
                            startY += slotTextHeight;
                        }
                    }
                }
            }

            // Shoes
            if (inventory.Shoes.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Shoes", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Shoes.RealName, textX + tabbedText, startY, GetItemColor(inventory.Shoes.ObjectName));
                startY += slotTextHeight;
            }

            // Vest
            if (inventory.Vest.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Vest", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Vest.RealName, textX + tabbedText, startY, GetItemColor(inventory.Vest.ObjectName));
                startY += slotTextHeight;

                if (inventory.Vest.HasContainer && inventory.Vest.Container != null)
                {
                    foreach (Item item in inventory.Vest.Container)
                    {
                        if (item.Pointer != IntPtr.Zero)
                        {
                            DirectXUI.DrawBaseText(DirectXUI.UIFont, item.RealName, textX + tabbedSubText, startY, GetItemColor(item.ObjectName));
                            startY += slotTextHeight;
                        }
                    }
                }
            }

            // Gloves
            if (inventory.Gloves.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Gloves", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Gloves.RealName, textX + tabbedText, startY, GetItemColor(inventory.Gloves.ObjectName));
                startY += slotTextHeight;
            }

            // Backpack
            if (inventory.Backpack.Pointer != IntPtr.Zero)
            {
                DirectXUI.DrawBaseText(DirectXUI.BoldFont, "- Backpack", textX, startY, Color.Black);
                startY += slotTextHeight;

                DirectXUI.DrawBaseText(DirectXUI.UIFont, inventory.Backpack.RealName, textX + tabbedText, startY, GetItemColor(inventory.Backpack.ObjectName));
                startY += slotTextHeight;

                if (inventory.Backpack.HasContainer && inventory.Backpack.Container != null)
                {
                    foreach (Item item in inventory.Backpack.Container)
                    {
                        if (item.Pointer != IntPtr.Zero)
                        {
                            DirectXUI.DrawBaseText(DirectXUI.UIFont, item.RealName, textX + tabbedSubText, startY, GetItemColor(item.ObjectName));
                            startY += slotTextHeight;
                        }
                    }
                }
            }

            startY += 30;
            return entity;
        }

        private Inventory GetInventory(IntPtr playerPtr)
        {
            Inventory inventory = new Inventory();

            // Primary
            if (Menu.InventriesShowPrimaryOption.State)
            {
                IntPtr primaryItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.PRIMARY);
                Item primaryItem = GetItem(primaryItemPtr);

                if (primaryItem.Pointer != IntPtr.Zero)
                    inventory.Primary = primaryItem;
            }

            // Meele
            if (Menu.InventriesShowMeleeOption.State)
            {
                IntPtr meleeItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.MELEE);
                Item meleeItem = GetItem(meleeItemPtr);

                if (meleeItem.Pointer != IntPtr.Zero)
                    inventory.Melee = meleeItem;
            }

            // Helmet
            if (Menu.InventriesShowHelmetOption.State)
            {
                IntPtr helmetItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.HELMET);
                Item helmetItem = GetItem(helmetItemPtr);

                if (helmetItem.Pointer != IntPtr.Zero)
                    inventory.Helmet = helmetItem;
            }

            // Mask
            if (Menu.InventriesShowMaskOption.State)
            {
                IntPtr maskItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.MASK);
                Item maskItem = GetItem(maskItemPtr);

                if (maskItem.Pointer != IntPtr.Zero)
                    inventory.Mask = maskItem;
            }

            // Glasses
            if (Menu.InventriesShowGlassesOption.State)
            {
                IntPtr glassesItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.GLASSES);
                Item glassesItem = GetItem(glassesItemPtr);

                if (glassesItem.Pointer != IntPtr.Zero)
                    inventory.Glasses = glassesItem;
            }

            // Chest
            if (Menu.InventriesShowChestOption.State)
            {
                IntPtr chestItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.CHEST);
                Item chestItem = GetItem(chestItemPtr);

                if (chestItem.Pointer != IntPtr.Zero)
                    inventory.Chest = chestItem;
            }

            // Pants
            if (Menu.InventriesShowPantsOption.State)
            {
                IntPtr pantsItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.PANTS);
                Item pantsItem = GetItem(pantsItemPtr);

                if (pantsItem.Pointer != IntPtr.Zero)
                    inventory.Pants = pantsItem;
            }

            // Shoes
            if (Menu.InventriesShowShoesOption.State)
            {
                IntPtr shoesItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.SHOES);
                Item shoesItem = GetItem(shoesItemPtr);

                if (shoesItem.Pointer != IntPtr.Zero)
                    inventory.Shoes = shoesItem;
            }

            // Vest
            if (Menu.InventriesShowVestOption.State)
            {
                IntPtr vestItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.VEST);
                Item vestItem = GetItem(vestItemPtr);

                if (vestItem.Pointer != IntPtr.Zero)
                    inventory.Vest = vestItem;
            }

            // Gloves
            if (Menu.InventriesShowGlovesOption.State)
            {
                IntPtr glovesItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.GLOVES);
                Item glovesItem = GetItem(glovesItemPtr);

                if (glovesItem.Pointer != IntPtr.Zero)
                    inventory.Gloves = glovesItem;
            }

            // Backpack
            if (Menu.InventriesShowBackpackOption.State)
            {
                IntPtr backpackItemPtr = Mem.Read<IntPtr>(playerPtr + Offsets.Entity.Inventory.BACKPACK);
                Item backpackItem = GetItem(backpackItemPtr);

                if (backpackItem.Pointer != IntPtr.Zero)
                    inventory.Backpack = backpackItem;
            }

            return inventory;
        }

        private Item GetItem(IntPtr itemPtr, bool checkContainer = true)
        {
            if (itemPtr == IntPtr.Zero)
                return new Item();

            // Object class
            IntPtr itemObjectClass = Mem.Read<IntPtr>(itemPtr + Offsets.Item_Animal.ObjectClass.BASE);

            // Real name
            IntPtr itemRealNamePtr = Mem.Read<IntPtr>(itemObjectClass + Offsets.Item_Animal.ObjectClass.REAL_NAME);
            int itemRealNameSize = Mem.Read<int>(itemRealNamePtr + 0x4);

            // Object name
            IntPtr itemObjectNamePtr = Mem.Read<IntPtr>(itemObjectClass + Offsets.Item_Animal.ObjectClass.NAME);
            int itemObjectNameSize = Mem.Read<int>(itemObjectNamePtr + 0x4);


            if (itemRealNamePtr != IntPtr.Zero && itemRealNameSize < 80 && itemRealNameSize > 0)
            {
                Item item = new Item();

                item.Pointer = itemPtr;

                item.RealName = Mem.ReadString(itemRealNamePtr + 0x8, itemRealNameSize, Encoding.ASCII);
                item.ObjectName = Mem.ReadString(itemObjectNamePtr + 0x8, itemObjectNameSize, Encoding.ASCII);

                // Type
                IntPtr itemTypePtr = Mem.Read<IntPtr>(itemObjectClass + Offsets.Item_Animal.ObjectClass.TYPE);
                int itemTypeSize = Mem.Read<int>(itemTypePtr + 0x4);
                item.Type = Mem.ReadString(itemTypePtr + 0x8, 64, Encoding.ASCII);

                // Is Ruined
                Int16 isRuined = Mem.Read<Int16>(itemPtr + Offsets.Item_Animal.ISRUINED);
                if (isRuined == 1)
                    return new Item();

                // Check if it has a container
                if (checkContainer)
                {
                    IntPtr containerPtr = Mem.Read<IntPtr>(itemPtr + Offsets.Item_Animal.CONTAINER);
                    if (containerPtr != IntPtr.Zero)
                    {
                        item.HasContainer = true;
                        int containerSize = Mem.Read<int>(containerPtr + 0x10);
                        IntPtr containerItemsPtr = Mem.Read<IntPtr>(containerPtr + 0xC);

                        for (int i = 0; i < containerSize; i++)
                        {
                            IntPtr containerItemPtr = Mem.Read<IntPtr>(containerItemsPtr + (i * 0x4));
                            if (containerItemPtr != IntPtr.Zero)
                            {
                                if (item.Container == null)
                                    item.Container = new List<Item>();

                                Item containerItem = GetItem(containerItemPtr, false);
                                item.Container.Add(containerItem);
                            }
                        }
                    }
                }

                return item;
            }

            return new Item();
        }

        private Color GetItemColor(string objectName)
        {
            if (ItemFilter.Weapons.Contains(objectName))
                return Menu.WeaponsColorOption.GetColor();
            else if (ItemFilter.Attachments.Contains(objectName))
                return Menu.AttachmentsColorOption.GetColor();
            else if (ItemFilter.Mags.Contains(objectName))
                return Menu.MagsColorOption.GetColor();
            else if (ItemFilter.Ammo.Contains(objectName))
                return Menu.AmmoColorOption.GetColor();
            else if (ItemFilter.Grenades.Contains(objectName))
                return Menu.GrenadesColorOption.GetColor();
            else if (ItemFilter.Pistols.Contains(objectName))
                return Menu.PistolsColorOption.GetColor();
            else if (ItemFilter.Meele.Contains(objectName))
                return Menu.MeeleColorOption.GetColor();
            else if (ItemFilter.Consumables.Contains(objectName))
                return Menu.ConsumablesColorOption.GetColor();
            else if (ItemFilter.Vests.Contains(objectName))
                return Menu.VestsColorOption.GetColor();
            else if (ItemFilter.Eyewear.Contains(objectName))
                return Menu.EyewearColorOption.GetColor();
            else if (ItemFilter.Gloves.Contains(objectName))
                return Menu.GlovesColorOption.GetColor();
            else if (ItemFilter.Headgear.Contains(objectName))
                return Menu.HeadgearColorOption.GetColor();
            else if (ItemFilter.Masks.Contains(objectName))
                return Menu.MasksColorOption.GetColor();
            else if (ItemFilter.Pants.Contains(objectName))
                return Menu.PantsColorOption.GetColor();
            else if (ItemFilter.Shoes.Contains(objectName))
                return Menu.ShoesColorOption.GetColor();
            else if (ItemFilter.ShirtsJackets.Contains(objectName))
                return Menu.ShirtJacketsColorOption.GetColor();
            else if (ItemFilter.Backpacks.Contains(objectName))
                return Menu.BackpacksColorOption.GetColor();
            else if (ItemFilter.Container.Contains(objectName))
                return Menu.ContainerColorOption.GetColor();
            else if (ItemFilter.Medical.Contains(objectName))
                return Menu.MedicalColorOption.GetColor();
            else if (ItemFilter.Drink.Contains(objectName))
                return Menu.DrinkColorOption.GetColor();
            else if (ItemFilter.Food.Contains(objectName))
                return Menu.FoodColorOption.GetColor();
            else if (ItemFilter.Tools.Contains(objectName))
                return Menu.ToolsColorOption.GetColor();
            else if (ItemFilter.Crafting.Contains(objectName))
                return Menu.CraftingColorOption.GetColor();
            else if (ItemFilter.Misc.Contains(objectName))
                return Menu.MiscColorOption.GetColor();
            else
            {
                if (Menu.UnkownOption.State)
                    return Menu.UnknownColorOption.GetColor();
            }

            return Color.Black;
        }

        #endregion
    }
}
