using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Sale : EntityBase
{
    public DateTime Date { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; }
}
