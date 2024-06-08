namespace WMS.Services.DTOs.Product;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public decimal SalePrice { get; set; }
    public decimal SupplyPrice { get; set; }
    public int QuantityInStock { get; set; }
    public int LowQuantityAmount { get; set; }

    public int CategoryId { get; init; }
    public string? Category { get; init; }
}
