using Microsoft.AspNetCore.Identity;

namespace ComputerSystem.Infrastructure.Identity
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}