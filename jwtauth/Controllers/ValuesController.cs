namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IStatusUnitOfWork _UnitOfWork;

    public ValuesController(IStatusUnitOfWork unitOfWork)
            => _UnitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Status status = await _UnitOfWork.GetStatus(DateTime.UtcNow.Year);

        ResponseResult<Status> response = new(status);

        return Ok(response);
    }
  
}
