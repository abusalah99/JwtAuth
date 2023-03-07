namespace jwtauth;
public interface IResponse<T>
{
    public ResponseResult<T> CreateResponse(T Response);
}
