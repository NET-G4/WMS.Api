using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SalesByCategory_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE OR ALTER VIEW dbo.SalesByCategory AS 
                select c.Id, c.Name as 'Category', count(si.Id) as 'Count'
                from category c
                inner join product p on c.Id = p.CategoryId
                inner join SaleItem si on si.ProductId = p.Id
                inner join sale s on si.SaleId = s.Id
                where MONTH(s.Date) = MONTH(getdate()) AND YEAR(s.Date) = YEAR(getdate())
                group by c.Id, c.Name;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.SalesByCategory");
        }
    }
}
