using Authorization.API.Consumers;
using Authorization.API.Validators;
using Authorization.Business.Abstractions;
using Authorization.Business.ServicesImplementations;
using Authorization.Data;
using Authorization.Data.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authorization.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthorizationDbConnection");
            var migrationAssembly = typeof(AuthorizationDbContext).Assembly.GetName().Name;

            services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(migrationAssembly)));
        }

        public static void ConfigureAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Account, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddSignInManager<SignInManager<Account>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddUserManager<UserManager<Account>>()
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityServer(options => options.IssuerUri =
                    configuration.GetValue<string>("JWTBearerConfiguration:Issuer"))
                .AddAspNetIdentity<Account>()
                .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
                .AddInMemoryClients(IdentityServerConfiguration.Clients)
                .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
                .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
                .AddDeveloperSigningCredential();
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Environment.CurrentDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UpdateAccountStatusConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
            });
        }
    }
}
