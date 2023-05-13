namespace jwtauth.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
public class Status : ControllerBase
{
    private readonly IStatusUnitOfWork _UnitOfWork;

    public Status(IStatusUnitOfWork unitOfWork)
            => _UnitOfWork = unitOfWork;

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        jwtauth.Status status = await _UnitOfWork.GetStatus(DateTime.UtcNow.Year);

        ResponseResult<jwtauth.Status> response = new(status);

        return Ok(response);
    }
  
}
