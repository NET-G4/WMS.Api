using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Supplier : EntityBase
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; }
}
