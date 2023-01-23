using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        private readonly IMapper _mapper;

        public ServiceController(IServicesManager servicesManager, IMapper mapper)
        {
            _servicesManager = servicesManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _servicesManager.ServiceServices.GetServices();

            return Ok(_mapper.Map<IEnumerable<ServicesManipulationDto>>(services));
        }

        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _servicesManager.ServiceServices.GetServiceById(id);
            if (service == null)
                return NotFound();

            return Ok(_mapper.Map<ServiceDto>(service));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreaeteService([FromBody] ServicesManipulationDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            var serviceToReturn = await _servicesManager.ServiceServices.CreateService(service);

            if (serviceToReturn == null)
                return NotFound();
            return CreatedAtRoute("GetServiceById", new { id = serviceToReturn.Id }, serviceToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditService(int id, [FromBody] ServicesManipulationDto service)
        {
            var success = await _servicesManager.ServiceServices.EditService(id, service);
            if (success == false)
                return NotFound();

            return NoContent();
        }
    }
}
