using AppointmentsService.ServiceExtensions.Exceptions;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;

namespace AppointmentsService.Services.Implementation
{
    public class AppoitmentService : IAppoitmentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AppoitmentService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<AppoitmentDto> CreateAppoitment(AppoitmentManipulationDto appoitmentDto)
        {
            var appoitment = _mapper.Map<Appoitment>(appoitmentDto);

            _repositoryManager.AppoitmentRepository.CreateAppoitment(appoitment);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<AppoitmentDto>(appoitment);
        }

        public async Task<AppoitmentDto?> GetAppoitmentById(int id)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(id, trackChanges: false);
            if (appoitment == null)
                throw new NotFoundException("appoitment with id:" + id + " wasnt found");

            return _mapper.Map<AppoitmentDto>(appoitment);
        }

        public async Task<IEnumerable<AppoitmentDto>> GetAppoitments()
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAllAppoitments(trackChanges: false);

            return _mapper.Map<IEnumerable<AppoitmentDto>>(appoitments);
        }

        public async Task<IEnumerable<AppoitmentDto>> GetDoctorHistory(int doctorId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsHistoryByDoctor(doctorId, trackChanges: false);

            return _mapper.Map<IEnumerable<AppoitmentDto>>(appoitments);
        }

        public async Task<IEnumerable<AppoitmentDto>> GetDoctorSchedule(int doctorId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsScheduleByDocrot(doctorId, trackChanges: false);

            return _mapper.Map<IEnumerable<AppoitmentDto>>(appoitments);
        }

        public async Task<IEnumerable<AppoitmentDto>> GetPatientAppoitments(int patientId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsByPatient(patientId, trackChanges: false);

            return _mapper.Map<IEnumerable<AppoitmentDto>>(appoitments);
        }

        public async Task<IEnumerable<AppoitmentDto>> GetPatientHistory(int patientId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsHistoryByPatient(patientId, trackChanges: false);

            return _mapper.Map<IEnumerable<AppoitmentDto>>(appoitments);
        }

        public async Task UpdateAppoitment(int id, AppoitmentManipulationDto appoitmentDto)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(id, trackChanges: true);
            if (appoitment == null)
                throw new NotFoundException("appoitment with id:" + id + " wasnt found");

            _mapper.Map(appoitmentDto, appoitment);
            await _repositoryManager.SaveAsync();
        }
    }
}
