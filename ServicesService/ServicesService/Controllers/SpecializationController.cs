using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/specializations")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationServices _servicesManager;
        private readonly IMapper _mapper;

        public SpecializationController(ISpecializationServices servicesManager,
            IMapper mapper)
        {
            _servicesManager = servicesManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var specializations = await _servicesManager.GetAllSpecializations();

            return Ok(_mapper.Map<IEnumerable<SpecializationDto>>(specializations));
        }

        [HttpGet("{id}", Name = "GetSpecializationById")]
        public async Task<IActionResult> GetSpecialization(int id)
        {
            var specialization = await _servicesManager.GetSpecializationById(id);

            return Ok(_mapper.Map<SpecializationDto>(specialization));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationManipulationDto specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);

            var specToReturn = await _servicesManager.CreateSpecialization(specialization);
            return CreatedAtRoute("GetSpecializationById", new { id = specToReturn.Id }, specToReturn);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditSpecialization(int id,
            [FromBody] SpecializationManipulationDto specializationDto)
        {
            await _servicesManager.EditSpecialization(id, specializationDto);

            return NoContent();
        }
    }
}
