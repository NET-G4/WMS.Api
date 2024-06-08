namespace WMS.Services.DTOs.Supplier;

public record SupplierForUpdateDto(
    int Id, 
    string FirstName, 
    string? LastName, 
    string PhoneNumber, 
    decimal Balance);
