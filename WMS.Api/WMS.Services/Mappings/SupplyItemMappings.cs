using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Product;
using WMS.Services.DTOs.SupplyItem;

namespace WMS.Services.Mappings
{
    public class SupplyItemMappings : Profile
    {
        public SupplyItemMappings()
        {
            CreateMap<SupplyItem, SupplyItemDto>()
                .ForMember(dto => dto.Product, e => e.MapFrom(r => r.Product.Name));
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
