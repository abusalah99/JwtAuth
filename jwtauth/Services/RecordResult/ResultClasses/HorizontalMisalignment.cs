namespace jwtauth;

public class HorizontalMisalignment : IRecordResult
{
    public RecordResult GetResult(Guid userId) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "HorizontalMisalignment",
    };
}
