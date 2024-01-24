using Core.DTOs;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Infrastructure.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JwtService(
    IConfiguration _configuration) : IJwtService
{
    public GenerateTokenServiceResponse GenerateToken(GenerateTokenServiceRequest request, int expiresInMinutes)
    {
        var claims = GetClaimsWithRoles(request);

        _configuration.CheckTokenPropertiesForNullability();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return new GenerateTokenServiceResponse
        {
            Token = tokenHandler.WriteToken(securityToken),
        };
    }

    private static List<Claim> GetClaimsWithRoles(GenerateTokenServiceRequest request)
    {
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };

        AddEmployeeIdAsClaimIfNotNullOrEmpty(request.EmployeeId, claims);

        AddCompanyIdAsClaimIfNotNullOrEmpty(request.CompanyId, claims);

        AddEmailAsClaimIfNotNullOrEmpty(request.Email, claims);

        AddRolesAsClaimIfNotNullOrEmpty(request.Roles, claims);

        return claims;
    }

    private static void AddCompanyIdAsClaimIfNotNullOrEmpty(string companyId, List<Claim> claims)
    {
        if (!string.IsNullOrEmpty(companyId))
        {
            claims.Add(new Claim(CustomClaims.CompanyId, companyId));
        }
    }

    private static void AddEmployeeIdAsClaimIfNotNullOrEmpty(string employeeId, List<Claim> claims)
    {
        if (!string.IsNullOrEmpty(employeeId))
        {
            claims.Add(new Claim(CustomClaims.EmployeeId, employeeId));
        }
    }

    private static void AddEmailAsClaimIfNotNullOrEmpty(string email, List<Claim> claims)
    {
        if (!string.IsNullOrEmpty(email))
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }
    }

    private static void AddRolesAsClaimIfNotNullOrEmpty(string roles, List<Claim> claims)
    {
        if (string.IsNullOrEmpty(roles)) { return; }

        var roleList = roles.Split(',');
        foreach (var role in roleList)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    }
}
