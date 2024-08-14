using FluentEmail.MailKitSmtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WMS.Infrastructure.Configurations;
using WMS.Infrastructure.Email;
using WMS.Infrastructure.Email.Factories;
using WMS.Infrastructure.Persistence;

namespace WMS.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WmsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        AddConfigurations(services, configuration);
        AddFluentEmail(services, configuration);
        AddEmailServices(services);
    }

    private static void AddConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<EmailConfiguration>()
            .Bind(configuration.GetSection(EmailConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddFluentEmail(IServiceCollection services, IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(EmailConfiguration.SectionName).Get<EmailConfiguration>();

        if (emailSettings is null)
        {
            throw new InvalidOperationException("Configuration values for email did not load correctly.");
        }

        var smptOptions = new SmtpClientOptions
        {
            Server = emailSettings.Server,
            Port = emailSettings.Port,
            User = emailSettings.From,
            Password = emailSettings.Password,
            UseSsl = true,
            RequiresAuthentication = true
        };

        services.AddFluentEmail(emailSettings.From, emailSettings.UserName)
            .AddMailKitSender(smptOptions)
            .AddRazorRenderer();
    }

    private static void AddEmailServices(IServiceCollection services)
    {
        services.AddSingleton<IEmailMetadaFactory, EmailMetadataFactory>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
