using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Supply : EntityBase
{
    public DateTime Date { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }

    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }

    public virtual ICollection<SupplyItem> SupplyItems { get; set; }
}
