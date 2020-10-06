namespace TropicalServerApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblCustomer> tblCustomer { get; set; }
        public virtual DbSet<tblOrder> tblOrder { get; set; }
        public virtual DbSet<tblOrderHistory> tblOrderHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustName)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustOfficeAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustOfficeAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustOfficeCity)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustOfficeState)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustFaxNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustOrderEntryContactName)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustBillingAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustBillingAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustBillingCity)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustBillingState)
                .IsUnicode(false);

            modelBuilder.Entity<tblCustomer>()
                .Property(e => e.CustRouteVisitWeekDay)
                .IsUnicode(false);

            modelBuilder.Entity<tblOrder>()
                .Property(e => e.OrderTrackingNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblOrder>()
                .Property(e => e.OrderGroupNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblOrder>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<tblOrder>()
                .Property(e => e.TabletID)
                .IsUnicode(false);
        }
    }
}
