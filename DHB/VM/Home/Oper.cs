using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHB.VM.Home
{
    public class Oper
    {
        public IQueryable<Models.Oper> Operations { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public DateTime StData { get; set; }
        public DateTime FinData { get; set; }
        public string NameAct { get; set; }
        public int maxsum { get; set; }
        public int minsum { get; set; }
        public int maxsump { get; set; }
        public int minsump { get; set; }
        public int maxpr { get; set; }
        public int minpr { get; set; }
        public int? id { get; set; }
    }
}
