using AppointmentsService.Filters;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsService.Controllers
{
    [Route("/api/results")]
    [ApiController]
    [Authorize]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        /// <summary>
        ///     Get Result By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetResultById")]
        public async Task<IActionResult> GetResultById(int id) =>
            Ok(await _resultService.GetResultById(id));

        /// <summary>
        ///     Get all Patient's Results
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetResultsByPatients(int patientId) =>
            Ok(await _resultService.GetResultsByPatient(patientId));

        /// <summary>
        ///     Get all Results made by the Doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        [HttpGet("doctor/{doctorId}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetResultsByDoctors(int doctorId) =>
            Ok(await _resultService.GetResultsByDoctor(doctorId));

        /// <summary>
        ///     Create new Result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> CreateResult([FromBody] ResultManipulationDto result)
        {

            var resultToReturn = await _resultService.CreateResult(result);

            return CreatedAtRoute("GetResultById", new { id = resultToReturn.Id }, resultToReturn);
        }

        /// <summary>
        ///     Delete Existing Result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            await _resultService.DeleteResult(id);

            return NoContent();
        }

        /// <summary>
        ///     Update Existing Result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resultDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> UpdateResult(int id, [FromBody] ResultManipulationDto resultDto)
        {
            await _resultService.UpdateResult(id, resultDto);

            return NoContent();
        }
    }
}
