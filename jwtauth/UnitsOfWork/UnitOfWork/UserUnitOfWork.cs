namespace jwtauth;
public class UserUnitOfWork : BaseSettingsUnitOfWork<User> , IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUnitOfWork> _logger;
    public UserUnitOfWork(IUserRepository repository, ILogger<UserUnitOfWork> logger) 
        : base(repository,logger)
    {
        _logger= logger;
        _userRepository = repository;
    } 

    public virtual async Task<User> GetUserByMail(string mail) => await _userRepository.GetByMail(mail);

    public override async Task Create(User user)
    {
        if (user == null)
            throw new ArgumentNullException("user was not provided.");

        User? userFromDb = await GetUserByMail(user.Email);
        if (userFromDb != null)
            throw new ArgumentException("this mail is already used");

        if (user.Password.Length < 5)
            throw new ArgumentException("password must be at least 6 charaters");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "User";

        await base.Create(user);
    }
    public override async Task Update(User user)
    {
        if (user == null)
            throw new ArgumentNullException("user was not provided.");

        User? userFromDb = await GetUserByMail(user.Email);
        if (userFromDb == null)
            throw new ArgumentException("user not found");

        if (!BCrypt.Net.BCrypt.Verify(user.Password,userFromDb.Password))
            throw new ArgumentException("Worng password");
        
       await base.Update(user);
    }

    public async Task DeleteUserByMail(string mail)
    {
        using IDbContextTransaction transaction = await _userRepository.GetTransaction();
        try
        {
            await _userRepository.DeleteByMail(mail);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }
        await transaction.CommitAsync();
    }

    public async Task<User> Login(LoginRequest request)
    {
        User? userFromDb = await GetUserByMail(request.Email);

        if (userFromDb == null)
            throw new ArgumentException("user was not found");

        if(!BCrypt.Net.BCrypt.Verify(request.password,userFromDb.Password))
            throw new ArgumentException("wrong password");

        return userFromDb;
    }
}

