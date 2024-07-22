using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Infrastructure.Persistence;
using WMS.Services.DTOs.Sale;
using WMS.Services.DTOs.Supply;
using WMS.Services.Interfaces;

namespace WMS.Services
{
    public class SupplyService(WmsDbContext context, IMapper mapper) : ISupplyService
    {
        private readonly WmsDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));

        public SupplyDto Create(SupplyForCreateDto supplyForCreate)
        {
            var entity = _mapper.Map<Supply>(supplyForCreate);
            entity.TotalDue = supplyForCreate.SupplyItems.Sum(x => x.Quantity * x.UnitPrice);

            foreach (var item in entity.SupplyItems)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);

                if (product is null)
                {
                    throw new InvalidOperationException($"Cannot create supply for non-existing product: {item.ProductId}.");
                }
                product.QuantityInStock += item.Quantity;
            }

            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == supplyForCreate.SupplierId);

            if (supplier is null)
            {
                throw new InvalidOperationException($"Cannot create supply for supplier which does not exist. Supplier id: {supplyForCreate.SupplierId}");
            }

            supplier.Balance += entity.TotalPaid - entity.TotalDue;
            var createdSale = _context.Supplies.Add(entity).Entity;
            _context.SaveChanges();

            return _mapper.Map<SupplyDto>(createdSale);
        }

        public void Delete(int id)
        {
            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);

            if (supply is null)
            {
                throw new EntityNotFoundException($"Supply with id: {id} does not exist.");
            }

            _context.Supplies.Remove(supply);
            _context.SaveChanges();
        }

        public async Task<List<SupplyDto>> GetSupplies()
        {
            var supplies = await _context
                .Supplies
                .Include(x => x.Supplier)
                .Include(x => x.SupplyItems)
                .ThenInclude(y => y.Product)
                .AsSplitQuery()
                .ProjectTo<SupplyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return supplies;
        }

        public SupplyDto GetSupplyById(int id)
        {
            var supply = _context
                .Supplies
                .AsNoTracking()
                .Include(x => x.Supplier)
                .Include(x => x.SupplyItems)
                .ThenInclude(x => x.Product)
                .AsSplitQuery()
                .FirstOrDefault(x => x.Id == id);

            if (supply is null)
            {
                throw new EntityNotFoundException($"Supply with id: {id} does not exist.");
            }

            return _mapper.Map<SupplyDto>(supply);
        }

        public void Update(SupplyForUpdateDto supplyForUpdate)
        {
            if (!_context.Supplies.Any(x => x.Id == supplyForUpdate.Id))
            {
                throw new EntityNotFoundException($"Supply with id: {supplyForUpdate.Id} does not exist.");
            }

            var entity = _mapper.Map<Supply>(supplyForUpdate);
            _context.Supplies.Update(entity);
            _context.SaveChanges();
        }
    }
}
