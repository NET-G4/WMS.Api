namespace WMS.Domain.QueryParameters;

public class CustomerQueryParameters : QueryParametersBase
{
    public decimal BalanceLessThan { get; set; }
    public decimal BalanceGreaterThan { get; set; }
}
