using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Models
{
    public class ScoreboardTable : Dictionary<int, string>
    {
        public string ServerName { get; set; }
    }
}
