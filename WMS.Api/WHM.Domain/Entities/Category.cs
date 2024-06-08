using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public virtual List<Product> Products { get; set; }
}
