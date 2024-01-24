namespace Integration.Harness;

internal static class EmployeeHarness
{
    internal static async Task<Employee> RegisterCompanyWithEmployeeAndGetEmployeeAsync(
        this TestBase testBase, bool assertSuccess = true, bool isEmployeeActive = false, bool isCompanyActive = false)
    {
        var companyService = testBase.ApplicationFixture.Services.GetRequiredService<ICompanyService>();
        var employeeRepository = testBase.ApplicationFixture.Services.GetRequiredService<IEmployeeRepository>();

        var employeeCounter = Guid.NewGuid().ToString()[..5];

        var companyDto = GetCreateCompanyDto();

        var registerResult = await companyService.CreateCompanyAsync(companyDto);
        AssertRegistrationSuccess(assertSuccess, registerResult);

        var companyListResult = await companyService.GetAllCompaniesAsync(c => c.BusinessRegistrationNumber == companyDto.BusinessRegistrationNumber);
        AssertCompanyList(assertSuccess, companyListResult.Data);

        var company = companyListResult.Data.First();
        var employee = companyListResult.Data.First().Employees.First();

        company.IsActive = isCompanyActive;
        employee.IsActive = isEmployeeActive;
        var updateResult = await employeeRepository.UpdateAsync(employee);
        AssertUpdateSuccess(assertSuccess, updateResult);

        return employee;
    }

    private static CreateCompanyDto GetCreateCompanyDto()
    {
        var employeeCounter = Guid.NewGuid().ToString()[..5];
        return new CreateCompanyDto
        {
            Name = $"Test Company{Guid.NewGuid().ToString()[..5]}",
            BusinessRegistrationNumber = $"{Guid.NewGuid().ToString()[..11]}",
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com"
        };
    }

    private static void AssertRegistrationSuccess(bool assertSuccess, Core.Utilities.Results.IResult registerResult)
    {
        if (assertSuccess)
            registerResult.Should().BeOfType<SuccessResult>();
    }

    private static void AssertCompanyList(bool assertSuccess, IEnumerable<Company> companyList)
    {
        if (assertSuccess)
            companyList.Should().NotBeNull().And.HaveCount(1);
    }

    private static void AssertUpdateSuccess(bool assertSuccess, int updateResult)
    {
        if (assertSuccess)
            updateResult.Should().BeGreaterThan(0);
    }

    internal static CreateEmployeeDto GetRandomCreateEmployeeDto(this TestBase testBase)
    {
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new CreateEmployeeDto
        {
            CompanyId = Guid.NewGuid(),
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com"
        };
    }

    internal static async Task<CreateEmployeeDto> RegisterAndGetCreateEmployeeDtoAsync(this TestBase testBase, bool isCompanyActive = false)
    {
        var firstEmployee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: isCompanyActive);
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new CreateEmployeeDto
        {
            CompanyId = firstEmployee.CompanyId,
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com",
        };
    }

    internal static async Task<CreateEmployeeDto> RegisterAndGetExistingCreateEmployeeDtoAsync(this TestBase testBase, bool isCompanyActive = false)
    {
        var firstEmployee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: isCompanyActive);
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new CreateEmployeeDto
        {
            CompanyId = firstEmployee.CompanyId,
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = firstEmployee.Email
        };
    }

    internal static UpdateEmployeeDto GetRandomUpdateEmployeeDto(this TestBase testBase)
    {
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new UpdateEmployeeDto
        {
            Id = Guid.NewGuid(),
            CompanyId = Guid.NewGuid(),
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com",
        };
    }

    internal static async Task<UpdateEmployeeDto> RegisterAndGetNonExistingUpdateEmployeeDtoAsync(this TestBase testBase, bool isCompanyActive = false)
    {
        var employee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: isCompanyActive);
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new UpdateEmployeeDto
        {
            Id = Guid.NewGuid(),
            CompanyId = employee.CompanyId,
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com"
        };
    }

    internal static async Task<UpdateEmployeeDto> RegisterAndGetExistingUpdateEmployeeDtoAsync(this TestBase testBase, bool isCompanyActive = false)
    {
        var employee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: isCompanyActive);
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new UpdateEmployeeDto
        {
            Id = employee.Id,
            CompanyId = employee.CompanyId,
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com"
        };
    }

    internal static DeleteEmployeeDto GetRandomDeleteEmployeeDto(this TestBase testBase)
    {
        return new DeleteEmployeeDto { Id = Guid.NewGuid(), DeletedBy = "Test User" };
    }

    internal static async Task<DeleteEmployeeDto> RegisterAndGetExistingDeleteEmployeeDtoAsync(this TestBase testBase, bool isCompanyActive = false)
    {
        var employee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: isCompanyActive);

        return new DeleteEmployeeDto { Id = employee.Id, DeletedBy = "Test User" };
    }

    internal static async Task<ListEmployeeDto> RegisterAndGetListEmployeeDtosAsync(this TestBase testBase, bool isEmployeeActive = false)
    {
        var employee = await testBase.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isEmployeeActive: isEmployeeActive);

        return new ListEmployeeDto
        {
            Id = employee.Id,
            CompanyId = employee.CompanyId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email
        };
    }
}
