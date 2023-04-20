using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Filters;
using ProfilesService.Services.Interfaces;
using System.Data;

namespace ProfilesService.Controllers
{
    [Route("api/receptionists")]
    [ApiController]
    public class ReceptionistProfileController : ControllerBase
    {
        private readonly IReceptionistProfileService _receptionistProfile;

        public ReceptionistProfileController(IReceptionistProfileService receptionistProfile)
        {
            _receptionistProfile = receptionistProfile;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetAllReceptionistsProfiles() =>
            Ok(await _receptionistProfile.GetReceptionistProfiles());

        [HttpGet("{id}", Name = "GetReceptionistProfileById")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetReceptionistProfileById(int id) =>
            Ok(await _receptionistProfile.GetReceptionistProfile(id));

        [HttpGet("account/{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetReceptionistProfileByAccount(string id) =>
            Ok(await _receptionistProfile.GetReceptionistProfileByAccount(id));

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> CreateReceptionistProfile([FromBody] ReceptionistProfileManipulationDto 
            receptionistProfile)
        {
            var profile = await _receptionistProfile.CreateReceptionistProfile(receptionistProfile);

            return CreatedAtRoute("GetReceptionistProfileById", new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> UpdateReceptionistProfile(int id,
            [FromBody] ReceptionistProfileManipulationDto receptionistProfile)
        {
            await _receptionistProfile.UpdateReceptionistProfile(id, receptionistProfile);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> DeleteReceptionistProfile(int id)
        {
            await _receptionistProfile.DeleteReceptionistProfile(id);

            return NoContent();
        }
    }
}
