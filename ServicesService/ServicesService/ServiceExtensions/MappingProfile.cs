using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;

namespace ServicesService.ServiceExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryManipulationDto, Category>();

            CreateMap<Service, ServiceDto>();
            CreateMap<ServicesManipulationDto, Service>();

            CreateMap<Specialization, SpecializationDto>();
            CreateMap<SpecializationManipulationDto, Specialization>();
        }
    }
}
