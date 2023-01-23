using Appoitments.Domain.DataTransferObjects;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsService.Controllers
{
    [Route("/api/appoitments")]
    [ApiController]
    public class AppoitmentController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AppoitmentController(IRepositoryManager repository, IMapper mapper)
        {
            _repositoryManager = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppoitments()
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAllAppoitments(trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<AppoitmentDto>>(appoitments));
        }

        [HttpGet("{id}", Name = "GetAppoitmentById")]
        public async Task<IActionResult> GetAppoitmentById(string id)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(id, trackChanges: false);
            if(appoitment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AppoitmentDto>(appoitment));
        }

        [HttpGet("doctor-history/{doctorId}")]
        public async Task<IActionResult> GetDoctorHistory(string doctorId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitHistoryByDoctor(doctorId, trackChanges: false);

            return Ok(_mapper.Map< IEnumerable<AppoitmentDto>>(appoitments));
        }

        [HttpGet("patient-history/{patientId}")]
        public async Task<IActionResult> GetPatientHistory(string patientId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitHistoryByPatient(patientId, trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<AppoitmentDto>>(appoitments));
        }

        [HttpGet("doctor-schedule/{doctorId}")]
        public async Task<IActionResult> GetDoctorSchedule(string doctorId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsScheduleByDocrot(doctorId, trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<AppoitmentDto>>(appoitments));
        }

        [HttpGet("patient-schedule/{patientId}")]
        public async Task<IActionResult> GetPatientAppoitments(string patientId)
        {
            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsByPatient(patientId, trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<AppoitmentDto>>(appoitments));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppoitment([FromBody]AppoitmentManipulationDto appoitmentDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var appoitmentEntity = _mapper.Map<Appoitment>(appoitmentDto);

            appoitmentEntity.Id = Guid.NewGuid().ToString();

            _repositoryManager.AppoitmentRepository.CreateAppoitment(appoitmentEntity);
            await _repositoryManager.SaveAsync();

            var appoitmentToReturn = _mapper.Map<AppoitmentDto>(appoitmentEntity);

            return CreatedAtRoute("GetAppoitmentById", new { id = appoitmentToReturn.Id }, appoitmentToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppoitment(string id, [FromBody]AppoitmentManipulationDto appoitmentDto)
        {
            var appoitment = await _repositoryManager.AppoitmentRepository.GetAppoitmentId(id, trackChanges: true);
            if (appoitment == null)
            {
                return NotFound();
            }

            _mapper.Map(appoitmentDto, appoitment);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
