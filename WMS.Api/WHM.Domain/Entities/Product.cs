using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SupplyPrice { get; set; }
    public int QuantityInStock { get; set; }
    public int LowQuantityAmount { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; }
}
