﻿using AutoMapper;
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
        private readonly IServiceServices _servicesManager;
        private readonly IMapper _mapper;

        public ServiceController(IServiceServices servicesManager, IMapper mapper)
        {
            _servicesManager = servicesManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _servicesManager.GetServices();

            return Ok(_mapper.Map<IEnumerable<ServicesManipulationDto>>(services));
        }

        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _servicesManager.GetServiceById(id);

            return Ok(_mapper.Map<ServiceDto>(service));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreaeteService([FromBody] ServicesManipulationDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            var serviceToReturn = await _servicesManager.CreateService(service);

            return CreatedAtRoute("GetServiceById", new { id = serviceToReturn.Id }, serviceToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditService(int id, [FromBody] ServicesManipulationDto service)
        {
            await _servicesManager.EditService(id, service);

            return NoContent();
        }
    }
}