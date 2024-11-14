namespace PGApi.PGApi.Shared
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public object Data { get; private set; }

        private Result(bool isSuccess, string errorMessage = null, object data = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Result Success(object data = null)
        {
            return new Result(true, null, data);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage, null);
        }
    }
}
