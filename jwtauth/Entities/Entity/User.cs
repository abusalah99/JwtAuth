namespace jwtauth;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? FristName { get; set; }
    public string? LastName { get; set; }    
    public string? Phone { get; set; }
    public DateTime? MailVerfiedAt { get; set; }
    public bool MailVerification { get;set; }
    public DateTime? MobileVerfiedAt { get; set; }
    public DateTime CreatedAt { get; set; }    
    public bool MobileVerification { get; set; }
    public string? ImageId { get; set; }
    public string? Role { get; set; }
    [JsonIgnore]
    public RefreshToken? Token { get; set; }
    [JsonIgnore]
    public IEnumerable<RecordResult>? Results { get; set; }
    [JsonIgnore]
    public OneTimePassword? OTP { get; set; }
}