using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Customer;
using WMS.Services.DTOs.Supplier;

namespace WMS.Services.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings() 
        {
            CreateMap<Supplier, SupplierDto>()
            .ForMember(x => x.FullName, r => r.MapFrom(e => GetFullName(e)));
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
        private static string GetFullName(Supplier supplier)
        {
            if (supplier.LastName == null)
            {
                return supplier.FirstName;
            }

            return supplier.FirstName + " " + supplier.LastName;
        }
    }
}
