select * from product;
select * from category;

select count(p.Id), c.Name
from product p
inner join category c on p.categoryId = c.id
group by c.Name;

select s.Id, count(si.Id)
from sale s
inner join saleitem si on s.Id = si.SaleId
group by s.Id;

select * from salesByCategory;

select c.Id, c.Name as 'Category', count(si.Id) as 'Count'
from category c
inner join product p on c.Id = p.CategoryId
inner join SaleItem si on si.ProductId = p.Id
inner join sale s on si.SaleId = s.Id
where MONTH(s.Date) = MONTH(getdate()) AND YEAR(s.Date) = YEAR(getdate())
group by c.Id, c.Name;

select count(si.id)
from saleItem si
inner join sale s on s.Id = si.SaleId
where (si.productId in (select p.id 
					from product p
					inner join category c on p.CategoryId = c.Id
					where c.Name = 'Automotive')) and
	Month(s.Date) = Month(getdate());

select * from saleitem


SELECT [c].[Name] AS [Category], COUNT(*) AS [SalesCount]
FROM [Category] AS [c]
INNER JOIN [Product] AS [p] ON [c].[Id] = [p].[CategoryId]
INNER JOIN [SaleItem] AS [s] ON [p].[Id] = [s].[ProductId]
INNER JOIN [Sale] AS [s0] ON [s].[SaleId] = [s0].[Id]
WHERE DATEPART(month, [s0].[Date]) = DATEPART(month, GETDATE()) AND DATEPART(year, [s0].[Date]) = DATEPART(year, GETDATE())
GROUP BY [c].[Id], [c].[Name];

select c.Id, c.Name as 'Category', count(si.Id) as 'Count'
from category c
inner join product p on c.Id = p.CategoryId
inner join SaleItem si on si.ProductId = p.Id
inner join sale s on si.SaleId = s.Id
where MONTH(s.Date) = MONTH(getdate()) AND YEAR(s.Date) = YEAR(getdate())
group by c.Id, c.Name;

select *
from sale s0
WHERE DATEPART(month, [s0].[Date]) = DATEPART(month, GETDATE()) AND DATEPART(year, [s0].[Date]) = DATEPART(year, GETDATE());

select * 
from sale s
where MONTH(s.Date) = MONTH(getdate()) AND YEAR(s.Date) = YEAR(getdate())