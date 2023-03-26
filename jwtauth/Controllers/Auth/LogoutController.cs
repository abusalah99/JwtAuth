namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogoutController : BaseSettingsController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public LogoutController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
            => _userUnitOfWork = userUnitOfWork;

    [HttpPost]
    public async Task<IActionResult> Logout(Token refreshToken)
    {

        string token = refreshToken.RefreshToken 
            ?? Request.Cookies["RefreshToken"]?? string.Empty;

        await _userUnitOfWork.Logout(token);

        ResponseResult<string> response = new("Logout Sccess");

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");
     
        return Ok(response);
    }
}
