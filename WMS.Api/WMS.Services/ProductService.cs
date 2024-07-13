using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Domain.QueryParameters;
using WMS.Infrastructure.Persistence;
using WMS.Services.Common;
using WMS.Services.DTOs.Category;
using WMS.Services.DTOs.Product;
using WMS.Services.Extensions;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class ProductService(IMapper mapper, WmsDbContext context) : IProductService
{
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    private readonly WmsDbContext _context = context ??
        throw new ArgumentNullException(nameof(context));

    public ProductDto Create(ProductForCreateDto productToCreate)
    {
        var entity = _mapper.Map<Product>(productToCreate);
        var createdEntity = _context.Products.Add(entity).Entity;

        _context.SaveChanges();

        return _mapper.Map<ProductDto>(createdEntity);
    }

    public void Delete(int id)
    {
        var entity = _context.Products.FirstOrDefault(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Product with id: {id} does not exist.");
        }

        _context.Products.Remove(entity);
        _context.SaveChanges();
    }

    public PaginatedList<ProductDto> GetAll(ProductQueryParameters queryParameters)
    {
        var query = _context.Products
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            query = query.Where(x => x.Name.Contains(queryParameters.Search) ||
                (x.Description != null && x.Description.Contains(queryParameters.Search)));
        }

        if (queryParameters.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == queryParameters.CategoryId);
        }

        var products = query.ToPaginatedList<ProductDto, Product>(_mapper.ConfigurationProvider, 1, 15);

        return products;
    }

    public ProductDto GetById(int id)
    {
        var entity = _context.Products.FirstOrDefault(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Product with id: {id} does not exist.");
        }

        return _mapper.Map<ProductDto>(entity);
    }

    public void Update(ProductForUpdateDto productToUpdate)
    {
        if (!_context.Products.Any(x => x.Id == productToUpdate.Id))
        {
            throw new EntityNotFoundException($"Product with id: {productToUpdate.Id} does not exist.");
        }

        var entity = _mapper.Map<Product>(productToUpdate);
        
        _context.Products.Update(entity);
        _context.SaveChanges();
    }
}
