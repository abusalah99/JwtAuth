namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : BaseSettingsController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public LoginController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
            => _userUnitOfWork = userUnitOfWork;
    
    [HttpGet, Authorize(Roles ="User")]
    public async Task<IActionResult> Get()
    {
        ResponseResult<IEnumerable<User>> response = new(await _userUnitOfWork.Read());

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        Token token = await _userUnitOfWork.Login(request);

        ResponseResult<Token> response = new(token);

        SetCookie("AccessToken",
            token.AccessToken,
            token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAtExpires);

        return Ok(response);
    }
}
