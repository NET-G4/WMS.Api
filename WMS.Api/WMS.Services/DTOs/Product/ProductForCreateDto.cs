namespace WMS.Services.DTOs.Product;

public record ProductForCreateDto(
    string Name, 
    string? Description,
    decimal SalePrice,
    decimal SupplyPrice,
    int QuantityInStock,
    int LowQuantityAmount,
    int CategoryId);
