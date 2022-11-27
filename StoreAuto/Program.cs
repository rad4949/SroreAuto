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

            Console.WriteLine("\n------------------------Read-------------------------------");
            Read_LINQ_Method();
            Create();
            Console.WriteLine("\n------------------------Create-------------------------------");
            Read_LINQ_Query_Syntax();
            Update();
            Console.WriteLine("\n------------------------Update-------------------------------");
            Read_LINQ_Query_Syntax();
            Delete();
            Console.WriteLine("\n------------------------Delete-------------------------------");
            Read_Select_Many();


            Console.WriteLine("\n------------------------Union-------------------------------");
            Union();
            Console.WriteLine("\n------------------------Except-------------------------------");
            Except();
            Console.WriteLine("\n------------------------Intersect-------------------------------");
            Intersect();
            Console.WriteLine("\n------------------------Join-------------------------------");
            Join();
            Console.WriteLine("\n------------------------GroupBy-------------------------------");
            GroupBy();
            Console.WriteLine("\n------------------------Distinct-------------------------------");
            Distinct();
            Console.WriteLine("\n------------------------Any-------------------------------");
            Any();
            Console.WriteLine("\n------------------------All-------------------------------");
            All();
            Console.WriteLine("\n------------------------Min-------------------------------");
            Min();
            Console.WriteLine("\n------------------------Max-------------------------------");
            Max();
            Console.WriteLine("\n------------------------Average-------------------------------");
            Average();
            Console.WriteLine("\n------------------------Sum-------------------------------");
            Sum();
            Console.WriteLine("\n------------------------Count-------------------------------");
            Count();
            Console.WriteLine("\n------------------------ExplicitLoading-------------------------------");
            ExplicitLoading();
            Console.WriteLine("\n------------------------AsNotTracking-------------------------------");
            AsNotTracking();
            Console.WriteLine("\n------------------------Procedure-------------------------------");
            Procedure();
            Console.WriteLine("\n------------------------Function-------------------------------");
            Function();
        }

        public static void DefaultDatabase()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var createSql = @"
                create procedure [dbo].[GetOrderedByCompleteSet] as
                begin
                    select * from dbo.CompleteSets
                    order by Price desc
                end
            ";

            context.Database.ExecuteSqlRaw(createSql);

            createSql = @"
                create function [dbo].[SearchCompleteSetsById] (@id int)
                returns table
                as
                return
                    select * from dbo.CompleteSets
                    where Id = @id
            ";

            context.Database.ExecuteSqlRaw(createSql);

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

            var temp = context.Invoices.Include(x => x.Client)
                .Where(x => x.ClientId == 2).First();

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

        public static void Union()
        {
            ApplicationDbContext context = new ApplicationDbContext();


            var query = context.Cars.Select(x => x.CompleteSetId)
                .Union(context.Cars.Include(x => x.CompleteSet)
                .Where(x => x.CompleteSet.Order != null).Select(x => x.Id));

            foreach (var item in query)
            {
                Console.WriteLine($"CompleteSetId: {item}");
            }
        }

        public static void Except()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var selector1 = context.Cars.ToList();
            var selector2 = context.Cars.Include(x => x.CompleteSet)
                .Where(x => x.CompleteSet.Order != null).ToList();

            var query = selector1.Except(selector2);

            foreach (var item in query)
            {
                Console.WriteLine($"CompleteSetId: {item.CompleteSetId}");
            }
        }

        public static void Intersect()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var query = context.Cars.Select(x => x.CompleteSetId)
                .Intersect(context.Cars.Include(x => x.CompleteSet)
                .Where(x => x.CompleteSet.Order != null).Select(x => x.Id));

            foreach (var item in query)
            {
                Console.WriteLine($"CompleteSetId: {item}");
            }
        }

        public static void Join()
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

        public static void GroupBy()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var groups = context.Cars
                .GroupBy(x => x.ColorName)
                .Select(m => new
                {
                    m.Key,
                    Count = m.Count()
                })
                .ToList();

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key} - {group.Count}");
            }
        }

        public static void Distinct()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var groups = context.Cars
                .Select(m => m.ColorName)
                .Distinct()
                .ToList();

            foreach (var group in groups)
            {
                Console.WriteLine($"{group}");
            }
        }

        public static void Any()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            bool result = context.Clients.Any(u => u.FirstName == "Igor");

            Console.WriteLine(result);
        }

        public static void All()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            bool result = context.Clients.All(u => u.FirstName == "Igor");

            Console.WriteLine(result);
        }

        public static void Min()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var minPrice = context.CompleteSets.Min(u => u.Price);

            Console.WriteLine($"Min price = {minPrice}");
        }

        public static void Max()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var maxPrice = context.CompleteSets.Max(u => u.Price);

            Console.WriteLine($"Max price = {maxPrice}");
        }

        public static void Average()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var avgPrice = context.CompleteSets.Average(u => u.Price);

            Console.WriteLine($"Average price = {avgPrice}");
        }

        public static void Sum()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var sumPrice = context.CompleteSets.Sum(u => u.Price);

            Console.WriteLine($"Sum price = {sumPrice}");
        }

        public static void Count()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var cars = context.Cars.Count();

            Console.WriteLine($"Count cars = {cars}");
        }
         
        public static void ExplicitLoading()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var model = context.Models.First();

            var totalModelsQuantity = context
                .Entry(model)
                .Collection(x => x.CompletedSets)
                .Query()
                .Count();

            context
                .Entry(model)
                .Collection(x => x.CompletedSets)
                .Load();

            context
                .Entry(model)
                .Reference(x => x.Brand)
                .Load();

            Console.WriteLine("Model information");
            Console.WriteLine($"Name: {model.ModelName}");

            Console.WriteLine($"Total count: {totalModelsQuantity}.");
            foreach (var item in model.CompletedSets)
            {
                Console.WriteLine($"Complet set where id - { item.Id}, price = {item.Price}.");
            }
        }

        public static void AsNotTracking()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var temp = context.CompleteSets.Where(x => x.Id == 2).Single();

            Console.WriteLine($"Price = {temp.Price}");

            temp.Price = 500000;

            context.SaveChanges();

            temp = context.CompleteSets.Where(x => x.Id == 2).AsNoTracking().Single();

            Console.WriteLine($"Price = {temp.Price}");

            temp.Price = 60000;

            context.SaveChanges();

            temp = context.CompleteSets.Where(x => x.Id == 2).AsNoTracking().Single();

            Console.WriteLine($"Price = {temp.Price}");
        }

        public static void Procedure()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var completeSet = context
                .CompleteSets
                .FromSqlRaw("EXECUTE dbo.GetOrderedByCompleteSet").ToList();

            Console.WriteLine("CompleteSets:");
            foreach (var item in completeSet)
            {
                Console.WriteLine("--------");
                Console.WriteLine(
                    $"Id: {item.Id}. " +
                    $"Price: {item.Price}.");
            }
        }

        public static void Function()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var completeSet = context
                .CompleteSets
                .FromSqlRaw("SELECT * FROM dbo.SearchCompleteSetsById(2)").ToList();

            Console.WriteLine("Founds:");
            foreach (var item in completeSet)
            {
                Console.WriteLine("--------");
                Console.WriteLine(
                    $"Id: {item.Id}. " +
                    $"Price: {item.Price}.");
            }
        }
    }
}
