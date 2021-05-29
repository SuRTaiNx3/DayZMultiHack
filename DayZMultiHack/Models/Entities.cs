using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Models
{
    public class EntitiesList
    {
        public List<Entity> Entities { get; set; }

        public EntitiesList()
        {
            Entities = new List<Entity>();
        }

        public List<Entity> GetEnemySoldiers(IntPtr localEntityPointer)
        {
            return Entities.Where(e => e.Pointer != localEntityPointer && !e.IsDead && !e.IsTN && e.Type == Entity.Types.Soldier).ToList();
        }

        public static double DistanceToPoint(Entity entity, float x, float y)
        {
            // Current Distance
            float dx = (entity.ScreenHeadPosition.X - x);
            float dy = (entity.ScreenHeadPosition.Y - y);
            double distance = Math.Sqrt((dx * dx) + (dy * dy));

            return distance;
        }
    }
}
