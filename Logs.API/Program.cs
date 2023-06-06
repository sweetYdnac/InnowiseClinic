using Logs.API.Extensions;
using Logs.API.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(LogEventLevel.Debug));

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureValidation();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureMassTransit();
builder.Services.ConfigureApiVersioning();

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

app.Run();
