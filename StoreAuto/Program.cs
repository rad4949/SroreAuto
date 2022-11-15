using Microsoft.EntityFrameworkCore;
using StoreAuto.EF;
using StoreAuto.Models;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StoreAuto
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //AddData();

            ApplicationDbContext context = new ApplicationDbContext();

            //var invoices = context.Invoices.Include(x => x.Car).ToList();
            //foreach (Invoice temp in invoices)
            //    Console.WriteLine($"{temp.CarId} ");


            //var clients = context.Clients.Include(x => x.Invoices).ToList();
            //foreach (Client client in clients)
            //{
            //    Console.Write($"\n Client: {client.LastName}");
            //    foreach (Invoice invoice in client.Invoices)
            //    {
            //        Console.WriteLine($" -> Invoice {invoice.CarId}");
            //    }
            //}
        }

        public static void AddData()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            Invoice invoice1 = new Invoice { Date = DateTime.Now };
            Invoice invoice2 = new Invoice { Date = DateTime.Now };

            Client client1 = new Client { FirstName = "Igor", LastName = "Radchuk", Phone = 0665001701 };
            Client client2 = new Client { FirstName = "Nazar", LastName = "Shevchuk", Phone = 0675001705 };

            Order order2 = new Order { DateOrder = DateTime.Now, Term = DateTime.Today.AddDays(20) };

            Brand brand1 = new Brand { BrandName = "Mercedes-Benz", Country = "Germany" };
            Brand brand2 = new Brand { BrandName = "Audi", Country = "Germany" };

            Model model1 = new Model { ModelName = "GLS", BodyType = "Crossover" };
            Model model2 = new Model { ModelName = "A6", BodyType = "Universal" };

            Storage storage1 = new Storage { Address = "Shevcenka 5" };
            Storage storage2 = new Storage { Address = "Konovaltsa 8" };

            Car car1 = new Car { IsCash = true, Invoice = invoice1 };
            Car car2 = new Car { IsCash = true, Invoice = invoice2 };

            AvailabilityCar availabilityCar1 = new AvailabilityCar();
            AvailabilityCar availabilityCar2 = new AvailabilityCar();

            CompleteSet completeSet1 = new CompleteSet { EngineVolume = 3, FuelType = "Gasoline", ModelYear = 2021, Price = 250000 };
            CompleteSet completeSet2 = new CompleteSet { EngineVolume = 2, FuelType = "Gasoline", ModelYear = 2020, Price = 200000 };

            Color color1 = new Color { ColorName = "Black", ColorCode = "12qw" };
            Color color2 = new Color { ColorName = "White", ColorCode = "1111" };

            model1.Brand = brand1;
            model2.Brand = brand2;

            completeSet1.Model = model1;
            completeSet2.Model = model2;
            completeSet2.Order = order2;

            car1.CompleteSet = completeSet1;
            car2.CompleteSet = completeSet2;
            car1.Color = color1;
            car2.Color = color2;
            car1.AvailabilityCar = availabilityCar1;
            car2.AvailabilityCar = availabilityCar2;

            availabilityCar1.Storage = storage1;
            availabilityCar2.Storage = storage2;

            order2.Invoice = invoice2;

            invoice1.Client = client1;
            invoice2.Client = client2;
            invoice1.Car = car1; 
            invoice2.Car = car2;

            context.Add(model1);
            context.Add(model2);

            context.Add(completeSet1);
            context.Add(completeSet2);

            context.Add(car1);
            context.Add(car2);

            context.Add(availabilityCar1);
            context.Add(availabilityCar2);

            context.Add(order2);

            context.Add(invoice1);
            context.Add(invoice2);

            context.SaveChanges();
        }
    }
}
