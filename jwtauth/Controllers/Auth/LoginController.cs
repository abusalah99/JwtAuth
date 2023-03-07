using Azure.Core;
using jwtauth.Response;

namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IResponse<User> _response;
    public LoginController(IUserUnitOfWork userUnitOfWork)
    {
        _userUnitOfWork = userUnitOfWork;
        _response = new SuccessResponse<User>();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
        => Ok(_response.CreateResponse(await _userUnitOfWork.Login(request)));

}
