using Appointments.Write.API.Validators.Appointment;
using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using Appointments.Write.Persistence.Contexts;
using Appointments.Write.Persistence.Implementations.Repositories;
using Appointments.Write.Persistence.Implementations.Services;
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

namespace Appointments.Write.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
        }

        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentsRepository, AppointmentsRepository>();
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
            services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestValidator>();
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
            services.AddMassTransit(x => x.UsingRabbitMq());

            EndpointConvention.Map<CreateAppointmentMessage>(
    new Uri(configuration.GetValue<string>("Messages:CreateAppointmentEndpoint")));
            EndpointConvention.Map<RescheduleAppointmentMessage>(
    new Uri(configuration.GetValue<string>("Messages:RescheduleAppointmentEndpoint")));
            EndpointConvention.Map<CancelAppointmentMessage>(
    new Uri(configuration.GetValue<string>("Messages:CancelAppointmentEndpoint")));
            EndpointConvention.Map<ApproveAppointmentMessage>(
    new Uri(configuration.GetValue<string>("Messages:ApproveAppointmentEndpoint")));
        }

        internal static void ConfigureMediatR(this IServiceCollection services) =>
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateAppointmentCommand>());
    }
}
