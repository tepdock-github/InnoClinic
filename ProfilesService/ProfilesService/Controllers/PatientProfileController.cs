using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.RequestFeatures;
using ProfilesService.Filters;
using ProfilesService.Services.Interfaces;
using System.Data;

namespace ProfilesService.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientProfileController : ControllerBase
    {
        private readonly IPatientProfileService _patientProfile;

        public PatientProfileController(IPatientProfileService patientProfile)
        {
            _patientProfile = patientProfile;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetAllPatientsProfiles([FromQuery] PatientParameters patientParameters)=>
            Ok(await _patientProfile.GetPatientProfiles(patientParameters));

        [HttpGet("{id}", Name = "GetPatientProfileById")]
        [Authorize]
        public async Task<IActionResult> GetPatientProfileById(int id) =>
            Ok(await _patientProfile.GetPatientProfile(id));

        [HttpGet("account/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPatientProfileByAccount(string id) =>
            Ok(await _patientProfile.GetPatientProfileByAccount(id));

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> CreatePatientProfile([FromBody] PatientProfileManipulationDto patientProfileDto)
        {
            var profile = await _patientProfile.CreatePatientProfile(patientProfileDto);

            return CreatedAtRoute("GetPatientProfileById", new {id = profile.Id}, profile);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> UpdatePatientProfile(int id, 
            [FromBody] PatientProfileManipulationDto patientProfileDto)
        {
            await _patientProfile.UpdatePatientProfile(id, patientProfileDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> DeletePatientProfile(int id)
        {
            await _patientProfile.DeletePatientProfile(id);

            return NoContent();
        }
    }
}
