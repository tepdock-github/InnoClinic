using Authorization.Domain.DataTransferObject;
using FluentValidation;

namespace AuthorizationService.FluentValidation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterAccountDto>
    {
        private readonly IConfiguration _configuration;

        public RegisterDtoValidator(IConfiguration configuration)
        {
            _configuration = configuration;

            var passwordSetting = _configuration.GetSection("PasswordConfiguration");
            int minPasswordLength = int.Parse(passwordSetting?.GetSection("MinRequiredLength").Value);
            int maxPasswordLength = int.Parse(passwordSetting?.GetSection("MaxRequiredLength").Value);

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(minPasswordLength).WithMessage("Your password length must " +
                "be at least " + minPasswordLength)
                .MaximumLength(maxPasswordLength).WithMessage("Your password length must " +
                "be at most " + maxPasswordLength);

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .Equal(r => r.Password);

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
