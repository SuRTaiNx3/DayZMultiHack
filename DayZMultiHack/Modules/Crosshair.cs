using D3DMenu.MenuTypes;
using ExternalD3D11;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class Crosshair : BaseModule
    {
        public Crosshair(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }

        public void Draw()
        {
            if (!Menu.CrosshairEnabledOption.State)
                return;

            float centerX = DirectXUI.Width / 2;
            float centerY = DirectXUI.Height / 2;

            float size = Menu.CrosshairSizeOption.Value;
            float thickness = Menu.CrosshairThicknessOption.Value;

            Color color = Menu.CrosshairColorOption.GetColor();

            switch (Menu.CrosshairTypeOption.SelectedType)
            {
                case CrosshairType.Type.Type1:
                    DirectXUI.DrawLine(centerX - size, centerY, centerX + size, centerY, thickness, color);
                    DirectXUI.DrawLine(centerX, centerY - size, centerX, centerY + size, thickness, color);
                    break;
                case CrosshairType.Type.Type2:
                    DirectXUI.DrawCircle(centerX, centerY, size, 1, color);
                    break;
                case CrosshairType.Type.Type3:
                    DirectXUI.DrawLine(centerX - size, centerY, centerX + size, centerY, thickness, color);
                    DirectXUI.DrawLine(centerX, centerY - size, centerX, centerY + size, thickness, color);
                    DirectXUI.DrawCircle(centerX, centerY, size - 5, thickness, color);
                    break;
                case CrosshairType.Type.Type4:
                    DirectXUI.DrawCircle(centerX, centerY, size, thickness, color);
                    //DrawFilledCircle(centerX, centerY, size / 5, color);
                    break;
                case CrosshairType.Type.Type5:
                    DirectXUI.DrawLine(centerX + size, centerY + size, centerX + 3, centerY + 3, thickness, color);
                    DirectXUI.DrawLine(centerX - size, centerY + size, centerX - 3, centerY + 3, thickness, color);
                    DirectXUI.DrawLine(centerX + size, centerY - size, centerX + 3, centerY - 3, thickness, color);
                    DirectXUI.DrawLine(centerX - size, centerY - size, centerX - 3, centerY - 3, thickness, color);
                    break;
                default:
                    break;
            }
        }
    }
}
