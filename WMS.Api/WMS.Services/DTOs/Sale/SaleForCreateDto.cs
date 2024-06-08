using WMS.Services.DTOs.SaleItem;

namespace WMS.Services.DTOs.Sale
{
    public class SaleForCreateDto
    {
        public DateTime Date { get; init; }
        public decimal TotalPaid { get; init; }
        public int CustomerId { get; init; }

        public ICollection<SaleItemForCreateDto> SaleItems { get; set; }
    }
}
