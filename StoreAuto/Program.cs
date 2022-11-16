using Microsoft.EntityFrameworkCore;
using StoreAuto.EF;
using StoreAuto.Models;
using System;
using System.Linq;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StoreAuto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DefaultDatabase();

            Console.WriteLine("------------------------Read-------------------------------");
            Read_LINQ_Method();
            Create();
            Console.WriteLine("------------------------Create-------------------------------");
            Read_LINQ_Query_Syntax();
            Update();
            Console.WriteLine("------------------------Update-------------------------------");
            Read_LINQ_Query_Syntax();
            Delete();
            Console.WriteLine("------------------------Delete-------------------------------");
            Read_Select_Many();

        }

        public static void DefaultDatabase()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void Create()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Invoice invoice = new Invoice { Date = DateTime.Now };
            Client client = new Client { FirstName = "Valeriy", LastName = "Melnyk", Phone = 0665001701 };
            Brand brand = new Brand { BrandName = "Citroen", Country = "France" };
            Model model = new Model { ModelName = "C4", BodyType = "Miniven" };
            Storage storage = new Storage { Address = "Shevcenka 5" };
            Car car = new Car { IsCash = true, Invoice = invoice };
            AvailabilityCar availabilityCar = new AvailabilityCar();
            CompleteSet completeSet = new CompleteSet
            {
                EngineVolume = 2,
                FuelType = "Gasoline",
                ModelYear = 2018,
                Price = 25000
            };
            Color color = new Color { ColorName = "Black", ColorCode = "123w" };

            model.Brand = brand;

            completeSet.Model = model;

            car.CompleteSet = completeSet;
            car.Color = color;

            availabilityCar.Storage = storage;
            availabilityCar.Car = car;

            invoice.Client = client;
            invoice.Car = car;

            context.Add(model);
            context.Add(completeSet);
            context.Add(car);
            context.Add(availabilityCar);
            context.Add(invoice);

            context.SaveChanges();
        }

        public static void Update()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var temp = context.Invoices.Include(x => x.Client).Where(x => x.ClientId == 2).First();

            temp.Client.FirstName = "Orest";

            context.SaveChanges();
        }

        public static void Read_LINQ_Method()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var query = context.Clients
                .Join(
                context.Invoices,
                cl => cl.Id,
                i => i.ClientId,
                (cl, i) => new { Client = cl, Invoice = i })
                .Join(
                context.Cars,
                ClI => ClI.Invoice.CarId,
                car => car.Id,
                (ClI, car) => new { ClI.Invoice, ClI.Client, Car = car })
                .Join(
                context.CompleteSets,
                ClIC => ClIC.Car.CompleteSetId,
                com => com.Id,
                (ClIC, com) => new { ClIC.Invoice, ClIC.Client, ClIC.Car, CompleteSet = com })
                .Join(
                context.Models,
                ClICC => ClICC.CompleteSet.ModelId,
                mod => mod.Id,
                (ClICC, mod) => new { ClICC.Invoice, ClICC.Client, ClICC.Car, ClICC.CompleteSet, Model = mod })
                .Join(
                context.Brands,
                ClICCM => ClICCM.Model.BrandId,
                br => br.Id,
                (ClICCM, br) => new { ClICCM.Invoice, ClICCM.Client, ClICCM.Car, ClICCM.CompleteSet, ClICCM.Model, Brand = br })
                .Select(c => new
                {
                    c.Client,
                    c.Invoice,
                    c.Car,
                    c.CompleteSet,
                    c.Model,
                    c.Brand
                });

            foreach (var item in query)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Client: {item.Client.FirstName} {item.Client.LastName}");
                Console.WriteLine($"Invoice: {item.Invoice.Id}");
                Console.WriteLine($"Car: {item.Brand.BrandName} {item.Model.ModelName} - {item.Brand.Country}");
                Console.WriteLine($"Complete set: ");
                Console.WriteLine($"         * Engine volume = {item.CompleteSet.EngineVolume}");
                Console.WriteLine($"         * Fuel type = {item.CompleteSet.FuelType}");
                Console.WriteLine($"         * Model year = {item.CompleteSet.ModelYear}");
                Console.WriteLine($"         * Price = {item.CompleteSet.Price}");
            }
        }

        public static void Read_LINQ_Query_Syntax()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var query = from client in context.Clients
                        join invoice in context.Invoices
                        on client.Id equals invoice.ClientId
                        join car in context.Cars
                        on invoice.CarId equals car.Id
                        join completeSet in context.CompleteSets
                        on car.CompleteSetId equals completeSet.Id
                        join model in context.Models
                        on completeSet.ModelId equals model.Id
                        join brand in context.Brands
                        on model.BrandId equals brand.Id
                        select new
                        {
                            Client = client,
                            Invoice = invoice,
                            Car = car,
                            CompleteSet = completeSet,
                            Model = model,
                            Brand = brand
                        };

            foreach (var item in query)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Client: {item.Client.FirstName} {item.Client.LastName}");
                Console.WriteLine($"Invoice: {item.Invoice.Id}");
                Console.WriteLine($"Car: {item.Brand.BrandName} {item.Model.ModelName} - {item.Brand.Country}");
                Console.WriteLine($"Complete set: ");
                Console.WriteLine($"         * Engine volume = {item.CompleteSet.EngineVolume}");
                Console.WriteLine($"         * Fuel type = {item.CompleteSet.FuelType}");
                Console.WriteLine($"         * Model year = {item.CompleteSet.ModelYear}");
                Console.WriteLine($"         * Price = {item.CompleteSet.Price}");
            }

        }

        public static void Read_Select_Many()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var query = from client in context.Set<Client>()
                        from invoice in context.Set<Invoice>().Where(invoice => invoice.ClientId == client.Id).DefaultIfEmpty()
                        from car in context.Set<Car>().Where(car => car.Id == invoice.CarId).DefaultIfEmpty()
                        from completeSet in context.Set<CompleteSet>().Where(completeSet => completeSet.Id == car.CompleteSetId).DefaultIfEmpty()
                        from model in context.Set<Model>().Where(model => model.Id == completeSet.ModelId).DefaultIfEmpty()
                        from brand in context.Set<Brand>().Where(brand => brand.Id == model.BrandId).DefaultIfEmpty()
                        select new
                        {
                            Client = client,
                            Invoice = invoice,
                            Car = car,
                            CompleteSet = completeSet,
                            Model = model,
                            Brand = brand
                        };

            foreach (var item in query)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Client: {item.Client.FirstName} {item.Client.LastName}");
                Console.WriteLine($"Invoice: {item.Invoice.Id}");
                Console.WriteLine($"Car: {item.Brand.BrandName} {item.Model.ModelName} - {item.Brand.Country}");
                Console.WriteLine($"Complete set: ");
                Console.WriteLine($"         * Engine volume = {item.CompleteSet.EngineVolume}");
                Console.WriteLine($"         * Fuel type = {item.CompleteSet.FuelType}");
                Console.WriteLine($"         * Model year = {item.CompleteSet.ModelYear}");
                Console.WriteLine($"         * Price = {item.CompleteSet.Price}");
            }
        }

        public static void Delete()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var temp = context.Clients.Where(x => x.FirstName == "Orest").Single();
            context.Clients.Remove(temp);
            context.SaveChanges();
        }
    }
}
