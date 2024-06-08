using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class SupplyItem : EntityBase
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int SupplyId { get; set; }
    public virtual Supply Supply { get; set; }
}
