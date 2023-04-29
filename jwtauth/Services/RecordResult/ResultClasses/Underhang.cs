namespace jwtauth;

public class Underhang : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Underhang",
        FilePath = rootPath + @"\Resources\Results\Underhang.pdf"
    };
}
