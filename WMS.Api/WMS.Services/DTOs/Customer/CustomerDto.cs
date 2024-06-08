namespace WMS.Services.DTOs.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; init; }
    public string PhoneNumber { get; init; }
    public string? Address { get; init; }
    public decimal Balance { get; init; }
    public decimal Discount { get; init; }
}
