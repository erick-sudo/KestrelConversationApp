namespace Integration.Fixtures;

public class ApplicationFixture : IDisposable
{
    public IServiceProvider Services { get; }
    protected IConfiguration Configuration { get; }
    protected IWebHostEnvironment Environment { get; }
    protected WebApplication Application { get; } 
    internal ApplicationDbContext DataContext { get; }

    public ApplicationFixture()
    {
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");

        var builder = WebApplication.CreateBuilder([]);
        builder.Configuration.AddAppSettings();
        builder.Services.AddInfrastructure(builder)
            .AddSingleton(HttpContextAccessorBuilder.CreateHttpContextAccessorWithClaims("test@example.com"));
        Application = builder.Build();

        var serviceScope = Application.Services.CreateScope();
        Services = serviceScope.ServiceProvider;
        Configuration = Application.Configuration;
        Environment = Application.Environment; 

        DataContext = Services.GetRequiredService<ApplicationDbContext>();

        DataContext.Database.EnsureDeleted();
        DataContext.Database.Migrate();
        DataContext.Database.EnsureCreated();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DataContext.Database.EnsureDeleted();
            DataContext.Dispose();
        }
    }
}
