using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;
using AppointmentsService.ServiceExtensions.Exceptions;

namespace AppointmentsService.Services.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ScheduleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ScheduleDto?> CreateScheduleAsync(ScheduleManipulationDto scheduleDto)
        {
            var schedule = _mapper.Map<Schedule>(scheduleDto);

            _repositoryManager.ScheduleRepository.CreateSchedule(schedule);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<ScheduleDto?>(schedule);
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await _repositoryManager.ScheduleRepository.GetScheduleById(id, trackChanges: false);
            if (schedule == null)
                throw new NotFoundException("schedule with id: " + id + " wasn't found");

            _repositoryManager.ScheduleRepository.DeleteSchedule(schedule);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync()
        {
            var schedules = await _repositoryManager.ScheduleRepository.GetAllSchedules(trackChanges: false);

            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllSchedulesByDoctorAndDateAsync(string doctorId, string date)
        {
           var schedules = await _repositoryManager.ScheduleRepository.GetAllSchedulesByDoctorAndDate(doctorId, date, trackChanges: false);

            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }

        public async Task<IEnumerable<ScheduleDto>> GetFreeSchedulesByDoctorAndDate(string doctorId, string date)
        {
            var schedules = await _repositoryManager.ScheduleRepository.GetFreeSchedulesByDoctorAndDate(doctorId, date, trackChanges: false);

            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }

        public async Task<ScheduleDto?> GetScheduleAsync(int id)
        {
            var schedule = await _repositoryManager.ScheduleRepository.GetScheduleById(id, trackChanges: false);
            if (schedule == null)
                throw new NotFoundException("Schedule with id: " + id + " wasn't found");

            return _mapper.Map<ScheduleDto?>(schedule);
        }

        public async Task<ScheduleDto?> GetScheduleByAppoitmentIdAsync(int appoitmentId)
        {
            var schedule = await _repositoryManager.ScheduleRepository.GetScheduleByAppoitmentId(appoitmentId, trackChanges: false);
            if (schedule == null)
                throw new NotFoundException("Schedule for appoitment with id: " + appoitmentId + " wasn't found");

            return _mapper.Map<ScheduleDto?>(schedule);
        }

        public async Task UpdateScheduleAsync(int id, ScheduleManipulationDto scheduleDto)
        {
            var schedule = await _repositoryManager.ScheduleRepository.GetScheduleById(id, trackChanges: false);
            if (schedule == null)
                throw new NotFoundException("Schedule with id: " + id + " wasn't found");

            _mapper.Map(scheduleDto, schedule);
            await _repositoryManager.SaveAsync();
        }
    }
}
