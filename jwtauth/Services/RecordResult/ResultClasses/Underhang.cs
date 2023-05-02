namespace jwtauth;

public class Underhang : IRecordResult
{
    public RecordResult GetResult(Guid userId) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Underhang",
    };
}
