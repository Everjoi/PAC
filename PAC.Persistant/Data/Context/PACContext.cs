using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PAC.Domain.Entitity;
using System.Dynamic;

namespace PAC.Persistant.Data.Context
{
    public class PACContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IHostEnvironment _environment;


        public PACContext(DbContextOptions<PACContext> options)
            : base(options)
        {

        }

        public PACContext(DbContextOptions<PACContext> options,IConfiguration configuration,IHostEnvironment environment)
            : base(options)
        {
            _environment = environment;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<User> Users => Set<User>();
        public DbSet<Discount> Discounts => Set<Discount>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne()
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Discounts)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);


            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .IsRequired();
        }

    }
}
