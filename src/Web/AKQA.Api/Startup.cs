using AKQA.Api.Filters;
using AKQA.Application.Customers.Commands.CreateCustomer;
using AKQA.Application.Customers.Queries;
using AKQA.Application.Infrastructure;
using AKQA.Application.Interfaces;
using AKQA.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

namespace AKQA.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new Info
                {
                    Title = "AKQA API",
                    Version = "v1.1",
                    Description = "Simple RESTful API built with ASP.NET Core 2.1 to show how to create RESTful services using a decoupled, maintainable architecture.",
                    Contact = new Contact
                    {
                        Name = "Durgesh Khandal",
                        Url = "https://github.com/techduggu",
                    },
                    License = new License
                    {
                        Name = "MIT",
                    },
                });
            });

            //Register Services
            services.AddScoped<IAppDbContext, AppDbContext>();

            // Add MediatR
            services.AddMediatR(typeof(GetCustomerQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AKQADatabase"));
            });

            //Enable CORS
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            
            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AKQA API");
            });
            app.UseCors("CorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Process}");
            });
        }
    }
}
