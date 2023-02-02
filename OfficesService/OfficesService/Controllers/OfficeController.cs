using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficesService.Data.Repositories;
using OfficesService.Domain.DataTransferObjects;
using OfficesService.Domain.Interfaces;
using OfficesService.Domain.Models;
using OfficesService.Filters;
using OfficesService.Services.Implementation;
using OfficesService.Services.Interfaces;

namespace OfficesService.Controllers
{
    [Route("api/offices")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }


        /// <summary>
        ///     Get All Officies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOffices()
        {
            return Ok(await _officeService.GetOffices());
        }

        /// <summary>
        ///     Get Office by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetOfficeById")]
        public async Task<IActionResult> GetOfficeById(string id)
        {
            return Ok(await _officeService.GetOfficeById(id));
        }

        /// <summary>
        ///     Create new Office
        /// </summary>
        /// <param name="officeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateOffice([FromForm] OfficeForManipulationDto officeDto)
        {
            var office = await _officeService.AddOffice(officeDto);

            return CreatedAtRoute("GetOfficeById", new { id = office.Id }, office);
        }

        /// <summary>
        ///     Update Office By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="officeDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> UpdateOffice(string id,
            [FromBody] OfficeForManipulationDto officeDto)
        {
            await _officeService.UpdateOffice(id, officeDto);

            return NoContent();
        }
    }
}
