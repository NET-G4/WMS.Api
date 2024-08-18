using WMS.Infrastructure.Models;

namespace WMS.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(EmailMetadata metadata);
}
