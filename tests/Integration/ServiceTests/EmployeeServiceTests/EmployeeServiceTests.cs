namespace Integration.ServiceTests.EmployeeServiceTests;

public class EmployeeServiceTests : TestBase
{
    public EmployeeServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture) { }


    [Fact]
    public async Task GetEmployeeByIdAsync_WhenEmployeeDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var companyId = Guid.NewGuid();

        // Act
        var result = await EmployeeService.GetEmployeeByIdAsync(companyId);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.EmployeeNotFound);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_WhenEmployeeExistsButCompanyIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync();

        // Act
        var result = await EmployeeService.GetEmployeeByIdAsync(employee.Id);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Data.Should().BeNull();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_WhenEmployeeExistsAndCompanyIsActive_ReturnsSuccessDataResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: true);

        // Act
        var result = await EmployeeService.GetEmployeeByIdAsync(employee.Id);

        // Assert
        result.Should().BeOfType<SuccessDataResult<Employee>>();
        result.Data.Should().BeEquivalentTo(employee);
    }

    [Fact]
    public async Task GetEmployeeByEmailAsync_WhenEmployeeDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var email = $"{Guid.NewGuid()}@test.com";

        // Act
        var result = await EmployeeService.GetEmployeeByEmailAsync(email);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.EmployeeNotFound);
    }

    [Fact]
    public async Task GetEmployeeByEmailAsync_WhenEmployeeExistsButCompanyIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync();

        // Act
        var result = await EmployeeService.GetEmployeeByEmailAsync(employee.Email);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Data.Should().BeNull();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task GetEmployeeByEmailAsync_WhenEmployeeExistsAndCompanyIsActive_ReturnsSuccessDataResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: true);

        // Act
        var result = await EmployeeService.GetEmployeeByEmailAsync(employee.Email);

        // Assert
        result.Should().BeOfType<SuccessDataResult<Employee>>();
        result.Data.Should().BeEquivalentTo(employee);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WhenEmployeeListEmpty_ReturnsErrorDataResult()
    {
        // Arrange 


        // Act
        var result = await EmployeeService.GetAllEmployeesAsync(c => c.Id == new Guid());

        // Assert
        result.Should().BeOfType<ErrorDataResult<IEnumerable<Employee>>>();
        result.Message.Should().Be(Messages.EmptyEmployeeList);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WhenEmployeeListIsNotEmpty_ReturnsSuccessDataResult()
    {
        // Arrange 
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync();

        // Act
        var result = await EmployeeService.GetAllEmployeesAsync(c => true);

        // Assert
        result.Should().BeOfType<SuccessDataResult<IEnumerable<Employee>>>();
        result.Data.Should().NotBeNull().And.Contain(employee);
    }

    [Fact]
    public async Task CreateEmployeeAsync_WhenCompanyDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange 
        var createEmployeeDto = this.GetRandomCreateEmployeeDto();

        // Act
        var result = await EmployeeService.CreateEmployeeAsync(createEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task CreateEmployeeAsync_WhenCompanyExistsButIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange 
        var createEmployeeDto = await this.RegisterAndGetCreateEmployeeDtoAsync();

        // Act
        var result = await EmployeeService.CreateEmployeeAsync(createEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task CreateEmployeeAsync_WhenEmployeeAlreadyExists_ReturnsErrorDataResult()
    {
        // Arrange 
        var createEmployeeDto = await this.RegisterAndGetExistingCreateEmployeeDtoAsync(isCompanyActive: true);

        // Act
        var result = await EmployeeService.CreateEmployeeAsync(createEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.EmployeeAlreadyExists);
    }

    [Fact]
    public async Task CreateEmployeeAsync_WhenCompanyExistsAndActive_ReturnsSuccessDataResult()
    {
        // Arrange 
        var createEmployeeDto = await this.RegisterAndGetCreateEmployeeDtoAsync(isCompanyActive: true);

        // Act
        var result = await EmployeeService.CreateEmployeeAsync(createEmployeeDto);

        // Assert
        result.Should().BeOfType<SuccessDataResult<ApplicationUser>>();
        result.Message.Should().Be(Messages.CreateEmployeeSuccess);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WhenCompanyDoesNotExist_ReturnsErrorResult()
    {
        // Arrange
        var updateEmployeeDto = this.GetRandomUpdateEmployeeDto();

        // Act 
        var result = await EmployeeService.UpdateEmployeeAsync(updateEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorResult>();
        result.Message.Should().Be(Messages.CompanyNotFound);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WhenCompanyAndEmployeeExistButCompanyIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange
        var updateEmployeeDto = await this.RegisterAndGetExistingUpdateEmployeeDtoAsync();

        // Act 
        var result = await EmployeeService.UpdateEmployeeAsync(updateEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WhenEmployeeDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var updateEmployeeDto = await this.RegisterAndGetNonExistingUpdateEmployeeDtoAsync(isCompanyActive: true);

        // Act 
        var result = await EmployeeService.UpdateEmployeeAsync(updateEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.EmployeeNotFound);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WhenCompanyAndEmployeeExistAndCompanyIsActive_ReturnsSuccessResult()
    {
        // Arrange
        var updateEmployeeDto = await this.RegisterAndGetExistingUpdateEmployeeDtoAsync(isCompanyActive: true);

        // Act 
        var result = await EmployeeService.UpdateEmployeeAsync(updateEmployeeDto);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.UpdateEmployeeSuccess);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WhenEmployeeDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var deleteEmployeeDto = this.GetRandomDeleteEmployeeDto();

        // Act 
        var result = await EmployeeService.DeleteEmployeeAsync(deleteEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.EmployeeNotFound);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WhenEmployeeExistsButCompanyIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange
        var deleteEmployeeDto = await this.RegisterAndGetExistingDeleteEmployeeDtoAsync();

        // Act 
        var result = await EmployeeService.DeleteEmployeeAsync(deleteEmployeeDto);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WhenEmployeeExistsAndCompanyIsActive_ReturnsSuccessResult()
    {
        // Arrange
        var deleteEmployeeDto = await this.RegisterAndGetExistingDeleteEmployeeDtoAsync(isCompanyActive: true);

        // Act 
        var result = await EmployeeService.DeleteEmployeeAsync(deleteEmployeeDto);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.DeleteEmployeeSuccess);
    }

    [Fact]
    public async Task UpdateEmployeeActivationStatusAsync_WhenEmployeeDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();

        // Act 
        var result = await EmployeeService.UpdateEmployeeActivationStatusAsync(employeeId, true);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.EmployeeNotFound);
    }

    [Fact]
    public async Task UpdateEmployeeActivationStatusAsync_WhenEmployeeExistsButCompanyIsNotActive_ReturnsErrorDataResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync();

        // Act 
        var result = await EmployeeService.UpdateEmployeeActivationStatusAsync(employee.Id, !employee.IsActive);

        // Assert
        result.Should().BeOfType<ErrorDataResult<Employee>>();
        result.Message.Should().Be(Messages.CompanyNotActiveError);
    }

    [Fact]
    public async Task UpdateEmployeeActivationStatusAsync_WhenEmployeeExistsAndCompanyIsActive_ReturnsSuccessResult()
    {
        // Arrange
        var employee = await this.RegisterCompanyWithEmployeeAndGetEmployeeAsync(isCompanyActive: true);

        // Act 
        var result = await EmployeeService.UpdateEmployeeActivationStatusAsync(employee.Id, !employee.IsActive);

        // Assert
        result.Should().BeOfType<SuccessResult>();
        result.Message.Should().Be(Messages.EmployeeStatusChanged);
    }

    [Fact]
    public async Task GetInactiveEmployeesAsync_WhenThereAreNoInactiveEmployees_ReturnsErrorDataResult()
    {
        // Arrange 
        var activeEmployee = await this.RegisterAndGetListEmployeeDtosAsync(isEmployeeActive: true);

        // Act
        var result = await EmployeeService.GetInactiveEmployeesAsync();

        // Assert 
        result.Data.Should().NotContainEquivalentOf(activeEmployee);
    }

    [Fact]
    public async Task GetInactiveEmployeesAsync_WhenThereAreInactiveEmployees_ReturnsSuccessDataResult()
    {
        // Arrange 
        var inactiveEmployee = await this.RegisterAndGetListEmployeeDtosAsync();

        // Act
        var result = await EmployeeService.GetInactiveEmployeesAsync();

        // Assert 
        result.Data.Should().NotBeNull()
            .And.HaveCountGreaterThan(0)
            .And.ContainEquivalentOf(inactiveEmployee);
    }
}
