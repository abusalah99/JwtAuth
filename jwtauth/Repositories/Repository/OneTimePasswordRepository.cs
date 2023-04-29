namespace jwtauth;

public class OneTimePasswordRepository : BaseRepository<OneTimePassword>, IOneTimePasswordRepository
{
    public OneTimePasswordRepository(ApplicationDbContext context) : base(context) { }
}
