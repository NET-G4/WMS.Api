using Microsoft.EntityFrameworkCore;
using WMS.Infrastructure.Email;
using WMS.Infrastructure.Email.Factories;
using WMS.Infrastructure.Models;
using WMS.Infrastructure.Persistence;
using WMS.Infrastructure.Sms.Interfaces;

namespace WMS.Api.Jobs;

public class CustomerDebtNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IEmailMetadaFactory _emailMetadataFactory;
    private readonly ILogger<CustomerDebtNotificationService> _logger;

    public CustomerDebtNotificationService(
        IServiceScopeFactory serviceScopeFactory,
        IEmailMetadaFactory emailMetadataFactory,
        ILogger<CustomerDebtNotificationService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _emailMetadataFactory = emailMetadataFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendNotificationsAsync();

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task SendNotificationsAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<WmsDbContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var smsService = scope.ServiceProvider.GetRequiredService<ISmsService>();

        var customers = await context.Customers
            .Where(x => x.Balance < 0)
            .ToListAsync();

        foreach (var customer in customers)
        {
            var userInfo = new UserInfo("khayitovabdurakhmon27@gmail.com", customer.FirstName);
            var metadata = _emailMetadataFactory.Create(
                EmailType.DebtNotification,
                userInfo.Email,
                userInfo);
            var smsMetaData = new SmsMetadata(
                "Abdurraxmon", 
                "934317077", 
                "Bu eskiz test");

            try
            {
                await emailService.SendAsync(metadata);
                await smsService.SendAsync(smsMetaData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
