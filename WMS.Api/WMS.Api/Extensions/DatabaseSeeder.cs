using Bogus;
using WMS.Domain.Entities;
using WMS.Infrastructure.Persistence;

namespace WMS.Api.Extensions
{
    public class DatabaseSeeder
    {
        private static Faker _faker = new();

        public static void SeedDatabase(WmsDbContext context)
        {
            try
            {
                AddCategories(context);
                AddProducts(context);
                AddCustomers(context);
                AddSales(context);
                AddSuppliers(context);
                AddSupplies(context);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static void AddCategories(WmsDbContext context)
        {
            if (context.Categories.Any()) return;

            var categories = _faker
                .Commerce
                .Categories(20)
                .Distinct()
                .Select(categoryName =>
                {
                    return new Category
                    {
                        Name = categoryName,
                        Description = _faker.Lorem.Sentence()
                    };
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void AddProducts(WmsDbContext context)
        {
            // if (context.Products.Any()) return;

            var categoryIds = context.Categories.Select(x => x.Id).ToArray();
            var products = Enumerable.Range(0, 50)
                .Select(productName =>
                {
                    var supplyPrice = _faker.Random.Decimal(10_000, 500_000);
                    var salePrice = _faker.Random.Decimal(supplyPrice, 650_000);
                    return new Product
                    {
                        Name = _faker.Commerce.ProductName(),
                        Description = _faker.Commerce.ProductDescription(),
                        LowQuantityAmount = _faker.Random.Int(5, 20),
                        QuantityInStock = _faker.Random.Int(15, 100),
                        CategoryId = _faker.PickRandom(categoryIds),
                        SupplyPrice = supplyPrice,
                        SalePrice = salePrice,
                    };
                });

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void AddCustomers(WmsDbContext context)
        {
            if (context.Customers.Any()) return;

            for(int i = 0; i < 25; i++)
            {
                var customer = new Customer
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    Address = _faker.Address.StreetAddress(),
                    Balance = _faker.Random.Decimal(-100_000, 100_000),
                    Discount = _faker.Random.Decimal(0, 99),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-##-###-##-##")
                };

                context.Customers.Add(customer);
            }

            context.SaveChanges();
        }

        private static void AddSales(WmsDbContext context)
        {
            if (context.SaleItems.Any()) return;

            var customerIds = context.Customers.Select(x => x.Id).ToArray();
            var products = context.Products.ToArray();

            foreach ( var customer in customerIds)
            {
                var numberOfSales = _faker.Random.Int(1, 15);

                for(int i = 0; i <  numberOfSales; i++)
                {
                    var saleItems = GetRandomSaleItems(products);
                    var totalDue = saleItems.Sum(x => x.Quantity * x.UnitPrice);
                    var totalPaid = _faker.Random.Decimal(0, totalDue);

                    var sale = new Sale
                    {
                        Date = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                        CustomerId = customer,
                        TotalDue = totalDue,
                        TotalPaid = totalPaid,
                        SaleItems = saleItems,
                    };

                    context.Sales.Add(sale);
                }
            }

            context.SaveChanges();
        }

        private static void AddSuppliers(WmsDbContext context)
        {
            if (context.Suppliers.Any()) return;

            for (int i = 0; i < 25; i++)
            {
                var supplier = new Supplier
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    Balance = _faker.Random.Decimal(-100_000, 100_000),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-##-###-##-##")
                };

                context.Suppliers.Add(supplier);
            }

            context.SaveChanges();
        }

        private static void AddSupplies(WmsDbContext context)
        {
            if (context.SupplyItems.Any()) return;

            var supplierIds = context.Suppliers.Select(x => x.Id).ToArray();
            var products = context.Products.ToArray();

            foreach (var supplier in supplierIds)
            {
                var numberOfSupplies = _faker.Random.Int(1, 15);

                for (int i = 0; i < numberOfSupplies; i++)
                {
                    var supplyItems = GetRandomSupplyItems(products);
                    var totalDue = supplyItems.Sum(x => x.Quantity * x.UnitPrice);
                    var totalPaid = _faker.Random.Decimal(0, totalDue);

                    var supply = new Supply
                    {
                        Date = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                        SupplierId = supplier,
                        TotalDue = totalDue,
                        TotalPaid = totalPaid,
                        SupplyItems = supplyItems,
                    };

                    context.Supplies.Add(supply);
                }
            }

            context.SaveChanges();
        }

        private static SaleItem[] GetRandomSaleItems(Product[] products)
        {
            return Enumerable.Range(2, _faker.Random.Int(2, 10))
                .Select(x =>
                {
                    var randomProduct = _faker.PickRandom(products);

                    return new SaleItem
                    {
                        ProductId = randomProduct.Id,
                        Quantity = _faker.Random.Int(1, 15),
                        UnitPrice = randomProduct.SalePrice
                    };
                })
                .ToArray();
        }

        private static SupplyItem[] GetRandomSupplyItems(Product[] products)
        {
            return Enumerable.Range(2, _faker.Random.Int(2, 10))
                .Select(x =>
                {
                    var randomProduct = _faker.PickRandom(products);

                    return new SupplyItem
                    {
                        ProductId = randomProduct.Id,
                        Quantity = _faker.Random.Int(1, 15),
                        UnitPrice = randomProduct.SalePrice
                    };
                })
                .ToArray();
        }
    }
}
