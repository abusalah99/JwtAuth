namespace jwtauth;

public class Normal : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Normal",
        FilePath = rootPath + @"\Resources\Results\Normal.pdf"
    };
}
