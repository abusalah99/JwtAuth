namespace jwtauth;

public class User : BaseEntitySettings
{
    public required string Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? Phone { get; set; }
    public int RecordNumber { get; set; }
    public string? Role { get; set; }
    public String? Token { get; set; }

}
