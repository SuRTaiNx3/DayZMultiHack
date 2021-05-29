using ExternalD3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class Speedhack : BaseModule
    {
        private Transformation _transData;

        public Speedhack(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            Thread speedhack = new Thread(new ThreadStart(this.SpeedHackThread));
            speedhack.Name = "Speedhack";
            speedhack.IsBackground = true;
            speedhack.Start();
        }

        public void UpdateTransformationData(Transformation transData)
        {
            _transData = transData;
        }

        private void SpeedHackThread()
        {
            while (!Menu.ExitOption.ExitApplication)
            {
                Thread.Sleep(1);
                if (!Menu.SpeedhackOnlyOnWalkingOption.State && !Menu.SpeedhackOption.State)
                    continue;

                if (MainModule.WorldBase != IntPtr.Zero && MainModule.LocalEntity.Pointer != IntPtr.Zero && MainModule.LocalEntity.Position != null && MainModule.LocalEntity.Position.X > 0)
                {
                    //Speedhack
                    Vector3 newPos = new Vector3();
                    Vector3 curPos = new Vector3();
                    curPos.X = MainModule.LocalEntity.Position.X;
                    curPos.Y = MainModule.LocalEntity.Position.Y;
                    curPos.Z = MainModule.LocalEntity.Position.Z;

                    bool doSpeedhack = false;
                    if (Menu.SpeedhackOnlyOnWalkingOption.State && DirectXUI.W_Pressed)
                        doSpeedhack = true;
                    else if (!Menu.SpeedhackOnlyOnWalkingOption.State && Menu.SpeedhackOption.State)
                        doSpeedhack = true;

                    if (doSpeedhack)
                    {
                        newPos.X = curPos.X + Menu.SpeedOption.Value * _transData.Forward.X;
                        newPos.Y = curPos.Y + Menu.SpeedOption.Value * _transData.Forward.Y;
                        newPos.Z = curPos.Z + Menu.SpeedOption.Value * _transData.Forward.Z;
                        Mem.Write<float>(MainModule.LocalEntity.VisualStatePointer + 0x28, newPos.X);
                        Mem.Write<float>(MainModule.LocalEntity.VisualStatePointer + 0x30, newPos.Y);
                        Mem.Write<float>(MainModule.LocalEntity.VisualStatePointer + 0x2c, newPos.Z);

                        //Land contact
                        Mem.Write<byte>(MainModule.LocalEntity.Pointer + 0x191, 0);
                        Mem.Write<byte>(MainModule.LocalEntity.Pointer + 0x192, 1);
                    }
                }
            }
        }
    }
}
