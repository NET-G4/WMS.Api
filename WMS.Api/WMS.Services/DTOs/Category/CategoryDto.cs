namespace WMS.Services.DTOs.Category;

public class CategoryDto
{
    /// <summary>
    /// The unique identifier for the category.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Description of the category.
    /// </summary>
    public string? Description { get; init; }
}
