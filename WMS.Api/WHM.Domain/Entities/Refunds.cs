using WMS.Domain.Common;

namespace WMS.Domain.Entities;

internal class Refund:EntityBase
{
    public DateTime Date { get; set; }
    public decimal TotalDue { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; }
    public IEnumerable<RefundItems> RefundItems{ get; set; }
}
