using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.ExtensionMethods;

public static class ApplicationBuilderExtensionMethods
{
    public static async Task EnsureDatabaseCreated(this IApplicationBuilder app, CancellationToken cancellationToken = default)
    {
        using var scope = app.ApplicationServices.CreateScope(); 
        var salesContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await salesContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
