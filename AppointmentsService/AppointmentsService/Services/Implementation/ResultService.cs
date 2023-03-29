using AppointmentsService.ServiceExtensions.Exceptions;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;
using MassTransit;
using SharedModelsInnoClinic;

namespace AppointmentsService.Services.Implementation
{
    public class ResultService : IResultService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ResultService(IRepositoryManager repositoryManager, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ResultDto> CreateResult(ResultManipulationDto resultDto)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(resultDto.AppoitmentId, trackChanges: false);
            if(appoitment == null)
                throw new NotFoundException("appoitment with id: " + resultDto.AppoitmentId + " wasnt found");

            var result = _mapper.Map<Result>(resultDto);

            _repositoryManager.ResultRepository.CreateResult(result);
            await _repositoryManager.SaveAsync();

            await _publishEndpoint.Publish<IResultManipulation>(new
            {
                resultDto.Complaints,
                resultDto.Conclusion,
                resultDto.Recomendations,
                resultDto.Diagnosis,
                resultDto.AppoitmentId
            });

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

            await _publishEndpoint.Publish<IResultManipulation>(new
            {
                resultDto.Complaints,
                resultDto.Conclusion,
                resultDto.Recomendations,
                resultDto.Diagnosis,
                resultDto.AppoitmentId
            });

            _mapper.Map(resultDto, result);
            await _repositoryManager.SaveAsync();
        }
    }
}
