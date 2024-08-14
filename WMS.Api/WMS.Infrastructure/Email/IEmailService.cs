using WMS.Infrastructure.Email.Models;

namespace WMS.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(EmailMetadata metadata);
}
