using WMS.Services.DTOs.Customer;

namespace WMS.Services.Interfaces;

public interface ICustomerService
{
    List<CustomerDto> GetCustomers();
    CustomerDto? GetById(int id);
    CustomerDto Create(CustomerForCreateDto customer);
    void Update(CustomerForUpdateDto customer);
    void Delete(int id);
}
