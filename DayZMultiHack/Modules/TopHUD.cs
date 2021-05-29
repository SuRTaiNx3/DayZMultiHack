using DayZMultiHack.Models;
using ExternalD3D11;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class TopHUD : BaseModule
    {
        private int prevEnemyCount = 0;
        private int prevVehicleCount = 0;

        public TopHUD(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }

        public void Draw()
        {
            if (!Menu.TopHUDEnabledOption.State)
                return;

            int enemyCount = MainModule.EntitiesList.Entities.Where(e => e.Type == Models.Entity.Types.Soldier && !e.IsTN && !e.IsDead && e.Name != "PlayerName" && e.Pointer != MainModule.LocalEntity.Pointer).Count();
            int unknownCount = MainModule.EntitiesList.Entities.Where(e => e.Type == Models.Entity.Types.Soldier && !e.IsTN && e.Name == "PlayerName").Count();
            int tnCount = MainModule.EntitiesList.Entities.Where(e => e.Type == Models.Entity.Types.Soldier && e.IsTN && e.Pointer != MainModule.LocalEntity.Pointer).Count();
            int vehicleCount = MainModule.EntitiesList.Entities.Where(e => e.Type != Models.Entity.Types.Soldier && e.Type != Models.Entity.Types.Zombie && e.DriverPointer != IntPtr.Zero).Count();

            if (enemyCount > prevEnemyCount || vehicleCount > prevVehicleCount)
                DirectXUI.ShowPlayerCountWarning();

            prevEnemyCount = enemyCount;
            prevVehicleCount = vehicleCount;

            // 3 Nearest players
            List<Entity> nearestEntities = MainModule.EntitiesList.Entities.Where(e => e.Type == Entity.Types.Soldier && !e.IsDead && !e.IsTN).OrderBy(e => e.Distance).Take(3).ToList();
            string nearestPlayersText = "Nearest Players: {0}, {1}, {2}";

            // Player 1
            string player1Text = "NONE";
            if (nearestEntities.Count > 0)
                player1Text = string.Format("{0}[{1}]", nearestEntities[0].Name, nearestEntities[0].Distance);

            // Player 2
            string player2Text = "NONE";
            if (nearestEntities.Count > 1)
                player2Text = string.Format("{0}[{1}]", nearestEntities[1].Name, nearestEntities[1].Distance);

            // Player 3
            string player3Text = "NONE";
            if (nearestEntities.Count > 2)
                player3Text = string.Format("{0}[{1}]", nearestEntities[2].Name, nearestEntities[2].Distance);

            nearestPlayersText = string.Format(nearestPlayersText, player1Text, player2Text, player3Text);

            
            int height = 20;
            //int totalScreenWidth = windowWidth +15;
            int screenCenter = DirectXUI.Width / 2;

            DirectXUI.DrawTransparentBox(0, 0, DirectXUI.Width, height, new Color(60, 60, 60), 240);
            DirectXUI.DrawLine(0, height, DirectXUI.Width, height, 1, Color.Black);

            if (Menu.TopHUDShowFPSOption.State)
                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, "FPS: " + DirectXUI.FPS.ToString("00"), DirectXUI.Width - 60, 0, Color.White);



            if (Menu.TopHUDShowServerNameOption.State)
            {
                DirectXUI.DrawCenterText(DirectXUI.UIFontSmall, MainModule.ScoreboardTable.ServerName, new RectangleF(0, 0, DirectXUI.Width, height), Color.White);
                //DirectXWrapper.DrawTextRect(HudNormalFont, servername, textRect, D3D.DrawTextFormat.Center, Color.White);
            }

            if (Menu.TopHUDShowLocalPlayerPosition.State)
            {
                string values = "Position: X:" + MainModule.LocalEntity.Position.X.ToString("0.00") + "  Y:" + MainModule.LocalEntity.Position.Y.ToString("0.00") + "  Z:" + MainModule.LocalEntity.Position.Z.ToString("0.00");
                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, values, 10, 0, Color.White);
            }

            if (Menu.TopHUDShowEnemyCount.State)
            {
                string text = String.Format("Enemy/Unknown/TN/Vehicles: {0} / {1} / {2} / {3}", enemyCount, unknownCount, tnCount, vehicleCount);
                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, text, 1500, 0, DirectXUI.PlayerCountColor);
            }

            if (Menu.TopHUDShowNearestPlayers.State)
                DirectXUI.DrawBaseText(DirectXUI.UIFontSmall, nearestPlayersText, 280, 0, Color.White);
        }
    }
}
