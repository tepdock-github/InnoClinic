using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.RequestFeatures;
using ProfilesService.Filters;
using ProfilesService.Services.Interfaces;

namespace ProfilesService.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorProfileController : ControllerBase
    {
        private readonly IDoctorProfileService _doctorProfile;

        public DoctorProfileController(IDoctorProfileService profileService)
        {
            _doctorProfile = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsProfiles([FromQuery] DoctorParameters doctorParameters) =>
            Ok(await _doctorProfile.GetDoctorProfiles(doctorParameters));

        [HttpGet("{id}", Name = "GetDoctorProfileById")]
        public async Task<IActionResult> GetDoctorById(int id) =>
            Ok(await _doctorProfile.GetDoctorProfile(id));

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> CreateDoctorProfile([FromBody] DoctorProfileManipulationDto doctorProfileDto)
        {
            var profile = await _doctorProfile.CreateDoctorProfile(doctorProfileDto);

            return CreatedAtRoute("GetDoctorProfileById", new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> UpdateDoctorProfile(int id, [FromBody] DoctorProfileManipulationDto doctorProfileDto)
        {
            await _doctorProfile.UpdaeDoctorProfile(id, doctorProfileDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> DeleteDoctorProfile(int id)
        {
            await _doctorProfile.DeleteDoctorProfile(id);

            return NoContent();
        }
    }
}