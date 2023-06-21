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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => await Remove(id);
}
