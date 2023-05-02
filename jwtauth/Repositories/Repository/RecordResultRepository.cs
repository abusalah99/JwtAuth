namespace jwtauth;

public class RecordResultRepository : BaseRepositiorySettings<RecordResult>, IRecordResultRepository
{
    public RecordResultRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<RecordResult>> GetByUserId(Guid userId)
        => await Task.Run(() => dbSet.Where(e => e.UserId == userId).ToList());

    public async Task<IEnumerable<RecordResult>>? GetRecordsCreatedToday()
    => await dbSet.Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date).ToListAsync();

    public async Task<IEnumerable<RecordResult>>? GetRecordsCreatedAtMonth(int month, int year)
        => await dbSet.Where(e => e.CreatedAt.Month == month && e.CreatedAt.Year == year).ToListAsync();

}