namespace jwtauth;

public class RecordResultRepository : BaseRepositiorySettings<RecordResult>, IRecordResultRepository
{
    public RecordResultRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<RecordResult>> GetByUserId(Guid userId)
        => await Task.Run(() => dbSet.Where(e => e.UserId == userId).ToList());
 
}