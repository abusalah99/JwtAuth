namespace jwtauth;

public class VerticalMisalignment : IRecordResult
{
    public RecordResult GetResult(Guid userId) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "VerticalMisalignment",
    };
}
