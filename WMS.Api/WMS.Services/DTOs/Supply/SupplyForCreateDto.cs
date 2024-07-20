using WMS.Services.DTOs.SupplyItem;

namespace WMS.Services.DTOs.Supply;

public class SupplyForCreateDto
{
    public DateTime Date { get; set; }
    public decimal TotalPaid { get; set; }

    public int SupplierId { get; set; }

    public ICollection<SupplyItemForCreateDto> SupplyItems { get; set; }
}
