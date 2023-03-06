namespace jwtauth;
public class BaseUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repository;
    public BaseUnitOfWork(IBaseRepository<TEntity> repository) => _repository = repository;

    public virtual async Task<IEnumerable<TEntity>> Read() => await _repository.Get();
    public virtual async Task<TEntity> Read(Guid id) => await _repository.Get(id);

    public virtual async Task Create(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Add(entity);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }

        await transaction.CommitAsync();
    }
    public virtual async Task Delete(Guid id)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Remove(id);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }

        await transaction.CommitAsync();
    }
    public virtual async Task Update(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Update(entity);

            transaction.Commit();
        }
        catch 
        {
            transaction.Rollback();
        }

        await transaction.CommitAsync();
    }
}
