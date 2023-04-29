namespace jwtauth;

public class HorizontalMisalignment : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Horizontal Misalignment",
        FilePath = rootPath + @"\Resources\Results\HorizontalMisalignment.pdf"
    };
}
