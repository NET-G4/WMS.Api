namespace WMS.Services.DTOs.Sale;

public class SaleForUpdateDto
{
    public int Id { get; set; }
    public DateTime Date { get; init; }
    public decimal TotalPaid { get; init; }
    public int CustomerId { get; init; }
}
