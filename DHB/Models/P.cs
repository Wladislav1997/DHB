using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHB.Models
{
    public class P
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sum { get; set; }
        public string Coment { get; set; }
        public DateTime Data { get; set; }

        public Oper Operation { get; set; }
        public int OperationId { get; set; }
    }
}
