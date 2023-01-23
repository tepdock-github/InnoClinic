using AutoMapper;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Extensions
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<DoctorsProfile, DoctorProfileDto>();
            CreateMap<DoctorProfileManipulationDto, DoctorsProfile>();

            CreateMap<PatientProfile, PatientProfileDto>();
            CreateMap<PatientProfileManipulationDto, PatientProfile>();

            CreateMap<ReceptionistProfile, ReceptionistProfileDto>();
            CreateMap<ReceptionistProfileManipulationDto, ReceptionistProfile>();
        }
    }
}
