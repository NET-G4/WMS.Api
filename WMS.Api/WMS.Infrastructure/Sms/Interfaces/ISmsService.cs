using WMS.Infrastructure.Models;

namespace WMS.Infrastructure.Sms.Interfaces;

public interface ISmsService
{
    Task SendAsync(SmsMetadata metadata);
}
