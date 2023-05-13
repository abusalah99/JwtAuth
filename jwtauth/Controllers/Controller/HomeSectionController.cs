namespace jwtauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSectionController : BaseSettingsController<HomeSection>
    {
        public HomeSectionController(IHomeSectionUnitOfWork unitOfWork)
                 : base(unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> Get() => await Read();
    }
}