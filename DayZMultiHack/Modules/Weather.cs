using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class Weather : BaseModule
    {
        private bool isFirstRun = true;

        public Weather(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }

        public void Update()
        {
            if (isFirstRun)
            {
                Menu.WeatherSunnyOption.State = true;
                isFirstRun = false;
            }

            if (Menu.WeatherSunnyOption.State)
            {
                Menu.WeatherOvercastOption.Value = 0;
                Menu.WeatherFogOption.Value = 0;
                Menu.WeatherWindOption.Value = 0;
                Menu.WeatherSunPositionOption.Value = -1;
                Menu.WeatherSunnyOption.State = false;
            }
            else if (Menu.WeatherStormyOption.State)
            {
                Menu.WeatherOvercastOption.Value = 1;
                Menu.WeatherFogOption.Value = 0.3f;
                Menu.WeatherWindOption.Value = 0.8f;
                Menu.WeatherSunPositionOption.Value = 0.05f;
                Menu.WeatherStormyOption.State = false;
            }
            else if (Menu.WeatherApocalypticOption.State)
            {
                Menu.WeatherOvercastOption.Value = 1;
                Menu.WeatherFogOption.Value = 1;
                Menu.WeatherWindOption.Value = -1;
                Menu.WeatherSunPositionOption.Value = 0.6f;
                Menu.WeatherApocalypticOption.State = false;
            }

            SetWantedOvercast(Menu.WeatherOvercastOption.Value);
            SetWantedFog(Menu.WeatherFogOption.Value);
            SetWantedWind(Menu.WeatherWindOption.Value);
            SetSunPosition(Menu.WeatherSunPositionOption.Value);
        }

        // Stormy
        private void SetWantedOvercast(float value)
        {
            Mem.Write<float>(MainModule.WorldBase + Offsets.World.WANTED_OVERCAST, value);
        }

        // Fog
        private void SetWantedFog(float value)
        {
            Mem.Write<float>(MainModule.WorldBase + Offsets.World.WANTED_FOG, value);
        }

        // Sun Position
        private void SetSunPosition(float value)
        {
            Mem.Write<float>(MainModule.WorldBase + Offsets.World.SUN_POSITION, value);
        }

        // Wind
        private void SetWantedWind(float value)
        {
            Mem.Write<float>(MainModule.WorldBase + Offsets.World.WANTED_WIND, value);
        }



        private float GetActualOvercast()
        {
            return Mem.Read<float>(MainModule.WorldBase + Offsets.World.ACTUAL_OVERCAST);
        }

        private float GetActualFog()
        {
            return Mem.Read<float>(MainModule.WorldBase + Offsets.World.ACTUAL_FOG);
        }

        private float GetSunPosition()
        {
            return Mem.Read<float>(MainModule.WorldBase + Offsets.World.SUN_POSITION);
        }

        private float GetActualWind()
        {
            return Mem.Read<float>(MainModule.WorldBase + Offsets.World.ACTUAL_WIND);
        }
    }
}
