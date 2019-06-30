using AKQA.Domain.Entities;
using AKQA.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AKQA.Application.Tests.Integration
{
    public class Utilities
    {
        public const string AKQAApiURL = "https://localhost:44343";

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(AppDbContext context)
        {
            context.Database.EnsureCreated();

            context.Customers.AddRange(new[] {
                new Customer { CustomerId = new Guid("2a1413bc-2d93-4c6c-a3d3-fa20ec4b14aa"), FirstName = "Thomas", MiddleName = "Tom", LastName = "Chandler", Amount = new Decimal(123.45) },
                new Customer { CustomerId = new Guid("96e9bd65-d91f-4195-a573-611db7cb66ca"), FirstName = "Sasha", LastName = "Cooper", Amount = new Decimal(0.25) }
            });

            context.SaveChanges();
        }
    }
}
