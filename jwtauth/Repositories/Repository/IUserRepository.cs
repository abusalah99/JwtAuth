namespace jwtauth;

public interface IUserRepository: IBaseRepositiorySettings<User>
{
    Task<User> GetByMail(string mail);
    Task DeleteByMail(string mail);
}
