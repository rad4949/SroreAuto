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
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
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
            Client client3 = new Client { Id = 3, FirstName = "Petro", LastName = "Romuniuk", Phone = 0678801788 };

            Invoice invoice1 = new Invoice
            {
                Id = 1,
                CarId = 1,
                OrderId = null,
                ClientId = 1,
                Date = DateTime.Now

            };

            Invoice invoice2 = new Invoice
            {
                Id = 2,
                CarId = 2,
                OrderId = 1,
                ClientId = 2,
                Date = DateTime.Now
            };

            Invoice invoice3 = new Invoice
            {
                Id = 3,
                CarId = 3,
                OrderId = null,
                ClientId = 3,
                Date = DateTime.Now

            };

            Order order2 = new Order
            {
                Id = 1,
                DateOrder = DateTime.Now,
                Term = DateTime.Today.AddDays(20)
            };

            Brand brand1 = new Brand { Id = 1, BrandName = "Mercedes-Benz", Country = "Germany" };
            Brand brand2 = new Brand { Id = 2, BrandName = "Audi", Country = "Germany" };
            Brand brand3 = new Brand { Id = 3, BrandName = "Mercedes-Benz", Country = "Germany" };

            Model model1 = new Model { Id = 1, BrandId = 1, ModelName = "GLS", BodyType = "Crossover" };
            Model model2 = new Model { Id = 2, BrandId = 2, ModelName = "A6", BodyType = "Universal" };
            Model model3 = new Model { Id = 3, BrandId = 3, ModelName = "CLC", BodyType = "Sedan" };

            Storage storage1 = new Storage { Id = 1, Address = "Shevcenka 5" };
            Storage storage2 = new Storage { Id = 2, Address = "Konovaltsa 8" };
            Storage storage3 = new Storage { Id = 3, Address = "Dachna 11" };

            AvailabilityCar availabilityCar1 = new AvailabilityCar { Id = 1, StorageId = 1, CarId = 1 };
            AvailabilityCar availabilityCar2 = new AvailabilityCar { Id = 2, StorageId = 2, CarId = 2 };
            AvailabilityCar availabilityCar3 = new AvailabilityCar { Id = 3, StorageId = 3, CarId = 3 };

            Color color1 = new Color { ColorName = "Black", ColorCode = new string("12qw") };
            Color color2 = new Color { ColorName = "White", ColorCode = new string("1111") };
            Color color3 = new Color { ColorName = "Black", ColorCode = new string("1rr1") };

            CompleteSet completeSet1 = new CompleteSet
            {
                Id = 1,
                ModelId = 1,
                EngineVolume = 3,
                FuelType = "Gasoline",
                ModelYear = 2021,
                Price = 250000,
            };

            CompleteSet completeSet2 = new CompleteSet
            {
                Id = 2,
                ModelId = 2,
                OrderId = 1,
                EngineVolume = 2,
                FuelType = "Gasoline",
                ModelYear = 2020,
                Price = 200000
            };

            CompleteSet completeSet3 = new CompleteSet
            {
                Id = 3,
                ModelId = 3,
                EngineVolume = 3,
                FuelType = "Gasoline",
                ModelYear = 2020,
                Price = 210000,
            };

            Car car1 = new Car
            {
                Id = 1,
                ColorName = color1.ColorName,
                ColorCode = color1.ColorCode,
                CompleteSetId = 1,

                IsCash = true,
            };

            Car car2 = new Car
            {
                Id = 2,
                ColorName = color2.ColorName,
                ColorCode = color2.ColorCode,
                CompleteSetId = 2,

                IsCash = true,
            };

            Car car3 = new Car
            {
                Id = 3,
                ColorName = color3.ColorName,
                ColorCode = color3.ColorCode,
                CompleteSetId = 3,

                IsCash = false,
            };

            modelBuilder.Entity<Invoice>().HasData(invoice1, invoice2, invoice3);
            modelBuilder.Entity<Client>().HasData(client1, client2, client3);
            modelBuilder.Entity<Order>().HasData(order2);
            modelBuilder.Entity<Color>().HasData(color1, color2, color3);
            modelBuilder.Entity<Car>().HasData(car1, car2, car3);
            modelBuilder.Entity<AvailabilityCar>().HasData(availabilityCar1, availabilityCar2, availabilityCar3);
            modelBuilder.Entity<Storage>().HasData(storage1, storage2, storage3);
            modelBuilder.Entity<CompleteSet>().HasData(completeSet1, completeSet2, completeSet3);
            modelBuilder.Entity<Model>().HasData(model1, model2, model3);
            modelBuilder.Entity<Brand>().HasData(brand1, brand2, brand3);


            modelBuilder
              .Entity<Invoice>()
              .HasKey(x => x.Id)
              .HasName("PK_Invoice");

            modelBuilder
                .Entity<Color>()
                .HasKey(x => new { x.ColorName, x.ColorCode })
                .HasName("PK_NameCode");

            modelBuilder
                .Entity<Invoice>()
                .ToTable("AllInvoices");

            modelBuilder
               .Entity<Invoice>()
               .Property(x => x.CarId)
               .HasColumnName("VIN_Number");

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
                .HasOne(x => x.AvailabilityCar)
                .WithOne(x => x.Car)
                .HasForeignKey<AvailabilityCar>(t => t.CarId);
            //modelBuilder
            //  .Entity<AvailabilityCar>()
            //  .HasOne(x => x.Car)
            //  .WithOne(x => x.AvailabilityCar)
            //  .HasForeignKey<Car>(t => t.AvailabilityCarId);

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
               .HasForeignKey(x => new { x.ColorName, x.ColorCode })
               .IsRequired();

            modelBuilder
                .Entity<Invoice>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Invoices)
                .HasForeignKey("ClientId")
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
