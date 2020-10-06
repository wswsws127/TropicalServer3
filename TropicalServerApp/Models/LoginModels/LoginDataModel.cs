namespace TropicalServerApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginDataModel : DbContext
    {
        public LoginDataModel()
            : base("name=LoginDataModel")
        {
        }

        public virtual DbSet<tblTropicalUser> tblTropicalUser { get; set; }
        public virtual DbSet<tblUserLogin> tblUserLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblTropicalUser>()
                .Property(e => e.LoginID)
                .IsUnicode(false);

            modelBuilder.Entity<tblTropicalUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tblTropicalUser>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<tblTropicalUser>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tblTropicalUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserLogin>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserLogin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserLogin>()
                .Property(e => e.EmailID)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<TropicalServerApp.Models.OrderModels.tblOrder> tblOrders { get; set; }
    }
}
