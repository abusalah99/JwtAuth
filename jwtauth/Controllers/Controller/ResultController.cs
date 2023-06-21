namespace jwtauth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResultController : BaseSettingsController<RecordResult>
{
    private readonly IRecordResultUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _hostEnvironment;
    public ResultController(IRecordResultUnitOfWork unitOfWork
        ,IHostEnvironment hostEnvironment ) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();
        IEnumerable<RecordResultResponse> records = await _unitOfWork.GetRecordsByUserId(userId);

        ResponseResult<IEnumerable<RecordResultResponse>> response = new(records);

        return Ok(response);
    }

    [HttpPost , Authorize]
    public async Task<IActionResult> Create([FromForm] IFormFile record)
    {
        Guid userId = GetUserId();
        await _unitOfWork.Create(record, _hostEnvironment.ContentRootPath, userId);

        ResponseResult<string> response = new("Result created");

        return Ok(response);
    }

    [HttpPut, Authorize]
    public  async Task<IActionResult> Put(RecordResult entity)
            => await Update(entity);

    [HttpDelete, Authorize]
    public async Task<IActionResult> Delete(Guid id)
            => await Remove(id);
    
}
