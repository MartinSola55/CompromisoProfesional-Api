namespace CompromisoProfesional_Api.Models.DTO
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = null!;
        public ErrorResponse? Error { get; set; }
        public bool Success => Error == null;

        public class ErrorResponse
        {
            public string Message { get; set; } = null!;
            public int Code { get; set; }
        }

        public BaseResponse<T> SetError(string error)
        {
            Error = new ErrorResponse
            {
                Message = error,
                Code = 400
            };
            return this;
        }

        public BaseResponse<T> Attach(BaseResponse<T> response)
        {
            if (Error == null && response.Error != null)
            {
                Error = response.Error;
            }

            return this;
        }
    }

    public class BaseResponse
    {
        public object Data { get; set; } = new();
        public string Message { get; set; } = null!;
        public ErrorResponse? Error { get; set; }
        public bool Success => Error == null;

        public class ErrorResponse
        {
            public string Message { get; set; } = null!;
            public int Code { get; set; }
        }

        public BaseResponse SetError(string error)
        {
            Error = new ErrorResponse
            {
                Message = error,
                Code = 400
            };
            return this;
        }

        public BaseResponse Attach(BaseResponse response)
        {
            if (Error == null && response.Error != null)
            {
                Error = response.Error;
            }

            return this;
        }
    }
}
