namespace jwtauth;
public class RefreshToken : BaseEntity
{
    public required string Value { get; set; } 
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpireAt { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}
 