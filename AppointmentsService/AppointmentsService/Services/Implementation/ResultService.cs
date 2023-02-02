using AppointmentsService.ServiceExtensions.Exceptions;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;

namespace AppointmentsService.Services.Implementation
{
    public class ResultService : IResultService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ResultService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ResultDto> CreateResult(ResultManipulationDto resultDto)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(resultDto.AppoitmentId, trackChanges: false);
            if(appoitment == null)
                throw new NotFoundException("appoitment with id: " + resultDto.AppoitmentId + " wasnt found");

            var result = _mapper.Map<Result>(resultDto);

            _repositoryManager.ResultRepository.CreateResult(result);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<ResultDto>(result);
        }

        public async Task DeleteResult(int id)
        {
            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: false);
            if (result == null)
                throw new NotFoundException("result with id: " + id + " wasnt found");

            _repositoryManager.ResultRepository.DeleteResult(result);
            await _repositoryManager.SaveAsync();
        }

        public async Task<ResultDto?> GetResultById(int id)
        {
            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: false);
            if (result == null)
                throw new NotFoundException("result with id: " + id + " wasnt found");

            return _mapper.Map<ResultDto>(result);
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByDoctor(int doctorId)
        {
            var results = await _repositoryManager.ResultRepository.GetAllResultByDoctor(doctorId, trackChanges: false);

            return _mapper.Map<IEnumerable<ResultDto>>(results);
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByPatient(int patientId)
        {
            var results = await _repositoryManager.ResultRepository.GetAllResultByPatient(patientId, trackChanges: false);

            return _mapper.Map<IEnumerable<ResultDto>>(results);
        }

        public async Task UpdateResult(int id, ResultManipulationDto resultDto)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(resultDto.AppoitmentId, trackChanges: false);
            if (appoitment == null)
                throw new NotFoundException("appoitment with id: " + resultDto.AppoitmentId + " wasnt found");

            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: false);
            if (result == null)
                throw new NotFoundException("result with id: " + id + " wasnt found");

            _mapper.Map(resultDto, result);
            await _repositoryManager.SaveAsync();
        }
    }
}
