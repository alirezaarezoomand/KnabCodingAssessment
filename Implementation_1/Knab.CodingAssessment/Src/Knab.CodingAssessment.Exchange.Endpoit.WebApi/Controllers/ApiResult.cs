namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Controllers
{
    public class ApiResult<TResult>
    {
        public bool Succeeded { get; set; }
        public TResult? Result { get; set; }
        public string Message { get; set; }

        public static ApiResult<TResult> Ok(TResult result)
        {
            return new ApiResult<TResult>
            {
                Succeeded = true,
                Result = result,
                Message = string.Empty
            };
        }
    }

    public class ApiResult : ApiResult<object>
    {
        public static ApiResult Ok()
        {
            return new ApiResult
            {
                Succeeded = true,
                Result = null,
                Message = string.Empty
            };
        }

        public static ApiResult Error(string error)
        {
            return new ApiResult
            {
                Succeeded = false,
                Result = null,
                Message = error
            };
        }
    }
}