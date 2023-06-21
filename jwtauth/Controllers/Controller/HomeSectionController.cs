namespace jwtauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSectionController : BaseSettingsController<HomeSection>
    {
        private readonly IHomeSectionUnitOfWork _unitOfWork;
        public HomeSectionController(IHomeSectionUnitOfWork unitOfWork)
                 : base(unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<HomeSectionResponse> responses = await _unitOfWork.ReadSectionsResponse();

            ResponseResult<IEnumerable<HomeSectionResponse>> response = new(responses);

            return Ok(response);
        }
    }
}