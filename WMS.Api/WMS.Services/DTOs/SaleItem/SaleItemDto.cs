namespace WMS.Services.DTOs.SaleItem;

public class SaleItemDto
{
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public int SaleId { get; set; }
    public int ProductId { get; init; }
    public string Product { get; init; }
}
