using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsService.Controllers
{
    [Route("/api/results")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ResultController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetResultById")]
        public async Task<IActionResult> GetResultById(string id)
        {
            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: false);
            if(result == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ResultDto>(result));
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetResultsByPatients(string patientId) 
        {
            var results = await _repositoryManager.ResultRepository.GetAllResultByPatient(patientId, trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<ResultDto>>(results));
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetResultsByDoctors(string doctorId)
        {
            var results = await _repositoryManager.ResultRepository.GetAllResultByDoctor(doctorId, trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<ResultDto>>(results));
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult([FromBody]ResultManipulationDto result) 
        {
            var resultEntity = _mapper.Map<Result>(result);

            resultEntity.Id = Guid.NewGuid().ToString();

            _repositoryManager.ResultRepository.CreateResult(resultEntity);
            await _repositoryManager.SaveAsync();

            var resultToReturn = _mapper.Map<ResultDto>(resultEntity);

            return CreatedAtRoute("GetResultById", new { id = resultToReturn.Id}, resultToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(string id)
        {
            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: false);
            if (result == null)
            {
                return NotFound();
            }

            _repositoryManager.ResultRepository.DeleteResult(result);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(string id, [FromBody]ResultManipulationDto resultDto)
        {
            var result = await _repositoryManager.ResultRepository.GetResultById(id, trackChanges: true);
            if(result == null) 
            {
                return NotFound();
            }

            _mapper.Map(resultDto, result);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
