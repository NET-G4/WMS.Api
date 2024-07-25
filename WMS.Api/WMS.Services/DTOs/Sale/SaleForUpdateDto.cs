using System.ComponentModel.DataAnnotations;
using WMS.Services.DTOs.SaleItem;

namespace WMS.Services.DTOs.Sale;

public class SaleForUpdateDto
{
    public int Id { get; set; }
    public DateTime Date { get; init; }
    public decimal TotalPaid { get; init; }
    public int CustomerId { get; init; }
    [Required]
    public IEnumerable<SaleItemForCreateDto> SaleItems { get; set; }
}
