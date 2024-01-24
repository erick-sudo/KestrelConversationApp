namespace Core.DTOs.Identity;

public class ChangePasswordDto
{ 
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string NewPasswordConfirm { get; set; }
}
