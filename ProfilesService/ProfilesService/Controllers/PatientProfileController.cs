using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.RequestFeatures;
using ProfilesService.Filters;
using ProfilesService.Services.Interfaces;

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
        public async Task<IActionResult> GetAllPatientsProfiles([FromQuery] PatientParameters patientParameters)=>
            Ok(await _patientProfile.GetPatientProfiles(patientParameters));

        [HttpGet("{id}", Name = "GetPatientProfileById")]
        public async Task<IActionResult> GetPatientProfileById(int id) =>
            Ok(await _patientProfile.GetPatientProfile(id));

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreatePatientProfile([FromBody] PatientProfileManipulationDto patientProfileDto)
        {
            var profile = await _patientProfile.CreatePatientProfile(patientProfileDto);

            return CreatedAtRoute("GetPatientProfileById", new {id = profile.Id}, profile);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> UpdatePatientProfile(int id, 
            [FromBody] PatientProfileManipulationDto patientProfileDto)
        {
            await _patientProfile.UpdatePatientProfile(id, patientProfileDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientProfile(int id)
        {
            await _patientProfile.DeletePatientProfile(id);

            return NoContent();
        }
    }
}
