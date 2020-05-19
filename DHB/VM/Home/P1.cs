using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHB.VM.Home
{
    public class P1
    {
        public IQueryable<Models.P1> p1s { get; set; }
        public string Name { get; set; }
        public DateTime StData { get; set; }
        public DateTime FinData { get; set; }
        public string NameAct { get; set; }
        public int maxsum { get; set; }
        public int minsum { get; set; }
    }
}
