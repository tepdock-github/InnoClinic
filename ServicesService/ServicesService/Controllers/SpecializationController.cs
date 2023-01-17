using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/specializations")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SpecializationController(IServicesManager servicesManager, IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _servicesManager = servicesManager;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var specializations = await _repositoryManager.SpecializationRepository.GetAllSpecializationsAsync(trackChanges: false);

            return Ok(_mapper.Map<IEnumerable<SpecializationDto>>(specializations));
        }

        [HttpGet("{id}", Name = "GetSpecializationById")]
        public async Task<IActionResult> GetSpecialization(int id)
        {
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(id, trackChanges: false);

            return Ok(_mapper.Map<SpecializationDto>(specialization));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateSpecialization([FromBody]SpecializationManipulationDto specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);

            var specToReturn = await _servicesManager.SpecializationServices.CreateSpecialization(specialization);
            return CreatedAtRoute("GetSpecializationById", new { id = specToReturn.Id }, specToReturn);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditSpecialization(int id, 
            [FromBody]SpecializationManipulationDto specializationDto) 
        {
            var success = await _servicesManager.SpecializationServices.EditSpecialization(id, specializationDto);
            if (success == false)
                return NotFound();

            return NoContent();
        }
    }
}
