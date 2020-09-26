using BusinessData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace BusinessData
{
    public class BusinessContext : DbContext
    {
        public BusinessContext() : base()
        {

        }
        public BusinessContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=NOTE-DIEGOV\\MSSQLSERVER;Initial Catalog=ApiTest;User ID=sa; Password=m3M1r0%0R1m3M;");
            optionsBuilder.UseSqlServer("Data Source=NOTE-DIEGOV;Initial Catalog=ApiTest;User ID=sa; Password=m3M1r0%0R1m3M;");
        }
        /*
         add-migration CreateBusiness
         update-database
        */

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }

        private string UserProvider
        {
            get
            {
                if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
                    return WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                return string.Empty;
            }
        }

        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is BaseEntity)
                {
                    var auditable = entry.Entity as BaseEntity;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreateUser = UserProvider;
                        auditable.CreatedDate = TimestampProvider();
                        auditable.ModifiedDate = TimestampProvider();
                    }
                    else
                    {
                        auditable.ModifiedUser = UserProvider;
                        auditable.ModifiedDate = TimestampProvider();
                    }
                }
            }
        }

        //Funcion Lambda fecha actual
        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;
    }
}
