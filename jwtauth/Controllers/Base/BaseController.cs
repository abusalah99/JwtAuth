namespace jwtauth.Controllers;

public class BaseController<TEntity> : ControllerBase
    where TEntity : BaseEntity
{
    private readonly IBaseUnitOfWork<TEntity> _unitOfWork;
    public BaseController(IBaseUnitOfWork<TEntity> unitOfWork) => _unitOfWork = unitOfWork;

    public virtual async Task<IActionResult> Post(TEntity entity)
    {
        await _unitOfWork.Create(entity);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} created");

        return Ok(response);
    }

    public virtual async Task<IActionResult> Get()
    {

        IEnumerable<TEntity> entities = await _unitOfWork.Read();

        ResponseResult<IEnumerable<TEntity>> response = new(entities);

        return Ok(response);
    }
    public virtual async Task<IActionResult> Get(Guid id) 
    {
        TEntity entity = await _unitOfWork.Read(id);

        ResponseResult<TEntity> response = new(entity); 

        return Ok(response);
    }

    public async virtual Task<IActionResult> Put(TEntity entity)
    {
        await _unitOfWork.Update(entity);

        ResponseResult<TEntity> response = new(entity);

        return Ok(response);
    }

    public async virtual Task<IActionResult> Delete(Guid id) 
    {
        await _unitOfWork.Delete(id);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} Deleted");

        return Ok(response);
    }
    protected void SetCookie(string name, string value, DateTime expireTime)
    => Response.Cookies.Append(name, value
        , new CookieOptions()
        {
            HttpOnly = true,
            Expires = expireTime
        });

}
