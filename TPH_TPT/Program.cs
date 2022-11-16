using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace TPH_TPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            AddData();
            ReadData_TPH();
        }

        public static void CreateDatabase()
        {
            using var dbContext = new ApplicationDbContext_TPH();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        public static void AddData()
        {
            using var dbContext = new ApplicationDbContext_TPH();

            var advancedSubscription = new AdvancedSubscription
            {
                ExpiredAt = new DateTimeOffset(
                    2007,
                    1,
                    1,
                    1,
                    1,
                    1,
                    TimeSpan.Zero),
                MaximumCoursesAllowedPerMonth = 5,
                Price = 10
            };

            var premiumSubscription = new PremiumSubscription
            {
                ExpiredAt = new DateTimeOffset(
                    2015,
                    1,
                    1,
                    1,
                    1,
                    1,
                    TimeSpan.Zero),
                AdditionalDiscount = 25,
                Price = 20
            };

            dbContext.Add(advancedSubscription);
            dbContext.Add(premiumSubscription);

            dbContext.SaveChanges();
        }

        public static void ReadData_TPH()
        {
            using var dbContext = new ApplicationDbContext_TPH();

            var advancedSubscriptions = dbContext.AdvancedSubscriptions.ToList();

            foreach (var advancedSubscription in advancedSubscriptions)
            {
                Console.WriteLine($"Продвинутая подписка. Цена: {advancedSubscription.Price}.");
            }

            var premiumSubscriptions = dbContext.PremiumSubscriptions.ToList();

            foreach (var premiumSubscription in premiumSubscriptions)
            {
                Console.WriteLine($"Премиум подписка. Цена: {premiumSubscription.Price}.");
            }
        }
    }

    public class ApplicationDbContext_TPH : DbContext
    {
        public DbSet<AdvancedSubscription> AdvancedSubscriptions { get; set; }

        public DbSet<PremiumSubscription> PremiumSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=TPH_TPT;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // обязательно необходимо это указать
            // если не указать, EF Core сгенерирует две таблицы AdvancedSubscriptions & PremiumSubscriptions
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");

            // настраиваем общую колонку
            // иначе будет две колонки в таблице, которые будут отличаться префиксом названия таблицы
            modelBuilder
                .Entity<AdvancedSubscription>()
                .Property(b => b.ExpiredAt)
                .HasColumnName(nameof(AdvancedSubscription.ExpiredAt));

            modelBuilder
                .Entity<PremiumSubscription>()
                .Property(b => b.ExpiredAt)
                .HasColumnName(nameof(AdvancedSubscription.ExpiredAt));
        }
    }

    public class ApplicationDbContext_TPH_Discriminator : DbContext
    {
        public DbSet<AdvancedSubscription> AdvancedSubscriptions { get; set; }

        public DbSet<PremiumSubscription> PremiumSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=TPH_TPT;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");

            modelBuilder
                .Entity<Subscription>()
                .Property(t => t.SubscriptionType)
                // просто для красоты представляем тип подписки в базе как строку вместо числа
                .HasConversion<string>();

            modelBuilder
                .Entity<Subscription>()
                // задаем свойство класса в виде дискриминатора
                .HasDiscriminator(x => x.SubscriptionType)
                // добавляем возможные значения заданного дискриминатора
                .HasValue<AdvancedSubscription>(SubscriptionType.Advanced)
                .HasValue<PremiumSubscription>(SubscriptionType.Premium);
        }
    }

    public abstract class Subscription
    {
        public int Id { get; set; }

        public decimal Price { get; set; }
    }

    public class AdvancedSubscription : Subscription
    {
        public DateTimeOffset ExpiredAt { get; set; }

        public int MaximumCoursesAllowedPerMonth { get; set; }
    }

    public class PremiumSubscription : Subscription
    {
        public DateTimeOffset ExpiredAt { get; set; }

        public int AdditionalDiscount { get; set; }
    }
}
