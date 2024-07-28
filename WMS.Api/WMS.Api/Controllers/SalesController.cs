using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Services.DTOs.Sale;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[Route("api/sales")]
[ApiController]
[Authorize(Roles = "Manager")]
public class SalesController(ISaleService saleService) : ControllerBase
{
    private readonly ISaleService _saleService = saleService
        ?? throw new ArgumentNullException(nameof(saleService));

    // GET: api/<SalesController>
    [HttpGet]
    public async Task<ActionResult<List<SaleDto>>> GetSalesAsync()
    {
        var sales = await _saleService.GetSales();

        return Ok(sales);
    }

    // GET api/<SalesController>/5
    [HttpGet("{id:int}", Name = "GetSaleById")]
    public ActionResult<SaleDto> GetSaleById(int id)
    {
        var sale = _saleService.GetSaleById(id);

        return Ok(sale);
    }

    // POST api/<SalesController>
    [HttpPost]
    public ActionResult<SaleDto> Post([FromBody] SaleForCreateDto sale)
    {
        var createdSale = _saleService.Create(sale);

        return Created("GetSaleById", createdSale);
    }

    // PUT api/<SalesController>/5
    [HttpPut("{id:int}")]
    public ActionResult UpdateSale(int id, SaleForUpdateDto sale)
    {
        _saleService.Update(sale);

        return NoContent();
    }

    // DELETE api/<SalesController>/5
    [HttpDelete("{id}")]
    public ActionResult DeleteSale(int id)
    {
        _saleService.Delete(id);

        return NoContent();
    }
}
