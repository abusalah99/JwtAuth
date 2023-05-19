namespace jwtauth;

public record PasswordRequest 
{
    public string? NewPassword { get; set; }
    public string? Password { get; set; }

}