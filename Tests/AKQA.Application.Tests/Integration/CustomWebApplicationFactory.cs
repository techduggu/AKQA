using AKQA.Application.Interfaces;
using AKQA.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AKQA.Application.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (AppDbContext)
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<IAppDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var concreteContext = (AppDbContext)context;

                    // Ensure the database is created.
                    concreteContext.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        Utilities.InitializeDbForTests(concreteContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
