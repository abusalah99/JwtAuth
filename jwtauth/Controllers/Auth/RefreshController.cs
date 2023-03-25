namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshController : AuthBaseController
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public RefreshController(IUserUnitOfWork userUnitOfWork)=>
        _userUnitOfWork = userUnitOfWork;
    
    [HttpPost]
    public async Task<IActionResult> Refresh(Token refreshToken)
    {
        string oldToken = refreshToken.RefreshToken
           ?? Request.Cookies["RefreshToken"] ?? string.Empty;

        Token token = await _userUnitOfWork.Refresh(oldToken);

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
