namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public UserController(IUserUnitOfWork userUnitOfWork)
        =>_userUnitOfWork = userUnitOfWork;

    [HttpGet , Authorize]
    public async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();

        ResponseResult<User> response = new(await _userUnitOfWork.Read(userId));

        return Ok(response);
    }

    [HttpPut, Authorize]
    public async Task<IActionResult> Put(User requestUser)
    {
        Guid id = GetUserId();

        User user =  await _userUnitOfWork.Update(
            requestUser,id);

        ResponseResult<User> response = new(user);

        return Ok(response);
    }

    [HttpPut , Route("updatepassword"), Authorize]
    public async Task<IActionResult> Put(PasswordRequest requestUser)
    {
        Guid id = GetUserId();

        Token token = await _userUnitOfWork.UpdatePassword(
            requestUser, id);

        ResponseResult<Token> response = new(token);

        return Ok(token);
    }

    [HttpDelete, Authorize]
    public async Task<IActionResult> Delete()
    {
        Guid userId = GetUserId();

        await _userUnitOfWork.Delete(userId);

        ResponseResult<string> response = new("user deleted") ;

        return Ok(response);
    }

    private Guid GetUserId()
        => new( User.FindFirst("Id")?.Value);
}
