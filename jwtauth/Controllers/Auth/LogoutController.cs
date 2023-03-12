namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogoutController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public LogoutController(IUserUnitOfWork userUnitOfWork)
            =>_userUnitOfWork = userUnitOfWork;

    [HttpPost]
    public async Task<IActionResult> Logout(Token refreshToken)
    {
        await _userUnitOfWork.Logout(refreshToken.RefreshToken);

        ResponseResult<string> response = new("Logout Sccess");
        return Ok(response);
    }
}
