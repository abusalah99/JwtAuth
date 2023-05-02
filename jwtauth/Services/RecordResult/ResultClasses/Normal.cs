namespace jwtauth;

public class Normal : IRecordResult
{
    public RecordResult GetResult(Guid userId) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Normal",
    };
}
