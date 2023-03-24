namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public LoginController(IUserUnitOfWork userUnitOfWork) => _userUnitOfWork = userUnitOfWork;

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

    /*    var authorizationCookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = 
        };*/
        return Ok(response);
    }

}
