namespace Core.DTOs.Identity;

public class ResetPasswordDto
{
    public string NewPassword { get; set; }

    public string NewPasswordConfirm { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }
}
