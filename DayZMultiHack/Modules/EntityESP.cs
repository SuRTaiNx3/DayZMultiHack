using D3DMenu.MenuTypes;
using DayZMultiHack.Models;
using ExternalD3D11;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class EntityESP : BaseModule
    {
        private const float entityBoxHalfWidth = 350;
        private const float entityBoxWidth = 600;

        public EntityESP(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Draw()
        {
            foreach (Entity entity in MainModule.EntitiesList.Entities)
            {
                switch (entity.Type)
                {
                    case Entity.Types.Soldier:
                        DrawPlayer(entity);
                        break;
                    case Entity.Types.Zombie:
                        DrawZombie(entity);
                        break;
                    case Entity.Types.Car:
                        DrawVehicle(entity, "Car", Menu.CarsColorOption.GetColor());
                        break;
                    case Entity.Types.Ship:
                        DrawVehicle(entity, "Ship", Menu.ShipsColorOption.GetColor());
                        break;
                    case Entity.Types.Helicopter:
                        DrawVehicle(entity, "Helicopter", Menu.HelicopterColorOption.GetColor());
                        break;
                    case Entity.Types.Airplane:
                        DrawVehicle(entity, "Airplane", Menu.PlanesColorOption.GetColor());
                        break;
                    case Entity.Types.Unknown:
                        Console.WriteLine(entity.TypeString);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DrawPlayer(Entity entity)
        {
            if (!Menu.EspPlayersOption.State || entity.Pointer == MainModule.LocalEntity.Pointer)
                return;

            if (entity.ScreenFootPosition.Z < 0.01f || entity.ScreenFootPosition.X <= 0 || 
                entity.ScreenFootPosition.X > DirectXUI.Width || entity.ScreenFootPosition.Y > DirectXUI.Height)
                return;

            string baseString = string.Empty;
            string fullString = string.Empty;

            if(Menu.EspSmallPlayerStats.State)
            {
                baseString = "{0}[{1}]\n{2}\n{3}{4}\nStats: {5}/{6}/{7}";
                fullString = "{0}[{1}]\n{2}\n{3}{4}\nStats: {5}/{6}/{7}\nTemps: {8}/{9}";
            }
            else
            {
                baseString = "{0}[{1}]\n{2}\n{3}{4}\nHealth: {5}\nBlood: {6}\nShock: {7}";
                fullString = "{0}[{1}]\n{2}\n{3}{4}\nHealth: {5}\nBlood: {6}\nShock: {7}\nTemperature: {8}\nHeat comfort: {9}";
            }


            if (entity.IsTN)
                DrawTNPlayer(entity, fullString, baseString);
            else if(entity.Name == "PlayerName" && !Menu.EspOnlyTNPlayers.State && Menu.EspDeadPlayerOption.State)
                DrawDeadPlayer(entity, baseString);
            else if (!Menu.EspOnlyTNPlayers.State && !entity.IsDead)
                DrawAlivePlayer(entity, baseString, fullString);
        }

        private void DrawTNPlayer(Entity entity, string espTextFull, string espTextSmall)
        {
            // Box
            float headFootDistance = entity.ScreenFootPosition.Y - entity.ScreenHeadPosition.Y;

            float boxX = entity.ScreenHeadPosition.X - (entityBoxHalfWidth / entity.Distance);
            float boxY = entity.ScreenHeadPosition.Y - (200 / entity.Distance);
            float boxWidth = entityBoxWidth / entity.Distance;
            float boxHeight = headFootDistance / entity.Distance;

            if (Menu.EspRoundedBoxesOption.State)
                DirectXUI.DrawBorderEdges(boxX, boxY, boxWidth, headFootDistance, 1, Menu.TNColorOption.GetColor());
            else
                DirectXUI.DrawBox(boxX, boxY, boxWidth, headFootDistance, 1, Menu.TNColorOption.GetColor());


            if (Menu.EspPlayerTemparatureOption.State)
            {

                string text = string.Format(espTextFull, entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand,
                    entity.Stance, entity.Stats.Health, entity.Stats.Blood, entity.Stats.Shock, entity.Stats.Temperature.ToString("00.00°C"),
                    entity.Stats.HeatComfort.ToString("00.00"));

                DirectXUI.DrawShadowText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, Menu.TNColorOption.GetColor());
            }
            else
            {

                string text = string.Format(espTextSmall, entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand,
                    entity.Stance, entity.State, entity.Stats.Health, entity.Stats.Blood, entity.Stats.Shock);

                DirectXUI.DrawShadowText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, Menu.TNColorOption.GetColor());
            }

            // Line
            if (Menu.EspPlayersLineOption.State)
                DrawLineToEntity(entity, Menu.TNLineColorOption.GetColor(), Menu.EspPlayersLinePositionOption.SelectedPosition);
        }

        private void DrawDeadPlayer(Entity entity, string espBaseText)
        {
            // Box
            float headFootDistance = entity.ScreenFootPosition.Y - entity.ScreenHeadPosition.Y;

            float boxX = entity.ScreenHeadPosition.X - (entityBoxHalfWidth / entity.Distance);
            float boxY = entity.ScreenHeadPosition.Y - (200 / entity.Distance);
            float boxWidth = entityBoxWidth / entity.Distance;
            float boxHeight = headFootDistance / entity.Distance;

            if (Menu.EspRoundedBoxesOption.State)
                DirectXUI.DrawBorderEdges(boxX, boxY, boxWidth, headFootDistance, 1, Menu.PlayerLoggingOffColorOption.GetColor());
            else
                DirectXUI.DrawBox(boxX, boxY, boxWidth, headFootDistance, 1, Menu.PlayerLoggingOffColorOption.GetColor());

            // Text
            string text = string.Format(espBaseText, entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand,
                    entity.Stance, entity.State, entity.Stats.Health, entity.Stats.Blood, entity.Stats.Shock);

            DirectXUI.DrawShadowText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, Menu.PlayerLoggingOffColorOption.GetColor());

            // Line
            if (Menu.EspPlayersLineOption.State)
                DrawLineToEntity(entity, Menu.PlayerLoggingOffLineColorOption.GetColor(), Menu.EspPlayersLinePositionOption.SelectedPosition);
        }

        private void DrawAlivePlayer(Entity entity, string espBaseText, string espFullText)
        {
            // Suspicious?
            Color playerBoxColor = Menu.PlayerColorOption.GetColor();
            Color playerLineColor = Menu.PlayerLineColorOption.GetColor();
            if (MainModule.SuspiciousPlayers.Contains(entity.Name.ToLower()))
            {
                playerBoxColor = Menu.SuspiciousPlayerColorOption.GetColor();
                playerLineColor = playerBoxColor;
            }

            // Box
            float headFootDistance = entity.ScreenFootPosition.Y - entity.ScreenHeadPosition.Y;

            float boxX = entity.ScreenHeadPosition.X - (entityBoxHalfWidth / entity.Distance);
            float boxY = entity.ScreenHeadPosition.Y - (200 / entity.Distance);
            float boxWidth = entityBoxWidth / entity.Distance;
            float boxHeight = headFootDistance / entity.Distance;
            
            if (Menu.EspRoundedBoxesOption.State)
                DirectXUI.DrawBorderEdges(boxX, boxY, boxWidth, headFootDistance, 1, playerBoxColor);
            else
                DirectXUI.DrawBox(boxX, boxY, boxWidth, headFootDistance, 1, playerBoxColor);

            // Text
            string text = string.Empty;
            if (Menu.EspPlayerTemparatureOption.State)
                text = string.Format(espFullText, entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand,
                    entity.Stance, entity.State, entity.Stats.Health, entity.Stats.Blood, entity.Stats.Shock, entity.Stats.Temperature.ToString("00.00°C"),
                    entity.Stats.HeatComfort.ToString("00.00"));
            else
                text = string.Format(espBaseText, entity.Name, entity.Distance.ToString("00"), entity.WeaponInHand,
                    entity.Stance, entity.State, entity.Stats.Health, entity.Stats.Blood, entity.Stats.Shock);

            DirectXUI.DrawBaseText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, playerBoxColor);


            // Line
            if (Menu.EspPlayersLineOption.State)
                DrawLineToEntity(entity, playerLineColor, Menu.EspPlayersLinePositionOption.SelectedPosition);

            // Head marker
            if(Menu.EspShowHeadMarker.State)
                DirectXUI.DrawCircle(entity.ScreenHeadPosition.X, entity.ScreenHeadPosition.Y, 50 / entity.Distance, 2, playerBoxColor);
        }

        private void DrawZombie(Entity entity)
        {
            if (entity.ScreenFootPosition.Z < 0.01f || entity.IsDead)
                return;

            string text = string.Format("Zombie[{0}]", entity.Distance.ToString("00"));

            // Box
            float headFootDistance = entity.ScreenFootPosition.Y - entity.ScreenHeadPosition.Y;

            float boxX = entity.ScreenHeadPosition.X - (entityBoxHalfWidth / entity.Distance);
            float boxY = entity.ScreenHeadPosition.Y - (200 / entity.Distance);
            float boxWidth = entityBoxWidth / entity.Distance;
            float boxHeight = headFootDistance / entity.Distance;

            if (Menu.EspRoundedBoxesOption.State)
                DirectXUI.DrawBorderEdges(boxX, boxY, boxWidth, headFootDistance, 1, Menu.ZombieColorOption.GetColor());
            else
                DirectXUI.DrawBox(boxX, boxY, boxWidth, headFootDistance, 1, Menu.ZombieColorOption.GetColor());

            // Text
            DirectXUI.DrawShadowText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, Menu.ZombieColorOption.GetColor());
        }

        private void DrawVehicle(Entity entity, string vehicleTypeName, Color color)
        {
            if (entity.ScreenFootPosition.Z < 0.01f || entity.Driver.Name == MainModule.LocalEntity.Driver.Name)
                return;

            string text = string.Empty;
            Color vehicleColor = color;
            if (entity.Driver.Pointer != IntPtr.Zero)
            {
                if (Menu.EspShowOccupiedVehicles.State)
                {
                    vehicleColor = Menu.OccupiedVehicleColorOption.GetColor();

                    Color vehicleLineColor = Menu.OccupiedVehicleLineColorOption.GetColor();

                    if (entity.Driver.Name.Contains("[TN]"))
                    {
                        vehicleColor = Menu.TNColorOption.GetColor();
                        vehicleLineColor = Menu.TNLineColorOption.GetColor();
                    }

                    if (Menu.EspShowOccupiedVehiclesLine.State)
                        DrawLineToEntity(entity, vehicleLineColor, Menu.EspOccupiedVehiclesLinePosition.SelectedPosition);

                    text = string.Format("{0}[{1}] - {2}\nDriver: {3}\nStats: {4}/{5}/{6}", vehicleTypeName, entity.Distance.ToString("00"),
                        entity.ObjectName, entity.Driver.Name, entity.Driver.Stats.Health, entity.Driver.Stats.Blood, entity.Driver.Stats.Shock);

                    DirectXUI.DrawCircle(entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, 2, 2, vehicleColor);
                }
            }
            else
                text = string.Format("{0}[{1}] - {2}", vehicleTypeName, entity.Distance.ToString("00"), entity.ObjectName);

            DirectXUI.DrawShadowText(DirectXUI.BaseFont, text, entity.ScreenFootPosition.X, entity.ScreenFootPosition.Y, vehicleColor);
        }

        private void DrawLineToEntity(Entity entity, Color color, LinePosition.Position position)
        {
            float y1 = 0;
            float y2 = entity.ScreenFootPosition.Y;

            if (position == LinePosition.Position.Bottom)
                y1 = DirectXUI.Height;
            else
                y2 -= (1450 / entity.Distance);

            DirectXUI.DrawLine(DirectXUI.Width / 2, y1, entity.ScreenFootPosition.X, y2, 1, color);
        }
    }
}
