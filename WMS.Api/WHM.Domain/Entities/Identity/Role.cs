using Microsoft.AspNetCore.Identity;

namespace WMS.Domain.Entities.Identity;

public class Role : IdentityRole<string>
{
    public string? Description { get; set; }
}
