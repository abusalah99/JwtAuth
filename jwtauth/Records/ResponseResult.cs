namespace jwtauth;
public class ResponseResult<T>
{
    public bool Status { get; set; }
    public int ErrorNumber { get; set; }
    public T Response { get; set; }
}
