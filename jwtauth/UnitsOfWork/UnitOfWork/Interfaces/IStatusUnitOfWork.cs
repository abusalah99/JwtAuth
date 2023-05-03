namespace jwtauth;

public interface IStatusUnitOfWork
{
    Task<Status> GetStatus(int year);
}
