using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;
using WMS.Domain.QueryParameters;
using WMS.Services.Common;
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
    public ActionResult<PaginatedList<CategoryDto>> Get([FromQuery] CategoryQueryParameters queryParameters)
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

    /// <summary>
    /// Returns Categories report in PDF format.
    /// </summary>
    /// <returns></returns>
    [HttpGet("download")]
    public async Task<IActionResult> GetFileAsync()
    {
        var categories = await _categoryService.GetAllAsync();

        PdfDocument document = new();
        PdfPage page = document.Pages.Add();
        PdfGrid grid = new();
        PdfGraphics graphics = page.Graphics;

        //Set the standard font.
        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
        //Draw the text.
        graphics.DrawString("Product Categories", font, PdfBrushes.Black, new PointF(150, 0));

        var data = ConvertCategoriesToDataTableObjects(categories);

        grid.DataSource = data;
        grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable5DarkAccent4);
        grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.ListTable2Accent4);
        grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.ListTable3);

        grid.Draw(page, new PointF(10, 50));

        var stream = new MemoryStream();
        document.Save(stream);
        stream.Position = 0;

        return File(stream, "application/pdf", "categories.pdf");
    }

    private static List<object> ConvertCategoriesToDataTableObjects(IEnumerable<CategoryDto> categories)
    {
        List<object> data = [];

        foreach(var category in categories)
        {
            data.Add(new { ID = category.Id, category.Name, category.Description });
        }

        return data;
    }

    private static DataTable GetCategoriesDataTable(IEnumerable<CategoryDto> categories)
    {
        DataTable table = new DataTable();
        table.TableName = "Categories";
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Description", typeof(string));

        foreach(var category in categories)
        {
            table.Rows.Add(category.Id, category.Name, category.Description);
        }

        return table;
    }
}

