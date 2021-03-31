using CatalogData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace CatalogData
{
    public class CatalogContext : DbContext
    {
        public CatalogContext() : base()
        {

        }
        public CatalogContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=NOTE-DIEGOV\\MSSQLSERVER;Initial Catalog=ApiTest;User ID=sa; Password=m3M1r0%0R1m3M;");
            optionsBuilder.UseSqlServer("Data Source=NOTE-DIEGOV;Initial Catalog=ApiTest;User ID=sa; Password=m3M1r0%0R1m3M;");
        }
        
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        


        //Funcion Lambda fecha actual
        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;
    }
}
