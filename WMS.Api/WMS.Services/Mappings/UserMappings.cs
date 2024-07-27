using AutoMapper;
using WMS.Domain.Entities.Identity;
using WMS.Services.DTOs.User;

namespace WMS.Services.Mappings
{
    internal class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(x => x.Id, x => x.MapFrom(_ => Guid.NewGuid()))
                .ForMember(x => x.UserName, x => x.MapFrom(e => e.Email));
        }
    }
}
