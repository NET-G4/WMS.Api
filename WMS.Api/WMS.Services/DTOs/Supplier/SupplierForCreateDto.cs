namespace WMS.Services.DTOs.Supplier;

public record SupplierForCreateDto(
    string FirstName, 
    string? LastName, 
    string PhoneNumber, 
    decimal Balance);
