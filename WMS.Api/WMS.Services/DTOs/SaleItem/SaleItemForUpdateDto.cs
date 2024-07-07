namespace WMS.Services.DTOs.SaleItem;

public record SaleItemForUpdateDto(
    int Id,
    int SaleId,
    int ProductId,
    int Quantity,
    decimal UnitPrice);
