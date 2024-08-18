using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Infrastructure.Persistence;
using WMS.Infrastructure.Persistence.Migrations;
using WMS.Services.DTOs.Customer;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class CustomerService(WmsDbContext context, IMapper mapper) : ICustomerService
{
    private readonly WmsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public CustomerDto Create(CustomerForCreateDto customer)
    {
        var entity = _mapper.Map<Customer>(customer);

        var createdEntity = _context.Customers.Add(entity).Entity;
        _context.SaveChanges();

        return _mapper.Map<CustomerDto>(createdEntity);
    }

    public void Delete(int id)
    {
        var entity = _context.Customers.FirstOrDefault(x => x.Id == id);
        
        if (entity is null)
        {
            throw new EntityNotFoundException($"Customer with id: {id} does not exist.");
        }

        _context.Customers.Remove(entity);
        _context.SaveChanges();
    }

    public CustomerDto? GetById(int id)
    {
        var entity = _context.Customers.FirstOrDefault(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Customer with id: {id} does not exist.");
        }

        return _mapper.Map<CustomerDto>(entity);
    }

    public List<CustomerDto> GetCustomers()
    {
        var entities = _context.Customers.AsNoTracking().ToList();

        return _mapper.Map<List<CustomerDto>>(entities);
    }

    public void Update(CustomerForUpdateDto customer)
    {
        if (!_context.Customers.Any(x => x.Id == customer.Id))
        {
            throw new EntityNotFoundException($"Customer with id: {customer.Id} does not exist.");
        }

        var entity = _mapper.Map<Customer>(customer);
        
        _context.Customers.Update(entity);
        _context.SaveChanges();
    }
}
