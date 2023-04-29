namespace jwtauth;

public class HomeSectionRepository : BaseRepositiorySettings<HomeSection>, IHomeSectionRepository
{
    public HomeSectionRepository(ApplicationDbContext context) : base(context) { }
}
