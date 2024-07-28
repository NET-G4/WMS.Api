using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WMS.Domain.Entities.Identity;
using WMS.Infrastructure.Configurations;
using WMS.Infrastructure.Persistence;
using WMS.Infrastructure.Persistence.Migrations;
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
        AddIdentity(services);
        AddAuthentication(services, configuration);
        AddJwtHandler(services);
        AddOptions(services, configuration);

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

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("Jwt");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions["ValidIssuer"],
                    ValidAudience = jwtOptions["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions["SecretKey"]))
                };
            });
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 7;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
            .AddEntityFrameworkStores<WmsDbContext>();
    }

    private static void AddJwtHandler(IServiceCollection services)
    {
        services.AddSingleton<JwtHandler>();
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
