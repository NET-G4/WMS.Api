using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using WMS.Domain.Entities;
using WMS.Infrastructure.Persistence;
using WMS.Infrastructure.Persistence.Migrations;
using WMS.Services.DTOs.Dashboard;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class DashboardService(WmsDbContext context) : IDashboardService
{
    private readonly WmsDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var summary = await GetSummaryAsync();
        var salesByCategory = await GetSalesByCategoryAsync();
        var transactions = await GetLatestTransactionsAsync();
        var chartData = await GetChartAsync();

        var dashboard = new DashboardDto
        {
            Summary = summary,
            SalesByCategories = salesByCategory,
            SplineCharts = chartData,
            Transactions = transactions
        };

        return dashboard;
    }

    private async Task<SummaryDto> GetSummaryAsync()
    {
        var summary = new SummaryDto();
        summary.Revenue = _context.Sales
            .AsNoTracking()
            .Sum(x => x.TotalDue) - _context.Supplies.AsNoTracking().Sum(x => x.TotalDue);

        summary.LowQuantityProducts = _context.Products
            .AsNoTracking()
            .Count(x => x.LowQuantityAmount >= x.QuantityInStock);

        summary.CustomersAmount = _context.Customers.AsNoTracking().Count();

        return summary;
    }

    private async Task<List<SalesByCategoryDto>> GetSalesByCategoryAsync()
    {
        var salesByCategory = from category in _context.Categories
                              join product in _context.Products on category.Id equals product.CategoryId
                              join saleItem in _context.SaleItems on product.Id equals saleItem.ProductId
                              join sale in _context.Sales on saleItem.SaleId equals sale.Id
                              where sale.Date.Month == DateTime.Now.Month && sale.Date.Year == DateTime.Now.Year
                              orderby category.Name
                              group saleItem by new { category.Id, category.Name } into groupedCategories
                              select new SalesByCategoryDto
                              {
                                  Category = groupedCategories.Key.Name,
                                  SalesCount = groupedCategories.Count()
                              };

        return await salesByCategory.AsSplitQuery().AsNoTracking().ToListAsync();
    }

    private async Task<List<SplineChart>> GetChartAsync()
    {
        var incomeTotal = _context
            .Sales
            .Where(x => x.Date > DateTime.Now.AddYears(-1))
            .ToList()
            .GroupBy(x => x.Date.ToString("MMMM"))
            .Select(x => new SplineChart
            {
                Month = x.Key,
                Income = x.Sum(x => x.TotalDue)
            });
        var expenseTotal = _context
            .Supplies
            .Where(x => x.Date > DateTime.Now.AddYears(-1))
            .ToList()
            .GroupBy(x => x.Date.ToString("MMMM"))
            .Select(x => new SplineChart
            {
                Month = x.Key,
                Expense = x.Sum(x => x.TotalDue)
            });

        var months = Enumerable.Range(0, 12)
            .Select(x => DateTime.Now.AddMonths(-x).ToString("MMMM"))
            .ToList();

        var chartData = from month in months
                        join income in incomeTotal on month equals income.Month into joinedIncome
                        from income in joinedIncome.DefaultIfEmpty()
                        join expense in expenseTotal on month equals expense.Month into joinedExpense
                        from expense in joinedExpense.DefaultIfEmpty()
                        select new SplineChart
                        {
                            Month = month,
                            Expense = expense?.Expense ?? 0,
                            Income = income?.Income ?? 0,
                        };

        return chartData
            .ToList();
    }

    private async Task<List<TransactionDto>> GetLatestTransactionsAsync()
    {
        var sales = _context
            .Sales
            .Where(x => x.Date.Day == DateTime.Now.Day)
            .Select(x => new TransactionDto
            {
                Id = x.Id,
                Amount = x.TotalPaid,
                Date = x.Date,
                Type = "Sale"
            })
            .AsSplitQuery()
            .AsNoTracking()
            .ToList();
        var supplies = _context.Supplies
            .Where(x => x.Date.Day == DateTime.Now.Day)
            .Select(x => new TransactionDto
            {
                Id = x.Id,
                Amount = x.TotalPaid,
                Date = x.Date,
                Type = "Supply"
            })
            .AsSplitQuery()
            .AsNoTracking()
            .ToList();

        List<TransactionDto> transactions = [.. sales, .. supplies];
        List<TransactionDto> orderedTransaction = transactions.Take(10).OrderBy(x => x.Date).ToList();

        return orderedTransaction;
    }
}
