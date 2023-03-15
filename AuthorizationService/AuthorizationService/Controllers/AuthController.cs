using Authorization.Domain.DataTransferObject;
using Authorization.Domain.Models;
using AutoMapper;
using CustomExceptionMiddleware.Exceptions;
using EmailService.Models;
using EmailService.Services;
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
        private readonly IEmailService _emailService;

        public AuthController(SignInManager<Account> signInManager, UserManager<Account> userManager,
            IMapper mapper, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(account);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", 
                new {token, email = account.Email}, Request.Scheme);

            var message = new Message(new string[] { account.Email }, "Coinfirmation Email", confirmationLink);
            await _emailService.SendEmail(message);

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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new BadRequestException("smth went wrong when confirming email");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded ? Ok() : BadRequest(result);
        }
    }
}
