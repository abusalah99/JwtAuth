namespace jwtauth;

public class RecordResult : BaseEntitySettings
{
    public DateTime CreatedAt { get; set; }
    public int Rate { get; set; }   
    public string? Feedback { get; set; } 
    [JsonIgnore]
    public User User { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }    
}