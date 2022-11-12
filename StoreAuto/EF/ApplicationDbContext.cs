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
using StoreAuto;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            Client client1 = new Client { Id = 1, FirstName = "Igor", LastName = "Radchuk", Phone = 0665001701 };
            Client client2 = new Client { Id = 2, FirstName = "Nazar", LastName = "Shevchuk", Phone = 0675001705 };

            Invoice invoice1 = new Invoice
            {
                Id = 1,
                CarId = 1,
                OrderId = 1,
                ClientId = 1,
                Order = null,
                Client = client1,
                Date = DateTime.Now
            };
            Invoice invoice2 = new Invoice
            {
                Id = 2,
                CarId = 2,
                OrderId = 2,
                ClientId = 2,
                Client = client2,
                Date = DateTime.Now
            };

            Order order2 = new Order
            {
                Id = 1,
                InvoiceId = 2,
                Invoice = invoice2,
                DateOrder = DateTime.Now,
                Term = DateTime.Today.AddDays(20)
            };

            Brand brand1 = new Brand { Id = 1, BrandName = "Mercedes-Benz", Country = "Germany" };
            Brand brand2 = new Brand { Id = 2, BrandName = "Audi", Country = "Germany" };

            Model model1 = new Model { Id = 1, BrandId = 1, Brand = brand1, ModelName = "GLS", BodyType = "Crossover" };
            Model model2 = new Model { Id = 2, BrandId = 2, Brand = brand2, ModelName = "A6", BodyType = "Universal" };

            Storage storage1 = new Storage { Id = 1, Address = "Shevcenka 5" };
            Storage storage2 = new Storage { Id = 2, Address = "Konovaltsa 8" };

            AvailabilityCar availabilityCar1 = new AvailabilityCar { Id = 1, StorageId = 1, CarId = 1, Storage = storage1 };
            AvailabilityCar availabilityCar2 = new AvailabilityCar { Id = 2, StorageId = 2, CarId = 2, Storage = storage2 };

            Color color1 = new Color { ColorName = "Black", ColorCode = "12qw" };
            Color color2 = new Color { ColorName = "White", ColorCode = "1111" };

            CompleteSet completeSet1 = new CompleteSet
            {
                Id = 1,
                ModelId = 1,
                OrderId = 1,
                EngineVolume = 3,
                FuelType = "Gasoline",
                ModelYear = 2021,
                Model = model1,
                Order = null,
                Price = 250000
            };

            CompleteSet completeSet2 = new CompleteSet
            {
                Id = 2,
                ModelId = 2,
                OrderId = 2,
                EngineVolume = 2,
                FuelType = "Gasoline",
                ModelYear = 2020,
                Model = model2,
                Price = 200000
            };

            Car car1 = new Car
            {
                Id = 1,
                InvoiceId = 1,
                AvailabilityCarId = 1,
                AvailabilityCar = availabilityCar1,
                Color = color1,
                CompleteSet = completeSet1,
                IsCash = true,
                Invoice = invoice1
            };

            Car car2 = new Car
            {
                Id = 2,
                InvoiceId = 2,
                AvailabilityCarId = 2,
                AvailabilityCar = availabilityCar2,
                Color = color2,
                CompleteSet = completeSet2,
                IsCash = true,
                Invoice = invoice2
            };

            modelBuilder.Entity<Invoice>().HasData(invoice1, invoice1);
            modelBuilder.Entity<Client>().HasData(client1, client2);
            modelBuilder.Entity<Order>().HasData(order2);
            modelBuilder.Entity<Color>().HasData(color1, color2);
            modelBuilder.Entity<Car>().HasData(car1, car2);
            modelBuilder.Entity<AvailabilityCar>().HasData(availabilityCar1, availabilityCar2);
            modelBuilder.Entity<Storage>().HasData(storage1, storage2);
            modelBuilder.Entity<CompleteSet>().HasData(completeSet1, completeSet2);
            modelBuilder.Entity<Model>().HasData(model1, model2);
            modelBuilder.Entity<Brand>().HasData(brand1, brand2);


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
              .Entity<AvailabilityCar>()
              .Property(x => x.Id)
              .IsRequired();

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
                .HasForeignKey("CompleteSetId")
                .IsRequired();

            modelBuilder
                .Entity<AvailabilityCar>()
                .HasOne(x => x.Storage)
                .WithMany(x => x.AvailabilityCars)
                .HasForeignKey("StorageId")
                .IsRequired();

            modelBuilder
               .Entity<CompleteSet>()
               .HasOne(x => x.Order)
               .WithMany(x => x.CompleteSets)
               .HasForeignKey("OrderId");

            modelBuilder
               .Entity<CompleteSet>()
               .HasOne(x => x.Model)
               .WithMany(x => x.CompletedSets)
               .HasForeignKey("ModelId")
               .IsRequired();

            modelBuilder
              .Entity<Model>()
              .HasOne(x => x.Brand)
              .WithMany(x => x.Models)
              .HasForeignKey("BrandId")
              .IsRequired();

        }
    }
}
