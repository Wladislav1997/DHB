using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHB.VM.Home
{
    public class P
    {
        public IQueryable<Models.P> ps { get; set; }
        public string Name { get; set; }
        public DateTime StData { get; set; }
        public DateTime FinData { get; set; }
        public int maxsum { get; set; }
        public int minsum { get; set; }
        public int id { get; set; }
    }
}
