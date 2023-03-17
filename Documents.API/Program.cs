using Documents.API.Extensions;
using Documents.API.Middlewares;
using Documents.Business.Configuration;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(
    Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
    builder.Configuration.GetValue<string>("LogPath"));

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File(logPath, LogEventLevel.Error)
    .WriteTo.Console(LogEventLevel.Debug));

builder.Services.AddSingleton<AzuriteConfiguration>();

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureMassTransit();
builder.Services.ConfigureAzureClients(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.ApplyMigrations();

app.Run();