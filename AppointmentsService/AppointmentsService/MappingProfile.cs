using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using AutoMapper;

namespace AppointmentsService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Result, ResultDto>();
            CreateMap<ResultManipulationDto, Result>();

            CreateMap<Appoitment, AppoitmentDto>();
            CreateMap<AppoitmentManipulationDto, Appoitment>();

            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleManipulationDto, Schedule>();
        }
    }
}
