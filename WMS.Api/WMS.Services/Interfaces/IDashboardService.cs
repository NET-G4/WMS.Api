using WMS.Services.DTOs.Dashboard;

namespace WMS.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}
