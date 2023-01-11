using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.DataTransferObject
{
    public class LoginAccountDto
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
