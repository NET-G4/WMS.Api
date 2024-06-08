namespace WMS.Services.DTOs.SaleItem;

public record SaleItemForCreateDto(
    int ProductId, 
    int Quantity, 
    decimal UnitPrice);
