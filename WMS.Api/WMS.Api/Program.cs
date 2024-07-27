using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using WMS.Api.Extensions;
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

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM1NzIyMUAzMjMxMmUzMTJlMzMzNUxoanpuL3lrdk9BK0dvWXY4RE9TMVVPWEhlak9OOSt0ditkK01saHprYUE9;MzM1NzIyMkAzMjMxMmUzMTJlMzMzNW9HM3RNZVZodFZzdnpPdUdsMll6cXZPRVBuQmZzMktpR3FYYzErQ0Jnam89;Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRbQltgSX9TdEZhWH1XcX0=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9nSXhQdkRjW3xccXdVTmY=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5VdUViWX1dcnVWRGRf;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3pTcEZjWH9cc3ZURWRcVQ==;MzM1NzIyN0AzMjMxMmUzMTJlMzMzNW51c25xdVJHalFiejRPMHNRNDNielRJY2dJKzJwWEM1dHBKQWNPdkc3ZEk9;MzM1NzIyOEAzMjMxMmUzMTJlMzMzNW9tL3VYYmpITFgxeHRydDVucGYzVjBDbURvZldXRDR6UXVteGVzMi9wVkk9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5VdUViWX1dcnVRQ2Vb;MzM1NzIzMEAzMjMxMmUzMTJlMzMzNWx6QXZBR3NrYzVnUDdQaXl3MG10MUhsNWp2YlVYUGN1dGNwUmJKTEY2bHM9;MzM1NzIzMUAzMjMxMmUzMTJlMzMzNWZvMURCcldUQnBqY29zbmRydDRqdjJZMGtoYXh0K2VHWHQvYnlFNFRmS009;MzM1NzIzMkAzMjMxMmUzMTJlMzMzNW51c25xdVJHalFiejRPMHNRNDNielRJY2dJKzJwWEM1dHBKQWNPdkc3ZEk9");

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
