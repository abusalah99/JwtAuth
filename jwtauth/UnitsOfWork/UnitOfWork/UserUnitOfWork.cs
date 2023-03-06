namespace jwtauth;
public class UserUnitOfWork : BaseSettingsUnitOfWork<User> , IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    public UserUnitOfWork(IUserRepository repository) : base(repository) => _userRepository = repository;

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

        await _userRepository.Add(user);
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
        
        await _userRepository.Update(user);
    }

    public async Task DeleteUserByMail(string mail)=> await _userRepository.DeleteByMail(mail);

    public async Task<User> Login(LoginRequest request)
    {
        User? userFromDb = await GetUserByMail(request.mail);
        if (userFromDb == null)
            throw new ArgumentException("user was not found");
        if(!BCrypt.Net.BCrypt.Verify(request.password,userFromDb.Password))
            throw new ArgumentException("wrong password");

        return userFromDb;
    }
}

