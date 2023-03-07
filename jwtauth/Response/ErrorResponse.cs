namespace jwtauth;
public class ErrorResponse : IResponse<string>
{
    public ResponseResult<string> CreateResponse(string Response)
          => new() 
          {
              Status = false,
              ErrorNumber= 500
              , Response = Response
          };   
}
