using AzureFunctions.Configurations;
using AzureFunctions.Services.Implementations;
using AzureFunctions.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctions.Startup))]
namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IEmailSenderService, EtherealEmailNotifierService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddOptions<EmailServiceConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                configuration.GetSection("EmailServiceConfiguration").Bind(settings));

            builder.Services.AddOptions<IdentityServerConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                configuration.GetSection("IdentityServer").Bind(settings));
        }
    }
}
