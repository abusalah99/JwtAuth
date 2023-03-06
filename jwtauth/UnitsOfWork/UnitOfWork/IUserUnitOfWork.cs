namespace jwtauth;

public interface IUserUnitOfWork : IBaseSettingsUnitOfWork<User>
    {
    Task<User> GetUserByMail(string mail);
    Task DeleteUserByMail(string mail);
    Task<User> Login(LoginRequest request);
}

