namespace WMS.Services.DTOs.Dashboard;

public class DashboardDto
{
    public SummaryDto Summary { get; set; }
    public List<SalesByCategoryDto> SalesByCategories { get; set; }
    public List<SplineChart> SplineCharts { get; set; }
    public List<TransactionDto> Transactions { get; set; }
}

public class SummaryDto
{
    public decimal Revenue { get; set; }
    public int LowQuantityProducts { get; set; }
    public int CustomersAmount { get; set; }
}

public class SalesByCategoryDto
{
    public string Category { get; set; }
    public int SalesCount { get; set; }
}

public class SplineChart
{
    public string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
    public decimal Refunds { get; set; }
}

public class TransactionDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}