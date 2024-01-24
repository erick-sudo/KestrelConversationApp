namespace Integration.ServiceTests.CompanyServiceTests;

public class CompanyServiceTests(ApplicationFixture applicationFixture) : TestBase(applicationFixture)
{
    [Fact]
    public async Task GetCompanyByIdAsync_WhenCompanyDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var companyId = Guid.NewGuid();

        // Act
        var result = await CompanyService.GetCompanyByIdAsync(companyId);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Company>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task GetCompanyByIdAsync_WhenCompanyExist_ReturnsSuccessDataResult()
    {
        // Arrange
        var company = await this.RegisterAndGetRandomCompanyAsync();

        // Act
        var result = await CompanyService.GetCompanyByIdAsync(company.Id);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<Company>>();
        result.Data.Should().BeEquivalentTo(company);
    }

    [Fact]
    public async Task GetAllCompaniesAsync_WhenCompanyListEmpty_ReturnsErrorDataResult()
    {
        // Arrange 


        // Act
        var result = await CompanyService.GetAllCompaniesAsync(c => c.Id == new Guid());

        // Assert
        result.Should().BeOfType<ErrorDataResult<IEnumerable<Company>>>();
        var errorResult = (ErrorDataResult<IEnumerable<Company>>)result;
        errorResult.Message.Should().Be(Messages.EmptyCompanyList);
    }

    [Fact]
    public async Task GetAllCompaniesAsync_WhenCompanyListIsNotEmpty_ReturnsSuccessDataResult()
    {
        // Arrange 
        var companyList = await this.RegisterAndGetAllCompaniesAsync(3);

        // Act
        var result = await CompanyService.GetAllCompaniesAsync(c => true);

        // Assert
        result.Should().BeOfType<SuccessDataResult<IEnumerable<Company>>>();
        var successResult = (SuccessDataResult<IEnumerable<Company>>)result;
        successResult.Data.Should().NotBeNull().And.Contain(companyList);
    }

    [Fact]
    public async Task CreateCompanyAsync_WhenCompanyAlreadyExists_ReturnsErrorResult()
    {
        // Arrange
        var createCompanyDto = await this.RegisterAndGetExistingCreateCompanyDtoAsync();

        // Act
        var result = await CompanyService.CreateCompanyAsync(createCompanyDto);

        // Assert
        result.Should().BeOfType<ErrorResult>();
        result.Message.Should().Be(Messages.CompanyAlreadyExists);
    }

