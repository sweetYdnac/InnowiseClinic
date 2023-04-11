using Serilog;
using Serilog.Events;
using Services.API.Extensions;
using Services.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(
    Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
    builder.Configuration.GetValue<string>("LogPath"));

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File(logPath, LogEventLevel.Error)
    .WriteTo.Console(LogEventLevel.Debug));

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureValidation();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();