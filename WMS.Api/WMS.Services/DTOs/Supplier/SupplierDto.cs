namespace WMS.Services.DTOs.Supplier;

public record SupplierDto(
    int Id, 
    string FullName, 
    string PhoneNumber, 
    decimal Balance);
