using FluentValidation;
using Immobilier.Domain;
using Immobilier.Domain.Validators;
using Immobilier.Host.Requests;
using Immobilier.Host.Validators;
using Immobilier.Infrastructure.Auth;
using Immobilier.Infrastructure.Auth.Contracts;
using Immobilier.Infrastructure.Config;
using Immobilier.Infrastructure.Repository;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

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
            services.AddHealthChecks();

            #region Services and Repos

            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            #endregion

            #region Auth

            var authKey = Configuration.GetValue<string>("Auth:Key");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<ITokenService, TokenService>(x => new TokenService(authKey));

            #endregion

            #region Validators

            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
            services.AddScoped<IValidator<CreatePropertyRequest>, CreatePropertyValidator>();
            services.AddScoped<IValidator<EditPropertyRequest>, EditPropertyValidator>();

            #endregion

            #region Database

            var connection = Configuration["PostgreSql:PostgreSqlConnectionString"];

            services.AddDbContextPool<AppDbContext>(options => 
            {
                options.UseNpgsql(connection, b => b.MigrationsAssembly("Immobilier.Host"));
            });

            #endregion

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy => {
                        policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
