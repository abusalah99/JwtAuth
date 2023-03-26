namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseSettingsController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public UserController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
        => _userUnitOfWork = userUnitOfWork;

    [HttpGet , Authorize]
    public override async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();

        return await Get(userId);
    }

    [HttpPut, Authorize]
    public override async Task<IActionResult> Put(User requestUser)
    {
        Guid id = GetUserId();

        User user =  await _userUnitOfWork.Update(
            requestUser,id);

        ResponseResult<User> response = new(user);

        return Ok(response);
    }

    [HttpPut , Route("updatepassword"), Authorize]
    public async Task<IActionResult> Put(PasswordRequest requestUser)
    {
        Guid id = GetUserId();

        Token token = await _userUnitOfWork.UpdatePassword(
            requestUser, id);

        ResponseResult<Token> response = new(token);

        SetCookie("AccessToken",
        token.AccessToken,
        token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAtExpires);

        return Ok(response);
    }

    [HttpDelete, Authorize]
    public async Task<IActionResult> Delete()
    {
        Guid userId = GetUserId();

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");

        return await Delete(userId);
    }
    protected Guid GetUserId()
    => new(User.FindFirst("Id")?.Value);
}
