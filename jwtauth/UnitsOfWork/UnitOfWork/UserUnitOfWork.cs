using Azure.Core;

namespace jwtauth;

public class UserUnitOfWork : BaseSettingsUnitOfWork<User> , IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUnitOfWork> _logger;
    private readonly IJwtProvider _jwtProvider;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    public UserUnitOfWork(IUserRepository repository, ILogger<UserUnitOfWork> logger,
        IJwtProvider jwtProvider , RefreshTokenValidator refreshTokenValidator ) : base(repository,logger)
    {
        _logger= logger;
        _userRepository = repository;
        _jwtProvider= jwtProvider;
        _refreshTokenValidator= refreshTokenValidator;
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
    public async Task<User> Update(User requestUser , Guid id)
    {
        if (requestUser == null)
            throw new ArgumentNullException("user was not provided.");

        User? userFromDb = await _userRepository.Get(id);
        if (userFromDb == null)
            throw new ArgumentException("invaild Token");

        User user = new()
        {
            Id = userFromDb.Id,
            Name = requestUser.Name,
            Password = userFromDb.Password,
            Email = requestUser.Email,
            Age = requestUser.Age,
            Phone = requestUser.Phone,
            Token = userFromDb.Token,
            Role = userFromDb.Role,
            RecordNumber = userFromDb.RecordNumber
        };

        await Update(user);

        return user;
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

    public async Task<Token> Login(LoginRequest request)
    {
        User? userFromDb = await GetUserByMail(request.Email);

        if (userFromDb == null)
            throw new ArgumentException("user was not found");

        if(!BCrypt.Net.BCrypt.Verify(request.password,userFromDb.Password))
            throw new ArgumentException("wrong password");

        string refreshToken = _jwtProvider.GenrateRefreshToken();
        if (userFromDb.Token == null || !_refreshTokenValidator.Validate(userFromDb.Token))
        {
            userFromDb.Token = refreshToken;
            await _userRepository.Update(userFromDb);
        }
        
        Token token = new(){
           AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
           RefreshToken= userFromDb.Token,
        };

        return token;
    }
    public async Task<Token> Register(User user)
    {
        string refreshToken = _jwtProvider.GenrateRefreshToken();

        user.Token = refreshToken;

        await this.Create(user);

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(user),
            RefreshToken = refreshToken
        };

        return token;
    }

    public async Task<Token> Refresh(string refreshToken)
    {
        User userFromDb = await _userRepository.GetByToken(refreshToken);
        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            RefreshToken = refreshToken
        };

        return token;
    }

    public async Task Logout(string refreshToken)
    {
        User userFromDb = await _userRepository.GetByToken(refreshToken);
        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        userFromDb.Token = null;

        await _userRepository.Update(userFromDb);
    }

    public async Task<Token> UpdatePassword(PasswordRequest password, Guid id)
    {
        User userFromDb = await _userRepository.Get(id);

        if (userFromDb == null)
            throw new ArgumentException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(password.Password, userFromDb.Password))
            throw new ArgumentException("wrong password");

        if (password.NewPassword == null)
            throw new ArgumentException("new password can not be null");

        string refreshToken = _jwtProvider.GenrateRefreshToken();

        userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(password.NewPassword);
        userFromDb.Token = refreshToken;

        await _userRepository.Update(userFromDb);

        Token newToken = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            RefreshToken = refreshToken
        };

        return newToken;
    }

}

