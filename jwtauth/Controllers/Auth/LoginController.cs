namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public LoginController(IUserUnitOfWork userUnitOfWork) => _userUnitOfWork = userUnitOfWork;
    [HttpGet]
    public async Task<ActionResult<User>> Get() => Ok(await _userUnitOfWork.Read());
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
          return Ok(await _userUnitOfWork.Login(request));
        }
        catch (Exception exception) 
        {
          return BadRequest(exception.Message);   
        }
        
    }
}
