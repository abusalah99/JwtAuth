namespace jwtauth;

public interface IRecordResultRepository : IBaseRepositiorySettings<RecordResult> 
{
    Task<IEnumerable<RecordResult>> GetByUserId(Guid userId);
}