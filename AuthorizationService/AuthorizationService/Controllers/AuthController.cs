using Authorization.Domain.DataTransferObject;
using Authorization.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("connect/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;

        public AuthController(SignInManager<Account> signInManager, UserManager<Account> userManager,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///  for registrattion 
        /// </summary>
        /// <param name="registerAccountDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountDto registerAccountDto)
        {

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var account = _mapper.Map<Account>(registerAccountDto);

            var result = await _userManager.CreateAsync(account, account.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(account, registerAccountDto.Roles);
            await _signInManager.SignInAsync(account, false);
            return StatusCode(201);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
