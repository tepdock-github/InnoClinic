using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceServices _servicesManager;

        public ServiceController(IServiceServices servicesManager)
        {
            _servicesManager = servicesManager;
        }

        /// <summary>
        /// get all services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllServices() =>
            Ok(await _servicesManager.GetServices());

        /// <summary>
        /// Get Service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<IActionResult> GetService(int id) =>
            Ok(await _servicesManager.GetServiceById(id));

        /// <summary>
        /// Add new service
        /// </summary>
        /// <param name="serviceDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreaeteService([FromBody] ServicesManipulationDto serviceDto)
        {
            var serviceToReturn = await _servicesManager.CreateService(serviceDto);

            return CreatedAtRoute("GetServiceById", new { id = serviceToReturn?.Id }, serviceToReturn);
        }

        /// <summary>
        /// Edit chosen by id service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditService(int id, [FromBody] ServicesManipulationDto service)
        {
            await _servicesManager.EditService(id, service);

            return NoContent();
        }
    }
}
