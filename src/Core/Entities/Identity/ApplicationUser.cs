using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
    }

    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
}
