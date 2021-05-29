using D3DMenu.MenuTypes;
using DayZMultiHack.Models;
using ExternalD3D11;
using ExternalD3D11.Console;
using ExternalD3D11.ScrollMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class MurderMode : BaseModule
    {
        #region Globals

        // Basic
        private bool _controlWasPressed = false;
        private bool _prefMurderModeState = false;
        private bool _resetMurderModeOnKey = false;
        private MurderModeType.Type _prefMurderModeType = MurderModeType.Type.Soldier;

        private float ToggleMurderModeOptionRadius = 100;

        // Commands
        private ConsoleCommand murderModeCommand;

        // Scroll menu items
        ScrollMenuFunctionItem scrollMenuItemTarget;
        ScrollMenuFunctionItem scrollMenuItemTargetActivate;

        #endregion

        #region Constructor

        public MurderMode(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            InitializeCommands();

            // Add to quick menu
            //ScrollMenuCategory murderModeCategory = new ScrollMenuCategory();
            //murderModeCategory.Title = "Murder Mode";
            //DirectXUI.ScrollMenu.Categories.Add(murderModeCategory);

            //scrollMenuItemTarget = new ScrollMenuFunctionItem("", SetNewTarget, TargetVisibilityCheck);
            //scrollMenuItemTargetActivate = new ScrollMenuFunctionItem("", SetNewTargetAndActivate, TargetVisibilityCheck);
            //murderModeCategory.Items.Add(scrollMenuItemTarget);
            //murderModeCategory.Items.Add(scrollMenuItemTargetActivate);
        }

        #endregion

        #region Methods

        #region Commands

        private void InitializeCommands()
        {
            murderModeCommand = new ConsoleCommand();
            murderModeCommand.Command = "murdermode";
            murderModeCommand.Description = "Gives access to murder mode properties.";
            murderModeCommand.Syntax = "murdermode <setting> [value]";
            murderModeCommand.Receiver = new ConsoleCommand.OnCommandEventHandler(MurderModeCommandExecuted);
            murderModeCommand.AutoCompleteCollection = new List<string>() { "enabled", "type", "position", "useplayerselection"};
            DirectXUI.Console.Commands.Add(murderModeCommand);
        }

        private void MurderModeCommandExecuted(ConsoleCommandArgs e)
        {
            string parameter1 = string.Empty;

            if(e.CommandData.Length > 1)
                 parameter1 = e.CommandData[1].ToLower();

            switch (e.CommandData[0])
            {
                case "enabled":
                    if (e.CommandData.Length < 2)
                        e.Callback("Missing paramter! Syntax: murdermode enabled <true|false>");
                    else if (parameter1 != "true" && parameter1 != "false")
                        e.Callback("Wrong paramter! Syntax: murdermode enabled <true|false>");
                    else
                    {
                        Menu.MurderModeOption.State = bool.Parse(parameter1);
                        e.Callback(string.Format("Murder mode enabled: '{0}'", parameter1));
                    }
                    break;
                case "type":
                    if (e.CommandData.Length < 2)
                        e.Callback("Missing paramter! Syntax: murdermode type <soldier|zombie|unknown>");
                    else if (parameter1 != "soldier" && parameter1 != "zombie" && parameter1 != "unknown")
                        e.Callback("Wrong paramter! Syntax: murdermode type <soldier|zombie|unknown>");
                    else
                    {
                        Menu.MurderModeTypeOption.SelectedType = (MurderModeType.Type)Enum.Parse(typeof(MurderModeType.Type), parameter1, true);
                        e.Callback(string.Format("Murder mode type is now set to: '{0}'", parameter1));
                    }
                    break;
                case "position":
                    if (e.CommandData.Length < 2)
                        e.Callback("Missing paramter! Syntax: murdermode position <head|chest|feet>");
                    else if (parameter1 != "head" && parameter1 != "chest" && parameter1 != "feet")
                        e.Callback("Wrong paramter! Syntax: murdermode position <head|chest|feet>");
                    else
                    {
                        Menu.MurderModePositionOption.SelectedPosition = (MurderModePosition.Positions)Enum.Parse(typeof(MurderModePosition.Positions), parameter1, true);
                        e.Callback(string.Format("Murder mode position is now set to: '{0}'", parameter1));
                    }
                    break;
                case "useplayerselection":
                    if (e.CommandData.Length < 2)
                        e.Callback("Missing paramter! Syntax: murdermode useplayerselection <true|false>");
                    else if (parameter1 != "true" && parameter1 != "false")
                        e.Callback("Wrong paramter! Syntax: murdermode useplayerselection <true|false>");
                    else
                    {
                        Menu.MurderModeUsePlayerSelection.State = bool.Parse(parameter1);
                        e.Callback(string.Format("Murder mode use player selection enabled: '{0}'", parameter1));
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        public void Run()
        {
            Entity entity;

            if (Menu.MurderModeUsePlayerSelection.State)
            {
                entity = MainModule.EntitiesList.Entities.FirstOrDefault(e => Menu.MurderModeSelectedPlayer.SelectedEnemy != null &&
                        e.Pointer == Menu.MurderModeSelectedPlayer.SelectedEnemy.Pointer);
            }
            else
            {
                entity = GetNearestEntity();
            }

            if (!Menu.MurderModeOption.State)
                return;

            // Murder Mode Victim
            string murderModeVictimName = "NONE";
            if (entity.Type == Entity.Types.Soldier)
                murderModeVictimName = entity.Name;
            else if (entity.Type == Entity.Types.Zombie)
                murderModeVictimName = "Zombie";
            else if (entity.Name == "PlayerName")
                murderModeVictimName = "Unknown Player";

            murderModeVictimName += "[" + entity.Distance + "]";

            DirectXUI.DrawWarningBox("Murder Mode enabled!", "Victim: " + murderModeVictimName, 60, 220);

            if (Menu.MurderModeOnKeyOption.State && !DirectXUI.Control_Pressed)
            {
                if (_controlWasPressed)
                {
                    Menu.MurderModeTypeOption.SelectedType = _prefMurderModeType;
                    Menu.MurderModeOption.State = _prefMurderModeState;
                    _controlWasPressed = false;
                }

                _resetMurderModeOnKey = true;
            }
            else if (Menu.MurderModeOnKeyOption.State && DirectXUI.Control_Pressed)
            {
                if (!_controlWasPressed)
                {
                    _prefMurderModeType = Menu.MurderModeTypeOption.SelectedType;
                    _prefMurderModeState = Menu.MurderModeOption.State;
                }

                Menu.MurderModeTypeOption.SelectedType = MurderModeType.Type.Soldier;
                Menu.MurderModeOption.State = true;
                _controlWasPressed = true;
            }
            else if (!Menu.MurderModeOnKeyOption.State && _resetMurderModeOnKey)
            {
                Menu.MurderModeOption.State = false;
                _resetMurderModeOnKey = false;
            }

            if(Menu.MurderModeOption.State)
                Kill(entity);

            return;
        }

        private void SetNewTarget()
        {
            Menu.MurderModeUsePlayerSelection.State = false;

            // Get Player
            PlayerSelection.Player newSelectedPlayer = Menu.MurderModeSelectedPlayer.Players.FirstOrDefault(p => p.Pointer == MainModule.NearestEntityToCursor.Pointer);

            if (newSelectedPlayer != null)
                Menu.MurderModeSelectedPlayer.SelectedEnemy = newSelectedPlayer;
        }

        private void SetNewTargetAndActivate()
        {
            SetNewTarget();
            Menu.MurderModeOption.State = true;
            Menu.MurderModeUsePlayerSelection.State = true;
        }

        private void TargetVisibilityCheck(ScrollMenuBaseItem item)
        {
            int x = DirectXUI.Width / 2;
            int y = DirectXUI.Height / 2;

            ScrollMenuFunctionItem murderModeTargetItem = item as ScrollMenuFunctionItem;

            double distance = DistanceToPoint(MainModule.NearestEntityToCursor, x, y);

            if (distance > 0 && distance <= ToggleMurderModeOptionRadius)
            {
                item.IsVisible = true;
                scrollMenuItemTarget.Text = string.Format("Set '{0}' as Target", MainModule.NearestEntityToCursor.Name);
                scrollMenuItemTargetActivate.Text = string.Format("Set '{0}' as Target and activate", MainModule.NearestEntityToCursor.Name);
            }
            else
            {
                item.IsVisible = false;
            }
        }

        private static double DistanceToPoint(Entity entity, float x, float y)
        {
            // Current Distance
            float dx = (entity.ScreenHeadPosition.X - x);
            float dy = (entity.ScreenHeadPosition.Y - y);
            double distance = Math.Sqrt((dx * dx) + (dy * dy));

            return distance;
        }

        private Entity GetNearestEntity()
        {
            Entity nearestEntity = new Entity();
            switch (Menu.MurderModeTypeOption.SelectedType)
            {
                case MurderModeType.Type.Soldier:
                    nearestEntity = MainModule.EntitiesList.Entities.Where(e => !e.IsTN && e.Type == Entity.Types.Soldier && e.Name != "PlayerName" && !e.IsDead && e.Pointer != MainModule.LocalEntity.Pointer).OrderBy(e => e.Distance).FirstOrDefault();
                    break;
                case MurderModeType.Type.Zombie:
                    nearestEntity = MainModule.EntitiesList.Entities.Where(e => !e.IsTN && e.Type == Entity.Types.Zombie && !e.IsDead).OrderBy(e => e.Distance).FirstOrDefault();
                    break;
                case MurderModeType.Type.Unknown:
                    nearestEntity = MainModule.EntitiesList.Entities.Where(e => !e.IsTN && e.Name == "PlayerName" && e.Type == Entity.Types.Soldier).OrderBy(e => e.Distance).FirstOrDefault();
                    break;
                default:
                    break;
            }

            return nearestEntity;
        }

        private void Kill(Entity entity)
        {
            IntPtr bulletTable = Mem.Read<IntPtr>(MainModule.WorldBase + Offsets.World.BULLET_TABLE);
            int bulletTableSize = Mem.Read<int>(MainModule.WorldBase + (Offsets.World.BULLET_TABLE + 0x4));

            for (int i = 0; i < bulletTableSize; i++)
            {   
                IntPtr bulletPtr = Mem.Read<IntPtr>(bulletTable + (i * 0x4));
                IntPtr visualState = Mem.Read<IntPtr>(bulletPtr + Offsets.Bullet.VisualState.BASE);
                Vector3 vec = Mem.ReadVector3(visualState + Offsets.Bullet.VisualState.POSITION_VEC3_START);

                if (entity.Type == Entity.Types.Soldier || entity.Type == Entity.Types.Zombie || entity.Name == "PlayerName")
                    Mem.WriteVector3(visualState + Offsets.Bullet.VisualState.POSITION_VEC3_START, entity.HeadPosition.X, entity.HeadPosition.Z, entity.HeadPosition.Y);
            }
        }

        #endregion
    }
}
