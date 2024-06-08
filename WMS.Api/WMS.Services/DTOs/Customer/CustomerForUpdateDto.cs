namespace WMS.Services.DTOs.Customer;

public record CustomerForUpdateDto(
    int Id,
    string FirstName, 
    string? LastName, 
    string PhoneNumber, 
    string? Address, 
    decimal Balance, 
    decimal Discount);
