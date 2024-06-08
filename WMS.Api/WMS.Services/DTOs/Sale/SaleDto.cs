using WMS.Services.DTOs.SaleItem;

namespace WMS.Services.DTOs.Sale;

public class SaleDto
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public decimal TotalDue { get; init; }
    public decimal TotalPaid { get; init; }

    public string Customer { get; init; }
    public int CustomerId { get; init; }

    public ICollection<SaleItemDto> SaleItems { get; init; }
}
