using jwtauth.Response;

namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IResponse<User> _response;
    public RegisterController(IUserUnitOfWork userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
        _response = new SuccessResponse<User>();
    }
    [HttpPost]
    public async Task<IActionResult> Post(User user) 
    {
        await _userUnitOfWork.Create(user);

        return Ok(_response.CreateResponse(user));
    }
}


