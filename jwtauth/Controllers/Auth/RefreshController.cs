namespace jwtauth.Controller;

[Route("api/[controller]")]
[ApiController]
public class RefreshController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public RefreshController(IUserUnitOfWork userUnitOfWork)=>
        _userUnitOfWork = userUnitOfWork;
    
    [HttpPost]
    public async Task<IActionResult> Refresh(Token refreshToken)
    {
        ResponseResult<Token> response = new(
            await _userUnitOfWork.Refresh(refreshToken.RefreshToken));

        return Ok(response);
    }

}
