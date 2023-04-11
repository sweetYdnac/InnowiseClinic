using Appointments.Read.API.Consumers;
using Appointments.Read.API.Consumers.Appointment;
using Appointments.Read.API.Consumers.AppointmentResult;
using Appointments.Read.Application.Features.Commands.Appointments;
using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Persistence.Contexts;
using Appointments.Read.Persistence.Implementations.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Messages;
using Shared.Models.Request.Appointments.Appointment.SwaggerExamples;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Appointments.Read.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentsRepository, AppointmentsRepository>();
            services.AddTransient<IAppointmentsResultsRepository, AppointmentsResultsRepository>();
        }

        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppointmentsDbConnection");
            var migrationAssembly = typeof(AppointmentsDbContext).Assembly.GetName().Name;

            services.AddDbContext<AppointmentsDbContext>(options =>
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(migrationAssembly)));
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
            services.AddSwaggerExamplesFromAssemblyOf<CreateAppointmentRequestExample>();
        }

        internal static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationAutoValidation();
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

        internal static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateAppointmentConsumer>();
                x.AddConsumer<CancelAppointmentConsumer>();
                x.AddConsumer<RescheduleAppointmentConsumer>();
                x.AddConsumer<ApproveAppointmentConsumer>();

                x.AddConsumer<CreateAppointmentResultConsumer>();
                x.AddConsumer<EditAppointmentResultConsumer>();

                x.AddConsumer<UpdatePatientConsumer>();
                x.AddConsumer<UpdateDoctorConsumer>();
                x.AddConsumer<UpdateServiceConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
            });
        }

        internal static void ConfigureMediatR(this IServiceCollection services) =>
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateAppointmentCommand>());

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
