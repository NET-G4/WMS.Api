using Microsoft.AspNetCore.Mvc;
using WMS.Services.DTOs.Dashboard;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboardAsync()
    {
        var dashboard = await _dashboardService.GetDashboardAsync();

        return Ok(dashboard);
    }
}
