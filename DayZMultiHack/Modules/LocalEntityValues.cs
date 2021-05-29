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
    public class LocalEntityValues : BaseModule
    {
        private float x = 8;
        private float y = 55;


        public LocalEntityValues(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Draw()
        {
            if (!Menu.UILocalPlayerValuesOption.State)
                return;

            EntityValues values =MainModule.LocalEntity.Stats;

            float height = 140;

            float boxesX = x + 2;
            float boxesYStart = y;


            float boxX = x - 2;
            float boxY = y - 28;
            float titleHeight = 23;
            float width = 250 + 7;

            //Content box
            DirectXUI.DrawTransparentBox(boxX, boxY, width, height, new Color(60, 60, 60), 240);
            DirectXUI.DrawBox(boxX, boxY, width, height, 1, Color.Black);

            //Title
            DirectXUI.DrawTransparentBox(boxX, boxY, width, titleHeight, new Color(40, 40, 40), 248);
            DirectXUI.DrawBox(boxX, boxY, width, titleHeight, 1, Color.Black);
            DirectXUI.DrawShadowText(DirectXUI.HeadlineFont, "Player Status", boxX + 5, boxY + 3, Color.White);

            DirectXUI.DrawProgressbar("Health", boxesX, boxesYStart, 5000, values.Health);
            DirectXUI.DrawProgressbar("Blood", boxesX, boxesYStart + 22, 5000, values.Blood);
            DirectXUI.DrawStatusBox("Shock", boxesX, boxesYStart + 44, values.Shock.ToString("0"));
            DirectXUI.DrawStatusBox("Temperature", boxesX, boxesYStart + 66, values.Temperature.ToString("00.00°C"));
            DirectXUI.DrawStatusBox("Heat comfort", boxesX, boxesYStart + 88, values.HeatComfort.ToString("00.00"));
        }
    }
}
