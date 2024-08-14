namespace WMS.Domain.QueryParameters;

public abstract class QueryParametersBase
{
    /// <summary>
    /// Search value to apply.
    /// </summary>
    public string? Search { get; set; }
}
