﻿namespace Core.DTOs.Identity;

public class LoginDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
