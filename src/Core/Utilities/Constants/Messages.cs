namespace Core.Utilities.Constants;

public class Messages
{
    public const string CompanyNotFound = "Company not found";
    public const string EmployeeNotFound = "Employee not found";
    public const string UserNotFound = "User not found";
    public const string SpecialDayNotFound = "Special Day is not found";
    public const string NoInactiveCompany = "There is no inactive companies";
    public const string NoInactiveEmployee = "There is no inactive employees";

    public const string EmptyCompanyList = "Company list is empty with the current filter";
    public const string EmptyEmployeeList = "Employee list is empty with the current filter";

    public const string AddCompanyError = "Error occured while registering the company to Database";
    public const string AddEmployeeError = "Error occured while registering the employee to Database";
    public const string CreateCompanySuccess = "Company successfully created with its first employee";
    public const string CreateEmployeeSuccess = "Employee successfully created";
    public const string CompanyAlreadyExists = "Company already exists with this business registration number";
    public const string EmployeeAlreadyExists = "Employee already exists with this Email";

    public const string UpdateCompanyError = "Error occured while updating the company in Database";
    public const string UpdateEmployeeError = "Error occured while updating the employee in Database";
    public const string UpdateUserError = "Error occured while updating the user in Database";
    public const string UpdateCompanySuccess = "Company successfully updated";
    public const string UpdateEmployeeSuccess = "Employee successfully updated";

    public const string DeleteCompanyError = "Error occured while deleting the company from Database";
    public const string DeleteEmployeeError = "Error occured while deleting the employee from Database";
    public const string DeleteCompanySuccess = "Company successfully deleted";
    public const string DeleteEmployeeSuccess = "Employee successfully deleted";

    public const string CompanyStatusChanged = "The Company Activation Status Changed Successfully";
    public const string EmployeeStatusChanged = "The Employee Activation Status Changed Successfully";
    public const string CompanyStatusChangeError = "Company status could not change";
    public const string EmployeeStatusChangeError = "Employee status could not change";

    public const string CompanyActivationSuccess = "Company successfully activated";
    public const string CompanyActivationError = "Company couldn't activated";
    public const string CompanyAlreadyActivated = "The company has already been activated";
    public const string CompanyNotActiveError = "The company is not activated";

    public const string LoginSuccess = "Login successfull";
    public const string LogoutSuccess = "Logout successfull";
    public const string EmailSendSuccess = "Email successfully sent";
    public const string ResetPasswordSuccess = "Password successfully changed";
    public const string CreateNewPasswordSuccess = "Password successfully created";
    public const string PasswordChangeSuccess = "Password successfully changed";
    public const string EmailChangeSuccess = "Email successfully changed";
    public const string AccountBlocked = "Your account is currently suspended. Please try again later";
    public const string ResetPasswordError = "Error occured while creating a new password";
    public const string UserLockedOutError = "Your account is locked out";
    public const string EmailOrPasswordError = "Your email or password is not correct";
    public const string UserEmailAlreadyConfirmed = "This email has already been confirmed.";
    public const string InvalidCurrentPassword = "Current password is not correct";
    public const string EmailAlreadyExists = "Email is already in use by other user";
}
