using Microsoft.AspNetCore.Mvc;
using WMS.Domain.QueryParameters;
using WMS.Services.DTOs.Category;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

    /// <summary>
    /// Retrieve all categories.
    /// </summary>
    /// <param name="queryParameters">Query parameters for filtering, sorting, and pagination.</param>
    /// <returns>A list of categories.</returns>
    [HttpGet]
    [HttpHead]
    public ActionResult<List<CategoryDto>> Get([FromQuery] CategoryQueryParameters queryParameters)
    {
        var result = _categoryService.GetAll(queryParameters);
        return Ok(result);
    }

    /// <summary>
    /// Retrieve a category by ID.
    /// </summary>
    /// <param name="id">ID of the category to retrieve.</param>
    /// <returns>The requested category.</returns>
    [HttpGet("{id:int}", Name = "GetCategoryById")]
    public ActionResult<CategoryDto> GetById(int id)
    {
        var result = _categoryService.GetById(id);
        return Ok(result);
    }

    /// <summary>
    /// Create a new category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    /// <returns>The newly created category.</returns>
    [HttpPost]
    public ActionResult<CategoryDto> Create(CategoryForCreateDto category)
    {
        var result = _categoryService.Create(category);
        return Created("GetCategoryById", result);
    }

    /// <summary>
    /// Update a category.
    /// </summary>
    /// <param name="id">ID of the category to update.</param>
    /// <param name="category">The updated category data.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id:int}")]
    public ActionResult Update(int id, CategoryForUpdateDto category)
    {
        if (id != category.Id)
        {
            return BadRequest($"Route id: {id} does not match with Category id: {category.Id}.");
        }

        _categoryService.Update(category);
        return NoContent();
    }

    /// <summary>
    /// Delete a category.
    /// </summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _categoryService.Delete(id);
        return NoContent();
    }
}

