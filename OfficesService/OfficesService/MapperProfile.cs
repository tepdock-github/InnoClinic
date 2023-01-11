using AutoMapper;
using OfficesService.Domain.DataTransferObjects;
using OfficesService.Domain.Models;

namespace OfficesService
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Office, OfficeDto>();
            CreateMap<OfficeForManipulationDto, Office>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoForManipulationDto, Photo>();
        }
    }
}
