namespace WMS.Domain.QueryParameters;

public class ProductQueryParameters : QueryParametersBase
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int MinStockAmount { get; set; }
}
