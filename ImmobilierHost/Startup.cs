using FluentValidation;
using Immobilier.DataAccess.Config;
using Immobilier.DataAccess.Repository;
using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Immobilier.Domain.Validators;
using Immobilier.Services;
using Immobilier.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ImmobilierHost
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

            services.AddControllers();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            #region Validators
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Property>, PropertyValidator>();
            #endregion

            #region Database
            var connection = Configuration["MySql:MySqlConnectionString"];
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            services.AddDbContextPool<AppDbContext>(options => 
            {
                options.UseMySql(connection, serverVersion, b => b.MigrationsAssembly("Immobilier.Host"));
            });
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImmobilierHost", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImmobilierHost v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
