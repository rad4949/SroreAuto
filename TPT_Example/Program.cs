using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace TPT_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            AddData();
            ReadData();
        }

        public static void CreateDatabase()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        public static void AddData()
        {
            using var dbContext = new ApplicationDbContext();

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

        public static void ReadData()
        {         
            using var dbContext = new ApplicationDbContext();

            var advancedSubscriptions = dbContext.AdvancedSubscriptions.ToList();

            foreach (var advancedSubscription in advancedSubscriptions)
            {
                Console.WriteLine($"Advanced subscription. Price: {advancedSubscription.Price}.");
            }

            var premiumSubscriptions = dbContext.PremiumSubscriptions.ToList();

            foreach (var premiumSubscription in premiumSubscriptions)
            {
                Console.WriteLine($"Premium subscription. Price: {premiumSubscription.Price}.");
            }
        }
    }

    public class ApplicationDbContext : DbContext
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

            modelBuilder.Entity<AdvancedSubscription>().ToTable("AdvancedSubscriptions");
            modelBuilder.Entity<PremiumSubscription>().ToTable("PremiumSubscriptions");
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
