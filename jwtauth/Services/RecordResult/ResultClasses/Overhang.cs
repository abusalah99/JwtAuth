namespace jwtauth;

public class Overhang : IRecordResult
{
    public RecordResult GetResult(Guid userId, string rootPath) => new()
    {
        UserId = userId,
        CreatedAt = DateTime.UtcNow.AddHours(2),
        Name = "Overhang",
        FilePath = rootPath + @"\Resources\Results\Overhang.pdf"
    };
}
