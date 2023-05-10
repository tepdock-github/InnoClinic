using AppointmentsService.Filters;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsService.Controllers
{
    [Route("/api/schedules")]
    [ApiController]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetSchedules() =>
            Ok(await _scheduleService.GetAllSchedulesAsync());

        [HttpGet("doctor/{doctorId}/date/{date}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetAllSchedulesByDoctorAndDate(string doctorId, string date) =>
            Ok(await _scheduleService.GetAllSchedulesByDoctorAndDateAsync(doctorId, date));

        [HttpGet("free/doctor/{doctorId}/date/{date}")]
        public async Task<IActionResult> GetFreeSchedules(string doctorId, string date) =>
            Ok(await _scheduleService.GetFreeSchedulesByDoctorAndDate(doctorId, date));

        [HttpGet("{id}", Name = "Get schedule by id")]
        public async Task<IActionResult> GetSchedule(int id) =>
            Ok(await _scheduleService.GetScheduleAsync(id));

        [HttpGet("appoitment/{id}")]
        public async Task<IActionResult> GetScheduleByAppoitment(int id) =>
            Ok(await _scheduleService.GetScheduleByAppoitmentIdAsync(id));

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleManipulationDto scheduleDto)
        {
            var scheduleToReturn = await _scheduleService.CreateScheduleAsync(scheduleDto);

            return CreatedAtRoute("Get schedule by id", new { id = scheduleToReturn.Id }, scheduleToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleService.DeleteScheduleAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleManipulationDto scheduleDto)
        {
            await _scheduleService.UpdateScheduleAsync(id, scheduleDto);

            return NoContent();
        }
    }
}
