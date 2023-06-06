using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Offices.API.Validators;
using Offices.Business.Implementations.Services;
using Offices.Business.Interfaces.Services;
using Offices.Data.Contexts;
using Offices.Data.Helpers;
using Offices.Data.Implementations.Repositories;
using Offices.Data.Interfaces.Repositories;
using Offices.Data.Migrations;
using Shared.Messages;
using Shared.Models.Request.Offices.SwaggerExamples;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Offices.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IOfficeRepository, OfficeRepository>();
        }

        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<OfficesDbContext>();
            services.AddScoped<DatabaseInitializer>();
            services.AddFluentMigratorCore()
                .ConfigureRunner(r => r
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("OfficesDbConnection"))
                .ScanIn(typeof(InitialTables_202302010714).Assembly));
            services.MigrateDatabase();
        }

        internal static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Environment.CurrentDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.ExampleFilters();
            });

            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerExamplesFromAssemblyOf<CreateOfficeRequestExample>();
        }

        internal static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<GetOfficesRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }

        internal static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("JWTBearerConfiguration:Authority");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(new BaseResponse(
                            HttpStatusCode.Unauthorized,
                            "Unauthorized",
                            "Please, check your token."
                            ).ToString());
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync(new BaseResponse(
                            HttpStatusCode.Forbidden,
                            "Forbidden",
                            "Please, check your token."
                            ).ToString());
                    }
                };
            });
        }

        internal static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x => x.UsingRabbitMq());

            EndpointConvention.Map<DisableOfficeMessage>(new Uri(configuration.GetValue<string>("Messages:DisableOfficeEndpoint")));
            EndpointConvention.Map<UpdateOfficeMessage>(new Uri(configuration.GetValue<string>("Messages:UpdateOfficeEndpoint")));
            EndpointConvention.Map<AddLogMessage>(new Uri(configuration.GetValue<string>("Messages:AddLogEndpoint")));
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

        private static void MigrateDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                databaseInitializer.CreateDatabase();

                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationService.MigrateUp();
            };
        }
    }
}
