using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Services.DTOs.Supplier;
using WMS.Services.Interfaces;


namespace WMS.Api.Controllers;

[Route("api/suppliers")]
[ApiController]
[Authorize]
public class SuppliersController(ISupplierService service) : ControllerBase
{
    private readonly ISupplierService _supplierService = service
       ?? throw new ArgumentNullException(nameof(service));
   
    [HttpGet]
    public ActionResult<List<SupplierDto>> Get()
    {
        var suppliers = _supplierService.GetSuppliers();

        return Ok(suppliers);
    }

    [HttpGet("{id:int}", Name = "GetSupplierById")]
    public ActionResult<SupplierDto> Get(int id)
    {
        var supplier = _supplierService.GetSupplierById(id);

        return Ok(supplier);
    }

    [HttpPost]
    public ActionResult<SupplierDto> Post(SupplierForCreateDto supplierForCreateDto)
    {
        var createdSupplier = _supplierService.Create(supplierForCreateDto);

        return Created("GetSupplierById", new { createdSupplier.Id });
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, SupplierForUpdateDto supplier) 
    {
        _supplierService.Update(supplier);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _supplierService.Delete(id);

        return NoContent();
    }
}
