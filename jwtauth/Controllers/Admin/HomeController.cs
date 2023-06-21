namespace jwtauth.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class HomeController : BaseSettingsController<HomeSection>
{
    private readonly IHomeSectionUnitOfWork _unitOfWork;
    public HomeController(IHomeSectionUnitOfWork unitOfWork)
        : base(unitOfWork) => _unitOfWork = unitOfWork; 

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] SectionRequest homeSectionRequest)
    {
        await _unitOfWork.Create(homeSectionRequest);

        ResponseResult<string> response = new("Home section created");
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromForm] SectionRequest homeSectionRequest)
    {
        await _unitOfWork.Update(homeSectionRequest);

        ResponseResult<string> response = new("Home section updated");
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => await Remove(id);
}

