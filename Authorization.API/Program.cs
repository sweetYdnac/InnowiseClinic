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
                builder.Configuration["LogPath"]);

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.File(logPath, LogEventLevel.Error)
                .WriteTo.Console(LogEventLevel.Debug));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddServices();
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureAspNetIdentity();
            builder.Services.ConfigureIdentityServer(builder.Configuration);
            builder.Services.ConfigureValidation();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = builder.Configuration["JWTBearerConfiguration:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}