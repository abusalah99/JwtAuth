﻿namespace jwtauth;
public class User : BaseEntitySettings
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Role { get; set;}

}
