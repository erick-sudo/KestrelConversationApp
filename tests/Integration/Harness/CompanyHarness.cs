namespace Integration.Harness;

internal static class CompanyHarness
{
    internal static async Task<Company> RegisterAndGetRandomCompanyAsync(this TestBase testBase, bool assertSuccess = true, bool isActive = false)
    {
        var companyRepository = testBase.ApplicationFixture.Services.GetRequiredService<ICompanyRepository>();
        var randomBusinessNumber = Guid.NewGuid().ToString()[..11];
        var companyCounter = Guid.NewGuid().ToString()[..5];

        var companyToAdd = new Company($"Test Company{companyCounter}", $"{randomBusinessNumber}", $"testUser{companyCounter}")
        {
            IsActive = isActive
        };

        var registerResult = await companyRepository.AddAsync(companyToAdd);
        AssertRegisterResult(assertSuccess, registerResult);

        return companyToAdd;
    }

    internal static async Task<List<Company>> RegisterAndGetAllCompaniesAsync(this TestBase testBase, int companyCount, bool assertSuccess = true, bool isActive = false)
    {
        var companyRepository = testBase.ApplicationFixture.Services.GetRequiredService<ICompanyRepository>();
        var companyList = new List<Company>();

        for (int i = 1; i <= companyCount; i++)
        {
            var companyToAdd = GetRandomCompany(isActive);

            var registerResult = await companyRepository.AddAsync(companyToAdd);
            AssertRegisterResult(assertSuccess, registerResult);

            companyList.Add(companyToAdd);
        }

        return companyList;
    }

    private static void AssertRegisterResult(bool assertSuccess, int registerResult)
    {
        if (assertSuccess)
            registerResult.Should().BeGreaterThan(0);
    }

    private static Company GetRandomCompany(bool isActive)
    {
        var randomBusinessNumber = Guid.NewGuid().ToString()[..11];
        var companyCounter = Guid.NewGuid().ToString()[..5];

        var companyToAdd = new Company($"Test Company{companyCounter}", $"{randomBusinessNumber}", $"testUser{companyCounter}")
        {
            IsActive = isActive
        };
        return companyToAdd;
    }

    internal static async Task<CreateCompanyDto> RegisterAndGetExistingCreateCompanyDtoAsync(this TestBase testBase)
    {
        var companyToAdd = await testBase.RegisterAndGetRandomCompanyAsync();

        return new CreateCompanyDto
        {
            Name = companyToAdd.Name,
            BusinessRegistrationNumber = companyToAdd.BusinessRegistrationNumber,
            FirstName = "Existing John",
            LastName = "Existing Doe",
            PhoneNumber = "123-456-7890",
            Email = "Existing.john.doe@example.com",
        };
    }

    internal static CreateCompanyDto GetARandomCreateCompanyDto(this TestBase testBase)
    {
        var employeeCounter = Guid.NewGuid().ToString()[..5];

        return new CreateCompanyDto
        {
            Name = $"Test Company{Guid.NewGuid().ToString()[..5]}",
            BusinessRegistrationNumber = $"{Guid.NewGuid().ToString()[..11]}",
            FirstName = $"John{employeeCounter}",
            LastName = $"Doe{employeeCounter}",
            PhoneNumber = "123-456-7890",
            Email = $"john{employeeCounter}.doe{employeeCounter}@example.com",
        };
    }

    internal static async Task<UpdateCompanyDto> RegisterAndGetUpdateCompanyDtoAsync(this TestBase testBase, bool isActive = false)
    {
        var companyToAdd = await testBase.RegisterAndGetRandomCompanyAsync(isActive: isActive);

        return new UpdateCompanyDto
        {
            Id = companyToAdd.Id,
            Name = "Updated Company Name",
            BusinessRegistrationNumber = $"-{companyToAdd.BusinessRegistrationNumber}-",
        };
    }

    internal static UpdateCompanyDto GetARandomUpdateCompanyDto(this TestBase testBase)
    {
        return new UpdateCompanyDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Name",
            BusinessRegistrationNumber = $"{Guid.NewGuid().ToString()[..11]}",
        };
    }

    internal static async Task<DeleteCompanyDto> RegisterAndGetDeleteCompanyDtoAsync(this TestBase testBase, bool isActive = false)
    {
        var companyToAdd = await testBase.RegisterAndGetRandomCompanyAsync(isActive: isActive);

        return new DeleteCompanyDto { Id = companyToAdd.Id, DeletedBy = "Random Test User" };
    }

    internal static DeleteCompanyDto GetARandomDeleteCompanyDto(this TestBase testBase)
    {
        return new DeleteCompanyDto { Id = new Guid(), DeletedBy = "Random Test User" };
    }

    internal static async Task<List<ListCompanyDto>> RegisterAndGetListCompanyDtoAsync(this TestBase testBase, int companyCount, bool isActive = false)
    {
        var companyList = await testBase.RegisterAndGetAllCompaniesAsync(companyCount, isActive);
        var listDtos = new List<ListCompanyDto>();

        foreach (var company in companyList)
        {
            listDtos.Add(new ListCompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                BusinessRegistrationNumber = company.BusinessRegistrationNumber,
            });
        }

        return listDtos;
    }

    internal static async Task<Company> RegisterAndGetRandomActiveCompanyWithEmployeeAsync(this TestBase testBase, bool assertSuccess = true)
    {
        var companyService = testBase.ApplicationFixture.Services.GetRequiredService<ICompanyService>();

        var companyDto = GetARandomCreateCompanyDto(testBase);

        var registerResult = await companyService.CreateCompanyAsync(companyDto);
        if (assertSuccess)
            registerResult.Should().BeOfType<SuccessResult>();

        var company = await companyService.GetAllCompaniesAsync(c => c.BusinessRegistrationNumber == companyDto.BusinessRegistrationNumber);
        if (assertSuccess)
            company.Data.Should().NotBeNull().And.HaveCount(1);

        return company.Data.First();
    }
}
