using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class LocationESP : BaseModule
    {
        public LocationESP(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Draw()
        {
            string baseText = "{0}[{1}]";

            if (Menu.EspAirfieldsOption.State)
            {
                Vector3 neafPos = new Vector3() { X = 12232f, Y = 12578f, Z = 200f };
                Vector3 neafScreen = Geometry.W2SN(neafPos, MainModule.TransformationData);
                Vector3 d1 = MainModule.LocalEntity.Position - neafPos;
                float neafDist = (float)Math.Sqrt((d1.X * d1.X) + (d1.Y * d1.Y) + (d1.Z * d1.Z));

                Vector3 nwafPos = new Vector3() { X = 4561, Y = 9560, Z = 350f };
                Vector3 nwafScreen = Geometry.W2SN(nwafPos, MainModule.TransformationData);
                Vector3 d2 = MainModule.LocalEntity.Position - nwafPos;
                float nwafDist = (float)Math.Sqrt((d2.X * d2.X) + (d2.Y * d2.Y) + (d2.Z * d2.Z)); ;

                Vector3 balotaPos = new Vector3() { X = 4471.17f, Y = 2383.34f, Z = 3.45f };
                Vector3 balotaScreen = Geometry.W2SN(balotaPos, MainModule.TransformationData);
                Vector3 d3 = MainModule.LocalEntity.Position - balotaPos;
                float balotaDist = (float)Math.Sqrt((d3.X * d3.X) + (d3.Y * d3.Y) + (d3.Z * d3.Z)); ;

                if (neafScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "NEAF", neafDist.ToString("00")), neafScreen.X, neafScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (nwafScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "NWAF", nwafDist.ToString("00")), nwafScreen.X, nwafScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (balotaScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Balota", balotaDist.ToString("00")), balotaScreen.X, balotaScreen.Y, Menu.AirfieldsColorOption.GetColor());
            }

            if(Menu.EspMainCitiesOption.State)
            {
                Vector3 berenzinoPos = new Vector3() { X = 12298.22f, Y = 9073.13f, Z = 33.98f };
                Vector3 berenzinoScreen = Geometry.W2SN(berenzinoPos, MainModule.TransformationData);
                Vector3 d1 = MainModule.LocalEntity.Position - berenzinoPos;
                float berenzinoDist = (float)Math.Sqrt((d1.X * d1.X) + (d1.Y * d1.Y) + (d1.Z * d1.Z));

                Vector3 kamyshovoPos = new Vector3() { X = 12153.61f, Y = 3498.92f, Z = 6.16f };
                Vector3 kamyshovoScreen = Geometry.W2SN(kamyshovoPos, MainModule.TransformationData);
                Vector3 d2 = MainModule.LocalEntity.Position - berenzinoPos;
                float kamyshovoDist = (float)Math.Sqrt((d2.X * d2.X) + (d2.Y * d2.Y) + (d2.Z * d2.Z));

                Vector3 elektroPos = new Vector3() { X = 10443.31f, Y = 2369.12f, Z = 5.99f };
                Vector3 elektroScreen = Geometry.W2SN(elektroPos, MainModule.TransformationData);
                Vector3 d3 = MainModule.LocalEntity.Position - berenzinoPos;
                float elektroDist = (float)Math.Sqrt((d3.X * d3.X) + (d3.Y * d3.Y) + (d3.Z * d3.Z));

                Vector3 chernogorskPos = new Vector3() { X = 6930.95f, Y = 2425.37f, Z = 6.00f };
                Vector3 chernogorskScreen = Geometry.W2SN(chernogorskPos, MainModule.TransformationData);
                Vector3 d4 = MainModule.LocalEntity.Position - berenzinoPos;
                float chernogorskDist = (float)Math.Sqrt((d4.X * d4.X) + (d4.Y * d4.Y) + (d4.Z * d4.Z));

                Vector3 zelenogorskPos = new Vector3() { X = 2553.79f, Y = 2553.79f, Z = 201.34f };
                Vector3 zelenogorskScreen = Geometry.W2SN(zelenogorskPos, MainModule.TransformationData);
                Vector3 d5 = MainModule.LocalEntity.Position - berenzinoPos;
                float zelenogorskDist = (float)Math.Sqrt((d5.X * d5.X) + (d5.Y * d5.Y) + (d5.Z * d5.Z));

                Vector3 starySoborPos = new Vector3() { X = 6253.72f, Y = 7628.25f, Z = 297.98f };
                Vector3 starySoborScreen = Geometry.W2SN(starySoborPos, MainModule.TransformationData);
                Vector3 d6 = MainModule.LocalEntity.Position - berenzinoPos;
                float starySoborDist = (float)Math.Sqrt((d6.X * d6.X) + (d6.Y * d6.Y) + (d6.Z * d6.Z));

                Vector3 solnichniyPos = new Vector3() { X = 13466.44f, Y = 6291.06f, Z = 5.65f };
                Vector3 solnichniyScreen = Geometry.W2SN(solnichniyPos, MainModule.TransformationData);
                Vector3 d7 = MainModule.LocalEntity.Position - berenzinoPos;
                float solnichniyDist = (float)Math.Sqrt((d7.X * d7.X) + (d7.Y * d7.Y) + (d7.Z * d7.Z));

                if (berenzinoScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Berezino", berenzinoDist.ToString("00")), berenzinoScreen.X, berenzinoScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (kamyshovoScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Kamyshovo", kamyshovoDist.ToString("00")), kamyshovoScreen.X, kamyshovoScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (elektroScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Elektrozavodsk", elektroDist.ToString("00")), elektroScreen.X, elektroScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (chernogorskScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Chernogorsk", chernogorskDist.ToString("00")), chernogorskScreen.X, chernogorskScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (zelenogorskScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Zelenogorsk", zelenogorskDist.ToString("00")), zelenogorskScreen.X, zelenogorskScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (starySoborScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Stary Sobor", starySoborDist.ToString("00")), starySoborScreen.X, starySoborScreen.Y, Menu.AirfieldsColorOption.GetColor());
                if (solnichniyScreen.Z > 0.01f)
                    DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format(baseText, "Solnichniy", solnichniyDist.ToString("00")), solnichniyScreen.X, solnichniyScreen.Y, Menu.AirfieldsColorOption.GetColor());
            }
        }
    }
}
