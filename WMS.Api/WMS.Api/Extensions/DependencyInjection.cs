using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WMS.Infrastructure.Persistence;
using WMS.Services;
using WMS.Services.Interfaces;
using WMS.Services.Mappings;

namespace WMS.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services);
        AddInfrastructure(services, configuration);
        AddControllers(services);
        AddSwagger(services);

        return services;    
    }

    private static void AddControllers(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        }).AddXmlDataContractSerializerFormatters();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setup.IncludeXmlComments(fullPath);
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ISupplyService, SupplyService>();
        services.AddScoped<IDashboardService, DashboardService>();

        services.AddAutoMapper(typeof(CategoryMappings).Assembly);
    }

    private static void AddInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WmsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddAutoMapper(typeof(CategoryMappings).Assembly);
    }
}
