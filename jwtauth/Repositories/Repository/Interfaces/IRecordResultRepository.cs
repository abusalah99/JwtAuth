using Microsoft.EntityFrameworkCore;

namespace jwtauth;

public interface IRecordResultRepository : IBaseRepositiorySettings<RecordResult> 
{
    Task<IEnumerable<RecordResult>> GetByUserId(Guid userId);
    Task<IEnumerable<RecordResult>>? GetRecordsCreatedToday();
    Task<IEnumerable<RecordResult>>? GetRecordsCreatedAtMonth(int month, int year);
}