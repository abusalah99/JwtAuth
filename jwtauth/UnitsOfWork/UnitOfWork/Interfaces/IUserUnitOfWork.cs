namespace jwtauth;

public interface IUserUnitOfWork : IBaseUnitOfWork<User>
    {
    Task<User> GetUserByMail(string mail);
    Task<Token> Login(LoginRequest request);
    Task<Token> Register(User user);
    Task<Token> Refresh(string refreshToken);
    Task Logout(string refreshToken);
    Task<UserResponse> Update(UserRequest user,Guid id);
    Task<UserResponse> ReadUserResponse(Guid id);
    Task<Token> UpdatePassword(PasswordRequest password,Guid id);

}
