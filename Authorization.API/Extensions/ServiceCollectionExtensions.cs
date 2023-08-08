using Authorization.API.Consumers;
using Authorization.API.Validators;
using Authorization.Business.Abstractions;
using Authorization.Business.ServicesImplementations;
using Authorization.Data;
using Authorization.Data.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;
using System.Reflection;

namespace Authorization.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthorizationDbConnection");
            var migrationAssembly = typeof(AuthorizationDbContext).Assembly.GetName().Name;

            services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(migrationAssembly)));
        }

        internal static void ConfigureAspNetIdentity(this IServiceCollection services)
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

        internal static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
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

        internal static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
            });

            services.AddFluentValidationRulesToSwagger();
        }

        internal static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }

        internal static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UpdateAccountStatusConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
            });

            EndpointConvention.Map<AddLogMessage>(
                new Uri(configuration.GetValue<string>("Messages:AddLogEndpoint")));
        }

        internal static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                }));
        }
    }
}
