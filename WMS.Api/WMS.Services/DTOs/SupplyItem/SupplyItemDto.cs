namespace WMS.Services.DTOs.SupplyItem;

public class SupplyItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int SupplyId { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
