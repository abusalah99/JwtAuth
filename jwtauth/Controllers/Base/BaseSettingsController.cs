namespace jwtauth.Controllers;

public class BaseSettingsController<TEntity> : BaseController<TEntity>
    where TEntity : BaseEntitySettings
{
    private readonly IBaseSettingsUnitOfWork<TEntity> _baseSettingsUnitOfWork;
    public BaseSettingsController(IBaseSettingsUnitOfWork<TEntity> unitOfWork) : base(unitOfWork) 
        => _baseSettingsUnitOfWork = unitOfWork;

    [HttpGet("Search/{searchText}")]
    public virtual async Task<IEnumerable<TEntity>> Search([FromRoute] string searchText)
        => await _baseSettingsUnitOfWork.Search(searchText);

}