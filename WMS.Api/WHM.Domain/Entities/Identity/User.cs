using Microsoft.AspNetCore.Identity;

namespace WMS.Domain.Entities.Identity;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
}
