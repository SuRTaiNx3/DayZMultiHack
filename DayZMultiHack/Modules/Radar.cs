using DayZMultiHack.Models;
using ExternalD3D11;
using ExternalD3D11.Console;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class Radar : BaseModule
    {
        #region Globals

        // Basic
        private Bitmap _bitmap;
        private float imageSize = 625;
        private float ingameMapSize = 15360f;
        private float imageMapSize = 4800;
        private float padding = 0;
        private int lineLength = 25;
        private int dotSize = 2;
        private int strokeAdd = 2;

        // Commands
        private ConsoleCommand radarCommand;

        #endregion

        #region Constructor

        public Radar(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            _bitmap = DirectXUI.LoadImageFromFile("radarControlImage.jpg");
            InitializeCommands();
        }

        #endregion

        #region Methods

        #region Commands

        private void InitializeCommands()
        {
            radarCommand = new ConsoleCommand();
            radarCommand.Command = "radar";
            radarCommand.Description = "Gives access to radar properties.";
            radarCommand.Syntax = "radar <setting> [value]";
            radarCommand.Receiver = new ConsoleCommand.OnCommandEventHandler(RadarCommandExecuted);
            radarCommand.AutoCompleteCollection = new List<string>() { "visible", "enemies", "enemylines", "zombies", "tn", "tnlines", "cars", "ships", "helicopter", "planes" };
            DirectXUI.Console.Commands.Add(radarCommand);
        }

        private void RadarCommandExecuted(ConsoleCommandArgs e)
        {
            if (e.CommandData.Length < 2)
            {
                e.Callback("Paramter missing! Syntax: " + e.Command.Syntax);
                return;
            }
            else if(e.CommandData[1] != "true" && e.CommandData[1] != "false")
            {
                e.Callback("Wrong parameter '" + e.CommandData[1] + "'! true or false expected!");
                return;
            }

            // Get boolean
            bool value = bool.Parse(e.CommandData[1]);

            switch (e.CommandData[0])
            {
                case "visible":
                    Menu.ShowRadarOption.State = value;
                    e.Callback(string.Format("Radar Visibility option set to '{0}'.", value.ToString()));
                    break;
                case "enemies":
                    Menu.RadarEnemiesOption.State = value;
                    e.Callback(string.Format("Radar Enemies option set to '{0}'.", value.ToString()));
                    break;
                case "enemylines":
                    Menu.RadarEnemyLinesOption.State = value;
                    e.Callback(string.Format("Radar Enemy lines option set to '{0}'.", value.ToString()));
                    break;
                case "zombies":
                    Menu.RadarZombiesOption.State = value;
                    e.Callback(string.Format("Radar Zombies option set to '{0}'.", value.ToString()));
                    break;
                case "tn":
                    Menu.RadarTNOption.State = value;
                    e.Callback(string.Format("Radar TN option set to '{0}'.", value.ToString()));
                    break;
                case "tnlines":
                    Menu.RadarTNLinesOption.State = value;
                    e.Callback(string.Format("Radar TN Lines option set to '{0}'.", value.ToString()));
                    break;
                case "cars":
                    Menu.RadarCarsOption.State = value;
                    e.Callback(string.Format("Radar Cars option set to '{0}'.", value.ToString()));
                    break;
                case "ships":
                    Menu.RadarShipsOption.State = value;
                    e.Callback(string.Format("Radar Ships option set to '{0}'.", value.ToString()));
                    break;
                case "helicopter":
                    Menu.RadarHelicoptersOption.State = value;
                    e.Callback(string.Format("Radar Helicopter option set to '{0}'.", value.ToString()));
                    break;
                case "planes":
                    Menu.RadarPlanesOption.State = value;
                    e.Callback(string.Format("Radar Planes option set to '{0}'.", value.ToString()));
                    break;
                default:
                    e.Callback(string.Format("Option '{0}' not found!", e.CommandData[0]));
                    break;
            }
        }

        #endregion

        public void Draw()
        {
            if (!Menu.ShowRadarOption.State)
                return;

            float imageHalfWidth = imageSize / 2;
            float ingameCoordsToImageRatio = ingameMapSize / imageMapSize;

            // Position on screen
            float screenTop = DirectXUI.Height - imageSize - padding;
            float screenBottom = DirectXUI.Height - padding;
            float screenLeft = padding;
            float screenRight = imageSize + padding;

            // Map center
            float mapCenterX = (MainModule.LocalEntity.Position.X / ingameCoordsToImageRatio) - imageHalfWidth;
            float mapcenterY = ((ingameMapSize - MainModule.LocalEntity.Position.Y) / ingameCoordsToImageRatio) - imageHalfWidth;

            // Image location shifting
            float top = mapcenterY;
            float bottom = (top + imageSize);
            float left = mapCenterX;
            float right = (left + imageSize);

            // Local entity position
            float localEntityX = (MainModule.LocalEntity.Position.X / ingameCoordsToImageRatio) - mapCenterX + screenLeft;
            float localEntityY = ((ingameMapSize - MainModule.LocalEntity.Position.Y) / ingameCoordsToImageRatio) - mapcenterY + screenTop;

            // Draw image
            DirectXUI.DrawBitmap(_bitmap, Menu.RadarOpacityOption.Value, new RawRectangleF(screenLeft, screenTop, screenRight, screenBottom), new RawRectangleF(left, top, right, bottom));

            // Draw network range circle
            DirectXUI.DrawCircle(localEntityX, localEntityY, 312.5f, 1, new Color(255, 255, 255, 100));


            foreach (Entity entity in MainModule.EntitiesList.Entities)
            {
                if (entity.IsDead)
                    continue;

                // Entity position relative to screen
                float entityX = (entity.Position.X / ingameCoordsToImageRatio) - mapCenterX + screenLeft;
                float entityY = ((ingameMapSize - entity.Position.Y) / ingameCoordsToImageRatio) - mapcenterY + screenTop;

                switch (entity.Type)
                {
                    case Entity.Types.Soldier:
                        DrawSoldier(entity, entityX, entityY, localEntityX, localEntityY);
                        break;
                    case Entity.Types.Zombie:
                        DrawZombie(entity, entityX, entityY);
                        break;
                    case Entity.Types.Car:
                        DrawVehicle(entity, entityX, entityY, localEntityX, localEntityY, "Car", Menu.CarsColorOption.GetColor(), Menu.RadarCarsOption.State);
                        break;
                    case Entity.Types.Ship:
                        DrawVehicle(entity, entityX, entityY, localEntityX, localEntityY, "Ship", Menu.ShipsColorOption.GetColor(), Menu.RadarShipsOption.State);
                        break;
                    case Entity.Types.Helicopter:
                        DrawVehicle(entity, entityX, entityY, localEntityX, localEntityY, "Helicopter", Menu.HelicopterColorOption.GetColor(), Menu.RadarHelicoptersOption.State);
                        break;
                    case Entity.Types.Airplane:
                        DrawVehicle(entity, entityX, entityY, localEntityX, localEntityY, "Airplane", Menu.PlanesColorOption.GetColor(), Menu.RadarPlanesOption.State);
                        break;
                    case Entity.Types.Unknown:
                        break;
                    default:
                        break;
                }
            }
        }

        private void DrawSoldier(Entity entity, float x, float y, float localPlayerX, float localPlayerY)
        {
            string text = string.Empty;
            if (Menu.RadarShowDistance.State)
                text = string.Format("{0}[{1}]\n{2}", entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand);
            else
                text = string.Format("{0}\n{1}", entity.Name, entity.WeaponInHand);


            if (entity.IsTN)
            {
                if (!Menu.RadarTNOption.State)
                    return;

                if (Menu.RadarTNLinesOption.State && entity.Pointer != MainModule.LocalEntity.Pointer)
                    DirectXUI.DrawLine(x, y, localPlayerX, localPlayerY, 1, Menu.TNLineColorOption.GetColor());

                DrawLine(entity, x, y, Menu.TNColorOption.GetColor());
                DirectXUI.DrawCircle(x, y, dotSize, dotSize + strokeAdd, Menu.TNColorOption.GetColor());

                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, text, x + 5, y + 5, Menu.TNColorOption.GetColor());
            }
            else
            {
                if (!Menu.RadarEnemiesOption.State)
                    return;

                if (Menu.RadarEnemyLinesOption.State)
                    DirectXUI.DrawLine(x, y, localPlayerX, localPlayerY, 1, Menu.PlayerLineColorOption.GetColor());

                DrawLine(entity, x, y, Menu.PlayerColorOption.GetColor());
                DirectXUI.DrawCircle(x, y, dotSize, dotSize + strokeAdd, Menu.PlayerColorOption.GetColor());
                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, text, x + 5, y + 5, Menu.PlayerColorOption.GetColor());
            }
        }

        private void DrawZombie(Entity entity, float x, float y)
        {
            if (!Menu.RadarZombiesOption.State)
                return;

            DirectXUI.DrawCircle(x, y, dotSize, dotSize + strokeAdd, Menu.ZombieColorOption.GetColor());
            //DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, "Zombie", x + 5, y + 5, Menu.ZombieColorOption.GetColor());
        }

        private void DrawVehicle(Entity entity, float x, float y, float localPlayerX, float localPlayerY, string vehicleTypeName, Color color, bool showNonOccopied)
        {
            string text = string.Empty;            

            Color vehicleColor = color;
            if (entity.Driver.Pointer != IntPtr.Zero && Menu.RadarOccupiedVehicle.State)
            {
                vehicleColor = Menu.OccupiedVehicleColorOption.GetColor();
                Color vehicleLineColor = Menu.OccupiedVehicleLineColorOption.GetColor();

                if (entity.Driver.Name.Contains("[TN]"))
                {
                    vehicleColor = Menu.TNColorOption.GetColor();
                    vehicleLineColor = Menu.TNLineColorOption.GetColor();
                }

                if (Menu.RadarOccupiedVehicleLine.State)
                    DirectXUI.DrawLine(x, y, localPlayerX, localPlayerY, 1, vehicleLineColor);

                DirectXUI.DrawCircle(x, y, dotSize, dotSize + strokeAdd, vehicleColor);

                // Text
                if (Menu.RadarShowDistance.State)
                    text = string.Format("{0}[{1}] - {2}\nDriver: {3}", vehicleTypeName, entity.Distance.ToString("00"), entity.ObjectName, entity.Driver.Name);
                else
                    text = string.Format("{0} - {1}\nDriver: {2}", vehicleTypeName, entity.ObjectName, entity.Driver.Name);

                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, text, x + 5, y + 5, vehicleColor);
            }
            else if(showNonOccopied)
            {
                // Text
                if (Menu.RadarShowDistance.State)
                    text = string.Format("{0}[{1}] - {2}", vehicleTypeName, entity.Distance.ToString("00"), entity.ObjectName);
                else
                    text = string.Format("{0} - {1}", vehicleTypeName, entity.ObjectName);

                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, text, x + 5, y + 5, vehicleColor);
            }
        }

        private void DrawLine(Entity entity, float entityX, float entityY, Color color)
        {
            double direction = Math.Atan2(entity.DirectionY, entity.DirectionX) * 57.295779513082323;

            if (direction < 0.0)
                direction = 360.0 + direction;

            float x = (float)((entityX + 0.1f) + Math.Cos(direction * Math.PI / 180.0 - 1.5707963267948966) * lineLength);
            float y = (float)((entityY + 0.1f) + Math.Sin(direction * Math.PI / 180.0 - 1.5707963267948966) * lineLength);

            DirectXUI.DrawLine(entityX + 0.1f, entityY + 0.1f, x, y, 2, color);
        }

        #endregion
    }
}
