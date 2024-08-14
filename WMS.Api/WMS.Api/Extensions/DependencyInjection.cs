using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Syncfusion.Licensing;
using System.Reflection;
using System.Text;
using WMS.Api.Filters;
using WMS.Domain.Entities.Identity;
using WMS.Infrastructure;
using WMS.Infrastructure.Configurations;
using WMS.Infrastructure.Email;
using WMS.Infrastructure.Persistence;
using WMS.Services;
using WMS.Services.Interfaces;
using WMS.Services.Mappings;

namespace WMS.Api.Extensions;

internal static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructureServices(configuration);
        AddServices(services);
        AddSwagger(services);
        AddIdentity(services);
        AddAuthentication(services, configuration);
		AddSyncfusion(configuration);

        services
            .AddControllers(options => options.ReturnHttpNotAcceptable = true)
            .AddXmlDataContractSerializerFormatters();
        services.AddAutoMapper(typeof(CategoryMappings).Assembly);
        services.AddHttpContextAccessor();

        return services;    
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Warehouse Management API",
                Version = "v1",
                Description = "Central API for Warehouse Management System.",
                Contact = new OpenApiContact
                {
                    Name = "WMS",
                    Email = "support@wms.uz",
                    Url = new Uri("https://wms.uz")
                }
            });

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setup.IncludeXmlComments(fullPath);

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            setup.AddSecurityDefinition("Bearer", jwtSecurityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            };

            setup.AddSecurityRequirement(securityRequirement);

            setup.OperationFilter<CommonErrorResponseFilter>();

            // setup.SchemaFilter<IgnoreSchemaFilter>();
        });

        services.AddSwaggerExamplesFromAssemblyOf<Program>();
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
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddSingleton<JwtHandler>();
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

        services.AddAuthorization();
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
            .AddEntityFrameworkStores<WmsDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(12);
        });
    }

    private static void AddSyncfusion(IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("Keys:Syncfusion");

        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("Cannot register Syncfusion without Key.");
        }

        SyncfusionLicenseProvider.RegisterLicense(key);
    }
}