    [Fact]
    public async Task CreateCompanyAsync_WhenCompanyDoesNotExist_ReturnsSuccessResult()
    {
        // Arrange
        var createCompanyDto = this.GetARandomCreateCompanyDto();

        // Act
        var result = await CompanyService.CreateCompanyAsync(createCompanyDto);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.CreateCompanySuccess);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WhenCompanyDoesNotExist_ReturnsErrorResult()
    {
        // Arrange 
        var updateCompanyDto = this.GetARandomUpdateCompanyDto();

        // Act
        var result = await CompanyService.UpdateCompanyAsync(updateCompanyDto);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Company>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WhenCompanyExistButCompanyIsNotActive_ReturnsErrorResult()
    {
        // Arrange
        var updateCompanyDto = await this.RegisterAndGetUpdateCompanyDtoAsync();

        // Act
        var result = await CompanyService.UpdateCompanyAsync(updateCompanyDto);

        // Assert
        result.Should().BeOfType<ErrorResult>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WhenCompanyExistsAndCompanyIsActive_ReturnsSuccessResult()
    {
        // Arrange
        var updateCompanyDto = await this.RegisterAndGetUpdateCompanyDtoAsync(isActive: true);

        // Act
        var result = await CompanyService.UpdateCompanyAsync(updateCompanyDto);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.UpdateCompanySuccess);
    }

    [Fact]
    public async Task DeleteCompanyAsync_WhenCompanyDoesNotExist_ReturnsErrorResult()
    {
        // Arrange 
        var deleteCompanyDto = this.GetARandomDeleteCompanyDto();

        // Act
        var result = await CompanyService.DeleteCompanyAsync(deleteCompanyDto);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Company>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task DeleteCompanyAsync_WhenCompanyExistsButCompanyIsNotActive_ReturnsErrorResult()
    {
        // Arrange 
        var deleteCompanyDto = await this.RegisterAndGetDeleteCompanyDtoAsync();

        // Act
        var result = await CompanyService.DeleteCompanyAsync(deleteCompanyDto);

        // Assert
        result.Should().BeOfType<ErrorResult>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task DeleteCompanyAsync_WhenCompanyExistsAndCompanyIsActive_ReturnsSuccessResult()
    {
        // Arrange 
        var deleteCompanyDto = await this.RegisterAndGetDeleteCompanyDtoAsync(isActive: true);

        // Act
        var result = await CompanyService.DeleteCompanyAsync(deleteCompanyDto);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.DeleteCompanySuccess);
    }

    [Fact]
    public async Task UpdateCompanyActivationStatusAsync_WhenCompanyDoesNotExist_ReturnsErrorResult()
    {
        // Arrange 
        var companyId = Guid.NewGuid();

        // Act
        var result = await CompanyService.UpdateCompanyActivationStatusAsync(companyId, true);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Company>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task UpdateCompanyActivationStatusAsync_WhenCompanyExists_ReturnsSuccessResult()
    {
        // Arrange 
        var company = await this.RegisterAndGetRandomCompanyAsync();

        // Act
        var result = await CompanyService.UpdateCompanyActivationStatusAsync(company.Id, true);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.CompanyStatusChanged);
    }

    [Fact]
    public async Task GetInactiveCompaniesAsync_WhenThereAreInactiveCompanies_ReturnsSuccessResult()
    {
        // Arrange 
        var inactiveCompanyList = await this.RegisterAndGetListCompanyDtoAsync(1);
        var inactiveCompany = inactiveCompanyList[0];

        // Act
        var result = await CompanyService.GetInactiveCompaniesAsync();

        // Assert 
        result.Data.Should().NotBeNull()
            .And.HaveCountGreaterThan(0)
            .And.ContainEquivalentOf(inactiveCompany);
    }

    [Fact]
    public async Task GetInactiveCompaniesAsync_WhenThereAreNoInactiveCompanies_ReturnsErrorResult()
    {
        // Arrange 
        var activeCompany = await this.RegisterAndGetListCompanyDtoAsync(1, isActive: true);

        // Act
        var result = await CompanyService.GetInactiveCompaniesAsync();

        // Assert 
        result.Data.Should().NotContain(activeCompany);
    }

    [Fact]
    public async Task ActivateCompanyAsync_WhenCompanyDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange 
        var companyId = Guid.NewGuid();

        // Act
        var result = await CompanyService.ActivateCompanyAsync(companyId);

        // Assert 
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task ActivateCompanyAsync_WhenCompanyAlreadyActivated_ReturnsErrorDataResult()
    {
        // Arrange 
        var activeCompany = await this.RegisterAndGetRandomCompanyAsync(isActive: true);

        // Act
        var result = await CompanyService.ActivateCompanyAsync(activeCompany.Id);

        // Assert 
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CompanyAlreadyActivated);
    }

    [Fact]
    public async Task ActivateCompanyAsync_WhenCompanySuccessfullyActivated_ReturnsSuccessDataResult()
    {
        // Arrange 
        var activeCompany = await this.RegisterAndGetRandomActiveCompanyWithEmployeeAsync();

        // Act
        var result = await CompanyService.ActivateCompanyAsync(activeCompany.Id);

        // Assert 
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CompanyActivationSuccess);
    }

    [Fact]
    public async Task CheckIfCompanyActiveAsync_WhenCompanyIsNotActive_ReturnsErrorResult()
    {
        // Arrange 
        var company = await this.RegisterAndGetRandomCompanyAsync();

        // Act
        var result = await CompanyService.CheckIfCompanyActiveAsync(company.Id);

        // Assert 
        result.Should().NotBeNull().And.BeOfType<ErrorResult>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task CheckIfCompanyActiveAsync_WhenCompanySuccessfullyActivated_ReturnsSuccessResult()
    {
        // Arrange 
        var activeCompany = await this.RegisterAndGetRandomCompanyAsync(isActive: true);

        // Act
        var result = await CompanyService.CheckIfCompanyActiveAsync(activeCompany.Id);

        // Assert 
        result.Should().NotBeNull().And.BeOfType<SuccessResult>();
        result.Message.Should().BeNull();
    }
}
