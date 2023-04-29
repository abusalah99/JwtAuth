namespace jwtauth;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User> GetByMail(string mail);
    Task DeleteByMail(string mail);
    Task<User>? GetByToken(string token);

}
