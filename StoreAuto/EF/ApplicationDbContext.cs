using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StoreAuto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<AvailabilityCar> AvailabilityCars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CompleteSet> CompleteSets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("D:\\КПІ\\3 курс\\1 семестр\\ПІС\\Лаб 2\\StoreAuto\\StoreAuto\\appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Invoice>()
              .HasKey(x => x.Id)
              .HasName("PK_Invoice");

            modelBuilder
                .Entity<Color>()
                .HasKey(x => new { x.ColorName, x.ColorCode })
                .HasName("PK_NameCode");

            modelBuilder
                .Entity<AvailabilityCar>()
                .HasOne(x => x.Car)
                .WithOne(x => x.AvailabilityCar)
                .HasForeignKey<Car>(t => t.AvailabilityCarId);

            modelBuilder
                .Entity<Invoice>()
                .ToTable("AllInvoices");

            modelBuilder
               .Entity<Invoice>()
               .Property(x => x.CarId)
               .HasColumnName("VIN_Number");

            modelBuilder
                .Entity<Invoice>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Invoices)
                .IsRequired();

            modelBuilder
               .Entity<CompleteSet>()
               .Property(x => x.Price)
               .HasDefaultValue(25000);

            modelBuilder
               .Entity<CompleteSet>()
               .HasCheckConstraint("Price", "Price > 10000 AND Price < 99999999");

            modelBuilder
               .Entity<Model>()
               .Property(x => x.ModelName)
               .HasColumnName("Name")
               .HasMaxLength(255);


            modelBuilder
                .Entity<Car>()
                .HasOne(x => x.Invoice)
                .WithOne(x => x.Car)
                .HasForeignKey<Invoice>(t => t.CarId);

            modelBuilder
                .Entity<Order>()
                .HasOne(x => x.Invoice)
                .WithOne(x => x.Order)
                .HasForeignKey<Invoice>(t => t.OrderId);

            modelBuilder
                .Entity<Car>()
                .HasOne(x => x.Color)
                .WithMany(x => x.Cars)
                .IsRequired();

            modelBuilder
                .Entity<Car>()
                .HasOne(x => x.CompleteSet)
                .WithMany(x => x.Cars)
                .IsRequired();

            modelBuilder
                .Entity<AvailabilityCar>()
                .HasOne(x => x.Storage)
                .WithMany(x => x.AvailabilityCars)
                .IsRequired();

            modelBuilder
               .Entity<CompleteSet>()
               .HasOne(x => x.Order)
               .WithMany(x => x.CompleteSets);

            modelBuilder
               .Entity<CompleteSet>()
               .HasOne(x => x.Model)
               .WithMany(x => x.CompletedSets)
               .IsRequired();

            modelBuilder
              .Entity<Model>()
              .HasOne(x => x.Brand)
              .WithMany(x => x.Models)
              .IsRequired();

        }
    }
}
