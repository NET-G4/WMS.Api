using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Infrastructure.Persistence;
using WMS.Services.DTOs.Sale;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class SaleService(WmsDbContext context, IMapper mapper) : ISaleService
{
    private readonly WmsDbContext _context = context 
        ?? throw new ArgumentNullException(nameof(context));
    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

    public SaleDto Create(SaleForCreateDto sale)
    {
        var entity = _mapper.Map<Sale>(sale);
        entity.TotalDue = sale.SaleItems.Sum(x => x.Quantity * x.UnitPrice);

        foreach (var item in entity.SaleItems)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);

            if (product is null)
            {
                throw new InvalidOperationException($"Cannot create sale for non-existing product: {item.ProductId}.");
            }

            if (product?.QuantityInStock < item.Quantity)
            {
                throw new InvalidOperationException($"Not enough items in stock: {item.Quantity} for product {product.Name}.");
            }

            product.QuantityInStock -= item.Quantity;
        }

        var customer = _context.Customers.FirstOrDefault(x => x.Id == sale.CustomerId);

        if (customer is null)
        {
            throw new InvalidOperationException($"Cannot create sale for customer which does not exist. Customer id: {sale.CustomerId}");
        }

        customer.Balance += entity.TotalPaid - entity.TotalDue;
        var createdSale = _context.Sales.Add(entity).Entity;
        _context.SaveChanges();

        return _mapper.Map<SaleDto>(createdSale);
    }

    public void Delete(int id)
    {
        var sale = _context.Sales.FirstOrDefault(x => x.Id == id);

        if (sale is null)
        {
            throw new EntityNotFoundException($"Sale with id: {id} does not exist.");
        }

        _context.Sales.Remove(sale);
        _context.SaveChanges();
    }

    public SaleDto GetSaleById(int id)
    {
        var sale = _context.Sales.FirstOrDefault(x => x.Id == id);

        if (sale is null)
        {
            throw new EntityNotFoundException($"Sale with id: {id} does not exist.");
        }

        return _mapper.Map<SaleDto>(sale);
    }

    public List<SaleDto> GetSales()
    {
        var sales = _context.Sales.Include(x => x.Customer).ToList();

        return _mapper.Map<List<SaleDto>>(sales);
    }

    public void Update(SaleForUpdateDto sale)
    {
        if (!_context.Sales.Any(x => x.Id == sale.Id))
        {
            throw new EntityNotFoundException($"Sale with id: {sale.Id} does not exist.");
        }

        var entity = _mapper.Map<Sale>(sale);
        _context.Sales.Update(entity);
        _context.SaveChanges();
    }
}
