using Elasticsearch.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using Logs.API.Consumers;
using Logs.API.Validators.LogsAPI;
using Logs.Business.Implementations.Services.v1;
using Logs.Business.Implementations.Services.v2;
using Logs.Business.Interfaces.Services.v1;
using Logs.Business.Interfaces.Services.v2;
using Logs.Data.Configurations;
using Logs.Data.Contexts;
using Logs.Data.Entities;
using Logs.Data.Implementations.Repositories.v1;
using Logs.Data.Implementations.Repositories.v2;
using Logs.Data.Interfaces.Repositories.v1;
using Logs.Data.Interfaces.Repositories.v2;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Shared.Models.Request.Offices.SwaggerExamples;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Logs.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMongoDbLogService, MongoDbLogService>();
            services.AddScoped<IElasticLogService, ElasticLogService>();
        }

        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMongoDbLogRepository, MongoDbLogRepository>();
            services.AddTransient<IElasticLogRepository, ElasticLogRepository>();
        }

        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MonboDbConfigurations>(configuration.GetSection("MongoDbConfigurations"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MonboDbConfigurations>>().Value);
            services.AddSingleton<LogsDbContext>();
        }

        internal static void ConfigureElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUri = configuration.GetValue<string>("ElasticConfigurations:BaseUri");
            var index = configuration.GetValue<string>("ElasticConfigurations:DefaultIndex");

            var pool = new SingleNodeConnectionPool(new Uri(baseUri));
            var settings = new ConnectionSettings(pool)
                .PrettyJson()
                .DefaultIndex(index)
                .DefaultMappingFor<Log>(m => m
                    .IndexName(index));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            client.Indices.Create(index, i => i.Map<Log>(x => x.AutoMap()));
        }

        internal static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.ExampleFilters();
            });

            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerExamplesFromAssemblyOf<CreateOfficeRequestExample>();
        }

        internal static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<GetLogsRequestValidator>();
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

        internal static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x => {
                x.AddConsumer<AddLogConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
            });
        }

        internal static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("x-version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
