using Microsoft.EntityFrameworkCore;
using SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemBarcode> MenuItemBarcodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Price)
                      .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<MenuItemBarcode>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.MenuItem)
                      .WithMany(x => x.Barcodes)
                      .HasForeignKey(x => x.MenuItemId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Quantity)
                      .HasColumnType("decimal(10,2)");

                entity.HasOne(x => x.Order)
                      .WithMany(x => x.Items)
                      .HasForeignKey(x => x.OrderId);

                entity.HasOne(x => x.MenuItem)
                      .WithMany(x => x.OrderItems)
                      .HasForeignKey(x => x.MenuItemId);
            });
        }
    }

}
