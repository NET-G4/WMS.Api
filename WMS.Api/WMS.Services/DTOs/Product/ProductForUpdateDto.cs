namespace WMS.Services.DTOs.Product;

public record ProductForUpdateDto(
    int Id, 
    string Name, 
    string? Description, 
    int CategoryId);
