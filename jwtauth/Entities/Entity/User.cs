namespace jwtauth;

public class User : BaseEntitySettings
{
    public required string Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? Phone { get; set; }
    public string? Role { get; set; }
    [JsonIgnore]
    public RefreshToken? Token { get; set; }
    [JsonIgnore]
    public IEnumerable<RecordResult>? Results { get; set; }
}