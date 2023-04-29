﻿namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : BaseController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public LoginController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
            => _userUnitOfWork = userUnitOfWork;
    
    [HttpGet, Authorize(Roles ="User")]
    public override async Task<IActionResult> Get() => await base.Get();

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
