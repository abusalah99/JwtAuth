namespace jwtauth;

public interface IBaseRepositiorySettings<TEntity> :
    IBaseRepository<TEntity> where TEntity : BaseEntitySettings
{
    Task<IEnumerable<TEntity>> Search(string searchText);

}
