using AutoMapper;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Domain.QueryParameters;
using WMS.Infrastructure.Persistence;
using WMS.Services.DTOs.Category;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class CategoryService(IMapper mapper, WmsDbContext context) : ICategoryService
{
    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));
    private readonly WmsDbContext _context = context 
        ?? throw new ArgumentNullException(nameof(context));

    public CategoryDto Create(CategoryForCreateDto categoryToCreate)
    {
        var entity = _mapper.Map<Category>(categoryToCreate);

        var createdEntity = _context.Categories.Add(entity).Entity;
        _context.SaveChanges();

        return _mapper.Map<CategoryDto>(createdEntity);
    }

    public void Delete(int id)
    {
        var entity = _context.Categories.FirstOrDefault(x => x.Id == id);
        
        if (entity is null)
        {
            throw new EntityNotFoundException($"Category with id: {id} does not exist.");
        }

        _context.Categories.Remove(entity);
        _context.SaveChanges();
    }

    public List<CategoryDto> GetAll(CategoryQueryParameters queryParameters)
    {
        var entities = _context.Categories.ToList();

        return _mapper.Map<List<CategoryDto>>(entities);
    }

    public CategoryDto GetById(int id)
    {
        var entity = _context.Categories.FirstOrDefault(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Category with id: {id} does not exist.");
        }

        return _mapper.Map<CategoryDto>(entity);
    }

    public void Update(CategoryForUpdateDto categoryToUpdate)
    {
        if (!_context.Categories.Any(x => x.Id == categoryToUpdate.Id))
        {
            throw new EntityNotFoundException($"Category with id: {categoryToUpdate.Id} does not exist.");
        }

        var entity = _mapper.Map<Category>(categoryToUpdate);
        
        _context.Categories.Update(entity);
        _context.SaveChanges();
    }
}
