using System;

namespace DotnetToolbox.Helpers
{
    public class Result<T>
    {
        public T Value { get; }
        public string Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(T value, string error, bool isSuccess)
        {
            Value = value;
            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result<T> Ok(T value) => new(value, null, true);
        public static Result<T> Fail(string error) => new(default, error, false);

        public Result<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            return IsSuccess
                ? Result<TNew>.Ok(mapper(Value))
                : Result<TNew>.Fail(Error);
        }

        public Result<TNew> Bind<TNew>(Func<T, Result<TNew>> binder)
        {
            return IsSuccess ? binder(Value) : Result<TNew>.Fail(Error);
        }

        public T GetOrDefault(T defaultValue = default)
        {
            return IsSuccess ? Value : defaultValue;
        }

        public void Match(Action<T> onSuccess, Action<string> onFailure)
        {
            if (IsSuccess) onSuccess(Value);
            else onFailure(Error);
        }
    }

    public static class Result
    {
        public static Result<T> Try<T>(Func<T> action)
        {
            try
            {
                return Result<T>.Ok(action());
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex.Message);
            }
        }
    }
}
