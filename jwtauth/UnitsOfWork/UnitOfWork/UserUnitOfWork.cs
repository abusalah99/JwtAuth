using System.Runtime.CompilerServices;

namespace jwtauth;

public class UserUnitOfWork : BaseUnitOfWork<User>, IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtRefreshOptions _jwtRefreshOptions;
    private readonly JwtAccessOptions _jwtAccessOptions;
    private readonly IImageConverter _imageConverter;
    private readonly ICloud _cloud;

    public UserUnitOfWork(IUserRepository repository, IJwtProvider jwtProvider
        , RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtRefreshOptions> jwtRefreshOptions, IOptions<JwtAccessOptions> jwtAccessOptions,
        IImageConverter converter, ICloud cloud) : base(repository)
    {
        _userRepository = repository;
        _jwtProvider = jwtProvider;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtRefreshOptions = jwtRefreshOptions.Value;
        _jwtAccessOptions = jwtAccessOptions.Value;
        _imageConverter = converter;
        _cloud = cloud;
    }
    public override async Task Create(User user)
    {
        User? userFromDb = await GetUserByMail(user.Email);
        if (userFromDb != null)
            throw new ArgumentException("this mail is already used");

        if (user.Password.Length < 5)
            throw new ArgumentException("password must be at least 6 charaters");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "User";

        await base.Create(user);

    }
    public async Task<UserResponse> ReadUserResponse(Guid id)
    {
        UserResponse response = new();

        User user = await Read(id);

        response = MapFromUserToResponse(user, response);

        response.ImageUrl = await GetImageUrl(user);

        return response;
    }
    public async Task<UserResponse> Update(UserRequest userRequest, Guid id)
    {
        User? userFromDb = await _userRepository.Get(id);
        if (userFromDb == null)
            throw new ArgumentException("invaild Token");

        User user = new();
        UserResponse userResponse = new();

        if (userRequest.UserImage != null)
        {
            byte[] image = await _imageConverter.ConvertImage(userRequest.UserImage);
            if (userFromDb.ImageId == null)
                user.ImageId = await _cloud.UploadFile($"{userFromDb.Id}.jpg", image);
            else
                user.ImageId = await _cloud.UploadFile($"{userFromDb.Id}.jpg", image, userFromDb.ImageId);
        }

        user.Id = userFromDb.Id;
        user.FristName = userRequest.FristName;
        user.LastName = userRequest.LastName;
        user.Password = userFromDb.Password;
        user.Email = userRequest.Email;
        user.Age = userRequest.Age;
        user.Phone = userRequest.Phone;
        user.Token = userFromDb.Token;
        user.Role = userFromDb.Role;

        await Update(user);

        userResponse = MapFromUserToResponse(user, userResponse);
        userResponse.ImageUrl = await GetImageUrl(user);

        return userResponse;
    }
    public Task<User> GetUserByMail(string mail) => _userRepository.GetByMail(mail);

    public async Task<Token> Login(LoginRequest request)
    {
        User? userFromDb = await GetUserByMail(request.Email);

        if (userFromDb == null)
            throw new ArgumentException("user was not found");

        if (!BCrypt.Net.BCrypt.Verify(request.password, userFromDb.Password))
            throw new ArgumentException("wrong password");

        if (userFromDb.Token == null)
        {
            userFromDb.Token = CreateNewRefreshToken(userFromDb.Id);
            await Update(userFromDb);
        }

        if (!_refreshTokenValidator.Validate(userFromDb.Token.Value))
        {
            userFromDb.Token = CreateNewRefreshToken(userFromDb.Id, userFromDb.Token.Id);
            await Update(userFromDb);
        }

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt,
            Role = userFromDb.Role
        };

        return token;
    }

    public async Task<Token> Register(User user)
    {
        user.Token = CreateNewRefreshToken();
        user.CreatedAt = DateTime.UtcNow;

        await this.Create(user);

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(user),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = user.Token.Value,
            RefreshTokenExpiresAtExpires = user.Token.ExpireAt,
            Role = user.Role
        };

        return token;
    }

    public async Task<Token> Refresh(string refreshToken)
    {
        User? userFromDb = await _userRepository.GetByToken(refreshToken);

        if (userFromDb == null || !_refreshTokenValidator.Validate(refreshToken))
            throw new ArgumentException("Invalid Token");

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt,
            Role = userFromDb.Role
        };

        return token;
    }

    public async Task Logout(string refreshToken)
    {
        User? userFromDb = await _userRepository.GetByToken(refreshToken);

        if (userFromDb == null || !_refreshTokenValidator.Validate(refreshToken))
            throw new ArgumentException("Invalid Token");

        await _refreshTokenRepository.Remove(userFromDb.Token.Id);
    }

    public async Task<Token> UpdatePassword(PasswordRequest password, Guid id)
    {
        User userFromDb = await _userRepository.Get(id);

        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        if (!BCrypt.Net.BCrypt.Verify(password.Password, userFromDb.Password))
            throw new ArgumentException("wrong password");

        if (password.NewPassword == null)
            throw new ArgumentException("new password can not be null");

        userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(password.NewPassword);

        userFromDb.Token = CreateNewRefreshToken(userFromDb.Id, userFromDb.Token.Id);

        await Update(userFromDb);

        Token newToken = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt,
            Role = userFromDb.Role
        };

        return newToken;
    }

    private RefreshToken CreateNewRefreshToken(Guid userId = default(Guid)
        , Guid id = default(Guid))
    {
        string refreshToken = _jwtProvider.GenrateRefreshToken();

        RefreshToken newRefreshToken = new()
        {
            Id = id,
            Value = refreshToken,
            CreatedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddMonths(_jwtRefreshOptions.ExpireTimeInMonths),
            UserId = userId
        };

        return newRefreshToken;
    }
    private UserResponse MapFromUserToResponse(User user, UserResponse response)
    {
        response.Id = user.Id;  
        response.Email = user.Email;
        response.Age = user.Age;
        response.FristName = user.FristName;
        response.LastName = user.LastName;
        response.Phone = user.Phone;

        return response;
    }

    private async Task<string> GetImageUrl(User user)
    {
        string url = string.Empty;

        if (user.ImageId == null)
            url = await _cloud.GetFileUrl("DefualtUser.jpg");

        else
            url = await _cloud.GetFileUrl($"{user.Id}.jpg");

        return url;
    }

}