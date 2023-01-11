using Authorization.Domain.DataTransferObject;
using Authorization.Domain.Models;
using AutoMapper;

namespace AuthorizationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterAccountDto, Account>()
                .ForMember(a => a.UserName, 
                ad => ad.MapFrom(x => x.Email));
            CreateMap<LoginAccountDto, Account>()
                .ForMember(a => a.UserName,
                ad => ad.MapFrom(x => x.Email));
        }
    }
}
