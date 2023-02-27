using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/specializations")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationServices _servicesManager;

        public SpecializationController(ISpecializationServices servicesManager)
        {
            _servicesManager = servicesManager;
        }

        /// <summary>
        /// Get all specializations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSpecializations() =>
            Ok(await _servicesManager.GetAllSpecializations());

        /// <summary>
        /// Get specialization by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSpecializationById")]
        public async Task<IActionResult> GetSpecialization(int id) =>
            Ok(await _servicesManager.GetSpecializationById(id));

        /// <summary>
        /// Add new specialization
        /// </summary>
        /// <param name="specializationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationManipulationDto specializationDto)
        {
            var specToReturn = await _servicesManager.CreateSpecialization(specializationDto);

            return CreatedAtRoute("GetSpecializationById", new { id = specToReturn.Id }, specToReturn);
        }

        /// <summary>
        /// Edit chosen by id specialization
        /// </summary>
        /// <param name="id"></param>
        /// <param name="specializationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditSpecialization(int id,
            [FromBody] SpecializationManipulationDto specializationDto)
        {
            await _servicesManager.EditSpecialization(id, specializationDto);

            return NoContent();
        }
    }
}
