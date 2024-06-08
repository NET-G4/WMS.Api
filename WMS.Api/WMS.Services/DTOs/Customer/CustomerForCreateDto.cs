namespace WMS.Services.DTOs.Customer;

public record CustomerForCreateDto(
    string FirstName, 
    string? LastName, 
    string PhoneNumber, 
    string? Address, 
    decimal Balance, 
    decimal Discount);
