namespace jwtauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSectionController : BaseSettingsController<HomeSection> 
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHomeSectionUnitOfWork _unitOfWork;
        public HomeSectionController(IHomeSectionUnitOfWork unitOfWork
            , IHostingEnvironment hostingEnvironment) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public override async Task<IActionResult> Get() =>await base.Get();

        [HttpPost, Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromForm] SectionRequest homeSectionRequest)
        {
                await _unitOfWork.Create(homeSectionRequest, _hostingEnvironment.ContentRootPath);

                ResponseResult<string> response = new("Home section created");
                return Ok(response);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromForm] SectionRequest homeSectionRequest)
        {
            await _unitOfWork.Update(homeSectionRequest, _hostingEnvironment.ContentRootPath);

            ResponseResult<string> response = new("Home section updated");
            return Ok(response);
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}