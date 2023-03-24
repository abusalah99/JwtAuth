namespace jwtauth;

public class UserRepository : BaseRepositiorySettings<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<User> Get(Guid id)
        => await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Id == id);
    public override async Task<IEnumerable<User>> Get()
        => await dbSet.Include(e => e.Token).ToListAsync();    
      
    public async Task<User>? GetByMail(string mail)
        => await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Email == mail);

    public async Task DeleteByMail(string mail)
    {
        User? userFromDb = await GetByMail(mail);
        if(userFromDb == null)
            throw  new ArgumentException("user was not found");

        await Task.Run(()=>dbSet.Remove(userFromDb));
        await SaveChangesAsync();
    }

    public async Task<User>? GetByToken(string token)
      => await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Token.Value == token);

}
