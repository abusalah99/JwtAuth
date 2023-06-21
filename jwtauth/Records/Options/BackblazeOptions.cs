namespace jwtauth;

public record BackblazeOptions
{
    public required string AccountId { get; set; }  
    public required string ApplicationKey { get; set; }
    public required string BucketId { get; set; }   
    public required string BucketName { get; set;}
}
