using Authorization.Data;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

namespace Authorization.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("AuthorizationDbConnection");
            var migrationAssembly = typeof(AuthorizationDbContext).Assembly.GetName().Name;

            if (seed)
            {
                SeedData.EnsureSeedData(connectionString);
            }

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.File(builder.Configuration["LogPath"], LogEventLevel.Error)
                .WriteTo.Console(LogEventLevel.Debug));

            // Add services to the container.

            builder.Services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(migrationAssembly)));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationDbContext>();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddDeveloperSigningCredential();

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:JwtSecret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddOpenIdConnect(opt =>
                {
                    opt.Authority = "https://localhost:5001";
                    opt.ClientId = "Authorization.API";
                    opt.ClientSecret = "ClientSecret".Sha256();
                    opt.ResponseType = "code";
                    opt.SaveTokens = true;
                });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}