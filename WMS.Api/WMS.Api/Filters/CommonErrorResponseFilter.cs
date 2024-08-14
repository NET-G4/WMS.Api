using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WMS.Api.Filters;

public class CommonErrorResponseFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidded" });
        operation.Responses.TryAdd("406", new OpenApiResponse { Description = "Unsupported format" });
        operation.Responses.TryAdd("500", new OpenApiResponse { Description = "Internal Server Error" });
    }
}
