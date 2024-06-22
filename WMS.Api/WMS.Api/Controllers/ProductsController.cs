using Microsoft.AspNetCore.Mvc;
using WMS.Domain.QueryParameters;
using WMS.Services.Common;
using WMS.Services.DTOs.Product;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService ?? throw new ArgumentNullException(nameof(productService));

    [HttpGet]
    [HttpHead]
    public ActionResult<PaginatedList<ProductDto>> Get([FromQuery] ProductQueryParameters queryParameters)
    {
        var result = _productService.GetAll(queryParameters);

        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var result = _productService.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<ProductDto> Create(ProductForCreateDto product)
    {
        var result = _productService.Create(product);

        return Created("GetProductById", result);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, ProductForUpdateDto product)
    {
        if (id != product.Id)
        {
            return BadRequest($"Route id: {id} does not match with Product id: {product.Id}.");
        }

        _productService.Update(product);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _productService.Delete(id);

        return NoContent();
    }
}
