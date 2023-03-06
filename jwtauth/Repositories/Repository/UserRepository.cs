namespace jwtauth;
public class UserRepository : BaseRepositiorySettings<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User>? GetByMail(string mail)
        => await dbSet.FirstOrDefaultAsync(e => e.Email == mail);
    public async Task DeleteByMail(string mail)
    {
        User? userFromDb = await GetByMail(mail);
        if(userFromDb == null)
            throw  new ArgumentException("user was not found");

        await Task.Run(()=>dbSet.Remove(userFromDb));
        await SaveChangesAsync();
    }

}
