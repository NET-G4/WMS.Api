using Microsoft.AspNetCore.Mvc;
using WMS.Services.DTOs.Supply;
using WMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace WMS.Api.Controllers;

[Route("api/supplies")]
[ApiController]
[Authorize]
public class SuppliesController(ISupplyService service) : ControllerBase
{
    private readonly ISupplyService _supplyService = service
        ?? throw new ArgumentNullException(nameof(service));

    [HttpGet]
    public async Task<ActionResult<List<SupplyDto>>> Get()
    {
        var supplies = await _supplyService.GetSupplies();

        return Ok(supplies);
    }

    [HttpGet("{id:int}", Name = "GetSupplyById")]
    public ActionResult<SupplyDto> Get(int id)
    {
        var supply = _supplyService.GetSupplyById(id);

        return Ok(supply);
    }

    [HttpPost]
    public ActionResult<SupplyDto> Post(SupplyForCreateDto supply)
    {
        var createdSupply = _supplyService.Create(supply);

        return Created("GetSupplyById", new { createdSupply.Id });
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, SupplyForUpdateDto supply)
    {
        _supplyService.Update(supply);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _supplyService.Delete(id);

        return NoContent();
    }
}
