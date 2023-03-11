namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public RegisterController(IUserUnitOfWork userUnitOfWork)
            =>_userUnitOfWork = userUnitOfWork;
   
    [HttpPost]
    public async Task<IActionResult> Post(User user) 
    {
        Token token = await _userUnitOfWork.Register(user);

        ResponseResult<Token> response = new(token);

        return Ok(response);
    }
}


