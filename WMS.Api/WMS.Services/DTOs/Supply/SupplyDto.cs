namespace WMS.Services.DTOs.Supply;

public class SupplyDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }

    public int SupplierId { get; set; }
    public string Supplier { get; set; }
}
