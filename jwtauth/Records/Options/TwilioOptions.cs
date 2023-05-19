namespace jwtauth;

public record TwilioOptions
{
    public string? AccountSID { get; set; } 
    public string? AuthToken { get; set; }
    public string? PhoneNumber { get; set; }
}
