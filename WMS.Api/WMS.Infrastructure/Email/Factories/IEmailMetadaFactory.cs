using WMS.Infrastructure.Email.Models;

namespace WMS.Infrastructure.Email.Factories;

public interface IEmailMetadaFactory
{
    EmailMetadata Create(EmailType emailType, string to, 
        UserInfo? userInfo = null, string? callbackUri = null, 
        List<string>? cc = null);
}
