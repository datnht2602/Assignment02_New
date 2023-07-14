using System;
using Assignment02_New.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment02_New.Data
{
	public class ApplicationDBContext : DbContext
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
        }

    }
}

