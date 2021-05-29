using ExternalD3D11;
using ExternalD3D11.Console;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class BulletESP : BaseModule
    {
        #region Constructor

        public BulletESP(Main main, UI ui, Menu menu)
            : base(main, ui, menu){}

        #endregion

        #region Methods

        public void Draw()
        {
            if (!Menu.EspBulletsOption.State && !Menu.AnonymousBulletsOption.State)
                return;

            IntPtr bulletTable = Mem.Read<IntPtr>(MainModule.WorldBase + Offsets.World.BULLET_TABLE);
            int bulletTableSize = Mem.Read<int>(MainModule.WorldBase + (Offsets.World.BULLET_TABLE + 0x4));

            int pointer = MainModule.EntitiesList.Entities.FirstOrDefault(e => e.Pointer != MainModule.LocalEntity.Pointer && !e.IsTN && !e.IsDead).BasePointer.ToInt32();

            for (int i = 0; i < bulletTableSize; i++)
            {
                IntPtr bulletPtr = Mem.Read<IntPtr>(bulletTable + (i * 0x4));
                IntPtr visualState = Mem.Read<IntPtr>(bulletPtr + Offsets.Bullet.VisualState.BASE);
                UtilsLibrary.MathObjects.Vector3 vec = Mem.ReadVector3(visualState + Offsets.Bullet.VisualState.POSITION_VEC3_START);

                // Current Distance
                float dx = (MainModule.LocalEntity.Position.X - vec.X);
                float dy = (MainModule.LocalEntity.Position.Y - vec.Y);
                float dz = (MainModule.LocalEntity.Position.Z - vec.Z);
                int distance = (int)Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));

                UtilsLibrary.MathObjects.Vector3 textPos = Geometry.W2SN(vec, MainModule.TransformationData);


                if (bulletPtr == IntPtr.Zero || visualState == IntPtr.Zero)
                    continue;

                //Mem.Write<int>(bulletPtr + Offsets.Bullet.PARENT, MainModule.LocalEntityBasePointer.ToInt32());

                if (textPos.Z < 0.01f || textPos.X <= 0 || textPos.X > DirectXUI.Width || textPos.Y > DirectXUI.Height)
                    continue;

                if (Menu.AnonymousBulletsOption.State)
                {
                    if (Mem.Read<IntPtr>(bulletPtr + Offsets.Bullet.PARENT) == MainModule.LocalEntityBasePointer && distance > 50)
                        Mem.Write<int>(bulletPtr + Offsets.Bullet.PARENT, pointer);
                }

                if (Menu.EspBulletsOption.State)
                {
                    DirectXUI.DrawShadowText(DirectXUI.BaseFont, string.Format("Bullet[{0}]", distance), textPos.X, textPos.Y, Color.Violet);
                    DirectXUI.DrawLine(DirectXUI.Width / 2, 0, textPos.X, textPos.Y, 1, Color.Violet);
                }
            }
        }

        #endregion
    }
}
