using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Supplier;
using WMS.Services.DTOs.Supply;

namespace WMS.Services.Mappings
{
    public class SupplyMappings : Profile
    {
        public SupplyMappings()
        {
            CreateMap<Supply, SupplyDto>()
            .ForMember(x => x.Supplier, r => r.MapFrom(e => e.Supplier.FirstName +" "+ e.Supplier.LastName));
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
      
    }
}
