using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class SaleItem : EntityBase
{
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }

    public int SaleId { get; set; }
    public virtual Sale Sale { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}