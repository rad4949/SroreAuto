using StoreAuto.EF;
using System;

namespace StoreAuto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureCreated();
        }
    }
}
