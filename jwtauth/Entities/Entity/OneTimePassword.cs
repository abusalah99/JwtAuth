namespace jwtauth;

public class OneTimePassword : BaseEntity
{
    public required string Value { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpireAt { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
}