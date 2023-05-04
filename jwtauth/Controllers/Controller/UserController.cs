namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public UserController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
        => _userUnitOfWork = userUnitOfWork;

    [HttpGet , Authorize]
    public async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();

        return await Read(userId);
    }

    [HttpPut, Authorize]
    public async Task<IActionResult> Put([FromForm] UserRequest requestUser)
    {
        Guid id = GetUserId();

        User user =  await _userUnitOfWork.Update(
            requestUser,id);

        ResponseResult<User> response = new(user);

        return Ok(response);
    }

    [HttpPut , Route("updatepassword"), Authorize]
    public async Task<IActionResult> Put( PasswordRequest requestUser)
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

        return await Remove(userId);
    }

}
