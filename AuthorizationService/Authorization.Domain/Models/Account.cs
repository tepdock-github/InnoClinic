using Microsoft.AspNetCore.Identity;

namespace Authorization.Domain.Models
{
    public class Account : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
