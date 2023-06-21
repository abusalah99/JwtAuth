namespace jwtauth;

public record GoogleSmtpOptions
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
