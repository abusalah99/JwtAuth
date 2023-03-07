namespace jwtauth.Response
{
    public class SuccessResponse<T> : IResponse<T>
    {
        public ResponseResult<T> CreateResponse(T Response)
              => new()
              {
                  Status = true,
                  ErrorNumber = 202,
                  Response = Response
              };
    }
}
