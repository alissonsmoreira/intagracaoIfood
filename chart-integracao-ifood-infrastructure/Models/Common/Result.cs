namespace chart_integracao_ifood_infrastructure.Models.Common
{
    public class Result
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public static Result Ok() => new()
        {
            Success = true
        };

        public static Result Erro(string message) => new()
        {
            Success = false,
            Message = message
        };
    }

    public class Result<T> : Result
    {
        public T Content { get; private set; }

        public static Result<T> Ok(T content) => new()
        {
            Success = true,
            Content = content
        };

        public static new Result<T> Erro(string message) => new()
        {
            Success = false,
            Message = message,
            Content = default
        };
    }
}
