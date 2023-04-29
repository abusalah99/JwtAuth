namespace jwtauth;

public interface IRecordResultUnitOfWork : IBaseSettingsUnitOfWork<RecordResult>
{
    Task Create(IFormFile formFile, string rootPath, Guid userId);
    Task<IEnumerable<RecordResult>> GetRecordsByUserId(Guid id);

}