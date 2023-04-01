/*namespace jwtauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSectionController : BaseSettingsController<HomeSection>
    {
        public HomeSectionController(IHomeSectionUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        [HttpGet, Authorize]
        public override async Task<IActionResult> Get() => await base.Get();

        [HttpPost*//*, Authorize(Roles = "Admin")*//*]
        public async Task<IActionResult> Post([FromForm] SectionRequest homeSectionRequest) {
            HomeSection homeSection = new()
            {
                Name= homeSectionRequest.Name,
                Image = byte.Parse(homeSectionRequest.Image),
                SectionText= homeSectionRequest.SectionText,
            };
             await base.Post(homeSection);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Put(HomeSection homeSection)
            => await base.Put(homeSection);

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete (Guid id)
            => await base.Delete(id);
    }
}*/