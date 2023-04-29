namespace jwtauth;

public class Imbalance : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Imbalance",
        FilePath = rootPath + @"\Resources\Results\Imbalance.pdf"
    };
}
