using WMS.Services.DTOs.Sale;

namespace WMS.Services.Interfaces;

public interface ISaleService
{
    List<SaleDto> GetSales();
    SaleDto GetSaleById(int id);
    SaleDto Create(SaleForCreateDto sale);
    void Update(SaleForUpdateDto sale);
    void Delete(int id);
}
