using Authorization.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;

namespace Authorization.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logPath = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                builder.Configuration.GetValue<string>("LogPath"));

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.File(logPath, LogEventLevel.Error)
                .WriteTo.Console(LogEventLevel.Debug));

            builder.Services.AddControllers();
            builder.Services.AddServices();
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureAspNetIdentity();
            builder.Services.ConfigureIdentityServer(builder.Configuration);
            builder.Services.ConfigureValidation();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.ConfigureMassTransit();
            builder.Services.ConfigureCors();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = builder.Configuration.GetValue<string>("JWTBearerConfiguration:Authority");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.ApplyMigrations();

            app.Run();
        }
    }
}