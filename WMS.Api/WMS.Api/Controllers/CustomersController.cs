using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Services.DTOs.Customer;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[Route("api/customers")]
[ApiController]
[Authorize(Roles = "Admin,Manager")]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService 
        ?? throw new ArgumentNullException(nameof(customerService));

    [HttpGet]
    [HttpHead]
    public ActionResult<List<CustomerDto>> GetCustomers()
    {
        var customers = _customerService.GetCustomers();

        return Ok(customers);
    }

    [HttpGet("{id:int}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id)
    {
        var customer = _customerService.GetById(id);

        return Ok(customer);
    }

    [HttpPost]
    public ActionResult<CustomerDto> Create(CustomerForCreateDto customer)
    {
        var createdCustomer = _customerService.Create(customer);

        return Created("GetCustomerById", new { createdCustomer.Id });
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateCustomer(CustomerForUpdateDto customer)
    {
        _customerService.Update(customer);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteCustomer(int id)
    {
        _customerService.Delete(id);

        return NoContent();
    }
}
