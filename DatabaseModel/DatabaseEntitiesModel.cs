using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DatabaseModel
{
    public partial class DatabaseEntitiesModel : DbContext
    {
        public DatabaseEntitiesModel()
            : base("name=DatabaseEntitiesModel")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Cleaning> Cleanings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }
        public virtual DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(e => e.Price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Cleaning>()
                .Property(e => e.Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CNP)
                .HasPrecision(13, 0);

            modelBuilder.Entity<PaymentHistory>()
                .Property(e => e.Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Property>()
                .Property(e => e.Price)
                .HasPrecision(5, 2);
        }
    }
}
