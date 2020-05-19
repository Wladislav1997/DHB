using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DHB.Models
{
    public class DHBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Oper> Operations { get; set; }
        public DbSet<Plan> Plans { get; set; }

        public DbSet<P> Ps { get; set; }
        public DbSet<P1> P1s { get; set; }
        public DHBContext(DbContextOptions<DHBContext> options)
            : base(options)
        {
            /* Database.EnsureDeleted(); */  // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
    }
}
