namespace WMS.Domain.QueryParameters;

public class SaleQueryParameters : QueryParametersBase
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
