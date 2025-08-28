using System.Buffers;

namespace ContactManager.Domain.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; }
        public OperationStatus Status { get; }
        public string? Error { get; }

        private Result(bool isSuccess, T value, OperationStatus status, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Status = status;
            Error = error;
        }

        public static Result<T> Success(T value) =>
            new Result<T>(true, value, OperationStatus.Success, null);

        public static Result<T> Failure(OperationStatus status, string error)
        {
            if (status == OperationStatus.Success)
            {
                throw new ArgumentException("Não se pode criar uma falha com status de sucesso.", nameof(status));
            }
            return new Result<T>(false, default(T)!, status, error);
        }

        public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
        {
            return IsSuccess
                ? Result<TOut>.Success(mapper(Value))
                : Result<TOut>.Failure(Status, Error!);
        }
    }
}