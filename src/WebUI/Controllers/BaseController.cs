using Core.Identity;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminPortal.Controllers;

public class BaseController(
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    protected UserManager<ApplicationUser> UserManager { get; } = userManager;
    protected string CurrentUser => User.Identity.Name;
    protected Guid EmployeeId => Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == CustomClaims.EmployeeId).Value);
    protected string Email => User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email).Value;  
    protected Guid CompanyId => Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == CustomClaims.CompanyId).Value);
}
