using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.DataTransferObject
{
    public class RegisterAccountDto
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
