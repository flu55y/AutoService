using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AutoService.Models.EF
{
    public partial class AutoServiceContext : DbContext
    {
        public AutoServiceContext()
            : base("name=AutoServiceContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderSparepart> OrderSpareparts { get; set; }
        public virtual DbSet<OrderTypeOfWork> OrderTypeOfWorks { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Sparepart> Spareparts { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<TypeOfWork> TypeOfWorks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
