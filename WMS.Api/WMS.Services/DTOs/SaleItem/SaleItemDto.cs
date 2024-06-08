namespace WMS.Services.DTOs.SaleItem;

public class SaleItemDto
{
    public int Quantity { get; init; }
    public int UnitPrice { get; init; }
    public int SaleId { get; set; }
    public int ProductId { get; init; }

}
