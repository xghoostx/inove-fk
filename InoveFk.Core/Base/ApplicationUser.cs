using Microsoft.AspNetCore.Identity;

namespace InoveFk.Core.Base;

public class ApplicationUser : IdentityUser<Guid> 
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool FirstAccess { get; set; }
    public bool Active { get; private set; }

    public bool Activate() => Active = true;
    public bool Deactivate() => Active = false;
}
