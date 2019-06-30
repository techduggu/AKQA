using AKQA.Domain.Entities;
using AKQA.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace AKQA.Application.Tests.Infrastructure
{
    public class AKQAContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Database.EnsureCreated();

            //Seed some Test Data
            context.Customers.AddRange(new[] {
                new Customer { CustomerId = new Guid("2a1413bc-2d93-4c6c-a3d3-fa20ec4b14aa"), FirstName = "Thomas", MiddleName = "Tom", LastName = "Chandler", Amount = new Decimal(123.45) },
                new Customer { CustomerId = new Guid("96e9bd65-d91f-4195-a573-611db7cb66ca"), FirstName = "Sasha", LastName = "Cooper", Amount = new Decimal(0.25) }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
