using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Customer : EntityBase
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; }
    public decimal Discount { get; set; }

    public virtual ICollection<Sale> Sales { get; set; }
}
