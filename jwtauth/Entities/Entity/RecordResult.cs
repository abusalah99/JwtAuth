namespace jwtauth;

public class RecordResult : BaseEntitySettings
{
    public required string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Rate { get; set; }   
    public string? Feedback { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    public Guid UserId { get; set; }    
}