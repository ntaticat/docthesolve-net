using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
}