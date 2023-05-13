namespace jwtauth.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : BaseController<User>
{
    private readonly IUserUnitOfWork _unitOfWork;

    public UsersController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
        => _unitOfWork = userUnitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get() => await Read();

    [HttpPost]
    public async Task<IActionResult> Post(User user) => await Create(user);

    [HttpPut]
    public async Task<IActionResult> Put([FromForm] UserRequest requestUser)
    {
        User user = await _unitOfWork.Update(
            requestUser , requestUser.Id);

        ResponseResult<User> response = new(user);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => await Remove(id);
}
