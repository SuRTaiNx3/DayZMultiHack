using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsLibrary.MathObjects;

namespace UtilsLibrary.MathObjects
{
    public class Transformation
    {
        public Vector3 Right; //0x4 
        public Vector3 Up; //0x10 
        public Vector3 Forward; //0x1C 
        public Vector3 Translation; //0x28 
        public Vector3 Viewport; //0x54 
        public Vector3 Fov; //0x190
        public Vector3 Proj1;//0xCC
        public Vector3 Proj2; //0xD8

        public Transformation() { }

        public Transformation(Vector3 right, Vector3 up, Vector3 forward, Vector3 translation, Vector3 viewport, Vector3 fov, Vector3 proj1, Vector3 proj2)
        {
            Right = right;
            Up = up;
            Forward = forward;
            Translation = translation;
            Viewport = viewport;
            Fov = fov;
            Proj1 = proj1;
            Proj2 = proj2;
        }

        public bool IsValid()
        {
            if (Right == null)
                return false;
            else if (Up == null)
                return false;
            else if (Forward == null)
                return false;
            else if (Translation == null)
                return false;
            else if (Viewport == null)
                return false;
            else if (Fov == null)
                return false;
            else if (Proj1 == null)
                return false;
            else if (Proj2 == null)
                return false;
            else
                return true;
        }
    }
}
