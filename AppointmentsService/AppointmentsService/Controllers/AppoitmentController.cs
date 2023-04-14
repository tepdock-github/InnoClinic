using AppointmentsService.Filters;
using AppointmentsService.Services.Interfaces;
using Appoitments.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsService.Controllers
{
    [Route("/api/appoitments")]
    [ApiController]
    [Authorize]
    public class AppoitmentController : ControllerBase
    {
        private readonly IAppoitmentService _appoitmentService;

        public AppoitmentController(IAppoitmentService appoitmentService)
        {
            _appoitmentService = appoitmentService;
        }

        /// <summary>
        ///     Get All Appoitmens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAppoitments() =>
            Ok(await _appoitmentService.GetAppoitments());

        /// <summary>
        ///     Get Appoitment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetAppoitmentById")]
        public async Task<IActionResult> GetAppoitmentById(int id) =>
            Ok(await _appoitmentService.GetAppoitmentById(id));

        /// <summary>
        ///     Get Doctor's History: all appoitments that was completed for the doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        [HttpGet("doctor-history/{doctorId}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetDoctorHistory(Guid doctorId) =>
            Ok(await _appoitmentService.GetDoctorHistory(doctorId));

        /// <summary>
        ///     Get Patient's History: all appoitments that was completed for the patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("patient-history/{patientId}")]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> GetPatientHistory(Guid patientId) =>
         Ok(await _appoitmentService.GetPatientHistory(patientId));

        /// <summary>
        ///     Get Doctor's Schedule: all appoitments that was approved for doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        [HttpGet("doctor-schedule/{doctorId}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetDoctorSchedule(Guid doctorId) =>
         Ok(await _appoitmentService.GetDoctorSchedule(doctorId));

        /// <summary>
        ///     Get Patient's Schedule: all appoitments for patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("patient-schedule/{patientId}")]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> GetPatientAppoitments(Guid patientId) =>
            Ok(await _appoitmentService.GetPatientAppoitments(patientId));

        /// <summary>
        ///     Create new Appoitment
        /// </summary>
        /// <param name="appoitmentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateAppoitment([FromBody] AppoitmentManipulationDto appoitmentDto)
        {
            var appoitmentToReturn = await _appoitmentService.CreateAppoitment(appoitmentDto);

            return CreatedAtRoute("GetAppoitmentById", new { id = appoitmentToReturn.Id }, appoitmentToReturn);
        }


        /// <summary>
        ///     Update Existing Appoitment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appoitmentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> UpdateAppoitment(int id, [FromBody] AppoitmentManipulationDto appoitmentDto)
        {
            await _appoitmentService.UpdateAppoitment(id, appoitmentDto);

            return NoContent();
        }
    }
}
