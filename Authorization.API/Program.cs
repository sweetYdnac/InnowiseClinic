using Authorization.Business.Abstractions;
using Authorization.Business.ServicesImplementations;
using Authorization.Data;
using Authorization.Data.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

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

            builder.Services.AddIdentity<Account, IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<Account>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddUserManager<UserManager<Account>>()
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<Account>()
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
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:44306";
                    options.ApiName = "Authorization.API";
                })
                .AddOpenIdConnect(opt =>
                {
                    opt.Authority = "https://localhost:44306";
                    opt.ClientId = "interactive";
                    opt.ClientSecret = "ClientSecret1";
                    opt.ResponseType = "code";
                    opt.SaveTokens = true;
                    opt.Scope.Add("openid");
                });

            builder.Services.AddControllers();

            builder.Services.AddScoped<IAccountService, AccountService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(builder.Configuration["XmlDoc"]);
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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