using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WMS.Api.Extensions;
using WMS.Api.Jobs;
using WMS.Api.Middlewares;
using WMS.Infrastructure.Persistence;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs_.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error_.txt", Serilog.Events.LogEventLevel.Fatal, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddHostedService<CustomerDebtNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<WmsDbContext>();

    DatabaseSeeder.SeedDatabase(context);
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
