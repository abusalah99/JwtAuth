namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public RegisterController(IUserUnitOfWork userUnitOfWork) =>_userUnitOfWork = userUnitOfWork;
    [HttpGet]
    public async Task<ActionResult<User>> Get() => Ok(await _userUnitOfWork.Read());
    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        try
        {
            await _userUnitOfWork.Create(user);
        }
        catch(Exception exception) 
        {
            return BadRequest(exception.Message);
        }
        return Ok(user);
    }
}


