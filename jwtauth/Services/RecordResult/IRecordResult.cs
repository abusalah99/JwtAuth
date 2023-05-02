namespace jwtauth;

public interface IRecordResult 
{
    RecordResult GetResult(Guid userId);
}