using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Exceptions;
using WMS.Infrastructure.Persistence;
using WMS.Services.DTOs.Customer;
using WMS.Services.DTOs.Supplier;
using WMS.Services.Interfaces;

namespace WMS.Services
{
    public class SupplierService(IMapper mapper, WmsDbContext context) : ISupplierService
    {
        private readonly IMapper _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        private readonly WmsDbContext _context = context ??
            throw new ArgumentNullException(nameof(context)); 
        public SupplierDto Create(SupplierForCreateDto supplier)
        {
            var entity = _mapper.Map<Supplier>(supplier);

            var createdEntity = _context.Suppliers.Add(entity).Entity;
            _context.SaveChanges();

            return _mapper.Map<SupplierDto>(createdEntity);
        }

        public void Delete(int id)
        {
            var entity = _context.Suppliers.FirstOrDefault(x => x.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Supplier with id: {id} does not exist.");
            }

            _context.Suppliers.Remove(entity);
            _context.SaveChanges();
        }

        public SupplierDto GetSupplierById(int id)
        {
            var entity = _context.Suppliers.FirstOrDefault(x => x.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Supplier with id: {id} does not exist.");
            }

            return _mapper.Map<SupplierDto>(entity);
        }

        public List<SupplierDto> GetSuppliers()
        {
            var entities = _context.Suppliers.ToList();

            return _mapper.Map<List<SupplierDto>>(entities);
        }

        public void Update(SupplierForUpdateDto supplier)
        {
            if (!_context.Suppliers.Any(x => x.Id == supplier.Id))
            {
                throw new EntityNotFoundException($"Supplier with id: {supplier.Id} does not exist.");
            }

            var entity = _mapper.Map<Supplier>(supplier);

            _context.Suppliers.Update(entity);
            _context.SaveChanges();
        }
    }
}
