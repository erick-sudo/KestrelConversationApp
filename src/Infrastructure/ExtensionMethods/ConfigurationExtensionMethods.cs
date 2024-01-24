using Microsoft.Extensions.Configuration;

namespace Infrastructure.ExtensionMethods;

public static class ConfigurationExtensions
{
    public static void CheckTokenPropertiesForNullability(this IConfiguration configuration)
    {
        var securityKey = configuration["Token:SecurityKey"];
        var issuer = configuration["Token:Issuer"];
        var audience = configuration["Token:Audience"];

        if (string.IsNullOrEmpty(securityKey))
        {
            throw new ArgumentNullException(nameof(securityKey), "Token:SecurityKey is null");
        }

        if (string.IsNullOrEmpty(issuer))
        {
            throw new ArgumentNullException(nameof(issuer), "Token:Issuer is null");
        }

        if (string.IsNullOrEmpty(audience))
        {
            throw new ArgumentNullException(nameof(audience), "Token:Audience is null");
        }
    }
}
