namespace Integration.Base;

[Collection("Database collection")]
public abstract class TestBase
{
    public ApplicationFixture ApplicationFixture { get; }
    public ICompanyService CompanyService 
        => ApplicationFixture.Services.GetRequiredService<ICompanyService>();

    public IEmployeeService EmployeeService 
        => ApplicationFixture.Services.GetRequiredService<IEmployeeService>();

    protected TestBase(ApplicationFixture applicationFixture)
    {
        ApplicationFixture = applicationFixture;
    }
}
