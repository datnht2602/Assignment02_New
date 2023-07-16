using System;
using Assignment02_New.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment02_New.Data
{
	public class ApplicationDBContext : IdentityDbContext<AppUser>
	{
		

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetails>().HasKey(OD => new { OD.ProductID, OD.OrderID });
            modelBuilder.Entity<Customer>().HasIndex(e => e.ContactName).IsUnique();
            foreach(var entityType in modelBuilder.Model.GetEntityTypes()){
                var tableName =entityType.GetTableName();
                if(tableName.StartsWith("AspNet")){
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }   

    }
}

