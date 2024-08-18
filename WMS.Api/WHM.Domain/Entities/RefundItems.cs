using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class RefundItems : EntityBase
{
    public int Count { get; set; }
    public int SaleItemId { get; set; }
    public SaleItem SaleItem { get; set; }
    public int RefundId { get; set; }
}