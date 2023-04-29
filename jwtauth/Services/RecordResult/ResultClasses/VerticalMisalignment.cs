namespace jwtauth;

public class VerticalMisalignment : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "VerticalMisalignment",
        FilePath = rootPath + @"\Resources\Results\VerticalMisalignment.pdf"
    };
}
