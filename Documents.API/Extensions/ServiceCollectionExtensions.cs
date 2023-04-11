using Documents.API.Consumers.AppointmentResult;
using Documents.API.Consumers.Photo;
using Documents.Business.Implementations;
using Documents.Business.Interfaces;
using Documents.Data.Configurations;
using Documents.Data.Implementations;
using Documents.Data.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Request.Services.Specialization.SwaggerExamples;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Documents.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IAppointmentResultsService, AppointmentsResultService>();
            services.AddScoped<IFileGeneratorService, FileGeneratorService>();
        }

        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentResultsRepository, AppointmentResultsRepository>();
            services.AddTransient<IPhotosRepository, PhotosRepository>();
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

            services.AddSwaggerExamplesFromAssemblyOf<CreateSpecializationRequestExample>();
        }

        internal static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
            services.AddMassTransit(x =>
            {
                x.AddConsumer<DeletePhotoConsumer>();
                x.AddConsumer<GeneratePdfConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
            });
        }

        internal static void ConfigureAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(
                    configuration.GetConnectionString("AzuriteBlobConnection"));
            });

            services.AddSingleton(provider =>
            {
                var config = new AzuriteConfiguration();
                configuration.Bind("AzuriteConfiguration", config);
                return config;
            });
        }

        internal static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
        }
    }
}
