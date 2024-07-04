namespace WMS.Services.DTOs.Supplier;

public class SupplierDto()
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
}
