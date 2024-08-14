using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WMS.Api.Filters;

public class IgnoreSchemaFilter : ISchemaFilter
{
    private readonly Type[] _typesToIgnore;

    public IgnoreSchemaFilter()
    {
        _typesToIgnore = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.Namespace != null && 
                x.Namespace.Contains("WMS.Api.Filters.SwaggerExamples"))
            .ToArray();
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (_typesToIgnore.Contains(context.Type))
        {
            // Remove the schema from the definitions
            var openApiDocument = context.SchemaRepository.Schemas;
            var schemaKey = openApiDocument.FirstOrDefault(s => s.Value == schema).Key;
            if (schemaKey != null)
            {
                openApiDocument.Remove(schemaKey);
            }
        }
    }
}
