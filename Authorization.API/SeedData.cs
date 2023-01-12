using Authorization.Data;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authorization.API
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AuthorizationDbContext>(
                options => options.UseSqlServer(connectionString));

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => 
                    db.UseSqlServer(
                        connectionString,
                        sql => sql.MigrationsAssembly(typeof(AuthorizationDbContext).Assembly.GetName().FullName)
                    );
            });

            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseSqlServer(
                        connectionString,
                        sql => sql.MigrationsAssembly(typeof(AuthorizationDbContext).Assembly.GetName().FullName)
                    );
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AuthorizationDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);
        }

        public static void EnsureUsers(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var evgeny = userManager.FindByNameAsync("evgeny").Result;
            if (evgeny is null)
            {
                evgeny = new IdentityUser
                {
                    UserName = "Evgeny",
                    Email = "evgeny@email.com",
                    EmailConfirmed= true,
                };

                var result = userManager.CreateAsync(evgeny, "beAsT3!").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(
                    evgeny,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Evgeny Koreba"),
                        new Claim(JwtClaimTypes.GivenName, "Evgeny"),   
                        new Claim(JwtClaimTypes.FamilyName, "Koreba"),
                    }
                ).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        public static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }
                    
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}
